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

        string columns = "dato,valor,detail";
        public string id, dato ="prosimport", valor = "", detail= "";
        conexion con = new conexion();

        public configuraciones(bool inst = false)
        {
            if(inst)
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

            this.addColumn();
        }

        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                try
                {

                    //Add column categoria if NOT exist
                    if (con.query("SHOW COLUMNS FROM configuraciones WHERE Field = 'detail' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE configuraciones ADD COLUMN detail VARCHAR(300) NULL");
                }
                catch
                {
                    //
                }
            }
            else
            {
                con.query("ALTER TABLE configuraciones ADD COLUMN detail VARCHAR(300) NULL");
            }


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

        public configuraciones get(string field)
        {
            sql = "SELECT * FROM configuraciones WHERE dato = '"+field+"' ";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                this.valor = con.query(sql).Tables[0].Rows[0]["valor"].ToString();
                this.id = con.query(sql).Tables[0].Rows[0]["id"].ToString();
                this.detail = con.query(sql).Tables[0].Rows[0]["detail"].ToString();
            }

            return this;
        }


        private bool exist()
        {
            sql = "SELECT * FROM configuraciones WHERE dato = '"+this.dato+"' ";
            if(con.query(sql).Tables.Count > 0 && con.query(sql).Tables[0].Rows.Count > 0)
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
                    sql = "UPDATE configuraciones SET valor = '" + this.valor + "',detail = '" + this.detail + "'  WHERE dato = '" + this.dato + "' ";
                }
                else
                {
                    sql = "INSERT INTO configuraciones (" + this.columns + ") VALUES(" +
                    "'" + this.dato + "'," +
                    "'" + this.valor + "'," +
                    "'" + this.detail + "'" +
                    ") ";
                }

                Console.WriteLine("\n\nSQL PROSIMPORT "+sql);

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR configuraciones "+ex.Message);
                return false;
            }

        }
    }
}
