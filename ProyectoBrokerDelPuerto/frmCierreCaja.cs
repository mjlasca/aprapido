using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBrokerDelPuerto
{
    public partial class frmCierreCaja : Form
    {
        int[] access;
        public frmCierreCaja()
        {
            InitializeComponent();
        }

        private async void  frmCierreCaja_Load(object sender, EventArgs e)
        {
            frmMigraciones frmmig = new frmMigraciones();
            solicitudes s = new solicitudes();
            s.solicitud_arqueos = true;
            await frmmig.importarData(s);

            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("cierrecajas");
            
            btnCierreCaja.Visible = Convert.ToBoolean(this.access[1]);

            this.arqueosAnteriores();
            this.datosArqueo();
        }

        private void arqueosAnteriores()
        {
            usuarios us = new usuarios();
            propuestas pro = new propuestas();
            DataSet ds = pro.get_all_arqueo(dateTimePicker2.Value.AddDays(-10).ToString("yyyy-MM-dd"), dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd"));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                arqueos ar = new arqueos();
                if(ds.Tables[0].Rows[i]["fecha"].ToString() != "")
                ar.fechadia = Convert.ToDateTime( ds.Tables[0].Rows[i]["fecha"].ToString()).ToString("yyyy-MM-dd");
                ar.usuario = ds.Tables[0].Rows[i]["usuario"].ToString();
                ar.valorinicial = "0";
                ar.dinerorealcaja = "0";
                ar.valormanual = "0";
                ar.cuadredescuadre = "0";
                ar.nombre = us.get_nombre(ar.usuario);
                ar.user_edit = MDIParent1.sesionUser;
                ar.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                ar.codestado = "1";
                ar.save_in();
            }



            ds = pro.get_all_arqueo_usuariopaga(dateTimePicker2.Value.AddDays(-10).ToString("yyyy-MM-dd"), dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd"));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                
                arqueos ar = new arqueos();
                if (ds.Tables[0].Rows[i]["fecha"].ToString() != "")
                    ar.fechadia = Convert.ToDateTime(ds.Tables[0].Rows[i]["fecha"].ToString()).ToString("yyyy-MM-dd");
                ar.usuario = ds.Tables[0].Rows[i]["usuario"].ToString();

                ar.get_fecha_user(ar.fechadia, ar.usuario);
                if((ar.id== null || ar.id == "") && Convert.ToDateTime(ds.Tables[0].Rows[i]["fecha"].ToString()).Date >= Convert.ToDateTime("2023-06-30").Date )
                {
                    ar.valorinicial = "0";
                    ar.dinerorealcaja = "0";
                    ar.valormanual = "0";
                    ar.cuadredescuadre = "0";
                    ar.nombre = us.get_nombre(ar.usuario);
                    ar.user_edit = MDIParent1.sesionUser;
                    ar.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    ar.codestado = "1";
                    ar.save_in();
                }
            }
        }

        public void datosArqueo()
        {
            
            arqueos ar = new arqueos();

            DateTime fec1 = dateTimePicker1.Value;
            DateTime fec2 = dateTimePicker2.Value;
            
            
            while (DateTime.Compare(fec1.Date, fec2.Date) <= 0)
            {
                if(DateTime.Compare(fec1.Date, Convert.ToDateTime("2021-03-20")) < 0)
                {
                    ar.eliminarblancos(fec1.ToString("yyyy-MM-dd"));
                }
                fec1 = fec1.AddDays(1);
            }

            DataSet ds;

            string todos_ = comboBox1.Text;
            if (todos_ == "Todo")
                todos_ = "";

            ds = ar.get_all_busqueda(textBox1.Text, dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"), todos_);
            dataGridView1.Rows.Clear();
            if (ds.Tables.Count > 0)
            {
                double totalpolizas = 0;
                double totaltodo = 0;
                double totalcontado = 0;
                double totalcredito = 0;
                double totalreal = 0;
                double totalotrosmedios = 0;
                double totalcajero = 0;
                double totaldiferencia = 0;
                double totalPagosCreditos = 0;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    double contado_ = ds.Tables[0].Rows[i]["contado"].ToString() == "" ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["contado"].ToString());
                    double pagoCredito = ds.Tables[0].Rows[i]["pagoscreditos"].ToString() == "" ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["pagoscreditos"].ToString());
                    double pagoscreditos_difefectivo = ds.Tables[0].Rows[i]["pagoscreditos_difefectivo"].ToString() == "" ? 0 : Convert.ToDouble(ds.Tables[0].Rows[i]["pagoscreditos_difefectivo"].ToString());
                    double realcaja = contado_ + (pagoCredito - pagoscreditos_difefectivo);
                    

                    dataGridView1.Rows.Add(
                        ds.Tables[0].Rows[i]["id"].ToString(),
                        ds.Tables[0].Rows[i]["fechadia"].ToString(),
                        ds.Tables[0].Rows[i]["nombre"].ToString(),
                        ds.Tables[0].Rows[i]["usuario"].ToString(),
                        ds.Tables[0].Rows[i]["cantpoli"].ToString(),
                        ds.Tables[0].Rows[i]["total"].ToString(),
                        contado_,
                        ds.Tables[0].Rows[i]["credito"].ToString(),
                        pagoCredito,
                        realcaja,
                        pagoscreditos_difefectivo,
                        ds.Tables[0].Rows[i]["valormanual"].ToString(),
                        ds.Tables[0].Rows[i]["cuadredescuadre"].ToString(),
                        ds.Tables[0].Rows[i]["nombresupervisor"].ToString(),
                        ds.Tables[0].Rows[i]["observaciones"].ToString()
                    );

                    if(contado_ != realcaja)
                    {
                        dataGridView1.Rows[i].Cells["pagoscredito"].Style.BackColor = Color.OrangeRed;
                        dataGridView1.Rows[i].Cells["pagoscredito"].Style.ForeColor = Color.White;
                    }
                        

                    totalpolizas += ds.Tables[0].Rows[i]["cantpoli"].ToString() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["cantpoli"].ToString()) : 0;
                    totaltodo += ds.Tables[0].Rows[i]["total"].ToString() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["total"].ToString()) : 0;
                    totalcontado += ds.Tables[0].Rows[i]["contado"].ToString() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["contado"].ToString()) : 0;
                    totalcredito += ds.Tables[0].Rows[i]["credito"].ToString() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["credito"].ToString()) : 0;
                    totalPagosCreditos += pagoCredito;
                    totalreal += realcaja;
                    totalotrosmedios += pagoscreditos_difefectivo;
                    totalcajero += ds.Tables[0].Rows[i]["valormanual"].ToString() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["valormanual"].ToString()) : 0;
                    totaldiferencia += ds.Tables[0].Rows[i]["cuadredescuadre"].ToString() != "" ? Convert.ToDouble(ds.Tables[0].Rows[i]["cuadredescuadre"].ToString()) : 0;
                }
                
                dataGridView1.Rows.Add(
                    "TOTALES => ",
                    "---",
                    "---",
                    "---",
                    totalpolizas,
                    totaltodo.ToString("C", CultureInfo.CurrentCulture),
                    totalcontado.ToString("C", CultureInfo.CurrentCulture),
                    totalcredito.ToString("C", CultureInfo.CurrentCulture),
                    totalPagosCreditos.ToString("C", CultureInfo.CurrentCulture),
                    totalreal.ToString("C", CultureInfo.CurrentCulture),
                    totalotrosmedios.ToString("C", CultureInfo.CurrentCulture),
                    totalcajero.ToString("C", CultureInfo.CurrentCulture), 
                    totaldiferencia.ToString("C", CultureInfo.CurrentCulture),
                    "---",
                    "---"
                );
            }
            


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.CurrentRow.Cells["supervisor"].Value.ToString() != "")
            {
                btnCierreCaja.Enabled = false;
            }
            else
            {
                btnCierreCaja.Enabled = true;
            }
        }

        private void btnCierreCaja_Click(object sender, EventArgs e)
        {
            frmCerrarArqueo frm = new frmCerrarArqueo();

            frm.id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            frm.dia = dataGridView1.CurrentRow.Cells["fecha"].Value.ToString();
            frm.user = dataGridView1.CurrentRow.Cells["usuario"].Value.ToString();
            frm.totalpremios = dataGridView1.CurrentRow.Cells["valor"].Value.ToString();
            frm.valorEfectivo = dataGridView1.CurrentRow.Cells["realcaja"].Value.ToString();
            frm.otrosMediosdePago = dataGridView1.CurrentRow.Cells["otrospagos"].Value.ToString();
            
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.datosArqueo();
                Task.Run(() => {
                    frmMigraciones frm0 = new frmMigraciones();
                    frm0.exportarRendiciones();
                });

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.datosArqueo();
        }
    }
}
