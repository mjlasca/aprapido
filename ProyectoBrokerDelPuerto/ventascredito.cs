using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class ventascredito
    {
        string sql = "";

        string columns = "idpropuesta,referencia,fecha,master,tomador,, useredit,ultmod,liquidado";
        public string id, idpropuesta = "", user = "", porc_comprima = "0", porc_compremio = "0", totalpropuesta = "0", liquidado = "0", useredit = MDIParent1.sesionUser;
        public string fechacomision = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        conexion con = new conexion();

        public ventascredito()
        {
            this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists ventascredito (id serial PRIMARY KEY, idpropuesta VARCHAR(30), user VARCHAR(150) NULL, porc_comprima double NULL, " +
                " porc_compremio double NULL, totalpropuesta double NULL, fechacomision DATETIME NULL,useredit VARCHAR(150) NULL, ultmod DATETIME NULL , liquidado int(1) DEFAULT 0 );";
            }
            else
            {
                sql = "CREATE TABLE if not exists ventascredito (id INTEGER PRIMARY KEY, idpropuesta VARCHAR(30), user VARCHAR(150) NULL, porc_comprima double NULL, " +
                " porc_compremio double NULL, totalpropuesta double NULL, fechacomision DATETIME NULL,useredit VARCHAR(150) NULL, ultmod DATETIME NULL , liquidado int(1) DEFAULT 0 );";
            }

            con.query(sql);
        }

        public bool get(string idpropuesta_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM ventascredito WHERE  fechaarqueo = '" + idpropuesta_ + "' ";
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
                Console.Write("ERROR al consultar rendiciones");
            }

            return false;
        }

        public DataSet get_rango(DateTime fec1, DateTime fec2, string user_)
        {
            DataSet ds = new DataSet();

            if (MDIParent1.baseDatos == "MySql")
            {

                sql = "SELECT t2.id as propuesta,t2.referencia, (SELECT CONCAT( nombres,' ',apellidos) FROM clientes WHERE t2.documento = id) as tomador,t2.prima,t2.premio, t1.porc_comprima, t1.porc_compremio " +
                " FROM ventascredito t1 INNER JOIN propuestas t2 ON  DATE(t1.ultmod) BETWEEN '" + fec1.ToString("yyyy-MM-dd") +
                "' AND '" + fec2.ToString("yyyy-MM-dd") + "' AND t1.liquidado = '0' AND t1.user = '" + user_ + "' AND t1.idpropuesta = t2.id ";

            }
            else
            {
                sql = "SELECT t2.id as propuesta,t2.referencia, (SELECT  nombres || ' ' || apellidos FROM clientes WHERE t2.documento = id) as tomador,t2.prima,t2.premio, t1.porc_comprima, t1.porc_compremio " +
                " FROM ventascredito t1 INNER JOIN propuestas t2 ON  DATE(t1.ultmod) BETWEEN '" + fec1.ToString("yyyy-MM-dd") +
                "' AND '" + fec2.ToString("yyyy-MM-dd") + "' AND t1.liquidado = '0' AND t1.user = '" + user_ + "' AND t1.idpropuesta = t2.id ";
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


        public bool liquidar_rango_user(DateTime fec1, DateTime fec2)
        {
            try
            {
                sql = "UPDATE ventascredito SET liquidado = 1 WHERE user = '" + this.user + "' AND DATE(ultmod) >= '" + 
                    fec1.ToString("yyyy-MM-dd") + "' AND  DATE(ultmod) <= '" + fec2.ToString("yyyy-MM-dd") + "' AND liquidado = '0' ";
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
            sql = "SELECT idpropuesta FROM ventascredito WHERE idpropuesta = '" + this.idpropuesta + "'";

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
                    sql = "UPDATE ventascredito SET " +
                       "porc_compremio = '" + this.porc_compremio + "'," +
                       "porc_comprima = '" + this.porc_comprima + "'," +
                       "totalpropuesta = '" + this.totalpropuesta + "'," +
                       "fechacomision = '" + this.fechacomision + "'," +
                       "useredit = '" + this.useredit + "'," +
                       "ultmod = '" + this.ultmod + "'," +
                       "liquidado = '" + this.liquidado + "'" +
                       " WHERE idpropuesta = '" + this.idpropuesta + "'";
                }
                else
                {
                    sql = "INSERT INTO ventascredito (" + this.columns + ") VALUES(" +
                      "'" + this.idpropuesta + "'," +
                      "'" + this.user + "'," +
                      "'" + this.porc_comprima + "'," +
                      "'" + this.porc_compremio + "'," +
                      "'" + this.totalpropuesta + "'," +
                      "'" + this.fechacomision + "'," +
                      "'" + this.useredit + "'," +
                      "'" + this.ultmod + "'," +
                      "'" + this.liquidado + "'" +
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
