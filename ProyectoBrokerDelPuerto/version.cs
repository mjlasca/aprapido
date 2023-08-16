using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class version
    {
            string sql = "";

            string columns = "detalle,version, ultmod";
            public string id, detalle,  ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            public double version_ = 0;
            conexion con = new conexion();

            public version()
            {
                this.install();
            }


            private void install()
            {
                /*sql = "DROP TABLE informes";
                con.query(sql);*/

                if (MDIParent1.baseDatos == "MySql")
                {
                    sql = "CREATE TABLE if not exists version (id serial PRIMARY KEY, detalle VARCHAR(300) NULL, version double  DEFAULT 0, " +
                    " ultmod DATETIME NULL ) ;";
                }
                else
                {
                    sql = "CREATE TABLE if not exists version (id INTEGER PRIMARY KEY, detalle VARCHAR(300) NULL, version double  DEFAULT 0, " +
                    " ultmod DATETIME NULL ) ;";
                }

                con.query(sql);

                this.newversion();

            }

            public void newversion()
            {
                this.detalle = "Desactiva import automaticas, exportar propuesta a la nube... y se ha colocado GRUOP BY en la función get_idpropuesta de lineas propuestas";
                this.version_ = 3.10;
                this.save();
            }

            
            public bool exist()
            {
                sql = "SELECT * FROM version WHERE version = '" + this.version_ + "' ";
                DataSet ds = con.query(sql);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }

            public double getversion()
            {
                sql = "SELECT MAX(version) as ver FROM version ";
                DataSet ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToDouble(ds.Tables[0].Rows[0]["ver"]);
                }
                return 0;
            }



        public bool save()
            {
                if(this.exist() == false)
                {
                    try
                    {
                        sql = "INSERT INTO version (" + this.columns + ") VALUES(" +
                        "'" + this.detalle + "'," +
                        "'" + this.version_ + "'," +
                        "'" + this.ultmod + "' " +
                        ") ";

                        con.query(sql);
                        return true;

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                return false;

            }
        
    }
}
