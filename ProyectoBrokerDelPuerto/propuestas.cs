using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ProyectoBrokerDelPuerto
{
    class propuestas
    {
        string sql = "";
        //Si codestado es 1. Està vigente la propuesta, 2. Ya no està vigente, 0. Anulada
        string columns = "documento,  num_polizas, meses, id_cobertura, id_barrio, nueva_poliza, premio, premio_total, fechaDesde ,fechaHasta,clausula, barrio_beneficiario, ultmod, " +
            "user_edit,codestado, cobertura_suma, cobertura_deducible, cobertura_gastos,promocion,paga,fecha_paga,referencia,prima,master,organizador,productor,"+
            "prefijo,formadepago,usuariopaga, tipopago, compformapago,idpropuesta,envionube,codempresa,nota,data_barrios,version";
        public string  id, documento,  num_polizas, meses, id_cobertura, id_barrio, nueva_poliza, premio, premio_total, fechaDesde, fechaHasta,clausula, barrio_beneficiario,ultmod, 
            user_edit, codestado, promocion, master, organizador, productor, usuariopaga, tipopago, compformapago,idpropuesta, codempresa,nota;
        public string paga = "1", envionube = "0", cobertura_suma="0", cobertura_deducible = "0", cobertura_gastos = "0";
        public string fecha_paga = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string referencia = "", prefijo = "", formadepago = "CONTADO";
        public string prima = "0", nombre = "", datos;
        public string fecha_nacimiento;
        public bool denube = false;
        public int version { get; set; } = 0;
        public string data_barrios { get; set; }

        conexion con = new conexion();

        public propuestas(bool inst = false)
        {
            
            if(inst)
                this.install();
        }


        private void install()
        {
            //sql = "DROP TABLE propuestas";
            //con.query(sql);
            bool  sicambiotabla = false;
            try
            {
                /*CAMBIO DE TPO DE DATO*/
                if (MDIParent1.baseDatos == "SQlite")
                {

                    sql = "SELECT count(1) as contar FROM propuestas WHERE idpropuesta LIKE '%.%'; ";
                    DataSet dsss = con.query(sql);
                    if (dsss.Tables[0].Rows[0]["contar"].ToString() != "0")
                    {
                        sql = "ALTER TABLE propuestas RENAME  To propuestas2;";
                        con.query(sql);
                        sicambiotabla = true;
                    }
                }
            }
            catch
            {
                //
            }

            


            if (MDIParent1.baseDatos == "MySql")
            {
                
                sql = "CREATE TABLE if not exists propuestas (id serial PRIMARY KEY, documento VARCHAR(100) NULL,  num_polizas INT(5) NULL, meses INT(2) NULL, id_cobertura VARCHAR(100) NULL, " +
                "id_barrio VARCHAR(100) NULL,nueva_poliza INT(2) NULL, premio DOUBLE NULL, premio_total DOUBLE NULL, fechaDesde DATETIME NULL, fechaHasta DATETIME NULL, clausula INT(2) NULL," +
                " barrio_beneficiario INT(2) NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL,cobertura_suma DOUBLE NULL, " +
                " cobertura_deducible DOUBLE NULL, cobertura_gastos DOUBLE NULL,paga int(1) NULL,fecha_paga DATETIME NULL, referencia VARCHAR(50) NULL , prima double NULL );";

            }
            else
            {
                sql = "CREATE TABLE if not exists propuestas (id INTEGER PRIMARY KEY, documento VARCHAR(100) NULL,  num_polizas INT(5) NULL, meses INT(2) NULL, id_cobertura VARCHAR(100) NULL, " +
                "id_barrio VARCHAR(100) NULL,nueva_poliza INT(2) NULL, premio DOUBLE NULL, premio_total DOUBLE NULL, fechaDesde DATETIME NULL, fechaHasta DATETIME NULL, clausula INT(2) NULL," +
                " barrio_beneficiario INT(2) NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL,cobertura_suma DOUBLE NULL, " +
                " cobertura_deducible DOUBLE NULL, cobertura_gastos DOUBLE NULL,paga int(1) NULL,fecha_paga DATETIME NULL, referencia VARCHAR(50) NULL , prima double NULL );";
            }

            con.query(sql);

            this.addColumn();
            this.addIndex();
            this.enviohechoFecha();
        }

        private void transicion()
        {

            puntodeventa punt = new puntodeventa();
            if (punt.get_principal() || punt.get_colaborador())
            {

                sql = "UPDATE propuestas SET fecha_paga = ultmod, prefijo = '" + punt.prefijo + "', idpropuesta = id WHERE  prefijo IS NULL ";
                con.query(sql);

                lineas_propuestas line = new lineas_propuestas(true);

                sql = "UPDATE lineas_propuestas t1, propuestas t2, actividades t3, clasificaciones t4 SET t1.actividad = t3.nombre , t1.clasificacion = t4.nombre ,  t1.prefijo = '" + punt.prefijo + "', t1.idprefijo = t1.id_propuesta, " +
                    "t1.fechaDesde = t2.fechaDesde, t1.fechaHasta = t2.fechaHasta  WHERE t1.id_propuesta = t2.id AND t1.id_actividad = t3.id AND t1.id_clasificacion = t4.id AND t1.prefijo IS NULL ";
                con.query(sql);



                barrios_propuesta barr = new barrios_propuesta(true);

                sql = "UPDATE barrios_propuesta SET prefijo = '" + punt.prefijo + "', idprefijo = id_propuesta WHERE  prefijo IS NULL ";
                con.query(sql);
                /*sql = "UPDATE propuestas SET prefijo = '" + punt.prefijo + "', idpropuesta = id WHERE prefijo = '' OR prefijo IS NULL ";
                con.query(sql);*/


                controlventas contr = new controlventas(true);
            }

            sql = "SELECT SUM(nota) as sumita FROM propuestas";
            DataSet ds0 = con.query(sql);
            if (ds0.Tables[0].Rows[0]["sumita"].ToString() == "0")
            {
                controlventas contr = new controlventas(true);
                if (MDIParent1.baseDatos == "SQlite")
                {

                    sql = "UPDATE propuestas t1, controlventas t2 SET t1.nota = t2.notacredito  WHERE  t2.codgrupo = (t1.prefijo || t1.idpropuesta)";
                    con.query(sql);
                }
                else
                {
                    sql = "UPDATE propuestas t1, controlventas t2 SET t1.nota = t2.notacredito  WHERE  t2.codgrupo = CONCAT(t1.prefijo , t1.idpropuesta)";
                    con.query(sql);
                }

            }

            
                

        }

        

        public DataSet revisarPropuestasLineas(string fecha_)
        {
            DataSet ds = new DataSet();
            sql = "SELECT t1.id FROM propuestas t1 WHERE((SELECT SUM(premio) FROM "+
                " lineas_propuestas WHERE t1.id = id_propuesta) - t1.premio_total) IS NULL AND DATE(t1.ultmod) = '"+fecha_+"'";

            ds = con.query(sql);

            if(ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
            }

            return ds;

        }

        private void addIndex()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                try
                {

                    

                    sql = "SELECT COUNT(1) IndexIsThere FROM INFORMATION_SCHEMA.STATISTICS WHERE table_name = 'propuestas' AND index_name = 'idx_documento'";
                    DataSet ds = con.query(sql);
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        if(ds.Tables[0].Rows[0]["IndexIsThere"].ToString() == "0")
                        {
                            sql = "CREATE INDEX idx_documento ON propuestas(documento);";
                            sql += "CREATE INDEX idx_ultmod ON propuestas(ultmod);";
                            con.query(sql);
                        }
                    }

                    sql = "SELECT COUNT(1) IndexIsThere FROM INFORMATION_SCHEMA.STATISTICS WHERE table_name = 'propuestas' AND index_name = 'idx_id'";
                    ds = con.query(sql);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["IndexIsThere"].ToString() == "0")
                        {
                            sql = "CREATE INDEX  idx_id ON propuestas(id);";
                            sql += "CREATE INDEX  idx_codestado ON propuestas(codestado);";
                            sql += "CREATE INDEX  idx_user_edit ON propuestas(user_edit);";
                            sql += "CREATE INDEX  idx_referencia ON controlventas(referencia);";
                            sql += "CREATE INDEX  idx_codgrupo ON controlventas(codgrupo);";
                            con.query(sql);
                        }
                    }

                    string sql1 = "SELECT COUNT(1) as cant FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE()   AND TABLE_NAME = 'propuestas'  AND INDEX_NAME = 'idx.idpropuesta'; ";
                    ds = con.query(sql1);
                    if (ds.Tables[0].Rows[0]["cant"].ToString() != "1")
                    {
                        sql1 = "CREATE INDEX `idx.idpropuesta` ON `propuestas` (`idpropuesta`);";
                        con.query(sql1);
                    }
                    sql1 = "SELECT COUNT(1) as cant FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE()   AND TABLE_NAME = 'propuestas'  AND INDEX_NAME = 'idx.prefijo'; ";
                    ds = con.query(sql1);
                    if (ds.Tables[0].Rows[0]["cant"].ToString() != "1")
                    {
                        sql1 = "CREATE INDEX `idx.prefijo` ON `propuestas` (`prefijo`);";
                        con.query(sql1);
                    }


                }
                catch
                {
                    //
                }
            }
            else
            {
                try
                {
                    sql = "CREATE INDEX idx_documento ON propuestas(documento);";
                    sql += "CREATE INDEX idx_ultmod ON propuestas(ultmod);";
                    con.query(sql);
                    sql = "CREATE INDEX  idx_id ON propuestas(id);";
                    sql += "CREATE INDEX  idx_codestado ON propuestas(codestado);";
                    sql += "CREATE INDEX  idx_user_edit ON propuestas(user_edit);";
                    sql += "CREATE INDEX  idx_referencia ON controlventas(referencia);";
                    sql += "CREATE INDEX  idx_codgrupo ON controlventas(codgrupo);";
                    con.query(sql);
                    con.query("CREATE INDEX `idx.idpropuesta` ON `propuestas` (`idpropuesta`);");
                        con.query("CREATE INDEX `idx.prefijo` ON `propuestas` (`prefijo`);");
                }
                catch
                {
                    //
                }
            }

        }

        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                try
                {
                    //Add column categoria if NOT exist
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'cobertura_suma' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN cobertura_suma double;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'cobertura_deducible' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN cobertura_deducible double;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'cobertura_gastos' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN cobertura_gastos double;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'promocion' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN promocion VARCHAR(50);");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'paga' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN paga INT(1) DEFAULT '1';");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'fecha_paga' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN fecha_paga DATETIME NULL;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'referencia' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN referencia VARCHAR(50)  NULL;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'prima' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN prima double  DEFAULT 0;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'master' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN master VARCHAR(150) NULL;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'organizador' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN organizador VARCHAR(150) NULL;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'productor' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN productor VARCHAR(150) NULL;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'prefijo' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN prefijo VARCHAR(10) NULL;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'formadepago' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN formadepago VARCHAR(100) DEFAULT 'CONTADO';");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'usuariopaga' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN usuariopaga VARCHAR(150) NULL;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'create_at' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN create_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'tipopago' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN tipopago VARCHAR(60) NULL;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'compformapago' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN compformapago VARCHAR(100) NULL;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'idpropuesta' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN idpropuesta  INT(50) NOT NULL;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'envionube' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN envionube  int(2) DEFAULT 0;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'codempresa' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN codempresa VARCHAR(150) NULL;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'nota' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN nota VARCHAR(10)  DEFAULT 0;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'data_barrios' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD data_barrios LONGTEXT CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL ;");
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'version' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN version int(10)  DEFAULT 0;");
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error al agregar columna de propuesta " + ex.Message);
                }
            }
            else
            {
                try
                {
                    con.query("ALTER TABLE propuestas ADD COLUMN cobertura_suma double;");
                    con.query("ALTER TABLE propuestas ADD COLUMN cobertura_deducible double;");
                    con.query("ALTER TABLE propuestas ADD COLUMN cobertura_gastos double;");
                    con.query("ALTER TABLE propuestas ADD COLUMN promocion VARCHAR(50);");
                    con.query("ALTER TABLE propuestas ADD COLUMN paga INT(1)  DEFAULT '1';");
                    con.query("ALTER TABLE propuestas ADD COLUMN fecha_paga DATETIME NULL;");
                    con.query("ALTER TABLE propuestas ADD COLUMN referencia VARCHAR(50)  NULL;");
                    con.query("ALTER TABLE propuestas ADD COLUMN prima double   DEFAULT 0;");
                    con.query("ALTER TABLE propuestas ADD COLUMN master VARCHAR(150) NULL;");
                    con.query("ALTER TABLE propuestas ADD COLUMN organizador VARCHAR(150) NULL;");
                    con.query("ALTER TABLE propuestas ADD COLUMN productor VARCHAR(150) NULL;");
                    con.query("ALTER TABLE propuestas ADD COLUMN prefijo VARCHAR(10) NULL;");
                    con.query("ALTER TABLE propuestas ADD COLUMN formadepago VARCHAR(100) DEFAULT 'CONTADO';");
                    con.query("ALTER TABLE propuestas ADD COLUMN usuariopaga VARCHAR(150) NULL;");
                    con.query("ALTER TABLE propuestas ADD COLUMN tipopago VARCHAR(60) NULL;");
                    con.query("ALTER TABLE propuestas ADD COLUMN compformapago VARCHAR(100) NULL;");
                    con.query("ALTER TABLE propuestas ADD COLUMN idpropuesta INT(50) NOT NULL;");
                    con.query("ALTER TABLE propuestas ADD COLUMN envionube  int(2) DEFAULT 0;");
                    con.query("ALTER TABLE propuestas ADD COLUMN codempresa VARCHAR(150) NULL;");
                    con.query("ALTER TABLE propuestas ADD COLUMN nota VARCHAR(10)  DEFAULT 0;");
                    con.query("ALTER TABLE propuestas ADD COLUMN data_barrios JSON NULL;");
                    con.query("ALTER TABLE propuestas ADD COLUMN version int(10)  DEFAULT 0;");
                }
                catch (Exception ex){
                    Console.WriteLine("Error al agregar columna de propuesta " + ex.Message);
                }
            }

        }

        public int consecutivo()
        {
            int res = 0;
            DataSet ds = new DataSet();

            sql = "SELECT MAX(id) as consecutivo FROM propuestas ";
            try
            {
                ds = con.query(sql);
                if(ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                        res = Convert.ToInt16(ds.Tables[0].Rows[0]["consecutivo"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");

            }

            return res + 1;
        }

        public int maxId()
        {
            int res = 0;
            DataSet ds = new DataSet();

            sql = "SELECT MAX(id) as consecutivo FROM propuestas ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                        res = Convert.ToInt16(ds.Tables[0].Rows[0]["consecutivo"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");

            }

            return res;
        }


        public DataSet get(string id_)
        {
            DataSet ds = new DataSet();

            
            sql = "SELECT * FROM propuestas WHERE (codestado = 1 OR codestado = 2) AND id = '"+id_+"'  ";

            Console.WriteLine("\n"+sql);

            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {   
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }

        public DataSet getprefijo(string prefijo_, string id_)
        {
            DataSet ds = new DataSet();


            sql = "SELECT * FROM propuestas WHERE (codestado = 1 OR codestado = 2) AND idpropuesta = '" + id_ + "' AND prefijo = '" + prefijo_ + "' ";
            //Console.WriteLine("GETPR--->\n\n"+sql);
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }


        public int consecutivo_prefijo()
        {
            if (this.idpropuesta != "")
                return Convert.ToInt32(this.idpropuesta);

            int res = 1;
            string sql1 = "SELECT MAX(idpropuesta) as idpropuesta FROM propuestas WHERE prefijo = '" + this.prefijo + "' ";

            DataSet ds = con.query(sql1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if(ds.Tables[0].Rows[0]["idpropuesta"] == null || ds.Tables[0].Rows[0]["idpropuesta"].ToString() == "")
                {
                    return 1;
                }
                return (Convert.ToInt32(ds.Tables[0].Rows[0]["idpropuesta"].ToString()) + 1);
            }

            return res;
        }

/*
        public int consecutivo_prefijo_()
        {
            if(this.idpropuesta != null)
            {
                if (this.idpropuesta != "")
                {
                    return Convert.ToInt32(this.idpropuesta);
                }
            }
            
            bool cons_active = false;
            DateTime dt_a = DateTime.Now;
            while (cons_active == false)
            {
                
                DateTime dt_n = DateTime.Now; 
                Console.WriteLine("Esta consultando " + dt_a.ToString() + " AHORA  " + dt_n.ToString());
                if(SELECT )//SELECT con número MAX consecutivo, si  existe número de referencia, cada 2 segundos
                {
                    cons_active = true;
                    //se actualiza el código de referencia 
                }

                System.Threading.Thread.Sleep( 2000 );
                if ( (dt_n -  dt_a).Seconds > 9.8 )//si existe un cpdigo de referencia pero han pasado 10 segundos es que algo no está bien, por tanto, se procede y se borra el código de referencia que existe y se coloca en el MAX ya que se está numerando nuevamente
                {
                    cons_active = true;
                }
            }

            int res = 1;
            sql = "SELECT MAX(idpropuesta) as idpropuesta FROM propuestas WHERE prefijo = '" + this.prefijo + "' ";

            DataSet ds = con.query(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return (Convert.ToInt32(ds.Tables[0].Rows[0]["idpropuesta"].ToString()) + 1);
            }
            
            return res;
        }*/

        public DataSet get_all_date_findia(string fecha_, string codorganizador_ = "TODOS")
        {
            DataSet ds = new DataSet();
            if(codorganizador_ != "TODOS")
                sql = "SELECT * FROM propuestas WHERE DATE(ultmod) = '" + fecha_ + "' AND codestado > 0 AND organizador = '"+codorganizador_+"'";
            else
                sql = "SELECT * FROM propuestas WHERE DATE(ultmod) = '" + fecha_ + "' AND codestado > 0 ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }

        public DataSet get_all_date(string prefijo_ = "")
        {
            logs l = new logs();
            DataSet ds = new DataSet();


            if (prefijo_ != "")
            {
                prefijo_ = " AND t1.prefijo = '"+prefijo_+"' ";
            }
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "SELECT *, (SELECT CONCAT(nombres,' ',apellidos) FROM clientes WHERE id = t1.documento LIMIT 1) as nombre, (SELECT fecha_nacimiento FROM clientes" +
                " WHERE id = t1.documento LIMIT 1) as fecha_nacimiento FROM propuestas t1 WHERE  (t1.envionube = '0' " + prefijo_ + ") OR (t1.envionube = '0' AND t1.codestado = '0' AND DATE(t1.ultmod) > '2023-02-04' ) ";
            }
            else
            {
                sql = "SELECT *, (SELECT nombres || ' ' || apellidos FROM clientes WHERE id = t1.documento LIMIT 1 ) as nombre, (SELECT fecha_nacimiento FROM clientes" +
                   " WHERE id = t1.documento LIMIT 1 ) as fecha_nacimiento FROM propuestas t1 WHERE  (t1.envionube = '0'  " + prefijo_ + ") OR (t1.envionube = '0' AND t1.codestado = '0' AND DATE(t1.ultmod) > '2023-02-04' ) ";
            }
            

            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }

        
        public DataSet get_all_date_entre(string fecha1, string fecha2, string referencia = "")
        {
            DataSet ds = new DataSet();


            if (MDIParent1.baseDatos == "SQlite")
            {
                //"(SELECT t1.notacredito FROM controlventas t1 WHERE t1.cliente = documento AND t1.codgrupo = (prefijo || idpropuesta) limit 1) as nota " +
                sql = "SELECT id,idpropuesta,prefijo,referencia as refe,prima,tipopago,fecha_paga,compformapago,premio_total,documento,fechaDesde,fechaHasta, DATE(ultmod) as ultmod, meses, "+
                    "id_cobertura,user_edit,paga, (SELECT t1.nombres||t1.apellidos  FROM clientes t1 WHERE t1.id = documento LIMIT 1) as nombre,codestado,nota" +
                "   FROM propuestas WHERE DATE(ultmod) >= '" + fecha1 + "'  AND DATE(ultmod) <= '" + fecha2 +
                "'  AND codestado > 0 AND user_edit LIKE '%" + this.user_edit+"%' ORDER BY ultmod ASC ";

                if (referencia != "")
                {
                    //" (SELECT t1.notacredito FROM controlventas t1 WHERE t1.cliente = t0.documento AND t1.codgrupo = (t0.prefijo || t0.idpropuesta) limit 1) as nota " +
                    sql = "SELECT t0.id,t0.idpropuesta,t0.prefijo,t0.referencia as refe,t0.prima,t0.tipopago,t0.fecha_paga,t0.compformapago,t0.premio_total,t0.documento,t0.fechaDesde,t0.fechaHasta,t0.paga, DATE(t0.ultmod) as ultmod, t0.meses," +
                        "t0.id_cobertura,t0.user_edit, (SELECT t1.nombres||t1.apellidos  FROM clientes t1 WHERE t1.id = t0.documento LIMIT 1) as nombre,t0.codestado,t0.nota" +
               "   FROM propuestas t0 INNER JOIN controlventas t2 ON "+
               " t2.referencia = '" + referencia + "' AND (t0.prefijo || t0.idpropuesta) = t2.codgrupo AND DATE(t0.ultmod) >= '" + fecha1 + "'  AND DATE(t0.ultmod) <= '" + fecha2 +
               "'  AND DATE(t0.ultmod) = t2.fecha AND t0.codestado > 0  AND user_edit LIKE '%" + this.user_edit + "%' ORDER BY t0.ultmod ASC ";
                }
            }
            else
            {
                sql = "SELECT id,idpropuesta,prefijo,tipopago,referencia as refe,prima,fecha_paga,compformapago,premio_total,documento, DATE(ultmod) as ultmod,fechaDesde,fechaHasta,paga, meses, " +
                    "id_cobertura,user_edit, (SELECT CONCAT(t1.nombres, ' ',t1.apellidos)  FROM clientes t1 WHERE t1.id = documento LIMIT 1) as nombre,codestado,nota " +
                "   FROM propuestas WHERE DATE(ultmod) >= '" + fecha1 + "'  AND DATE(ultmod) <= '" + fecha2 +
                "'  AND codestado > 0  AND user_edit LIKE '%" + this.user_edit + "%' ORDER BY ultmod ASC ";

                if (referencia != "")
                {
                    sql = "SELECT t0.id,t0.idpropuesta,t0.prima,t0.prefijo,t0.referencia as refe,t0.tipopago,t0.fecha_paga,t0.compformapago,t0.premio_total,t0.documento,t0.fechaDesde,t0.fechaHasta,t0.paga, DATE(t0.ultmod) as ultmod, t0.meses," +
                        "t0.id_cobertura,t0.user_edit," +
                    " (SELECT CONCAT(t1.nombres, t1.apellidos) FROM clientes t1 WHERE t1.id = t0.documento LIMIT 1) as nombre,t0.codestado, t0.nota" +
                    " FROM propuestas t0 INNER JOIN controlventas t2 ON " +
                    " t2.referencia = '"+referencia+ "' AND CONCAT(t0.prefijo,t0.idpropuesta) = t2.codgrupo AND DATE(t0.ultmod) >= '" + fecha1 + "'  AND DATE(t0.ultmod) <= '" + fecha2 +
                    "' AND DATE(t0.ultmod) = t2.fecha AND t0.codestado > 0 AND user_edit LIKE '%" + this.user_edit + "%' ORDER BY t0.ultmod ASC";
                }
                    
            }

            
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }

        public DataSet get_all_date_dias(string fecha1, string fecha2)
        {
            DataSet ds = new DataSet();

            if(MDIParent1.baseDatos == "SQlite")
            {
                sql = "SELECT SUM(t1.prima) as prima,  SUM(t1.nota) as nota, SUM(t1.premio) as premio, SUM(t1.premio_total) as totalpremio, DATE(t1.ultmod) as fecha, COUNT(*) as cantidad " +
                    " FROM propuestas t1 LEFT JOIN controlventas t2 ON  DATE(t1.ultmod) = t2.fecha AND (t1.prefijo||t1.idpropuesta) = t2.codgrupo  " +
                "WHERE  DATE(t1.ultmod) >= '" + fecha1 + "'  AND DATE(t1.ultmod) <= '" + fecha2 + "' AND t1.codestado > 0  AND t1.user_edit LIKE '%" + this.user_edit + "%' " +
                " GROUP BY strftime('%d', t1.ultmod) ORDER BY t1.ultmod ASC ";
            }
            else
            {
                sql = "SELECT SUM(t1.prima) as prima,  SUM(t1.nota) as nota, SUM(t1.premio) as premio, SUM(t1.premio_total) as totalpremio, DATE(t1.ultmod) as fecha, COUNT(*) as cantidad" +
                    " FROM propuestas t1 LEFT JOIN controlventas t2 ON  DATE(t1.ultmod) = t2.fecha AND CONCAT(t1.prefijo,t1.idpropuesta) = t2.codgrupo " +
                "WHERE  DATE(t1.ultmod) >= '" + fecha1 + "'  AND DATE(t1.ultmod) <= '" + fecha2 + "' AND t1.codestado > 0   AND t1.user_edit LIKE '%" + this.user_edit + "%'  " +
                " GROUP BY DAY(t1.ultmod) ORDER BY t1.ultmod ASC ";
            }


            try
            {
                
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }

        public DataSet get_all_date_meses(string fecha1, string fecha2)
        {
            DataSet ds = new DataSet();
            if (MDIParent1.baseDatos == "SQlite")
            {
                sql = "SELECT SUM(t1.prima) as prima,  SUM(t1.nota) as nota, SUM(t1.premio) as premio, SUM(t1.premio_total) as totalpremio, strftime('%m', t1.ultmod) as fecha, COUNT(*) as cantidad" +
                    " FROM propuestas t1 LEFT JOIN controlventas t2 ON  DATE(t1.ultmod) = t2.fecha AND (t1.prefijo||t1.idpropuesta) = t2.codgrupo " +
                " WHERE DATE(t1.ultmod) >= '" + fecha1 + "'  AND DATE(t1.ultmod) <= '" + fecha2 + "' AND t1.codestado > 0  AND t1.user_edit LIKE '%" + this.user_edit + "%' " +
                " GROUP BY strftime('%m', t1.ultmod) ORDER BY t1.ultmod ASC ";
            }
            else
            {
                sql = "SELECT SUM(t1.prima) as prima,  SUM(t1.nota) as nota, SUM(premio) as premio, SUM(premio_total) as totalpremio, MONTH(t1.ultmod) as fecha, COUNT(*) as cantidad  " +
                    " FROM propuestas t1 LEFT JOIN controlventas t2 ON  DATE(t1.ultmod) = t2.fecha AND CONCAT(t1.prefijo,t1.idpropuesta) = t2.codgrupo " +
               " WHERE DATE(t1.ultmod) >= '" + fecha1 + "'  AND DATE(t1.ultmod) <= '" + fecha2 + "' AND t1.codestado > 0  AND t1.user_edit LIKE '%" + this.user_edit + "%'" +
               " GROUP BY MONTH(t1.ultmod) ORDER BY t1.ultmod ASC ";
            }
            
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }

        public DataSet get_all_date_anos(string fecha1, string fecha2)
        {
            DataSet ds = new DataSet();
            if (MDIParent1.baseDatos == "SQlite")
            {
                sql = "SELECT SUM(t1.prima) as prima,  SUM(t1.nota) as nota, SUM(t1.premio) as premio, SUM(t1.premio_total) as totalpremio, strftime('%Y', t1.ultmod) as fecha, COUNT(*) as cantidad " +
                    " FROM propuestas t1 LEFT JOIN controlventas t2 ON  DATE(t1.ultmod) = t2.fecha AND (t1.prefijo||t1.idpropuesta) = t2.codgrupo " +
                " WHERE DATE(t1.ultmod) >= '" + fecha1 + "'  AND DATE(t1.ultmod) <= '" + fecha2 + "'  AND t1.codestado > 0  AND t1.user_edit LIKE '%" + this.user_edit + "%' " +
                " GROUP BY strftime('%Y', t1.ultmod) ORDER BY t1.ultmod ASC ";
            }
            else
            {
                sql = "SELECT SUM(t1.prima) as prima,  SUM(t1.nota) as nota, SUM(t1.premio) as premio, SUM(t1.premio_total) as totalpremio, YEAR(t1.ultmod) as fecha, COUNT(*) as cantidad " +
                    " FROM propuestas t1 LEFT JOIN controlventas t2 ON  DATE(t1.ultmod) = t2.fecha AND CONCAT(t1.prefijo,t1.idpropuesta) = t2.codgrupo " +
                " WHERE DATE(t1.ultmod) >= '" + fecha1 + "'  AND DATE(t1.ultmod) <= '" + fecha2 + "'  AND t1.codestado > 0  AND t1.user_edit LIKE '%" + this.user_edit + "%'" +
                " GROUP BY YEAR(t1.ultmod) ORDER BY t1.ultmod ASC ";
            }
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }

        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM propuestas WHERE codestado = 1";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }


        public DataSet get_all_credito(DateTime fecha1, DateTime fecha2, bool quienpaga = false, string user_ = "", string sinpagar = " >= '0' ")
        {
            DataSet ds = new DataSet();

            if (MDIParent1.baseDatos == "MySql")
            {

                sql = "SELECT *,(SELECT CONCAT(t1.nombres,' ',t1.apellidos)  FROM clientes t1 WHERE t1.id = documento LIMIT 1) as nombre FROM propuestas " +
                " WHERE codestado > 0 AND  DATE(ultmod) BETWEEN '" + fecha1.ToString("yyyy-MM-dd") + "' AND '" + fecha2.ToString("yyyy-MM-dd") +
                "'  AND formadepago = 'CREDITO' AND user_edit LIKE '%" + user_ + "%' AND paga " + sinpagar + " ";

                if (quienpaga)
                {
                    sql = "SELECT *,(SELECT CONCAT(t1.nombres,' ',t1.apellidos)  FROM clientes t1 WHERE t1.id = documento LIMIT 1) as nombre FROM propuestas " +
                    " WHERE codestado > 0 AND DATE(fecha_paga) BETWEEN '" + fecha1.ToString("yyyy-MM-dd") + "' AND '" + fecha2.ToString("yyyy-MM-dd") +
                    "'  AND formadepago = 'CREDITO' AND usuariopaga LIKE '%" + user_ + "%'  AND paga  " + sinpagar + " ";

                }
            }
            else
            {
                sql = "SELECT *,(SELECT (t1.nombres|| ' ' || t1.apellidos)  FROM clientes t1 WHERE t1.id = documento LIMIT 1) as nombre FROM propuestas " +
                " WHERE codestado > 0 AND  DATE(ultmod) BETWEEN '" + fecha1.ToString("yyyy-MM-dd") + "' AND '" + fecha2.ToString("yyyy-MM-dd") +
                "'  AND formadepago = 'CREDITO' AND user_edit LIKE '%" + user_ + "%' AND paga " + sinpagar + " ";

                if (quienpaga)
                {
                    sql = "SELECT *,(SELECT (t1.nombres|| ' ' || t1.apellidos)  FROM clientes t1 WHERE t1.id = documento LIMIT 1) as nombre FROM propuestas " +
                    " WHERE codestado > 0 AND DATE(fecha_paga) BETWEEN '" + fecha1.ToString("yyyy-MM-dd") + "' AND '" + fecha2.ToString("yyyy-MM-dd") +
                    "'  AND formadepago = 'CREDITO' AND usuariopaga LIKE '%" + user_ + "%'  AND paga  " + sinpagar + " ";

                }
            }

            Console.WriteLine("CRÉDITO \n "+sql);

            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }


        public DataSet get_all_paga(DateTime fecha1, DateTime fecha2)
        {
            DataSet ds = new DataSet();
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "SELECT *,(SELECT CONCAT(t1.nombres,' ',t1.apellidos)  FROM clientes t1 WHERE t1.id = documento LIMIT 1) as nombre FROM propuestas " +
                " WHERE DATE(ultmod) BETWEEN '" + fecha1.ToString("yyyy-MM-dd") + "' AND '" + fecha2.ToString("yyyy-MM-dd") + "' AND paga = '0' AND formadepago = 'CREDITO' ";
            }
            else
            {
                sql = "SELECT *,(SELECT (t1.nombres|| ' ' || t1.apellidos)  FROM clientes t1 WHERE t1.id = documento LIMIT 1) as nombre FROM propuestas " +
                " WHERE DATE(ultmod) BETWEEN '" + fecha1.ToString("yyyy-MM-dd") + "' AND '" + fecha2.ToString("yyyy-MM-dd") + "' AND paga = '0' AND formadepago = 'CREDITO' ";
            }
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }

        public List<string> get_cod_organizador(string fecha_)
        {
            List<string> lsorg = new List<string>();
            lsorg.Add("TODOS");
            DataSet ds = new DataSet();

            sql = "SELECT organizador FROM propuestas WHERE codestado =  1 AND DATE(ultmod) = '"+ fecha_ + "'  GROUP BY organizador ORDER BY organizador ASC ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lsorg.Add(ds.Tables[0].Rows[i]["organizador"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas "+ex.Message);
            }

            return lsorg;
        }

        public DataSet get_all_estado(string estado_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM propuestas WHERE codestado =  '"+estado_+"' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }

        public DataSet get_all_arqueo_usuariopaga(string fecha1, string fecha2)
        {
            DataSet ds = new DataSet();

            sql = "SELECT DATE(fecha_paga) as fecha, usuariopaga as usuario, SUM(premio_total) as total FROM propuestas WHERE " +
                " codestado >  '0' AND usuariopaga != '' AND DATE(fecha_paga) BETWEEN '" + fecha1 + "' AND '" + fecha2 + "' GROUP BY " +
                " DATE(fecha_paga), usuariopaga";
            
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }

        public DataSet get_all_arqueo(string fecha1, string fecha2)
        {
            DataSet ds = new DataSet();

            sql = "SELECT DATE(ultmod) as fecha, user_edit as usuario, SUM(premio_total) as total FROM propuestas WHERE "+
                " codestado >  '0' AND DATE(ultmod) BETWEEN '"+fecha1+"' AND '"+fecha2+"' GROUP BY "+
                " DATE(ultmod), user_edit";
            
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }

        public DataSet get_vencimientos(string fecha_vence)
        {
            DataSet ds = new DataSet();
            controlventas control = new controlventas(true);
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "SELECT t1.idpropuesta,t1.referencia,t1.prefijo,t1.formadepago,"+
                " (SELECT t0.nombres FROM clientes t0 WHERE t0.id = t1.documento LIMIT 1 ) as nombres,"+
                " (SELECT t0.apellidos FROM clientes t0 WHERE t0.id = t1.documento LIMIT 1 ) as apellidos," +
                " (SELECT t0.id FROM clientes t0 WHERE t0.id = t1.documento LIMIT 1) as id," +
                " (SELECT t0.fecha_nacimiento FROM clientes t0 WHERE t0.id = t1.documento LIMIT 1) as fecha_nacimiento," +
                " (SELECT t0.email FROM clientes t0 WHERE t0.id = t1.documento LIMIT 1) as email," +
                " (SELECT t0.telefono FROM clientes t0 WHERE t0.id = t1.documento LIMIT 1) as telefono," +
                " t1.documento, t1.ultmod, t1.id_cobertura, t1.fechaDesde,t1.user_edit,t1.prima,t1.premio, t1.fechaHasta, t1.paga, t1.codestado,t1.nota " +
                " FROM propuestas t1 WHERE " +
                " DATE(t1.fechaHasta) BETWEEN  '" + DateTime.Today.ToString("yyyy-MM-dd") + "' AND '" + fecha_vence + "' AND  t1.codestado = 1  " +
                " ORDER BY t1.id DESC";
            }
            else
            {
                sql = "SELECT t1.idpropuesta,t1.referencia,t1.prefijo,t1.formadepago,"+
                " (SELECT t0.nombres FROM clientes t0 WHERE t0.id = t1.documento LIMIT 1 ) as nombres," +
                " (SELECT t0.apellidos FROM clientes t0 WHERE t0.id = t1.documento LIMIT 1 ) as apellidos," +
                " (SELECT t0.id FROM clientes t0 WHERE t0.id = t1.documento LIMIT 1) as id," +
                " (SELECT t0.fecha_nacimiento FROM clientes t0 WHERE t0.id = t1.documento LIMIT 1) as fecha_nacimiento," +
                " (SELECT t0.email FROM clientes t0 WHERE t0.id = t1.documento LIMIT 1) as email," +
                " (SELECT t0.telefono FROM clientes t0 WHERE t0.id = t1.documento LIMIT 1) as telefono," +
                " t1.documento, t1.ultmod, t1.id_cobertura, t1.fechaDesde,t1.user_edit,t1.prima,t1.premio, t1.fechaHasta, t1.paga, t1.codestado,t1.nota " +
                " FROM propuestas t1 WHERE " +
                " DATE(t1.fechaHasta) BETWEEN  '" + DateTime.Today.ToString("yyyy-MM-dd") + "' AND '" + fecha_vence + "' AND  t1.codestado = 1 " +
                " ORDER BY t1.id DESC";
            }
                

            Console.WriteLine("********** \n"+sql);
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas "+ex.Message);
            }

            return ds;
        }


        public DataSet get_all_busqueda(string coincidencia, string estado_ = "", string fecha1="", string fecha2 = "")
        {
            DataSet ds = new DataSet();

            if (fecha1 == "")
                fecha1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            if (fecha2 == "")
                fecha2 = DateTime.Now.ToString("yyyy-MM-dd");
            if(this.referencia != "")
            {
                this.referencia = " AND t1.referencia = '" + this.referencia + "' ";
            }

            if (this.user_edit != "")
            {
                this.user_edit = " AND t3.nombre = '" + this.user_edit + "' ";
            }

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "SELECT t1.prefijo,t1.formadepago,t1.idpropuesta,t1.referencia,t1.prima, t2.nombres,t1.fechaHasta,t1.premio_total, t2.apellidos, t1.id, t1.documento, t1.ultmod, t1.id_cobertura, t1.fechaDesde, " +
                " t1.fechaHasta, t1.codestado, t3.nombre as nombreuser, t1.paga FROM propuestas t1 INNER JOIN clientes t2  " +
                "  INNER JOIN usuarios t3 ON " +
                " DATE(t1.ultmod) BETWEEN '" + fecha1 + "' AND '" + fecha2 + "' AND t2.id = t1.documento  AND  t1.user_edit = t3.loggin WHERE "
                + estado_ + " (CONCAT(t1.prefijo,t1.idpropuesta)  LIKE '%" + coincidencia + "%'    OR  t1.documento LIKE '" + coincidencia + "'  OR " +
                " t2.nombres LIKE '%" + coincidencia + "%' OR t2.apellidos LIKE '%" + coincidencia + "%'   " +
                "  OR  t1.id_cobertura = '" + coincidencia + "'   )   " + this.user_edit + "  " + this.referencia +
                " GROUP BY t1.prefijo,t1.idpropuesta ORDER BY t1.id DESC";
            }
            else
            {
                sql = "SELECT t1.prefijo,t1.formadepago,t1.idpropuesta,t1.referencia,t1.prima, t2.nombres,t1.fechaHasta,t1.premio_total, t2.apellidos, t1.id, t1.documento, t1.ultmod, t1.id_cobertura, t1.fechaDesde, " +
                " t1.fechaHasta, t1.codestado, t3.nombre as nombreuser, t1.paga FROM propuestas t1 INNER JOIN clientes t2  " +
                "  INNER JOIN usuarios t3 ON " +
                " DATE(t1.ultmod) BETWEEN '" + fecha1 + "' AND '" + fecha2 + "' AND t2.id = t1.documento AND  t1.user_edit = t3.loggin WHERE " 
                + estado_ + " ((t1.prefijo || t1.idpropuesta)  LIKE '%" + coincidencia + "%'   OR  t1.documento LIKE '" + coincidencia + "'  OR " +
                " t2.nombres LIKE '%" + coincidencia + "%' OR t2.apellidos LIKE '%" + coincidencia + "%'   " +
                "  OR  t1.id_cobertura = '" + coincidencia + "'   )   " + this.user_edit + "  " + this.referencia +
                " GROUP BY t1.prefijo,t1.idpropuesta ORDER BY t1.id DESC";
            }
            Console.WriteLine("Propuestas \n"+sql);
     
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestas");
            }

            return ds;
        }

        public void borrarRegistros()
        {
            sql = "DELETE FROM propuestas";
            con.query(sql);
        }


        public bool exist()
        {
            if(this.idpropuesta != "")
            {
                sql = "SELECT id FROM propuestas WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "'";

                if (con.query(sql).Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }

            


            return false;
        }


        public bool verify_pref_cons(string pref, string idpro)
        {
            string sql1 = $"SELECT idpropuesta,prefijo FROM propuestas WHERE idpropuesta = '{idpro}' AND prefijo = '{pref}' ";

            if(con.query(sql1).Tables[0].Rows.Count > 0)
            {
                return false;
            }

            return true;
        }

        public bool confirmVersion()
        {
            if (this.idpropuesta != "")
            {
                sql = "SELECT id FROM propuestas WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "' AND version < '"+this.version+"' ";
                if (con.query(sql).Tables[0].Rows.Count > 0)
                    return true;
            }

            return false;
        }

        public bool save_import()
        {
            try
            {
                if (this.idpropuesta == "")
                {
                    this.idpropuesta = this.consecutivo_prefijo().ToString();
                }

                this.data_barrios = this.data_barrios == "" ? "{\"barrios\":[]}" : this.data_barrios;


                if (this.exist())
                {

                    
                        if (this.denube )
                        {

                            if( this.confirmVersion())
                            {
                                sql = "UPDATE propuestas SET " +
                                "documento = '" + this.documento + "'," +
                                "num_polizas = '" + this.num_polizas + "'," +
                                "meses = '" + this.meses + "'," +
                                "id_cobertura = '" + this.id_cobertura + "'," +
                                "id_barrio = '" + this.id_barrio + "'," +
                                "nueva_poliza = '" + this.nueva_poliza + "'," +
                                "premio = '" + this.premio + "'," +
                                "premio_total = '" + this.premio_total + "'," +
                                "fechaDesde = '" + this.fechaDesde + "'," +
                                "fechaHasta = '" + this.fechaHasta + "'," +
                                "clausula = '" + this.clausula + "'," +
                                "barrio_beneficiario = '" + this.barrio_beneficiario + "'," +
                                "ultmod = '" + this.ultmod + "'," +
                                "user_edit = '" + this.user_edit + "'," +
                                "usuariopaga = '" + this.usuariopaga + "'," +
                                "fecha_paga = '" + this.fecha_paga + "'," +
                                "codestado = '" + this.codestado + "'," +
                                "cobertura_suma = '" + this.cobertura_suma + "'," +
                                "cobertura_gastos = '" + this.cobertura_gastos + "'," +
                                "cobertura_deducible = '" + this.cobertura_deducible + "'," +
                                "promocion = '" + this.promocion + "'," +
                                "paga = '" + this.paga + "'," +
                                "referencia = '" + this.referencia + "'," +
                                "prima = '" + this.prima.Trim().Replace(",", ".") + "'," +
                                "master = '" + this.master + "'," +
                                "organizador = '" + this.organizador + "'," +
                                "productor = '" + this.productor + "'," +
                                "formadepago = '" + this.formadepago + "'," +
                                "prefijo = '" + this.prefijo + "'," +
                                "tipopago = '" + this.tipopago + "'," +
                                "compformapago = '" + this.compformapago + "'," +
                                "nota = '" + this.nota + "'," +
                                "codempresa = '" + MDIParent1.codempresa + "'," +
                                "data_barrios = '" + this.data_barrios + "'," +
                                " version = '" + this.version + "'," +
                                "idpropuesta = '" + this.idpropuesta + "'" +
                                " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "' ";
                            }

                        }
                        else
                        {
                            if (this.formadepago == "CREDITO")
                            {
                                sql = "UPDATE propuestas SET " +
                                "documento = '" + this.documento + "'," +
                                "num_polizas = '" + this.num_polizas + "'," +
                                "meses = '" + this.meses + "'," +
                                "id_cobertura = '" + this.id_cobertura + "'," +
                                "id_barrio = '" + this.id_barrio + "'," +
                                "nueva_poliza = '" + this.nueva_poliza + "'," +
                                "premio = '" + this.premio + "'," +
                                "premio_total = '" + this.premio_total + "'," +
                                "fechaDesde = '" + this.fechaDesde + "'," +
                                "fechaHasta = '" + this.fechaHasta + "'," +
                                "clausula = '" + this.clausula + "'," +
                                "barrio_beneficiario = '" + this.barrio_beneficiario + "'," +
                                "ultmod = '" + this.ultmod + "'," +
                                "codestado = '" + this.codestado + "'," +
                                "cobertura_suma = '" + this.cobertura_suma + "'," +
                                "cobertura_gastos = '" + this.cobertura_gastos + "'," +
                                "cobertura_deducible = '" + this.cobertura_deducible + "'," +
                                "promocion = '" + this.promocion + "'," +
                                "paga = '" + this.paga + "'," +
                                "usuariopaga = ''," +
                                "fecha_paga  = '1000-01-01 01:00:00'," +
                                "referencia = '" + this.referencia + "'," +
                                "prima = '" + this.prima.Trim().Replace(",", ".") + "'," +
                                "master = '" + this.master + "'," +
                                "organizador = '" + this.organizador + "'," +
                                "productor = '" + this.productor + "'," +
                                "formadepago = '" + this.formadepago + "'," +
                                "prefijo = '" + this.prefijo + "'," +
                                "tipopago = '" + this.tipopago + "'," +
                                "compformapago = '" + this.compformapago + "'," +
                                "codempresa = '" + MDIParent1.codempresa + "'," +
                                "envionube = '0'," +
                                "version = (version + 1 ) , "+
                                "data_barrios = '" + this.data_barrios + "'," +
                                "idpropuesta = '" + this.idpropuesta + "'" +
                                " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "' ";
                            }
                            else
                            {
                                sql = "UPDATE propuestas SET " +
                                "documento = '" + this.documento + "'," +
                                "num_polizas = '" + this.num_polizas + "'," +
                                "meses = '" + this.meses + "'," +
                                "id_cobertura = '" + this.id_cobertura + "'," +
                                "id_barrio = '" + this.id_barrio + "'," +
                                "nueva_poliza = '" + this.nueva_poliza + "'," +
                                "premio = '" + this.premio + "'," +
                                "premio_total = '" + this.premio_total + "'," +
                                "fechaDesde = '" + this.fechaDesde + "'," +
                                "fechaHasta = '" + this.fechaHasta + "'," +
                                "clausula = '" + this.clausula + "'," +
                                "barrio_beneficiario = '" + this.barrio_beneficiario + "'," +
                                "ultmod = '" + this.ultmod + "'," +
                                "fecha_paga = '" + this.fecha_paga + "'," +
                                "codestado = '" + this.codestado + "'," +
                                "cobertura_suma = '" + this.cobertura_suma + "'," +
                                "cobertura_gastos = '" + this.cobertura_gastos + "'," +
                                "cobertura_deducible = '" + this.cobertura_deducible + "'," +
                                "promocion = '" + this.promocion + "'," +
                                "paga = '" + this.paga + "'," +
                                "referencia = '" + this.referencia + "'," +
                                "prima = '" + this.prima.Trim().Replace(",", ".") + "'," +
                                "master = '" + this.master + "'," +
                                "organizador = '" + this.organizador + "'," +
                                "productor = '" + this.productor + "'," +
                                "formadepago = '" + this.formadepago + "'," +
                                "prefijo = '" + this.prefijo + "'," +
                                "tipopago = '" + this.tipopago + "'," +
                                "compformapago = '" + this.compformapago + "'," +
                                "envionube = '0'," +
                                "version = (version + 1 ) , " +
                                "codempresa = '" + MDIParent1.codempresa + "'," +
                                "data_barrios = '" + this.data_barrios + "'," +
                                "idpropuesta = '" + this.idpropuesta + "'" +
                                " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "' ";
                            }
                        }
                    

                }
                else
                {


                    sql = "INSERT INTO propuestas (" + this.columns + ") VALUES(" +
                        "'" + this.documento + "'," +
                        "'" + this.num_polizas + "'," +
                        "'" + this.meses + "'," +
                        "'" + this.id_cobertura + "'," +
                        "'" + this.id_barrio + "'," +
                        "'" + this.nueva_poliza + "'," +
                        "'" + this.premio + "'," +
                        "'" + this.premio_total + "'," +
                        "'" + this.fechaDesde + "'," +
                        "'" + this.fechaHasta + "'," +
                        "'" + this.clausula + "'," +
                        "'" + this.barrio_beneficiario + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.cobertura_suma + "'," +
                        "'" + this.cobertura_deducible + "'," +
                        "'" + this.cobertura_gastos + "'," +
                        "'" + this.promocion + "'," +
                        "'" + this.paga + "'," +
                        "'" + this.fecha_paga + "'," +
                        "'" + this.referencia + "'," +
                        "'" + this.prima.Trim().Replace(",", ".") + "'," +
                        "'" + this.master + "'," +
                        "'" + this.organizador + "'," +
                        "'" + this.productor + "'," +
                        "'" + this.prefijo + "'," +
                        "'" + this.formadepago + "'," +
                        "'" + this.usuariopaga + "'," +
                        "'" + this.tipopago + "'," +
                        "'" + this.compformapago + "'," +
                        "'" + this.idpropuesta + "'," +
                        "'" + this.envionube + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + this.nota + "'," +
                        "'" + this.data_barrios + "'," +
                        "'" + this.version + "'" +

                        ") ";

                    if (sql.IndexOf("INSERT") > -1)
                    {
                        Random rnd = new Random();

                        int mili = DateTime.Now.Millisecond + (rnd.Next(4) * 1000);
                        System.Threading.Thread.Sleep(mili);

                        if (!verify_pref_cons(this.prefijo, this.idpropuesta))
                        {
                            this.idpropuesta = "";
                            this.idpropuesta = this.consecutivo_prefijo().ToString();
                            this.id_barrio = this.idpropuesta;

                            sql = "INSERT INTO propuestas (" + this.columns + ") VALUES(" +
                            "'" + this.documento + "'," +
                            "'" + this.num_polizas + "'," +
                            "'" + this.meses + "'," +
                            "'" + this.id_cobertura + "'," +
                            "'" + this.id_barrio + "'," +
                            "'" + this.nueva_poliza + "'," +
                            "'" + this.premio + "'," +
                            "'" + this.premio_total + "'," +
                            "'" + this.fechaDesde + "'," +
                            "'" + this.fechaHasta + "'," +
                            "'" + this.clausula + "'," +
                            "'" + this.barrio_beneficiario + "'," +
                            "'" + this.ultmod + "'," +
                            "'" + this.user_edit + "'," +
                            "'" + this.codestado + "'," +
                            "'" + this.cobertura_suma + "'," +
                            "'" + this.cobertura_deducible + "'," +
                            "'" + this.cobertura_gastos + "'," +
                            "'" + this.promocion + "'," +
                            "'" + this.paga + "'," +
                            "'" + this.fecha_paga + "'," +
                            "'" + this.referencia + "'," +
                            "'" + this.prima.Trim().Replace(",", ".") + "'," +
                            "'" + this.master + "'," +
                            "'" + this.organizador + "'," +
                            "'" + this.productor + "'," +
                            "'" + this.prefijo + "'," +
                            "'" + this.formadepago + "'," +
                            "'" + this.usuariopaga + "'," +
                            "'" + this.tipopago + "'," +
                            "'" + this.compformapago + "'," +
                            "'" + this.idpropuesta + "'," +
                            "'" + this.envionube + "'," +
                            "'" + MDIParent1.codempresa + "'," +
                            "'" + this.nota + "'," +
                            "'" + this.data_barrios + "'," +
                            "'" + this.version + "'" +

                            ") ";
                        }
                    }




                }

                con.query(sql);

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar propuesta \n" + ex.Message + "\n" + sql);
                return false;
            }

        }


        public bool save_dapper()
        {
            try
            {
                if (this.idpropuesta == "")
                {
                    this.idpropuesta = this.consecutivo_prefijo().ToString();
                }


                if (this.exist())
                {
                    if (this.denube)
                    {

                        sql = "UPDATE propuestas SET " +
                        "num_polizas = '" + this.num_polizas + "'," +
                        "meses = '" + this.meses + "'," +
                        "id_cobertura = '" + this.id_cobertura + "'," +
                        "id_barrio = '" + this.id_barrio + "'," +
                        "nueva_poliza = '" + this.nueva_poliza + "'," +
                        "premio = '" + this.premio + "'," +
                        "premio_total = '" + this.premio_total + "'," +
                        "fechaDesde = '" + this.fechaDesde + "'," +
                        "fechaHasta = '" + this.fechaHasta + "'," +
                        "clausula = '" + this.clausula + "'," +
                        "barrio_beneficiario = '" + this.barrio_beneficiario + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "usuariopaga = '" + this.usuariopaga + "'," +
                        "fecha_paga = '" + this.fecha_paga + "'," +
                        "codestado = '" + this.codestado + "'," +
                        "cobertura_suma = '" + this.cobertura_suma + "'," +
                        "cobertura_gastos = '" + this.cobertura_gastos + "'," +
                        "cobertura_deducible = '" + this.cobertura_deducible + "'," +
                        "promocion = '" + this.promocion + "'," +
                        "paga = '" + this.paga + "'," +
                        "referencia = '" + this.referencia + "'," +
                        "prima = '" + this.prima.Trim().Replace(",", ".") + "'," +
                        "master = '" + this.master + "'," +
                        "organizador = '" + this.organizador + "'," +
                        "productor = '" + this.productor + "'," +
                        "formadepago = '" + this.formadepago + "'," +
                        "prefijo = '" + this.prefijo + "'," +
                        "tipopago = '" + this.tipopago + "'," +
                        "compformapago = '" + this.compformapago + "'," +
                        "nota = '" + this.nota + "'," +
                        "codempresa = '" + MDIParent1.codempresa + "'," +
                        "data_barrios = '" +  this.data_barrios + "'," +
                        "idpropuesta = '" + this.idpropuesta + "'" +
                        " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "' ";

                    }
                    else
                    {
                        if (this.formadepago == "CREDITO")
                        {
                            sql = "UPDATE propuestas SET " +
                            "num_polizas = '" + this.num_polizas + "'," +
                            "meses = '" + this.meses + "'," +
                            "id_cobertura = '" + this.id_cobertura + "'," +
                            "id_barrio = '" + this.id_barrio + "'," +
                            "nueva_poliza = '" + this.nueva_poliza + "'," +
                            "premio = '" + this.premio + "'," +
                            "premio_total = '" + this.premio_total + "'," +
                            "fechaDesde = '" + this.fechaDesde + "'," +
                            "fechaHasta = '" + this.fechaHasta + "'," +
                            "clausula = '" + this.clausula + "'," +
                            "barrio_beneficiario = '" + this.barrio_beneficiario + "'," +
                            "ultmod = '" + this.ultmod + "'," +
                            "codestado = '" + this.codestado + "'," +
                            "cobertura_suma = '" + this.cobertura_suma + "'," +
                            "cobertura_gastos = '" + this.cobertura_gastos + "'," +
                            "cobertura_deducible = '" + this.cobertura_deducible + "'," +
                            "promocion = '" + this.promocion + "'," +
                            "paga = '" + this.paga + "'," +
                            "usuariopaga = ''," +
                            "fecha_paga  = '1000-01-01 01:00:00'," +
                            "referencia = '" + this.referencia + "'," +
                            "prima = '" + this.prima.Trim().Replace(",", ".") + "'," +
                            "master = '" + this.master + "'," +
                            "organizador = '" + this.organizador + "'," +
                            "productor = '" + this.productor + "'," +
                            "formadepago = '" + this.formadepago + "'," +
                            "prefijo = '" + this.prefijo + "'," +
                            "tipopago = '" + this.tipopago + "'," +
                            "compformapago = '" + this.compformapago + "'," +
                            "codempresa = '" + MDIParent1.codempresa + "'," +
                            "envionube = '0'," +
                            "data_barrios = '" + this.data_barrios + "'," +
                            "idpropuesta = '" + this.idpropuesta + "'" +
                            " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "' ";
                        }
                        else
                        {
                            sql = "UPDATE propuestas SET " +
                            "num_polizas = '" + this.num_polizas + "'," +
                            "meses = '" + this.meses + "'," +
                            "id_cobertura = '" + this.id_cobertura + "'," +
                            "id_barrio = '" + this.id_barrio + "'," +
                            "nueva_poliza = '" + this.nueva_poliza + "'," +
                            "premio = '" + this.premio + "'," +
                            "premio_total = '" + this.premio_total + "'," +
                            "fechaDesde = '" + this.fechaDesde + "'," +
                            "fechaHasta = '" + this.fechaHasta + "'," +
                            "clausula = '" + this.clausula + "'," +
                            "barrio_beneficiario = '" + this.barrio_beneficiario + "'," +
                            "ultmod = '" + this.ultmod + "'," +
                            "fecha_paga = '" + this.fecha_paga + "'," +
                            "codestado = '" + this.codestado + "'," +
                            "cobertura_suma = '" + this.cobertura_suma + "'," +
                            "cobertura_gastos = '" + this.cobertura_gastos + "'," +
                            "cobertura_deducible = '" + this.cobertura_deducible + "'," +
                            "promocion = '" + this.promocion + "'," +
                            "paga = '" + this.paga + "'," +
                            "referencia = '" + this.referencia + "'," +
                            "prima = '" + this.prima.Trim().Replace(",", ".") + "'," +
                            "master = '" + this.master + "'," +
                            "organizador = '" + this.organizador + "'," +
                            "productor = '" + this.productor + "'," +
                            "formadepago = '" + this.formadepago + "'," +
                            "prefijo = '" + this.prefijo + "'," +
                            "tipopago = '" + this.tipopago + "'," +
                            "compformapago = '" + this.compformapago + "'," +
                            "envionube = '0'," +
                            "codempresa = '" + MDIParent1.codempresa + "'," +
                            "data_barrios = '" + this.data_barrios + "'," +
                            "idpropuesta = '" + this.idpropuesta + "'" +
                            " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "' ";
                        }
                    }

                }
                else
                {


                    sql = "INSERT INTO propuestas (" + this.columns + ") VALUES(@documento,@num_polizas,@meses,@id_cobertura,@id_barrio,@nueva_poliza,@premio,@premio_total,@fechaDesde,@fechaHasta,@clausula,@barrio_beneficiario,@ultmod,@user_edit,@codestado,@cobertura_suma,@cobertura_deducible,@cobertura_gastos,@promocion,@paga,@fecha_paga,@referencia,@prima,@master,@organizador,@productor,@prefijo,@formadepago,@usuariopaga,@tipopago,@compformapago,@idpropuesta,@envionube,@codempresa,@nota,@data_barrios) ";




                }

                /*if (sql.IndexOf("INSERT") > -1)
                {
                    Random rnd = new Random();

                    int mili = DateTime.Now.Millisecond + (rnd.Next(4) * 1000);
                    System.Threading.Thread.Sleep(mili);

                    if (!verify_pref_cons(this.prefijo, this.idpropuesta))
                    {
                        this.idpropuesta = "";
                        this.idpropuesta = this.consecutivo_prefijo().ToString();
                        this.id_barrio = this.idpropuesta;

                        sql = "INSERT INTO propuestas (" + this.columns + ") VALUES(" +
                        "'" + this.documento + "'," +
                        "'" + this.num_polizas + "'," +
                        "'" + this.meses + "'," +
                        "'" + this.id_cobertura + "'," +
                        "'" + this.id_barrio + "'," +
                        "'" + this.nueva_poliza + "'," +
                        "'" + this.premio + "'," +
                        "'" + this.premio_total + "'," +
                        "'" + this.fechaDesde + "'," +
                        "'" + this.fechaHasta + "'," +
                        "'" + this.clausula + "'," +
                        "'" + this.barrio_beneficiario + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.cobertura_suma + "'," +
                        "'" + this.cobertura_deducible + "'," +
                        "'" + this.cobertura_gastos + "'," +
                        "'" + this.promocion + "'," +
                        "'" + this.paga + "'," +
                        "'" + this.fecha_paga + "'," +
                        "'" + this.referencia + "'," +
                        "'" + this.prima.Trim().Replace(",", ".") + "'," +
                        "'" + this.master + "'," +
                        "'" + this.organizador + "'," +
                        "'" + this.productor + "'," +
                        "'" + this.prefijo + "'," +
                        "'" + this.formadepago + "'," +
                        "'" + this.usuariopaga + "'," +
                        "'" + this.tipopago + "'," +
                        "'" + this.compformapago + "'," +
                        "'" + this.idpropuesta + "'," +
                        "'" + this.envionube + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + this.nota + "'," +
                        "'" + this.data_barrios + "'" +

                        ") ";
                    }
                }*/
                this.prima = this.prima.Trim().Replace(",", ".");
                con.conecction.Execute(sql, 
                    new
                    {
                        this.documento ,
                        this.num_polizas ,
                        this.meses ,
                        this.id_cobertura ,
                        this.id_barrio ,
                        this.nueva_poliza ,
                        this.premio ,
                        this.premio_total ,
                        this.fechaDesde ,
                        this.fechaHasta ,
                        this.clausula ,
                        this.barrio_beneficiario ,
                        this.ultmod ,
                        this.user_edit ,
                        this.codestado ,
                        this.cobertura_suma ,
                        this.cobertura_deducible ,
                        this.cobertura_gastos ,
                        this.promocion ,
                        this.paga ,
                        this.fecha_paga ,
                        this.referencia ,
                        this.prima,
                        this.master ,
                        this.organizador ,
                        this.productor ,
                        this.prefijo ,
                        this.formadepago ,
                        this.usuariopaga ,
                        this.tipopago ,
                        this.compformapago ,
                        this.idpropuesta ,
                        this.envionube ,
                        MDIParent1.codempresa ,
                        this.nota ,
                        this.data_barrios
                    }
                );
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar propuesta \n" + ex.Message + "\n" + sql);
                return false;
            }

        }



        public bool save()
        {
            try
            {
                bool auxidpropuesta = true;
                if(this.idpropuesta == "")
                {
                    auxidpropuesta = false;
                    this.idpropuesta = this.consecutivo_prefijo().ToString();
                }
                    
                
                if (this.exist() && auxidpropuesta == true)
                {
                    
                        if (this.denube && this.confirmVersion())
                        {

                            sql = "UPDATE propuestas SET " +
                            "documento = '" + this.documento + "'," +
                            "num_polizas = '" + this.num_polizas + "'," +
                            "meses = '" + this.meses + "'," +
                            "id_cobertura = '" + this.id_cobertura + "'," +
                            "id_barrio = '" + this.id_barrio + "'," +
                            "nueva_poliza = '" + this.nueva_poliza + "'," +
                            "premio = '" + this.premio + "'," +
                            "premio_total = '" + this.premio_total + "'," +
                            "fechaDesde = '" + this.fechaDesde + "'," +
                            "fechaHasta = '" + this.fechaHasta + "'," +
                            "clausula = '" + this.clausula + "'," +
                            "barrio_beneficiario = '" + this.barrio_beneficiario + "'," +
                            "ultmod = '" + this.ultmod + "'," +
                            "usuariopaga = '" + this.usuariopaga + "'," +
                            "fecha_paga = '" + this.fecha_paga + "'," +
                            "codestado = '" + this.codestado + "'," +
                            "cobertura_suma = '" + this.cobertura_suma + "'," +
                            "cobertura_gastos = '" + this.cobertura_gastos + "'," +
                            "cobertura_deducible = '" + this.cobertura_deducible + "'," +
                            "promocion = '" + this.promocion + "'," +
                            "paga = '" + this.paga + "'," +
                            "referencia = '" + this.referencia + "'," +
                            "prima = '" + this.prima.Trim().Replace(",", ".") + "'," +
                            "master = '" + this.master + "'," +
                            "organizador = '" + this.organizador + "'," +
                            "productor = '" + this.productor + "'," +
                            "formadepago = '" + this.formadepago + "'," +
                            "prefijo = '" + this.prefijo + "'," +
                            "tipopago = '" + this.tipopago + "'," +
                            "compformapago = '" + this.compformapago + "'," +
                            "nota = '" + this.nota + "'," +
                            "version = '"+this.version+"' , " +
                            "codempresa = '" + MDIParent1.codempresa + "'," +
                            "data_barrios = '" + this.data_barrios + "'," +
                            "idpropuesta = '" + this.idpropuesta + "'" +
                            " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "' ";

                        }
                        else
                        {
                            if (this.formadepago == "CREDITO")
                            {
                                sql = "UPDATE propuestas SET " +
                                "documento = '" + this.documento + "'," +
                                "num_polizas = '" + this.num_polizas + "'," +
                                "meses = '" + this.meses + "'," +
                                "id_cobertura = '" + this.id_cobertura + "'," +
                                "id_barrio = '" + this.id_barrio + "'," +
                                "nueva_poliza = '" + this.nueva_poliza + "'," +
                                "premio = '" + this.premio + "'," +
                                "premio_total = '" + this.premio_total + "'," +
                                "fechaDesde = '" + this.fechaDesde + "'," +
                                "fechaHasta = '" + this.fechaHasta + "'," +
                                "clausula = '" + this.clausula + "'," +
                                "barrio_beneficiario = '" + this.barrio_beneficiario + "'," +
                                "ultmod = '" + this.ultmod + "'," +
                                "codestado = '" + this.codestado + "'," +
                                "cobertura_suma = '" + this.cobertura_suma + "'," +
                                "cobertura_gastos = '" + this.cobertura_gastos + "'," +
                                "cobertura_deducible = '" + this.cobertura_deducible + "'," +
                                "promocion = '" + this.promocion + "'," +
                                "paga = '" + this.paga + "'," +
                                "usuariopaga = ''," +
                                "fecha_paga  = '1000-01-01 01:00:00'," +
                                "referencia = '" + this.referencia + "'," +
                                "prima = '" + this.prima.Trim().Replace(",", ".") + "'," +
                                "master = '" + this.master + "'," +
                                "organizador = '" + this.organizador + "'," +
                                "productor = '" + this.productor + "'," +
                                "formadepago = '" + this.formadepago + "'," +
                                "prefijo = '" + this.prefijo + "'," +
                                "tipopago = '" + this.tipopago + "'," +
                                "compformapago = '" + this.compformapago + "'," +
                                "codempresa = '" + MDIParent1.codempresa + "'," +
                                "envionube = '0'," +
                                "version = (version + 1 ) , " +
                                "data_barrios = '" + this.data_barrios + "'," +
                                "idpropuesta = '" + this.idpropuesta + "'" +
                                " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "' ";
                            }
                            else
                            {
                                sql = "UPDATE propuestas SET " +
                                "documento = '" + this.documento + "'," +
                                "num_polizas = '" + this.num_polizas + "'," +
                                "meses = '" + this.meses + "'," +
                                "id_cobertura = '" + this.id_cobertura + "'," +
                                "id_barrio = '" + this.id_barrio + "'," +
                                "nueva_poliza = '" + this.nueva_poliza + "'," +
                                "premio = '" + this.premio + "'," +
                                "premio_total = '" + this.premio_total + "'," +
                                "fechaDesde = '" + this.fechaDesde + "'," +
                                "fechaHasta = '" + this.fechaHasta + "'," +
                                "clausula = '" + this.clausula + "'," +
                                "barrio_beneficiario = '" + this.barrio_beneficiario + "'," +
                                "ultmod = '" + this.ultmod + "'," +
                                "fecha_paga = '" + this.fecha_paga + "'," +
                                "codestado = '" + this.codestado + "'," +
                                "cobertura_suma = '" + this.cobertura_suma + "'," +
                                "cobertura_gastos = '" + this.cobertura_gastos + "'," +
                                "cobertura_deducible = '" + this.cobertura_deducible + "'," +
                                "promocion = '" + this.promocion + "'," +
                                "paga = '" + this.paga + "'," +
                                "referencia = '" + this.referencia + "'," +
                                "prima = '" + this.prima.Trim().Replace(",", ".") + "'," +
                                "master = '" + this.master + "'," +
                                "organizador = '" + this.organizador + "'," +
                                "productor = '" + this.productor + "'," +
                                "formadepago = '" + this.formadepago + "'," +
                                "prefijo = '" + this.prefijo + "'," +
                                "tipopago = '" + this.tipopago + "'," +
                                "compformapago = '" + this.compformapago + "'," +
                                "envionube = '0'," +
                                "version = (version + 1 ) , " +
                                "codempresa = '" + MDIParent1.codempresa + "'," +
                                "data_barrios = '" + this.data_barrios + "'," +
                                "idpropuesta = '" + this.idpropuesta + "'" +
                                " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "' ";
                            }
                        }
                    
                    
                }
                else
                {
                    

                    sql = "INSERT INTO propuestas (" + this.columns + ") VALUES(" +
                        "'" + this.documento + "'," +
                        "'" + this.num_polizas + "'," +
                        "'" + this.meses + "'," +
                        "'" + this.id_cobertura + "'," +
                        "'" + this.id_barrio + "'," +
                        "'" + this.nueva_poliza + "'," +
                        "'" + this.premio + "'," +
                        "'" + this.premio_total + "'," +
                        "'" + this.fechaDesde + "'," +
                        "'" + this.fechaHasta + "'," +
                        "'" + this.clausula + "'," +
                        "'" + this.barrio_beneficiario + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.cobertura_suma + "'," +
                        "'" + this.cobertura_deducible + "'," +
                        "'" + this.cobertura_gastos + "'," +
                        "'" + this.promocion + "'," +
                        "'" + this.paga + "'," +
                        "'" + this.fecha_paga + "'," +
                        "'" + this.referencia + "'," +
                        "'" + this.prima.Trim().Replace(",", ".") + "'," +
                        "'" + this.master + "'," +
                        "'" + this.organizador+ "'," +
                        "'" + this.productor + "'," +
                        "'" + this.prefijo + "'," +
                        "'" + this.formadepago + "'," +
                        "'" + this.usuariopaga + "'," +
                        "'" + this.tipopago + "'," +
                        "'" + this.compformapago + "'," +
                        "'" + this.idpropuesta + "'," +
                        "'" + this.envionube + "'," +
                        "'" + MDIParent1.codempresa+ "'," +
                        "'" + this.nota + "'," +
                        "'" + this.data_barrios + "'," +
                        "'" + this.version + "'" +

                        ") ";

                }

                if (sql.IndexOf("INSERT") > -1)
                {
                    Random rnd = new Random();

                    int mili = DateTime.Now.Millisecond + (rnd.Next(4) * 1000);
                    System.Threading.Thread.Sleep(mili);

                    if (verify_pref_cons(this.prefijo, this.idpropuesta) == false)
                    {
                        this.idpropuesta = "";
                        this.idpropuesta = this.consecutivo_prefijo().ToString();
                        this.id_barrio = this.idpropuesta;

                        sql = "INSERT INTO propuestas (" + this.columns + ") VALUES(" +
                        "'" + this.documento + "'," +
                        "'" + this.num_polizas + "'," +
                        "'" + this.meses + "'," +
                        "'" + this.id_cobertura + "'," +
                        "'" + this.id_barrio + "'," +
                        "'" + this.nueva_poliza + "'," +
                        "'" + this.premio + "'," +
                        "'" + this.premio_total + "'," +
                        "'" + this.fechaDesde + "'," +
                        "'" + this.fechaHasta + "'," +
                        "'" + this.clausula + "'," +
                        "'" + this.barrio_beneficiario + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.cobertura_suma + "'," +
                        "'" + this.cobertura_deducible + "'," +
                        "'" + this.cobertura_gastos + "'," +
                        "'" + this.promocion + "'," +
                        "'" + this.paga + "'," +
                        "'" + this.fecha_paga + "'," +
                        "'" + this.referencia + "'," +
                        "'" + this.prima.Trim().Replace(",", ".") + "'," +
                        "'" + this.master + "'," +
                        "'" + this.organizador + "'," +
                        "'" + this.productor + "'," +
                        "'" + this.prefijo + "'," +
                        "'" + this.formadepago + "'," +
                        "'" + this.usuariopaga + "'," +
                        "'" + this.tipopago + "'," +
                        "'" + this.compformapago + "'," +
                        "'" + this.idpropuesta + "'," +
                        "'" + this.envionube + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + this.nota + "'," +
                        "'" + this.data_barrios + "'," +
                        "'" + this.version + "'" +

                        ") ";
                    }
                }

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar propuesta \n"+ex.Message+ "\n"+sql);
                return false;
            }

        }

        public void save_concat(string sql_)
        {
            con.query(sql_);
        }

        public string concat_sql()
        {
            try
            {
                if (this.idpropuesta == "")
                    this.idpropuesta = this.consecutivo_prefijo().ToString();

                this.data_barrios = this.data_barrios == "" ? "{\"barrios\":[]}" : this.data_barrios;

                if (this.exist())
                {
                    if (this.confirmVersion())
                    {
                        if (this.denube)
                        {

                            sql = "UPDATE propuestas SET " +
                            "documento = '" + this.documento + "'," +
                            "num_polizas = '" + this.num_polizas + "'," +
                            "meses = '" + this.meses + "'," +
                            "id_cobertura = '" + this.id_cobertura + "'," +
                            "id_barrio = '" + this.id_barrio + "'," +
                            "nueva_poliza = '" + this.nueva_poliza + "'," +
                            "premio = '" + this.premio + "'," +
                            "premio_total = '" + this.premio_total + "'," +
                            "fechaDesde = '" + this.fechaDesde + "'," +
                            "fechaHasta = '" + this.fechaHasta + "'," +
                            "clausula = '" + this.clausula + "'," +
                            "barrio_beneficiario = '" + this.barrio_beneficiario + "'," +
                            "ultmod = '" + this.ultmod + "'," +
                            "usuariopaga = '" + this.usuariopaga + "'," +
                            "fecha_paga = '" + this.fecha_paga + "'," +
                            "codestado = '" + this.codestado + "'," +
                            "cobertura_suma = '" + this.cobertura_suma + "'," +
                            "cobertura_gastos = '" + this.cobertura_gastos + "'," +
                            "cobertura_deducible = '" + this.cobertura_deducible + "'," +
                            "promocion = '" + this.promocion + "'," +
                            "paga = '" + this.paga + "'," +
                            "referencia = '" + this.referencia + "'," +
                            "prima = '" + this.prima.Trim().Replace(",", ".") + "'," +
                            "master = '" + this.master + "'," +
                            "organizador = '" + this.organizador + "'," +
                            "productor = '" + this.productor + "'," +
                            "formadepago = '" + this.formadepago + "'," +
                            "prefijo = '" + this.prefijo + "'," +
                            "tipopago = '" + this.tipopago + "'," +
                            "compformapago = '" + this.compformapago + "'," +
                            "nota = '" + this.nota + "'," +
                            "codempresa = '" + MDIParent1.codempresa + "'," +
                            "data_barrios = '" + this.data_barrios + "'," +
                            "version = '" + this.version + "'," +
                            "idpropuesta = '" + this.idpropuesta + "'" +
                            " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "'; ";

                        }
                        else
                        {
                            if (this.formadepago == "CREDITO")
                            {
                                sql = "UPDATE propuestas SET " +
                                "num_polizas = '" + this.num_polizas + "'," +
                                "meses = '" + this.meses + "'," +
                                "id_cobertura = '" + this.id_cobertura + "'," +
                                "id_barrio = '" + this.id_barrio + "'," +
                                "nueva_poliza = '" + this.nueva_poliza + "'," +
                                "premio = '" + this.premio + "'," +
                                "premio_total = '" + this.premio_total + "'," +
                                "fechaDesde = '" + this.fechaDesde + "'," +
                                "fechaHasta = '" + this.fechaHasta + "'," +
                                "clausula = '" + this.clausula + "'," +
                                "barrio_beneficiario = '" + this.barrio_beneficiario + "'," +
                                "ultmod = '" + this.ultmod + "'," +
                                "codestado = '" + this.codestado + "'," +
                                "cobertura_suma = '" + this.cobertura_suma + "'," +
                                "cobertura_gastos = '" + this.cobertura_gastos + "'," +
                                "cobertura_deducible = '" + this.cobertura_deducible + "'," +
                                "promocion = '" + this.promocion + "'," +
                                "paga = '" + this.paga + "'," +
                                "usuariopaga = ''," +
                                "fecha_paga  = '1000-01-01 01:00:00'," +
                                "referencia = '" + this.referencia + "'," +
                                "prima = '" + this.prima.Trim().Replace(",", ".") + "'," +
                                "master = '" + this.master + "'," +
                                "organizador = '" + this.organizador + "'," +
                                "productor = '" + this.productor + "'," +
                                "formadepago = '" + this.formadepago + "'," +
                                "prefijo = '" + this.prefijo + "'," +
                                "tipopago = '" + this.tipopago + "'," +
                                "compformapago = '" + this.compformapago + "'," +
                                "codempresa = '" + MDIParent1.codempresa + "'," +
                                "envionube = '0'," +
                                "version = (version + 1 ) , " +
                                "data_barrios = '" + this.data_barrios + "'," +
                                "idpropuesta = '" + this.idpropuesta + "'" +
                                " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "' ; ";
                            }
                            else
                            {
                                sql = "UPDATE propuestas SET " +
                                "num_polizas = '" + this.num_polizas + "'," +
                                "meses = '" + this.meses + "'," +
                                "id_cobertura = '" + this.id_cobertura + "'," +
                                "id_barrio = '" + this.id_barrio + "'," +
                                "nueva_poliza = '" + this.nueva_poliza + "'," +
                                "premio = '" + this.premio + "'," +
                                "premio_total = '" + this.premio_total + "'," +
                                "fechaDesde = '" + this.fechaDesde + "'," +
                                "fechaHasta = '" + this.fechaHasta + "'," +
                                "clausula = '" + this.clausula + "'," +
                                "barrio_beneficiario = '" + this.barrio_beneficiario + "'," +
                                "ultmod = '" + this.ultmod + "'," +
                                "fecha_paga = '" + this.fecha_paga + "'," +
                                "codestado = '" + this.codestado + "'," +
                                "cobertura_suma = '" + this.cobertura_suma + "'," +
                                "cobertura_gastos = '" + this.cobertura_gastos + "'," +
                                "cobertura_deducible = '" + this.cobertura_deducible + "'," +
                                "promocion = '" + this.promocion + "'," +
                                "paga = '" + this.paga + "'," +
                                "referencia = '" + this.referencia + "'," +
                                "prima = '" + this.prima.Trim().Replace(",", ".") + "'," +
                                "master = '" + this.master + "'," +
                                "organizador = '" + this.organizador + "'," +
                                "productor = '" + this.productor + "'," +
                                "formadepago = '" + this.formadepago + "'," +
                                "prefijo = '" + this.prefijo + "'," +
                                "tipopago = '" + this.tipopago + "'," +
                                "compformapago = '" + this.compformapago + "'," +
                                "envionube = '0'," +
                                "version = (version + 1 ) , " +
                                "codempresa = '" + MDIParent1.codempresa + "'," +
                                "data_barrios = '" + this.data_barrios + "'," +
                                "idpropuesta = '" + this.idpropuesta + "'" +
                                " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '" + this.prefijo + "'; ";
                            }
                        }
                    }
                    

                }
                else
                {


                    sql = "INSERT INTO propuestas (" + this.columns + ") VALUES(" +
                        "'" + this.documento + "'," +
                        "'" + this.num_polizas + "'," +
                        "'" + this.meses + "'," +
                        "'" + this.id_cobertura + "'," +
                        "'" + this.id_barrio + "'," +
                        "'" + this.nueva_poliza + "'," +
                        "'" + this.premio + "'," +
                        "'" + this.premio_total + "'," +
                        "'" + this.fechaDesde + "'," +
                        "'" + this.fechaHasta + "'," +
                        "'" + this.clausula + "'," +
                        "'" + this.barrio_beneficiario + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.cobertura_suma + "'," +
                        "'" + this.cobertura_deducible + "'," +
                        "'" + this.cobertura_gastos + "'," +
                        "'" + this.promocion + "'," +
                        "'" + this.paga + "'," +
                        "'" + this.fecha_paga + "'," +
                        "'" + this.referencia + "'," +
                        "'" + this.prima.Trim().Replace(",", ".") + "'," +
                        "'" + this.master + "'," +
                        "'" + this.organizador + "'," +
                        "'" + this.productor + "'," +
                        "'" + this.prefijo + "'," +
                        "'" + this.formadepago + "'," +
                        "'" + this.usuariopaga + "'," +
                        "'" + this.tipopago + "'," +
                        "'" + this.compformapago + "'," +
                        "'" + this.idpropuesta + "'," +
                        "'" + this.envionube + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + this.nota + "'," +
                        "'" + this.data_barrios + "', " +
                        "'" + this.version + "' " +

                        "); ";


                }


                return sql;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar propuesta \n" + ex.Message + "\n" + sql);
                return "";
            }

        }

        public bool get_paga(string prefijo_, string idpropuesta_)
        {

            DataSet dspr = this.getprefijo(prefijo_, idpropuesta_);
            if (dspr.Tables[0].Rows.Count > 0)
            {
                if (dspr.Tables[0].Rows[0]["paga"].ToString() != "")
                {
                    if (dspr.Tables[0].Rows[0]["paga"].ToString() == "1")
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public void pagar(string id_, string prefijo_, string tipopago_, string compago_, string fechapago_ = "" )
        {
            if(fechapago_ == "")
                fechapago_ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            sql = "UPDATE propuestas SET paga = 1, usuariopaga = '" + MDIParent1.sesionUser + "', fecha_paga = '" +
                fechapago_ + "',compformapago = '" + compago_ + "',tipopago = '" + tipopago_ + "',envionube = 0, version = ( version + 1 )  WHERE " +
                " idpropuesta = '" + id_.Trim() + "' AND prefijo = '" + prefijo_ + "' ";
            con.query(sql);
        }

        public void enviohecho()
        {
            sql = "UPDATE propuestas SET envionube = 1 WHERE envionube = 0   ";

            Console.WriteLine("\nENVIO HECHO \n"+sql);

            con.query(sql);
        }


        public void enviohechoFecha()
        {
            DateTime fec = DateTime.Now.AddDays(-7);
            sql = "UPDATE propuestas SET envionube = 1 WHERE envionube = 0  AND ultmod < '" + fec.ToString("yyyy-MM-dd HH:mm:ss") +"' ";
            con.query(sql);
        }


        public void no_vigente(string id_)
        {
            sql = "UPDATE propuestas SET codestado = 2 WHERE id = '" + id_ + "'  ";
            con.query(sql);
        }

        public void no_vigente_all(string fecha_)
        {
            sql = "UPDATE propuestas SET codestado = 2 WHERE fechaHasta <= '" + fecha_ + "' AND codestado = 1 ";
            con.query(sql);
        }


        public void asignar_referencia1(string fecha_)
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "UPDATE propuestas AS p, controlventas AS c  " +
                    "SET p.referencia = c.referencia, p.prima = c.primaemitida, p.nota = c.notacredito  " +
                    " WHERE ( p.referencia IS NULL OR p.referencia = '' ) AND CONCAT(p.prefijo,p.idpropuesta) = c.codgrupo  AND c.fecha = '" + fecha_+"'";
                
            }
            else
            {
                sql = "UPDATE propuestas AS p, controlventas AS c  " +
                    "SET p.referencia = c.referencia, p.prima = c.primaemitida, p.nota = c.notacredito  " +
                    " WHERE ( p.referencia IS NULL OR p.referencia = '' ) AND (p.prefijo || p.idpropuesta) = c.codgrupo  AND c.fecha = '" + fecha_ + "'";
            }
            con.query(sql);


        }
        public void asignar_referencia(string refe, string codgrupo_, string prima_, string nota_)
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "UPDATE propuestas SET referencia = '" + refe + "', prima = '" + prima_ + "', nota = '" + nota_ + "', envionube = '0', version = ( version + 1 )  WHERE CONCAT(prefijo,idpropuesta) = '" + codgrupo_ + "' ";
            }
            else
            {
                sql = "UPDATE propuestas SET referencia = '" + refe + "', prima = '" + prima_ + "', nota = '" + nota_ + "', envionube = '0', version = ( version + 1 ) WHERE (prefijo || idpropuesta) = '" + codgrupo_ + "' ";
            }
            con.query(sql);

           
        }

        public List<RegistroRef> getRefPropuestas(string fecha_)
        {
            this.con = new conexion();
            sql = $"SELECT prefijo,idpropuesta,referencia,nota,prima FROM propuestas  WHERE referencia != '' AND DATE(ultmod) = '{fecha_}'";
            IEnumerable<RegistroRef> refpro1;

            if (MDIParent1.baseDatos == "MySql")
            {
                this.con.conectarDB();
                refpro1 = this.con.conecction.Query<RegistroRef>(sql);
            }
            else
            {
                this.con.conectarDBSQLITE();
                refpro1 = this.con.connectionSQLite.Query<RegistroRef>(sql);
            }

            return refpro1.ToList();
        }

        public void anular_propuesta(string prefijo_, string idpropuesta_)
        {
            sql = "UPDATE propuestas SET codestado = 0, envionube = 0, version = ( version + 1 ) WHERE idpropuesta = '" + idpropuesta_ + "' AND prefijo = '" + prefijo_ + "' ";
            con.query(sql);
        }

        public void modificacionClausulas(string prefijo_, string idpropuesta_)
        {
            sql = "UPDATE propuestas SET  envionube = 0, data_barrios = '"+this.data_barrios+ "', version = ( version + 1 ) WHERE idpropuesta = '" + idpropuesta_ + "' AND prefijo = '" + prefijo_ + "' ";
            con.query(sql);
        }

        public void delete()
        {
            sql = "UPDATE propuestas SET codestado = 0 WHERE id = '" + this.id + "' ";
            con.query(sql);
        }
        
    }

}
