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
    public partial class frmConfigDB : Form
    {
        public frmConfigDB()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(ip_txt.Text != "" && dbname_txt.Text != "" && userDb_txt.Text != "")
            {
                System.IO.File.WriteAllText("ip.txt", ip_txt.Text.Trim()+"\n"+dbname_txt.Text.Trim()+ "\n"+userDb_txt.Text.Trim()+ "\n"+passDb_txt.Text.Trim());
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Todos los campos (*) son requeridos",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
