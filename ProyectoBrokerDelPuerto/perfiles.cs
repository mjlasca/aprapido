using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class perfiles
    {
        string sql = "";
        string columns = "nombre,modulo,access,vista,edicion,eliminar,exportar,codempresa,envionube";
        public string reg = "", nombre = "", modulo = "", vista = "0", edicion = "0", eliminar = "0",exportar = "0", codempresa = "", envionube = "0";
        public int access = 0;
        conexion con = new conexion();

        public perfiles(bool inst = false)
        {
            if(inst)
                this.install();
        }

        private void install()
        {
            /*sql = "DROP TABLE perfiles;";
            con.query(sql);*/
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists perfiles (reg serial PRIMARY KEY, nombre VARCHAR(150) NULL, " +
                "modulo VARCHAR(150) NULL, access INT(1) NULL, vista INT(1) NULL, edicion INT(1) NULL, eliminar INT(1) NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists perfiles (reg INTEGER PRIMARY KEY, nombre VARCHAR(150) NULL, " +
                "modulo VARCHAR(150) NULL, access INT(1) NULL, vista INT(1) NULL, edicion INT(1) NULL, eliminar INT(1) NULL );";
            }
            con.query(sql);

            this.addColumn();

            sql = "UPDATE perfiles SET vista = access, edicion = access , eliminar = access WHERE access = 1  AND (vista = 0 OR vista IS NULL)";
            con.query(sql);

            sql = "UPDATE perfiles SET codempresa = '"+MDIParent1.codempresa+"' WHERE codempresa IS NULL OR codempresa = '' ";
            con.query(sql);

            this.defaultprofile();
        }


        private void defaultprofile()
        {
            sql = "SELECT COUNT(1) as cont FROM perfiles WHERE nombre = 'productorJunior' ";
            DataSet ds = con.query(sql);
            if (ds.Tables[0].Rows[0]["cont"].ToString() == "0")
            {
                string nameprofile = "productorJunior";
                //Profile productorJunior
                this.nombre = nameprofile;
                this.modulo = "clientes";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "barrios";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "propuestas";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "cierrecajas";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "rendiciones";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "informe_findia";
                this.vista = "1";
                this.edicion = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "mail_findeldia";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "controlventas_nuevo";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "ventascredito";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();
            }

            sql = "SELECT COUNT(1) as cont FROM perfiles WHERE nombre = 'productorSenior' ";
            ds = con.query(sql);

            if (ds.Tables[0].Rows[0]["cont"].ToString() == "0")
            {

                string nameprofile = "productorSenior";
                //Profile productorSenior
                this.nombre = nameprofile;
                this.modulo = "usuarios";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "perfiles";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "actividades";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "clasificaciones";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "festivos";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "coberturas";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "confcorreo";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "migraciones";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "puntodeventa";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "clientes";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "barrios";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "propuestas";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "cierrecajas";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "rendiciones";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "cobranzas_importarclientes";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "nuevacomision";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "informe_findia";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "listacomision";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "mail_findeldia";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "controlventas_nuevo";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "controlventas_informe";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "ventascredito";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

            }

            sql = "SELECT COUNT(1) as cont FROM perfiles WHERE nombre = 'Master-Organizador-Productor' ";
            ds = con.query(sql);

            if (ds.Tables[0].Rows[0]["cont"].ToString() == "0")
            {

                string nameprofile = "Master-Organizador-Productor";
                //Profile Master-Organizador-Productor
                this.nombre = nameprofile;
                this.modulo = "usuarios";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1  ";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "perfiles";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "actividades";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "clasificaciones";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "festivos";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "coberturas";
                this.vista = "1";
                this.edicion = "0";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "confcorreo";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "migraciones";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "puntodeventa";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "clientes";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "barrios";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "propuestas";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "cierrecajas";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "rendiciones";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "cobranzas_importarclientes";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "nuevacomision";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();


                this.nombre = nameprofile;
                this.modulo = "listacomision";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();



                this.nombre = nameprofile;
                this.modulo = "informe_findia";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "mail_findeldia";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "controlventas_nuevo";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "0";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "controlventas_informe";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "ventascredito";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "0";
                this.save();

            }

            sql = "SELECT COUNT(1) as cont FROM perfiles WHERE nombre = 'adminpunto' ";
            ds = con.query(sql);

            if (ds.Tables[0].Rows[0]["cont"].ToString() == "0")
            {

                string nameprofile = "adminpunto";
                //Profile adminpunto
                this.nombre = nameprofile;
                this.modulo = "usuarios";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "perfiles";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "actividades";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "clasificaciones";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "festivos";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "coberturas";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "confcorreo";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "migraciones";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "listacomision";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "combinar_informes";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "puntodeventa";
                this.vista = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "clientes";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "barrios";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "propuestas";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "cierrecajas";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "rendiciones";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "cobranzas_importarclientes";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "nuevacomision";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "informe_findia";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "mail_findeldia";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "controlventas_nuevo";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "controlventas_informe";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

                this.nombre = nameprofile;
                this.modulo = "ventascredito";
                this.vista = "1";
                this.edicion = "1";
                this.eliminar = "1";
                this.exportar = "1";
                this.save();

            }

        }

        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                try
                {
                    //Add column categoria if NOT exist
                    if (con.query("SHOW COLUMNS FROM perfiles WHERE Field = 'vista' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE perfiles ADD COLUMN vista int(1) NULL;");
                    if (con.query("SHOW COLUMNS FROM perfiles WHERE Field = 'edicion' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE perfiles ADD COLUMN edicion int(1) NULL;");
                    if (con.query("SHOW COLUMNS FROM perfiles WHERE Field = 'eliminar' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE perfiles ADD COLUMN eliminar int(1) NULL;");
                    if (con.query("SHOW COLUMNS FROM perfiles WHERE Field = 'exportar' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE perfiles ADD COLUMN exportar int(1) DEFAULT 0;");
                    if (con.query("SHOW COLUMNS FROM perfiles WHERE Field = 'codempresa' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE perfiles ADD COLUMN codempresa VARCHAR(150) NULL;");
                    if (con.query("SHOW COLUMNS FROM perfiles WHERE Field = 'envionube' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE perfiles ADD COLUMN envionube INT(1) DEFAULT 0;");

                }
                catch
                {
                    //
                }
            }
            else
            {
                con.query("ALTER TABLE perfiles ADD COLUMN vista int(1) NULL;");
                con.query("ALTER TABLE perfiles ADD COLUMN edicion int(1) NULL;");
                con.query("ALTER TABLE perfiles ADD COLUMN eliminar int(1) NULL;");
                con.query("ALTER TABLE perfiles ADD COLUMN exportar int(1) DEFAULT 0;");
                con.query("ALTER TABLE perfiles ADD COLUMN codempresa VARCHAR(150) NULL;");
                con.query("ALTER TABLE perfiles ADD COLUMN envionube INT(1) DEFAULT 0;");
            }

        }

        public bool exist()
        {

            sql = "SELECT * FROM perfiles WHERE nombre = '"+this.nombre+"' AND modulo = '"+ this.modulo + "' ";
            DataSet ds = con.query(sql);
            if (ds.Tables[0].Rows.Count > 0)
                return true;


            return false;
        }

        public DataSet get()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM perfiles WHERE nombre = '"+this.nombre+"' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar perfiles " + ex.Message);
            }

            return ds;
        }

        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM perfiles GROUP BY nombre  ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar perfiles "+ex.Message);
            }

            return ds;
        }


      
        public void borrarRegistros()
        {
            sql = "DELETE FROM perfiles";
            con.query(sql);
        }

        public DataSet get_all_fecha(string fecha)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM perfiles ";
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

        public DataSet getEnvioNube()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM perfiles WHERE envionube = 0 ";
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



        public bool save()
        {
            try
            {
                if (!this.exist())
                {
                    if(this.vista == "1" || this.edicion == "1" || this.eliminar == "1")
                    {
                        sql = "INSERT INTO perfiles (" + this.columns + ") VALUES(" +
                        "'" + this.nombre + "'," +
                        "'" + this.modulo + "'," +
                        "'" + this.access + "'," +
                        "'" + this.vista + "'," +
                        "'" + this.edicion + "'," +
                        "'" + this.eliminar + "'," +
                        "'" + this.exportar + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + this.envionube + "'" +
                        ") ;";

                        



                        con.query(sql);
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    
                }
                else
                {
                    return false;
                }
                

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public string concat_sql()
        {
            try
            {
                
                    if (this.vista == "1" || this.edicion == "1" || this.eliminar == "1")
                    {
                        sql = " (" +
                        "'" + this.nombre + "'," +
                        "'" + this.modulo + "'," +
                        "'" + this.access + "'," +
                        "'" + this.vista + "'," +
                        "'" + this.edicion + "'," +
                        "'" + this.eliminar + "'," +
                        "'" + this.exportar + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'1'" +
                        ") ";


                        return sql;
                    }
                    else
                    {
                        return sql;
                    }

            }
            catch (Exception ex)
            {
                return sql;
            }

        }


        public void save_concat_sql(string query_)
        {
            con.query("INSERT INTO perfiles (" + this.columns + ") VALUES "+query_);
        }

        public void envioHecho()
        {
            con.query("UPDATE perfiles SET envionube = 1 WHERE envionube = 0 ");
        }


        public void delete_modulo(string _modulo)
        {
            sql = "DELETE FROM perfiles WHERE  modulo = '"+_modulo+"' ";
            con.query(sql);
        }

        public void delete()
        {
            sql = "DELETE FROM perfiles WHERE nombre = '" + this.nombre + "' ";
            con.query(sql);
        }

    }
}
