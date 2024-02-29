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
    public partial class frmImputaciones : Form
    {
        public frmImputaciones()
        {
            InitializeComponent();
        }

        private void frmImputaciones_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            propuestas pro = new propuestas();
            int imputacion_ = -1;
            if (comboBox1.Text == "SI")
                imputacion_ = 1;
            if (comboBox1.Text == "NO")
                imputacion_ = 0;
            string tipo_fecha = comboBox2.Text;
            DataSet ds = pro.getImputaciones(date1.Value, date2.Value, textBox1.Text, textBox2.Text,tipo_fecha, imputacion_);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(
                        ds.Tables[0].Rows[i]["id"].ToString(),
                        ds.Tables[0].Rows[i]["ultmod"].ToString(),
                        ds.Tables[0].Rows[i]["fecha_comprobante"].ToString(),
                        ds.Tables[0].Rows[i]["referencia"].ToString(),
                        ds.Tables[0].Rows[i]["prefijo"].ToString(),
                        ds.Tables[0].Rows[i]["idpropuesta"].ToString(),
                        ds.Tables[0].Rows[i]["premio_total"].ToString(),
                        ds.Tables[0].Rows[i]["valor_pagado"].ToString(),
                        ds.Tables[0].Rows[i]["compformapago"].ToString(),
                        Convert.ToBoolean(ds.Tables[0].Rows[i]["imputacion"]),
                        Convert.ToInt16(ds.Tables[0].Rows[i]["imputacion"])
                    );
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                try
                {
                    List<string> listIn = new List<string>();
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (Convert.ToInt16(dataGridView1.Rows[i].Cells["si"].Value) != 1 && Convert.ToBoolean(dataGridView1.Rows[i].Cells["imputado"].Value))
                        {
                            listIn.Add(dataGridView1.Rows[i].Cells["id"].Value.ToString());
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["imputado"].ReadOnly = true;
                        }
                    }

                    propuestas pro = new propuestas();
                    pro.updateImputaciones(listIn);

                    MessageBox.Show("Se ha hecho las imputaciones con éxito");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hubo un error al generar las imputaciones" + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                excelDocuments xls = new excelDocuments();
                string ruta = "";

                MDIParent1.rutaInformes_global = ruta;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                saveFileDialog.Filter = "Archivos (*.xlsx)|*.xls|Todos los archivos (*.*)|*.*";
                saveFileDialog.FileName = "Imputaciones " + date1.Value.ToString("yyyy-MM-dd") + "-" + date2.Value.ToString("yyyy-MM-dd") + ".xlsx";
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string FileName = saveFileDialog.FileName;
                    ruta = @FileName;

                    //Función para generar base de datos de la tabla en cuestión
                    xls.baseExcel(ruta, this.generar_informe());
                }
            }
           


        }

        private DataTable generar_informe()
        {
            DataTable dt = new DataTable();

            //columnas
            dt.Columns.Add("fecha", typeof(string));
            dt.Columns.Add("referencia", typeof(string));
            dt.Columns.Add("prefijo", typeof(string));
            dt.Columns.Add("idpropuesta", typeof(string));
            dt.Columns.Add("valor_póliza", typeof(double));
            dt.Columns.Add("valor_pagado", typeof(double));
            dt.Columns.Add("no.comprobante", typeof(string));
            dt.Columns.Add("imputado", typeof(string));

            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dt.Rows.Add(
                    dataGridView1.Rows[i].Cells["fecha"].Value.ToString(),
                    dataGridView1.Rows[i].Cells["referencia"].Value.ToString(),
                    dataGridView1.Rows[i].Cells["prefijo"].Value.ToString(),
                    dataGridView1.Rows[i].Cells["idpropuesta"].Value.ToString(),
                    dataGridView1.Rows[i].Cells["suma_total"].Value.ToString(),
                    dataGridView1.Rows[i].Cells["valor_comprobante"].Value.ToString(),
                    dataGridView1.Rows[i].Cells["comprobante"].Value.ToString(),
                    dataGridView1.Rows[i].Cells["imputado"].Value.ToString() == "True" ? "SI" : "NO"
                );
            }
            return dt;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToInt16(dataGridView1.Rows[i].Cells["si"].Value) != 1 && Convert.ToBoolean(dataGridView1.Rows[i].Cells["imputado"].Value))
                {
                    dataGridView1.Rows[i].Cells["imputado"].Value = 0;
                }
                else
                {
                    dataGridView1.Rows[i].Cells["imputado"].Value = 1;
                }
            }
        }

        private void suma_btn_Click(object sender, EventArgs e)
        {
            suma_lbl.Text = "";
            double suma_total = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["imputado"].Value))
                {
                    suma_total += dataGridView1.Rows[i].Cells["valor_comprobante"].Value.ToString() != "" ? Convert.ToDouble(dataGridView1.Rows[i].Cells["valor_comprobante"].Value) : 0;
                }
                
            }
            suma_lbl.Text = "Total valor pagado seleccionado " + suma_total.ToString("c");
        }
    }
}
