using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto.cobranzas
{
    class clientes_cobranzas : clientes
    {
        string sql1 = "";

        public string get_mail_cliente(string dato, string tipo_dato)
        {
            
            conexion con1 = new conexion();

            if(tipo_dato == "Nombre")
            {
                if (MDIParent1.baseDatos == "MySql")
                {
                    sql1 = "SELECT email FROM clientes WHERE  " +
                        "UPPER(TRIM(CONCAT(nombres,' ',apellidos))) LIKE UPPER('%" + dato +
                        "%') OR UPPER(TRIM(CONCAT(apellidos,' ',nombres))) LIKE UPPER('%" + dato + "%') LIMIT 1 ";
                }
                else
                {
                    sql1 = "SELECT email FROM clientes WHERE  UPPER(nombres || ' ' || apellidos) LIKE UPPER('%" + dato +
                        "%') OR UPPER(apellidos || ' ' || nombres) LIKE UPPER('%" + dato + "%') LIMIT 1 ";
                }
            }

            if (tipo_dato == "DNI")
            {
                if (MDIParent1.baseDatos == "MySql")
                {
                    sql1 = "SELECT email FROM clientes WHERE  " +
                        " id = '" + dato + "' LIMIT 1 ";
                }
                else
                {
                    sql1 = "SELECT email FROM clientes WHERE  " +
                        " id = '" + dato + "' LIMIT 1 ";
                }
            }

            if (tipo_dato == "Email")
            {
                if (MDIParent1.baseDatos == "MySql")
                {
                    sql1 = "SELECT email FROM clientes WHERE  " +
                        " email LIKE '%" + dato + "%' LIMIT 1 ";
                }
                else
                {
                    sql1 = "SELECT email FROM clientes WHERE  " +
                        " email LIKE '%" + dato + "%' LIMIT 1 ";
                }
            }

            try
            {
                DataSet ds = new DataSet();
                ds = con1.query(sql1);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["email"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar clientes cobranzas "+ex.Message);
            }

            return "";
        }

        public DataTable get_cliente_(string dato, string tipo_dato)
        {
            DataTable dt = new DataTable();

            conexion con1 = new conexion();

            if (tipo_dato == "Nombre")
            {
                if (MDIParent1.baseDatos == "MySql")
                {
                    sql1 = "SELECT * FROM clientes WHERE  " +
                        "UPPER(TRIM(CONCAT(nombres,' ',apellidos))) LIKE UPPER('%" + dato +
                        "%') OR UPPER(TRIM(CONCAT(apellidos,' ',nombres))) LIKE UPPER('%" + dato + "%') LIMIT 1 ";
                }
                else
                {
                    sql1 = "SELECT * FROM clientes WHERE  UPPER(nombres || ' ' || apellidos) LIKE UPPER('%" + dato +
                        "%') OR UPPER(apellidos || ' ' || nombres) LIKE UPPER('%" + dato + "%') LIMIT 1 ";
                }
            }

            if (tipo_dato == "DNI")
            {
                if (MDIParent1.baseDatos == "MySql")
                {
                    sql1 = "SELECT * FROM clientes WHERE  " +
                        " id = '" + dato + "' LIMIT 1 ";
                }
                else
                {
                    sql1 = "SELECT * FROM clientes WHERE  " +
                        " id = '" + dato + "' LIMIT 1 ";
                }
            }

            if (tipo_dato == "Email")
            {
                if (MDIParent1.baseDatos == "MySql")
                {
                    sql1 = "SELECT * FROM clientes WHERE  " +
                        " email LIKE '%" + dato + "%' LIMIT 1 ";
                }
                else
                {
                    sql1 = "SELECT * FROM clientes WHERE  " +
                        " email LIKE '%" + dato + "%' LIMIT 1 ";
                }
            }

            Console.WriteLine("SQL CL "+sql1);

            try
            {
                DataSet ds = new DataSet();
                ds = con1.query(sql1);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return dt;
                }

            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar clientes cobranzas " + ex.Message);
            }

            return dt;
        }
    }
}
