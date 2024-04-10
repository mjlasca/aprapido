using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace ProyectoBrokerDelPuerto
{
    public partial class frmVersion : Form
    {
        string versOnline = "0";
        public frmVersion()
        {
            InitializeComponent();
        }

        private async void frmVersion_Load(object sender, EventArgs e)
        {
            label1.Text = "Versión " + MDIParent1.versionsistema+ "";
            bool res = await this.validar_version();
            if (res)
            {
                btnActualiza.Text = "Nueva Actualización " + this.versOnline;
                btnActualiza.Visible = true;
            }
            
        }

        public async Task<bool> validar_version()
        {
            try
            {
                var webRequest = WebRequest.Create(MDIParent1.apiuri+"/versionAp.txt");

                using (var response = webRequest.GetResponse())
                using (var content = response.GetResponseStream())
                using (var reader = new StreamReader(content))
                {
                    var strContent = reader.ReadToEnd();
                    if (strContent != "")
                    {
                        if (strContent != "0" && strContent != "")
                        {
                            if (Convert.ToDouble(strContent.Replace(".", ",")) > Convert.ToDouble(MDIParent1.versionsistema.Replace(".",",")))
                            {
                                this.versOnline = strContent;
                                return true;
                            }
                        }
                    }
 
                }


            }
            catch(Exception ex)
            {
                return false;
            }

            return false;


        }

        private async void btnActualiza_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro(a) de actualizar el sistema?", "Actualización", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool res = false;
                try
                {
                    res = await this.descarga_actualizacion();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha podido descargar la actualización\n" + ex,
                        "Error de descarga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    res = false;
                }


                if (res)
                {
                    //System.IO.File.Create("ProyectoBrokerDelPuerto_" + this.versOnline + ".exe");
                    //System.IO.File.Replace("ProyectoBrokerDelPuerto_" + this.versOnline + ".exe", "ProyectoBrokerDelPuerto.exe", "ProyectoBrokerDelPuerto_" + MDIParent1.versionsistema + ".exe");
                    this.deleteDirectAccess();
                    this.createDirectAccess();

                    //System.IO.File.WriteAllText("versionAp.txt", this.versOnline);
                    Task.Run(() =>
                    {
                        ProcessStartInfo info = new ProcessStartInfo();

                        info.UseShellExecute = true;
                        info.FileName = "ProyectoBrokerDelPuerto_" + this.versOnline + ".exe";
                        info.WorkingDirectory = Application.StartupPath;
                        System.Threading.Thread.Sleep(1500);
                        Process.Start(info);
                    });
                    


                    DialogResult = DialogResult.OK;

                }
            }
              
            
        }

        private void deleteDirectAccess()
        {
            string rutaAccesoDirecto = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Acceso_directo_BrokerdelPuerto.lnk");

            if (System.IO.File.Exists(rutaAccesoDirecto))
            {
                System.IO.File.Delete(rutaAccesoDirecto);
                //MessageBox.Show("El acceso directo ha sido eliminado.");
            }
           
        }

        private void createDirectAccess()
        {
            string rutaDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Acceso_directo_BrokerdelPuerto.lnk");

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(rutaDestino);

            string rutaEjecutable = Path.Combine(Application.StartupPath, "ProyectoBrokerDelPuerto_" + this.versOnline + ".exe");

            shortcut.TargetPath = rutaEjecutable;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Description = "Acceso directo a barrios privados";
            // Ruta al archivo de ícono (.ico)
            string rutaIcono = Path.Combine(Application.StartupPath, "logoap.ico");
            shortcut.IconLocation = rutaIcono;

            shortcut.Save();

            MessageBox.Show("El sistema está listo para ser usado, hay un acceso directo en el escritorio con el nombre Acceso_directo_BrokerdelPuerto");
        }

        private async Task<bool> descarga_actualizacion()
        {
            try
            {
                WebClient mywebClient = new WebClient();
                mywebClient.DownloadFile(MDIParent1.apiuri+"/ProyectoBrokerDelPuerto.exe", "ProyectoBrokerDelPuerto_" + this.versOnline + ".exe");
                mywebClient.DownloadFile(MDIParent1.apiuri+"/filelist.txt", "filelist.txt");
                string fileListContent = System.IO.File.ReadAllText("filelist.txt");
                    
                string[] fileUrls = fileListContent.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string fileUrl in fileUrls)
                {
                    mywebClient.DownloadFile(MDIParent1.apiuri + fileUrl, fileUrl.Replace("/downloadfiles/", ""));
                    Console.WriteLine($"Descargado: {MDIParent1.apiuri + fileUrl} -> {fileUrl.Replace("/downloadfiles/", "")}");
                }

                return true;
            }catch(Exception ex)
            {
                Console.WriteLine("Hay un error al descargar el archivo de actualización \n"+ex);
                return false;
            }
        }
    }
}
