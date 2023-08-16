using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class actividades
    {
        string sql = "";
        string columns = "cod, nombre, ultmod, user_edit, codestado,version ";
        public string id, cod, nombre, ultmod, user_edit, codestado = "";

        public int version { set; get; } = 0;
        conexion con = new conexion();

        public actividades(bool inst = false)
        {
            if(inst)
                this.install();
        }


        private void install()
        {

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists actividades (id serial PRIMARY KEY, cod INT(6) NULL, nombre VARCHAR(150) NULL, " +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists actividades (id INTEGER PRIMARY KEY, cod INT(6) NULL, nombre VARCHAR(150) NULL, " +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }
                con.query(sql);
            this.addColumn();
        }

        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                try
                {

                    //Add column categoria if NOT exist
                    if (con.query("SHOW COLUMNS FROM actividades WHERE Field = 'version' ").Tables[0].Rows.Count == 0)
                    {
                        con.query("ALTER TABLE actividades ADD COLUMN version INT(11) DEFAULT 0");
                    }

                }
                catch
                {
                    //
                }
            }
            else
            {
                con.query("ALTER TABLE actividades ADD COLUMN version INT(11) DEFAULT 0");
            }


        }

        public bool codigo_exist(string id_)
        {

            DataSet ds = new DataSet();

            sql = "SELECT cod FROM actividades WHERE cod = '" + id_ + "' ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar actividades "+ex.Message);
            }

            return false;
        }

        public bool exist()
        {
            if(this.id != null && this.id != "")
            {
                sql = "SELECT id, version FROM actividades WHERE id = '" + this.id.Trim() + "'";
                if (con.query(sql).Tables[0].Rows.Count > 0)
                {
                    if (this.version < 2)
                        this.version = Convert.ToInt16(con.query(sql).Tables[0].Rows[0]["version"].ToString()) + 1;
                    return true;
                }
                
            }

            return false;

        }


        public string get_id(string nombre_)
        {
            nombre_ = corregir_nombre(nombre_);
            string res = "";
            DataSet ds = new DataSet();

            sql = "SELECT id FROM actividades WHERE nombre = '" + nombre_ + "' ";

            

            try
            {
                ds = con.query(sql);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        res = ds.Tables[0].Rows[0]["id"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar actividades "+ex.Message);
            }

            return res;
        }

        public string corregir_nombre(string res)
        {

            try {

                if (Convert.ToInt16(res.Substring(0, 1)) >= 0)
                {
                    return res.Substring(res.IndexOf("-") + 2, res.Length - res.IndexOf("-") - 2);
                }
            }
            catch
            {
                return res;
            }

            return res;
        }

        public DataSet get_all_estado()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM actividades WHERE codestado = 1 ORDER BY nombre ASC";
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

        public DataSet get_all_fecha(string fecha)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM actividades  WHERE ultmod >= '"+fecha+ "' AND ultmod <= '" 
                + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

            Console.WriteLine(sql);
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

        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM actividades ORDER BY nombre ASC";
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


        public DataSet get_nombre(string nombre_)
        {
            nombre_ = corregir_nombre(nombre_);
            DataSet ds = new DataSet();

            sql = "SELECT * FROM actividades WHERE nombre = '"+ nombre_ + "' ORDER BY nombre ASC";
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

        public DataSet get(string id_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM actividades WHERE  id = '" + id_ +"'";
            //Console.Write("\n\n"+sql);
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



        public void borrarRegistros()
        {
            sql = "DELETE FROM actividades";
            con.query(sql);
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
                    sql = "UPDATE actividades SET " +
                        "cod = '" + this.cod + "'," +
                        "nombre = '" + corregir_nombre(this.nombre) + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "user_edit = '" + this.user_edit + "'" +
                        " WHERE id = '" + this.id + "'; ";
                }
                else
                {
                    sql = "INSERT INTO actividades (" + this.columns + ") VALUES(" +
                        "'" + this.cod + "'," +
                        "'" + corregir_nombre(this.nombre) + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'" +
                        "); ";
                }

                return sql;

            }
            catch (Exception ex)
            {
                return "";
            }

        }


        public bool save(bool importCloud = false)
        {
            try
            {
                if (this.exist())
                {
                    sql = "UPDATE actividades SET " +
                        "cod = '" + this.cod + "'," +
                        "nombre = '" + corregir_nombre(this.nombre) + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "version = '" + this.version + "'," +
                        "user_edit = '" + this.user_edit + "'" +
                        " WHERE id = '" + this.id + "'";
                }
                else
                {
                    sql = "INSERT INTO actividades (" + this.columns + ") VALUES(" +
                        "'" + this.cod + "'," +
                        "'" + corregir_nombre(this.nombre) + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.version + "'" +
                        ") ";
                }

                con.query(sql);

                if (importCloud == false)
                {
                    if (this.id == null || this.id == "")
                    {
                        sql = $"SELECT MAX(id) as id FROM actividades limit 1";
                        DataSet dsCons = con.query(sql);
                        if (dsCons.Tables[0].Rows.Count > 0)
                            this.id = dsCons.Tables[0].Rows[0]["id"].ToString();
                    }

                    Task.Run(() => {
                        ApiActividades apact = new ApiActividades();
                        List<actividades> lsact = new List<actividades>();
                        lsact.Add(this);
                        apact.Post(lsact);
                    });
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool versionUpdate(string id_, int version_)
        {
            sql = "SELECT COUNT(1) as cant FROM actividades WHERE id = '" + id_ + "' AND version > 0 ";
            DataSet ds = con.query(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "0")
                {
                    sql = $"SELECT COUNT(1) as cant FROM actividades WHERE cod = '{id_ }' AND version < '{version_}' ";
                    ds = con.query(sql);
                    if (ds.Tables[0].Rows[0]["cant"].ToString() == "0")
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public async void importGetApi()
        {
            ApiActividades apact = new ApiActividades();
            List<actividades> lsact = await apact.Get();
            validaciones val = new validaciones();
            if (val.dataComparacion("api_actividades", lsact))
            {
                foreach (actividades c in lsact)
                {
                    if (versionUpdate(c.id, c.version))
                        c.save(true);
                }
                val.createData0("api_actividades");
            }
        }

        public void delete()
        {
            this.exist();
            sql = "UPDATE actividades SET codestado = 0, ultmod = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', version = '"+this.version+"' WHERE id = '" + this.id + "' ";
            con.query(sql);
        }
    }
}
