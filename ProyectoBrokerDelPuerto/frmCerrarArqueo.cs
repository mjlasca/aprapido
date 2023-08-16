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
    public partial class frmCerrarArqueo : Form
    {
        public string dia;
        public string id;
        public string user;
        public string totalpremios;
        public string valorEfectivo;
        public string otrosMediosdePago;
        bool clavesAprobada1 = false;
        bool clavesAprobada2 = false;
        List<string> loginList = new List<string>();
        public frmCerrarArqueo()
        {
            InitializeComponent();
        }
        public void datosIniciales()
        {
            usuarios us = new usuarios();
            DataSet ds = us.get_all_allow();

            txtSupervisor.Items.Add("");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                txtSupervisor.Items.Add(
                    ds.Tables[0].Rows[i]["nombre"].ToString()
                );
                loginList.Add(
                    ds.Tables[0].Rows[i]["loggin"].ToString()
                );
            }

            us.loggin = MDIParent1.sesionUser;
            ds = us.get();
            //this.Text = this.Text + " Fecha : " + dia + " Usuario : " + user;
            txtDiaCierre.Text = this.dia;
            txtUsuario.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
            loginUser_txt.Text = ds.Tables[0].Rows[0]["loggin"].ToString();
            txtTotales.Text = this.totalpremios;
            txtOtrosMedios.Text = this.otrosMediosdePago;
            txtTotalPremio.Text = this.valorEfectivo;
            txtTotalCaja.Select();
            txtTotalCaja.Focus();
        }

        private void frmCerrarArqueo_Load(object sender, EventArgs e)
        {
            this.datosIniciales();
            
        }

        private void txtTotalPremio_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtTotalCaja_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != 46)) 
            {
                e.Handled = true;
                return;
            }
        }

        private void txtTotalCaja_TextChanged(object sender, EventArgs e)
        {
            if(txtTotalPremio.Text != "")
            {
                if(txtTotalCaja.Text != "")
                {
                    txtDiferencia.Text = ( Convert.ToDouble(txtTotalCaja.Text) - Convert.ToDouble(txtTotalPremio.Text)).ToString();
                    if(Convert.ToDouble(txtDiferencia.Text) != 0)
                    {
                        txtRazon.Text = "";
                    }
                    else
                    {
                        txtRazon.Text = "";
                    }
                }
            }
        }

        private void txtSupervisor_Leave(object sender, EventArgs e)
        {
            
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
            if(txtClave.Text != "")
            {
                usuarios us = new usuarios();
                this.abrirSesion( loginUser_txt.Text, txtClave.Text, txtClave);
            }
                
        }

        private void txtClaveSupervisor_Leave(object sender, EventArgs e)
        {
            if (txtClaveSupervisor.Text != "" && txtSupervisor.Text != "")
            {
                usuarios us = new usuarios();
                this.abrirSesion(loginList[txtSupervisor.SelectedIndex - 1], txtClaveSupervisor.Text, txtClaveSupervisor);
            }
                
        }

        private bool validar()
        {
            string errores = "";
            if(txtClave.Text == "" || txtTotalCaja.Text == ""  )
            {
                errores += "\nHay campos obligatorios sin diligenciar";
            }

            if( !clavesAprobada1 && txtClave.Text != "" )
            {
                errores += "\nLa clave del usuario que cierra no es correcta";
            }

            if ( txtSupervisor.Text != "")
            {
                if (!clavesAprobada2)
                {
                    errores += "\nLa clave del usuario que recibe no es correcta";
                }
            }


            if (txtDiferencia.Text != "0")
            {
                if(txtRazon.Text.Trim() == "")
                {
                    errores += "\nHay descuadre en el cierre de caja, por favor debe colocar la razón del descuadre";
                    txtRazon.Focus();
                }
            }

            if(errores != "")
            {
                MessageBox.Show("No se puede realizar el registro del cierre por : " + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.validar())
            {
                usuarios us = new usuarios();
                arqueos ar = new arqueos();
                ar.id = this.id;
                ar.fechadia = Convert.ToDateTime(txtDiaCierre.Text).ToString("yyyy-MM-dd") ;
                ar.usuario = this.user;
                ar.nombre = us.get_nombre(this.user);
                ar.valorinicial = "0";
                ar.dinerorealcaja = txtTotalPremio.Text;
                ar.valormanual = txtTotalCaja.Text;
                if (txtSupervisor.Text == "")
                {
                    ar.supervisor = MDIParent1.sesionUser;
                    ar.nombresupervisor = txtUsuario.Text;
                }
                else{
                    ar.supervisor = loginList[txtSupervisor.SelectedIndex - 1];
                    ar.nombresupervisor = txtSupervisor.Text;
                }
                
                ar.cuadredescuadre = txtDiferencia.Text;
                ar.observaciones = txtRazon.Text;
                ar.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                ar.user_edit = MDIParent1.sesionUser;
                ar.codestado = "1";

                if (ar.save())
                {
                    this.DialogResult = DialogResult.OK;
                }

            }
        }
    }
}
