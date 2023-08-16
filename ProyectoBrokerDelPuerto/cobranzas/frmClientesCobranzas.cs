using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBrokerDelPuerto.cobranzas
{
    public partial class frmClientesCobranzas : frmClientes
    {
        public int[] access;
        public string rutaPlantilla = "";
        public bool estate = false;
        
        public frmClientesCobranzas()
        {
            InitializeComponent();
        }

        private void frmClientesCobranzas_Load(object sender, EventArgs e)
        {
            this.Text = "Importar Clientes";

            perfiles perf = new perfiles();
            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            //this.access = usu.accessperfil("cobranzas_importarclientes");

            button2.Visible = Convert.ToBoolean(this.access[2]);
            button3.Visible = Convert.ToBoolean(this.access[1]);
            button4.Visible = Convert.ToBoolean(this.access[2]);
            button6.Visible = Convert.ToBoolean(this.access[2]);
            button5.Visible = Convert.ToBoolean(this.access[3]);

        }


        private void button6_Click(object sender, EventArgs e)
        {
            /*try
            {*/
                this.rutaPlantilla = "";
                /*OpenFileDialog openfile = new OpenFileDialog();
                openfile.Filter = "Archivos Excel|*.xls;*.xlsx";
                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    foreach (String file in openfile.FileNames)
                    {
                        this.rutaPlantilla = @file;
                    }
                    */
                string extensionFile = "";
                OpenFileDialog openfile = new OpenFileDialog();
                openfile.Filter = "Archivos Excel|*.xls;*.xlsx";
                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    foreach (String file in openfile.FileNames)
                    {
                        extensionFile = System.IO.Path.GetExtension(openfile.FileName);
                        this.rutaPlantilla = @file;
                        if (extensionFile == ".xls")
                        {
                            this.convertxlstoxlsx(this.rutaPlantilla);
                            this.rutaPlantilla = this.rutaPlantilla + "x";
                        }

                    }
                }
                if (MessageBox.Show("Si alguno de los clientes importados ya existe será actualizado con la información que se está subiendo ¿Desea continuar?", "Validación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.datosArchivoPlantilla();
                }
                //}
            /*}catch(Exception ex)
            {
                MessageBox.Show("No se pudo importar el archivo\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/


        }

        public void convertxlstoxlsx(string path)
        {
            Spire.Xls.Workbook wb = new Spire.Xls.Workbook();
            wb.LoadFromFile(path);
            wb.SaveToFile(path + "x");
        }

        public void mostrarcarga(bool st)
        {
            this.Enabled = !st;
            pictureBox3.Enabled = st;
            pictureBox3.Visible = st;
            if (st == false)
                this.llenarGrid();
            
        }

        private void datosArchivoPlantilla()
        {
            this.estate = true;

            if (MDIParent1.versionwindows != "7")
            {
                this.mostrarcarga(true);
            }
            else{
                lblCarga.Visible = true;
            }

            Task.Run(() => {
                excelDocuments excel = new excelDocuments();
                try
                {
                                               
                    int res = excel.ImportaDatosClientes(this.rutaPlantilla);

                    if (res == 2)
                    {
                        MessageBox.Show("No hay datos para importar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Invoke( new Action(() => this.mostrarcarga(false) ));
                    //this.mostrarcarga(false) ;
                    this.estate = false;
                    lblCarga.Visible = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show("ERROR al importar los datos\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Invoke(new Action(() => this.mostrarcarga(false)));
                    this.estate = false;
                    lblCarga.Visible = false;
                }

            });
            
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea descargar el formato para subir clientes con un archivo?", "Descargar plantilla Clientes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    /*System.Net.WebClient webClient = new System.Net.WebClient();
                    webClient.DownloadFile("clientesbp.xlsx", getDocumentsPath()+ "clientesbp.xlsx");
                    MessageBox.Show("El archivo se ha descargado en la carpeta DOCUMENTOS con el nombre clientesbp.xlsx");
                    System.Diagnostics.Process.Start(getDocumentsPath());*/
                    this.descarga(true);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("No se pudo descargar la plantilla \n"+Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

            }
        }

        public static string getDocumentsPath()
        {
            // Not in .NET 2.0
            // System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            /*if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
                return System.Environment.GetEnvironmentVariable("Downloads");*/

            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\";
        }

       


    }
}
