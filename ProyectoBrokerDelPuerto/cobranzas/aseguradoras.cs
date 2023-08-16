using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto.cobranzas
{
    class aseguradoras
    {
        string sql = "";
        string columns = "id,tipo_id,razonsocial,titular,descripcion,direccion,telefono,email,ultmod,user_edit,codestado";
        public string reg, id, tipo_id, razonsocial, titular, descripcion, direccion, telefono, email, ultmod, user_edit, codestado = "";
        conexion con = new conexion();

        public aseguradoras()
        {
            this.install();
        }


        private void install()
        {
            sql = "DROP TABLE aseguradoras";
            //con.query(sql);


            
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists aseguradoras (reg serial PRIMARY KEY,tipo_id VARCHAR(20) NULL, id VARCHAR(100) NOT NULL, razonsocial VARCHAR(350)  NULL, " +
                " titular VARCHAR(300) NULL, descripcion  VARCHAR(2000), direccion VARCHAR(300), telefono VARCHAR(100), email VARCHAR(150), " +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
                con.query(sql);
            }
            else
            {
                sql = "CREATE TABLE if not exists aseguradoras (reg INTEGER PRIMARY KEY AUTOINCREMENT,tipo_id VARCHAR(20) NULL, id VARCHAR(100) NOT NULL, razonsocial VARCHAR(350)  NULL, " +
                " titular VARCHAR(300) NULL, descripcion  VARCHAR(2000), direccion VARCHAR(300), telefono VARCHAR(100), email VARCHAR(150), " +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
                con.query(sql);
            }
            

            this.addColumn();
        }

        private void addColumn()
        {
            /*if (MDIParent1.baseDatos == "MySql")
            {
                //Add column categoria if NOT exist
                if (con.query("Select CHARACTER_MAXIMUM_LENGTH as longitud from information_schema.columns WHERE TABLE_NAME='aseguradoras' AND COLUMN_NAME='nombre' ").Tables[0].Rows[0]["longitud"].ToString() == "150")
                {
                    con.query("ALTER TABLE aseguradoras MODIFY nombre VARCHAR(1500) NULL;");
                }
            }
            else
            {
                con.query("ALTER TABLE aseguradoras MODIFY nombre VARCHAR(1500) NULL;");
            }*/

        }

        public DataSet get()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM aseguradoras WHERE id = '" + this.id + "' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar aseguradoras");
            }

            return ds;
        }


        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM aseguradoras WHERE codestado = 1";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar aseguradoras");
            }

            return ds;
        }

        public bool exist()
        {
            sql = "SELECT id,reg FROM aseguradoras WHERE id = '" + this.id + "'";
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
                    sql = "UPDATE aseguradoras SET " +
                        "tipo_id = '" + this.tipo_id.Trim() + "'," +
                        "razonsocial = '" + this.razonsocial.Trim() + "'," +
                        "titular = '" + this.titular.Trim() + "'," +
                        "descripcion = '" + this.descripcion.Trim() + "'," +
                        "direccion = '" + this.direccion.Trim() + "'," +
                        "telefono = '" + this.telefono.Trim() + "'," +
                        "email = '" + this.email.Trim() + "'," +
                        "ultmod = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "codestado = '1' " +
                        " WHERE id = '" + this.id.Trim() + "'";
                }
                else
                {
                    sql = "INSERT INTO aseguradoras (" + this.columns + ") VALUES(" +
                        "'" + this.id.Trim() + "'," +
                        "'" + this.tipo_id.Trim() + "'," +
                        "'" + this.razonsocial.Trim() + "'," +
                        "'" + this.titular.Trim() + "'," +
                        "'" + this.descripcion.Trim() + "'," +
                        "'" + this.direccion.Trim() + "'," +
                        "'" + this.telefono.Trim() + "'," +
                        "'" + this.email.Trim() + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + MDIParent1.sesionUser.Trim() + "'," +
                        " '1' " +
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
            sql = "DELETE FROM aseguradoras WHERE id = '" + this.id + "' ";
            con.query(sql);
        }

    }
}
