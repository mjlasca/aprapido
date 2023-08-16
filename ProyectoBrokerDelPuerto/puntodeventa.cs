using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class puntodeventa
    {
        string sql = "";

        string columns = "nombre,usuario, prefijo, apitoken, rol,user_edit,codestado,ultmod,urlapi,apiversion,codempresa,perfil,master,organizador,aseguradora,codmaster,codorganizador";
        public string id, nombre, usuario, prefijo, apitoken, rol, user_edit, codestado, ultmod, urlapi, apiversion, codempresa, perfil  = "";
        public int accesos = 0;
        public string master, organizador, aseguradora, codmaster, codorganizador;
        conexion con = new conexion();

        public puntodeventa(bool inst = false)
        {
            
            
            if (inst)
                this.install();
        }


        private void install()
        {
            /*sql = "DROP TABLE puntodeventa";
            con.query(sql);*/
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists puntodeventa (id serial PRIMARY KEY, nombre VARCHAR(200) NULL,prefijo VARCHAR(10) NULL, apitoken VARCHAR(60) NULL, usuario VARCHAR(150) NULL, rol VARCHAR(200), ultmod DATETIME NULL, " +
                " user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL, urlapi VARCHAR(500) NULL, accesos int(3) DEFAULT 0 );";
            }
            else
            {
                sql = "CREATE TABLE if not exists puntodeventa (id INTEGER PRIMARY KEY, nombre VARCHAR(200) NULL,prefijo VARCHAR(10) NULL, apitoken VARCHAR(60) NULL, usuario VARCHAR(150) NULL, rol VARCHAR(200), ultmod DATETIME NULL, " +
                " user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL, urlapi VARCHAR(500) NULL, accesos int(3) DEFAULT 0 );";
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

                    if (con.query("SHOW COLUMNS FROM puntodeventa WHERE Field = 'urlapi' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE puntodeventa ADD COLUMN urlapi VARCHAR(100) NULL;");
                    if (con.query("SHOW COLUMNS FROM puntodeventa WHERE Field = 'accesos' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE puntodeventa ADD COLUMN accesos int(3) DEFAULT 0;");
                    if (con.query("SHOW COLUMNS FROM puntodeventa WHERE Field = 'apiversion' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE puntodeventa ADD COLUMN apiversion VARCHAR(10) NULL;");
                    if (con.query("SHOW COLUMNS FROM puntodeventa WHERE Field = 'codempresa' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE puntodeventa ADD COLUMN codempresa VARCHAR(150) NULL;");
                    if (con.query("SHOW COLUMNS FROM puntodeventa WHERE Field = 'perfil' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE puntodeventa ADD COLUMN perfil VARCHAR(150) NULL;");
                    if (con.query("SHOW COLUMNS FROM puntodeventa WHERE Field = 'master' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE puntodeventa ADD COLUMN master VARCHAR(300) NULL;");
                    if (con.query("SHOW COLUMNS FROM puntodeventa WHERE Field = 'organizador' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE puntodeventa ADD COLUMN organizador VARCHAR(300) NULL;");
                    if (con.query("SHOW COLUMNS FROM puntodeventa WHERE Field = 'aseguradora' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE puntodeventa ADD COLUMN aseguradora VARCHAR(300) NULL;");
                    if (con.query("SHOW COLUMNS FROM puntodeventa WHERE Field = 'codmaster' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE puntodeventa ADD COLUMN codmaster VARCHAR(300) NULL;");
                    if (con.query("SHOW COLUMNS FROM puntodeventa WHERE Field = 'codorganizador' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE puntodeventa ADD COLUMN codorganizador VARCHAR(300) NULL;");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al agregar columna de propuesta " + ex.Message);
                }
            }
            else
            {
                try
                {
                    con.query("ALTER TABLE puntodeventa ADD COLUMN urlapi VARCHAR(100) NULL;");
                    con.query("ALTER TABLE puntodeventa ADD COLUMN accesos int(3) DEFAULT 0;");
                    con.query("ALTER TABLE puntodeventa ADD COLUMN apiversion VARCHAR(10) NULL;");
                    con.query("ALTER TABLE puntodeventa ADD COLUMN codempresa VARCHAR(150) NULL;");
                    con.query("ALTER TABLE puntodeventa ADD COLUMN perfil VARCHAR(150) NULL;");
                    con.query("ALTER TABLE puntodeventa ADD COLUMN master VARCHAR(300) NULL;");
                    con.query("ALTER TABLE puntodeventa ADD COLUMN organizador VARCHAR(300) NULL;");
                    con.query("ALTER TABLE puntodeventa ADD COLUMN aseguradora VARCHAR(300) NULL;");
                    con.query("ALTER TABLE puntodeventa ADD COLUMN codmaster VARCHAR(300) NULL;");
                    con.query("ALTER TABLE puntodeventa ADD COLUMN codorganizador VARCHAR(300) NULL;");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al agregar columna de propuesta " + ex.Message);
                }
            }

        }


        public bool exist()
        {
            sql = "SELECT usuario FROM puntodeventa WHERE usuario = '" + this.usuario + "' OR prefijo = '"+this.prefijo+"'";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }


            return false;
        }


        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM puntodeventa ORDER BY prefijo,nombre DESC";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar puntodeventa "+ex.Message);
            }

            return ds;
        }

        public bool get_colaborador()
        {
            bool res = false;

            sql = "SELECT * FROM puntodeventa WHERE rol = 'COLABORADOR' AND codestado = 1 ";
            try
            {
                DataSet ds = new DataSet();
                ds = con.query(sql);
                Console.WriteLine("SQL " + sql + " / COUNT " + ds.Tables[0].Rows.Count);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.nombre = ds.Tables[0].Rows[0]["nombre"].ToString();
                    this.usuario = ds.Tables[0].Rows[0]["usuario"].ToString();
                    this.prefijo = ds.Tables[0].Rows[0]["prefijo"].ToString();
                    this.apitoken = ds.Tables[0].Rows[0]["apitoken"].ToString();
                    this.apiversion = ds.Tables[0].Rows[0]["apiversion"].ToString();
                    this.rol = ds.Tables[0].Rows[0]["rol"].ToString();
                    this.user_edit = ds.Tables[0].Rows[0]["user_edit"].ToString();
                    this.codestado = ds.Tables[0].Rows[0]["codestado"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
                    this.urlapi = ds.Tables[0].Rows[0]["urlapi"].ToString();
                    this.codempresa = ds.Tables[0].Rows[0]["codempresa"].ToString();
                    this.perfil = ds.Tables[0].Rows[0]["perfil"].ToString();
                    this.master = ds.Tables[0].Rows[0]["master"].ToString();
                    this.organizador = ds.Tables[0].Rows[0]["organizador"].ToString();
                    this.aseguradora = ds.Tables[0].Rows[0]["aseguradora"].ToString();
                    this.codmaster = ds.Tables[0].Rows[0]["codmaster"].ToString();
                    this.codorganizador = ds.Tables[0].Rows[0]["codorganizador"].ToString();
                    res = true;
                }
                else
                {
                    res = false;
                }

            }
            catch (Exception ex)
            {
                res = false;
                Console.Write("ERROR al consultar puntodeventa "+ex.Message);
            }

            return res;
        }


        public bool get_principal()
        {
            bool res = false;

            sql = "SELECT * FROM puntodeventa WHERE rol = 'PRINCIPAL' ";
            try
            {
                
                DataSet ds = new DataSet();
                ds = con.query(sql);
                Console.WriteLine("SQL " + sql + " / COUNT " + ds.Tables[0].Rows.Count);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.nombre = ds.Tables[0].Rows[0]["nombre"].ToString();
                    this.usuario = ds.Tables[0].Rows[0]["usuario"].ToString();
                    this.prefijo = ds.Tables[0].Rows[0]["prefijo"].ToString();
                    this.apitoken = ds.Tables[0].Rows[0]["apitoken"].ToString();
                    this.apiversion = ds.Tables[0].Rows[0]["apiversion"].ToString();
                    this.rol = ds.Tables[0].Rows[0]["rol"].ToString();
                    this.user_edit = ds.Tables[0].Rows[0]["user_edit"].ToString();
                    this.codestado = ds.Tables[0].Rows[0]["codestado"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
                    this.urlapi = ds.Tables[0].Rows[0]["urlapi"].ToString();
                    this.codempresa = ds.Tables[0].Rows[0]["codempresa"].ToString();
                    this.perfil = ds.Tables[0].Rows[0]["perfil"].ToString();
                    this.master = ds.Tables[0].Rows[0]["master"].ToString();
                    this.organizador = ds.Tables[0].Rows[0]["organizador"].ToString();
                    this.aseguradora = ds.Tables[0].Rows[0]["aseguradora"].ToString();
                    this.codmaster = ds.Tables[0].Rows[0]["codmaster"].ToString();
                    this.codorganizador = ds.Tables[0].Rows[0]["codorganizador"].ToString();
                    res = true;
                }
                else
                {
                    res = false;
                }
                
            }
            catch (Exception ex)
            {
                res = false;
                Console.Write("ERROR al consultar puntodeventa "+ex.Message);
            }

            return res;
        }

        public void inhabilitarpunto(int activo = 0)
        {
            sql = "UPDATE puntodeventa SET " +
                    "codestado = '"+activo+"', apitoken = '"+this.apitoken+"'   WHERE usuario = '" + this.usuario + "' ;";

            
            con.query(sql);
        }

        public bool getaccesos()
        {
            bool res = false;

            sql = "SELECT * FROM puntodeventa WHERE id > 0 ";
            try
            {
                DataSet ds = new DataSet();
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.nombre = ds.Tables[0].Rows[0]["nombre"].ToString();
                    this.usuario = ds.Tables[0].Rows[0]["usuario"].ToString();
                    this.prefijo = ds.Tables[0].Rows[0]["prefijo"].ToString();
                    this.apitoken = ds.Tables[0].Rows[0]["apitoken"].ToString();
                    this.rol = ds.Tables[0].Rows[0]["rol"].ToString();
                    this.user_edit = ds.Tables[0].Rows[0]["user_edit"].ToString();
                    this.codestado = ds.Tables[0].Rows[0]["codestado"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
                    this.urlapi = ds.Tables[0].Rows[0]["urlapi"].ToString();
                    this.urlapi = ds.Tables[0].Rows[0]["urlapi"].ToString();
                    this.accesos = Convert.ToInt16( ds.Tables[0].Rows[0]["accesos"].ToString() );
                    this.codempresa = ds.Tables[0].Rows[0]["codempresa"].ToString();
                    this.perfil = ds.Tables[0].Rows[0]["perfil"].ToString();
                    this.master = ds.Tables[0].Rows[0]["master"].ToString();
                    this.organizador = ds.Tables[0].Rows[0]["organizador"].ToString();
                    this.aseguradora = ds.Tables[0].Rows[0]["aseguradora"].ToString();
                    this.codmaster = ds.Tables[0].Rows[0]["codmaster"].ToString();
                    this.codorganizador = ds.Tables[0].Rows[0]["codorganizador"].ToString();
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                res = false;
                Console.Write("ERROR al consultar puntodeventa "+ex.Message);
            }

            return res;
        }

        public bool actualizarmenor(string op)
        {
            bool res = false;

            try
            {

                if (op == "abierto")
                {
                    sql = "UPDATE puntodeventa SET accesos = 0 WHERE id > 0 ";
                    con.query(sql);
                }


                return true;
            }
            catch (Exception ex)
            {
                res = false;
                Console.Write("ERROR al actualizar accesos puntodeventa");
            }

            return res;
        }

        public bool actualizaraccesos(string op)
        {
            bool res = false;

            try
            {

                if (op == "abierto")
                {
                    sql = "UPDATE puntodeventa SET accesos = accesos + 1 WHERE id > 0 ";
                    con.query(sql);
                }

                if (op == "cerrado")
                {
                    sql = "UPDATE puntodeventa SET accesos = accesos - 1 WHERE id > 0 ";
                    con.query(sql);
                }

                return true;
            }
            catch (Exception ex)
            {
                res = false;
                Console.Write("ERROR al actualizar accesos puntodeventa");
            }

            return res;
        }

        public bool get(string usuario_)
        {
            bool res = false;

            sql = "SELECT * FROM puntodeventa WHERE usuario = '" + usuario_ + "' ";
            try
            {
                DataSet ds = new DataSet();
                ds = con.query(sql);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    this.nombre = ds.Tables[0].Rows[0]["nombre"].ToString();
                    this.usuario = ds.Tables[0].Rows[0]["usuario"].ToString();
                    this.prefijo = ds.Tables[0].Rows[0]["prefijo"].ToString();
                    this.apitoken = ds.Tables[0].Rows[0]["apitoken"].ToString();
                    this.rol = ds.Tables[0].Rows[0]["rol"].ToString();
                    this.user_edit= ds.Tables[0].Rows[0]["user_edit"].ToString();
                    this.codestado = ds.Tables[0].Rows[0]["codestado"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
                    this.urlapi = ds.Tables[0].Rows[0]["urlapi"].ToString();
                    this.codempresa = ds.Tables[0].Rows[0]["codempresa"].ToString();
                    this.perfil = ds.Tables[0].Rows[0]["perfil"].ToString();
                    this.master = ds.Tables[0].Rows[0]["master"].ToString();
                    this.organizador = ds.Tables[0].Rows[0]["organizador"].ToString();
                    this.aseguradora = ds.Tables[0].Rows[0]["aseguradora"].ToString();
                    this.codmaster = ds.Tables[0].Rows[0]["codmaster"].ToString();
                    this.codorganizador = ds.Tables[0].Rows[0]["codorganizador"].ToString();
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                res = false;
                Console.Write("ERROR al consultar puntodeventa "+ex.Message);
            }

            return res;
        }

        public bool update()
        {
            try
            {
               
                    sql = "UPDATE puntodeventa SET " +
                    " nombre = '" + this.nombre + "'," +
                    "usuario = '" + this.usuario + "'," +
                    "prefijo = '" + this.prefijo + "'," +
                    "apitoken = '" + this.apitoken + "'," +
                    " user_edit = '" + this.user_edit + "'," +
                    " ultmod = '" + this.ultmod + "'," +
                    "codestado = '" + this.codestado + "'," +
                    "urlapi = '" + this.urlapi + "'," +
                    "codempresa = '" + this.codempresa + "'," +
                    "master = '" + this.master + "'," +
                    "organizador = '" + this.organizador + "'," +
                    "aseguradora = '" + this.aseguradora + "'," +
                    "codorganizador = '" + this.codorganizador + "'," +
                    "codmaster = '" + this.codmaster + "'," +
                    "perfil = '" + this.perfil + "'" +
                    " WHERE usuario != '' ;";

                    con.query(sql);
                    return true;
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar la configuración");
                return false;
            }
        }

        public bool save_nube()
        {

            try
            {
                if (!this.exist())
                {
                    
                    sql = "INSERT INTO puntodeventa (" + this.columns + ") VALUES(" +
                      "'" + this.nombre + "'," +
                      "'" + this.usuario + "'," +
                      "'" + this.prefijo + "'," +
                      "'" + this.apitoken + "'," +
                      "'" + this.rol + "'," +
                      "'" + this.user_edit + "'," +
                      "'" + this.codestado + "'," +
                      "'" + this.ultmod + "'," +
                      "'" + this.urlapi + "'," +
                      "'" + this.apiversion + "'," +
                      "'" + this.codempresa + "'," +
                      "'" + this.perfil + "'," +
                      "'" + this.master + "'," +
                      "'" + this.organizador + "'," +
                      "'" + this.aseguradora + "'," +
                      "'" + this.codmaster + "'," +
                      "'" + this.codorganizador + "'" +
                      ") ";

                    con.query(sql);
                    return true;

                }
                else
                {
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar la configuración");
                return false;
            }



        }


        public bool save()
        {
            
            try
            {
                if (!this.exist())
                {
                   /* sql = "UPDATE puntodeventa SET " +
                    " nombre = '" + this.nombre + "'," +
                    "usuario = '" + this.usuario + "'," +
                    "prefijo = '" + this.prefijo+ "'," +
                    "apitoken = '" + this.apitoken+ "'," +
                    " user_edit = '" + this.user_edit + "'," +
                    " ultmod = '" + this.ultmod + "'," +
                    "codestado = '" + this.codestado + "'" +
                    " WHERE usuario = '" + usuario + "' ;";
                }
                else
                {*/
                    sql = "INSERT INTO puntodeventa (" + this.columns + ") VALUES(" +
                      "'" + this.nombre + "'," +
                      "'" + this.usuario + "'," +
                      "'" + this.prefijo+ "'," +
                      "'" + this.apitoken+ "'," +
                      "'" + this.rol+ "'," +
                      "'" + this.user_edit+ "'," +
                      "'" + this.codestado + "'," +
                      "'" + this.ultmod + "'," +
                      "'" + this.urlapi + "'," +
                      "'" + this.apiversion + "'," +
                      "'" + this.codempresa + "'," +
                      "'" + this.perfil + "'," +
                      "'" + this.master + "'," +
                      "'" + this.organizador + "'," +
                      "'" + this.aseguradora + "'," +
                      "'" + this.codmaster + "'," +
                      "'" + this.codorganizador + "'" +
                      ") ";

                    con.query(sql);
                    return true;
                        
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar la configuración");
                return false;
            }

            

        }
    }
}
