using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace ProyectoBrokerDelPuerto
{
    class mailBarrios
    {
        /*private string emailOrigen = "documentosbroker@gmail.com";
        private string pass = "broker2020";

            ****
        siteground

        apraapido@ahorrandoenseguros.com
        apraapido2022

        Nombre de usuario: Aquí sería el nombre de la cuenta de correo

        Contraseña: Utilizar la contraseña de la cuenta de e-mail.

        Servidor entrante: mail.ahorrandoenseguros.com

        Puerto IMAP: 993

        Puerto POP3: 995

        Servidor de salida: mail.ahorrandoenseguros.com

        Puerto SMTP: 465

            */
        private string emailOrigen = "";
        private string pass = "";
        private string servidor = "";
        private string puerto = "";
        private bool success = false;
        public MailMessage objmail;
        /*
        Enlace 1 para permitir otras apps
        Enlace 2 para aceptar que otros dispositivos ingresen
        https://myaccount.google.com/lesssecureapps?utm_source=google-account&utm_medium=web&pli=1
        https://accounts.google.com/b/0/DisplayUnlockCaptcha
        */

        public mailBarrios(string uso = "general")
        {
            configcorreo conf = new configcorreo();
            if (conf.get(uso))
            {
                this.emailOrigen = conf.email;
                this.pass = conf.pass;
                this.servidor = conf.servidor;
                this.puerto = conf.puerto;
                this.success = true;
            }
            else
            {
                
                this.success = false;
            }

            
        }

        private bool validarconfiguracion()
        {
            if (!this.success)
            {
                MessageBox.Show("La configuración del correo no se ha hecho, informar al administrador"
                    , "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public bool enviarMail(string mailDestino, string asunto, string mensaje, string rutaArchivo, string nombreCorreo = "Documentos Bróker del Puerto")
        {
            if (!this.validarconfiguracion())
                return false;

            bool result = true;

            try
            {
                objmail = new MailMessage();
                objmail.From = new MailAddress(this.emailOrigen, nombreCorreo);

                string[] email_enviar = mailDestino.Split(',');
                for (int i = 0; i < email_enviar.Length; i++)
                {
                    if(email_enviar[i].Trim() != "")
                        objmail.To.Add(email_enviar[i].Trim());
                }
                    
                objmail.Subject = asunto;
                objmail.Body = mensaje;
                objmail.IsBodyHtml = true;
                objmail.BodyEncoding = System.Text.Encoding.UTF8;
                objmail.SubjectEncoding = System.Text.Encoding.UTF8;
                

                if (rutaArchivo != "")
                {
                    string[] spearator = { "," };
                    string[] rutas = rutaArchivo.Split(spearator, StringSplitOptions.None);

                    for (int i = 0; i < rutas.Length; i++)
                    {
                        if(rutas[i].Trim() != "")
                            objmail.Attachments.Add(new Attachment(@rutas[i]));
                    }
                    
                }

                SmtpClient objClient = new SmtpClient(this.servidor, Convert.ToInt16(this.puerto) );
                objClient.Credentials = new NetworkCredential(this.emailOrigen, this.pass);
                if(this.emailOrigen.IndexOf("gmail") > 0)
                    objClient.EnableSsl = true;
                else
                    objClient.EnableSsl = false;

                objClient.Send(objmail);

                result = true;
            }
            catch (Exception ex)
            {
                logs log = new logs();
                log.coderror = "MAILBARRIOS 101";
                log.mensaje = "No se ha podido enviar correo a "+ mailDestino + " por "+@ex;
                log.save();
                result = false;
            }

            return result;

        }

        /* public bool enviarMail(string mailDestino, string asunto, string mensaje, string rutaArchivo, string nombreCorreo = "Documentos Bróker del Puerto")
         {
             bool result = true;

             try
             {
                 mailDestino = "especialistasenexcel.soporte@gmail.com";
                 MailMessage sendMail = new MailMessage(this.emailOrigen, mailDestino, asunto, "<p>" + mensaje + "</p>");

                 string[] spearator = { "," };
                 string[] rutas = rutaArchivo.Split(spearator, StringSplitOptions.None);

                 for (int i = 0; i < rutas.Length; i++)
                 {
                     sendMail.Attachments.Add(new Attachment(rutas[i]));
                 }


                 sendMail.IsBodyHtml = true;
                 //sendMail.From = new MailAddress(this.emailOrigen, nombreCorreo);
                 SmtpClient smtpSend = new SmtpClient("smtp.gmail.com");
                 smtpSend.EnableSsl = true;
                 smtpSend.UseDefaultCredentials = false;
                 smtpSend.Port = 587;
                 smtpSend.Credentials = new System.Net.NetworkCredential(this.emailOrigen, this.pass);
                 smtpSend.Send(sendMail);
                 smtpSend.Dispose();

                 result = true;
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Error al enviar mail "+ex.Message);
                 result = false;
             }

             return result;

         }

         public void enviarMailInforme(string mailDestino, string asunto, string mensaje, string rutaArchivo, string nombreCorreo = "Documentos Bróker del Puerto")
         {

             this.emailOrigen = "informebarriosprivados@gmail.com";
             mailDestino = "especialistasenexcel.soporte@gmail.com";

             MailMessage sendMail = new MailMessage(this.emailOrigen, mailDestino, asunto, "<p>" + mensaje + "</p>");

             string[] spearator = { "," };
             string[] rutas = rutaArchivo.Split(spearator, StringSplitOptions.None);

             for (int i = 0; i < rutas.Length; i++)
             {
                 sendMail.Attachments.Add(new Attachment(rutas[i]));
             }


             sendMail.IsBodyHtml = true;
             sendMail.From = new MailAddress(this.emailOrigen, nombreCorreo);
             SmtpClient smtpSend = new SmtpClient("smtp.gmail.com");
             smtpSend.EnableSsl = true;
             smtpSend.UseDefaultCredentials = false;
             smtpSend.Port = 587;
             smtpSend.Credentials = new System.Net.NetworkCredential(this.emailOrigen, this.pass);
             smtpSend.Send(sendMail);
             smtpSend.Dispose();
         }
         */

    }
}
