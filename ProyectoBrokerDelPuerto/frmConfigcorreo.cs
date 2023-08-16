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
    public partial class frmConfigcorreo : Form
    {
        int[] access;
        public frmConfigcorreo()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            configcorreo confcorreo = new configcorreo();

            if(!confcorreo.get())
            {
                MessageBox.Show("No se ha guardado los datos del correo");
                return;
            }


            validaciones val = new validaciones();

            if (!val.correo(txtEmailPrueba.Text))
            {
                MessageBox.Show("El correo de prueba no está bien escrito");
                return;
            }

            mailBarrios enviomail = new mailBarrios();

            if(enviomail.enviarMail(
                txtEmailPrueba.Text,
                "Correo de prueba, configuración con éxito",
                "Correo de prueba<br><p style='background-color:yellow;'> Configuración EXITOSA </p>",
                "",
                "Correo de prueba"
            ))
            {
                MessageBox.Show("El correo se ha enviado, por favor revise la bandeja de entrada de "+txtEmailPrueba.Text);
            }
            else
            {
                MessageBox.Show("Hubo un error al enviar el correo" 
                    , "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.linkLabel3.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://mail.google.com/mail/u/0/?tab=rm&ogbl#settings/fwdandpop");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.linkLabel4.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://myaccount.google.com/lesssecureapps?utm_source=google-account&utm_medium=web&pli=1");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.guardardatos();
        }

        public bool validaciones()
        {
            bool res = true;
            string errores = "";
            validaciones val = new validaciones();


            if (txtEmail.Text == "")
            {
                errores += "\nEl email no puede estar vacío";
            }

            if (!val.correo(txtEmail.Text))
            {
                errores += "\nEl email está mal ecrito";
            }

            if (txtPass.Text == "")
            {
                errores += "\nLa contraseña no puede estar vacía";
            }

            if(errores != "")
            {
                res = false;
                MessageBox.Show("Hay errores de validación\n" + errores
                    , "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return res;
        }

        private void guardardatos()
        {
            if (this.validaciones())
            {
                configcorreo confcorreo = new configcorreo();
                confcorreo.email = txtEmail.Text;
                confcorreo.servidor = "smtp.gmail.com";
                confcorreo.puerto = "587";
                confcorreo.pass = txtPass.Text;
                confcorreo.useredit = MDIParent1.sesionUser;
                confcorreo.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (confcorreo.save())
                {
                    MessageBox.Show("Se ha guardado con éxito");
                }
            }
        }

        private void frmConfigcorreo_Load(object sender, EventArgs e)
        {
            perfiles perf = new perfiles();
            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("confcorreo");

            button1.Visible = Convert.ToBoolean(this.access[1]);
            button2.Visible = Convert.ToBoolean(this.access[1]);

            configcorreo conf = new configcorreo();
            conf.get();

            txtEmail.Text = conf.email;
            txtPass.Text = conf.pass;
        }
    }
}
