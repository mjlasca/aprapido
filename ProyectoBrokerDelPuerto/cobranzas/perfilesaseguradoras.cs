using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto.cobranzas
{
    class perfilesaseguradoras
    {
        string sql = "";
        string columns = "idaseguradora,codigoproductor,matricula,nombre,ramo,poliza,certificado,cuotagarantizada,sms,colorcobertura,fechavencimiento,"+
            "esprimeracuota,moneda,monto,agenterecaudador,pagotramite,cobrador,saldovencido,montopagotramite,referencia,producto,formapago,fechaemisionrecibo,"+
            "fechainiciovigenciapoliza,fechafinvigenciapoliza,organizador,riesgoasegurado,endoso,factura,fechabaja,cuotan,dnicliente,periodo,email,telefono,"+
            "pesoargentino,dolartipodato,ultmod,user_edit,codestado,filacomienzo";
        public string reg, idaseguradora, codigoproductor, matricula, nombre, ramo, poliza, certificado, cuotagarantizada, sms, colorcobertura, fechavencimiento,
            esprimeracuota,moneda,monto,agenterecaudador,pagotramite,cobrador,saldovencido,montopagotramite,referencia,producto,formapago,fechaemisionrecibo,
            fechainiciovigenciapoliza,fechafinvigenciapoliza,organizador,riesgoasegurado,endoso,factura,fechabaja,cuotan,dnicliente,periodo,email,telefono,
            pesoargentino,dolartipodato, ultmod, user_edit, codestado, filacomienzo = "";
        conexion con = new conexion();

        public perfilesaseguradoras()
        {
            this.install();
        }


        private void install()
        {
            sql = "DROP TABLE perfilesaseguradoras";
            //con.query(sql);



            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists perfilesaseguradoras (reg serial PRIMARY KEY,idaseguradora VARCHAR(20) NULL, codigoproductor VARCHAR(20) NULL, matricula VARCHAR(20) NULL, nombre VARCHAR(20) NULL, ramo VARCHAR(20) NULL, poliza VARCHAR(20) NULL," +
                    "certificado VARCHAR(20) NULL, cuotagarantizada VARCHAR(20) NULL, sms VARCHAR(20) NULL, colorcobertura VARCHAR(20) NULL, fechavencimiento VARCHAR(20) NULL, esprimeracuota VARCHAR(20) NULL,moneda VARCHAR(20) NULL,monto VARCHAR(20) NULL,agenterecaudador VARCHAR(20) NULL,pagotramite VARCHAR(20) NULL,cobrador VARCHAR(20) NULL," +
                "saldovencido VARCHAR(20) NULL,montopagotramite VARCHAR(20) NULL,referencia VARCHAR(20) NULL,producto VARCHAR(20) NULL,formapago VARCHAR(20) NULL,fechaemisionrecibo VARCHAR(20) NULL, fechainiciovigenciapoliza VARCHAR(20) NULL,fechafinvigenciapoliza VARCHAR(20) NULL,organizador VARCHAR(20) NULL," +
                "riesgoasegurado VARCHAR(20) NULL,endoso VARCHAR(20) NULL,factura VARCHAR(20) NULL,fechabaja VARCHAR(20) NULL,cuotan VARCHAR(20) NULL,dnicliente VARCHAR(20) NULL,periodo VARCHAR(20) NULL,email VARCHAR(20) NULL,telefono VARCHAR(20) NULL, pesoargentino VARCHAR(20) NULL,dolartipodato VARCHAR(20) NULL,"+
                "ultmod DATETIME NULL, user_edit VARCHAR(150) NULL, codestado INT(2) NULL, filacomienzo INT(5) NULL ); ";
                con.query(sql);
            }
            else
            {
                sql = "CREATE TABLE if not exists perfilesaseguradoras (reg INTEGER PRIMARY KEY AUTOINCREMENT,idaseguradora VARCHAR(20) NULL, codigoproductor VARCHAR(20) NULL, matricula VARCHAR(20) NULL, nombre VARCHAR(20) NULL, ramo VARCHAR(20) NULL, poliza VARCHAR(20) NULL," +
                    "certificado VARCHAR(20) NULL, cuotagarantizada VARCHAR(20) NULL, sms VARCHAR(20) NULL, colorcobertura VARCHAR(20) NULL, fechavencimiento VARCHAR(20) NULL, esprimeracuota VARCHAR(20) NULL,moneda VARCHAR(20) NULL,monto VARCHAR(20) NULL,agenterecaudador VARCHAR(20) NULL,pagotramite VARCHAR(20) NULL,cobrador VARCHAR(20) NULL," +
                "saldovencido VARCHAR(20) NULL,montopagotramite VARCHAR(20) NULL,referencia VARCHAR(20) NULL,producto VARCHAR(20) NULL,formapago VARCHAR(20) NULL,fechaemisionrecibo VARCHAR(20) NULL, fechainiciovigenciapoliza VARCHAR(20) NULL,fechafinvigenciapoliza VARCHAR(20) NULL,organizador VARCHAR(20) NULL," +
                "riesgoasegurado VARCHAR(20) NULL,endoso VARCHAR(20) NULL,factura VARCHAR(20) NULL,fechabaja VARCHAR(20) NULL,cuotan VARCHAR(20) NULL,dnicliente VARCHAR(20) NULL,periodo VARCHAR(20) NULL,email VARCHAR(20) NULL,telefono VARCHAR(20) NULL, pesoargentino VARCHAR(20) NULL,dolartipodato VARCHAR(20) NULL," +
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
                if (con.query("Select CHARACTER_MAXIMUM_LENGTH as longitud from information_schema.columns WHERE TABLE_NAME='perfilesaseguradoras' AND COLUMN_NAME='nombre' ").Tables[0].Rows[0]["longitud"].ToString() == "150")
                {
                    con.query("ALTER TABLE perfilesaseguradoras MODIFY nombre VARCHAR(1500) NULL;");
                }
            }
            else
            {
                con.query("ALTER TABLE perfilesaseguradoras MODIFY nombre VARCHAR(1500) NULL;");
            }*/

        }

        public DataSet get()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM perfilesaseguradoras WHERE idaseguradora = '" + this.idaseguradora + "' ";
            try
            {
                Console.WriteLine("SQL "+sql);
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar perfilesaseguradoras");
            }

            return ds;
        }


        public DataSet get_all()
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM perfilesaseguradoras ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar perfilesaseguradoras");
            }

            return ds;
        }

        public bool exist()
        {
            sql = "SELECT reg FROM perfilesaseguradoras WHERE idaseguradora = '" + this.idaseguradora + "'";
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
            
                    sql = "UPDATE perfilesaseguradoras SET " +
                        "idaseguradora = '" + this.idaseguradora + "'," +
                        "codigoproductor = '" + this.codigoproductor + "'," +
                        "matricula  = '" + this.matricula + "'," +
                        "nombre = '" + this.nombre + "'," +
                        "ramo = '" + this.ramo + "'," +
                        "poliza = '" + this.poliza + "'," +
                        "certificado = '" + this.certificado + "'," +
                        "cuotagarantizada = '" + this.cuotagarantizada + "'," +
                        "sms = '" + this.sms + "'," +
                        "colorcobertura = '" + this.colorcobertura + "'," +
                        "fechavencimiento = '" + this.fechavencimiento + "'," +
                        "esprimeracuota = '" + this.esprimeracuota + "'," +
                        "moneda = '" + this.moneda + "'," +
                        "monto = '" + this.monto + "'," +
                        "agenterecaudador = '" + this.agenterecaudador + "'," +
                        "pagotramite = '" + this.pagotramite + "'," +
                        "cobrador = '" + this.cobrador + "'," +
                        "pagotramite = '" + this.pagotramite + "'," +
                        "saldovencido = '" + this.saldovencido + "'," +
                        "montopagotramite = '" + this.montopagotramite + "'," +
                        "referencia = '" + this.referencia + "'," +
                        "producto = '" + this.producto + "'," +
                        "formapago = '" + this.formapago + "'," +
                        "fechaemisionrecibo = '" + this.fechaemisionrecibo + "'," +
                        "fechainiciovigenciapoliza = '" + this.fechainiciovigenciapoliza + "'," +
                        "fechafinvigenciapoliza = '" + this.fechafinvigenciapoliza + "'," +
                        "organizador = '" + this.organizador + "'," +
                        "riesgoasegurado = '" + this.riesgoasegurado + "'," +
                        "endoso = '" + this.endoso + "'," +
                        "factura = '" + this.factura + "'," +
                        "fechabaja = '" + this.fechabaja + "'," +
                        "cuotan = '" + this.cuotan + "'," +
                        "dnicliente = '" + this.dnicliente + "'," +
                        "periodo = '" + this.periodo + "'," +
                        "email = '" + this.email + "'," +
                        "telefono = '" + this.telefono + "'," +
                        "pesoargentino = '" + this.pesoargentino + "'," +
                        "dolartipodato = '" + this.dolartipodato + "'," +
                        "filacomienzo = '" + this.filacomienzo + "'," +
                        "ultmod = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "user_edit = '" + MDIParent1.sesionUser + "'," +
                        "codestado = '1' " +
                        " WHERE idaseguradora = '" + this.idaseguradora + "'";
                }
                else
                {
                    sql = "INSERT INTO perfilesaseguradoras (" + this.columns + ") VALUES(" +
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
                }

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
            sql = "DELETE perfilesaseguradoras WHERE idaseguradora = '" + this.idaseguradora + "' ";
            con.query(sql);
        }
    }
}
