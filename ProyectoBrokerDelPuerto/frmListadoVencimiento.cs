using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBrokerDelPuerto
{
    public partial class frmListadoVencimiento : Form
    {
        DataSet dataexport;
        public frmListadoVencimiento()
        {
            InitializeComponent();
        }

        private void frmListadoVencimiento_Load(object sender, EventArgs e)
        {
            this.cargarDatos(15);
        }

        public void cargarDatos(int dias)
        {
            
            propuestas pro = new propuestas();
            DataSet ds = pro.get_vencimientos(DateTime.Now.AddDays(dias).ToString("yyyy-MM-dd"));
            dataGridView1.Rows.Clear();
            if (ds.Tables.Count > 0)
            {
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.dataexport = ds;
                    
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //concatenar_vencidos += "La propuesta No. " + ds.Tables[0].Rows[i]["id"].ToString() + " de " + dd.Tables[0].Rows[0]["apellidos"].ToString() + " " + dd.Tables[0].Rows[0]["nombres"].ToString() + " perderá vigencia en " + (Convert.ToDateTime(ds.Tables[0].Rows[i]["fechaHasta"].ToString()).Day - DateTime.Now.Day) + " días \n ";
                        dataGridView1.Rows.Add(
                                ds.Tables[0].Rows[i]["id"].ToString(),
                                ds.Tables[0].Rows[i]["documento"].ToString(),
                                ds.Tables[0].Rows[i]["apellidos"].ToString() + " " + ds.Tables[0].Rows[i]["nombres"].ToString(),
                                ds.Tables[0].Rows[i]["telefono"].ToString(),
                                ds.Tables[0].Rows[i]["email"].ToString(),
                                ds.Tables[0].Rows[i]["fechaHasta"].ToString()
                        );
                    }
                }
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0 && textBox1.Text.Length < 4 )
            {
                try
                {
                    this.cargarDatos(Convert.ToInt16(textBox1.Text));
                }
                catch
                {
                    //
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                //try
                //{
                    excelDocuments xls = new excelDocuments();
                    string ruta = "";

                    MDIParent1.rutaInformes_global = ruta;
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    saveFileDialog.Filter = "Archivos de texto (*.xlsx)|*.xls|Todos los archivos (*.*)|*.*";
                    saveFileDialog.FileName = "Vencimiento a "+textBox1.Text+ " días del " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";
                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        string FileName = saveFileDialog.FileName;
                        ruta = @FileName;
                        //Función para generar base de datos de la tabla en cuestión
                        xls.baseExcel(ruta, this.generar_informe());
                        MessageBox.Show("Archivo generado con éxito en " + ruta);
                    }
                    
                /*}
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el .xls de vencimientos");
                }*/
            }
        }

        private DataTable generar_informe()
        {
            DataTable dt = new DataTable();

            //columnas
            dt.Columns.Add("Prefijo", typeof(string));
            dt.Columns.Add("Nro Propuesta", typeof(int));
            dt.Columns.Add("Referencia", typeof(int));
            dt.Columns.Add("Nro Documento", typeof(string));
            dt.Columns.Add("Nombre Cliente", typeof(string));
            dt.Columns.Add("Teléfono", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Fecha Nacimiento", typeof(string));
            dt.Columns.Add("Vigencia Desde", typeof(string));
            dt.Columns.Add("Vigencia Hasta", typeof(string));
            dt.Columns.Add("Prima", typeof(double));
            dt.Columns.Add("Premio", typeof(double));
            dt.Columns.Add("Usuario_venta", typeof(string));
            dt.Columns.Add("Paga", typeof(string));
            dt.Columns.Add("Nota Crédito", typeof(int));
            dt.Columns.Add("Forma de Pago", typeof(string));


            for (int i = 0; i < this.dataexport.Tables[0].Rows.Count; i++)
            {

                dt.Rows.Add(
                    this.dataexport.Tables[0].Rows[i]["prefijo"].ToString(),
                    this.dataexport.Tables[0].Rows[i]["idpropuesta"].ToString(),
                    this.dataexport.Tables[0].Rows[i]["referencia"].ToString() != "" ? this.dataexport.Tables[0].Rows[i]["referencia"].ToString() : "0",
                    this.dataexport.Tables[0].Rows[i]["documento"].ToString(),
                    this.dataexport.Tables[0].Rows[i]["apellidos"].ToString() + " " + this.dataexport.Tables[0].Rows[i]["nombres"].ToString(),
                    this.dataexport.Tables[0].Rows[i]["telefono"].ToString(),
                    this.dataexport.Tables[0].Rows[i]["email"].ToString(),
                    this.dataexport.Tables[0].Rows[i]["fecha_nacimiento"].ToString(),
                    this.dataexport.Tables[0].Rows[i]["fechaDesde"].ToString() != "" ? Convert.ToDateTime(this.dataexport.Tables[0].Rows[i]["fechaDesde"].ToString()).ToString("dd/MM/yyyy") : "",
                    this.dataexport.Tables[0].Rows[i]["fechaHasta"].ToString() != "" ? Convert.ToDateTime(this.dataexport.Tables[0].Rows[i]["fechaHasta"].ToString()).ToString("dd/MM/yyyy") : "",
                    this.dataexport.Tables[0].Rows[i]["prima"].ToString() != "" ? this.dataexport.Tables[0].Rows[i]["prima"].ToString() : "0",
                    this.dataexport.Tables[0].Rows[i]["premio"].ToString() != "" ? this.dataexport.Tables[0].Rows[i]["premio"].ToString() : "0",
                    this.dataexport.Tables[0].Rows[i]["user_edit"].ToString(),
                    this.dataexport.Tables[0].Rows[i]["paga"].ToString() == "1" ? "SI" : "NO",
                    this.dataexport.Tables[0].Rows[i]["nota"].ToString() != "" ? Convert.ToDouble(this.dataexport.Tables[0].Rows[i]["nota"].ToString()) : 0,
                    this.dataexport.Tables[0].Rows[i]["formadepago"].ToString() != "" ? this.dataexport.Tables[0].Rows[i]["formadepago"].ToString() : ""
                );
            }
            return dt;
        }

        private void frmListadoVencimiento_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width - 40;
            dataGridView1.Height = this.Height - 80;
        }
    }
}
