using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class propuestaencurso
    {

        string sql = "";

        string columns = "usuario,ultmod,ip";
        public string id, usuario = "", ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ip = "";

        conexion con = new conexion();

        public propuestaencurso()
        {
            this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists propuestaencurso (id serial PRIMARY KEY, usuario VARCHAR(150), ultmod DATETIME NULL, ip VARCHAR(200) NULL);";
            }
            else
            {
                sql = "CREATE TABLE if not exists propuestaencurso (id INTEGER PRIMARY KEY, usuario VARCHAR(150), ultmod DATETIME NULL, ip VARCHAR(200) NULL);";
            }

            con.query(sql);
        }

        public DataSet get_encurso()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM propuestaencurso WHERE  usuario != '' ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestaencurso");
            }

            return ds;
        }

        public bool get(string user_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM propuestaencurso WHERE  usuario = '" + user_ + "' ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.usuario = ds.Tables[0].Rows[0]["usuario"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
                    this.ip = ds.Tables[0].Rows[0]["ip"].ToString();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar propuestaencurso");
            }

            return false;
        }

        public bool exist()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM propuestaencurso WHERE  usuario = '" + this.usuario + "' AND ip = '"+this.ip+"' ";
            try
            {
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
                Console.Write("ERROR al consultar propuestaencurso");
            }

            return false;
        }

        public  void delete()
        {

            sql = "DELETE FROM propuestaencurso WHERE usuario = '"+this.usuario+"' AND ip = '"+this.ip+"' ";

            con.query(sql);
        }

        public void delete_all()
        {

            sql = "DELETE FROM propuestaencurso WHERE usuario != '' ";

            con.query(sql);
        }



        public bool save()
        {
            if (!this.exist())
            {
                try
                {
                
                    sql = "INSERT INTO propuestaencurso (" + this.columns + ") VALUES(" +
                        "'" + this.usuario + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.ip + "'" +
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
