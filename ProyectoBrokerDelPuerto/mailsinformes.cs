using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class mailsinformes
    {
        string sql = "";
        string columns = "fecha,useredit,ultmod";
        public string id, fecha, useredit, ultmod = "";
        conexion con = new conexion();

        public mailsinformes()
        {
            this.install();
        }

        private void install()
        {
            /*sql = "DROP TABLE mailsinformes;";

            con.query(sql);*/

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists mailsinformes ( id serial PRIMARY KEY, fecha DATE NULL, " +
                " useredit VARCHAR(150) NULL, ultmod datetime NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists mailsinformes ( id INTEGER PRIMARY KEY, fecha DATE NULL, " +
                " useredit VARCHAR(150) NULL, ultmod datetime NULL );";
            }
            con.query(sql);


        }




        public bool exist()
        {
            sql = "SELECT fecha FROM mailsinformes WHERE fecha = '" + this.fecha + "'";
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
                if (!this.exist())
                {

                    sql = "INSERT INTO mailsinformes (" + this.columns + ") VALUES(" +
                        "'" + this.fecha + "'," +
                        "'" + this.useredit + "'," +
                        "'" + this.ultmod + "'" +
                        ") ;";

                    con.query(sql);
                }


                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool get_fecha()
        {
            bool res = false;

            sql = "SELECT * FROM mailsinformes WHERE fecha = '" + this.fecha + "' ";
            try
            {
                DataSet ds = new DataSet();
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar mailsinformes " + ex.Message);
                res = false;
            }

            return res;
        }



        public DataSet get_all()
        {
            DataSet ds = new DataSet();
            sql = "SELECT * FROM mailsinformes ORDER BY  fecha ASC ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar mailsinformes " + ex.Message);
            }

            return ds;
        }



        public void delete()
        {
            sql = "DELETE FROM mailsinformes WHERE fecha = '" + this.fecha + "'";
            con.query(sql);
        }

    }

}
