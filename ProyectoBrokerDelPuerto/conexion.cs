using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.Net;

namespace ProyectoBrokerDelPuerto
{
    class conexion
    {
        public SQLiteConnection connectionSQLite { get; set; }
        public MySqlConnection conecction { get; set; }


        public conexion()
        {
            
            if (MDIParent1.baseDatos == "")
            {
                try
                {
                    this.conectarDB();
                    MDIParent1.baseDatos = "MySql";
                }
                catch (Exception ex)
                {
                    this.conectarDBSQLITE();
                    MDIParent1.baseDatos = "SQlite";
                    System.IO.File.WriteAllText("ip.txt", "");
                    MessageBox.Show("No hay conexión con la base de datos LAN, el sistema se conectará a la base de datos portable \n" + ex.Message, "Sin Conexion LAN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

        }

        public string GetLocalIPAddress()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            //Console.WriteLine(hostName);
            // Get the IP  
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

            return myIP;
        }


        public void conectarDB()
        {
            string ipguardada = "100";
            string usuarioBaseDatos = "remoto";
            string contraBaseDatos = "remoto";
            string nombreBaseDatos = "barriosprivados";

            
            if (!System.IO.File.Exists("ip.txt"))
                System.IO.File.WriteAllText("ip.txt", "");
            

            string[] lineText = System.IO.File.ReadAllLines("ip.txt");
            if (lineText.Count() < 1)
            {
                frmConfigDB configdb = new frmConfigDB();
                configdb.ShowDialog();
                if (configdb.DialogResult == DialogResult.OK)
                {
                    lineText = System.IO.File.ReadAllLines("ip.txt");
                    ipguardada = lineText[0];
                    nombreBaseDatos = lineText[1];
                    usuarioBaseDatos = lineText[2];
                    if(lineText.Count() < 4 )
                        contraBaseDatos = "";
                    else
                        contraBaseDatos = lineText[3];
                }

            }
            else
            {
                lineText = System.IO.File.ReadAllLines("ip.txt");
                ipguardada = lineText[0];
                nombreBaseDatos = lineText[1];
                usuarioBaseDatos = lineText[2];
                if (lineText.Count() < 4)
                    contraBaseDatos = "";
                else
                    contraBaseDatos = lineText[3];
            }


            
                conecction = new MySqlConnection("server=" + ipguardada + $"; database={nombreBaseDatos}; Uid=" + usuarioBaseDatos + "; pwd=" + contraBaseDatos + "; Allow Zero Datetime=True;");
                conecction.Open();
            
            
        }


        public void conectarDBSQLITE()
        {
            try
            {
                string connetionString = "Data Source=dbBroker.db;Version=3;New=True;Compress=True;";
                connectionSQLite = new SQLiteConnection(connetionString);
                connectionSQLite.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }



        public DataSet query(string sql)
        {
            DataSet DS = new DataSet();
            if (MDIParent1.baseDatos == "MySql")
            {
                 /*try
                 {*/
                    this.conectarDB();
                    
                    //instancia del DataSet que viene siendo como una matriz
                    MySqlDataAdapter DP = new MySqlDataAdapter(sql, conecction);//ojo que es con Mysql
                    conecction.Close();
                    DP.Fill(DS);
                    return DS;
                /*}
                catch (Exception ex)
                {
                    Console.WriteLine("Error QUERY "+sql +"\n"+ex.Message);
                    //MessageBox.Show("Error de conexión con la base de datos\n" + ex.Message + "\nQUERY\n"+ sql, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show("No hay conexión con la base de datos");
                    return DS;
                }*/
            }
            else
            {
                /*try
                {*/
                    this.conectarDBSQLITE();
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connectionSQLite);
                    adapter.Fill(DS);
                    connectionSQLite.Close();
                    return DS;
                /*}
                catch (Exception ex)
                {
                    //MessageBox.Show("Error de conexión con la base de datos\n" + ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return DS;
                }*/
            }
            
            
        }

        internal IDisposable BeginTransaction()
        {
            throw new NotImplementedException();
        }
    }
}


public static class Prompt
{
    public static string ShowDialog(string text, string caption)
    {
        Form prompt = new Form()
        {
            Width = 500,
            Height = 150,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = caption,
            StartPosition = FormStartPosition.CenterScreen
        };
        Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
        TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
        Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
        confirmation.Click += (sender, e) => { prompt.Close(); };
        prompt.Controls.Add(textBox);
        prompt.Controls.Add(confirmation);
        prompt.Controls.Add(textLabel);
        prompt.AcceptButton = confirmation;

        return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
    }
}
