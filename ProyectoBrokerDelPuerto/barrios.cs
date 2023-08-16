using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class barrios
    {
        string sql = "";
        string columns = "id,nombre,telefono,direccion,email, sub_barrio, clase_barrio, suma_muerte, suma_gm, suma_rc, exige, observaciones, ultmod,user_edit,codestado,envionube";
        public string reg ,id, nombre, telefono, direccion, email, sub_barrio, clase_barrio, suma_muerte, suma_gm, suma_rc, exige, observaciones,  ultmod, user_edit, codestado = "1";
        public int envionube { get; set; } = 1;
        conexion con = new conexion();

        public barrios(bool inst = false)
        {
            if(inst)
                this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists barrios (reg serial PRIMARY KEY, id VARCHAR(100) NULL, nombre VARCHAR(1500) NULL, " +
                "telefono VARCHAR(100) NULL,direccion VARCHAR(100) NULL, email VARCHAR(100) NULL, sub_barrio VARCHAR(100) NULL, clase_barrio VARCHAR(100) NULL," +
                "suma_muerte VARCHAR(100) NULL, suma_gm VARCHAR(100) NULL, suma_rc VARCHAR(100) NULL, exige VARCHAR(100) NULL, observaciones VARCHAR(300) NULL, " +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
                con.query(sql);
            }
            else
            {
                sql = "CREATE TABLE if not exists barrios (reg INTEGER PRIMARY KEY, id VARCHAR(100) NULL, nombre VARCHAR(1500) NULL, " +
                "telefono VARCHAR(100) NULL,direccion VARCHAR(100) NULL, email VARCHAR(100) NULL, sub_barrio VARCHAR(100) NULL, clase_barrio VARCHAR(100) NULL," +
                "suma_muerte VARCHAR(100) NULL, suma_gm VARCHAR(100) NULL, suma_rc VARCHAR(100) NULL, exige VARCHAR(100) NULL, observaciones VARCHAR(300) NULL, " +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
                con.query(sql);
            }

            this.addColumn();
        }

        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                //Add column categoria if NOT exist
                if (con.query("Select CHARACTER_MAXIMUM_LENGTH as longitud from information_schema.columns WHERE TABLE_NAME='barrios' AND COLUMN_NAME='nombre' ").Tables[0].Rows[0]["longitud"].ToString() == "150")
                {
                    con.query("ALTER TABLE barrios MODIFY nombre VARCHAR(1500) NULL;");
                }
                if (con.query("SHOW COLUMNS FROM barrios WHERE Field = 'envionube' ").Tables[0].Rows.Count == 0)
                    con.query("ALTER TABLE barrios ADD COLUMN envionube int(10) DEFAULT 0");
            }
            else
            {
                con.query("ALTER TABLE barrios MODIFY nombre VARCHAR(1500) NULL;");
                con.query("ALTER TABLE barrios ADD COLUMN envionube int(10) DEFAULT 0");
            }

        }

        public bool validar_id()
        {
            sql = "SELECT id FROM barrios WHERE id = '" + this.id + "' AND codestado = '1' ";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }


            return false;
        }

        public DataSet busqueda_barrios(string busqueda_)
        {
            DataSet ds = new DataSet();
            sql = "SELECT id, nombre,suma_muerte,suma_gm FROM barrios WHERE ( id = '" + busqueda_ + "' OR nombre LIKE '%" + busqueda_+"%' ) AND  codestado = '1' ";
            ds = con.query(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds = con.query(sql);
            }


            return ds;
        }

        public DataSet get_all_fecha(string fecha)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM barrios  WHERE envionube > '0'  ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar coberturas " + ex.Message);
            }

            return ds;
        }

        public bool exist()
        {
            sql = "SELECT id,reg FROM barrios WHERE id = '" + this.id + "'";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                string sql1 = "DELETE FROM barrios WHERE id = '" + this.id + "' AND reg != '" + con.query(sql).Tables[0].Rows[0]["reg"].ToString() + "' ";
                if (con.query(sql).Tables[0].Rows.Count > 1)
                {
                    con.query(sql1);
                }
                return true;
            }


            return false;
        }


        public bool update_nuevapropuesta()
        {
            
            DataSet ds = new DataSet();
            string masdatos = "";
            if (this.suma_muerte != "" && this.suma_muerte != null)
            {
                masdatos += ", suma_muerte = '"+this.suma_muerte+"'";
            }
            if (this.suma_gm != "" && this.suma_gm != null)
            {
                masdatos += ", suma_gm = '" + this.suma_gm + "'" ;
            }
            if (this.suma_rc != "" && this.suma_rc != null)
            {
                masdatos += ", suma_rc = '" + this.suma_rc + "' ";
            }
            if (this.exige != "" && this.exige != null)
            {
                masdatos += ", exige = '" + this.exige + "' ";
            }

            sql = "UPDATE barrios SET email = '"+this.email+"'  "+masdatos+", envionube = '1'  WHERE id = '" + this.id + "' ";
            try
            {
                ds = con.query(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar barrios");
                
            }

            return false;

        }

        public string get_id(string nombre_)
        {
            string res = "";
            DataSet ds = new DataSet();

            sql = "SELECT id FROM barrios WHERE lower(nombre) = lower('"+nombre_+"') ";

            try
            {
                ds = con.query(sql);
                if(ds.Tables.Count > 0)
                {
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        res = ds.Tables[0].Rows[0]["id"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar barrios");
            }

            return res;
        }


        public string get_nombre(string id_)
        {
            string res = "";
            DataSet ds = new DataSet();

            sql = "SELECT nombre FROM barrios WHERE id = '" + id_ + "' ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        res = ds.Tables[0].Rows[0]["nombre"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar barrios");
            }

            return res;
        }

        public DataSet get()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM barrios WHERE id = '"+this.id+"' ";
            try
            {
                
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar barrios");
            }

            return ds;
        }


        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM barrios WHERE codestado = 1";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar barrios");
            }

            return ds;
        }


        public DataSet get_all_busqueda(string coincidencia)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM barrios WHERE codestado = 1 AND " +
                " ( nombre LIKE  '%" + coincidencia + "%' OR id LIKE  '%" + coincidencia + "%' OR telefono LIKE  '%" + coincidencia + "%' ) ORDER BY nombre ASC";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar barrios");
            }

            return ds;
        }

        public void borrarRegistros()
        {
            sql = "DELETE FROM barrios";
            con.query(sql);
        }



        public bool save()
        {
            try
            {
                if (this.exist())
                {
                    sql = "UPDATE barrios SET " +
                        "nombre = '" + this.formated_string_query(this.nombre) + "'," +
                        "telefono = '" + this.formated_string_query(this.telefono) + "'," +
                        "direccion = '" + this.direccion + "'," +
                        "email = '" + this.email + "'," +
                        "sub_barrio = '" + this.sub_barrio + "'," +
                        "clase_barrio = '" + this.clase_barrio + "'," +
                        "suma_muerte = '" + this.formated_string_query(this.suma_muerte) + "'," +
                        "suma_gm = '" + this.formated_string_query(this.suma_gm) + "'," +
                        "suma_rc = '" + this.formated_string_query(this.suma_rc) + "'," +
                        "exige = '" + this.exige + "'," +
                        "ultmod = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "codestado = '" + this.codestado + "'," +
                        "envionube = '" + this.envionube + "' ," +
                        "observaciones = '" + this.observaciones + "'" +
                        " WHERE id = '" + this.id + "' AND envionube < 1 ";
                }
                else
                {
                    sql = "INSERT INTO barrios (" + this.columns + ") VALUES(" +
                        "'" + this.formated_string_query(this.id) + "'," +
                        "'" + this.formated_string_query(this.nombre) + "'," +
                        "'" + this.telefono + "'," +
                        "'" + this.direccion + "'," +
                        "'" + this.email + "'," +
                        "'" + this.sub_barrio + "'," +
                        "'" + this.clase_barrio + "'," +
                        "'" + this.formated_string_query(this.suma_muerte) + "'," +
                        "'" + this.formated_string_query(this.suma_gm) + "'," +
                        "'" + this.formated_string_query(this.suma_rc) + "'," +
                        "'" + this.exige + "'," +
                        "'" + this.observaciones + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.envionube + "'" +
                        ") ";
                }

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error 1" + sql + "\n" + ex.Message);
                return false;
            }

        }

        public string formated_string_query(string text_)
        {
            text_ = text_.Replace("\n", "");
            text_ = text_.Replace("\n", "");
            text_ = text_.Replace("\r", "");
            text_ = text_.Replace("\r", "");
            text_ = text_.Replace("\r\n", "");
            text_ = text_.Replace("\r\n", "");
            text_ = text_.TrimEnd('\n');
            text_ = text_.Replace("\t", " ");
            text_ = text_.Replace("\t", " ");
            text_ = text_.Replace("\t", " ").Replace("\n", "").Replace("\"", "");
            return text_.Trim();
        }

        public void save_concat(string sql1)
        {
            con.query(sql1);
        }

        public string concat_sql()
        {
            try
            {
                if (this.exist())
                {
                    sql = "UPDATE barrios SET " +
                        "nombre = '" + this.nombre + "'," +
                        "telefono = '" + this.telefono + "'," +
                        "direccion = '" + this.direccion + "'," +
                        "email = '" + this.email + "'," +
                        "sub_barrio = '" + this.sub_barrio + "'," +
                        "clase_barrio = '" + this.clase_barrio + "'," +
                        "suma_muerte = '" + this.suma_muerte + "'," +
                        "suma_gm = '" + this.suma_gm + "'," +
                        "suma_rc = '" + this.suma_rc + "'," +
                        "exige = '" + this.exige + "'," +
                        "ultmod = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "codestado = '"+this.codestado+"'," +
                        "observaciones = '" + this.observaciones + "'" +
                        " WHERE id = '" + this.id + "' AND envionube < 1; ";
                }
                else
                {
                    sql = "INSERT INTO barrios (" + this.columns + ") VALUES(" +
                        "'" + this.id + "'," +
                        "'" + this.nombre + "'," +
                        "'" + this.telefono + "'," +
                        "'" + this.direccion + "'," +
                        "'" + this.email + "'," +
                        "'" + this.sub_barrio + "'," +
                        "'" + this.clase_barrio + "'," +
                        "'" + this.suma_muerte + "'," +
                        "'" + this.suma_gm + "'," +
                        "'" + this.suma_rc + "'," +
                        "'" + this.exige + "'," +
                        "'" + this.observaciones + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'0'" +
                        "); ";
                }

                return sql;

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public void envionube_()
        {
            sql = "UPDATE barrios SET envionube = '0' WHERE envionube = '1' ";
            con.query(sql);
        }

        public void delete()
        {
            sql = "UPDATE barrios SET codestado = 0, envionube = '1', ultmod = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"' WHERE reg = '" + this.reg + "' ";
            con.query(sql);
        }


    }
}
