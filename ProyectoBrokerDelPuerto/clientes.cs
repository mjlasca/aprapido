using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class clientes
    {
        string sql = "";
        string columns = "id,nombres,apellidos,tipo_id,telefono,direccion,email, codpostal, localidad,ciudad, sexo, fecha_nacimiento, situacion,ultmod,user_edit,codestado,categoria,codempresa,idaseguradora,envionube";
        public string id, nombres, apellidos, tipo_id, telefono, direccion, email, codpostal, localidad, ciudad, sexo, fecha_nacimiento, situacion, ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), user_edit = MDIParent1.sesionUser, codestado = "1", categoria, codempresa, idaseguradora = "";
        public int envionube = 0;
        conexion con = new conexion();

        public clientes(bool installsi = false)
        {
            if(installsi)
                this.install();
        }

       
        private void install()
        {

            /*
            sql = " SELECT DISTINCT * FROM clientes GROUP BY id  HAVING COUNT(id) > 1";
            Console.Write("\n\n" + con.query(sql).Tables[0].Rows.Count + "\n\n");

            for (int i = 0; i < con.query(sql).Tables[0].Rows.Count; i++)
            {
                sql = "DELETE FROM clientes WHERE reg = '"+ con.query(sql).Tables[0].Rows[i]["reg"].ToString()+ "' ";
            }*/
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists clientes (reg serial PRIMARY KEY, id VARCHAR(100) NULL, nombres VARCHAR(150) NULL, apellidos VARCHAR(150) NULL, tipo_id VARCHAR(100) NULL," +
                    "telefono VARCHAR(100) NULL,direccion VARCHAR(100) NULL, email VARCHAR(100) NULL, codpostal VARCHAR(50) NULL, localidad VARCHAR(100) NULL, ciudad VARCHAR(100) NULL," +
                    " sexo VARCHAR(50) NULL, fecha_nacimiento DATE NULL, situacion VARCHAR(100) NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL, categoria VARCHAR(80) NULL);";
            }
            else
            {
                sql = "CREATE TABLE if not exists clientes (reg INTEGER PRIMARY KEY, id VARCHAR(100) NULL, nombres VARCHAR(150) NULL, apellidos VARCHAR(150) NULL, tipo_id VARCHAR(100) NULL," +
                    "telefono VARCHAR(100) NULL,direccion VARCHAR(100) NULL, email VARCHAR(100) NULL, codpostal VARCHAR(50) NULL, localidad VARCHAR(100) NULL, ciudad VARCHAR(100) NULL," +
                    " sexo VARCHAR(50) NULL, fecha_nacimiento DATE NULL, situacion VARCHAR(100) NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL, categoria VARCHAR(80) NULL);";
            }
            
            con.query(sql);

            this.addColumn();

            this.addIndex();

            con.query("UPDATE clientes SET telefono = replace(telefono , ' ',''), telefono = replace(telefono , '-','') WHERE telefono != '' ");
            con.query("UPDATE clientes SET codempresa = '"+MDIParent1.codempresa+"' WHERE codempresa IS NULL OR codempresa = '' ");
            if (MDIParent1.baseDatos != "MySql")
            {
                con.query("UPDATE clientes SET fecha_nacimiento = NULL WHERE fecha_nacimiento = '0000-00-00' ");
                con.query("UPDATE clientes SET ultmod = NULL WHERE ultmod = '0000-00-00 00:00:00' ");
            }
        }

        private void addIndex()
        {
            if (MDIParent1.baseDatos != "MySql")
            {
                try
                {
                    //sql = " CREATE INDEX IF NOT EXISTS busquedaClientes ON clientes(id, nombres, apellidos); ";
                    sql = "CREATE INDEX idx_id_cliente on clientes (id, nombres); ";
                    con.query(sql);
                }
                catch
                {
                    //
                }
            }


        }

        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                try
                {
                    if (con.query("SHOW COLUMNS FROM clientes WHERE Field = 'categoria' ").Tables[0].Rows.Count == 0)
                    {
                        con.query("ALTER TABLE clientes ADD COLUMN categoria VARCHAR(80) NULL");
                    }
                    if (con.query("SHOW COLUMNS FROM clientes WHERE Field = 'codempresa' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE clientes ADD COLUMN codempresa VARCHAR(150) NULL;");
                    if (con.query("SHOW COLUMNS FROM clientes WHERE Field = 'idaseguradora' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE clientes ADD COLUMN idaseguradora VARCHAR(150) NULL;");
                    if (con.query("SHOW COLUMNS FROM clientes WHERE Field = 'envionube' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE clientes ADD COLUMN envionube INT(1) DEFAULT 1;");
                }
                catch
                {
                    //
                }
            }
            else
            {
                con.query("ALTER TABLE clientes ADD COLUMN categoria VARCHAR(80) NULL");
                con.query("ALTER TABLE clientes ADD COLUMN codempresa VARCHAR(150) NULL;");
                con.query("ALTER TABLE clientes ADD COLUMN idaseguradora VARCHAR(150) NULL;");
                con.query("ALTER TABLE clientes ADD COLUMN envionube INT(1) DEFAULT 1;");
            }
            

        }

        public  bool exist()
        {
            sql = "SELECT id FROM clientes WHERE id = '" + this.id + "'";
            if(con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }


            return false;
        }

        

        public bool validar_id()
        {
            sql = "SELECT id FROM clientes WHERE id = '" + this.id + "' AND codestado = '1'  ";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }


            return false;
        }

        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM clientes WHERE codestado = 1";
            Console.WriteLine("CLI LOAD "+sql);
            try
            {
                ds = con.query(sql);
            }catch (Exception ex)
            {
                Console.Write("ERROR al consultar clientes " + ex.Message);
            }

            return ds;
        }

        public DataSet get_all_clientes()
        {
            DataSet ds = new DataSet();

            sql = "SELECT tipo_id, id, nombres, apellidos, telefono, direccion, email FROM clientes WHERE codestado = 1";
            Console.WriteLine("CLI LOAD " + sql);
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar clientes");
            }

            return ds;
        }

        public DataSet get(string id_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM clientes WHERE id = '"+ id_ + "' LIMIT 1";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar clientes");
            }

            return ds;
        }


        public DataSet get_all_ExpClientesfecha(string fecha)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM clientes  WHERE ultmod >= '" + fecha + "' AND ultmod <= '"
                + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' AND fecha_nacimiento != '0000-00-00' ";

            Console.WriteLine("CONSULTA DE CLIENTES \n" + sql);
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar actividades");
            }

            return ds;
        }

        public DataSet getEnvioNube()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM clientes  WHERE envionube = 0 ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar actividades");
            }

            return ds;
        }

        public void setEnvioNube()
        {
            sql = "UPDATE clientes  SET envionube = 1 WHERE envionube = 0 ";
            try
            {
                con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar actividades");
            }
        }


        public DataSet get_all_fecha(string fecha, int index = 0, int length = 2000)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM clientes  WHERE ultmod >= '" + fecha + "'  AND ultmod <= '"
                + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' AND fecha_nacimiento != '0000-00-00' LIMIT "+index+","+length+"";

            Console.WriteLine("CONSULTA DE CLIENTES \n"+sql);
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar actividades");
            }

            return ds;
        }




        public DataSet get_all_busqueda(string coincidencia)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM clientes WHERE codestado = 1 AND "+
                " ( nombres LIKE  '%"+coincidencia+ "%' OR id LIKE  '%" + coincidencia + "%' OR apellidos LIKE  '%" + coincidencia + "%' OR tipo_id LIKE  '%" 
                + coincidencia + "%' OR telefono LIKE  '%" + coincidencia + "%'   )";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar clientes");
            }

            return ds;
        }

        public void borrarRegistros()
        {
            sql = "DELETE FROM clientes";
            con.query(sql);
        }


        public string compilar_save()
        {

            sql = "";

            try
            {
                if (this.exist())
                {
                    sql = "UPDATE clientes SET " +
                        "nombres = '" + this.nombres + "'," +
                        "apellidos = '" + this.apellidos + "'," +
                        "tipo_id = '" + this.tipo_id + "'," +
                        "telefono = '" + this.telefono + "'," +
                        "direccion = '" + this.direccion + "'," +
                        "email = '" + this.email + "'," +
                        "codpostal = '" + this.codpostal + "'," +
                        "localidad = '" + this.localidad + "'," +
                        "ciudad = '" + this.ciudad + "'," +
                        "sexo = '" + this.sexo + "'," +
                        "fecha_nacimiento = '" + this.fecha_nacimiento + "'," +
                        "situacion = '" + this.situacion + "'," +
                        "user_edit = '" + this.user_edit + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "codestado  = '" + this.codestado + "'," +
                        "categoria = '" + this.categoria + "', " +
                        "idaseguradora = '" + this.idaseguradora + "', " +
                        "idaseguradora = '" + this.idaseguradora + "', " +
                        "codempresa = '" + MDIParent1.codempresa + "' " +
                        " WHERE id = '" + this.id + "' ;";
                }
                else
                {
                    sql = "INSERT INTO clientes (" + this.columns + ") VALUES(" +
                        "'" + this.id + "'," +
                        "'" + this.nombres + "'," +
                        "'" + this.apellidos + "'," +
                        "'" + this.tipo_id + "'," +
                        "'" + this.telefono + "'," +
                        "'" + this.direccion + "'," +
                        "'" + this.email + "'," +
                        "'" + this.codpostal + "'," +
                        "'" + this.localidad + "'," +
                        "'" + this.ciudad + "'," +
                        "'" + this.sexo + "'," +
                        "'" + this.fecha_nacimiento + "'," +
                        "'" + this.situacion + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.categoria + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + this.idaseguradora + "'" +
                        ") ;";
                }

                return sql;
            }
            catch(Exception ex)
            {
                return "";
            }
 
                

          
        }

        public void query_compil(string ssql)
        {
            Console.WriteLine("\n\nSSQL"+ssql);
            con.query(ssql);
        }

        public bool save()
        {
            try
            {
                if (this.exist())
                {
                    sql = "UPDATE clientes SET " +
                        "nombres = '" + this.nombres + "'," +
                        "apellidos = '" + this.apellidos + "'," +
                        "tipo_id = '" + this.tipo_id + "'," +
                        "telefono = '" + this.telefono + "'," +
                        "direccion = '" + this.direccion + "'," +
                        "email = '" + this.email + "'," +
                        "codpostal = '" + this.codpostal + "'," +
                        "localidad = '" + this.localidad + "'," +
                        "ciudad = '" + this.ciudad + "'," +
                        "sexo = '" + this.sexo + "'," +
                        "fecha_nacimiento = '" + this.fecha_nacimiento + "'," +
                        "situacion = '" + this.situacion + "'," +
                        "user_edit = '" + this.user_edit + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "codestado  = '" + this.codestado + "'," +
                        "categoria = '" + this.categoria + "', " +
                        "idaseguradora = '" + this.idaseguradora + "', " +
                        "envionube = '" + this.envionube + "', " +
                        "codempresa = '" + MDIParent1.codempresa + "' " +
                        " WHERE id = '" + this.id + "'";
                }
                else
                {
                    sql = "INSERT INTO clientes (" + this.columns + ") VALUES(" +
                        "'" + this.id + "'," +
                        "'" + this.nombres + "'," +
                        "'" + this.apellidos + "'," +
                        "'" + this.tipo_id + "'," +
                        "'" + this.telefono + "'," +
                        "'" + this.direccion + "'," +
                        "'" + this.email + "'," +
                        "'" + this.codpostal + "'," +
                        "'" + this.localidad + "'," +
                        "'" + this.ciudad + "'," +
                        "'" + this.sexo + "'," +
                        "'" + this.fecha_nacimiento + "'," +
                        "'" + this.situacion + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.categoria + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + idaseguradora + "'," +
                        "'" + this.envionube + "'" +
                        ") ";
                }


                
                con.query(sql);
                return true;

            }catch(Exception ex)
            {
                return false;
            }

        }

        public void save_concat(string sql1)
        {
            con.query("INSERT INTO clientes (" + this.columns + ") VALUES "+sql1);
        }

        public string concat_sql()
        {
            try
            {
                

                if (this.exist())
                {
                    sql = "UPDATE clientes SET " +
                        "nombres = '" + this.nombres + "'," +
                        "apellidos = '" + this.apellidos + "'," +
                        "tipo_id = '" + this.tipo_id + "'," +
                        "telefono = '" + this.telefono + "'," +
                        "direccion = '" + this.direccion + "'," +
                        "email = '" + this.email + "'," +
                        "codpostal = '" + this.codpostal + "'," +
                        "localidad = '" + this.localidad + "'," +
                        "ciudad = '" + this.ciudad + "'," +
                        "sexo = '" + this.sexo + "'," +
                        "fecha_nacimiento = '" + this.fecha_nacimiento + "'," +
                        "situacion = '" + this.situacion + "'," +
                        "user_edit = '" + this.user_edit + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "codestado  = '" + this.codestado + "'," +
                        "categoria = '" + this.categoria + "', " +
                        "idaseguradora = '" + this.idaseguradora + "', " +
                        "codempresa = '" + MDIParent1.codempresa + "' " +
                        " WHERE id = '" + this.id + "'";

                    con.query(sql);
                    sql = "";
                }
                else
                {
                    sql = " ( " +
                        "'" + this.id + "'," +
                        "'" + this.nombres + "'," +
                        "'" + this.apellidos + "'," +
                        "'" + this.tipo_id + "'," +
                        "'" + this.telefono + "'," +
                        "'" + this.direccion + "'," +
                        "'" + this.email + "'," +
                        "'" + this.codpostal + "'," +
                        "'" + this.localidad + "'," +
                        "'" + this.ciudad + "'," +
                        "'" + this.sexo + "'," +
                        "'" + this.fecha_nacimiento + "'," +
                        "'" + this.situacion + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.categoria + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + this.idaseguradora + "'" +
                        ") ";
                }


                return sql;

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public void delete_id()
        {
            sql = "DELETE FROM clientes WHERE id = '" + this.id + "'; ";
            con.query(sql);
        }

        public void delete()
        {
            sql = "UPDATE clientes SET codestado = 0 WHERE id = '" + this.id + "' ";
            con.query(sql);
        }



    }

}
