using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class controlventas
    {
        string sql = "";
        string columns = "fecha,propuesta,codgrupo,referencia,tomador,titular,cliente, primaemitida, premioemitido, notacredito,ultmod,user_edit,codestado,prefijo";
        public string reg, fecha, propuesta, codgrupo, referencia, tomador, titular, cliente, primaemitida, premioemitido, notacredito, ultmod, user_edit, codestado = "";
        conexion con = new conexion();

        public controlventas(bool inst = false)
        {
            if(inst)
                this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists controlventas (reg serial PRIMARY KEY, fecha DATE NULL,propuesta VARCHAR(150) NULL, codgrupo VARCHAR(100) NULL, referencia VARCHAR(150) NULL, tomador VARCHAR(100) NULL," +
                    "titular VARCHAR(100) NULL, cliente VARCHAR(100) NULL, primaemitida double NULL, premioemitido double NULL, " +
                    " notacredito VARCHAR(10) NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";

            }
            else
            {
                sql = "CREATE TABLE if not exists controlventas (reg INTEGER PRIMARY KEY, fecha DATE NULL,propuesta VARCHAR(150) NULL, codgrupo VARCHAR(100) NULL, referencia VARCHAR(150) NULL, tomador VARCHAR(100) NULL," +
                    "titular VARCHAR(100) NULL, cliente VARCHAR(100) NULL, primaemitida double NULL, premioemitido double NULL, " +
                    " notacredito VARCHAR(10) NULL, ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL );";
            }

                

            con.query(sql);

            this.addColumn();

            puntodeventa punt = new puntodeventa();
            if (punt.get_principal() || punt.get_colaborador())
            {
                if (MDIParent1.baseDatos == "MySql")
                {
                    sql = "UPDATE propuestas t1, controlventas t2 SET  t1.referencia = t2.referencia, t1.prima = t2.primaemitida WHERE " +
                    " CONCAT( t1.prefijo,t1.idpropuesta ) = t2.codgrupo AND t1.referencia IS NULL ";
                    con.query(sql);

                    sql = "UPDATE controlventas  SET  codgrupo = CONCAT('" + punt.prefijo + "',codgrupo), prefijo = '" + punt.prefijo + "' WHERE " +
                        "   prefijo IS NULL ";
                    con.query(sql);
                }
                else
                {
                    sql = "UPDATE propuestas t1, controlventas t2 SET  t1.referencia = t2.referencia, t1.prima = t2.primaemitida WHERE " +
                    " ( t1.prefijo || t1.idpropuesta ) = t2.codgrupo AND t1.referencia IS NULL ";
                    con.query(sql);

                    sql = "UPDATE controlventas  SET  codgrupo = ('" + punt.prefijo + "' || codgrupo), prefijo = '" + punt.prefijo + "' WHERE " +
                        "   prefijo IS NULL ";
                    con.query(sql);
                }
                
            }

            
        }

        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                try
                {
                    //Add column categoria if NOT exist
                    if (con.query("SHOW COLUMNS FROM controlventas WHERE Field = 'prefijo' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE controlventas ADD COLUMN prefijo VARCHAR(10) NULL;");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al agregar columna de controlventas " + ex.Message);
                }
            }
            else
            {
                try
                {
                    con.query("ALTER TABLE controlventas ADD COLUMN prefijo VARCHAR(10) NULL;");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al agregar columna de controlventas " + ex.Message);
                }
            }

        }

        private void modificarcolumna()
        {
            sql = "ALTER TABLE propuestas MODIFY nombre_columna DATE NOT NULL;";
            if (MDIParent1.baseDatos == "MySql")
            {
                try
                {
                    //Add column categoria if NOT exist
                    if (con.query("SHOW COLUMNS FROM propuestas WHERE Field = 'cobertura_suma' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE propuestas ADD COLUMN cobertura_suma double;");
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
                    con.query("ALTER TABLE propuestas ADD COLUMN cobertura_suma double;");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al agregar columna de propuesta " + ex.Message);
                }
            }
        }

        public bool exist()
        {
            sql = "SELECT reg FROM controlventas WHERE reg = '" + this.reg + "'";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        



        public bool validar_id()
        {
            sql = "SELECT reg FROM controlventas WHERE reg = '" + this.reg + "' AND codestado = '1'  ";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }


            return false;
        }

        public bool get_date(string fecha_)
        {
            sql = "SELECT fecha FROM controlventas WHERE fecha = '" + fecha_ +"'";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }

            return false;
        }


        public DataSet get_all_fecha(string fecha1_, string fecha2_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT DATE(fecha) as fecha, COUNT(*)  as cantemisiones, SUM(primaemitida) as primaemitida, SUM(premioemitido) as premioemitido,  SUM(notacredito) as notacredito, COUNT(notacredito) as contnota FROM controlventas WHERE  fecha BETWEEN '" + fecha1_ + "' AND '" + fecha2_ + "' GROUP BY fecha ORDER BY fecha ASC";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar controlventas");
            }

            return ds;
        }


        public DataSet get_all_fecha_registros(string fecha1_, string fecha2_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM controlventas WHERE  fecha >= '" + fecha1_ + "' <= '" + fecha2_ + "' ORDER BY fecha ASC";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar controlventas");
            }

            return ds;
        }

        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM controlventas WHERE codestado = 1";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar controlventas");
            }

            return ds;
        }

        public DataSet get(string reg_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM controlventas WHERE codestado = 1 AND reg = '" + reg_ + "' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar controlventas");
            }

            return ds;
        }


    


        public void borrarRegistros()
        {
            sql = "DELETE FROM controlventas";
            con.query(sql);
        }





        public string get_prefijo_propuesta(string codgrupo_)
        {
            string pref = "";
            puntodeventa punt = new puntodeventa();
            if (punt.get_principal() || punt.get_colaborador())
            {
                pref = punt.prefijo;
            }

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "SELECT prefijo FROM propuestas WHERE CONCAT(prefijo,idpropuesta) = '" + codgrupo_ + "'";
            }
            else
            {
                sql = "SELECT prefijo FROM propuestas WHERE (prefijo||idpropuesta|| '.0') = '" + codgrupo_ + "'";
            }

            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                pref = con.query(sql).Tables[0].Rows[0]["prefijo"].ToString();
            }

            return pref;
        }

        public bool save()
        {
            try
            {
                
                    sql = "INSERT INTO controlventas (" + this.columns + ") VALUES(" +
                        "'" + this.fecha + "'," +
                        "'" + this.propuesta + "'," +
                        "'" + this.codgrupo + "'," +
                        "'" + this.referencia + "'," +
                        "'" + this.tomador + "'," +
                        "'" + this.titular + "'," +
                        "'" + this.cliente + "'," +
                        "'" + this.primaemitida + "'," +
                        "'" + this.premioemitido + "'," +
                        "'" + this.notacredito + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.user_edit + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.get_prefijo_propuesta(this.codgrupo) + "'" +
                        ") ";


                Console.WriteLine("SQL CONTROL VENTAS \n "+sql);
                
                con.query(sql);
                
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public void delete()
        {
            sql = "UPDATE controlventas SET codestado = 0 WHERE id = '" + this.reg + "' ";
            con.query(sql);
        }

        public void delete_date(string fech)
        {
            sql = "DELETE FROM controlventas WHERE fecha = '" + fech + "' ";
            con.query(sql);
        }

    }
}
