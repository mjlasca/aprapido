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
    public partial class frmNewCommission : Form
    {
        public frmNewCommission()
        {
            InitializeComponent();
        }

        private void frmNewCommission_Load(object sender, EventArgs e)
        {
            this.get_commissions();
        }

        public void get_commissions()
        {
            commission_user cus = new commission_user();
            List<commission_user> listcus = new List<commission_user>();
            listcus = cus.get_list(user_txt.Text);

            dataGridView1.Rows.Clear();

            foreach (commission_user item in listcus) { 
                dataGridView1.Rows.Add(
                    item.reg,
                    item.date_ini,
                    item.comm_prima,
                    item.comm_premio
                );
            }

        }

        private void save_btn_Click(object sender, EventArgs e)
        {

            

            if(user_txt.Text != "" && comisionpremio_txt.Text != "" && comisionprima_txt.Text != "")
            {
                double comprima = Convert.ToDouble(comisionprima_txt.Text.Replace(",","."));
                double compremio = Convert.ToDouble(comisionpremio_txt.Text.Replace(",", "."));

                if(comprima < 0 || comprima > 100)
                {
                    MessageBox.Show("La comisión de prima debe ser un número entre 1 y 100", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                    
                if (compremio < 0 || compremio > 100)
                {
                    MessageBox.Show("La comisión de premio debe ser un número entre 1 y 100", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                    

                this.save_reg();

            }else
            {
                MessageBox.Show("No se ha asignado valores de comisión", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private bool save_reg()
        {
            bool res = false;

            commission_user cus = new commission_user();
            cus.date_ini = date1.Value.ToString("yyyy-MM-dd");
            cus.user_comm = user_txt.Text;
            cus.comm_prima = comisionprima_txt.Text ;
            cus.comm_premio = comisionpremio_txt.Text;
            cus.useredit = MDIParent1.sesionUser;
            cus.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if(!cus.save())
            {
                comisionpremio_txt.Text = "";
                comisionprima_txt.Text = "";
                MessageBox.Show("No se ha podido guardar el registro");
            }

            this.get_commissions();

            return res;
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                if(dataGridView1.CurrentRow.Cells["reg"].Value != null)
                {
                    if (MessageBox.Show("¿Está Segur@ de eliminar el registro?", "Eliminar registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        commission_user cus = new commission_user();
                        if (!cus.delete_reg(dataGridView1.CurrentRow.Cells["reg"].Value.ToString()))
                        {
                            MessageBox.Show("No se ha podido Eliminar el registro");
                        }
                        else
                        {
                            this.get_commissions();
                        }
                    }
                }
            }
        }

        private void frmNewCommission_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
