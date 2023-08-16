using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class rendiciones
    {
        string sql = "";

        string columns = " idarqueos, detalle, valor, nocomprobante, entregadopor,entregadoa, ultmod,user_edit,codestado";
        public string reg, idarqueos, detalle, valor, nocomprobante, entregadopor, entregadoa, ultmod, user_edit, codestado, codempresa = "";
        conexion con = new conexion();

        public rendiciones()
        {
            this.install();
        }


        private void install()
        {
            /*sql = "DROP TABLE rendiciones";
            con.query(sql);*/
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists rendiciones (reg serial PRIMARY KEY, idarqueos VARCHAR(150) NULL, detalle VARCHAR(200) NULL, valor DOUBLE NULL, " +
                " nocomprobante VARCHAR(50) NULL, entregadopor VARCHAR(100) NULL, entregadoa VARCHAR(100) NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists rendiciones (reg INTEGER PRIMARY KEY, idarqueos VARCHAR(150) NULL, detalle VARCHAR(200) NULL, valor DOUBLE NULL, " +
                " nocomprobante VARCHAR(50) NULL, entregadopor VARCHAR(100) NULL, entregadoa VARCHAR(100) NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }

            con.query(sql);

        }

        public bool exist()
        {
            sql = "SELECT reg FROM rendiciones WHERE reg = '" + this.reg + "'";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }


            return false;
        }


        public DataSet get_all_fecha_export(string fecha)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM rendiciones  WHERE ultmod >= '" + fecha + "' AND user_edit = '" + MDIParent1.sesionUser + "' AND ultmod <= '"
                + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ORDER BY reg ASC ";

            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar actividades");
            }

            return ds;
        }


        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM rendiciones WHERE codestado = 1  ORDER BY reg DESC ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar rendiciones");
            }

            return ds;
        }


        public DataSet get_all_busqueda(string fecha1, string fecha2)   
        {
            DataSet ds = new DataSet();

            sql = "SELECT t1.reg,t1.idarqueos, t1.detalle, (SELECT SUM(t4.valordia) FROM lineas_rendiciones t4 WHERE t4.idrendicion = t1.reg) as valor " +
                " , t1.nocomprobante, t1.entregadopor,t1.entregadoa, t1.ultmod,t1.user_edit,t1.codestado FROM " +
                "  rendiciones t1 INNER JOIN lineas_rendiciones t2 ON DATE( t2.fechaarqueo )  BETWEEN '" + fecha1 + "' AND '" + fecha2 
                + "' AND t1.codestado = 1 AND t2.idrendicion = t1.reg GROUP BY t1.reg ORDER BY t1.reg DESC ";
            
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

        public string get_max_fecha()
        {
            DataSet ds = new DataSet();

            sql = "SELECT MAX(fechadia) as maxfecha FROM rendiciones WHERE codestado = 1";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["maxfecha"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar rendiciones");
            }

            return "";
        }

        public string get_campo_fecha(string fecha1, string fecha2, string fecha3)
        {
            DataSet ds = new DataSet();

            sql = "SELECT reg FROM rendiciones WHERE  idarqueos LIKE '%"+fecha1+ "%' OR idarqueos LIKE '%" + fecha2 + "%' OR idarqueos LIKE '%" + fecha3 + "%'";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["reg"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar rendiciones");
            }

            return "";
        }


        public bool get(string reg_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM rendiciones WHERE codestado = 1 AND reg = '" + reg_ + "' ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.idarqueos = ds.Tables[0].Rows[0]["idarqueos"].ToString();
                    this.detalle = ds.Tables[0].Rows[0]["detalle"].ToString();
                    this.valor = ds.Tables[0].Rows[0]["valor"].ToString();
                    this.nocomprobante = ds.Tables[0].Rows[0]["nocomprobante"].ToString();
                    this.entregadopor = ds.Tables[0].Rows[0]["entregadopor"].ToString();
                    this.entregadoa = ds.Tables[0].Rows[0]["entregadoa"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
                    this.user_edit = ds.Tables[0].Rows[0]["user_edit"].ToString();
                    this.codestado = ds.Tables[0].Rows[0]["codestado"].ToString();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar rendiciones");
            }

            return false;
        }

        public string consecutivo()
        {
            string res = "";

            DataSet ds = con.query("SELECT MAX(reg) as maxred FROM rendiciones ");
            if(ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["maxred"].ToString();
            }
            return res;
        }

        public void delete_idarqueos()
        {
            sql = "DELETE FROM rendiciones WHERE idarqueos = '" + this.idarqueos + "'  ";
            con.query(sql);
        }


        public void save_concat(string sql1)
        {
            con.query("INSERT INTO rendiciones (" + this.columns + ") VALUES "+sql1);
        }

        public string concat_sql()
        {
            try
            {
                sql = "(" +
                    "'" + this.idarqueos + "'," +
                    "'" + this.detalle + "'," +
                    "'" + this.valor + "'," +
                    "'" + this.nocomprobante + "'," +
                    "'" + this.entregadopor + "'," +
                    "'" + this.entregadoa + "'," +
                    "'" + this.ultmod + "'," +
                    "'" + this.user_edit + "'," +
                    "'" + this.codestado + "'" +
                    ") ";

                return sql;

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public bool save()
        {
            try
            {
                if (this.exist())
                {
                    sql = "UPDATE rendiciones SET " +
                        "usuario = '" + this.idarqueos + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "user_edit = '" + this.user_edit + "'," +
                        "codestado = '" + this.codestado + "'," +
                        " WHERE reg = '" + this.reg + "'";
                }
                else
                {
                    sql = "INSERT INTO rendiciones (" + this.columns + ") VALUES(" +
                        "'" + this.idarqueos + "'," +
                        "'" + this.detalle + "'," +
                        "'" + this.valor+ "'," +
                        "'" + this.nocomprobante + "'," +
                        "'" + this.entregadopor + "'," +
                        "'" + this.entregadoa + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'" +
                        ") ";
                }

                Console.Write("\n\n---->"+sql+"\n\n");

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
