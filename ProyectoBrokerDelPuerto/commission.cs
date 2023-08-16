using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class commission
    {
        string sql = "";

        string columns = "prefijo,idpropuesta,user,porc_comprima,porc_compremio,fecha_paga_propuesta, useredit,ultmod, liquidado,idliquidacion";
        public string id, prefijo, idpropuesta, user, porc_comprima, porc_compremio, fecha_paga_propuesta, useredit = MDIParent1.sesionUser, liquidado = "1", idliquidacion;
        public string  ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        conexion con = new conexion();

        public commission(bool inst_ = false)
        {
            if(inst_)
                this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists commission (id serial PRIMARY KEY, prefijo VARCHAR(30), idpropuesta VARCHAR(30), user VARCHAR(150) NULL, porc_comprima double DEFAULT 0, " +
                " porc_compremio double DEFAULT 0, useredit VARCHAR(150) NULL, fecha_paga_propuesta DATE NULL, ultmod DATETIME NULL, liquidado int(2) DEFAULT 1, idliquidacion VARCHAR(30) NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists commission (id INTEGER PRIMARY KEY, prefijo VARCHAR(30), idpropuesta VARCHAR(30), user VARCHAR(150) NULL, porc_comprima double DEFAULT 0, " +
                " porc_compremio double DEFAULT 0, useredit VARCHAR(150) NULL, fecha_paga_propuesta DATE NULL, ultmod DATETIME NULL, liquidado int(2) DEFAULT 1, idliquidacion VARCHAR(30) NULL);";
            }


            con.query(sql);

            this.addColumn();
            this.addIndex();
        }

        public void addIndex()
        {

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "SELECT COUNT(1) as cant FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE()   AND TABLE_NAME = 'commission'  AND INDEX_NAME = 'idx.prefijo'; ";
                DataSet ds = con.query(sql);
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "1")
                {
                    sql = "CREATE INDEX `idx.prefijo` ON `commission` (`prefijo`);";
                    con.query(sql);
                }

                sql = "SELECT COUNT(1) as cant FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE()   AND TABLE_NAME = 'commission'  AND INDEX_NAME = 'idx.idpropuesta'; ";

                ds = con.query(sql);
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "1")
                {
                    sql = "CREATE INDEX `idx.idpropuesta` ON `commission` (`idpropuesta`);";
                    con.query(sql);
                }

                sql = "SELECT COUNT(1) as cant FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE()   AND TABLE_NAME = 'commission'  AND INDEX_NAME = 'idx.fecha_paga_propuesta'; ";

                ds = con.query(sql);
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "1")
                {
                    sql = "CREATE INDEX `idx.fecha_paga_propuesta` ON `commission` (`fecha_paga_propuesta`);";
                    con.query(sql);
                }


                sql = "SELECT COUNT(1) as cant FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE()   AND TABLE_NAME = 'commission'  AND INDEX_NAME = 'idx.idliquidacion'; ";
                ds = con.query(sql);
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "1")
                {
                    sql = "CREATE INDEX `idx.idliquidacion` ON `commission` (`idliquidacion`);";
                    con.query(sql);
                }
            }
            else
            {
                sql = "CREATE INDEX idx_prefijo ON commission(prefijo);";
                con.query(sql);
                sql = "CREATE INDEX idx_idpropuesta ON commission(idpropuesta);";
                con.query(sql);
                sql = "CREATE INDEX idx_fecha_paga_propuesta ON commission(fecha_paga_propuesta);";
                con.query(sql);
                sql = "CREATE INDEX idx_idliquidacion ON commission(idliquidacion);";
                con.query(sql);
            }
        }

        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                //Add column categoria if NOT exist
                /*if (con.query("SHOW COLUMNS FROM commission WHERE Field = 'prefijo' ").Tables[0].Rows.Count == 0)
                    con.query("ALTER TABLE commission ADD COLUMN prefijo VARCHAR(20) NULL;");*/
              
            }
            else
            {
                //con.query("ALTER TABLE commission ADD COLUMN prefijo VARCHAR(20) NULL;");
            }

        }


        public DataSet get_datospago(string idliquidado_)
        {
            DataSet ds = new DataSet();

            if (MDIParent1.baseDatos == "MySql")
            {

                sql = "SELECT CONCAT(t2.prefijo,t2.idpropuesta)  as Propuesta " +
                    ",t2.prima as Prima,t2.premio_total as Premio, t1.porc_comprima as Porcent_Prima, t1.porc_compremio as Porcent_Premio ,"+
                    " (t2.prima*t1.porc_comprima /100) as ComPrima, (t2.premio_total*t1.porc_compremio /100) as ComPremio" +
                " FROM commission t1 INNER JOIN propuestas t2 ON t2.codestado > 0 AND t2.paga = 1 AND t1.liquidado = '1' AND t1.idliquidacion = '" + idliquidado_
                + "' AND  t1.idpropuesta = t2.idpropuesta AND t1.prefijo = t2.prefijo ";

            }
            else
            {
                sql = "SELECT ( t2.prefijo || t2.idpropuesta)  as Propuesta " +
                    ",t2.prima as Prima,t2.premio_total as Premio, t1.porc_comprima as Porcent_Prima, t1.porc_compremio as Porcent_Premio ,"+
                    " (t2.prima*t1.porc_comprima /100) as ComPrima, (t2.premio_total*t1.porc_compremio /100) as ComPremio " +
                " FROM commission t1 INNER JOIN propuestas t2 ON t2.codestado > 0 AND t2.paga = 1 AND t1.liquidado = '1' AND t1.idliquidacion = '" + idliquidado_
                + "' AND  t1.idpropuesta = t2.idpropuesta AND t1.prefijo = t2.prefijo ";
            }


            try
            {

                ds = con.query(sql);

            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar rendiciones " + ex.Message);
            }

            return ds;
        }



        public bool save()
        {
            try
            {
                liquidaciones liq = new liquidaciones();
                string consecutivo = liq.consecutivo();

                sql = "INSERT INTO commission (" + this.columns + ") VALUES(" +
                      "'" + this.prefijo + "'," +
                      "'" + this.idpropuesta + "'," +
                      "'" + this.user + "'," +
                      "'" + this.porc_comprima + "'," +
                      "'" + this.porc_compremio + "'," +
                      "'" + this.fecha_paga_propuesta + "'," +
                      "'" + this.useredit + "'," +
                      "'" + this.ultmod + "'," +
                      "'" + this.liquidado + "'," +
                      "'" + consecutivo + "'" +
                      ") ";


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
