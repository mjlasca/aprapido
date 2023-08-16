using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class AuditoriaBarrios
    {
        string sql = "";
        string columns = "prefijo, idpropuesta, ultmod, user_edit, barrios_antes, barrios_despues ";
        public string prefijo, idpropuesta, ultmod, user_edit, barrios_antes, barrios_despues = "";

        conexion con = new conexion();

        public AuditoriaBarrios(bool inst = false)
        {
            if(inst)
                this.install();
        }


        private void install()
        {

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists auditoriabarrios (id serial PRIMARY KEY, prefijo VARCHAR(50) NULL, idpropuesta VARCHAR(150) NULL, " +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, barrios_antes LONGTEXT CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL, barrios_despues LONGTEXT CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL);";
            }
            else
            {
                sql = "CREATE TABLE if not exists auditoriabarrios (id INTEGER PRIMARY KEY, prefijo VARCHAR(50) NULL, idpropuesta VARCHAR(150) NULL, " +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, barrios_antes JSON NULL, barrios_despues JSON NULL);";
            }
            con.query(sql);
        }


        public bool save()
        {
            try
            {
             
                sql = "INSERT INTO auditoriabarrios (" + this.columns + ") VALUES(" +
                    "'" + this.prefijo + "'," +
                    "'" + this.idpropuesta + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + this.user_edit + "'," +
                    "'" + this.barrios_antes+ "'," +
                    "'" + this.barrios_despues + "'" +
                    ") ";

                con.query(sql);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public DataSet get_all()
        {
            string concat_f = "";
            if (this.prefijo  != null && this.prefijo != "")
                concat_f += $" AND prefijo = '{this.prefijo}'";
            if (this.idpropuesta != null &&  this.idpropuesta != "")
                concat_f += $" AND idpropuesta = '{this.idpropuesta}'";
            if(this.user_edit != null &&  this.user_edit != "")
                concat_f += $" AND  user_edit = ( SELECT t1.loggin FROM usuarios t1 WHERE t1.nombre LIKE '{this.user_edit}' LIMIT 1 ) ";

            DataSet ds = new DataSet();

            sql = $"SELECT * FROM auditoriabarrios WHERE id > 0 {concat_f} ORDER BY ultmod ASC";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar AuditoriaBarrios");
            }

            return ds;
        }

        
    }
}
