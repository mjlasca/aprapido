using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class Configmaster
    {

        string sql = "";

        string columns = "tipo, nomcodigo, ultmod,user_edit,nombre,codestado,coempresa";
        public string id, tipo, nomcodigo, ultmod, user_edit, nombre, codestado,codempresa ;
        conexion con = new conexion();

        public Configmaster(bool ins = false)
        {
            if(ins)
                this.install();
        }


        private void install()
        {
            /*sql = "DROP TABLE Configmaster";
            con.query(sql);*/
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists configmaster (id serial PRIMARY KEY, tipo VARCHAR(150) NULL,nomcodigo VARCHAR(150) NULL, ultmod DATETIME NULL, " +
                " user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL,nombre VARCHAR(150) NULL,codempresa VARCHAR(150) NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists configmaster (id INTEGER PRIMARY KEY, tipo VARCHAR(150) NULL,nomcodigo VARCHAR(150) NULL, ultmod DATETIME NULL, " +
                " user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL,nombre VARCHAR(150) NULL,codempresa VARCHAR(150) NULL  );";
            }
                
            
            con.query(sql);

            sql = "SELECT * FROM configmaster WHERE tipo = 'MASTER' ";
            DataSet ds = con.query(sql);
            if(ds.Tables[0].Rows.Count < 1)
            {
                
                sql = "INSERT INTO configmaster(tipo) VALUES("+
                    " 'MASTER') ;";
                sql += "INSERT INTO configmaster(tipo) VALUES(" +
                    " 'ASEGURADORA') ;";
                sql += "INSERT INTO configmaster(tipo) VALUES(" +
                    " 'ORGANIZADOR') ;";
                sql += "INSERT INTO configmaster(tipo) VALUES(" +
                    " 'PRODUCTOR') ;";

                con.query(sql);
            }

            sql = "SELECT * FROM configmaster WHERE tipo = 'ASEGURADORA' ";
            ds = con.query(sql);
            if (ds.Tables[0].Rows.Count < 1)
            {

                sql = "INSERT INTO configmaster(tipo) VALUES(" +
                    " 'ASEGURADORA') ;";
                con.query(sql);
            }


            this.addColumn();
        }

        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                if (con.query("SHOW COLUMNS FROM configmaster WHERE Field = 'nombre' ").Tables[0].Rows.Count == 0)
                    con.query("ALTER TABLE configmaster ADD COLUMN nombre VARCHAR(150) NULL;");
                if (con.query("SHOW COLUMNS FROM configmaster WHERE Field = 'codempresa' ").Tables[0].Rows.Count == 0)
                    con.query("ALTER TABLE configmaster ADD COLUMN codempresa VARCHAR(150) NULL;");

            }
            else
            {
                con.query("ALTER TABLE configmaster ADD COLUMN nomcodigo VARCHAR(150) NULL;");
                con.query("ALTER TABLE configmaster ADD COLUMN nombre VARCHAR(150) NULL;");
                con.query("ALTER TABLE configmaster ADD COLUMN codempresa VARCHAR(150) NULL;");
            }

        }

        public bool exist()
        {
            sql = "SELECT id FROM configmaster WHERE id = '" + this.id + "'";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }


            return false;
        }


        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM configmaster WHERE codestado = 1";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar configmaster");
            }

            return ds;
        }

        //if get ready does'nt show, then configure
        public bool get_ready()
        {
            bool res = true;

            sql = "SELECT * FROM configmaster WHERE ( tipo = 'MASTER' AND nomcodigo != '') OR ( tipo = 'ORGANIZADOR' AND nomcodigo != '') OR ( tipo = 'PRODUCTOR' AND nomcodigo != '')  ";
            try
            {
                if(con.query(sql).Tables[0].Rows.Count > 0)
                    res = false;
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar configmaster");
                res = true;
            }

            return res;
        }




        public DataSet get(string _tipo)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM configmaster WHERE tipo = '" + _tipo + "' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar configmaster");
            }

            return ds;
        }


        public bool user_allow()
        {
            bool res = false;

            sql = "SELECT * FROM configmaster WHERE user_edit = '" + this.user_edit + "' ";
            try
            {
                DataSet ds = con.query(sql);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    res = true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar configmaster");
            }

            return res;
        }


        public string get_codempresa()
        {
            string res = "";

            sql = "SELECT codempresa FROM configmaster WHERE codestado = 1 ";
            try
            {
                DataSet ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    res = ds.Tables[0].Rows[0]["codempresa"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar el código de empresa");
            }

            return res;
        }



        public bool updateCodempresa()
        {
            try
            {
                sql = "UPDATE configmaster SET " +
                    "codempresa = '" + this.codempresa + "'" +
                    " WHERE tipo != '' ;";

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar la configuración");
                return false;
            }

        }



        public bool save(string _tipo)
        {
            try
            {
                sql = "UPDATE configmaster SET " +
                    " nomcodigo = '" + this.nomcodigo + "'," +
                    "ultmod = '" + this.ultmod + "'," +
                    "user_edit = '" + this.user_edit + "'," +
                    "nombre = '" + this.nombre + "'," +
                    "codempresa = '" + this.codempresa + "'," +
                    " codestado = '" + this.codestado + "'" +
                    " WHERE tipo = '"+_tipo+"' ;";

                con.query(sql);

                if (this.codempresa != "")
                    MDIParent1.codempresa = this.codempresa;

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar la configuración");
                return false;
            }

        }




    }
}
