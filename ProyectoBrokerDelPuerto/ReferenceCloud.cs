using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoBrokerDelPuerto
{
    class ReferenceCloud
    {
        string sql = "";

        string columns = "idpropuesta,prefijo,codempresa,referencia,nota,prima,codestado,ptoventa,user, ultmod";
        public string reg, idpropuesta, prefijo, referencia, nota,prima, codempresa = MDIParent1.codempresa, ptoventa = MDIParent1.prefijo, user = MDIParent1.sesionUser, ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        conexion con = new conexion();
        int codestado { get; set; }
        public ReferenceCloud(bool ins = false)
        {
            if (ins)
                this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists referencecloud (reg serial PRIMARY KEY, prefijo VARCHAR(100) NULL,idpropuesta VARCHAR(150) NULL,codempresa VARCHAR(100) NULL, ultmod DATETIME NULL, " +
                    " referencia VARCHAR(100) NULL,nota VARCHAR(100) NULL,prima double DEFAULT 0, ptoventa VARCHAR(100) NULL, user VARCHAR(100) NULL, codestado int(1) DEFAULT 0);";
            }
            else
            {
                sql = "CREATE TABLE if not exists referencecloud  (reg INTEGER PRIMARY KEY, prefijo VARCHAR(100) NULL,idpropuesta VARCHAR(150) NULL,codempresa VARCHAR(100) NULL, ultmod DATETIME NULL, " +
                    " referencia VARCHAR(100) NULL,nota VARCHAR(100) NULL,prima double DEFAULT 0, ptoventa VARCHAR(100) NULL, user VARCHAR(100) NULL, codestado int(1) DEFAULT 0);";
            }

            con.query(sql);
        }


        public async void consultaReferencias()
        {


            DateTime dt = DateTime.Now.AddDays(-80);
            DateTime dtcu = DateTime.Now;

            sql = "SELECT t0.idpropuesta,t0.prefijo FROM propuestas t0 " +
                $"WHERE (t0.referencia IS NULL OR t0.referencia = '') AND t0.codestado > 0 AND ( DATE(t0.ultmod) > '{dt.ToString("yyyy-MM-dd HH:mm:ss")}' AND DATE(t0.ultmod) < '{dtcu.ToString("yyyy-MM-dd HH:mm:ss")}' )" +
                $"  AND ( (SELECT COUNT(1) FROM referencecloud t1 WHERE t0.idpropuesta = t1.idpropuesta AND t0.prefijo = t1.prefijo) < 1 ) LIMIT 500 ";

            IEnumerable<RegistroRef> refpro;

            if (MDIParent1.baseDatos == "MySql")
            {
                refpro = con.conecction.Query<RegistroRef>(sql);
            }
            else
            {
                refpro = con.connectionSQLite.Query<RegistroRef>(sql);
            }
            var concat_ = new System.Text.StringBuilder();
            if (refpro.Count() > 0)
            {
                ApiPropuestas apProp = new ApiPropuestas();
                try
                {
                    List<ReferenceCloud> refCloud = new List<ReferenceCloud>();
                    refCloud = await apProp.GetRef(refpro);
                    if (refCloud.Count > 0)
                    {

                        foreach (ReferenceCloud re in refCloud)
                        {
                            if (re.referencia != null && re.referencia != "")
                            {
                                if (concat_.ToString() != "")
                                    concat_.AppendLine("," + re.save());
                                else
                                    concat_.AppendLine(re.save());
                            }

                        }

                        if (concat_.ToString() != "")
                        {
                            sql = "INSERT INTO referencecloud (" + this.columns + ") VALUES";
                            con.query(sql + concat_.ToString());
                        }

                    }
                }
                catch(Exception ex)
                {
                    //
                }
                

                
                
            }


            sql = $"SELECT prefijo,idpropuesta,referencia,nota,prima FROM referencecloud  WHERE codestado = 0 LIMIT 200";

            IEnumerable<RegistroRef> refpro1;

            if (MDIParent1.baseDatos == "MySql")
            {
                refpro1 = con.conecction.Query<RegistroRef>(sql);
            }
            else
            {
                refpro1 = con.connectionSQLite.Query<RegistroRef>(sql);
            }

            
            if (refpro1.Count() > 0)
            {
                foreach (RegistroRef re in refpro1)
                {
                    sql = $"UPDATE propuestas SET referencia = '{re.referencia}', nota = '{re.nota}', prima = '{re.prima}' WHERE prefijo = '{re.prefijo}' AND idpropuesta = '{re.idpropuesta}' ";
                    con.query(sql);

                    sql = $"UPDATE referencecloud SET codestado = '1' WHERE prefijo = '{re.prefijo}' AND idpropuesta = '{re.idpropuesta}' AND codestado = '0' ";
                    con.query(sql);
                }
            }





        }

        public void actualizarReferenciasNube()
        {


            DateTime dt = DateTime.Now.AddDays(-80);
            DateTime dtcu = DateTime.Now.AddDays(-2);

            sql = "SELECT idpropuesta,prefijo,referencia,nota,prima FROM propuestas " +
                $" WHERE ( referencia != '' ) AND ( prima != '' ) AND  ( DATE(ultmod) > '{dt.ToString("yyyy-MM-dd HH:mm:ss")}' AND DATE(ultmod) < '{dtcu.ToString("yyyy-MM-dd HH:mm:ss")}' ) ";


            IEnumerable<RegistroRef> refpro;

            if (MDIParent1.baseDatos == "MySql")
            {
                refpro = con.conecction.Query<RegistroRef>(sql);
            }
            else
            {
                refpro = con.connectionSQLite.Query<RegistroRef>(sql);
            }

            var resultados = new { registros = refpro.ToList() };
            var jsonData = JsonConvert.SerializeObject(resultados);

            if (!File.Exists(@"file_reference/ref_propuestas.txt"))
            {
                System.IO.Directory.CreateDirectory("file_reference");
                System.IO.File.WriteAllText(@"file_reference/ref_propuestas.txt", jsonData);
            }


        }

        public string actualizarReferenciasNubeFile(DateTime date_)
        {
            conexion con = new conexion();
            sql = "SELECT idpropuesta,prefijo,referencia,nota,prima FROM propuestas " +
                $" WHERE ( referencia != '' ) AND  ( DATE(ultmod) = DATE('{date_.ToString("yyyy-MM-dd HH:mm:ss")}') ) ";


            IEnumerable<RegistroRef> refpro;

            if (MDIParent1.baseDatos == "MySql")
            {
                refpro = con.conecction.Query<RegistroRef>(sql);
            }
            else
            {
                refpro = con.connectionSQLite.Query<RegistroRef>(sql);
            }

            var resultados = new { registros = refpro.ToList() };
            
            var jsonData = JsonConvert.SerializeObject(resultados);
            if (refpro.Count() > 0)
                return jsonData;
            else
                return "";

        }

        public string save()
        {
            try
            {
                
                sql = "('" + this.idpropuesta + "'," +
                "'" + this.prefijo + "'," +
                "'" + this.codempresa + "'," +
                "'" + this.referencia + "'," +
                "'" + this.nota + "'," +
                "'" + this.prima + "'," +
                "'" + this.codestado + "'," +
                "'" + this.ptoventa + "'," +
                "'" + MDIParent1.sesionUser + "'," +
                "'" + this.ultmod + "' " +
                ") ";
                
                return sql;

            }
            catch (Exception ex)
            {
                return "";
            }

        }
    }

    public class RegistroRef
    {
        public int idpropuesta { get; set; }
        public string prefijo { get; set; }
        public string referencia { get; set; }
        public string nota { get; set; }
        public double prima { get; set; } = 0;
    }
}
