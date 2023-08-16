using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBrokerDelPuerto.cobranzas
{
    public partial class frmModificaEmail : Form
    {
        public string cliente_id = "";
        public string nuevo_mensaje = "";
        public frmModificaEmail()
        {
            InitializeComponent();
        }

        private void fmModificaEmail_Load(object sender, EventArgs e)
        {
            lblUsuarioModifica.Text = "Modificar mail para "+this.cliente_id;
            this.inicial_mensaje();
        }

        private void inicial_mensaje()
        {

            configcorreo conf = new configcorreo();
            if (conf.get("cobranza"))
            {
                txtMensaje.ScrollBars = ScrollBars.Vertical;

                parametrosmailinforme parammail = new parametrosmailinforme(true);
                parammail.plantillatipo = "mailcobranza";
                if (parammail.get())
                {
                    txtMensaje.Text = parammail.mensaje;
                    if(this.nuevo_mensaje != "")
                    {
                        txtMensaje.Text = this.nuevo_mensaje;
                    }
                }
            }

        }

        private void guardar_btn_Click(object sender, EventArgs e)
        {
            if(txtMensaje.Text != "")
            {
                this.nuevo_mensaje = txtMensaje.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("No puede generar un correo sin información, por favor cierre esta ventana y vuelva a abrirla");
            }
        }
    }
}
