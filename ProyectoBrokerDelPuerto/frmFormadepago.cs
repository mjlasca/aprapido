using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProyectoBrokerDelPuerto
{
    public partial class frmFormadepago : Form
    {
        public decimal valorAPAgar = 0;
        public DataSet ds = new DataSet();
        private string ruta = "";
        public frmFormadepago()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Visible = false;
            textBox1.Visible = false;

            if (comboBox1.Text == "OTRO")
            {
                label2.Visible = true;
                textBox1.Visible = true;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string val = this.validation();
            if (val == "valormayor")
                return;
            if ( val == "")
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Hay errores de validación: " + val, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string validation()
        {
            string error = "";
            if(comboBox1.Text == "")
            {
                error += "\nSeleccione la forma de pago";
            }else
            {
                if(comboBox1.Text == "OTRO")
                {
                    if(textBox1.Text == "")
                        error += "\nHa seleccionado otra forma de pago pero no se ha especificado";
                }
            }
            if(ValorPagado_txt.Text == "")
            {
                error += "\nEl valor pagado no puede estar vacío";
            }else
            {
                try
                {
                    decimal valorpagado = Convert.ToDecimal(ValorPagado_txt.Text);
                    decimal valorpoliza = Convert.ToDecimal(ValorPoliza_txt.Text);
                    if (valorpoliza > valorpagado)
                        error += "\nEl valor pagado no es igual al valor de la póliza";
                    else if(valorpoliza < valorpagado)
                    {
                        if (MessageBox.Show("El valor del comprobante es mayor a la póliza, ¿desea continuar?", "Valor pago comprobante", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return "valormayor";
                        }
                    }
                }
                catch (Exception ex)
                {
                    error += "\nValor inválido";
                }
            }

            


            return error;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(this.Width > 500)
            {
                button2.Text = ">";
                this.Width = 305;
            }
            else
            {
                button2.Text = "<";
                this.Width = 550;
            }
            
        }

        private void configSavve_Click(object sender, EventArgs e)
        {
            configuraciones confiprosimport = new configuraciones();
            confiprosimport.dato = "ruta_drive";
            confiprosimport.detail = textBox3.Text.Replace(@"\","-");
            confiprosimport.save();
        }

        private void search_Click(object sender, EventArgs e)
        {
            using (var fd = new FolderBrowserDialog())
            {
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fd.SelectedPath))
                {
                    textBox3.Text = fd.SelectedPath;
                }
            }
        }

        private void frmFormadepago_Load(object sender, EventArgs e)
        {
            configuraciones confiprosimport = new configuraciones();
            confiprosimport.get("ruta_drive");
            textBox3.Text = confiprosimport.detail.Replace("-",@"\");
        }

        private void imageProcess_Click(object sender, EventArgs e)
        {
            imageProcess.Text = "Guardar Imagen";
            imageProcess.BackColor = Color.Gray;
            this.ruta = "";
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Archivos de Imagen/PDF|*.jpg;*.jpeg;*.png;*.gif;*.pdf";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in openfile.FileNames)
                {
                    this.ruta = @file;
                }

                if(this.ruta != "")
                {
                    imageProcess.Text = "Imagen Cargada";
                    imageProcess.BackColor = Color.Green;
                }
            }
        }

        public void processImage()
        {
            
            string carpetaDestino = @textBox3.Text + @"\" + DateTime.Now.ToString("dd-MM-yyyy");

            // Crear la carpeta si no existe
            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }
            string extension = Path.GetExtension(this.ruta);
            string rutaDestino = Path.Combine(carpetaDestino, ds.Tables[0].Rows[0]["documento"].ToString() + "-" + ds.Tables[0].Rows[0]["prefijo"].ToString() + ds.Tables[0].Rows[0]["idpropuesta"].ToString() + extension); // Combinar la carpeta de destino con el nombre del archivo

            // Copiar el archivo
            File.Copy(this.ruta, rutaDestino, true); // El tercer parámetro indica si se sobreescribe el archivo si ya existe
        }
    }
}
