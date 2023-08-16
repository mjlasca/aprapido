using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto.cobranzas
{
    class datauploadtemp
    {
        string sql = "";
        string columns = "idaseguradora,codigoproductor,matricula,nombre,ramo,poliza,certificado,cuotagarantizada,sms,colorcobertura,fechavencimiento,"+
            "esprimeracuota,moneda,monto,agenterecaudador,pagotramite,cobrador,saldovencido,montopagotramite,referencia,producto,formapago,fechaemisionrecibo,"+
            "fechainiciovigenciapoliza,fechafinvigenciapoliza,organizador,riesgoasegurado,endoso,factura,fechabaja,cuotan,dnicliente,periodo,email,telefono,"+
            "pesoargentino,dolartipodato,ultmod,user_edit,codestado,filacomienzo";
        public string reg, idaseguradora, codigoproductor, matricula, nombre, ramo, poliza, certificado, cuotagarantizada, sms, colorcobertura, fechavencimiento,
            esprimeracuota, moneda, agenterecaudador, pagotramite, cobrador, saldovencido, montopagotramite, referencia, producto, formapago, fechaemisionrecibo,
            fechainiciovigenciapoliza, fechafinvigenciapoliza, organizador, riesgoasegurado, endoso, factura, fechabaja, cuotan, dnicliente, periodo, email, telefono,
            pesoargentino, dolartipodato, ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), user_edit = MDIParent1.sesionUser, codestado = "1", filacomienzo = "0", monto = "0";
        conexion con = new conexion();

        public datauploadtemp()
        {
            this.install();
        }


        private void install()
        {
            //sql = "DROP TABLE datauploadtemp";
            //con.query(sql);



            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists datauploadtemp (reg serial PRIMARY KEY,idaseguradora VARCHAR(150) NULL, codigoproductor VARCHAR(150) NULL, matricula VARCHAR(150) NULL, nombre VARCHAR(450) NULL, ramo VARCHAR(150) NULL, poliza VARCHAR(150) NULL," +
                    "certificado VARCHAR(150) NULL, cuotagarantizada VARCHAR(150) NULL, sms VARCHAR(150) NULL, colorcobertura VARCHAR(150) NULL, fechavencimiento VARCHAR(150) NULL, esprimeracuota VARCHAR(150) NULL,moneda VARCHAR(150) NULL,monto double DEFAULT 0,agenterecaudador VARCHAR(150) NULL,pagotramite VARCHAR(150) NULL,cobrador VARCHAR(150) NULL," +
                "saldovencido VARCHAR(150) NULL,montopagotramite VARCHAR(150) NULL,referencia VARCHAR(150) NULL,producto VARCHAR(150) NULL,formapago VARCHAR(150) NULL,fechaemisionrecibo VARCHAR(150) NULL, fechainiciovigenciapoliza VARCHAR(150) NULL,fechafinvigenciapoliza VARCHAR(150) NULL,organizador VARCHAR(150) NULL," +
                "riesgoasegurado VARCHAR(150) NULL,endoso VARCHAR(150) NULL,factura VARCHAR(150) NULL,fechabaja VARCHAR(150) NULL,cuotan VARCHAR(150) NULL,dnicliente VARCHAR(150) NULL,periodo VARCHAR(150) NULL,email VARCHAR(150) NULL,telefono VARCHAR(150) NULL, pesoargentino VARCHAR(150) NULL,dolartipodato VARCHAR(150) NULL,"+
                "ultmod DATETIME NULL, user_edit VARCHAR(150) NULL, codestado INT(2) NULL, filacomienzo INT(5) NULL ); ";
                con.query(sql);
            }
            else
            {
                sql = "CREATE TABLE if not exists datauploadtemp (reg INTEGER PRIMARY KEY AUTOINCREMENT,idaseguradora VARCHAR(150) NULL, codigoproductor VARCHAR(150) NULL, matricula VARCHAR(150) NULL, nombre VARCHAR(150) NULL, ramo VARCHAR(150) NULL, poliza VARCHAR(150) NULL," +
                    "certificado VARCHAR(150) NULL, cuotagarantizada VARCHAR(150) NULL, sms VARCHAR(150) NULL, colorcobertura VARCHAR(150) NULL, fechavencimiento VARCHAR(150) NULL, esprimeracuota VARCHAR(150) NULL,moneda VARCHAR(150) NULL,monto double DEFAULT 0,agenterecaudador VARCHAR(150) NULL,pagotramite VARCHAR(150) NULL,cobrador VARCHAR(150) NULL," +
                "saldovencido VARCHAR(150) NULL,montopagotramite VARCHAR(150) NULL,referencia VARCHAR(150) NULL,producto VARCHAR(150) NULL,formapago VARCHAR(150) NULL,fechaemisionrecibo VARCHAR(150) NULL, fechainiciovigenciapoliza VARCHAR(150) NULL,fechafinvigenciapoliza VARCHAR(150) NULL,organizador VARCHAR(150) NULL," +
                "riesgoasegurado VARCHAR(150) NULL,endoso VARCHAR(150) NULL,factura VARCHAR(150) NULL,fechabaja VARCHAR(150) NULL,cuotan VARCHAR(150) NULL,dnicliente VARCHAR(150) NULL,periodo VARCHAR(150) NULL,email VARCHAR(150) NULL,telefono VARCHAR(150) NULL, pesoargentino VARCHAR(150) NULL,dolartipodato VARCHAR(150) NULL," +
                "ultmod DATETIME NULL, user_edit VARCHAR(150) NULL, codestado INT(2) NULL, filacomienzo INT(5) NULL ); ";
                con.query(sql);
            }


            //this.addColumn();
        }

        private void addColumn()
        {
            /*if (MDIParent1.baseDatos == "MySql")
            {
                //Add column categoria if NOT exist
                if (con.query("Select CHARACTER_MAXIMUM_LENGTH as longitud from information_schema.columns WHERE TABLE_NAME='datauploadtemp' AND COLUMN_NAME='nombre' ").Tables[0].Rows[0]["longitud"].ToString() == "150")
                {
                    con.query("ALTER TABLE datauploadtemp MODIFY nombre VARCHAR(1500) NULL;");
                }
            }
            else
            {
                con.query("ALTER TABLE datauploadtemp MODIFY nombre VARCHAR(1500) NULL;");
            }*/

        }

        public DataSet get()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM datauploadtemp WHERE idaseguradora = '" + this.idaseguradora + "' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar datauploadtemp");
            }

            return ds;
        }


        public DataSet get_group_name(string idaseguradora_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM datauploadtemp WHERE idaseguradora = '" + idaseguradora_ + "' GROUP BY nombre ORDER BY nombre ASC";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar datauploadtemp");
            }

            return ds;
        }


        public DataSet get_all(string idaseguradora_)
        {
            DataSet ds = new DataSet();

            sql = "SELECT *, SUM(monto) as total_monto FROM datauploadtemp WHERE idaseguradora = '"+ idaseguradora_ +
                "' GROUP BY nombre,poliza,cuotan,moneda  ORDER BY nombre ASC";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar datauploadtemp");
            }

            return ds;
        }

        public bool exist()
        {
            sql = "SELECT reg FROM datauploadtemp WHERE idaseguradora = '" + this.idaseguradora + "'";
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
                
                    sql = "INSERT INTO datauploadtemp (" + this.columns + ") VALUES(" +
                        "'" + this.idaseguradora + "'," +
                        "'" + this.codigoproductor + "'," +
                        "'" + this.matricula + "'," +
                        "'" + this.nombre+ "'," +
                        "'" + this.ramo+ "'," +
                        "'" + this.poliza+ "'," +
                        "'" + this.certificado+ "'," +
                        "'" + this.cuotagarantizada+ "'," +
                        "'" + this.sms+ "'," +
                        "'" + this.colorcobertura + "'," +
                        "'" + this.fechavencimiento+ "'," +
                        "'" + this.esprimeracuota+ "'," +
                        "'" + this.moneda+ "'," +
                        "'" + this.monto+ "'," +
                        "'" + this.agenterecaudador+ "'," +
                        "'" + this.pagotramite+ "'," +
                        "'" + this.cobrador+ "'," +
                        "'" + this.saldovencido+ "'," +
                        "'" + this.montopagotramite+ "'," +
                        "'" + this.referencia+ "'," +
                        "'" + this.producto+ "'," +
                        "'" + this.formapago+ "'," +
                        "'" + this.fechaemisionrecibo+ "'," +
                        "'" + this.fechainiciovigenciapoliza+ "'," +
                        "'" + this.fechafinvigenciapoliza+ "'," +
                        "'" + this.organizador + "'," +
                        "'" + this.riesgoasegurado+ "'," +
                        "'" + this.endoso+ "'," +
                        "'" + this.factura+ "'," +
                        "'" + this.fechabaja+ "'," +
                        "'" + this.cuotan+ "'," +
                        "'" + this.dnicliente+ "'," +
                        "'" + this.periodo+ "'," +
                        "'" + this.email+ "'," +
                        "'" + this.telefono+ "'," +
                        "'" + this.pesoargentino+ "'," +
                        "'" + this.dolartipodato + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + MDIParent1.sesionUser + "'," +
                        "'1'," +
                        "'" + this.filacomienzo + "'" +
                        ") ";

                con.query(sql);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public void update_mail(string nom_, string mail_)
        {
            sql = "UPDATE datauploadtemp SET email = '"+mail_+"' WHERE nombre LIKE '%"+nom_+"%' ";
            con.query(sql);
        }

        public void delete(string aseguradora_)
        {
            sql = "DELETE FROM datauploadtemp WHERE idaseguradora = '"+aseguradora_+"' ";
            con.query(sql);
        }
    }
}
