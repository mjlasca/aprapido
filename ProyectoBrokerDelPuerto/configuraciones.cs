using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class configuraciones
    {
        string sql = "";

        string columns = "dato,valor";
        public string id, dato ="prosimport", valor = "";
        conexion con = new conexion();

        public configuraciones()
        {
            this.install();
        }


        private void install()
        {
            /*sql = "DROP TABLE informes";
            con.query(sql);*/

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists configuraciones (id serial PRIMARY KEY, dato VARCHAR(150) NULL, valor VARCHAR(50)  NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists configuraciones (id INTEGER PRIMARY KEY, dato VARCHAR(150) NULL, valor VARCHAR(50)  NULL );";
            }

            con.query(sql);


            

        }

        public string get_prosimport()
        {
            sql = "SELECT * FROM configuraciones WHERE dato = 'prosimport' ";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                if(con.query(sql).Tables[0].Rows[0]["valor"].ToString() == "1")
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }

            return "0";
        }
        

        private bool exist()
        {
            sql = "SELECT * FROM configuraciones WHERE dato = '"+this.dato+"' ";
            if(con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }

            return false;
        }


        public bool save()
        {
            try
            {
                if (this.exist())
                {
                    sql = "UPDATE configuraciones SET valor = '" + this.valor + "' WHERE dato = '" + this.dato + "' ";
                }
                else
                {
                    sql = "INSERT INTO configuraciones (" + this.columns + ") VALUES(" +
                    "'" + this.dato + "'," +
                    "'" + this.valor + "'" +
                    ") ";
                }

                Console.WriteLine("\n\nSQL PROSIMPORT "+sql);

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
