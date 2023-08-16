using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class comisiones
    {
        string sql = "";

        string columns = "idpropuesta,user,porc_comprima,porc_compremio,totalpropuesta,fechacomision, useredit,ultmod,liquidado,prefijo,idliquidacion";
        public string id, idpropuesta = "", user = "", porc_comprima = "0", porc_compremio = "0", totalpropuesta = "0", liquidado = "0", useredit = MDIParent1.sesionUser,prefijo, idliquidacion= "";
        public string fechacomision = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        conexion con = new conexion();

        public comisiones()
        {
            this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists comisiones (id serial PRIMARY KEY, idpropuesta VARCHAR(30), user VARCHAR(150) NULL, porc_comprima double NULL, " +
                " porc_compremio double NULL, totalpropuesta double NULL, fechacomision DATETIME NULL,useredit VARCHAR(150) NULL, ultmod DATETIME NULL , liquidado int(1) DEFAULT 0, prefijo VARCHAR(20) NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists comisiones (id INTEGER PRIMARY KEY, idpropuesta VARCHAR(30), user VARCHAR(150) NULL, porc_comprima double NULL, " +
                " porc_compremio double NULL, totalpropuesta double NULL, fechacomision DATETIME NULL,useredit VARCHAR(150) NULL, ultmod DATETIME NULL , liquidado int(1) DEFAULT 0, prefijo VARCHAR(20) NULL );";
            }
                

            con.query(sql);

            this.addColumn();
            this.transicion();
        }

        private void transicion()
        {
            sql = "SELECT COUNT(1) cant FROM comisiones WHERE idpropuesta = '-1' ";

            DataSet ds = con.query(sql);

            if(ds.Tables[0].Rows[0]["cant"].ToString() == "0")
            {
                sql = "UPDATE comisiones t1, propuestas t2 SET t1.idpropuesta = t2.idpropuesta  WHERE  t1.idpropuesta = t2.id ";
                con.query(sql);

                sql = "INSERT INTO comisiones(idpropuesta) VALUES('-1')";
                con.query(sql);
            }

        }

        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                //Add column categoria if NOT exist
                if (con.query("SHOW COLUMNS FROM comisiones WHERE Field = 'prefijo' ").Tables[0].Rows.Count == 0)
                    con.query("ALTER TABLE comisiones ADD COLUMN prefijo VARCHAR(20) NULL;");
                if (con.query("SHOW COLUMNS FROM comisiones WHERE Field = 'idliquidacion' ").Tables[0].Rows.Count == 0)
                    con.query("ALTER TABLE comisiones ADD COLUMN idliquidacion VARCHAR(20) NULL;");
            }
            else
            {
                con.query("ALTER TABLE comisiones ADD COLUMN prefijo VARCHAR(20) NULL;");
                con.query("ALTER TABLE comisiones ADD COLUMN idliquidacion VARCHAR(20) NULL;");
            }

        }

        public bool get(string idpropuesta_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM comisiones WHERE  fechaarqueo = '" + idpropuesta_ + "' ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.idpropuesta = ds.Tables[0].Rows[0]["idpropuesta"].ToString();
                    this.user = ds.Tables[0].Rows[0]["user"].ToString();
                    this.porc_comprima = ds.Tables[0].Rows[0]["porc_comprima"].ToString();
                    this.porc_compremio = ds.Tables[0].Rows[0]["porc_compremio"].ToString();
                    this.fechacomision = ds.Tables[0].Rows[0]["fechacomision"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
                    this.liquidado = ds.Tables[0].Rows[0]["liquidado"].ToString();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar rendiciones " + ex.Message);
            }

            return false;
        }

        public DataSet get_datospago(string idliquidado_)
        {
            DataSet ds = new DataSet();

            if (MDIParent1.baseDatos == "MySql")
            {

                sql = "SELECT CONCAT(t2.prefijo,t2.idpropuesta)  as Propuesta " +
                    ",t2.prima as Prima,t2.premio_total as Premio, t1.porc_comprima as Porcent_Prima, t1.porc_compremio as Porcent_Premio , (t2.prima*t1.porc_comprima /100) as ComPrima, (t2.premio_total*t1.porc_compremio /100) as ComPremio" +
                " FROM comisiones t1 INNER JOIN propuestas t2 ON t2.codestado > 0 AND t2.paga = 1 AND t1.liquidado = '1' AND t1.idliquidacion = '" + idliquidado_ 
                + "' AND  t1.idpropuesta = t2.idpropuesta AND t1.prefijo = t2.prefijo ";

            }
            else
            {
                sql = "SELECT ( t2.prefijo || t2.idpropuesta)  as Propuesta " +
                    ",t2.prima as Prima,t2.premio_total as Premio, t1.porc_comprima as Porcent_Prima, t1.porc_compremio as Porcent_Premio , (t2.prima*t1.porc_comprima /100) as ComPrima, (t2.premio_total*t1.porc_compremio /100) as ComPremio " +
                " FROM comisiones t1 INNER JOIN propuestas t2 ON t2.codestado > 0 AND t2.paga = 1 AND t1.liquidado = '1' AND t1.idliquidacion = '" + idliquidado_
                + "' AND  t1.idpropuesta = t2.idpropuesta AND t1.prefijo = t2.prefijo ";
            }

            Console.WriteLine("SQL "+sql);

            try
            {

                ds = con.query(sql);

            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar rendiciones " + ex.Message);
            }

            return ds;
        }

        public DataSet get_range_commission(DateTime fec1, DateTime fec2, string user_)
        {
            DataSet ds = new DataSet();

            if (MDIParent1.baseDatos == "MySql")
            {

                sql = "SELECT t1.id as idcomision, t1.prefijo,t1.fecha_paga, t1.idpropuesta as propuesta , t1.referencia, "+
                    " (SELECT CONCAT( nombres,' ',apellidos) FROM clientes WHERE t1.documento = id LIMIT 1) as tomador, " +
                    " t1.prima,t1.premio,t1.premio_total,"+
                    "(SELECT t0.comm_prima FROM commission_user t0 WHERE t0.user_comm = t1.user_edit AND  DATE(t0.date_ini) <= DATE(t1.fecha_paga) ORDER BY DATE(date_ini) DESC  LIMIT 1) as porc_comprima," +
                    "(SELECT t0.comm_premio FROM commission_user t0 WHERE t0.user_comm = t1.user_edit AND  DATE(t0.date_ini) <= DATE(t1.fecha_paga) ORDER BY DATE(date_ini) DESC  LIMIT 1) as porc_compremio" +
                    " FROM propuestas t1 " +
                    "WHERE t1.codestado > 0 AND t1.paga = 1 AND ( ( DATE(t1.fecha_paga) BETWEEN '" + fec1.ToString("yyyy-MM-dd") +
                "' AND '" + fec2.ToString("yyyy-MM-dd") + "') ) AND (SELECT COUNT(1) FROM commission t2 WHERE  t2.prefijo = t1.prefijo AND t2.idpropuesta = t1.idpropuesta ) < 1"+
                " AND t1.user_edit = '" + user_+ "' ORDER BY DATE(t1.fecha_paga) DESC ";

            }
            else
            {
                sql = "SELECT t1.id as idcomision, t1.prefijo,t1.fecha_paga, t1.idpropuesta as propuesta , t1.referencia, " +
                    " (SELECT ( nombres || ' ' || apellidos) FROM clientes WHERE t1.documento = id LIMIT 1) as tomador, " +
                    " t1.prima,t1.premio,t1.premio_total," +
                    "(SELECT t0.comm_prima FROM commission_user t0 WHERE t0.user_comm = t1.user_edit AND  DATE(t0.date_ini) <= DATE(t1.fecha_paga) ORDER BY DATE(date_ini) DESC  LIMIT 1) as porc_comprima," +
                    "(SELECT t0.comm_premio FROM commission_user t0 WHERE t0.user_comm = t1.user_edit AND  DATE(t0.date_ini) <= DATE(t1.fecha_paga) ORDER BY DATE(date_ini) DESC  LIMIT 1) as porc_compremio" +
                    " FROM propuestas t1 " +
                    "WHERE t1.codestado > 0 AND t1.paga = 1 AND ( ( DATE(t1.fecha_paga) BETWEEN '" + fec1.ToString("yyyy-MM-dd") +
                "' AND '" + fec2.ToString("yyyy-MM-dd") + "') ) AND (SELECT COUNT(1) FROM commission t2 WHERE  t2.prefijo = t1.prefijo AND t2.idpropuesta = t1.idpropuesta ) < 1" +
                " AND t1.user_edit = '" + user_ + "' ORDER BY DATE(t1.fecha_paga) DESC ";
            }

            try
            {

                ds = con.query(sql);

            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar rendiciones " + ex.Message);
            }

            return ds;
        }

        public DataSet get_rango(DateTime fec1, DateTime fec2, string user_)
        {
            DataSet ds = new DataSet();

            if (MDIParent1.baseDatos == "MySql")
            {

                sql = "SELECT t1.id as idcomision, t2.fecha_paga, t2.prefijo, t2.idpropuesta as propuesta,t2.referencia, (SELECT CONCAT( nombres,' ',apellidos) FROM clientes WHERE t2.documento = id) as tomador" +
                    ",t2.prima,t2.premio,t2.premio_total, t1.porc_comprima, t1.porc_compremio " +
                " FROM comisiones t1 INNER JOIN propuestas t2 ON t2.codestado > 0 AND t2.paga = 1 AND ( ( DATE(t2.fecha_paga) BETWEEN '" + fec1.ToString("yyyy-MM-dd") +
                "' AND '" + fec2.ToString("yyyy-MM-dd") + "') ) AND t1.liquidado = '0' AND t1.user = '" + user_ + "' AND t1.idpropuesta = t2.idpropuesta AND t1.prefijo = t2.prefijo ";
                
            }
            else
            {
                sql = "SELECT t1.id as idcomision, t2.prefijo, t2.fecha_paga, t2.idpropuesta as propuesta,t2.referencia, (SELECT  nombres || ' ' || apellidos FROM clientes WHERE t2.documento = id) as tomador" +
                    ",t2.prima,t2.premio,t2.premio_total, t1.porc_comprima, t1.porc_compremio " +
                " FROM comisiones t1 INNER JOIN propuestas t2 ON t2.codestado > 0 AND t2.paga = 1 AND ( ( DATE(t2.fecha_paga) BETWEEN '" + fec1.ToString("yyyy-MM-dd") +
                "' AND '" + fec2.ToString("yyyy-MM-dd") + "') ) AND t1.liquidado = '0' AND t1.user = '" + user_ + "' AND t1.idpropuesta = t2.idpropuesta AND t1.prefijo = t2.prefijo ";
            }

            try
            {
                 
                ds = con.query(sql); 

            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar rendiciones "+ex.Message);
            }

            return ds;
        }


        public bool liquidar_rango_user(string idcomision_)
        {
            liquidaciones liq = new liquidaciones();
            string consecutivo = liq.consecutivo();

            try
            {
                sql = "UPDATE comisiones SET liquidado = 1, idliquidacion = '"+ consecutivo + "' WHERE id = '"+idcomision_+"' ";
                con.query(sql);
                
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool exist()
        {
            sql = "SELECT idpropuesta FROM comisiones WHERE idpropuesta = '" + this.idpropuesta + "'";

            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }


            return false;
        }

        public bool save()
        {
            try
            {
                if (exist())
                {
                    sql = "UPDATE comisiones SET " +
                       "totalpropuesta = '" + this.totalpropuesta + "'," +
                       "fechacomision = '" + this.fechacomision + "'," +
                       "useredit = '" + this.useredit + "'," +
                       "ultmod = '" + this.ultmod + "'," +
                       "liquidado = '" + this.liquidado + "'" +
                       " WHERE idpropuesta = '" + this.idpropuesta + "' AND prefijo = '"+this.prefijo+"' ";
                }
                else
                {
                    sql = "INSERT INTO comisiones (" + this.columns + ") VALUES(" +
                      "'" + this.idpropuesta + "'," +
                      "'" + this.user + "'," +
                      "'" + this.porc_comprima + "'," +
                      "'" + this.porc_compremio + "'," +
                      "'" + this.totalpropuesta + "'," +
                      "'" + this.fechacomision + "'," +
                      "'" + this.useredit + "'," +
                      "'" + this.ultmod + "'," +
                      "'" + this.liquidado + "'," +
                      "'" + this.prefijo + "'," +
                      "'" + this.idliquidacion + "'" +
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

    }
}
