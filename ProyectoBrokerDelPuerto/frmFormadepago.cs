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
using System.Text.RegularExpressions;

namespace ProyectoBrokerDelPuerto
{
    public partial class frmFormadepago : Form
    {
        public decimal valorAPAgar = 0;
        public DataSet ds = new DataSet();
        public List<string> ruta = new List<string>();
        public bool comprobanteExistente = false;
        public frmFormadepago()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageProcess.Enabled = true;
            label2.Visible = false;
            textBox1.Visible = false;

            if (comboBox1.Text == "OTRO")
            {
                label2.Visible = true;
                textBox1.Visible = true;
            }
            if(comboBox1.Text == "EFECTIVO")
            {
                imageProcess.Enabled = false;
                this.ruta = new List<string>();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                string val = this.validation();
                if (val == "valormayor")
                    return;
                if (val == "")
                {
                    if (MessageBox.Show("¿Está seguro(a) que la fecha es " + fecha_comprobante.Value.ToString("dd/MM/yyyy") + " ?", "Confirmar pago", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
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

            if(comboBox1.Text != "EFECTIVO" && textBox2.Text == "")
            {
                error += "\nEl número de comprobante es obligatorio";
            }else
            {
                string patron = @"^[a-zA-Z0-9]+(?:-[a-zA-Z0-9]+)*$";


                if (!Regex.IsMatch(textBox2.Text, patron))
                {
                    error += "\nEl número de comprobante no está bien escrito\nRecuerde: Alfanumérico y si son varios separados por guiones (21546464-f545464...)" ;
                }else
                {
                    string[] partes = textBox2.Text.Split('-');
                    propuestas pr = new propuestas();
                    
                    foreach (string parte in partes)
                    {
                        string resultS = pr.getNumComprobante(parte);
                        if (comboBox1.Text != "EFECTIVO" && resultS != "" && MessageBox.Show("¿El número de comprobante ya fue ingresado (" + resultS + ") anteriormente, ¿Desea continuar?", "Comprobante existente", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            error += $"\nEl número de comprobante {parte} ya fue ingresado\nel comprobante se registró en la propuesta " + resultS;
                        }
                        else
                        {
                            this.comprobanteExistente = true;
                        }
                    }
                }
            }


            if (ValorPagado_txt.Text == "")
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

            if (comboBox1.Text != "EFECTIVO" && this.ruta.Count == 0)
            {
                error += "\nDebe cargar la imagen del comprobante";
            }

            if(DateTime.Now.Date.CompareTo(fecha_comprobante.Value.Date) < 0)
            {
                error += "\nLa fecha no puede ser mayor a la actual";
            }

            if(textBox2.Text != "" && comboBox1.Text != "EFECTIVO")
            {
                string[] parts = textBox2.Text.Split('-');
                if(this.ruta.Count < parts.Length)
                {
                    error += $"\nSólo ha cargado {this.ruta.Count} de {parts.Length} archivos";
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
            fecha_comprobante.Value = DateTime.Now;
        }

        private void imageProcess_Click(object sender, EventArgs e)
        {
            string patron = @"^[a-zA-Z0-9]+(?:-[a-zA-Z0-9]+)*$";

            if (textBox2.Text == "")
            {
                MessageBox.Show("Por favor ingrese el número de comprobante primero", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox2.Text != "" && !Regex.IsMatch(textBox2.Text, patron))
            {
                MessageBox.Show("El número de comprobante no está bien escrito\nRecuerde: Alfanumérico y si son varios separados por guiones (21546464-f545464...)", "Error de validacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }else
            {
                string[] parts = textBox2.Text.Split('-');

                if(parts.Length > this.ruta.Count)
                {
                    

                    OpenFileDialog openfile = new OpenFileDialog();
                    openfile.Filter = "Archivos de Imagen/PDF|*.jpg;*.jpeg;*.png;*.gif;*.pdf";
                    if (openfile.ShowDialog() == DialogResult.OK)
                    {
                        foreach (String file in openfile.FileNames)
                        {
                            this.ruta.Add(@file);
                        }

                        if (this.ruta.Count > 0)
                        {
                            imageProcess.Text = $"Imagen {parts[this.ruta.Count - 1]} Cargada";
                            //imageProcess.BackColor = Color.Green;
                            string varios = this.ruta.Count == 1 ? "imagen" : "imágenes";

                            label8.Text = $"Se ha cargado {this.ruta.Count} {varios} de {parts.Length} comprobantes";
                        }
                    }
                    
                }
                else
                {
                    if (MessageBox.Show("Ya se ha cargado todos los archivos ¿Quiere volver a cargarlos?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.ruta = new List<string>();
                        label8.Text = "";
                        imageProcess.Text = "Guardar Imagen";
                        imageProcess.BackColor = Color.Gray;
                    }
                }

            }
            
        }

        public void processImage()
        {
            if(this.ruta.Count > 0)
            {
                string carpetaDestino = @textBox3.Text + @"\" + fecha_comprobante.Value.ToString("dd-MM-yyyy");

                // Crear la carpeta si no existe
                if (!Directory.Exists(carpetaDestino))
                {
                    Directory.CreateDirectory(carpetaDestino);
                }
                
                for(int i= 0; i < this.ruta.Count; i++)
                {
                    string rut = this.ruta[i];
                    string comp = textBox2.Text.Split('-')[i];
                    string extension = Path.GetExtension(rut);
                    string rutaDestino = Path.Combine(carpetaDestino, ds.Tables[0].Rows[0]["documento"].ToString() + "-" + comp + "-" + ds.Tables[0].Rows[0]["prefijo"].ToString() + ds.Tables[0].Rows[0]["idpropuesta"].ToString()); // Combinar la carpeta de destino con el nombre del archivo


                    string[] archivos = Directory.GetFiles(carpetaDestino, "*-" + comp + "-*");

                    if (textBox2.Text != "" && archivos.Length > 0)
                    {
                        // Cambiar el nombre de los archivos encontrados
                        foreach (string archivo in archivos)
                        {
                            string nombreArchivoSinExtension = Path.GetFileNameWithoutExtension(archivo);
                            string extensionArchivo = Path.GetExtension(archivo);
                            string nuevoNombre = nombreArchivoSinExtension + "_" + ds.Tables[0].Rows[0]["prefijo"].ToString() + ds.Tables[0].Rows[0]["idpropuesta"].ToString() + extensionArchivo;
                            string nuevoNombreArchivo = Path.Combine(Path.GetDirectoryName(archivo), nuevoNombre);
                            File.Move(archivo, nuevoNombreArchivo);
                        }
                    }
                    else
                    {
                        // Copiar el archivo
                        File.Copy(rut, rutaDestino + extension, true); // El tercer parámetro indica si se sobreescribe el archivo si ya existe
                    }
                }
                
                
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
            {
                // Realiza la acción que desees cuando se detecte el guion -
                Console.WriteLine("Se ha ingresado el guion -");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.ruta = new List<string>();
            label8.Text = "";
            imageProcess.Text = "Guardar Imagen";
            imageProcess.BackColor = Color.Gray;
        }
    }
}
