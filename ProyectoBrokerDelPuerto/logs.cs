using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ProyectoBrokerDelPuerto
{
    class logs
    {
        string sql = "";

        string columns = "coderror,mensaje, ultmod,user_edit,codempresa";
        public string id, coderror = "", mensaje = "", ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), user_edit = MDIParent1.sesionUser, codempresa = MDIParent1.codempresa;
        conexion con = new conexion();

        public logs()
        {
            this.install();
        }


        private void install()
        {
            /*sql = "DROP TABLE informes";
            con.query(sql);*/

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists logs (id serial PRIMARY KEY, coderror VARCHAR(50) NULL, mensaje VARCHAR(5000)  NULL, " +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codempresa VARCHAR(150) NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists logs (id INTEGER PRIMARY KEY, coderror VARCHAR(50) NULL, mensaje VARCHAR(5000)  NULL, " +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codempresa VARCHAR(150) NULL );";
            }

            con.query(sql);

            
        }

        public void newError(string coderror_, string mensaje_)
        {
            this.coderror = coderror_;
            this.mensaje = mensaje_;
            this.save();
        }


        public bool save_1()
        {
            try
            {
                this.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sql = $"INSERT INTO logs (" + this.columns + $") VALUES('${this.coderror}','${this.mensaje}','${this.ultmod}','${MDIParent1.sesionUser}','${MDIParent1.codempresa}') ";

                con.query(sql);
                
                return true;

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("No se ha podido generar el log "+ex);
                return false;
            }

        }



        public bool save()
        {
            try
            {
                
                    sql = "INSERT INTO logs (" + this.columns + ") VALUES(@coderror,@mensaje,@ultmod,@user_edit,@codempresa) ";

                this.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (MDIParent1.baseDatos == "MySql")
                {
                    con.conecction.Execute(sql,
                    new
                    {
                        this.coderror,
                        this.mensaje,
                        this.ultmod,
                        this.user_edit,
                        this.codempresa,
                    }
                    );
                }
                else
                {
                    con.connectionSQLite.Execute(sql,
                    new
                    {
                        this.coderror,
                        this.mensaje,
                        this.ultmod,
                        this.user_edit,
                        this.codempresa,
                    }
                    );
                }
                return true;

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("No se ha podido generar el log "+ex);
                return false;
            }

        }
    }
}
