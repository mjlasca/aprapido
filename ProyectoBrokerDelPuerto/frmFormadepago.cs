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
    public partial class frmFormadepago : Form
    {
        public frmFormadepago()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Visible = false;
            textBox1.Visible = false;

            if (comboBox1.Text == "OTRO")
            {
                label2.Visible = true;
                textBox1.Visible = true;
            }
            
        }
    }
}
