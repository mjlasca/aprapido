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
    public partial class frmLiquidaciones : Form
    {
        public frmLiquidaciones()
        {
            InitializeComponent();
        }

        private void frmLiquidaciones_Load(object sender, EventArgs e)
        {
            this.datosIniciales();
        }

        private void datosIniciales()
        {
            usuarios us = new usuarios();
            comboBox1.Items.Add("Todos");
            DataSet ds = us.get_all_comision();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(
                    ds.Tables[0].Rows[i]["loggin"].ToString()
                );
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.buscar_datos();
        }

        private void buscar_datos()
        {
            if(comboBox1.Text != "")
            {
                string query_ = "";

                if (comboBox1.Text == "Todos")
                    query_ = "";
                else
                    query_ = comboBox1.Text;
                liquidaciones liq = new liquidaciones();
                DataSet ds = liq.get_all(query_, dateTimePicker1.Value, dateTimePicker2.Value);
                dataGridView1.Rows.Clear();
                for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(
                        ds.Tables[0].Rows[i]["usuario"].ToString(),
                        ds.Tables[0].Rows[i]["user_liquidador"].ToString(),
                        ds.Tables[0].Rows[i]["fecha_inicia"].ToString() +" a " + ds.Tables[0].Rows[i]["fecha_fin"].ToString(),
                        ds.Tables[0].Rows[i]["com_prima"].ToString(),
                        ds.Tables[0].Rows[i]["com_premio"].ToString(),
                        ds.Tables[0].Rows[i]["ultmod"].ToString(),
                        ds.Tables[0].Rows[i]["id"].ToString()

                    );
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            excelDocuments xls = new excelDocuments();
            string ruta = "";

            MDIParent1.rutaInformes_global = ruta;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.xlsx)|*.xls|Todos los archivos (*.*)|*.*";
            saveFileDialog.FileName = "Liquidaciones USUARIO "+comboBox1.Text+ " RANGO "+  dateTimePicker1.Value.ToString("dd-MM-yyyy")+" al "+ dateTimePicker1.Value.ToString("dd-MM-yyyy") + ".xlsx";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                ruta = @FileName;

                //Función para generar base de datos de la tabla en cuestión
                xls.baseExcel(ruta, this.generar_informe());
            }
        }

        private DataTable generar_informe()
        {
            DataTable dt = new DataTable();

            //columnas
            dt.Columns.Add("usuario", typeof(string));
            dt.Columns.Add("liquidador", typeof(string));
            dt.Columns.Add("periodo", typeof(string));
            dt.Columns.Add("comisión prima", typeof(string));
            dt.Columns.Add("comisión premio", typeof(string));
            dt.Columns.Add("fecha liquidación", typeof(string));

            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dt.Rows.Add(
                    dataGridView1.Rows[i].Cells["usuario"].Value,
                    dataGridView1.Rows[i].Cells["liquidador"].Value,
                    dataGridView1.Rows[i].Cells["periodo"].Value,
                    dataGridView1.Rows[i].Cells["comprima"].Value,
                    dataGridView1.Rows[i].Cells["compremio"].Value,
                    dataGridView1.Rows[i].Cells["fecha"].Value
                );
            }
            return dt;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.mostrarDatosDePago();
        }

        private void mostrarDatosDePago()
        {
            if(dataGridView1.Rows.Count > 0)
            {
                
                commission com = new commission();

                DataSet ds = com.get_datospago(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                
                dataGridView2.DataSource = ds.Tables[0];

                if (dataGridView2.Rows.Count > 0)
                {
                    panel1.Visible = true;
                    label3.Visible = true;
                    lblUsuario.Text = "Usuario: "+dataGridView1.CurrentRow.Cells["usuario"].Value.ToString();
                    lblPeriodo.Text = "Periodo: " + dataGridView1.CurrentRow.Cells["periodo"].Value.ToString();

                }
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            label3.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.mostrarDatosDePago();
        }
    }
}
