using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{

    /*
    Se generará registros de los informes que descarguen para hacer una auditoría de ser necesrio
    el tipo de informe o tipo_informe será 1 : Informe fin del día, 2 : informe individual, 3 : informe colectivo
    */
    class informes
    {

        string sql = "";
        
        string columns = "tipo_informe, ruta, ultmod,user_edit,codestado,nomtipo,diacierre";
        public string id, tipo_informe, ruta, ultmod, user_edit, codestado = "", nomtipo = "", diacierre = "";
        conexion con = new conexion();

        public informes()
        {
            this.install();
        }


        private void install()
        {
            /*sql = "DROP TABLE informes";
            con.query(sql);*/

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists informes (id serial PRIMARY KEY, tipo_informe INT(2) NULL, ultmod DATETIME NULL, " +
                "ruta VARCHAR(200) NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL,nomtipo VARCHAR(150) NULL,diacierre DATE NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists informes (id INTEGER PRIMARY KEY, tipo_informe INT(2) NULL, ultmod DATETIME NULL, " +
                "ruta VARCHAR(200) NULL, user_edit VARCHAR(100) NULL, codestado VARCHAR(2) NULL,nomtipo VARCHAR(150) NULL,diacierre DATE NULL );";
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
                    if (con.query("SHOW COLUMNS FROM informes WHERE Field = 'nomtipo' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE informes ADD COLUMN nomtipo VARCHAR(150) NULL;");
                    if (con.query("SHOW COLUMNS FROM informes WHERE Field = 'diacierre' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE informes ADD COLUMN diacierre DATE NULL;");
                    if (con.query("SHOW COLUMNS FROM informes WHERE Field = 'tipo_informe' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE informes ADD COLUMN tipo_informe int(2) NULL;");
                    if (con.query("SHOW COLUMNS FROM informes WHERE Field = 'ruta' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE informes ADD COLUMN ruta VARCHAR(350) NULL;");
                    if (con.query("SHOW COLUMNS FROM informes WHERE Field = 'codestado' ").Tables[0].Rows.Count == 0)
                        con.query("ALTER TABLE informes ADD COLUMN codestado VARCHAR(2) NULL;");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al agregar columna de informes " + ex.Message);
                }
            }
            else
            {
                try
                {
                    con.query("ALTER TABLE informes ADD COLUMN nomtipo VARCHAR(150) NULL;");
                    con.query("ALTER TABLE informes ADD COLUMN diacierre DATE;");
                    con.query("ALTER TABLE informes ADD COLUMN tipo_informe int(2) NULL;");
                    con.query("ALTER TABLE informes ADD COLUMN ruta VARCHAR(350) NULL;");
                    con.query("ALTER TABLE informes ADD COLUMN codestado VARCHAR(2) NULL;");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al agregar columna de informes " + ex.Message);
                }
            }

        }

        public bool exist()
        {
            sql = "SELECT id FROM informes WHERE diacierre = '" + this.diacierre + "' AND nomtipo = '"+this.nomtipo+"' ";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }


            return false;
        }


        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM informes WHERE codestado = 1";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar informes");
            }

            return ds;
        }


        public DataSet get_tipo_informe(string tipo_, string fecha_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM informes WHERE nomtipo = '" + tipo_ + "'  AND DATE(diacierre) = DATE('" + fecha_ + "') ORDER BY id DESC LIMIT 1 ";
            Console.WriteLine("---> "+sql);
            
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar informes");
            }

            return ds;
        }

        public DataSet get_informeentre(DateTime fec1, DateTime fec2)
        {
            DataSet ds = new DataSet();

            sql = "SELECT DATE(t1.ultmod) as ultmod FROM propuestas t1 INNER JOIN informes t2 ON t1.codestado > 0 AND DATE(diacierre) != DATE(t1.fecha_paga)  AND  DATE(t1.fecha_paga) > DATE('" + fec1.ToString("yyyy-MM-dd")+
                "') AND DATE(t1.fecha_paga) < DATE('" + fec2.ToString("yyyy-MM-dd") + "') GROUP BY DATE(t1.fecha_paga) ";
            Console.WriteLine("--> "+sql);
            ds = con.query(sql);
            return ds;

        }

        public DateTime get_ultdia()
        {
            DataSet ds = new DataSet();

            DateTime fechita = DateTime.Now ;
                
            sql = "SELECT MAX(diacierre) as diacierre FROM informes";


            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count> 0)
                {
                    if(ds.Tables[0].Rows[0]["diacierre"] != null)
                    {
                        if (ds.Tables[0].Rows[0]["diacierre"].ToString() != "")
                        {
                            fechita = Convert.ToDateTime(ds.Tables[0].Rows[0]["diacierre"].ToString());
                        }
                            
                    }

                }
                else
                {
                    sql = "SELECT date(ultmod) as diacierre FROM informes   ORDER BY id DESC LIMIT 1 ";
                    ds = con.query(sql);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["diacierre"] != null)
                        {
                            if (ds.Tables[0].Rows[0]["diacierre"].ToString() != "")
                                fechita = Convert.ToDateTime(ds.Tables[0].Rows[0]["diacierre"].ToString());
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar informes");
                return fechita;
            }

            return fechita;
        }

        public DataSet get_dia(string dia_, string nomtipo_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT ruta FROM informes WHERE diacierre = '" + dia_ + "' AND nomtipo LIKE '%"+nomtipo_+"%' ";

            Console.WriteLine("--> "+sql);

            try
            {
                
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar informes");
            }

            return ds;
        }

        public DataSet get(string id_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM informes WHERE codestado = 1 AND id = '" + id_ + "' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar informes");
            }

            return ds;
        }


        public void borrarRegistros()
        {
            sql = "DELETE FROM informes";
            con.query(sql);
        }



        public bool save()
        {
            try
            {
                if (this.exist()){

                    sql = "UPDATE informes SET " +
                        "ultmod = '" + this.ultmod + "'," +
                        "user_edit = '" + this.user_edit + "'," +
                        "diacierre = '" + this.diacierre + "'," +
                        "ruta = '" + this.ruta + "'" +
                        " WHERE diacierre = '" + this.diacierre + "' AND nomtipo = '" + this.nomtipo + "'";
                }
                else
                {
                    sql = "INSERT INTO informes (" + this.columns + ") VALUES(" +
                    "'" + this.tipo_informe + "'," +
                    "'" + this.ruta + "'," +
                    "'" + this.ultmod + "'," +
                    "'" + this.user_edit + "'," +
                    "'" + this.codestado + "'," +
                    "'" + this.nomtipo + "'," +
                    "'" + this.diacierre + "'" +
                    ") ";
                }


                Console.WriteLine("\nINFORMES "+sql+"\n");

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }




    }
}
