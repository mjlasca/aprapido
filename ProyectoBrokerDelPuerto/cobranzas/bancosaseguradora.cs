using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto.cobranzas
{
    class bancosaseguradora
    {
        string sql = "";
        string columns = "idaseguradora,cuit,categoriamediopago,nombrebanco,titularcuenta,cuenta,cbu,sucursal,direccion,telefono, ultmod,user_edit,codestado,codempresa";
        public string reg, idaseguradora, cuit, categoriamediopago, nombrebanco, titularcuenta, cuenta, cbu, sucursal, direccion, telefono, ultmod, user_edit, codestado = "";
        public string codempresa = MDIParent1.codempresa;
        conexion con = new conexion();
        logs log = new logs();

        public bancosaseguradora(bool inst = false)
        {
            if (inst)
                this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists bancosaseguradora (reg serial PRIMARY KEY,idaseguradora VARCHAR(50) NULL, cuit VARCHAR(50) NULL, categoriamediopago VARCHAR(150) NULL, nombrebanco  VARCHAR(150) NULL, " +
                    " titularcuenta  VARCHAR(150) NULL, cuenta  VARCHAR(100) NULL, cbu  VARCHAR(100) NULL, sucursal  VARCHAR(300) NULL, direccion  VARCHAR(200) NULL, "+
                    "  telefono  VARCHAR(30) NULL,  ultmod  DATETIME NULL,  user_edit  VARCHAR(150) NULL,  codestado  int(2) NULL,codempresa  VARCHAR(200) NULL);";
                con.query(sql);
            }
            else
            {
                sql = "CREATE TABLE if not exists bancosaseguradora (reg INTEGER PRIMARY KEY,idaseguradora VARCHAR(50) NULL, cuit VARCHAR(50), categoriamediopago VARCHAR(150) NULL, nombrebanco  VARCHAR(150) NULL, " +
                " titularcuenta  VARCHAR(150) NULL, cuenta  VARCHAR(100) NULL, cbu  VARCHAR(100) NULL, sucursal  VARCHAR(300) NULL, direccion  VARCHAR(200) NULL, " +
                "  telefono  VARCHAR(30) NULL,  ultmod  DATETIME NULL,  user_edit  VARCHAR(150) NULL,  codestado  int(2) NULL,codempresa  VARCHAR(200) NULL);";
                con.query(sql);
            }

        }

     

        
        public bool exist()
        {
            sql = "SELECT reg FROM bancosaseguradora WHERE reg = '" + this.reg + "'";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }


       
        public DataSet get(string reg_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM bancosaseguradora WHERE reg = '" + reg_ + "' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                log.coderror = "BC101";
                log.mensaje = "Error al consultar banco aseguradora "+ex.Message;
                log.save();
                Console.Write("ERROR al consultar bancosaseguradora");
            }

            return ds;
        }

        public DataSet get_all_aseguradora(string idaseguradora_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM bancosaseguradora WHERE trim( idaseguradora ) = trim( '"+idaseguradora_+ "' ) ORDER BY categoriamediopago,nombrebanco DESC   ";

            Console.WriteLine("SQL FORMA DE PAGO "+sql);
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                log.coderror = "BC104";
                log.mensaje = "Error al consultar todos los bancos aseguradora de una aseguradora específica " + ex.Message;
                log.save();
                Console.Write("ERROR al consultar bancosaseguradora");
            }

            return ds;
        }



        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM bancosaseguradora ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                log.coderror = "BC102";
                log.mensaje = "Error al consultar todos los bancos aseguradora " + ex.Message;
                log.save();
                Console.Write("ERROR al consultar bancosaseguradora");
            }

            return ds;
        }


       


        public bool save()
        {
            try
            {
                if (this.exist())
                {
                    sql = "UPDATE bancosaseguradora SET " +
                        "idaseguradora = '" + this.idaseguradora + "'," +
                        "cuit = '" + this.cuit + "'," +
                        "categoriamediopago = '" + this.categoriamediopago + "'," +
                        "nombrebanco = '" + this.nombrebanco + "'," +
                        "titularcuenta = '" + this.titularcuenta + "'," +
                        "cuenta = '" + this.cuenta + "'," +
                        "cbu = '" + this.cbu + "'," +
                        "sucursal = '" + this.sucursal + "'," +
                        "telefono = '" + this.telefono + "'," +
                        "direccion = '" + this.direccion + "'," +
                        "ultmod = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "codempresa = '" + MDIParent1.codempresa + "'," +
                        "codestado = '1' " +
                        " WHERE reg = '" + this.reg + "'";
                }
                else
                {
                    sql = "INSERT INTO bancosaseguradora (" + this.columns + ") VALUES(" +
                        "'" + this.idaseguradora + "'," +
                        "'" + this.cuit + "'," +
                        "'" + this.categoriamediopago + "'," +
                        "'" + this.nombrebanco + "'," +
                        "'" + this.titularcuenta + "'," +
                        "'" + this.cuenta + "'," +
                        "'" + this.cbu + "'," +
                        "'" + this.sucursal + "'," +
                        "'" + this.direccion + "'," +
                        "'" + this.telefono + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + MDIParent1.sesionUser + "'," +
                        "'" + 1 + "'," +
                        "'" + MDIParent1.codempresa + "'" +
                        ") ";
                }

                Console.WriteLine("SQL BANCOS "+sql);

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                log.coderror = "BC103";
                log.mensaje = "Error al guardar o actualizar datos banco aseguradora " + ex.Message;
                log.save();
                return false;
            }

        }

        public void delete()
        {
            sql = "DELETE FROM bancosaseguradora  WHERE reg = '" + this.reg + "' ";
            con.query(sql);
        }

    }
}
