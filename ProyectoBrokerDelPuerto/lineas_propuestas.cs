using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class lineas_propuestas
    {
        string sql = "";
        string columns = "id_propuesta,documento,tipo_documento,  apellidos, nombres,  fecha_nacimiento, id_actividad,"+
            " id_clasificacion, premio ,ultmod,user_edit,codestado,prefijo,actividad,clasificacion,fechaDesde, fechaHasta, idprefijo,codempresa ";
        public string id, id_propuesta,documento, tipo_documento, apellidos, nombres, fecha_nacimiento, id_actividad,
              id_clasificacion, premio, ultmod, user_edit, codestado, prefijo, actividad, clasificacion, fechaDesde, fechaHasta = "", idprefijo = "",codempresa = "";
        conexion con = new conexion();

        public lineas_propuestas(bool inst = false)
        {
            if(inst)
                this.install();
        }


        private void install()
        {
            //sql = "DROP TABLE lineas_propuestas";
            //con.query(sql);
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists lineas_propuestas (id serial PRIMARY KEY,  id_propuesta INT(15) NULL, documento VARCHAR(100) NULL, tipo_documento VARCHAR(100) NULL, apellidos VARCHAR(200) NULL , nombres VARCHAR(200) NULL , fecha_nacimiento DATE NULL,  " +
                "id_actividad VARCHAR(100) NULL,id_clasificacion VARCHAR(100) NULL, premio DOUBLE NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists lineas_propuestas (id INTEGER PRIMARY KEY,  id_propuesta INT(15) NULL, documento VARCHAR(100) NULL, tipo_documento VARCHAR(100) NULL, apellidos VARCHAR(200) NULL , nombres VARCHAR(200) NULL , fecha_nacimiento DATE NULL,  " +
                "id_actividad VARCHAR(100) NULL,id_clasificacion VARCHAR(100) NULL, premio DOUBLE NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }

            con.query(sql);
            this.addColumn();

            if (MDIParent1.baseDatos == "MySql")
            {

                string sql1 = "SELECT COUNT(1) as cant FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE()   AND TABLE_NAME = 'lineas_propuestas'  AND INDEX_NAME = 'idx.id_propuesta'; ";
                DataSet ds = con.query(sql1);
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "1")
                {
                    sql1 = "CREATE INDEX `idx.id_propuesta` ON `lineas_propuestas` (`id_propuesta`);";
                    con.query(sql1);
                }
                sql1 = "SELECT COUNT(1) as cant FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE()   AND TABLE_NAME = 'lineas_propuestas'  AND INDEX_NAME = 'idx.prefijo'; ";
                ds = con.query(sql1);
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "1")
                {
                    sql1 = "CREATE INDEX `idx.prefijo` ON `lineas_propuestas` (`prefijo`);";
                    con.query(sql1);
                }
                sql1 = "SELECT COUNT(1) as cant FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE()   AND TABLE_NAME = 'lineas_propuestas'  AND INDEX_NAME = 'idx.idprefijo'; ";
                ds = con.query(sql1);
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "1")
                {
                    sql1 = "CREATE INDEX `idx.idprefijo` ON `lineas_propuestas` (`idprefijo`);";
                    con.query(sql1);
                }

            }

            con.query("UPDATE lineas_propuestas SET codempresa = '" + MDIParent1.codempresa + "' WHERE (codempresa IS NULL OR codempresa = '')  AND ultmod > '" + DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss") + "' ");

        }

        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                try
                {
                    //Add column categoria if NOT exist
                    if (con.query("SHOW COLUMNS FROM lineas_propuestas WHERE Field = 'prefijo' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE lineas_propuestas ADD COLUMN prefijo VARCHAR(50);");
                    if (con.query("SHOW COLUMNS FROM lineas_propuestas WHERE Field = 'actividad' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE lineas_propuestas ADD COLUMN actividad VARCHAR(300);");
                    if (con.query("SHOW COLUMNS FROM lineas_propuestas WHERE Field = 'clasificacion' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE lineas_propuestas ADD COLUMN clasificacion VARCHAR(300);");
                    if (con.query("SHOW COLUMNS FROM lineas_propuestas WHERE Field = 'fechaDesde' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE lineas_propuestas ADD COLUMN fechaDesde DATETIME NULL;");
                    if (con.query("SHOW COLUMNS FROM lineas_propuestas WHERE Field = 'fechaHasta' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE lineas_propuestas ADD COLUMN fechaHasta DATETIME NULL;");
                    if (con.query("SHOW COLUMNS FROM lineas_propuestas WHERE Field = 'idprefijo' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE lineas_propuestas ADD COLUMN idprefijo DOUBLE NOT NULL;");
                    if (con.query("SHOW COLUMNS FROM lineas_propuestas WHERE Field = 'codempresa' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE lineas_propuestas ADD COLUMN codempresa VARCHAR(150) NULL;");
                }
                catch
                {
                    //
                }
            }
            else
            {
                con.query("ALTER TABLE lineas_propuestas ADD COLUMN prefijo VARCHAR(50);");
                con.query("ALTER TABLE lineas_propuestas ADD COLUMN actividad VARCHAR(300);");
                con.query("ALTER TABLE lineas_propuestas ADD COLUMN clasificacion VARCHAR(300);");
                con.query("ALTER TABLE lineas_propuestas ADD COLUMN fechaDesde DATETIME NULL;");
                con.query("ALTER TABLE lineas_propuestas ADD COLUMN fechaHasta DATETIME NULL;");
                con.query("ALTER TABLE lineas_propuestas ADD COLUMN idprefijo DOUBLE  NULL;");
                con.query("ALTER TABLE lineas_propuestas ADD COLUMN codempresa VARCHAR(150) NULL;");
            }

        }

        public DataSet get_idpropuesta(string idpropuesta_, string prefijo_ )
        {
            DataSet ds = new DataSet();
            lineas_propuestas lin = new lineas_propuestas();
            sql = "SELECT *,(SELECT cod  FROM actividades t1 WHERE t1.id = t2.id_actividad) as cod_actividad, (SELECT cod  FROM clasificaciones t1 WHERE t1.id = t2.id_clasificacion) as cod_clasificacion, (SELECT SUM(t1.prima)  FROM propuestas t1 WHERE t1.prefijo = t2.prefijo AND t1.idpropuesta = t2.id_propuesta) as prima, " +
                " (SELECT SUM(t1.referencia) FROM propuestas t1 WHERE t1.prefijo = t2.prefijo AND t1.idpropuesta = t2.id_propuesta) as referencia," +
                " (SELECT SUM(t1.nota) FROM propuestas t1 WHERE t1.prefijo = t2.prefijo AND t1.idpropuesta = t2.id_propuesta) as nota FROM lineas_propuestas t2 WHERE t2.idprefijo = '" +
                idpropuesta_ + "' AND t2.prefijo = '"+prefijo_+"' GROUP BY t2.documento ";

            Console.WriteLine("\n\n\n----> " + sql);

            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar lineas_propuestas");
            }

            return ds;
        }

        public DataSet get_entre_controlventas_efectivo(string fecha1, string fecha2, string refe = "")
        {
            if (refe != "")
            {
                refe = " AND t0.referencia = '" + refe + "' ";
            }
            DataSet ds = new DataSet();
            if (MDIParent1.baseDatos == "SQlite")
            {
                sql = "SELECT t0.id,t0.idpropuesta,t0.prima,t0.prefijo,t0.referencia,t0.tipopago,t0.fecha_paga,t0.compformapago,t0.formadepago,t0.premio_total,t0.documento,t0.fechaDesde,t0.fechaHasta,t0.paga, DATE(t0.ultmod) as ultmod, t0.meses," +
                         "t0.id_cobertura,t0.user_edit,t3.nombres,t3.apellidos,t0.nota," +
                     " (SELECT (t1.nombres || ' ' || t1.apellidos) FROM clientes t1 WHERE t1.id = t0.documento LIMIT 1) as nombre,t0.codestado, t3.premio, " +
                     " t3.fecha_nacimiento, t3.actividad, t3.clasificacion " +
                     " FROM propuestas t0 INNER JOIN  lineas_propuestas t3 ON " +
                     " t0.prefijo = t3.prefijo AND t0.idpropuesta = t3.id_propuesta   WHERE   DATE(t0.fecha_paga) >= '" + fecha1 + "'  AND DATE(t0.fecha_paga) <= '" + fecha2 +
                     "' AND t0.paga = 1  AND (t0.formadepago = 'CONTADO' OR (t0.formadepago = 'CREDITO' AND t0.tipopago = 'EFECTIVO'))  AND t0.codestado > 0  " + refe + 
                     "  AND t0.user_edit LIKE '%" + this.user_edit + "%'  GROUP BY t3.prefijo,t3.id_propuesta,t3.documento ";

            }
            else
            {

                sql = "SELECT t0.id,t0.idpropuesta,t0.prima,t0.prefijo,t0.referencia,t0.tipopago,t0.fecha_paga,t0.compformapago,t0.formadepago,t0.premio_total,t0.documento,t0.fechaDesde,t0.fechaHasta,t0.paga, DATE(t0.ultmod) as ultmod, t0.meses," +
                    "t0.id_cobertura,t0.user_edit,t3.nombres,t3.apellidos,t0.nota," +
                " (SELECT CONCAT(t1.nombres, ' ', t1.apellidos) FROM clientes t1 WHERE t1.id = t0.documento LIMIT 1) as nombre,t0.codestado, t3.premio, " +
                " t3.fecha_nacimiento, t3.actividad, t3.clasificacion " +
                " FROM propuestas t0 INNER JOIN  lineas_propuestas t3 ON " +
                " t0.prefijo = t3.prefijo AND t0.idpropuesta = t3.id_propuesta  WHERE   DATE(t0.fecha_paga) >= '" + fecha1 + "'  AND DATE(t0.fecha_paga) <= '" + fecha2 +
                "' AND t0.paga = 1  AND (t0.formadepago = 'CONTADO' OR (t0.formadepago = 'CREDITO' AND t0.tipopago = 'EFECTIVO')) AND t0.codestado > 0  " + refe + 
                " AND t0.user_edit LIKE '%" + this.user_edit + "%' GROUP BY t3.prefijo,t3.id_propuesta,t3.documento ";

            }

            Console.WriteLine("\n SQL INF EFECTIVO " + sql);

            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar lineas_propuestas");
            }

            return ds;
        }

        public DataSet get_entre_controlventas(string fecha1, string fecha2, string refe = "")
        {
            if(refe != "")
            {
                refe = " AND t0.referencia = '" + refe + "' ";
            }
            DataSet ds = new DataSet();
            if (MDIParent1.baseDatos == "SQlite")
            {
                sql = "SELECT t0.organizador,t0.productor,t0.id,t0.idpropuesta,t0.prima,t0.prefijo,t0.referencia,t0.tipopago,t0.fecha_paga,t0.compformapago,t0.formadepago,t0.premio_total,t0.documento,t0.fechaDesde,t0.fechaHasta,t0.paga, DATE(t0.ultmod) as ultmod, t0.meses," +
                         "t0.id_cobertura,t0.user_edit,t3.nombres,t3.apellidos,t0.nota," +
                     " (SELECT (t1.nombres ||  ' ' || t1.apellidos) FROM clientes t1 WHERE t1.id = t0.documento LIMIT 1) as nombre, " +
                     " (SELECT t1.email FROM clientes t1 WHERE t1.id = t0.documento LIMIT 1) as correo, " +
                     " (SELECT t1.telefono FROM clientes t1 WHERE t1.id = t0.documento LIMIT 1) as telefono, t0.codestado, t3.premio, " +
                     " (SELECT t1.fecha_nacimiento FROM clientes t1 WHERE t1.id = t0.documento LIMIT 1) as nacimientotomador, " +
                     " t3.fecha_nacimiento, t3.actividad, t3.clasificacion " +
                     " FROM propuestas t0 INNER JOIN  lineas_propuestas t3 ON " +
                     " t0.prefijo = t3.prefijo AND t0.idpropuesta = t3.id_propuesta   WHERE   DATE(t0.ultmod) >= '" + fecha1 + "'  AND DATE(t0.ultmod) <= '" + fecha2 +
                     "'  AND t0.codestado > 0  "+refe+"  AND t0.user_edit LIKE '%" + this.user_edit + "%' ";

            }
            else
            {
               
                    sql = "SELECT t0.organizador,t0.productor,t0.id,t0.idpropuesta,t0.prima,t0.prefijo,t0.referencia,t0.tipopago,t0.fecha_paga,t0.compformapago,t0.formadepago,t0.premio_total,t0.documento,t0.fechaDesde,t0.fechaHasta,t0.paga, DATE(t0.ultmod) as ultmod, t0.meses," +
                        "t0.id_cobertura,t0.user_edit,t3.nombres,t3.apellidos,t0.nota," +
                    " (SELECT CONCAT(t1.nombres, ' ', t1.apellidos) FROM clientes t1 WHERE t1.id = t0.documento LIMIT 1) as nombre, " +
                     " (SELECT t1.email FROM clientes t1 WHERE t1.id = t0.documento LIMIT 1) as correo, " +
                     " (SELECT t1.telefono FROM clientes t1 WHERE t1.id = t0.documento LIMIT 1) as telefono, t0.codestado, t3.premio, " +
                     " (SELECT t1.fecha_nacimiento FROM clientes t1 WHERE t1.id = t0.documento LIMIT 1) as nacimientotomador, " +
                    " t3.fecha_nacimiento, t3.actividad, t3.clasificacion " +
                    " FROM propuestas t0 INNER JOIN  lineas_propuestas t3 ON " +
                    " t0.prefijo = t3.prefijo AND t0.idpropuesta = t3.id_propuesta  WHERE   DATE(t0.ultmod) >= '" + fecha1 + "'  AND DATE(t0.ultmod) <= '" + fecha2 +
                    "'  AND t0.codestado > 0  " + refe + " AND t0.user_edit LIKE '%" + this.user_edit + "%' ";

            }

            Console.WriteLine("\n SQL "+sql);

            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar lineas_propuestas");
            }

            return ds;
        }

        public DataSet get_entrefechaslineas(string fec1, string fec2, string referencia_)
        {
            DataSet ds = new DataSet();

            if(referencia_ != "")
            {
                referencia_ = " AND t2.referencia  LIKE '%" + referencia_ + "%' ";
            }

            if (MDIParent1.baseDatos == "MySql")
            {
                //" (SELECT SUM(t0.primaemitida)  FROM controlventas t0 WHERE t0.codgrupo = CONCAT(t1.prefijo,t1.id_propuesta) ) as prima  " +
                sql = "SELECT t1.premio, t1.tipo_documento, t1.documento, t1.apellidos, t1.nombres, t1.id_propuesta, t1.prefijo,t1.ultmod,t1.user_edit," +
                " t2.premio_total, t2.referencia,t2.prima,t2.nota" +
                " FROM lineas_propuestas t1 INNER JOIN propuestas t2 ON t1.prefijo = t2.prefijo AND t2.idpropuesta = t1.id_propuesta"+
                " WHERE " +
                " DATE(t1.ultmod) >= '" + fec1 + "'  AND DATE(t1.ultmod) <= '" + fec2 +
                "'  AND t2.codestado > 0  AND t1.user_edit LIKE '%" + this.user_edit + "%' " + referencia_ + "   ORDER BY t1.ultmod ASC ";
            }
            else
            {
                sql = "SELECT t1.premio, t1.tipo_documento, t1.documento, t1.apellidos, t1.nombres, t1.id_propuesta, t1.prefijo,t1.ultmod,t1.user_edit," +
                " t2.premio_total, t2.referencia,t2.prima,t2.nota" +
                " FROM lineas_propuestas t1 INNER JOIN propuestas t2 ON t1.prefijo = t2.prefijo AND t2.idpropuesta = t1.id_propuesta" +
                " WHERE " +
                " DATE(t1.ultmod) >= '" + fec1 + "'  AND DATE(t1.ultmod) <= '" + fec2 +
                "'  AND t2.codestado > 0  AND t1.user_edit LIKE '%" + this.user_edit + "%'  " + referencia_ + "  ORDER BY t1.ultmod ASC ";
            }

            Console.WriteLine("---> "+sql);

            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar lineas_propuestas");
            }

            return ds;
        }


        public DataSet get(string id_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM lineas_propuestas WHERE codestado = 1 AND id= '" + id_ + "' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar lineas_propuestas");
            }

            return ds;
        }

        public DataSet get_idprefijo(string idPropuesta_, string prefijo_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM lineas_propuestas WHERE  id_propuesta= '" + idPropuesta_ + "' AND prefijo = '" + prefijo_ + "' ";
            try
            {
               Console.WriteLine("CONSULTA "+sql);
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar lineas_propuestas");
            }

            return ds;
        }

        
        public DataSet get_all_findia(string fecha_)
        {
            DataSet ds = new DataSet();
            sql = "SELECT *, "+
                " (SELECT t0.codestado FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1) as codestado_, "+
                " (SELECT t0.promocion FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as promocion_ ," +
                " (SELECT t0.meses FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as meses_, " +
                " (SELECT t0.premio_total FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as premio_total, " +
                " (SELECT t0.master FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as master, " +
                " (SELECT t0.organizador FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as organizador, " +
                " (SELECT t0.productor FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as productor, " +
                " (SELECT t0.direccion FROM clientes t0 WHERE t0.id = documento LIMIT 1 ) as direccion, " +
                " (SELECT t0.codpostal FROM clientes t0 WHERE t0.id = documento LIMIT 1 ) as codpostal, " +
                " (SELECT t0.localidad FROM clientes t0 WHERE t0.id = documento LIMIT 1 ) as localidad " +
                " FROM lineas_propuestas WHERE DATE(ultmod) = '" + fecha_ + "'  ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar lineas_propuestas");
            }

            return ds;
        }
        
        public DataSet get_all_findia_col(string fecha_)
        {
            DataSet ds = new DataSet();
            sql = "SELECT *, " +
                " (SELECT t0.codestado FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1) as codestado_, " +
                " (SELECT t0.promocion FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as promocion_ ," +
                " (SELECT t0.meses FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as meses_, " +
                " (SELECT t0.premio_total FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as premio_total, " +
                " (SELECT t0.master FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as master, " +
                " (SELECT t0.clausula FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as clausula, " +
                " (SELECT t0.nueva_poliza FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as nueva_poliza, " +
                " (SELECT t0.organizador FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as organizador, " +
                " (SELECT t0.productor FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as productor, " +
                " (SELECT t0.cobertura_suma FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as cobertura_suma, " +
                " (SELECT t0.cobertura_gastos FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as cobertura_gastos, " +
                " (SELECT t0.id_cobertura FROM propuestas t0 WHERE t0.prefijo = prefijo AND t0.idpropuesta = id_propuesta LIMIT 1 ) as id_cobertura, " +
                " (SELECT t0.direccion FROM clientes t0 WHERE t0.id = documento LIMIT 1 ) as direccion, " +
                " (SELECT t0.codpostal FROM clientes t0 WHERE t0.id = documento LIMIT 1 ) as codpostal, " +
                " (SELECT t0.localidad FROM clientes t0 WHERE t0.id = documento LIMIT 1 ) as localidad, " +
                " (SELECT t0.sexo FROM clientes t0 WHERE t0.id = documento LIMIT 1 ) as sexo " +
                " FROM lineas_propuestas WHERE DATE(ultmod) = '" + fecha_ + "'  ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar lineas_propuestas");
            }

            return ds;
        }

        public DataSet get_all_date(string fecha)
        {
            DataSet ds = new DataSet();
            sql = "SELECT * FROM lineas_propuestas WHERE DATE(ultmod) >= '"+fecha+"' AND DATE(ultmod) <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar lineas_propuestas");
            }

            return ds;
        }

        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM lineas_propuestas WHERE codestado = 1";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar lineas_propuestas");
            }

            return ds;
        }


        public DataSet get_all_busqueda(string coincidencia)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM lineas_propuestas WHERE codestado = 1 AND " +
                " ( nombre LIKE  '%" + coincidencia + "%' OR id LIKE  '%" + coincidencia + "%' )";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar lineas_propuestas");
            }

            return ds;
        }

        public void borrarRegistros()
        {
            sql = "DELETE FROM lineas_propuestas";
            con.query(sql);
        }

        private bool exist()
        {
            sql = "SEECT COUNT(1) as cant FROM lineas_propuestas WHERE  prefijo = '" + this.prefijo + "' AND id_propuesta = '" + this.id_propuesta + "'";
            
            if (con.query(sql).Tables[0].Rows[0]["cant"].ToString() != "0")
                return true;

            return false;
        }

        public bool save_nube()
        {
            try
            {
                if (!this.exist())
                {
                    sql = "INSERT INTO lineas_propuestas (" + this.columns + ") VALUES(" +
                   "'" + this.id_propuesta + "'," +
                   "'" + this.documento + "'," +
                   "'" + this.tipo_documento + "'," +
                   "'" + this.apellidos + "'," +
                   "'" + this.nombres + "'," +
                   "'" + this.fecha_nacimiento + "'," +
                   "'" + this.id_actividad + "'," +
                   "'" + this.id_clasificacion + "'," +
                   "'" + this.premio + "'," +
                   "'" + this.ultmod + "'," +
                   "'" + this.user_edit + "'," +
                   "'" + this.codestado + "'," +
                   "'" + this.prefijo + "'," +
                   "'" + this.actividad + "'," +
                   "'" + this.clasificacion + "'," +
                   "'" + this.fechaDesde + "'," +
                   "'" + this.fechaHasta + "'," +
                   "'" + this.idprefijo + "'," +
                   "'" + MDIParent1.codempresa + "'" +
                   ") ";


                    con.query(sql);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public void save_concat(string sql1)
        {
            con.query("INSERT INTO lineas_propuestas (" + this.columns + ") VALUES " + sql1);
        }

        public string concat_sql()
        {
            try
            {
                sql = "(" +
                    "'" + this.id_propuesta + "'," +
                    "'" + this.documento + "'," +
                    "'" + this.tipo_documento + "'," +
                    "'" + this.apellidos + "'," +
                    "'" + this.nombres + "'," +
                    "'" + this.fecha_nacimiento + "'," +
                    "'" + this.id_actividad + "'," +
                    "'" + this.id_clasificacion + "'," +
                    "'" + this.premio + "'," +
                    "'" + this.ultmod + "'," +
                    "'" + this.user_edit + "'," +
                    "'" + this.codestado + "'," +
                    "'" + this.prefijo + "'," +
                    "'" + this.actividad + "'," +
                    "'" + this.clasificacion + "'," +
                    "'" + this.fechaDesde + "'," +
                    "'" + this.fechaHasta + "'," +
                    "'" + this.idprefijo + "'," +
                    "'" + MDIParent1.codempresa + "'" +
                    ") ";


                return sql;

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public string concat_sql_delete()
        {
            return  " ( prefijo = '"+this.prefijo+"' AND  id_propuesta = '" + this.id_propuesta+"' ) ";
        }



        public bool save()
        {
            try
            {
                    sql = "INSERT INTO lineas_propuestas (" + this.columns + ") VALUES(" +
                        "'" + this.id_propuesta + "'," +
                        "'" + this.documento + "'," +
                        "'" + this.tipo_documento + "'," +
                        "'" + this.apellidos + "'," +
                        "'" + this.nombres + "'," +
                        "'" + this.fecha_nacimiento + "'," +
                        "'" + this.id_actividad + "'," +
                        "'" + this.id_clasificacion + "'," +
                        "'" + this.premio + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.prefijo + "'," +
                        "'" + this.actividad + "'," +
                        "'" + this.clasificacion + "'," +
                        "'" + this.fechaDesde + "'," +
                        "'" + this.fechaHasta + "'," +
                        "'" + this.idprefijo + "'," +
                        "'" + MDIParent1.codempresa + "'" +
                        ") ";


                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public void delete_idpropuesta(string idprefijo_, string prefijo_)
        {
            sql = "DELETE FROM lineas_propuestas WHERE idprefijo = '" + idprefijo_ + "' AND prefijo = '"+prefijo_+"' ";
            con.query(sql);
        }

        public void delete_concat(string sqlconcat)
        {
            sql = "DELETE FROM lineas_propuestas WHERE "+ sqlconcat;
            con.query(sql);
        }

        public void delete()
        {
            sql = "UPDATE lineas_propuestas SET codestado = 0 WHERE id = '" + this.id + "' ";
            con.query(sql);
        }
    }
}
