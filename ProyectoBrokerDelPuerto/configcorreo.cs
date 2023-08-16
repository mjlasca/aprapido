using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class configcorreo
    {
        string sql = "";
        string columns = "servidor,email,pass,puerto,useredit,ultmod,usocorreo";
        public string id, servidor, email, pass, puerto, useredit, ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), usocorreo = "general";
        conexion con = new conexion();

        public configcorreo()
        {
            this.install();
        }

        private void install()
        {
            /*sql = "DROP TABLE configcorreo;";

            con.query("DROP TABLE configcorreo;");*/

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists configcorreo ( id serial PRIMARY KEY, servidor VARCHAR(200), email VARCHAR(200)," +
                "pass VARCHAR(200),puerto VARCHAR(100), useredit VARCHAR(150) NULL, ultmod datetime NULL, usocorreo VARCHAR(50) DEFAULT 'general' );";
                con.query(sql);
            }
            else
            {
                sql = "CREATE TABLE if not exists configcorreo ( id INTEGER PRIMARY KEY, servidor VARCHAR(200), email VARCHAR(200)," +
                "pass VARCHAR(200),puerto VARCHAR(100), useredit VARCHAR(150) NULL, ultmod datetime NULL, usocorreo VARCHAR(50) DEFAULT 'general' );";
                con.query(sql);

                string sql1 = "SELECT * FROM configcorreo WHERE id IS NULL LIMIT 1";
                DataSet dsss = con.query(sql1);
                if(dsss.Tables[0].Rows.Count > 0)
                {
                    con.query("DROP TABLE configcorreo;");
                    con.query(sql);
                    this.servidor = dsss.Tables[0].Rows[0]["servidor"].ToString();
                    this.email = dsss.Tables[0].Rows[0]["email"].ToString();
                    this.pass = dsss.Tables[0].Rows[0]["pass"].ToString();
                    this.puerto = dsss.Tables[0].Rows[0]["puerto"].ToString();
                    this.useredit = dsss.Tables[0].Rows[0]["useredit"].ToString();
                    this.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm-ss"); ;
                    this.save();
                }
               

                
            }

            this.addColumn();




        }

        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                try
                {

                    //Add column categoria if NOT exist
                    if (con.query("SHOW COLUMNS FROM configcorreo WHERE Field = 'usocorreo' ").Tables[0].Rows.Count == 0)
                    {
                        con.query("ALTER TABLE configcorreo ADD COLUMN usocorreo VARCHAR(50) DEFAULT 'general'");
                    }
                }
                catch
                {
                    //
                }
            }
            else
            {
                con.query("ALTER TABLE configcorreo ADD COLUMN usocorreo VARCHAR(50) DEFAULT 'general'");
            }


        }




        public bool exist()
        {
            sql = "SELECT id FROM configcorreo WHERE id != '' AND usocorreo = '"+this.usocorreo+"' ";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }

            return false;
        }




        public bool save()
        {
            try
            {
                if (this.exist())
                {
                    sql = "UPDATE configcorreo SET " +
                       "servidor = '" + this.servidor + "'," +
                       "email = '" + this.email + "'," +
                       "pass = '" + this.pass + "'," +
                       "puerto = '" + this.puerto + "'," +
                       "useredit = '" + this.useredit + "'," +
                       "usocorreo = '" + this.usocorreo + "'," +
                       "ultmod = '" + this.ultmod + "'" +
                       " WHERE id != '' AND usocorreo = '"+this.usocorreo+"' " ;
                }
                else
                {

                    sql = "INSERT INTO configcorreo (" + this.columns + ") VALUES(" +
                        "'" + this.servidor + "'," +
                        "'" + this.email + "'," +
                        "'" + this.pass + "'," +
                        "'" + this.puerto + "'," +
                        "'" + this.useredit + "'," +
                        "'" + this.ultmod + "'," +
                        "'" + this.usocorreo + "'" +
                        ") ;";
                    
                }


                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

     

        public bool get(string usocorreo_ = "general")
        {
            bool res = false;

            sql = "SELECT * FROM configcorreo WHERE id > '0'  AND usocorreo = '"+ usocorreo_ + "'";
            try
            {
                DataSet ds = new DataSet();
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.servidor = ds.Tables[0].Rows[0]["servidor"].ToString();
                    this.email = ds.Tables[0].Rows[0]["email"].ToString();
                    this.pass = ds.Tables[0].Rows[0]["pass"].ToString();
                    this.puerto = ds.Tables[0].Rows[0]["puerto"].ToString();
                    this.useredit= ds.Tables[0].Rows[0]["useredit"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
                    this.usocorreo = ds.Tables[0].Rows[0]["usocorreo"].ToString();
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar configcorreo " + ex.Message);
                res = false;
            }

            return res;
        }


    }
}
