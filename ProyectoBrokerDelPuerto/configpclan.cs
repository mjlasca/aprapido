using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class configpclan
    {
        string sql = "";
        string columns = "ip,basededatos,usuariobase,passusuario,ultmod";
        public string id, ip, basededatos, usuariobase, passusuario, ultmod = "";
        

        private void install()
        {
            /*sql = "DROP TABLE configpclan;";

            con.query(sql);*/
            conexion con = new conexion();

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists configpclan ( id serial PRIMARY KEY, ip VARCHAR NULL, " +
                " basededatos VARCHAR(150) NULL, usuariobase VARCHAR(150) NULL, passusuario VARCHAR(150) NULL, ultmod datetime NULL );";
                //con.query(sql);
            }

        }




        public bool exist()
        {
            conexion con = new conexion();
            sql = "SELECT ip FROM configpclan WHERE id != '" + this.id + "'";
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
                if (this.exist())
                {
                    sql = "UPDATE configpclan SET " +
                        "usuario = '" + this.ip + "'," +
                        "basededatos = '" + this.basededatos + "'," +
                        "passusuario = '" + this.passusuario + "'," +
                        "usuariobase = '" + this.usuariobase + "'," +
                        " WHERE id = '" + this.id + "'";
                }
                else
                {

                    sql = "INSERT INTO configpclan (" + this.columns + ") VALUES(" +
                        "'" + this.ip + "'," +
                        "'" + this.basededatos + "'," +
                        "'" + this.usuariobase + "'," +
                        "'" + this.passusuario + "'," +
                        "'" + this.ultmod + "'" +
                        ") ;";
                    
                }
                conexion con = new conexion();
                con.query(sql);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public string get_ip()
        {
            string res = "";

            DataSet ds = new DataSet();
            sql = "SELECT ip FROM configpclan where id != '' ";
            try
            {
                conexion con = new conexion();
                ds = con.query(sql);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    res = ds.Tables[0].Rows[0]["ip"].ToString();
                    return res;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar configpclan " + ex.Message);
            }

            return res;
        }



       

    }
}
