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
    public partial class frmRendiciones : Form
    {
        int[] access;
        string detalleArqueo = "";
        string errores = "";
        public frmRendiciones()
        {
            InitializeComponent();
        }

        private async void frmRendiciones_Load(object sender, EventArgs e)
        {
            frmMigraciones frmmig = new frmMigraciones();
            solicitudes s = new solicitudes();
            s.solicitud_arqueos = true;
            s.solicitud_rendiciones = true;
            await frmmig.importarData(s);

            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("rendiciones");

            button1.Visible = Convert.ToBoolean(this.access[1]);
            

            this.datosIniciales();
        }

        private void datosIniciales()
        {
            this.grilla1();
            this.grilla2();
        }

        private void grilla2()
        {

            rendiciones rend = new rendiciones();
            lineas_rendiciones lirend = new lineas_rendiciones();
            DataSet ds = rend.get_all_busqueda(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            dataGridView2.Rows.Clear();
            usuarios us = new usuarios();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dataGridView2.Rows.Add(
                    ds.Tables[0].Rows[i]["reg"].ToString(),
                    Convert.ToDateTime( ds.Tables[0].Rows[i]["ultmod"].ToString() ).ToString("dd-MM-yyyy"),
                    ds.Tables[0].Rows[i]["nocomprobante"].ToString(),
                    ds.Tables[0].Rows[i]["detalle"].ToString(),
                    ds.Tables[0].Rows[i]["valor"].ToString(),
                    us.get_nombre( ds.Tables[0].Rows[i]["entregadopor"].ToString()),
                    us.get_nombre(ds.Tables[0].Rows[i]["entregadoa"].ToString())
                );

            }
        }

        private void grilla1()
        {
            arqueos ar = new arqueos();

           /* DateTime fec1 = dateTimePicker1.Value;
            DateTime fec2 = dateTimePicker2.Value;
            
            rendiciones rendici = new rendiciones();
            while (DateTime.Compare(fec1.Date, fec2.Date) <= 0)
            {
                if(rendici.get_campo_fecha(fec1.ToString("dd/MM/yyyy"), fec1.ToString("d/M/yyyy"), fec1.ToString("dd/M/yyyy")) != "")
                {

                    lineas_rendiciones linren = new lineas_rendiciones();
                    
                    if (!linren.get_fecha(fec1.ToString("yyyy-MM-dd")))
                    {
                        linren.fechaarqueo = fec1.ToString("yyyy-MM-dd");
                        linren.idrendicion = rendici.get_campo_fecha(fec1.ToString("dd/MM/yyyy"), fec1.ToString("d/M/yyyy"), fec1.ToString("dd/M/yyyy"));
                        linren.valordia = ar.get_sum_fecha(linren.fechaarqueo);
                        linren.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        linren.save();
                    }
                    
                }
                 //   MessageBox.Show( "FECHA "+ rendici.get_campo_fecha(fec1.ToString("dd/MM/yyyy"), fec1.ToString("d/M/yyyy"), fec1.ToString("dd/M/yyyy")) );
                fec1 = fec1.AddDays(1);
            }*/

            DataSet ds = ar.get_all_pordia_rendiciones(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            dataGridView1.Rows.Clear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dataGridView1.Rows.Add(
                    0,
                    ds.Tables[0].Rows[i]["fechadia"].ToString(),
                    ds.Tables[0].Rows[i]["valormanual"].ToString(),
                    ds.Tables[0].Rows[i]["completo"].ToString()
                );

            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if(dataGridView1.Rows[i].Cells["completo"].Value != null)
                {
                    if(Convert.ToInt16(dataGridView1.Rows[i].Cells["completo"].Value) < 0)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.grilla1();
            this.grilla2();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            this.grilla1();
            this.grilla2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double val = this.valorEntrega();
            if (val > 0)
            {
                frmNuevaEntrega frm = new frmNuevaEntrega();
                frm.txtValor.Text = this.valorEntrega().ToString();
                frm.detallearqueo = this.detalleArqueo;
                frm.ShowDialog();
                if(frm.DialogResult == DialogResult.OK)
                {
                    this.actualizarArqueos(frm.consecutivo);
                    this.grilla1();
                    this.grilla2();
                    Task.Run(()=>{
                        frmMigraciones frm0 = new frmMigraciones();
                        frm0.exportarRendiciones();
                    });
                }
            }else if(val < 0)
            {
                MessageBox.Show("Los días " + errores+ " tienen arqueos sin cerrar", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void actualizarArqueos(string numentrega)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToInt16(dataGridView1.Rows[i].Cells["che"].Value) > 0)
                {
                    arqueos ar = new arqueos();
                    string fechita = Convert.ToDateTime(dataGridView1.Rows[i].Cells["fecha"].Value.ToString()).ToString("yyyy-MM-dd");
                    ar.actualizarRendicion(fechita, numentrega);
                }
            }
        }


        private double valorEntrega()
        {
            double res = 0;
            errores = "";
            this.detalleArqueo = "";
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if( Convert.ToInt16( dataGridView1.Rows[i].Cells["che"].Value ) > 0)
                {
                    res += Convert.ToDouble(dataGridView1.Rows[i].Cells["valor"].Value);
                    if (i == 0)
                        this.detalleArqueo = Convert.ToDateTime(dataGridView1.Rows[i].Cells["fecha"].Value.ToString()).ToString("dd/MM/yyyy");
                    else
                        this.detalleArqueo += ","+ Convert.ToDateTime( dataGridView1.Rows[i].Cells["fecha"].Value.ToString() ).ToString("dd/MM/yyyy");

                    if (Convert.ToInt16(dataGridView1.Rows[i].Cells["completo"].Value) < 0)
                        errores += dataGridView1.Rows[i].Cells["fecha"].Value.ToString()+" ";
                }
            }

            if (errores != "")
                return -1;

            return res;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            this.Width = 1100;
            tabControl1.Width = 1050;
            dataGridView2.Width = 1000;
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            this.Width = 605;
            tabControl1.Width = 555;
            dataGridView2.Width = 520;
        }
    }
}
