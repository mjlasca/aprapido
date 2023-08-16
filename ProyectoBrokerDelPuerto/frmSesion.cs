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
    
    public partial class frmSesion : Form
    {
        bool abierta = false;
        usuarios user = new usuarios();
        public frmSesion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtUsuario.Text != "" && txtPass.Text != "")
            {
                this.abrirSesion();
            }
        }

        private void abrirSesion()
        {
            if (user.loggin_pass(txtUsuario.Text, txtPass.Text))
            {
                MDIParent1.sesionUser = txtUsuario.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                txtPass.Text = "";
                MessageBox.Show("El usuario y/o la contraseña no coinciden");
            }
                
            
        }

        private void frmSesion_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == (int)Keys.Enter)
            {
                if (txtUsuario.Text != "" && txtPass.Text != "")
                {
                    button1.PerformClick();
                }
            }
        }
    }
}
