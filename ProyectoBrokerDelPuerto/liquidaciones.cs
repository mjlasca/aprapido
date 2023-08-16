using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class liquidaciones
    {
        string sql = "";

        string columns = "usuario,user_liquidador,fecha_inicia,fecha_fin,prima,premio,com_prima,com_premio,ultmod,useredit";
        public string id, usuario = "", user_liquidador = "", fecha_inicia = "", fecha_fin = "", prima = "0", premio = "0", 
             com_prima = "0", com_premio = "0", ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),useredit = "";

        conexion con = new conexion();

        public liquidaciones()
        {
            this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists liquidaciones (id serial PRIMARY KEY, usuario VARCHAR(150), user_liquidador VARCHAR(150) NULL, fecha_inicia DATE NULL, " +
                " fecha_fin DATE NULL, prima double NULL, premio double NULL, com_prima  double NULL,com_premio  double NULL,ultmod DATE NULL, useredit VARCHAR(150) NULL);";
            }
            else
            {
                sql = "CREATE TABLE if not exists liquidaciones (id INTEGER PRIMARY KEY, usuario VARCHAR(150), user_liquidador VARCHAR(150) NULL, fecha_inicia DATE NULL, " +
                " fecha_fin DATE NULL, prima double NULL, premio double NULL, com_prima  double NULL,com_premio  double NULL,ultmod DATE NULL, useredit VARCHAR(150) NULL);";
            }

            con.query(sql);
        }

        public string consecutivo()
        {
            DataSet ds = new DataSet();

            sql = "SELECT COUNT(id) as maximo FROM liquidaciones  ";
            
            ds = con.query(sql);
            if(ds.Tables[0].Rows.Count > 0)
            {
                return (1+Convert.ToInt16(ds.Tables[0].Rows[0]["maximo"].ToString())).ToString();
            }
           

            return "1";
        }

        public bool get(string idpropuesta_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM liquidaciones WHERE  fechaarqueo = '" + idpropuesta_ + "' ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.usuario = ds.Tables[0].Rows[0]["usuario"].ToString();
                    this.user_liquidador = ds.Tables[0].Rows[0]["user_liquidador"].ToString();
                    this.fecha_inicia = ds.Tables[0].Rows[0]["fecha_inicia"].ToString();
                    this.fecha_fin = ds.Tables[0].Rows[0]["fecha_fin"].ToString();
                    this.prima = ds.Tables[0].Rows[0]["prima"].ToString();
                    this.premio = ds.Tables[0].Rows[0]["premio"].ToString();
                    this.com_prima = ds.Tables[0].Rows[0]["com_prima"].ToString();
                    this.com_premio = ds.Tables[0].Rows[0]["com_premio"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
                    this.useredit = ds.Tables[0].Rows[0]["useredit"].ToString();

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar rendiciones");
            }

            return false;
        }


        public DataSet getDatosPago(DateTime fec1, DateTime fec2)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM comisiones WHERE  usuario LIKE '%%' AND  DATE(ultmod) BETWEEN '" + fec1.ToString("yyyy-MM-dd") + "' AND '" + fec2.ToString("yyyy-MM-dd") + "' ";


            try
            {
                ds = con.query(sql);
                Console.WriteLine("--> " + sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar las liquidaciones ");
            }

            return ds;
        }

        public DataSet get_all(string user_, DateTime fec1, DateTime fec2)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM liquidaciones WHERE  usuario LIKE '%" + user_ + "%' AND  DATE(ultmod) BETWEEN '" + fec1.ToString("yyyy-MM-dd")+ "' AND '"+ fec2.ToString("yyyy-MM-dd")+"' ";

            
            try
            {
                ds = con.query(sql);
                Console.WriteLine("--> "+sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar las liquidaciones ");
            }

            return ds;
        }




        public bool save()
        {
            try
            {
                sql = "INSERT INTO liquidaciones (" + this.columns + ") VALUES(" +
                       "'" + this.usuario + "'," +
                       "'" + this.user_liquidador + "'," +
                       "'" + this.fecha_inicia + "'," +
                       "'" + this.fecha_fin + "'," +
                       "'" + this.prima+ "'," +
                       "'" + this.premio + "'," +
                       "'" + this.com_prima + "'," +
                       "'" + this.com_premio + "'," +
                       "'" + this.ultmod + "'," +
                       "'" + this.useredit + "'" +
                       ") ";
                
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
