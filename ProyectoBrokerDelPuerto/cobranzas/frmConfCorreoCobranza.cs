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
    public partial class frmConfCorreoCobranza : Form
    {
        
        public frmConfCorreoCobranza()
        {
            InitializeComponent();
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

            if (errores != "")
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
                confcorreo.servidor = txtServidor.Text;
                confcorreo.puerto = txtPuerto.Text;
                confcorreo.pass = txtPass.Text;
                confcorreo.usocorreo = "cobranza";
                confcorreo.useredit = MDIParent1.sesionUser;
                confcorreo.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (confcorreo.save())
                {
                    MessageBox.Show("Se ha guardado con éxito");
                }
            }
        }

        private void frmConfCorreoCobranza_Load(object sender, EventArgs e)
        {

            configcorreo conf = new configcorreo();
            if (conf.get("cobranza"))
            {
                txtEmail.Text = conf.email;
                txtServidor.Text = conf.servidor;
                txtPuerto.Text = conf.puerto;
            }
            

            txtMensaje.ScrollBars = ScrollBars.Vertical;

            parametrosmailinforme parammail = new parametrosmailinforme();
            parammail.plantillatipo = "mailcobranza";
            if (parammail.get())
            {
                txtMensaje.Text = parammail.mensaje;
            }
            else
            {

                txtMensaje.Text = "Estimado [CLIENTE], Buen dia!,   " + Environment.NewLine + Environment.NewLine +
                    "Nos ponemos en contacto con Ud. para enviarle la factura y el estado de cuenta de su póliza que corresponde a la compañía  [ASEGURADORA]," + Environment.NewLine +
                    "Recordando que si el pago lo realiza a través de transferencia es necesario que nos envié copia del comprobante de pago para poder rendirlo a la aseguradora." + Environment.NewLine +
                    "Otra modalidad que facilita la gestión es adherir sus pólizas a débito automático, CBU y/o, Tarjeta de crédito y así facilitar su pago." + Environment.NewLine + Environment.NewLine +
                    "Aseguradora: [ASEGURADORA]" + Environment.NewLine + Environment.NewLine +
                    "[DETALLE_COBRANZA]" + Environment.NewLine + Environment.NewLine +
                    "Medios de pago " + Environment.NewLine + "[MEDIOS_PAGO]" + Environment.NewLine + Environment.NewLine +
                    "Muchas gracias," + Environment.NewLine +
                    "Saludos cordiales.";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(txtMailPrueba.Text != "")
            {
                parametrosmailinforme parammail = new parametrosmailinforme();
                parammail.mensaje = txtMensaje.Text;
                parammail.plantillatipo = "mailcobranza";
                parammail.save();
                this.proosend();
            }
            else
            {
                txtMailPrueba.Focus();
                MessageBox.Show("Por favor escriba un mail receptor");
            }
            
        }

        private string detalle_cobranza()
        {
            string html = "<table><thead style='background-color : black;padding : 5px;color : white;'>" +
                "<tr ><th>Código Productor</th><th>Nombre</th><th>Póliza</th><th>Cuota</th><th>Moneda</th>"+
                "<th>Forma de Pago</th><th>Monto</th><th style='background-color : black;color : white;'>Vencimiento</th>"+
               "<th style='background-color : white;color : white;'>-</th></tr></thead>" +
                "<tbody style='border : 1px solid #ddd;background-color : #ddd;'><tr><td>---</td><td>---</td><td>---</td><td>---</td><td>---</td><td>---</td><td>---</td><td>---</td></tr></tbody>";

            html += "</table>";

            return html;
        }

        private string medios_pago()
        {
            /*
            Depósito o Transferencia en pesos
            Transferencia en dólares
            Cobro en efectivo
            Emitir Cheque
            */

            string html = "<table><tbody>" +
                "<tr ><td style='background-color : black;padding : 5px;color : white;'>Depósito o Transferencia en pesos</th></tr>" +
                "<tr style='background-color : #ddd;border : 1px solid #ddd;'><td>---</td></tr>" +
                "<tr ><td style='background-color : black;padding : 5px;color : white;'>Transferencia en dólares</th></tr>" +
                "<tr style='background-color : #ddd;border : 1px solid #ddd;'><td>---</td></tr>" +
                "<tr ><td style='background-color : black;padding : 5px;color : white;'>Cobro en efectivo</th></tr>" +
                "<tr style='background-color : #ddd;border : 1px solid #ddd;'><td>---</td></tr>" +
                "<tr ><td style='background-color : black;padding : 5px;color : white;'>Emitir Cheque</th></tr>" +
                "<tr style='background-color : #ddd;border : 1px solid #ddd;'><td>---</td></tr>";

            html += "</tbody></table>";



            return html;
        }

        private void proosend()
        {
            mailBarrios enviomail = new mailBarrios("cobranza");

            if (enviomail.enviarMail(
                txtMailPrueba.Text,
                "Correo de prueba de cobranzas, configuración con éxito",
                txtMensaje.Text.Replace("\n", "<br>").Replace("[CLIENTE]","Pepito Pérez").Replace("[ASEGURADORA]","SANCOR SEGUROS S.A.").Replace("[DETALLE_COBRANZA]",this.detalle_cobranza()).Replace("[MEDIOS_PAGO]",this.medios_pago()),
                "",
                "Correo de prueba"
            ))
            {
                MessageBox.Show("El correo se ha enviado con éxito, por favor revise la bandeja de entrada de " + txtMailPrueba.Text);
            }
            else
            {
                MessageBox.Show("Hubo un error al enviar el correo, no hay respuesta del servidor \nSe ha excedido el tiempo de espera"
                    , "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
