using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto.cobranzas
{
    class logaseguradora
    {
        string sql = "";
        string columns = "idaseguradora,nombre,mensaje,email, ultmod,user_edit";
        public string reg, idaseguradora, nombre, mensaje, email, ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), user_edit = MDIParent1.sesionUser;
        conexion con = new conexion();

        public logaseguradora()
        {
            this.install();
        }


        private void install()
        {
            //sql = "DROP TABLE logaseguradora";
            //con.query(sql);



            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists logaseguradora (reg serial PRIMARY KEY,idaseguradora VARCHAR(150) NULL,nombre VARCHAR(150) NULL, mensaje VARCHAR(5000) NULL," +
                    " email VARCHAR(500) NULL, ultmod DATETIME NULL, user_edit VARCHAR(150) NULL); ";
                con.query(sql);
            }
            else
            {
                sql = "CREATE TABLE if not exists logaseguradora (reg INTEGER PRIMARY KEY AUTOINCREMENT,idaseguradora VARCHAR(150) NULL,nombre VARCHAR(150) NULL, mensaje VARCHAR(5000) NULL," +
                    " email VARCHAR(500) NULL, ultmod DATETIME NULL, user_edit VARCHAR(150) NULL); ";
                con.query(sql);
            }


            //this.addColumn();
        }

        public DataSet get_aseguradora(string idaseguradora_ ,string nomb_ = "")
        {
            DataSet ds = new DataSet();
            string sqlAdd = "";
            if(nomb_ != "")
            {
                sqlAdd += $" AND nombre LIKE '%{nomb_}%'";
            }

            sql = "SELECT * FROM logaseguradora WHERE idaseguradora = '" + idaseguradora_ + 
                "' "+sqlAdd+" ORDER BY ultmod DESC LIMIT 500 ";

            Console.WriteLine("\nSQL "+sql);

            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar logaseguradora " + ex.Message);
            }

            return ds;
        }


        


        public DataSet get_all(string idaseguradora_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT *, SUM(monto) as total_monto FROM logaseguradora WHERE idaseguradora = '" + idaseguradora_ +
                "' GROUP BY nombre,poliza,moneda  ORDER BY nombre ASC";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar logaseguradora");
            }

            return ds;
        }

        public bool exist()
        {
            sql = "SELECT reg FROM logaseguradora WHERE idaseguradora = '" + this.idaseguradora + "'";
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

                sql = "INSERT INTO logaseguradora (" + this.columns + ") VALUES(" +
                    "'" + this.idaseguradora + "'," +
                    "'" + this.nombre + "'," +
                    "'" + this.mensaje.Replace("'", "\"") + "'," +
                    "'" + this.email + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + MDIParent1.sesionUser + "'" +
                    ") ";

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public void delete(string aseguradora_)
        {
            sql = "DELETE FROM logaseguradora WHERE idaseguradora = '" + aseguradora_ + "' ";
            con.query(sql);
        }
    }
}
