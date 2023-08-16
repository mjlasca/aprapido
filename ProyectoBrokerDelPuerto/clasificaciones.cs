using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class clasificaciones
    {
        string sql = "";
        string columns = "cod, nombre, id_actividad, ultmod, user_edit, codestado, version ";
        public string id, cod, nombre, id_actividad, ultmod, user_edit, codestado = "";
        conexion con = new conexion();
        public int version { set; get; } = 0;
        public clasificaciones(bool inst = false)
        {
            if(inst)
                this.install();
        }


        private void install()
        {

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists clasificaciones (id serial PRIMARY KEY, cod INT(6) NULL,  nombre VARCHAR(150) NULL, id_actividad INT(10) NULL," +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists clasificaciones (id INTEGER PRIMARY KEY, cod INT(6) NULL,  nombre VARCHAR(150) NULL, id_actividad INT(10) NULL," +
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
                    if (con.query("SHOW COLUMNS FROM clasificaciones WHERE Field = 'version' ").Tables[0].Rows.Count == 0)
                    {
                        con.query("ALTER TABLE clasificaciones ADD COLUMN version INT(11) DEFAULT 0");
                    }

                }
                catch
                {
                    //
                }
            }
            else
            {
                con.query("ALTER TABLE clasificaciones ADD COLUMN version INT(11) DEFAULT 0");
            }


        }

        public bool exist()
        {
            if (this.id != null && this.id != "")
            {
                sql = "SELECT id, version FROM clasificaciones WHERE id = '" + this.id.Trim() + "'";
                if (con.query(sql).Tables[0].Rows.Count > 0)
                {
                    if (this.version < 2)
                        this.version = Convert.ToInt16(con.query(sql).Tables[0].Rows[0]["version"].ToString()) + 1;
                    return true;
                }
            }
            return false;
        }

        public string corregir_nombre(string res)
        {
            try
            {

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


        public string get_id(string nombre_)
        {
            nombre_ = corregir_nombre(nombre_);
            string res = "";
            DataSet ds = new DataSet();

            sql = "SELECT id FROM clasificaciones WHERE nombre = '" + nombre_ + "' ";
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
                Console.Write("ERROR al consultar clasificaciones " + ex.Message);
            }

            return res;
        }

        public DataSet get(string id_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM clasificaciones WHERE id = '"+id_+"' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar clasificaciones");
            }

            return ds;
        }
        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            string filtros = "";

            if (this.cod != "")
            {
                filtros = " cod='" + this.cod + "' ";
            }
            if(this.nombre != "")
            {
                if(filtros != "")
                    filtros += " OR nombre LIKE '%" + this.nombre+"%' ";
                else
                    filtros = "  nombre LIKE '%" + this.nombre + "%' ";
            }

            if (filtros != "")
            {
                filtros = " AND ( " + filtros + " ) ";
            }

            if (this.id_actividad != "")
            {
                filtros += "AND id_actividad =  '" + this.id_actividad + "' ";
            }

            sql = "SELECT * FROM clasificaciones WHERE codestado = 1 "+filtros+" ORDER BY nombre ASC";


            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar clasificaciones");
            }

            return ds;
        }


        public DataSet get_all_fecha(string fecha)
        {
            DataSet ds = new DataSet();
            sql = "SELECT * FROM clasificaciones WHERE ultmod > '" + fecha + "' AND  ultmod <= '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar clasificaciones");
            }

            return ds;
        }

        public DataSet get_all_actividad()
        {
            DataSet ds = new DataSet();
            sql = "SELECT * FROM clasificaciones WHERE id_actividad = '"+this.id_actividad+"'  ORDER BY cod ASC";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar clasificaciones");
            }

            return ds;
        }


        //Se revisa si hay un registro que tenga el mismos código y actividad
        public bool codigo_actividad_exist(string cod_, string id_)
        {

            DataSet ds = new DataSet();

            sql = "SELECT id,nombre,id_actividad FROM clasificaciones WHERE cod = '" + cod_ + "' ";
            
            try
            {
                ds = con.query(sql);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            clasificaciones ac = new clasificaciones();
                        //Console.WriteLine("ACT " + ds.Tables[0].Rows[i]["id_actividad"].ToString() + " / " + id_);
                            if (ds.Tables[0].Rows[i]["id_actividad"].ToString() == id_)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar clasificaciones "+ex.Message);
                return false;
            }

            return false;
        }

        public bool codigo_exist(string cod_)
        {
            
            DataSet ds = new DataSet();

            sql = "SELECT cod FROM clasificaciones WHERE cod = '" + cod_ + "' ";
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
                Console.Write("ERROR al consultar clasificaciones");
            }

            return false;
        }

        public string get_nombre(string id_)
        {
            string res = "";
            DataSet ds = new DataSet();

            sql = "SELECT nombre FROM clasificaciones WHERE id = '" + id_ + "' ";
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
                Console.Write("ERROR al consultar clasificaciones");
            }

            return res;
        }

        public void borrarRegistros()
        {
            sql = "DELETE FROM clasificaciones";
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
                    sql = "UPDATE clasificaciones SET " +
                        "cod = '" + this.cod + "'," +
                        "nombre = '" + corregir_nombre(this.nombre) + "'," +
                        "id_actividad = '" + this.id_actividad + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "user_edit = '" + this.user_edit + "'" +
                        " WHERE id = '" + this.id + "' ; ";
                }
                else
                {
                    sql = "INSERT INTO clasificaciones (" + this.columns + ") VALUES(" +
                        "'" + this.cod + "'," +
                        "'" + corregir_nombre(this.nombre) + "'," +
                        "'" + this.id_actividad + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'" +
                        ") ; ";
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
                    sql = "UPDATE clasificaciones SET " +
                        "cod = '" + this.cod + "'," +
                        "nombre = '" + corregir_nombre( this.nombre ) + "'," +
                        "id_actividad = '" + this.id_actividad + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "version = '" + this.version + "'," +
                        "user_edit = '" + this.user_edit + "'" +
                        " WHERE id = '" + this.id + "'";
                }
                else
                {
                    sql = "INSERT INTO clasificaciones (" + this.columns + ") VALUES(" +
                        "'" + this.cod + "'," +
                        "'" + corregir_nombre( this.nombre ) + "'," +
                        "'" + this.id_actividad + "'," +
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
                        sql = $"SELECT MAX(id) as id FROM clasificaciones limit 1";
                        DataSet dsCons = con.query(sql);
                        if (dsCons.Tables[0].Rows.Count > 0)
                            this.id = dsCons.Tables[0].Rows[0]["id"].ToString();
                    }

                    Task.Run(() => {
                        ApiClasificaciones apcla = new ApiClasificaciones();
                        List<clasificaciones> lscla = new List<clasificaciones>();
                        lscla.Add(this);
                        apcla.Post(lscla);
                    });
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool versionUpdate(string cod_, int version_)
        {
            sql = "SELECT COUNT(1) as cant FROM clasificaciones WHERE cod = '" + cod_ + "' ";
            DataSet ds = con.query(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "0")
                {
                    sql = $"SELECT COUNT(1) as cant FROM clasificaciones WHERE cod = '{cod_ }' AND version < '{version_}' ";
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
            ApiClasificaciones apcla = new ApiClasificaciones();
            List<clasificaciones> lscla = await apcla.Get();
            validaciones val = new validaciones();
            if (val.dataComparacion("api_clasificaciones", lscla))
            {
                foreach (clasificaciones c in lscla)
                {
                    if (versionUpdate(c.cod, c.version))
                        c.save(true);
                }
                val.createData0("api_clasificaciones");
            }
        }

        public void delete()
        {
            this.exist();
            sql = "UPDATE clasificaciones SET codestado = 0, ultmod = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', version = '"+this.version+"' WHERE id = '" + this.id + "' ";
            con.query(sql);
        }
    }
}
