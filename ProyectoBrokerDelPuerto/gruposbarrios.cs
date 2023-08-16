using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class gruposbarrios
    {
        string sql = "";
        string columns = "id,nombre,idbarrio,nombrebarrio,ultmod,envionube,codestado";
        public string reg, id, nombre, idbarrio, nombrebarrio = "", envionube = "1",codestado = "1";
        public string ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public int access = 0;
        conexion con = new conexion();

        public gruposbarrios(bool inst = false)
        {
            if(inst)
                this.install();
        }


        private void install()
        {
            /*sql = "DROP TABLE gruposbarrios;";
            con.query(sql);*/
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists gruposbarrios (reg serial PRIMARY KEY, id INT(10) , nombre VARCHAR(150) NULL, idbarrio VARCHAR(100) NULL, " +
                "nombrebarrio VARCHAR(1500) NULL);";
            }
            else
            {
                sql = "CREATE TABLE if not exists gruposbarrios (reg INTEGER PRIMARY KEY, id INT(10) , nombre VARCHAR(150) NULL, idbarrio VARCHAR(100) NULL, " +
                "nombrebarrio VARCHAR(1500) NULL);";
            }
            con.query(sql);

            con.query("UPDATE gruposbarrios SET nombrebarrio = TRIM(nombrebarrio) ");

            this.addColumn();
        }

        private void addColumn()
        {
            try
            {
                if (MDIParent1.baseDatos == "MySql")
                {
                    //Add column categoria if NOT exist
                    if (con.query("Select CHARACTER_MAXIMUM_LENGTH as longitud from information_schema.columns WHERE TABLE_NAME='gruposbarrios' AND COLUMN_NAME='nombre' ").Tables[0].Rows[0]["longitud"].ToString() == "1500")
                    {
                        con.query("ALTER TABLE gruposbarrios MODIFY nombre VARCHAR(150) NULL;");
                        con.query("ALTER TABLE gruposbarrios MODIFY nombrebarrio VARCHAR(1500) NULL;");
                    }
                    if (con.query("SHOW COLUMNS FROM gruposbarrios WHERE Field = 'ultmod' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE gruposbarrios ADD COLUMN ultmod DATETIME DEFAULT CURRENT_TIMESTAMP;");
                    if (con.query("SHOW COLUMNS FROM gruposbarrios WHERE Field = 'envionube' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE gruposbarrios ADD COLUMN envionube INT(2) DEFAULT 0;");
                    if (con.query("SHOW COLUMNS FROM gruposbarrios WHERE Field = 'codestado' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE gruposbarrios ADD COLUMN codestado INT(2) DEFAULT 1;");
                }
                else
                {
                    con.query("ALTER TABLE gruposbarrios MODIFY nombre VARCHAR(150) NULL;");
                    con.query("ALTER TABLE gruposbarrios MODIFY nombrebarrio VARCHAR(1500) NULL;");
                    con.query("ALTER TABLE gruposbarrios ADD COLUMN ultmod DATETIME  DEFAULT CURRENT_TIMESTAMP;");
                    con.query("ALTER TABLE gruposbarrios ADD COLUMN envionube INT(2) DEFAULT 0;");
                    con.query("ALTER TABLE gruposbarrios ADD COLUMN codestado INT(2) DEFAULT 1;");
                }
            }
            catch
            {
                //
            }
            

        }

        public bool exist(string nombre_)
        {
            sql = "SELECT nombre FROM gruposbarrios WHERE TRIM(nombre) = TRIM('" + nombre_ + "')";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }
            
            return false;
        }

        public int id_consecutivo()
        {
            DataSet ds = new DataSet();

            sql = "SELECT MAX(id) as m FROM gruposbarrios ";
            try
            {
                ds = con.query(sql);
                return   ( Convert.ToInt16(ds.Tables[0].Rows[0]["m"].ToString() ) + 1 );

            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar gruposbarrios " + ex.Message);
            }

            return 1;
        }

        public bool get()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM gruposbarrios WHERE TRIM( nombre ) = TRIM( '" + this.nombre + "' ) AND codestado = '1' ";
            try
            {
                ds = con.query(sql);
                this.reg = ds.Tables[0].Rows[0]["reg"].ToString();
                this.id = ds.Tables[0].Rows[0]["id"].ToString();
                this.nombre = ds.Tables[0].Rows[0]["nombre"].ToString();
                this.idbarrio = ds.Tables[0].Rows[0]["idbarrio"].ToString();
                this.nombrebarrio = ds.Tables[0].Rows[0]["nombrebarrio"].ToString();
                this.codestado = ds.Tables[0].Rows[0]["codestado"].ToString();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar gruposbarrios " + ex.Message);
            }

            return false;
        }

        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT nombre FROM gruposbarrios WHERE codestado = '1' GROUP BY nombre ORDER BY nombre ASC ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar gruposbarrios " + ex.Message);
            }

            return ds;
        }

        public DataSet get_all_id()
        {
            DataSet ds = new DataSet();

            sql = "SELECT reg,id,nombre,idbarrio,TRIM(nombrebarrio) as nombrebarrio,ultmod FROM gruposbarrios WHERE nombre = '" + this.nombre+"' AND codestado = '1' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar gruposbarrios " + ex.Message);
            }

            return ds;
        }



        public DataSet get_all_fecha(string fecha)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM gruposbarrios  WHERE envionube > '0' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar grupo de barrios");
            }

            return ds;
        }


        public void save_concat(string sql1)
        {
            con.query("INSERT INTO gruposbarrios (" + this.columns + ") VALUES "+sql1);
        }


        public string concat_sql()
        {
            try
            {

                sql = "(" +
                    "'" + this.id + "'," +
                    "'" + this.nombre + "'," +
                    "'" + this.idbarrio + "'," +
                    "'" + this.nombrebarrio + "'," +
                    "'" + this.ultmod + "'," +
                    "'0'" +
                    "'"+ this.codestado + "'," +
                    ") ";


                return sql;

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public bool save()
        {
            barrios bar = new barrios();
            try
            {
               
                sql = "INSERT INTO gruposbarrios (" + this.columns + ") VALUES(" +
                    "'" + this.id + "'," +
                    "'" + bar.formated_string_query(this.nombre) + "'," +
                    "'" + bar.formated_string_query(this.idbarrio) + "'," +
                    "'" + bar.formated_string_query(this.nombrebarrio) + "'," +
                    "'" + this.ultmod + "'," +
                    "'" + this.envionube + "'," +
                    "'" + this.codestado + "'" +
                    ") ";
               

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async void importGetApi()
        {
            ApiGrupoBarrios ap = new ApiGrupoBarrios();
            List<gruposbarrios> ls = await ap.Get();
            validaciones val = new validaciones();
            if (val.dataComparacion("api_gruposbarrios", ls))
            {
                var concat_ = new System.Text.StringBuilder();
                List<string> noms = new List<string>();

                if (ls.Count > 0)
                {
                    gruposbarrios gb = new gruposbarrios();
                    gb.delete_all();
                    foreach (gruposbarrios obj in ls)
                    {
                        if (obj.idbarrio != null && noms.IndexOf(obj.nombre.Trim().Trim().Trim() + obj.idbarrio.Trim()) < 0)
                        {
                            /*if (concat_.ToString() != "")
                                concat_.AppendLine(", " + obj.concat_sql());
                            else
                                concat_.AppendLine(obj.concat_sql());*/
                            obj.codestado = "1";
                            obj.envionube = "0";
                            obj.save();

                            noms.Add(obj.nombre.Trim().Trim().Trim().Trim() + obj.idbarrio);
                        }

                    }
                }

                val.createData0("api_gruposbarrios");
            }


        }

        public void actualizar_envionube()
        {
            sql = "UPDATE gruposbarrios SET envionube = '0' WHERE envionube = '1' ";
            con.query(sql);
        }

        public void delete_all()
        {
            sql = "DELETE FROM gruposbarrios WHERE id > 0 AND envionube < 1 ";
            con.query(sql);
        }

        public void delete()
        {
            sql = "UPDATE gruposbarrios SET codestado = '0', envionube = '1' WHERE TRIM(nombre) LIKE TRIM('" + this.nombre.Trim() + "') ";
            con.query(sql);
        }
    }
}
