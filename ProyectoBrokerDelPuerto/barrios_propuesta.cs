using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SQLite;

namespace ProyectoBrokerDelPuerto
{
    class barrios_propuesta
    {
        string sql = "";
        string columns = "id_propuesta,id_barrio,nombre,ultmod,user_edit,codestado, prefijo, idprefijo, codempresa";
        public string id, id_propuesta, id_barrio, nombre, ultmod, user_edit, codestado, prefijo, idprefijo, codempresa = "";
        conexion con = new conexion();
        public string sumamuerte { get; set; }
        public string sumagm { get; set; }
        public string email { get; set; }

        public barrios_propuesta(bool inst = false)
        {
            if(inst)
                this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists barrios_propuesta (id serial PRIMARY KEY,  id_propuesta INT(15) NULL, id_barrio VARCHAR(50) NULL, nombre VARCHAR(1500) NULL, " +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }
            else {
                sql = "CREATE TABLE if not exists barrios_propuesta (id INTEGER PRIMARY KEY,  id_propuesta INT(15) NULL, id_barrio VARCHAR(50) NULL, nombre VARCHAR(1500) NULL, " +
                " ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }
            con.query(sql);
            this.addColumn();
        }

        private void addColumn()
        {
            try
            {
                if (MDIParent1.baseDatos == "MySql")
                {
                    //Add column categoria if NOT exist
                    if (con.query("Select CHARACTER_MAXIMUM_LENGTH as longitud from information_schema.columns WHERE TABLE_NAME='barrios_propuesta' AND COLUMN_NAME='nombre' ").Tables[0].Rows[0]["longitud"].ToString() == "100")
                    {
                        con.query("ALTER TABLE barrios_propuesta MODIFY nombre VARCHAR(1500) NULL;");
                    }
                    if (con.query("SHOW COLUMNS FROM barrios_propuesta WHERE Field = 'prefijo' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE barrios_propuesta ADD COLUMN prefijo VARCHAR(50);");
                    if (con.query("SHOW COLUMNS FROM barrios_propuesta WHERE Field = 'idprefijo' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE barrios_propuesta ADD COLUMN idprefijo DOUBLE NOT NULL;");
                    if (con.query("SHOW COLUMNS FROM barrios_propuesta WHERE Field = 'codempresa' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE barrios_propuesta ADD COLUMN codempresa VARCHAR(150) NULL;");
                }
                else
                {
                    con.query("ALTER TABLE barrios_propuesta MODIFY nombre VARCHAR(1500) NULL;");
                    con.query("ALTER TABLE barrios_propuesta ADD COLUMN prefijo VARCHAR(50);");
                    con.query("ALTER TABLE barrios_propuesta ADD COLUMN idprefijo DOUBLE  NULL;");
                    con.query("ALTER TABLE barrios_propuesta ADD COLUMN codempresa VARCHAR(150) NULL;");
                }
            }
            catch
            {
                //
            }
            

        }


        public DataSet get_idpropuesta(string idpropuesta_, string prefijo_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM barrios_propuesta WHERE idprefijo = '" + idpropuesta_ + "' AND prefijo = '"+prefijo_+"' ";
            try
            {
                Console.Write("\n\n" + sql + "\n\n");
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar barrios_propuesta");
            }

            return ds;
        }

        public string consecutivo()
        {
            DataSet ds = new DataSet();

            sql = "SELECT MAX(id) as conse FROM barrios_propuesta ";
            try
            {
                ds = con.query(sql);
                if(ds.Tables.Count > 0)
                {
                    if(ds.Tables[0].Rows.Count > 0)
                    {

                        return ( Convert.ToInt16( ds.Tables[0].Rows[0]["conse"].ToString() ) + 1).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return "0";
            }

            return "0";
        }


        public DataSet get_idprefijo(string idPropuesta_, string prefijo_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM barrios_propuesta WHERE id_propuesta= '" + idPropuesta_ + "' AND prefijo = '" + prefijo_ + "' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar barrios_propuesta");
            }

            return ds;
        }

        public DataSet get_all_fecha()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM barrios_propuesta WHERE codestado = 1";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar barrios_propuesta");
            }

            return ds;
        }

        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM barrios_propuesta WHERE codestado = 1";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar barrios_propuesta");
            }

            return ds;
        }


        public DataSet get_all_busqueda(string coincidencia)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM barrios_propuesta WHERE codestado = 1 AND " +
                " ( nombre LIKE  '%" + coincidencia + "%' OR id LIKE  '%" + coincidencia + "%' )";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar barrios_propuesta");
            }

            return ds;
        }

        public void borrarRegistros()
        {
            sql = "DELETE FROM barrios_propuesta";
            con.query(sql);
        }

        private bool exist()
        {
            sql = "SEECT COUNT(1) as cant FROM barrios_propuesta WHERE  prefijo = '" + this.prefijo + "' AND id_propuesta = '" + this.id_propuesta + "'";

            if (con.query(sql).Tables[0].Rows[0]["cant"].ToString() != "0")
                return true;

            return false;
        }


        public bool save_nube()
        {
            try
            {
                if (!this.exist())
                {
                    sql = "INSERT INTO barrios_propuesta (" + this.columns + ") VALUES(" +
                    "'" + this.id_propuesta + "'," +
                    "'" + this.id_barrio + "'," +
                    "'" + this.nombre + "'," +
                    "'" + this.ultmod + "'," +
                    "'" + this.user_edit + "'," +
                    "'" + this.codestado + "'," +
                    "'" + this.prefijo + "'," +
                    "'" + this.idprefijo + "'," +
                    "'" + MDIParent1.codempresa + "'" +
                    ") ";

                    con.query(sql);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public bool save()
        {
            try
            {
                sql = "INSERT INTO barrios_propuesta (" + this.columns + ") VALUES(" +
                    "'" + this.id_propuesta + "'," +
                    "'" + this.id_barrio + "'," +
                    "'" + this.nombre + "'," +
                    "'" + this.ultmod + "'," +
                    "'" + this.user_edit + "'," +
                    "'" + this.codestado + "'," +
                    "'" + this.prefijo + "'," +
                    "'" + this.idprefijo + "'," +
                    "'" + MDIParent1.codempresa + "'" +
                    ") ";

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public bool save_insert_masive(string sql1)
        {
            try
            {
                sql = "INSERT INTO barrios_propuesta (" + this.columns + ") VALUES "+ sql1 ;

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public bool delete_colletion(List<barrios_propuesta> lisBaPro)
        {
            

            if (MDIParent1.baseDatos == "MySql") {
                foreach (barrios_propuesta ba in lisBaPro)
                {

                    con.conecction.Execute("DELETE FROM barrios_propuesta WHERE idprefijo = @id_propuesta AND prefijo = @prefijo ",
                            new
                            {
                                ba.id_propuesta,
                                ba.prefijo
                            }
                    );
                }
            }else
            {
                con.conectarDBSQLITE();

                foreach (barrios_propuesta ba in lisBaPro)
                {
                    con.connectionSQLite.Execute("DELETE FROM barrios_propuesta WHERE id_propuesta = @id_propuesta AND prefijo = @prefijo ",
                            new
                            {
                                ba.id_propuesta,
                                ba.prefijo
                            }
                    );
                }

            }
            

            return true;
        }

        public bool insert_colletion(List<barrios_propuesta> lisBaPro)
        {
            
            if (MDIParent1.baseDatos == "MySql")
            {
                foreach (barrios_propuesta ba in lisBaPro)
                {
                    con.conecction.Execute("INSERT INTO barrios_propuesta (" + this.columns +
                        ") VALUES(@id_propuesta,@id_barrio,@nombre,@ultmod,@user_edit,@codestado,@prefijo,@idprefijo,@codempresa) ",
                            new
                            {
                                ba.id_propuesta,
                                ba.id_barrio,
                                ba.nombre,
                                ba.ultmod,
                                ba.user_edit,
                                ba.codestado,
                                ba.prefijo,
                                ba.idprefijo,
                                ba.codempresa
                            }
                    );
                }
            }else
            {
                con.conectarDBSQLITE();

                foreach (barrios_propuesta ba in lisBaPro)
                {
                    con.connectionSQLite.Execute("INSERT INTO barrios_propuesta (" + this.columns +
                        ") VALUES(@id_propuesta,@id_barrio,@nombre,@ultmod,@user_edit,@codestado,@prefijo,@idprefijo,@codempresa) ",
                            new
                            {
                                ba.id_propuesta,
                                ba.id_barrio,
                                ba.nombre,
                                ba.ultmod,
                                ba.user_edit,
                                ba.codestado,
                                ba.prefijo,
                                ba.idprefijo,
                                ba.codempresa
                            }
                    );
                }
            }

            return true;
        }

        public string concatc_insert()
        {
                sql = " (" +
                    "'" + this.id_propuesta + "'," +
                    "'" + this.id_barrio + "'," +
                    "'" + this.nombre + "'," +
                    "'" + this.ultmod + "'," +
                    "'" + this.user_edit + "'," +
                    "'" + this.codestado + "'," +
                    "'" + this.prefijo + "'," +
                    "'" + this.idprefijo + "'," +
                    "'" + MDIParent1.codempresa + "'" +
                    ") ";

            return sql;

        }

        public void delete_idpropuesta(string idprefijo_, string prefijo_)
        {
            sql = "DELETE FROM barrios_propuesta WHERE idprefijo = '" + idprefijo_ + "' AND prefijo = '"+prefijo_+"'  ";
            
            con.query(sql);
        }

        public void delete()
        {
            sql = "UPDATE barrios_propuesta SET codestado = 0 WHERE id = '" + this.id + "' ";
            con.query(sql);
        }
    }
}
