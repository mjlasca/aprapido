using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{   
    class arqueos
    {

        string sql = "";

        string columns = " usuario, nombre, fechadia, valorinicial,dinerorealcaja,valormanual, cuadredescuadre, supervisor, nombresupervisor, observaciones, ultmod,user_edit,codestado,rendicion,cantpolizas,codempresa,envionube";
        public string  id, usuario, nombre, fechadia, valorinicial, dinerorealcaja, valormanual, cuadredescuadre, supervisor, nombresupervisor, observaciones, ultmod, user_edit, codestado, rendicion, codempresa, envionube = "";
        public int cantpolizas = 0;
        conexion con = new conexion();

        public arqueos(bool insta = false) 
        {
            if(insta)
                this.install();
        }


        private void install()
        {
            /*sql = "DROP TABLE arqueos";
            con.query(sql);*/
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists arqueos (id serial PRIMARY KEY, usuario VARCHAR(100) NULL, nombre VARCHAR(150) NULL, " +
                " fechadia DATE NULL, valorinicial DOUBLE NULL, dinerorealcaja DOUBLE NULL, valormanual DOUBLE NULL, cuadredescuadre DOUBLE NULL, " +
                " supervisor VARCHAR(100) NULL, nombresupervisor VARCHAR(150) NULL, observaciones VARCHAR(350) NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL, rendicion VARCHAR(20) NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists arqueos (id INTEGER PRIMARY KEY, usuario VARCHAR(100) NULL, nombre VARCHAR(150) NULL, " +
                " fechadia DATE NULL, valorinicial DOUBLE NULL, dinerorealcaja DOUBLE NULL, valormanual DOUBLE NULL, cuadredescuadre DOUBLE NULL, " +
                " supervisor VARCHAR(100) NULL, nombresupervisor VARCHAR(150) NULL, observaciones VARCHAR(350) NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL, rendicion VARCHAR(20) NULL );";
            }

            con.query(sql);

            this.addColumn();

            /*
            sql = "DELETE FROM arqueos WHERE (SELECT MIN(t1.id) FROM arqueos t1 WHERE DATE(t1.fechadia) = '2021-03-02' " +
                " AND(t1.usuario = '' OR t1.usuario = '1')) < id AND fechadia = '2021-03-02' AND(usuario = '' OR usuario = '1')";
            con.query(sql);
            sql = "DELETE FROM arqueos WHERE (SELECT MIN(t1.id) FROM arqueos t1 WHERE DATE(t1.fechadia) = '2021-03-03' " +
                " AND(t1.usuario = '' OR t1.usuario = '1')) < id AND fechadia = '2021-03-03' AND(usuario = '' OR usuario = '1')";
            con.query(sql);
            sql = "DELETE FROM arqueos WHERE (SELECT MIN(t1.id) FROM arqueos t1 WHERE DATE(t1.fechadia) = '2021-03-04' " +
                " AND(t1.usuario = '' OR t1.usuario = '1')) < id AND fechadia = '2021-03-04' AND(usuario = '' OR usuario = '1')";
            con.query(sql);
            sql = "DELETE FROM arqueos WHERE (SELECT MIN(t1.id) FROM arqueos t1 WHERE DATE(t1.fechadia) = '2021-03-05' " +
                " AND(t1.usuario = '' OR t1.usuario = '1')) < id AND fechadia = '2021-03-05' AND(usuario = '' OR usuario = '1')";
            con.query(sql);
            sql = "DELETE FROM arqueos WHERE (SELECT MIN(t1.id) FROM arqueos t1 WHERE DATE(t1.fechadia) = '2021-03-06' " +
                " AND(t1.usuario = '' OR t1.usuario = '1')) < id AND fechadia = '2021-03-06' AND(usuario = '' OR usuario = '1')";
            con.query(sql);
            sql = "DELETE FROM arqueos WHERE (SELECT MIN(t1.id) FROM arqueos t1 WHERE DATE(t1.fechadia) = '2021-03-07' " +
                " AND(t1.usuario = '' OR t1.usuario = '1')) < id AND fechadia = '2021-03-07' AND(usuario = '' OR usuario = '1')";
            con.query(sql);
            sql = "DELETE FROM arqueos WHERE (SELECT MIN(t1.id) FROM arqueos t1 WHERE DATE(t1.fechadia) = '2021-03-08' " +
                " AND(t1.usuario = '' OR t1.usuario = '1')) < id AND fechadia = '2021-03-08' AND(usuario = '' OR usuario = '1')";
            con.query(sql);
            sql = "DELETE FROM arqueos WHERE (SELECT MIN(t1.id) FROM arqueos t1 WHERE DATE(t1.fechadia) = '2021-03-09' " +
                " AND(t1.usuario = '' OR t1.usuario = '1')) < id AND fechadia = '2021-03-09' AND(usuario = '' OR usuario = '1')";
            con.query(sql);
            */
        }

        public void eliminarblancos(string fechita)
        {
            sql = "SELECT MIN(t1.id) as idmin FROM arqueos t1 WHERE DATE(t1.fechadia) = '"+ fechita + "' AND(t1.usuario = '' OR t1.usuario = '1')";
            DataSet ds = con.query(sql);
            if(ds.Tables[0].Rows.Count > 0)
            {
                if(ds.Tables[0].Rows[0]["idmin"] != null)
                {
                    if (ds.Tables[0].Rows[0]["idmin"].ToString() != "")
                    {
                        sql = "DELETE FROM arqueos WHERE  fechadia = '" + fechita + "' AND id > '" + ds.Tables[0].Rows[0]["idmin"].ToString() + "' AND(usuario = '' OR usuario = '1')";
                        con.query(sql);
                        //Console.WriteLine("EXEC " + sql);
                    }
                }

                
                
            }

            
        }


        public DataSet get_all_fecha_export(string fecha)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM arqueos  WHERE ultmod >= '" + fecha + "' AND user_edit = '" + MDIParent1.sesionUser + "' AND ultmod <= '"
                + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' AND supervisor != '' ORDER BY id ASC ";
            if(DateTime.Now.ToString("yyyy-MM-dd") == Convert.ToDateTime(fecha).ToString("yyyy-MM-dd") )
            {
                sql = "SELECT * FROM arqueos  WHERE ultmod >= '" + Convert.ToDateTime(fecha).ToString("yyyy-MM-dd 06:00:00") + "' AND user_edit = '" + MDIParent1.sesionUser + "' AND ultmod <= '"
                + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' AND supervisor != '' ORDER BY id ASC ";
            }

            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar actividades "+ex.Message);
            }

            return ds;
        }

        public int consecutivo()
        {
            DataSet ds = new DataSet();

            sql = "SELECT COUNT(*) as cant FROM arqueos";
            
            try
            {
                ds = con.query(sql);
                return (Convert.ToInt32(ds.Tables[0].Rows[0]["cant"].ToString())+1);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar arqueos "+ex.Message);
            }

            return 0;
        }


        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                try
                {

                    //Add column categoria if NOT exist
                    if (con.query("SHOW COLUMNS FROM arqueos WHERE Field = 'cantpolizas' ").Tables[0].Rows.Count == 0)
                    {
                        con.query("ALTER TABLE arqueos ADD COLUMN cantpolizas int(5) NULL");
                    }
                    if (con.query("SHOW COLUMNS FROM arqueos WHERE Field = 'codempresa' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE arqueos ADD COLUMN codempresa VARCHAR(100) NULL");
                    if (con.query("SHOW COLUMNS FROM arqueos WHERE Field = 'envionube' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE arqueos ADD COLUMN envionube VARCHAR(100) NULL");
                }
                catch
                {
                    //
                }
            }
            else
            {
                
                con.query("ALTER TABLE arqueos ADD COLUMN cantpolizas int(5) NULL");
                con.query("ALTER TABLE arqueos ADD COLUMN codempresa VARCHAR(100) NULL");
                con.query("ALTER TABLE arqueos ADD COLUMN envionube VARCHAR(100) NULL");
            }


        }

        public bool exist()
        {
            sql = "SELECT id FROM arqueos WHERE id = '" + this.id + "'";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }


            return false;
        }

        public bool actualizarRendicion(string fec, string rend)
        {
            sql = " UPDATE arqueos SET rendicion = '"+rend+"', ultmod = '"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"' WHERE fechadia = '"+ fec + "' ";
            if (con.query(sql).Tables.Count > 0)
            {
                return true;
            }


            return false;
        }

        

        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM arqueos WHERE codestado = 1  ORDER BY id DESC ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar arqueos");
            }

            return ds;
        }


        public DataSet get_all_pordia_rendiciones(string fec1, string fec2)
        {
            DataSet ds = new DataSet();

            sql = "SELECT t1.fechadia, SUM(t1.valormanual) as valormanual, COUNT(*) - (SELECT COUNT(*) FROM arqueos WHERE t1.fechadia = fechadia ) as completo "+
                " FROM arqueos t1 WHERE t1.codestado = 1 AND t1.supervisor <> '' AND ( t1.rendicion = '' OR t1.rendicion IS NULL ) "+
                " AND t1.fechadia BETWEEN '" + fec1 + "' AND '" + fec2 + "' GROUP BY t1.fechadia  ORDER BY t1.fechadia DESC";
            
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar arqueos");
            }

            return ds;
        }


        public DataSet get_all_rendiciones(string fec1, string fec2)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM arqueos WHERE codestado = 1 "+
                " AND supervisor <> '' AND ( rendicion = '' || rendicion IS NULL ) AND fechadia BETWEEN '"+fec1+ "' AND '" + fec2 + "'  ORDER BY id DESC";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar arqueos");
            }

            return ds;
        }





        public DataSet get_all_busqueda(string busqueda, string fecha1, string fecha2, string cierre = " ")
        {
            DataSet ds = new DataSet();

            
            object cacheGet = CacheManager.GetFromCache(busqueda + fecha1 + fecha2);
            if(cacheGet != null)
            {
                //return (DataSet)cacheGet;
            }

            if(cierre == "Sin Cerrar")
            {
                cierre = " AND ( t1.supervisor = '' OR t1.supervisor IS NULL ) ";
            }
            if (cierre == "Cerrados")
            {
                cierre = " AND ( t1.supervisor != '' OR t1.supervisor IS NOT NULL ) ";
            }

            /*if (MDIParent1.baseDatos == "MySql")
            {
                sql = "SELECT t1.id,t1.fechadia,t1.nombre,t1.usuario,t1.valormanual,t1.cuadredescuadre,t1.nombresupervisor,t1.observaciones,"+
                    "( SELECT COUNT(1) FROM propuestas WHERE codestado >  '0' AND DATE(ultmod) = t1.fechadia AND user_edit = t1.usuario ) as cantpoli," +
                "( SELECT SUM(premio_total) FROM propuestas WHERE codestado >  '0' AND ( DATE(ultmod) = t1.fechadia AND user_edit = t1.usuario AND formadepago = 'CONTADO') ) as contado, " +
                "( SELECT SUM(premio_total) FROM propuestas WHERE codestado >  '0'   AND DATE(ultmod) = t1.fechadia AND user_edit = t1.usuario  AND formadepago = 'CREDITO'  ) as credito," +
                " ( SELECT SUM(premio_total) FROM propuestas WHERE codestado >  '0' AND DATE(ultmod) = t1.fechadia AND user_edit = t1.usuario ) as total,  " +
                " ( SELECT SUM(premio_total) FROM propuestas WHERE codestado >  '0' AND ( DATE(fecha_paga) = t1.fechadia AND usuariopaga = t1.usuario AND paga = 1 AND formadepago = 'CREDITO') ) as pagoscreditos,  " +
                " ( SELECT SUM(premio_total) FROM propuestas WHERE codestado >  '0' AND ( DATE(fecha_paga) = t1.fechadia AND usuariopaga = t1.usuario AND paga = 1 AND formadepago = 'CREDITO' AND tipopago != 'EFECTIVO') ) as pagoscreditos_difefectivo,  " +
                " ( SELECT SUM(premio_total) FROM propuestas WHERE codestado >  '0' AND ( DATE(fecha_paga) = t1.fechadia AND usuariopaga = t1.usuario AND paga = 1) ) as realcaja " +
                " FROM arqueos t1 WHERE t1.fechadia  BETWEEN '" + fecha1 + "' AND '" + fecha2 + "' AND t1.codestado = 1 " +
                " AND ( t1.id = '" + busqueda + "' OR t1.nombre LIKE ('%" + busqueda + "%') OR t1.nombresupervisor LIKE ('%" + busqueda + "%') ) " + cierre + " ORDER BY t1.id DESC ";

                
            }
            else
            {
                sql = "SELECT *, ( SELECT COUNT(1) FROM propuestas WHERE codestado >  '0' AND DATE(ultmod) = t1.fechadia AND user_edit = t1.usuario ) as cantpoli," +
                "( SELECT SUM(premio_total) FROM propuestas WHERE codestado >  '0' AND ( DATE(ultmod) = t1.fechadia AND user_edit = t1.usuario AND formadepago = 'CONTADO') ) as contado, " +
                "( SELECT SUM(premio_total) FROM propuestas WHERE codestado >  '0'   AND DATE(ultmod) = t1.fechadia AND user_edit = t1.usuario  AND formadepago = 'CREDITO'  ) as credito," +
                " ( SELECT SUM(premio_total) FROM propuestas WHERE codestado >  '0' AND DATE(ultmod) = t1.fechadia AND user_edit = t1.usuario ) as total,  " +
                " ( SELECT SUM(premio_total) FROM propuestas WHERE codestado >  '0' AND ( DATE(fecha_paga) = t1.fechadia AND usuariopaga = t1.usuario AND paga = 1 AND formadepago = 'CREDITO') ) as pagoscreditos,  " +
                " ( SELECT SUM(premio_total) FROM propuestas WHERE codestado >  '0' AND ( DATE(fecha_paga) = t1.fechadia AND usuariopaga = t1.usuario AND paga = 1 AND formadepago = 'CREDITO' AND tipopago != 'EFECTIVO') ) as pagoscreditos_difefectivo,  " +
                " ( SELECT SUM(premio_total) FROM propuestas WHERE codestado >  '0' AND ( DATE(fecha_paga) = t1.fechadia AND usuariopaga = t1.usuario AND paga = 1) ) as realcaja " +
                " FROM arqueos t1 WHERE t1.fechadia  BETWEEN '" + fecha1 + "' AND '" + fecha2 + "' AND t1.codestado = 1 "+
                " AND ( id = '" + busqueda + "' OR nombre LIKE ('%" + busqueda + "%') OR nombresupervisor LIKE ('%" + busqueda + "%') ) " + cierre + " ORDER BY t1.id DESC ";
            }*/

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"
                                   SELECT t1.id,t1.fechadia,t1.nombre,t1.usuario,t1.valormanual,t1.cuadredescuadre,t1.nombresupervisor,t1.observaciones, 
                                           SUM(CASE WHEN DATE(p1.ultmod) = t1.fechadia AND p1.user_edit = t1.usuario THEN 1 ELSE 0 END) AS cantpoli,
                                           SUM(CASE WHEN DATE(p1.ultmod) = t1.fechadia AND p1.user_edit = t1.usuario AND  p1.formadepago = 'CONTADO' THEN p1.premio_total ELSE 0 END) AS contado,
                                           SUM(CASE WHEN  DATE(p1.ultmod) = t1.fechadia AND p1.user_edit = t1.usuario AND  p1.formadepago = 'CREDITO' THEN p1.premio_total ELSE 0 END) AS credito,
                                           SUM(CASE WHEN DATE(p1.ultmod) = t1.fechadia AND p1.user_edit = t1.usuario THEN p1.premio_total ELSE 0 END) AS total,
                                           SUM(CASE WHEN   DATE(p1.fecha_paga) = t1.fechadia AND p1.usuariopaga = t1.usuario AND p1.paga = 1 AND p1.formadepago = 'CREDITO' THEN p1.premio_total ELSE 0 END) AS pagoscreditos,
                                           SUM(CASE WHEN   DATE(p1.fecha_paga) = t1.fechadia AND p1.usuariopaga = t1.usuario AND  p1.paga = 1 AND p1.formadepago = 'CREDITO' AND p1.tipopago != 'EFECTIVO' THEN p1.premio_total ELSE 0 END) AS pagoscreditos_difefectivo,
                                           SUM(CASE WHEN   DATE(p1.fecha_paga) = t1.fechadia AND p1.usuariopaga = t1.usuario AND p1.paga = 1 THEN p1.premio_total ELSE 0 END) AS realcaja
                                    FROM arqueos t1
                                    LEFT JOIN propuestas p1 ON p1.id > 0
                                    WHERE t1.fechadia BETWEEN '@fecha1' AND '@fecha2'
                                      AND t1.codestado = 1
                                      AND p1.codestado > 0
                                      AND ( t1.id = '%@busqueda%' OR t1.nombre LIKE '%@busqueda%' OR t1.nombresupervisor LIKE '%@busqueda%' )
                                    @cierre  
                                    GROUP BY t1.id
                                    ORDER BY t1.id DESC;
                                ";

            // Añadir parámetros al comando SQL
            cmd.Parameters.AddWithValue("@fecha1", fecha1);
            cmd.Parameters.AddWithValue("@fecha2", fecha2);
            cmd.Parameters.AddWithValue("@busqueda", busqueda);
            cmd.Parameters.AddWithValue("@cierre", cierre);
            string consultaConParametros = cmd.CommandText;
            foreach (SqlParameter param in cmd.Parameters)
            {
                consultaConParametros = consultaConParametros.Replace(param.ParameterName, param.Value.ToString());
            }
            Console.WriteLine(sql);
            // Imprimir la consulta SQL con los parámetros incluidos
            Console.WriteLine(consultaConParametros);
            sql = consultaConParametros;

            Console.WriteLine(sql);
            
            try
            {
                ds = con.query(sql);
                if(fecha1 != "")
                {
                    DateTime fec1 = Convert.ToDateTime(fecha1);
                    if (DateTime.Compare(fec1.Date, DateTime.Now.AddDays(-2)) < 0)
                    {
                        CacheManager.AddToCache(
                            busqueda + fecha1 + fecha2,
                            ds,
                            new TimeSpan(1, 1, 1)
                        );
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar arqueos "+ex.Message);
            }

            return ds;
        }

        public string get_sum_fecha(string fecha_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT SUM(valormanual) as valor FROM arqueos WHERE 	fechadia = '" + fecha_+"' ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["valor"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar arqueos");
            }

            return "";
        }

        public string get_max_fecha()
        {
            DataSet ds = new DataSet();

            sql = "SELECT MAX(fechadia) as maxfecha FROM arqueos WHERE codestado = 1";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return  ds.Tables[0].Rows[0]["maxfecha"].ToString() ;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar arqueos");
            }

            return "";
        }


        public bool get_fecha_user(string fecha, string user_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM arqueos WHERE codestado = 1 AND usuario = '" + user_ + "' AND fechadia = '"+fecha+"' ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.id = ds.Tables[0].Rows[0]["id"].ToString();
                    this.usuario = ds.Tables[0].Rows[0]["usuario"].ToString();
                    this.nombre = ds.Tables[0].Rows[0]["nombre"].ToString();
                    this.fechadia = ds.Tables[0].Rows[0]["fechadia"].ToString();
                    this.valorinicial = ds.Tables[0].Rows[0]["valorinicial"].ToString();
                    this.dinerorealcaja = ds.Tables[0].Rows[0]["dinerorealcaja"].ToString();
                    this.valormanual = ds.Tables[0].Rows[0]["valormanual"].ToString();
                    this.cuadredescuadre = ds.Tables[0].Rows[0]["cuadredescuadre"].ToString();
                    this.supervisor = ds.Tables[0].Rows[0]["supervisor"].ToString();
                    this.nombresupervisor = ds.Tables[0].Rows[0]["nombresupervisor"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
                    this.user_edit = ds.Tables[0].Rows[0]["user_edit"].ToString();
                    this.codestado = ds.Tables[0].Rows[0]["codestado"].ToString();
                    this.observaciones = ds.Tables[0].Rows[0]["observaciones"].ToString();
                    this.rendicion = ds.Tables[0].Rows[0]["rendicion"].ToString();
                    this.cantpolizas = Convert.ToInt16( ds.Tables[0].Rows[0]["cantpolizas"].ToString());

                }

                return true;
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar arqueos");
            }

            return false;
        }

        public void delete_userdate()
        {
            sql = "DELETE FROM arqueos WHERE usuario = '"+this.usuario+ "' AND fechadia = '"+this.fechadia+"' ";
            con.query(sql);
        }


        public bool get(string id_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM arqueos WHERE codestado = 1 AND id = '" + id_ + "' ";
            try
            {
                ds = con.query(sql);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    this.id = ds.Tables[0].Rows[0]["id"].ToString();
                    this.usuario = ds.Tables[0].Rows[0]["usuario"].ToString();
                    this.nombre = ds.Tables[0].Rows[0]["nombre"].ToString();
                    this.fechadia = ds.Tables[0].Rows[0]["fechadia"].ToString();
                    this.valorinicial = ds.Tables[0].Rows[0]["valorinicial"].ToString();
                    this.dinerorealcaja = ds.Tables[0].Rows[0]["dinerorealcaja"].ToString();
                    this.valormanual = ds.Tables[0].Rows[0]["valormanual"].ToString();
                    this.cuadredescuadre = ds.Tables[0].Rows[0]["cuadredescuadre"].ToString();
                    this.supervisor = ds.Tables[0].Rows[0]["supervisor"].ToString();
                    this.nombresupervisor = ds.Tables[0].Rows[0]["nombresupervisor"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
                    this.user_edit = ds.Tables[0].Rows[0]["user_edit"].ToString();
                    this.codestado = ds.Tables[0].Rows[0]["codestado"].ToString();
                    this.observaciones = ds.Tables[0].Rows[0]["observaciones"].ToString();
                    this.rendicion = ds.Tables[0].Rows[0]["rendicion"].ToString();
                    this.cantpolizas = Convert.ToInt16(ds.Tables[0].Rows[0]["cantpolizas"].ToString());

                }

                return true;
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar arqueos");
            }

            return false;
        }

        public bool save_in()
        {
            try
            {
                DataSet ds = con.query("SELECT * FROM arqueos WHERE fechadia = '"+this.fechadia+"' AND usuario = '"+this.usuario+"' ");

                if(ds.Tables[0].Rows.Count < 1)
                {
                    
                        sql = "INSERT INTO arqueos (" + this.columns + ") VALUES(" +
                                    "'" + this.usuario + "'," +
                                    "'" + this.nombre + "'," +
                                    "'" + this.fechadia + "'," +
                                    "'" + this.valorinicial + "'," +
                                    "'" + this.dinerorealcaja + "'," +
                                    "'" + this.valormanual + "'," +
                                    "'" + this.cuadredescuadre + "'," +
                                    "'" + this.supervisor + "'," +
                                    "'" + this.nombresupervisor + "'," +
                                    "'" + this.observaciones + "'," +
                                    "'" + this.ultmod + "'," +
                                    "'" + this.user_edit + "'," +
                                    "'" + this.codestado + "'," +
                                    "'" + this.rendicion + "'," +
                                    "'" + this.cantpolizas + "'," +
                                    "'" + MDIParent1.codempresa + "'," +
                                    "'" + 0 + "'" +
                                    ") ";
                    
                   
                    con.query(sql);
                }

                
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool save()
        {
            try
            {
                if (this.exist())
                {
                    sql = "UPDATE arqueos SET " +
                        "usuario = '" + this.usuario + "'," +
                        "nombre = '" + this.nombre + "'," +
                        "fechadia = '" + this.fechadia+ "'," +
                        "valorinicial = '" + this.valorinicial + "'," +
                        "dinerorealcaja = '" + this.dinerorealcaja+ "'," +
                        "valormanual = '" + this.valormanual + "'," +
                        "cuadredescuadre = '" + this.cuadredescuadre + "'," +
                        "supervisor = '" + this.supervisor + "'," +
                        "nombresupervisor = '" + this.nombresupervisor + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "user_edit = '" + this.user_edit + "'," +
                        "codestado = '" + this.codestado + "'," +
                        "observaciones = '" + this.observaciones + "'," +
                        "rendicion = '" + this.rendicion + "'," +
                        "cantpolizas = '" + this.cantpolizas + "'" +
                        " WHERE id = '" + this.id + "'";
                }
                else
                {
                    sql = "INSERT INTO arqueos (" + this.columns + ") VALUES(" +
                        "'" + this.usuario + "'," +
                        "'" + this.nombre + "'," +
                        "'" + this.fechadia + "'," +
                        "'" + this.valorinicial + "'," +
                        "'" + this.dinerorealcaja + "'," +
                        "'" + this.valormanual + "'," +
                        "'" + this.cuadredescuadre + "'," +
                        "'" + this.supervisor + "'," +
                        "'" + this.nombresupervisor + "'," +
                        "'" + this.observaciones + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.rendicion + "'," +
                        "'" + this.cantpolizas + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + this.envionube + "'" +
                        ") ";
                }

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public void save_concat(string sql1)
        {
            con.query("INSERT INTO arqueos (" + this.columns + ") VALUES " + sql1);
        }


        public string concat_sql()
        {
            try
            {
               
                sql = "(" +
                    "'" + this.usuario + "'," +
                    "'" + this.nombre + "'," +
                    "'" + this.fechadia + "'," +
                    "'" + this.valorinicial + "'," +
                    "'" + this.dinerorealcaja + "'," +
                    "'" + this.valormanual + "'," +
                    "'" + this.cuadredescuadre + "'," +
                    "'" + this.supervisor + "'," +
                    "'" + this.nombresupervisor + "'," +
                    "'" + this.observaciones + "'," +
                    "'" + this.ultmod + "'," +
                    "'" + this.user_edit + "'," +
                    "'" + this.codestado + "'," +
                    "'" + this.rendicion + "'," +
                    "'" + this.cantpolizas + "'," +
                    "'" + MDIParent1.codempresa + "'," +
                    "'" + this.envionube + "'" +
                    ") ";
                

                return sql;

            }
            catch (Exception ex)
            {
                return "";
            }

        }





    }
}
