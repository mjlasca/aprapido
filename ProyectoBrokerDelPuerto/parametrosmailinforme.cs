using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class parametrosmailinforme
    {
        string sql = "";

        string columns = "mails,asunto,mensaje, useredit,ultmod, plantillatipo";
        public string id, mails="", asunto = "", mensaje = "", useredit = MDIParent1.sesionUser, ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), plantillatipo = "infodeldia";
        conexion con = new conexion();

        public parametrosmailinforme(bool est = false )
        {
            if(est)
                this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists parametrosmailinforme (id serial PRIMARY KEY, mails VARCHAR(2000) NULL,asunto VARCHAR(300) NULL, mensaje VARCHAR(2000) NULL," +
                " useredit VARCHAR(150) NULL, ultmod DATETIME NULL);";
            }
            else
            {
                sql = "CREATE TABLE if not exists parametrosmailinforme (id INTEGER PRIMARY KEY, mails VARCHAR(2000) NULL,asunto VARCHAR(300) NULL, mensaje VARCHAR(2000) NULL," +
                " useredit VARCHAR(150) NULL, ultmod DATETIME NULL);";
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
                    if (con.query("SHOW COLUMNS FROM parametrosmailinforme WHERE Field = 'plantillatipo' ").Tables[0].Rows.Count == 0)
                    {
                        con.query("ALTER TABLE parametrosmailinforme ADD COLUMN plantillatipo VARCHAR(50) NULL");
                    }
                }
                catch
                {
                    //
                }
            }
            else
            {
                con.query("ALTER TABLE parametrosmailinforme ADD COLUMN plantillatipo VARCHAR(50) NULL");
            }


        }

        public bool get()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM parametrosmailinforme WHERE  plantillatipo = '"+this.plantillatipo+"' ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.mails = ds.Tables[0].Rows[0]["mails"].ToString();
                    this.asunto = ds.Tables[0].Rows[0]["asunto"].ToString();
                    this.mensaje = ds.Tables[0].Rows[0]["mensaje"].ToString();
                    this.useredit = ds.Tables[0].Rows[0]["useredit"].ToString();
                    this.ultmod = ds.Tables[0].Rows[0]["ultmod"].ToString();
                    this.plantillatipo = ds.Tables[0].Rows[0]["plantillatipo"].ToString();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar parametrosmailinforme");
            }

            return false;
        }

       

        public bool exist()
        {
            sql = "SELECT mails FROM parametrosmailinforme WHERE plantillatipo = '" + this.plantillatipo+ "' ";

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
                    sql = "UPDATE parametrosmailinforme SET " +
                       "mails = '" + this.mails + "'," +
                       "asunto = '" + this.asunto + "'," +
                       "mensaje = '" + this.mensaje + "'," +
                       "useredit = '" + this.useredit + "'," +
                       "plantillatipo = '" + this.plantillatipo + "'," +
                       "ultmod = '" + this.ultmod + "'" +
                       " WHERE plantillatipo = '"+this.plantillatipo+"' ";
                }
                else
                {
                    sql = "INSERT INTO parametrosmailinforme (" + this.columns + ") VALUES(" +
                      "'" + this.mails + "'," +
                      "'" + this.asunto + "'," +
                      "'" + this.mensaje + "'," +
                      "'" + this.useredit + "'," +
                      "'" + this.ultmod + "'," +
                      "'" + this.plantillatipo + "'" +
                      ") ";
                }

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
