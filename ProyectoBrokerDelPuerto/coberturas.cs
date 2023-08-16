using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class coberturas
    {
        string sql = "";
        string columns = "nombre,suma,gastos,deducible,vrMensual,vrTrimestral,vrSemestral,x21,x32,x64,ultmod,user_edit,codestado,version";
        public string reg, nombre, suma, gastos, deducible, vrMensual, vrTrimestral, vrSemestral, x21, x32, x64, ultmod, user_edit, codestado = "";
        public int version { set; get; } = 0;
        conexion con = new conexion();

        public coberturas(bool inst = false)
        {
            if(inst)
                this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists coberturas (reg serial PRIMARY KEY, nombre VARCHAR(100) NULL, suma DOUBLE NULL,  gastos DOUBLE NULL,  deducible DOUBLE NULL,  vrMensual DOUBLE NULL, " +
                "  vrTrimestral DOUBLE NULL,  vrSemestral DOUBLE NULL,  x21 DOUBLE NULL,   x32 DOUBLE NULL,   x64 DOUBLE NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists coberturas (reg INTEGER PRIMARY KEY, nombre VARCHAR(100) NULL, suma DOUBLE NULL,  gastos DOUBLE NULL,  deducible DOUBLE NULL,  vrMensual DOUBLE NULL, " +
                "  vrTrimestral DOUBLE NULL,  vrSemestral DOUBLE NULL,  x21 DOUBLE NULL,   x32 DOUBLE NULL,   x64 DOUBLE NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
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
                    if (con.query("SHOW COLUMNS FROM coberturas WHERE Field = 'version' ").Tables[0].Rows.Count == 0)
                    {
                        con.query("ALTER TABLE coberturas ADD COLUMN version INT(11) DEFAULT 0");
                    }
                    
                }
                catch
                {
                    //
                }
            }
            else
            {
                con.query("ALTER TABLE coberturas ADD COLUMN version INT(11) DEFAULT 0");
            }


        }

        public bool exist()
        {
            sql = "SELECT nombre,version FROM coberturas WHERE nombre = '" + this.nombre.Trim() + "'";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                if (this.version < 2)
                    this.version = Convert.ToInt16(con.query(sql).Tables[0].Rows[0]["version"].ToString()) + 1;
                return true;
            }
            return false;
        }

        public bool exist_vista()
        {
            sql = "SELECT nombre FROM coberturas WHERE nombre = '" + this.nombre + "' AND codestado = '1'";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }


            return false;
        }

        public DataSet get_all_fecha(string fecha)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM coberturas  WHERE ultmod > '" + fecha + "' AND ultmod <= '"
                + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar coberturas");
            }

            return ds;
        }

        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM coberturas WHERE codestado = 1";
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

        public DataSet get_reg(string reg_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM coberturas WHERE codestado = 1 AND reg = '" + reg_ + "'";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar coberturas");
            }

            return ds;
        }

        public DataSet get(string nombre_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM coberturas WHERE codestado = 1 AND nombre = '"+ nombre_ + "'";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar coberturas");
            }

            return ds;
        }


        public DataSet get_all_busqueda(string coincidencia)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM coberturas WHERE codestado = 1 AND " +
                " ( nombre LIKE  '%" + coincidencia + "%' )";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar coberturas");
            }

            return ds;
        }

        public void borrarRegistros()
        {
            sql = "DELETE FROM coberturas";
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
                    if (this.codestado != "")
                    {
                        sql = "UPDATE coberturas SET " +
                        "suma = '" + this.suma + "'," +
                        "gastos = '" + this.gastos + "'," +
                        "deducible = '" + this.deducible + "'," +
                        "vrMensual = '" + this.vrMensual + "'," +
                        "vrTrimestral = '" + this.vrTrimestral + "'," +
                        "vrSemestral = '" + this.vrSemestral + "'," +
                        "x21 = '" + this.x21 + "'," +
                        "x32 = '" + this.x32 + "'," +
                        "x64 = '" + this.x64 + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "user_edit = '" + this.user_edit + "'," +
                        "codestado = '" + this.codestado + "'" +
                        " WHERE nombre = '" + this.nombre.Trim() + "'; ";
                    }
                    else
                    {
                        sql = "UPDATE coberturas SET " +
                        "suma = '" + this.suma + "'," +
                        "gastos = '" + this.gastos + "'," +
                        "deducible = '" + this.deducible + "'," +
                        "vrMensual = '" + this.vrMensual + "'," +
                        "vrTrimestral = '" + this.vrTrimestral + "'," +
                        "vrSemestral = '" + this.vrSemestral + "'," +
                        "x21 = '" + this.x21 + "'," +
                        "x32 = '" + this.x32 + "'," +
                        "x64 = '" + this.x64 + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "user_edit = '" + this.user_edit + "'," +
                        "codestado = '1'" +
                        " WHERE nombre = '" + this.nombre.Trim() + "'; ";
                    }

                }
                else
                {
                    sql = "INSERT INTO coberturas (" + this.columns + ") VALUES(" +
                        "'" + this.nombre.Trim() + "'," +
                        "'" + this.suma + "'," +
                        "'" + this.gastos + "'," +
                        "'" + this.deducible + "'," +
                        "'" + this.vrMensual + "'," +
                        "'" + this.vrTrimestral + "'," +
                        "'" + this.vrSemestral + "'," +
                        "'" + this.x21 + "'," +
                        "'" + this.x32 + "'," +
                        "'" + this.x64 + "'," +
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
                    if(this.codestado != "")
                    {
                        sql = "UPDATE coberturas SET " +
                        "suma = '" + this.suma + "'," +
                        "gastos = '" + this.gastos + "'," +
                        "deducible = '" + this.deducible + "'," +
                        "vrMensual = '" + this.vrMensual + "'," +
                        "vrTrimestral = '" + this.vrTrimestral + "'," +
                        "vrSemestral = '" + this.vrSemestral + "'," +
                        "x21 = '" + this.x21 + "'," +
                        "x32 = '" + this.x32 + "'," +
                        "x64 = '" + this.x64 + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "user_edit = '" + this.user_edit + "'," +
                        "version = '" + this.version + "'," +
                        "codestado = '" +this.codestado+"'" +
                        " WHERE nombre = '" + this.nombre.Trim() + "'";
                    }
                    else
                    {
                        sql = "UPDATE coberturas SET " +
                        "suma = '" + this.suma + "'," +
                        "gastos = '" + this.gastos + "'," +
                        "deducible = '" + this.deducible + "'," +
                        "vrMensual = '" + this.vrMensual + "'," +
                        "vrTrimestral = '" + this.vrTrimestral + "'," +
                        "vrSemestral = '" + this.vrSemestral + "'," +
                        "x21 = '" + this.x21 + "'," +
                        "x32 = '" + this.x32 + "'," +
                        "x64 = '" + this.x64 + "'," +
                        "ultmod = '" + this.ultmod + "'," +
                        "user_edit = '" + this.user_edit + "'," +
                        "codestado = '1'" +
                        " WHERE nombre = '" + this.nombre.Trim() + "'";
                    }
                    
                }
                else
                {
                    sql = "INSERT INTO coberturas (" + this.columns + ") VALUES(" +
                        "'" + this.nombre.Trim() + "'," +
                        "'" + this.suma + "'," +
                        "'" + this.gastos + "'," +
                        "'" + this.deducible + "'," +
                        "'" + this.vrMensual + "'," +
                        "'" + this.vrTrimestral + "'," +
                        "'" + this.vrSemestral + "'," +
                        "'" + this.x21 + "'," +
                        "'" + this.x32 + "'," +
                        "'" + this.x64 + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.version + "'" +
                        ") ";
                }



                

                con.query(sql);

                if (importCloud == false)
                {
                    if(this.reg == null || this.reg == "")
                    {
                        sql = $"SELECT reg FROM coberturas where nombre = '{this.nombre.Trim()}' limit 1";
                        DataSet dsCons = con.query(sql);
                        if (dsCons.Tables[0].Rows.Count > 0)
                            this.reg = dsCons.Tables[0].Rows[0]["reg"].ToString();
                    }
                    

                    Task.Run(() => {
                        ApiCoberturas apcob = new ApiCoberturas();
                        List<coberturas> lscob = new List<coberturas>();
                        lscob.Add(this);
                        apcob.Post(lscob);
                    });
                }
                
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool versionUpdate(string nombre_, int version_)
        {
            sql = "SELECT COUNT(1) as cant FROM coberturas WHERE nombre = '" + nombre_ + "' ";
            DataSet ds = con.query(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "0")
                {
                    sql = $"SELECT COUNT(1) as cant FROM coberturas WHERE nombre = '{nombre_}' AND version < '{version_}' ";
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
            ApiCoberturas apcob = new ApiCoberturas();
            List<coberturas> lscob = await apcob.Get();
            validaciones val = new validaciones();
            if (val.dataComparacion("api_coberturas", lscob))
            {
                foreach (coberturas c in lscob)
                {
                    if (versionUpdate(c.nombre, c.version))
                        c.save(true);
                }
                val.createData0("api_coberturas");
            }
        }

        public void delete()
        {
            this.exist();
            sql = "UPDATE coberturas SET codestado = 0, ultmod = '"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"', version = '"+this.version+"' WHERE reg = '" + this.reg + "' ";
            con.query(sql);
        }
    }
}
