using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class lineas_rendiciones
    {
        string sql = "";

        string columns = "idrendicion,fechaarqueo, valordia,ultmod";
        public string id,idrendicion, fechaarqueo, valordia,ultmod,codempresa;
        conexion con = new conexion();

        public lineas_rendiciones()
        {
            this.install();
        }



        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists lineas_rendiciones (id serial PRIMARY KEY, idrendicion VARCHAR(100), fechaarqueo DATE NULL, valordia double NULL, " +
                " ultmod DATETIME NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists lineas_rendiciones (id INTEGER PRIMARY KEY, idrendicion VARCHAR(100), fechaarqueo DATE NULL, valordia double NULL, " +
                " ultmod DATETIME NULL );";
            }

            con.query(sql);
        }

        public bool get_fecha(string fecha_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM lineas_rendiciones WHERE  fechaarqueo = '" + fecha_ + "' ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.idrendicion = ds.Tables[0].Rows[0]["idrendicion"].ToString();
                    this.fechaarqueo = ds.Tables[0].Rows[0]["fechaarqueo"].ToString();
                    this.valordia = ds.Tables[0].Rows[0]["valordia"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
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

        public void save_concat(string sql1)
        {
            con.query("INSERT INTO lineas_rendiciones (" + this.columns + ") VALUES "+sql1);
        }
        public string concat_sql()
        {
            try
            {
                sql = "(" +
                       "'" + this.idrendicion + "'," +
                       "'" + this.fechaarqueo + "'," +
                       "'" + this.valordia + "'," +
                       "'" + this.ultmod + "'" +
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
                sql = "INSERT INTO lineas_rendiciones (" + this.columns + ") VALUES(" +
                       "'" + this.idrendicion + "'," +
                       "'" + this.fechaarqueo + "'," +
                       "'" + this.valordia+ "'," +
                       "'" + this.ultmod + "'" +
                       ") ";

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public void delete()
        {
            sql = "DELETE FROM lineas_rendiciones WHERE id = '"+this.id+"' ";
            con.query(sql);
        }

        public DataSet get_all_fecha_export(string fecha)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM lineas_rendiciones  WHERE ultmod >= '" + fecha + "' AND ultmod <= '"
                + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ORDER BY id ASC ";

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



    }
}
