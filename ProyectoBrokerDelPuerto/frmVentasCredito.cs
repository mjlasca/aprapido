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
    public partial class frmVentasCredito : Form
    {
        int[] access;
        public frmVentasCredito()
        {
            InitializeComponent();
        }

        private void frmVentasCredito_Load(object sender, EventArgs e)
        {
            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("ventascredito");
            button2.Visible = Convert.ToBoolean(this.access[1]);
            button3.Visible = Convert.ToBoolean(this.access[3]);

            comboBox1.Items.Add("Todos");
            DataSet dsusu = usu.get_all();
            for(int i = 0; i< dsusu.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(
                        dsusu.Tables[0].Rows[i]["loggin"].ToString()
                );
            }

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            this.consultapropuestas();
        }

        public void consultapropuestas()
        {
            propuestas pro = new propuestas();

            bool quienpaga = false;
            if (comboBox2.Text == "Quien Paga")
                quienpaga = true;

            string usuarioconsulta = comboBox1.Text;
            if (usuarioconsulta == "Todos")
                usuarioconsulta = "";
            string sinpagar = " >= '0'";
            if (comboBox3.Text == "Pagas")
                sinpagar = " = '1' ";
            if (comboBox3.Text == "Sin Pagar")
                sinpagar = " = '0' ";

            DataSet ds  = pro.get_all_credito(dateTimePicker1.Value, dateTimePicker2.Value,quienpaga,usuarioconsulta,sinpagar);
            dataGridView1.Rows.Clear();
            for(int i =0; i < ds.Tables[0].Rows.Count; i++)
            {
                
                dataGridView1.Rows.Add(
                    ds.Tables[0].Rows[i]["ultmod"].ToString(),
                    ds.Tables[0].Rows[i]["prefijo"].ToString(),
                    ds.Tables[0].Rows[i]["idpropuesta"].ToString(),
                    ds.Tables[0].Rows[i]["referencia"].ToString(),
                    ds.Tables[0].Rows[i]["nombre"].ToString(),
                    ds.Tables[0].Rows[i]["premio_total"].ToString(),
                    ds.Tables[0].Rows[i]["master"].ToString(),
                    ds.Tables[0].Rows[i]["organizador"].ToString(),
                    ds.Tables[0].Rows[i]["productor"].ToString(),
                    ds.Tables[0].Rows[i]["user_edit"].ToString(),
                    ds.Tables[0].Rows[i]["fecha_paga"].ToString().IndexOf("1000") > 0 ? "" : ds.Tables[0].Rows[i]["fecha_paga"].ToString(),
                    ds.Tables[0].Rows[i]["usuariopaga"].ToString(),
                    ds.Tables[0].Rows[i]["tipopago"].ToString(),
                    ds.Tables[0].Rows[i]["compformapago"].ToString()

                );
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.consultapropuestas();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            

            if (dataGridView1.CurrentRow.Cells["propuesta"].Value != null)
            {
                if (dataGridView1.CurrentRow.Cells["propuesta"].Value.ToString() != "")
                {
                    if(dataGridView1.CurrentRow.Cells["usuariopaga"].Value.ToString() == "")
                    {
                        

                            if (MessageBox.Show("¿Está segur@ que desea incluir este valor en el arqueo del día? ", "Confirmar pago", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                frmPropuestas frmPro = new frmPropuestas();
                                bool resp = await frmPro.pagopropuesta_(dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString(), dataGridView1.CurrentRow.Cells["propuesta"].Value.ToString());
                                /*string formapago = "";
                                string compformapago = "";

                                frmFormadepago frmpago = new frmFormadepago();

                                frmpago.ShowDialog();


                                if (frmpago.DialogResult == DialogResult.OK)
                                {
                                    formapago = frmpago.comboBox1.Text;

                                    compformapago = frmpago.textBox2.Text;
                                }
                                else
                                {
                                    return;
                                }


                            frmNuevaPropuesta frm = new frmNuevaPropuesta();
                            propuestas pro = new propuestas();
                            if (frm.polizamanana(false))
                            {
                                pro.pagar(
                                    dataGridView1.CurrentRow.Cells["propuesta"].Value.ToString(),
                                    dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString(),
                                    formapago,
                                    compformapago
                                );
                            }
                            else
                            {
                                arqueos ar = new arqueos();
                                usuarios us = new usuarios();
                                ar.fechadia = frm.fechacierresiguientediahabil.ToString("yyyy-MM-dd");
                                ar.usuario = MDIParent1.sesionUser; 
                                ar.valorinicial = "0";
                                ar.dinerorealcaja = "0";
                                ar.valormanual = "0";
                                ar.cuadredescuadre = "0";
                                ar.nombre = us.get_nombre(ar.usuario);
                                ar.user_edit = MDIParent1.sesionUser;
                                ar.ultmod = frm.fechacierresiguientediahabil.ToString("yyyy-MM-dd HH:mm:ss");
                                ar.codestado = "1";
                                ar.save_in();

                                pro.pagar(
                                    dataGridView1.CurrentRow.Cells["propuesta"].Value.ToString(),
                                    dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString(),
                                    formapago,
                                    compformapago,
                                    frm.fechacierresiguientediahabil.ToString("yyyy-MM-dd HH:mm:ss")
                                );
                            }*/

                            this.consultapropuestas();
                        }
                    }
                    
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            excelDocuments xls = new excelDocuments();
            string ruta = "";

            MDIParent1.rutaInformes_global = ruta;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.xlsx)|*.xls|Todos los archivos (*.*)|*.*";
            saveFileDialog.FileName = "Ventas crédito  " + dateTimePicker1.Value.ToString("dd-MM-yyyy") +" - "+ dateTimePicker2.Value.ToString("dd-MM-yyyy") + ".xlsx";
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
            dt.Columns.Add("fecha", typeof(string));
            dt.Columns.Add("prefijo", typeof(string));
            dt.Columns.Add("propuesta", typeof(string));
            dt.Columns.Add("referencia", typeof(string));
            dt.Columns.Add("tomador", typeof(string));
            dt.Columns.Add("premio", typeof(double));
            dt.Columns.Add("master", typeof(string));
            dt.Columns.Add("organizador", typeof(string));
            dt.Columns.Add("productor", typeof(string));
            dt.Columns.Add("usuario", typeof(string));
            dt.Columns.Add("fechapago", typeof(string));
            dt.Columns.Add("usuariopaga", typeof(string));
            dt.Columns.Add("formapago", typeof(string));
            dt.Columns.Add("numreferencia_pago", typeof(string));

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dt.Rows.Add(
                    dataGridView1.Rows[i].Cells["fecha"].Value,
                    dataGridView1.Rows[i].Cells["prefijo"].Value,
                    dataGridView1.Rows[i].Cells["propuesta"].Value,
                    dataGridView1.Rows[i].Cells["referencia"].Value,
                    dataGridView1.Rows[i].Cells["tomador"].Value,
                    dataGridView1.Rows[i].Cells["premio"].Value,
                    dataGridView1.Rows[i].Cells["master"].Value,
                    dataGridView1.Rows[i].Cells["organizador"].Value,
                    dataGridView1.Rows[i].Cells["productor"].Value,
                    dataGridView1.Rows[i].Cells["user"].Value,
                    dataGridView1.Rows[i].Cells["fechapago"].Value,
                    dataGridView1.Rows[i].Cells["usuariopaga"].Value,
                    dataGridView1.Rows[i].Cells["tipopago"].Value,
                    dataGridView1.Rows[i].Cells["comprobante"].Value
                );
            }
            return dt;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["usuariopaga"].Value.ToString() != "")
            {
                button2.Enabled = false;
            }
            else {
                button2.Enabled = true;
            }
        }
    }
}
