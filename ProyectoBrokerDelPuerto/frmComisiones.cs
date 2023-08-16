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
    public partial class frmComisiones : Form
    {
        int[] access;
        public frmComisiones()
        {
            InitializeComponent();
        }

        private void frmComisiones_Load(object sender, EventArgs e)
        {
            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("nuevacomision");

            button1.Visible = Convert.ToBoolean(this.access[1]);

            this.datos_iniciales();
        }

        private void datos_iniciales()
        {
            usuarios usu = new usuarios();
            DataSet ds = usu.get_all_comision();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                
                usuario_txt.Items.Add(ds.Tables[0].Rows[i]["loggin"].ToString());
            }

            ds = usu.get_all_allow();
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                
                liquidadores_txt.Items.Add(ds.Tables[0].Rows[i]["loggin"].ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.revisarestado1();
        }

        private void revisarestado1()
        {
            if (usuario_txt.Text != "")
            {
                comisiones com = new comisiones();
                DataSet ds = com.get_range_commission(dateTimePicker1.Value, dateTimePicker2.Value, usuario_txt.Text);

                dataGridView1.Rows.Clear();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    double totalPremio = 0, totalPrima = 0, comPrima = 0, comPremio = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if(ds.Tables[0].Rows[i]["porc_compremio"].ToString() != "" && ds.Tables[0].Rows[i]["porc_compremio"].ToString() != "")
                        {
                            dataGridView1.Rows.Add(
                            ds.Tables[0].Rows[i]["idcomision"].ToString(),
                            ds.Tables[0].Rows[i]["fecha_paga"].ToString(),
                            ds.Tables[0].Rows[i]["prefijo"].ToString() ,
                            ds.Tables[0].Rows[i]["propuesta"].ToString(),
                            ds.Tables[0].Rows[i]["referencia"].ToString(),
                            ds.Tables[0].Rows[i]["tomador"].ToString(),
                            ds.Tables[0].Rows[i]["prima"].ToString(),
                            ds.Tables[0].Rows[i]["premio_total"].ToString(),
                            ds.Tables[0].Rows[i]["porc_comprima"].ToString(),
                            ds.Tables[0].Rows[i]["porc_compremio"].ToString(),
                            ((Convert.ToDouble(ds.Tables[0].Rows[i]["prima"].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i]["porc_comprima"].ToString())) / 100),
                            ((Convert.ToDouble(ds.Tables[0].Rows[i]["premio_total"].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i]["porc_compremio"].ToString())) / 100)

                        );

                            totalPremio += Convert.ToDouble(ds.Tables[0].Rows[i]["premio_total"].ToString());
                            totalPrima += Convert.ToDouble(ds.Tables[0].Rows[i]["prima"].ToString());

                            comPrima += ((Convert.ToDouble(ds.Tables[0].Rows[i]["prima"].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i]["porc_comprima"].ToString())) / 100);
                            comPremio += ((Convert.ToDouble(ds.Tables[0].Rows[i]["premio_total"].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i]["porc_compremio"].ToString())) / 100);

                        }

                    }

                    txttotalPremio.Text = totalPremio.ToString();
                    txttotalPrima.Text = totalPrima.ToString();
                    txtcomPremio.Text = comPremio.ToString();
                    txtcomPrima.Text = comPrima.ToString();
                    usuario_txt.Enabled = false;
                    groupBox1.Visible = true;
                }

            }

        }

        private void revisarestado()
        {
            if(usuario_txt.Text != "")
            {
                comisiones com = new comisiones();
                DataSet ds = com.get_rango(dateTimePicker1.Value,dateTimePicker2.Value, usuario_txt.Text);

                dataGridView1.Rows.Clear();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    double totalPremio=0,totalPrima = 0,comPrima = 0,comPremio = 0;
                    for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(
                            ds.Tables[0].Rows[i]["idcomision"].ToString(),
                            ds.Tables[0].Rows[i]["fecha_paga"].ToString(),
                            ds.Tables[0].Rows[i]["prefijo"].ToString()+
                            ds.Tables[0].Rows[i]["propuesta"].ToString(),
                            ds.Tables[0].Rows[i]["referencia"].ToString(),
                            ds.Tables[0].Rows[i]["tomador"].ToString(),
                            ds.Tables[0].Rows[i]["prima"].ToString(),
                            ds.Tables[0].Rows[i]["premio_total"].ToString(),
                            ds.Tables[0].Rows[i]["porc_comprima"].ToString(),
                            ds.Tables[0].Rows[i]["porc_compremio"].ToString(),
                            ( ( Convert.ToDouble(ds.Tables[0].Rows[i]["prima"].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i]["porc_comprima"].ToString())) / 100 ),
                            ((Convert.ToDouble(ds.Tables[0].Rows[i]["premio_total"].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i]["porc_compremio"].ToString())) / 100)

                        );

                        totalPremio += Convert.ToDouble(ds.Tables[0].Rows[i]["premio_total"].ToString());
                        totalPrima += Convert.ToDouble(ds.Tables[0].Rows[i]["prima"].ToString());
                        
                        comPrima += ((Convert.ToDouble(ds.Tables[0].Rows[i]["prima"].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i]["porc_comprima"].ToString())) / 100);
                        comPremio += ((Convert.ToDouble(ds.Tables[0].Rows[i]["premio_total"].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i]["porc_compremio"].ToString())) / 100);
                        
                    }

                    txttotalPremio.Text = totalPremio.ToString();
                    txttotalPrima.Text = totalPrima.ToString();
                    txtcomPremio.Text = comPremio.ToString();
                    txtcomPrima.Text = comPrima.ToString();
                    usuario_txt.Enabled = false;
                    groupBox1.Visible = true;
                }
                
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usuarios user = new usuarios();
            if (!user.loggin_pass(liquidadores_txt.Text, claveliquidador_txt.Text))
            {
                MessageBox.Show("La clave del usuario liquidador no es válida");
                return;
            }

            

            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                commission comm = new commission();
                comm.prefijo = dataGridView1.Rows[i].Cells["prefijo"].Value.ToString();
                comm.idpropuesta = dataGridView1.Rows[i].Cells["propuesta"].Value.ToString();
                comm.user = usuario_txt.Text.Trim();
                comm.porc_comprima = dataGridView1.Rows[i].Cells["comprima"].Value.ToString();
                comm.porc_compremio = dataGridView1.Rows[i].Cells["compremio"].Value.ToString();
                comm.fecha_paga_propuesta =  Convert.ToDateTime( dataGridView1.Rows[i].Cells["fecha"].Value.ToString() ).ToString("yyyy-MM-dd");
                comm.save();
            }

                liquidaciones liq = new liquidaciones();
                liq.usuario = usuario_txt.Text;
                liq.user_liquidador = liquidadores_txt.Text;
                liq.fecha_inicia = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                liq.fecha_fin = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                liq.prima = txttotalPrima.Text.Replace(",",".");
                liq.premio = txttotalPremio.Text.Replace(",", ".");
                if (txtcomPrima.Text == "")
                    txtcomPrima.Text = "0";
                liq.com_prima = txtcomPrima.Text.Replace(",", ".");
                liq.com_premio = txtcomPremio.Text.Replace(",", ".");
                liq.useredit = MDIParent1.sesionUser;
                liq.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (liq.save())
                {
                    MessageBox.Show("Usuario liquidado con éxito");

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    saveFileDialog.Filter = "Documento PDF (*.pdf)|*.pdf";
                    saveFileDialog.FileName = "FinDelDia" + dateTimePicker1.Value.ToString("dd-MM-yyyy")+"a"+ dateTimePicker2.Value.ToString("dd-MM-yyyy") + ".pdf";
                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        string FileName = saveFileDialog.FileName;
                        generarPdfs genpdf = new generarPdfs();
                        genpdf.pdfLiquidacion(
                            dataGridView1,
                            dateTimePicker1.Value,
                            dateTimePicker2.Value,
                            Convert.ToDouble(txttotalPrima.Text), 
                            Convert.ToDouble(txttotalPremio.Text), 
                            Convert.ToDouble(txtcomPrima.Text), 
                            Convert.ToDouble(txtcomPremio.Text),
                            liq,
                            FileName,
                            usuario_txt.Text,
                            liq.ultmod
                        );
                    }

                this.limpiar();
            }
            
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.limpiar();
        }
        private void limpiar()
        {
            usuario_txt.Enabled = true;
            liquidadores_txt.Text = "";
            claveliquidador_txt.Text = "";
            groupBox1.Visible = false;
            dataGridView1.Rows.Clear();
            txtcomPremio.Text = "";
            txtcomPrima.Text = "";
            txttotalPremio.Text = "";
            txttotalPrima.Text = "";
            usuario_txt.Text = "";
        }
    }
}
