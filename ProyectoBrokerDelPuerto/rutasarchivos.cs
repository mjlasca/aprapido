using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class rutasarchivos
    {
        string sql = "";
        string columns = "rutacertificados,rutarecibos,rutainformes,ipestacion,ultmod,user_edit,codestado";
        public string reg, rutacertificados, rutarecibos, rutainformes, ipestacion, ultmod, user_edit, codestado = "";
        conexion con = new conexion();

        public rutasarchivos()
        {
            this.install();
        }


        private void install()
        {
            //sql = "DROP table rutasarchivos";
            //con.query(sql);
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists rutasarchivos (reg serial PRIMARY KEY, rutacertificados VARCHAR(200) NULL, rutarecibos VARCHAR(200) NULL, rutainformes VARCHAR(200) NULL, ipestacion VARCHAR(200) NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists rutasarchivos (reg INTEGER PRIMARY KEY, rutacertificados VARCHAR(200) NULL, rutarecibos VARCHAR(200) NULL, rutainformes VARCHAR(200) NULL, ipestacion VARCHAR(200) NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }
            con.query(sql);

        }

        public bool exist()
        {
            sql = "SELECT ipestacion FROM rutasarchivos WHERE ipestacion = '" + this.ipestacion + "'";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }


            return false;
        }

     

        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM rutasarchivos WHERE codestado = 1";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar rutasarchivos");
            }

            return ds;
        }

        public string GetLocalIPAddress()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            // Get the IP  
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

            return myIP;
        }

        public DataSet get(string ipestacion_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM rutasarchivos WHERE codestado = 1 AND ipestacion = '"+ipestacion_+"' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar rutasarchivos");
            }

            return ds;
        }


        public void borrarRegistros()
        {
            sql = "DELETE FROM rutasarchivos";
            con.query(sql);
        }



        public bool save()
        {
            try
            {
                if (this.exist())
                {
                    sql = "UPDATE rutasarchivos SET " +
                        "rutacertificados = '" + this.rutacertificados + "'," +
                        "rutarecibos = '" + this.rutarecibos + "'," +
                        "rutainformes = '" + this.rutainformes + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "user_edit = '" + this.user_edit + "'," +
                        "codestado = '1'" +
                        " WHERE ipestacion = '" + this.ipestacion + "'";
                }
                else
                {
                    sql = "INSERT INTO rutasarchivos (" + this.columns + ") VALUES(" +
                        "'" + this.rutacertificados + "'," +
                        "'" + this.rutarecibos + "'," +
                        "'" + this.rutainformes + "'," +
                        "'" + this.ipestacion + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'" +
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

        public void delete()
        {
            sql = "UPDATE rutasarchivos SET codestado = 0 WHERE reg = '" + this.reg + "' ";
            con.query(sql);
        }
    }
}
