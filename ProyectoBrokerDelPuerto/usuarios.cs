using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBrokerDelPuerto
{
    class usuarios
    {
        string sql = "";
        string columns = "loggin, pass, nombre, mail, perfil, allow,comisionprima,comisionpremio,codigoproductor,codestado,codempresa, adminempresa,codorganizador,version";
        public string id, loggin = "", pass = "", nombre = "", mail = "", perfil = "", allow = "0", comisionprima = "0", comisionpremio = "0", codigoproductor = "", codorganizador = "", codestado = "1",codempresa = "", adminempresa = "0";
        public int version = 1;
        public bool passhash = false;
        conexion con = new conexion();

        public usuarios(bool inst = false)
        {
            if(inst)
                this.install();
        }

        private void install()
        {
            /*sql = "DROP TABLE usuarios;";

            con.query(sql);*/

            if (MDIParent1.baseDatos == "MySql")
            {
                sql = "CREATE TABLE if not exists usuarios (id serial PRIMARY KEY, loggin VARCHAR(100) NULL, pass VARCHAR(100) NULL , nombre VARCHAR(150) NULL, " +
                " mail VARCHAR(100) NULL, perfil VARCHAR(100) NULL, allow int(2) NULL, comisionprima double NULL, comisionpremio double NULL  );";
            }
            else
            {
                sql = "CREATE TABLE if not exists usuarios (id INTEGER PRIMARY KEY, loggin VARCHAR(100) NULL, pass VARCHAR(100) NULL , nombre VARCHAR(150) NULL, " +
                " mail VARCHAR(100) NULL, perfil VARCHAR(100) NULL, allow int(2) NULL, comisionprima double NULL, comisionpremio double NULL  );";
            }
            con.query(sql);
            /*
                con.query(sql);
            */
            this.addColumn();

            /*con.query("DELETE FROM usuarios WHERE id = '9' ");
            con.query("DELETE FROM usuarios WHERE id = '12' ");*/

            sql = "UPDATE usuarios SET codempresa = '" + MDIParent1.codempresa + "' WHERE codempresa IS NULL OR codempresa = '' ";
            con.query(sql);

            Configmaster conf = new Configmaster();
            string organ = conf.get("ORGANIZADOR").Tables[0].Rows[0]["nomcodigo"].ToString();
            if ( organ != "")
            {
                sql = "UPDATE usuarios SET codorganizador = '"+ organ + "' WHERE codorganizador = '' OR codorganizador IS NULL ";
                con.query(sql);
            }
            

        }


        private void addColumn()
        {
            if (MDIParent1.baseDatos == "MySql")
            {
                try
                {
                    if (MDIParent1.baseDatos == "MySql")
                    {
                        //Add column categoria if NOT exist
                        if (con.query("SHOW COLUMNS FROM usuarios WHERE Field = 'allow' ").Tables[0].Rows.Count == 0)
                        {
                            con.query("ALTER TABLE usuarios ADD allow int(2) NULL");
                        }
                        if (con.query("SHOW COLUMNS FROM usuarios WHERE Field = 'comisionprima' ").Tables[0].Rows.Count == 0)
                        {
                            con.query("ALTER TABLE usuarios ADD comisionprima double NULL");
                        }
                        if (con.query("SHOW COLUMNS FROM usuarios WHERE Field = 'comisionpremio' ").Tables[0].Rows.Count == 0)
                        {
                            con.query("ALTER TABLE usuarios ADD comisionpremio double NULL");
                        }
                        if (con.query("SHOW COLUMNS FROM usuarios WHERE Field = 'codigoproductor' ").Tables[0].Rows.Count == 0)
                        {
                            con.query("ALTER TABLE usuarios ADD codigoproductor VARCHAR(150) NULL");
                        }
                        if (con.query("SHOW COLUMNS FROM usuarios WHERE Field = 'codestado' ").Tables[0].Rows.Count == 0)
                        {
                            con.query("ALTER TABLE usuarios ADD codestado int(1) DEFAULT 1");
                        }
                        if (con.query("SHOW COLUMNS FROM usuarios WHERE Field = 'codempresa' ").Tables[0].Rows.Count == 0)
                            con.query("ALTER TABLE usuarios ADD COLUMN codempresa VARCHAR(150) NULL;");
                        if (con.query("SHOW COLUMNS FROM usuarios WHERE Field = 'adminempresa' ").Tables[0].Rows.Count == 0)
                            con.query("ALTER TABLE usuarios ADD COLUMN adminempresa int(2) DEFAULT 0;");
                        if (con.query("SHOW COLUMNS FROM usuarios WHERE Field = 'codorganizador' ").Tables[0].Rows.Count == 0)
                            con.query("ALTER TABLE usuarios ADD COLUMN codorganizador VARCHAR(150) NULL;");
                        if (con.query("SHOW COLUMNS FROM usuarios WHERE Field = 'version' ").Tables[0].Rows.Count == 0)
                            con.query("ALTER TABLE usuarios ADD COLUMN version int(11) DEFAULT 1;");
                    }
                   

                }
                catch
                {
                    //
                }
            }
            else
            {
                con.query("ALTER TABLE usuarios ADD allow int(2) NULL");
                con.query("ALTER TABLE usuarios ADD comisionprima double DEFAULT 0");
                con.query("ALTER TABLE usuarios ADD comisionpremio double DEFAULT 0");
                con.query("ALTER TABLE usuarios ADD codigoproductor VARCHAR(150) NULL");
                con.query("ALTER TABLE usuarios ADD codestado int(1) DEFAULT 1");
                con.query("ALTER TABLE usuarios ADD COLUMN codempresa VARCHAR(150) NULL;");
                con.query("ALTER TABLE usuarios ADD COLUMN adminempresa int(2) DEFAULT 0;");
                con.query("ALTER TABLE usuarios ADD COLUMN codorganizador VARCHAR(150) NULL;");
                con.query("ALTER TABLE usuarios ADD COLUMN version int(11) DEFAULT 1;");
            }


        }
        public  byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public  string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public bool exist()
        {
            sql = "SELECT id,loggin,version FROM usuarios WHERE loggin = '" + this.loggin + "'";
            if (con.query(sql).Tables[0].Rows.Count > 0)
            {
                if(this.version < 2)
                    this.version = Convert.ToInt16(con.query(sql).Tables[0].Rows[0]["version"].ToString()) + 1;
                return true;
            }

            return false;
        }



        public bool save()
        {
            try
            {
                this.codempresa = MDIParent1.codempresa;
                if (this.exist())
                {

                    if (this.pass != "")
                    {
                        if (!this.passhash)
                        {
                            this.pass = GetHashString(this.pass);
                            sql = "UPDATE usuarios SET " +
                            "nombre = '" + this.nombre + "'," +
                            "loggin = '" + this.loggin + "'," +
                            "pass = '" + this.pass + "'," +
                            "perfil = '" + this.perfil + "'," +
                            "mail = '" + this.mail + "'," +
                            "allow = '" + this.allow + "'," +
                            "comisionpremio = '" + this.comisionpremio + "'," +
                            "comisionprima = '" + this.comisionprima + "'," +
                            "codigoproductor = '" + this.codigoproductor + "'," +
                            "codorganizador = '" + this.codorganizador + "'," +
                            "codempresa = '" + MDIParent1.codempresa + "'," +
                            "adminempresa = '" + this.adminempresa + "'," +
                            "codestado = '1'," +
                            "version = '" + this.version + "' " +

                            " WHERE loggin = '" + this.loggin + "'";
                        }
                        else
                        {
                            sql = "UPDATE usuarios SET " +
                            "nombre = '" + this.nombre + "'," +
                            "loggin = '" + this.loggin + "'," +
                            "pass = '" + this.pass + "'," +
                            "perfil = '" + this.perfil + "'," +
                            "mail = '" + this.mail + "'," +
                            "allow = '" + this.allow + "'," +
                            "comisionpremio = '" + this.comisionpremio + "'," +
                            "comisionprima = '" + this.comisionprima + "'," +
                            "codigoproductor = '" + this.codigoproductor + "'," +
                            "codorganizador = '" + this.codorganizador + "'," +
                            "codempresa = '" + MDIParent1.codempresa + "'," +
                            "adminempresa = '" + this.adminempresa + "'," +
                            "codestado = '1'," +
                            "version = '" + this.version + "' " +

                            " WHERE loggin = '" + this.loggin + "'";
                        }

                    }
                    else
                    {
                        sql = "UPDATE usuarios SET " +
                            "nombre = '" + this.nombre + "'," +
                            "loggin = '" + this.loggin + "'," +
                            "perfil = '" + this.perfil + "'," +
                            "mail = '" + this.mail + "'," +
                            "allow = '" + this.allow + "'," +
                            "comisionprima = '" + this.comisionprima + "'," +
                            "comisionpremio = '" + this.comisionpremio + "'," +
                            "codigoproductor = '" + this.codigoproductor + "'," +
                            "codempresa = '" + MDIParent1.codempresa + "'," +
                            "codorganizador = '" + this.codorganizador + "'," +
                            "adminempresa = '" + this.adminempresa + "'," +
                            "codestado = '1'," +
                            "version = '" + this.version + "' " +

                            " WHERE loggin = '" + this.loggin + "'";
                    }

                }
                else
                {
                    if (!this.passhash)
                    {
                        this.pass = GetHashString(this.pass);
                        sql = "INSERT INTO usuarios (" + this.columns + ") VALUES(" +
                        "'" + this.loggin + "'," +
                        "'" + this.pass + "'," +
                        "'" + this.nombre + "'," +
                        "'" + this.mail + "'," +
                        "'" + this.perfil + "'," +
                        "'" + this.allow + "'," +
                        "'" + this.comisionprima + "'," +
                        "'" + this.comisionpremio + "'," +
                        "'" + this.codigoproductor + "'," +
                        "'" + this.codestado + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + this.adminempresa + "'," +
                        "'" + this.codorganizador + "'," +
                        "'" + this.version + "'" +
                        ") ";
                    }
                    else
                    {
                        sql = "INSERT INTO usuarios (" + this.columns + ") VALUES(" +
                        "'" + this.loggin + "'," +
                        "'" + this.pass + "'," +
                        "'" + this.nombre + "'," +
                        "'" + this.mail + "'," +
                        "'" + this.perfil + "'," +
                        "'" + this.allow + "'," +
                        "'" + this.comisionprima + "'," +
                        "'" + this.comisionpremio + "'," +
                        "'" + this.codigoproductor + "'," +
                        "'" + this.codestado + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + this.adminempresa + "'," +
                        "'" + this.codorganizador + "'," +
                        "'" + this.version + "'" +
                        ") ";
                    }
                }

                con.query(sql);

                Task.Run(() => {

                    if (this.id == null || this.id == "")
                    {
                        sql = $"SELECT id FROM usuarios where loggin = '{this.loggin}' limit 1";
                        DataSet dsCons = con.query(sql);
                        if (dsCons.Tables[0].Rows.Count > 0)
                            this.id = dsCons.Tables[0].Rows[0]["id"].ToString();
                    }

                    ApiUsuarios apus = new ApiUsuarios();
                    List<usuarios> lsus = new List<usuarios>();
                    lsus.Add(this);
                    apus.Post(lsus);
                });

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public bool save_import()
        {
            try
            {
                if(this.codempresa == "")
                    this.codempresa = MDIParent1.codempresa;

                if (this.exist())
                {

                    if (this.pass != "")
                    {
                        if (!this.passhash)
                        {
                            this.pass = GetHashString(this.pass);
                            sql = "UPDATE usuarios SET " +
                            "nombre = '" + this.nombre + "'," +
                            "loggin = '" + this.loggin + "'," +
                            "pass = '" + this.pass + "'," +
                            "perfil = '" + this.perfil + "'," +
                            "mail = '" + this.mail + "'," +
                            "allow = '" + this.allow + "'," +
                            "comisionpremio = '" + this.comisionpremio + "'," +
                            "comisionprima = '" + this.comisionprima + "'," +
                            "codigoproductor = '" + this.codigoproductor + "'," +
                            "codorganizador = '" + this.codorganizador + "'," +
                            "codempresa = '" + this.codempresa + "'," +
                            "adminempresa = '" + this.adminempresa + "'," +
                            "codestado = '1'," +
                            "version = '" + this.version + "' " +

                            " WHERE loggin = '" + this.loggin + "'";
                        }
                        else
                        {
                            sql = "UPDATE usuarios SET " +
                            "nombre = '" + this.nombre + "'," +
                            "loggin = '" + this.loggin + "'," +
                            "pass = '" + this.pass + "'," +
                            "perfil = '" + this.perfil + "'," +
                            "mail = '" + this.mail + "'," +
                            "allow = '" + this.allow + "'," +
                            "comisionpremio = '" + this.comisionpremio + "'," +
                            "comisionprima = '" + this.comisionprima + "'," +
                            "codigoproductor = '" + this.codigoproductor + "'," +
                            "codorganizador = '" + this.codorganizador + "'," +
                            "codempresa = '" + this.codempresa + "'," +
                            "adminempresa = '" + this.adminempresa + "'," +
                            "codestado = '1'," +
                            "version = '" + this.version + "' " +

                            " WHERE loggin = '" + this.loggin + "'";
                        }

                    }
                    else
                    {
                        sql = "UPDATE usuarios SET " +
                            "nombre = '" + this.nombre + "'," +
                            "loggin = '" + this.loggin + "'," +
                            "perfil = '" + this.perfil + "'," +
                            "mail = '" + this.mail + "'," +
                            "allow = '" + this.allow + "'," +
                            "comisionprima = '" + this.comisionprima + "'," +
                            "comisionpremio = '" + this.comisionpremio + "'," +
                            "codigoproductor = '" + this.codigoproductor + "'," +
                            "codempresa = '" + this.codempresa + "'," +
                            "codorganizador = '" + this.codorganizador + "'," +
                            "adminempresa = '" + this.adminempresa + "'," +
                            "codestado = '1'," +
                            "version = '" + this.version + "' " +

                            " WHERE loggin = '" + this.loggin + "'";
                    }

                }
                else
                {
                    if (!this.passhash)
                    {
                        this.pass = GetHashString(this.pass);
                        sql = "INSERT INTO usuarios (" + this.columns + ") VALUES(" +
                        "'" + this.loggin + "'," +
                        "'" + this.pass + "'," +
                        "'" + this.nombre + "'," +
                        "'" + this.mail + "'," +
                        "'" + this.perfil + "'," +
                        "'" + this.allow + "'," +
                        "'" + this.comisionprima + "'," +
                        "'" + this.comisionpremio + "'," +
                        "'" + this.codigoproductor + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.codempresa + "'," +
                        "'" + this.adminempresa + "'," +
                        "'" + this.codorganizador + "'," +
                        "'" + this.version + "'" +
                        ") ";
                    }
                    else
                    {
                        sql = "INSERT INTO usuarios (" + this.columns + ") VALUES(" +
                        "'" + this.loggin + "'," +
                        "'" + this.pass + "'," +
                        "'" + this.nombre + "'," +
                        "'" + this.mail + "'," +
                        "'" + this.perfil + "'," +
                        "'" + this.allow + "'," +
                        "'" + this.comisionprima + "'," +
                        "'" + this.comisionpremio + "'," +
                        "'" + this.codigoproductor + "'," +
                        "'" + this.codestado + "'," +
                        "'" + this.codempresa + "'," +
                        "'" + this.adminempresa + "'," +
                        "'" + this.codorganizador + "'," +
                        "'" + this.version + "'" +
                        ") ";
                    }
                }

                Console.WriteLine("\n\n"+sql);
                con.query(sql);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public string concat_sql()
        {
            try
            {
                if (this.exist())
                {

                    if(this.pass != "") {
                        if (!this.passhash)
                        {
                            sql = "UPDATE usuarios SET " +
                            "nombre = '" + this.nombre + "'," +
                            "loggin = '" + this.loggin + "'," +
                            "pass = '" + GetHashString(this.pass) + "'," +
                            "perfil = '" + this.perfil + "'," +
                            "mail = '" + this.mail + "'," +
                            "allow = '" + this.allow + "'," +
                            "comisionpremio = '" + this.comisionpremio + "'," +
                            "comisionprima = '" + this.comisionprima + "'," +
                            "codigoproductor = '" + this.codigoproductor + "'," +
                            "codorganizador = '" + this.codorganizador + "'," +
                            "codempresa = '" + MDIParent1.codempresa + "'," +
                            "adminempresa = '" + this.adminempresa + "'," +
                            "codestado = '1'," +
                            "version = '" + this.version + "' " +

                            " WHERE loggin = '" + this.loggin + "'; ";
                        }
                        else
                        {
                            sql = "UPDATE usuarios SET " +
                            "nombre = '" + this.nombre + "'," +
                            "loggin = '" + this.loggin + "'," +
                            "pass = '" + this.pass + "'," +
                            "perfil = '" + this.perfil + "'," +
                            "mail = '" + this.mail + "'," +
                            "allow = '" + this.allow + "'," +
                            "comisionpremio = '" + this.comisionpremio + "'," +
                            "comisionprima = '" + this.comisionprima + "'," +
                            "codigoproductor = '" + this.codigoproductor + "'," +
                            "codorganizador = '" + this.codorganizador + "'," +
                            "codempresa = '" + MDIParent1.codempresa + "'," +
                            "adminempresa = '" + this.adminempresa + "'," +
                            "codestado = '1'," +
                            "version = '" + this.version + "' " +

                            " WHERE loggin = '" + this.loggin + "'; ";
                        }
                        
                    }
                    else
                    {
                        sql = "UPDATE usuarios SET " +
                            "nombre = '" + this.nombre + "'," +
                            "loggin = '" + this.loggin + "'," +
                            "perfil = '" + this.perfil + "'," +
                            "mail = '" + this.mail + "'," +
                            "allow = '" + this.allow + "'," +
                            "comisionprima = '" + this.comisionprima + "'," +
                            "comisionpremio = '" + this.comisionpremio + "'," +
                            "codigoproductor = '" + this.codigoproductor + "'," +
                            "codorganizador = '" + this.codorganizador + "'," +
                            "codempresa = '" + MDIParent1.codempresa + "'," +
                            "adminempresa = '" + this.adminempresa + "'," +
                            "codestado = '1'," +
                            "version = '" + this.version + "' " +
                            " WHERE loggin = '" + this.loggin + "'; ";
                    }
                    
                }
                else
                {
                    if (!this.passhash)
                    {
                        sql = "INSERT INTO usuarios (" + this.columns + ") VALUES(" +
                        "'" + this.loggin + "'," +
                        "'" + GetHashString(this.pass) + "'," +
                        "'" + this.nombre + "'," +
                        "'" + this.mail + "'," +
                        "'" + this.perfil + "'," +
                        "'" + this.allow + "'," +
                        "'" + this.comisionprima + "'," +
                        "'" + this.comisionpremio + "'," +
                        "'" + this.codigoproductor + "'," +
                        "'" + this.codestado + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + this.adminempresa + "'," +
                        "'" + this.codorganizador + "'," +
                        "'" + this.version + "'" +
                        "); ";
                    }
                    else
                    {
                        sql = "INSERT INTO usuarios (" + this.columns + ") VALUES(" +
                        "'" + this.loggin + "'," +
                        "'" + this.pass + "'," +
                        "'" + this.nombre + "'," +
                        "'" + this.mail + "'," +
                        "'" + this.perfil + "'," +
                        "'" + this.allow + "'," +
                        "'" + this.comisionprima + "'," +
                        "'" + this.comisionpremio + "'," +
                        "'" + this.codigoproductor + "'," +
                        "'" + this.codestado + "'," +
                        "'" + MDIParent1.codempresa + "'," +
                        "'" + this.adminempresa + "'," +
                        "'" + this.codorganizador + "'," +
                        "'" + this.version + "'" +
                        "); ";
                    }
                }

                return sql;

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public void save_sql(string query_)
        {
            con.query(query_);
        }

        public bool versionUpdate(string loggin_, string codempresa_, int version_)
        {
            sql = $"SELECT COUNT(1) as cant FROM  usuarios WHERE loggin = '{loggin_}' AND codempresa = '{codempresa_}' ";
            DataSet ds = con.query(sql);
            if(ds.Tables[0].Rows.Count > 0)
            {
                if(ds.Tables[0].Rows[0]["cant"].ToString() != "0")
                {
                    sql = $"SELECT COUNT(1) as cant FROM  usuarios WHERE loggin = '{loggin_}' AND codempresa = '{codempresa_}' AND version < '{version_}' ";
                    ds = con.query(sql);
                    if (ds.Tables[0].Rows[0]["cant"].ToString() == "0")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public async void updateCreateVersion_import()
        {
            ApiUsuarios apusu = new ApiUsuarios();
            List<usuarios> lsusu = await apusu.Get();
            validaciones val = new validaciones();
            if(val.dataComparacion("api_usuarios", lsusu))
            {
                foreach (usuarios u in lsusu)
                {
                    u.passhash = true;
                    if (versionUpdate(u.loggin, u.codempresa, u.version))
                        u.save_import();
                }
                val.createData0("api_usuarios");
            }
            
        }

        // [0] Vista , [1] edicion , [2] eliminar, [3] exportar
        public int[] accessperfil(string _modulo)
        {
            int[] arr = new int[4];
            arr[0] = 0;
            arr[1] = 0;
            arr[2] = 0;
            arr[3] = 0;

            sql = "SELECT t1.vista,t1.access,t1.edicion,t1.eliminar,t1.exportar FROM perfiles t1 INNER JOIN usuarios t2 " +
                " ON t1.nombre = t2.perfil AND t2.loggin = '"+this.loggin+"' AND t1.modulo = '"+_modulo+"' ";

            DataSet ds = con.query(sql);
            if(ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["vista"].ToString() != "")
                    arr[0] = Convert.ToInt16(ds.Tables[0].Rows[0]["vista"].ToString());
                if (ds.Tables[0].Rows[0]["edicion"].ToString() != "")
                    arr[1] = Convert.ToInt16(ds.Tables[0].Rows[0]["edicion"].ToString());
                if (ds.Tables[0].Rows[0]["eliminar"].ToString() != "")
                    arr[2] = Convert.ToInt16(ds.Tables[0].Rows[0]["eliminar"].ToString());
                if(ds.Tables[0].Rows[0]["exportar"].ToString() != "")
                    arr[3] = Convert.ToInt16(ds.Tables[0].Rows[0]["exportar"].ToString());

                if (Convert.ToInt16(ds.Tables[0].Rows[0]["access"].ToString()) == 1)
                {
                    arr[0] = 1;
                    arr[1] = 1;
                    arr[2] = 1;
                }
            }


            return arr;
        }

        public string get_nombre(string login_)
        {
            DataSet ds = new DataSet();
            sql = "SELECT * FROM usuarios WHERE loggin = '" + login_.Trim() + "' ";
            try
            {
                ds = con.query(sql);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["nombre"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar usuarios " + ex.Message);
            }

            return "";
        }


        public string get_loggin(string nombre_)
        {
            DataSet ds = new DataSet();
            sql = "SELECT * FROM usuarios WHERE nombre = '" + nombre_.Trim() + "' ";
            try
            {
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["loggin"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar usuarios " + ex.Message);
            }

            return "";
        }


        public string get_codproductor(string login_)
        {
            string res = "";
            sql = "SELECT * FROM usuarios WHERE loggin = '" + login_ + "' AND codestado = 1 ";
            try
            {
                DataSet ds = new DataSet();
                ds = con.query(sql);
                if(ds.Tables[0].Rows.Count > 0){
                    res = ds.Tables[0].Rows[0]["codigoproductor"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar código productor " + ex.Message);
            }

            return res;
        }

        public string get_codorganizador(string login_)
        {
            string res = "";
            sql = "SELECT * FROM usuarios WHERE loggin = '" + login_ + "' AND codestado = 1 ";
            try
            {
                DataSet ds = new DataSet();
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    res = ds.Tables[0].Rows[0]["codorganizador"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar código productor " + ex.Message);
            }

            return res;
        }


        public List<string> listorganizador()
        {
            List<string> lsorg = new List<string>();
            lsorg.Add("TODOS");
            sql = "SELECT codorganizador FROM usuarios WHERE codorganizador != '' GROUP BY codorganizador";

            try
            {
                DataSet ds = new DataSet();
                ds = con.query(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lsorg.Add(ds.Tables[0].Rows[i]["codorganizador"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                logs log = new logs();
                log.newError("204", "Error al generar listado de códigos organizadores" + ex.Message);
                Console.Write("ERROR al generar listado de códigos organizadores " + ex.Message);
            }

            return lsorg;
        }

        public DataSet get_all_fecha(string fecha)
        {
            DataSet ds = new DataSet();

            sql = "SELECT * FROM usuarios  WHERE codestado > '0' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar coberturas");
            }

            return ds;
        }


        public DataSet get()
        {
            DataSet ds = new DataSet();
            sql = "SELECT * FROM usuarios WHERE loggin = '"+this.loggin+"' AND codestado = 1 ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar usuarios " + ex.Message);
            }

            return ds;
        }

        public DataSet get_all_comision()
        {
            DataSet ds = new DataSet();
            sql = "SELECT * FROM usuarios t1 INNER JOIN commission_user t2 ON t1.loggin = t2.user_comm WHERE t2.comm_prima != '' OR t2.comm_premio != ''  ORDER BY t1.nombre DESC";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar usuarios con comisión " + ex.Message);
            }

            return ds;
        }

        public DataSet get_all()
        {
            DataSet ds = new DataSet();
            sql = "SELECT * FROM usuarios WHERE codestado = 1 ORDER BY nombre ASC ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar usuarios "+ex.Message);
            }

            return ds;
        }

        public DataSet get_all_allow()
        {
            DataSet ds = new DataSet();
            sql = "SELECT * FROM usuarios WHERE allow = '1' AND codestado = '1' ";
            try
            {
                ds = con.query(sql);
            }
            catch (Exception ex)
            {
                Console.Write("ERROR al consultar usuarios " + ex.Message);
            }

            return ds;
        }

        public bool loggin_pass(string loggin, string pass)
        {
            pass = GetHashString(pass);
            sql = "SELECT loggin, pass FROM usuarios WHERE loggin = '" + loggin +"' ";
            
            DataSet ds = con.query(sql);
            
            if (ds.Tables.Count > 0){
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    if (ds.Tables[0].Rows[0]["loggin"].ToString() == loggin && ds.Tables[0].Rows[0]["pass"].ToString() == pass)
                    {
                        
                        return true;
                    }else
                        return false;
                }
            }

            return false;
        }

        public void delete()
        {
            sql = "UPDATE usuarios SET codestado = '0' WHERE id = '" + this.id + "'";
            con.query(sql);
        }

    }
}
