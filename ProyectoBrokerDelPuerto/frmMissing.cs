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
    public partial class frmMissing : Form
    {
        public frmMissing()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pref = MDIParent1.prefijo;
            string idpro = "";
            if (idpropuesta_txt.Text.Trim() != "" && prefijo_txt.Text.Trim() != "")
            {
                pref = prefijo_txt.Text;
                idpro = idpropuesta_txt.Text;
            }
            label4.Text = "Procesando...";
            string date = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            this.Enabled = false;
            Task.Run(async () => {
                ApiMissing apmiss = new ApiMissing();
                int rest = await apmiss.Get(date, pref, idpro);
                this.Invoke((MethodInvoker)delegate
                {
                    
                    if (rest == 0)
                        label4.Text = $"No se encontraron resultados";
                    else
                    {
                        label4.Text = $"Se encontraron {rest} resultados, estará actualizando la(s) propuesta(s)";
                    }
                    this.Enabled = true;
                });
                
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                groupBox2.Visible = true;
                groupBox1.Visible = false;
            }else
            {
                groupBox2.Visible = false;
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                groupBox1.Visible = true;
                prefijo_txt.Text = "";
                idpropuesta_txt.Text = "";
                groupBox2.Visible = false;
            }
            else
            {
                groupBox1.Visible = false;
            }
        }
    }
}
