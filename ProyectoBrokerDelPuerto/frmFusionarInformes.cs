using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBrokerDelPuerto
{
    public partial class frmFusionarInformes : Form
    {
        public frmFusionarInformes()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string ruta = "";
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Multiselect = true;
            openfile.Filter = "Archivos Excel|*.xls;*.xlsx";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in openfile.FileNames)
                {
                    ruta = @file;
                    listBox1.Items.Add(ruta);
                    ruta = "";
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Condición para saber si hay almenos dos archivos para combinar
            if(listBox1.Items.Count > 1)
            {

                string[] listado = new string[listBox1.Items.Count];

                //Este ciclo guarda el listado de archivos que hay 
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    listado[i] = listBox1.Items[i].ToString();
                }

                string ruta = "";

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                saveFileDialog.Filter = "Archivos de texto (*.xlsx)|*.xls|Todos los archivos (*.*)|*.*";
                saveFileDialog.FileName = "ArchivosAgrupados" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string FileName = saveFileDialog.FileName;
                    ruta = @FileName;
                }


                excelDocuments excel = new excelDocuments();
                excel.FusionExcels(listado, ruta);
            }
            else
            {
                MessageBox.Show("No hay archivos qué agrupar");
            }
            
        }
    }
}
