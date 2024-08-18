using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class Cola
    {
        string sql = "";

        string columns = "id,entity,entity_id,ultmod,user_edit";
        public string reg, id, entity, entity_id, ultmod, user_edit = "";
        conexion con = new conexion();

        public Cola(bool install_ = false)
        {
            if(install_)
                this.install(); 
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists colas (reg serial PRIMARY KEY, id INT(10), entity VARCHAR(100), entity_id INT(10),  " +
                " ultmod DATETIME, user_edit VARCHAR(100));";
            }
            else
            {
                sql = "CREATE TABLE if not exists colas (reg serial PRIMARY KEY, id INT(10), entity VARCHAR(100), entity_id INT(10),  " +
                " ultmod DATETIME, user_edit VARCHAR(100));";
            }

            con.query(sql);

        }

        public bool save()
        {
            try
            {
                sql = "INSERT INTO colas (" + this.columns + ") VALUES(" +
                        "'" + this.id + "'," +
                        "'" + this.entity + "'," +
                        "'" + this.entity_id + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'," +
                        "'" + MDIParent1.sesionUser + "'" +
                        ") ";

                con.query(sql);
                return true;
            }
            catch (Exception ex)
            {
                logs log = new logs();
                log.newError("COLA400",ex.Message);
                return false;
            }

        }

        public static void setLastCola(List<Cola> listado)
        {
            conexion con = new conexion();
            foreach (Cola cola_ in listado) {
                cola_.save();
            }
        }

        public static int getLastCola(string entity_)
        {
            conexion con = new conexion();
            int res = 0;
            string sql = "SELECT MAX(id) as max FROM colas WHERE entity = '"+ entity_ + "' ";
            DataSet ds = con.query(sql);
            if(ds.Tables[0].Rows.Count > 0)
            {
                if(ds.Tables[0].Rows[0]["max"] != null && ds.Tables[0].Rows[0]["max"].ToString() != "")
                    res = Convert.ToInt32(ds.Tables[0].Rows[0]["max"].ToString());
            }
            return res;
        }
    }
}
