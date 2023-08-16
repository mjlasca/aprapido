using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class migraciones
    {
        string sql = "";

        string columns = "fecha,tabla,cantidad_registros,tipo,numeracion,useredit,ultmod";
        public string id, fecha, tabla, cantidad_registros = "0",tipo, numeracion, useredit = MDIParent1.sesionUser;
        public string  ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        conexion con = new conexion();

        public migraciones()
        {
            this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists migraciones (id serial PRIMARY KEY, fecha DATETIME NULL, tabla VARCHAR(150) NULL," +
                " tipo VARCHAR(150) NULL, cantidad_registros VARCHAR(150) NULL,numeracion VARCHAR(50000) NULL,useredit VARCHAR(150) NULL, ultmod DATETIME NULL);";
            }
            else
            {
                sql = "CREATE TABLE if not exists migraciones (id INTEGER PRIMARY KEY, fecha DATETIME NULL, tabla VARCHAR(150) NULL," +
                " tipo VARCHAR(150) NULL, cantidad_registros VARCHAR(150) NULL,numeracion VARCHAR(50000) NULL,useredit VARCHAR(150) NULL, ultmod DATETIME NULL);";
            }

            con.query(sql);
        }


        public DateTime get_ultimafecha(bool reset = false)
        {
            if(reset)
                return Convert.ToDateTime("2019-01-01");
            DataSet ds = new DataSet();

            sql = "SELECT MAX(fecha) as fecha FROM migraciones WHERE  tabla = '" + this.tabla + "' AND tipo = '"+this.tipo+"'  ORDER BY fecha DESC";
            ds = con.query(sql);
            if (ds.Tables[0].Rows[0]["fecha"].ToString() != string.Empty )
            {
                return Convert.ToDateTime(ds.Tables[0].Rows[0]["fecha"].ToString());
            }
            else
            {
                if (this.tabla == "propuestas")
                    return DateTime.Now.AddDays(-1);
                else
                {
                    if(this.tabla == "arqueos")
                        return DateTime.Now.AddDays(-30);
                    if (this.tabla == "rendiciones")
                        return DateTime.Now.AddDays(-30);
                    if (this.tabla == "lineasrendiciones")
                        return DateTime.Now.AddDays(-30);
                    return Convert.ToDateTime("2019-01-01");
                }
                    
            }
        }



        public bool exist()
        {
            sql = "SELECT id FROM migraciones WHERE fecha = '" + this.fecha +
                "' AND tabla = '"+this.tabla+"' AND tipo = '"+this.tipo+"' ";

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
                if (!exist())
                {
                    sql = "INSERT INTO migraciones (" + this.columns + ") VALUES(" +
                      "'" + this.fecha + "'," +
                      "'" + this.tabla + "'," +
                      "'" + this.cantidad_registros+ "'," +
                      "'" + this.tipo + "'," +
                      "'" + this.numeracion + "'," +
                      "'" + this.useredit+ "'," +
                      "'" + this.ultmod + "'" +
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
    }
}
