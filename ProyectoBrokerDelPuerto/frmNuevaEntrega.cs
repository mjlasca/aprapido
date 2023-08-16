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
    public partial class frmNuevaEntrega : Form
    {
        bool clavesAprobada1 = false;
        bool clavesAprobada2 = false;
        public string detallearqueo = "";
        public string consecutivo = "";
        public frmNuevaEntrega()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.validar())
            {
                this.guardarEntrega();
            }
        }

        private void guardarEntrega()
        {
            usuarios us = new usuarios();
            rendiciones ren = new rendiciones();
            ren.idarqueos = this.detallearqueo;
            ren.detalle = "Arqueos : " + this.detallearqueo;
            ren.valor = txtValor.Text;
            ren.nocomprobante = txtComprobante.Text;
            ren.entregadopor = us.get_loggin( txtUsuario.Text );
            ren.entregadoa = us.get_loggin(txtSupervisor.Text);
            ren.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ren.codestado = "1";
            ren.user_edit = MDIParent1.sesionUser;
            
            if (ren.save())
            {
                this.consecutivo = ren.consecutivo();
                this.DialogResult = DialogResult.OK;
                string[] subs = this.detallearqueo.Split(',');

                foreach (var sub in subs)
                {
                    if(sub != "" && sub != null)
                    {
                        arqueos ar = new arqueos();
                        lineas_rendiciones linren = new lineas_rendiciones();
                        linren.idrendicion = this.consecutivo;
                        linren.fechaarqueo = Convert.ToDateTime(sub.Trim().Substring(6, 4) + "-" + sub.Trim().Substring(3, 2) +
                            "-" + sub.Trim().Substring(0, 2)).ToString("yyyy-MM-dd");
                        linren.valordia = ar.get_sum_fecha(linren.fechaarqueo);
                        linren.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        linren.save();
                    }
                }

                
                Task.Run(() => {
                    frmMigraciones frm0 = new frmMigraciones();
                    frm0.exportarRendiciones();
                });
            }

        }

        private void datosIniciales()
        {
            usuarios us = new usuarios();
            DataSet ds = us.get_all_allow();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                txtSupervisor.Items.Add(
                    ds.Tables[0].Rows[i]["nombre"].ToString()
                );
                txtUsuario.Items.Add(
                    ds.Tables[0].Rows[i]["nombre"].ToString()
                );
            }
        }

        

        

        private bool validar()
        {
            bool res = true;
            string errores = "";
            if (txtClave.Text == "" || txtComprobante.Text == "" || txtUsuario.Text == "" || txtSupervisor.Text == "" || txtClaveSupervisor.Text == "")
            {
                errores += "\nHay campos obligatorios sin diligenciar";
            }

            if ((!clavesAprobada1 && txtClave.Text != "") || (!clavesAprobada2 && txtClaveSupervisor.Text != ""))
            {
                errores += "\nUna de las claves no es correcta";
            }

         

            if (errores != "")
            {
                MessageBox.Show("No se puede realizar el registro de entrega por : " + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return res;
        }

        private void abrirSesion(string usu, string pass, TextBox txt)
        {
            usuarios user = new usuarios();
            if (!user.loggin_pass(usu, pass))
            {
                txt.BackColor = Color.Red;
                if (txt.Name == "txtClave")
                    this.clavesAprobada1 = false;
                if (txt.Name == "txtClaveSupervisor")
                    this.clavesAprobada2 = false;
            }
            else
            {
                txt.BackColor = Color.White;
                if (txt.Name == "txtClave")
                    this.clavesAprobada1 = true;
                if (txt.Name == "txtClaveSupervisor")
                    this.clavesAprobada2 = true;
            }
        }

        private void txtClave_Leave(object sender, EventArgs e)
        {
            if (txtClave.Text != "")
            {
                usuarios us = new usuarios();
                this.abrirSesion(us.get_loggin(txtUsuario.Text), txtClave.Text, txtClave);
            }
        }

        private void txtClaveSupervisor_Validated(object sender, EventArgs e)
        {
            if (txtClaveSupervisor.Text != "" && txtSupervisor.Text != "")
            {
                usuarios us = new usuarios();
                this.abrirSesion(us.get_loggin(txtSupervisor.Text), txtClaveSupervisor.Text, txtClaveSupervisor);
            }
        }

        private void frmNuevaEntrega_Load(object sender, EventArgs e)
        {
            this.datosIniciales();
        }
    }
}
