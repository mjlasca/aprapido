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
    public partial class frmMailFindeldia : Form
    {
        private int[] access;
        protected frmInformeDelDia frminfo;
        public frmMailFindeldia()
        {
            this.frminfo = new frmInformeDelDia();
            InitializeComponent();
        }

        private void frmMailFindeldia_Load(object sender, EventArgs e)
        {
            this.frminfo.initform();
            ultdia.Text = this.frminfo.ultdia.Text;

            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("mail_findeldia");

            button1.Visible = Convert.ToBoolean(this.access[1]);
            comboBox1.DataSource = this.frminfo.comboBox2.DataSource;

            this.mensaje();
        }

        private void mensaje()
        {
            textBox2.Text = "";
            textBox3.Text = "";

            parametrosmailinforme parammail = new parametrosmailinforme();
            parammail.plantillatipo = "infodeldia";
            if (parammail.get())
            {
                textBox1.Text = parammail.mails;
                textBox2.Text = parammail.asunto;
                textBox3.Text = parammail.mensaje;
            }
            else
            {
                textBox2.Text = "Informe Ventas Programa BP - BDP Certificados ([FECHA])";

                textBox3.Text = "Estimados Buenas tardes,   " + Environment.NewLine + Environment.NewLine +
                    "Adjunto las ventas del día ([FECHA]) para emitir con las sumas vigentes. " + Environment.NewLine +
                    "Las polizas que están con la promoción 3x2 están marcadas en color lila en los tres archivos adjuntos " +
                    "ESTIMADOS FAVOR REALIZAR LA INCLUSIÓN DE LA CLÁUSULA DE NO REPETICIÓN A FAVOR DE NORDELTA A TODAS LAS PÓLIZAS INFORMADAS EN ESTE ENVIO, YA QUE HEMOS IDENTIFICADO ALGUNAS A LAS QUE NO SE LES HA INCLUIDO " + Environment.NewLine + Environment.NewLine +
                    "Recuerden que no deben imprimir las pólizas! " + Environment.NewLine +
                    "Favor informar los premios y prima de cada póliza e indicar a las que se le genera recibos manuales por Ingresos Brutos Percepción." + Environment.NewLine + Environment.NewLine +
                    "Desde ya muchas gracias por su colaboración  " + Environment.NewLine +
                    "Saludos cordiales.";
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.frminfo.fecha.Text = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            comboBox1.DataSource = this.frminfo.comboBox2.DataSource;
        }

        private bool validacion()
        {
            bool rest = true;
            string errores = "";
            validaciones val = new validaciones();



            if (textBox1.Text != "")
            {
                string[] spearator = { "," };
                string[] vecmail = textBox1.Text.Split(spearator, StringSplitOptions.None);
                for (int i = 0; i < vecmail.Count(); i++)
                {
                    if (vecmail[i].Trim() != "")
                    {
                        if (!val.correo(vecmail[i].Trim()))
                        {
                            errores += "\nEl correo " + vecmail[i].Trim() + " está mal escrito";
                        }
                    }
                    else
                    {
                        errores += "\nSi se coloca una coma (,) debe haber un correo luego";
                    }
                }

            }
            else
            {
                errores += "\nEl correo no puede estar vacío";
            }

            if (textBox2.Text == "")
            {
                errores += "\nEl asunto no puede estar vacío";
            }

            if (textBox3.Text == "")
            {
                errores += "\nEl cuerpo del mensaje no puede estar vacío";
            }



            if (errores != "")
            {
                MessageBox.Show("Se debe corregir los siguientes errores " + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rest = false;
            }

            return rest;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.validacion())
            {
                parametrosmailinforme parammail = new parametrosmailinforme();
                parammail.mails = textBox1.Text.Trim();
                parammail.asunto = textBox2.Text;
                parammail.mensaje = textBox3.Text;
                parammail.plantillatipo = "infodeldia";
                parammail.save();

                mailsinformes minfo = new mailsinformes();
                minfo.fecha = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                usuarios usssu = new usuarios();
                usssu.loggin = MDIParent1.sesionUser;
                DataSet dssusu = usssu.get_all_allow();
                bool enviardenuevo = false;

                if (dssusu.Tables[0].Rows.Count > 0)
                    enviardenuevo = true;

                if (!minfo.exist() || enviardenuevo)
                {
                    enviando.Text = "Generando archivos para enviar...";

                    if (MessageBox.Show("Segur@ desea enviar el mensaje?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            //this.exportar(false);

                            informes info = new informes();
                            DataSet dsinfo = info.get_dia(dateTimePicker1.Value.ToString("yyyy-MM-dd"), comboBox1.Text);
                            string ruta = "";

                            if (dsinfo.Tables[0].Rows.Count > 0)
                            {
                                ruta += dsinfo.Tables[0].Rows[0]["ruta"].ToString();
                                ruta += "," + dsinfo.Tables[0].Rows[1]["ruta"].ToString();
                                ruta += "," + dsinfo.Tables[0].Rows[2]["ruta"].ToString();
                            }

                            if (ruta == "")
                            {
                                MessageBox.Show("No hay archivos para adjuntar, genere los archivos primero " + dateTimePicker1.Value.ToString("dd/MM/yyyy")
                                    , "No hay datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }




                            mailBarrios mail = new mailBarrios();

                            if (mail.enviarMail(
                                textBox1.Text,
                                textBox2.Text.Replace("[FECHA]", this.frminfo.fecha.Text) +" Cod. ORG. "+ comboBox1.Text,
                                textBox3.Text.Replace("[FECHA]", this.frminfo.fecha.Text).Replace("\n", "<br>"),
                                ruta
                            ))
                            {
                                minfo.useredit = MDIParent1.sesionUser;
                                minfo.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                minfo.save();
                                MessageBox.Show("Mensaje enviado con éxito");
                            }
                            else
                            {
                                MessageBox.Show("No se ha podido enviar el correo, en éste equipo no se encuentran los archivos a enviar", "Error Envío", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Se ha generado error al enviar correo\n" + ex, "Error Envío", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }



                    }
                    enviando.Text = "";
                }
                else
                {
                    MessageBox.Show("El cierre del " + dateTimePicker1.Value.ToString("dd/MM/yyyy") + " ya se ha enviado anteriormente ", "Ya se envió el mail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
