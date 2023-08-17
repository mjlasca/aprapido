using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace ProyectoBrokerDelPuerto
{
    class RegisterPending
    {
        string sql = "";

        string columns = "path,data,verbo, ultmod,user_edit,codempresa, codestado";

        public string id, ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), user_edit = MDIParent1.sesionUser, codempresa = MDIParent1.codempresa;
        public string path { get; set; }
        public string data { get; set; }
        public string verbo { get; set; }
        public int codestado { get; set; } = 0;

        conexion con = new conexion();

        public RegisterPending(bool install = false)
        {
            if(install)
                this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists register_pending (id serial PRIMARY KEY, path VARCHAR(250) NULL, data LONGTEXT CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL, " +
                    " verbo VARCHAR(20) NULL , ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codempresa VARCHAR(150) NULL, codestado INT(2) DEFAULT 0 );";
                con.query(sql);

                DataSet ds = con.query("SELECT COUNT(1) as cant FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE()   AND TABLE_NAME = 'register_pending'  AND INDEX_NAME = 'idx.id'; ");
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "1")
                {
                    con.query("CREATE INDEX `idx.id` ON `register_pending` (`id`);");
                    con.query("CREATE INDEX `idx.cod` ON `register_pending` (`codestado`);");
                }
                    
            }
            else
            {
                sql = "CREATE TABLE if not exists register_pending (id INTEGER PRIMARY KEY, path VARCHAR(250) NULL, data JSON NULL, " +
                    " verbo VARCHAR(20) NULL , ultmod DATETIME NULL, user_edit VARCHAR(100) NULL, codempresa VARCHAR(150) NULL, codestado INT(2) DEFAULT 0 );";
                con.query(sql);
                con.query("CREATE INDEX `idx.id` ON `register_pending` (`id`);");
                con.query("CREATE INDEX `idx.cod` ON `register_pending` (`codestado`);");

            }

            


        }

        public async void sendListPending()
        {
            sql = "SELECT id,path,data,ultmod FROM register_pending WHERE codestado = 0 ORDER BY id ASC ";
            DataSet ds = con.query(sql);
            if(ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ExportData expdata = new ExportData(ds.Tables[0].Rows[i]["path"].ToString());
                        bool rest = await expdata.Post(ds.Tables[0].Rows[i]["data"].ToString());
                        if (rest)
                        {
                            this.updateCodestado(ds.Tables[0].Rows[i]["id"].ToString());
                        }else
                        {
                            DateTime dateReg = Convert.ToDateTime(ds.Tables[0].Rows[i]["ultmod"].ToString());

                            if(DateTime.Now.Date > dateReg.Date)
                                this.updateCodestado(ds.Tables[0].Rows[i]["id"].ToString(), 2);
                        }

                    }

                }
            }
            
        }

        public bool allowImport()
        {
            sql = $"SELECT COUNT(1) as cant FROM register_pending WHERE codestado = 0";
            DataSet ds = con.query(sql);
            if(ds.Tables[0].Rows.Count >= 1)
            {
                if(ds.Tables[0].Rows[0]["cant"].ToString() == "0")
                {
                    return true;
                }
            }

            return false;
        }

        public void updateCodestado(string id_, int status = 1)
        {
            sql = $"UPDATE register_pending SET codestado = '{status}'  WHERE id = '{id_}' ";
            con.query(sql);
        }

        public bool save()
        {
            try
            {
                sql = "INSERT INTO register_pending (" + this.columns +
                    $") VALUES('{this.path}','{this.data}','{this.verbo}','{this.ultmod}','{this.user_edit}','{this.codempresa}','{this.codestado}') ";
                con.query(sql);

                /*if (MDIParent1.baseDatos == "MySql")
                {
                    con.conecction.Execute(sql,
                    new
                    {
                        this.path,
                        this.data,
                        this.verbo,
                        this.ultmod,
                        this.user_edit,
                        this.codempresa,
                        this.codestado,
                    }
                    );
                }
                else
                {
                    con.connectionSQLite.Execute(sql,
                    new
                    {
                        this.path,
                        this.data,
                        this.verbo,
                        this.ultmod,
                        this.user_edit,
                        this.codempresa,
                        this.codestado,
                    }
                    );
                }*/
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("NO se ha podido guardar pending register "+ex);
                return false;
            }
            

        }
    }
}
