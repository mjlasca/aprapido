using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class commission_user
    {
        string sql = "";

        string columns = " date_ini,user_comm,comm_prima,comm_premio,useredit,ultmod";
        public string reg { get; set; }
        public string date_ini { get; set; }
        public string user_comm { get; set; }
        public string comm_prima { get; set; } = "0";
        public string comm_premio { get; set; } = "0";
        public string useredit { get; set; }
        public string ultmod { get; set; }

        conexion con = new conexion();

        public commission_user(bool inst = false)
        {
            if(inst)
                this.install();
        }


        private void install()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists commission_user (reg serial PRIMARY KEY, date_ini DATE NULL, user_comm VARCHAR(150) NULL, comm_prima DOUBLE DEFAULT 0, " +
                " comm_premio DOUBLE DEFAULT 0, useredit VARCHAR(150) NULL, ultmod DATETIME NULL );";
            }
            else
            {
                sql = "CREATE TABLE if not exists commission_user (reg INTEGER PRIMARY KEY, date_ini DATE NULL, user_comm VARCHAR(150) NULL, comm_prima DOUBLE DEFAULT 0, " +
                " comm_premio DOUBLE DEFAULT 0, useredit VARCHAR(150) NULL, ultmod DATETIME NULL );";
            }

            con.query(sql);
            this.addIndex();

        }

        public void addIndex()
        {

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "SELECT COUNT(1) as cant FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE()   AND TABLE_NAME = 'commission_user'  AND INDEX_NAME = 'idx.user_comm'; ";
                DataSet ds = con.query(sql);
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "1")
                {
                    sql = "CREATE INDEX `idx.user_comm` ON `commission_user` (`user_comm`);";
                    con.query(sql);
                }
                sql = "SELECT COUNT(1) as cant FROM information_schema.statistics WHERE TABLE_SCHEMA = DATABASE()   AND TABLE_NAME = 'commission_user'  AND INDEX_NAME = 'idx.date_ini'; ";

                ds = con.query(sql);
                if (ds.Tables[0].Rows[0]["cant"].ToString() != "1")
                {
                    sql = "CREATE INDEX `idx.date_ini` ON `commission_user` (`date_ini`);";
                    con.query(sql);
                }
            }
            else
            {
                sql = "CREATE INDEX idx_user_comm ON commission_user(user_comm);";
                con.query(sql);
                sql = "CREATE INDEX idx_date_ini ON commission_user(date_ini);";
                con.query(sql);
            }
        }

        public commission_user get(string user_)
        {
            commission_user cus = new commission_user();
            sql = $"SELECT reg,date_ini,user_comm,comm_prima,comm_premio,useredit,ultmod FROM commission_user WHERE user_comm = '{user_}' AND  DATE(date_ini) <= '{DateTime.Now.ToString("yyyy-MM-dd")}' ORDER BY DATE(date_ini) DESC  LIMIT 1";

            DataSet ds = con.query(sql);
            if(ds.Tables[0].Rows.Count > 0)
            {
                cus.reg = ds.Tables[0].Rows[0]["reg"].ToString();
                cus.date_ini = Convert.ToDateTime( ds.Tables[0].Rows[0]["date_ini"].ToString() ).ToString("yyyy-MM-dd");
                cus.user_comm = ds.Tables[0].Rows[0]["user_comm"].ToString();
                cus.comm_prima =  ds.Tables[0].Rows[0]["comm_prima"].ToString() ;
                cus.comm_premio = ds.Tables[0].Rows[0]["comm_premio"].ToString() ;
                cus.useredit = ds.Tables[0].Rows[0]["useredit"].ToString();
                cus.ultmod = Convert.ToDateTime( ds.Tables[0].Rows[0]["ultmod"].ToString() ).ToString("yyyy-MM-dd HH:mm:ss");
            }
            

            return cus;
        }

        public List<commission_user> get_list(string user_)
        {
            List<commission_user> listcus = new  List<commission_user>();
            sql = $"SELECT reg,DATE(date_ini) as date_ini,comm_prima,comm_premio FROM commission_user WHERE user_comm = '{user_}' ORDER BY DATE(date_ini) DESC ";

            DataSet ds = con.query(sql);

            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                commission_user cus = new commission_user();
                cus.reg = ds.Tables[0].Rows[i]["reg"].ToString();
                cus.date_ini = Convert.ToDateTime(ds.Tables[0].Rows[i]["date_ini"].ToString()).ToString("dd-MM-yyyy");
                cus.comm_prima = ds.Tables[0].Rows[i]["comm_prima"].ToString();
                cus.comm_premio = ds.Tables[0].Rows[i]["comm_premio"].ToString();
                listcus.Add(cus);
            }

            return listcus;
        }


        public bool delete_reg(string reg_)
        {
            sql = $"DELETE FROM commission_user WHERE reg = '{reg_}' ";
            try
            {
                con.query(sql);
                return true;
            }
            catch (Exception ex)
            {
                logs log = new logs();
                log.newError("COM-101","Error al eliminar registro de comisión "+ex.Message);
                return false;
            }
            
        }

        public bool save()
        {
            try
            {
                sql = "INSERT INTO commission_user (" + this.columns + ")"+
                    $" VALUES('{this.date_ini}','{this.user_comm}','{this.comm_prima}','{this.comm_premio}','{this.useredit}','{this.ultmod}') ";

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
