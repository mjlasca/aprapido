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
    
    public partial class frmCoberturas : Form
    {
        int[] access;
        public frmCoberturas()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmNuevaCobertura frm = new frmNuevaCobertura();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.llenar_grid();
            }
        }
        private void llenar_grid()
        {
            coberturas cob = new coberturas();
            DataSet ds = cob.get_all_busqueda(textBox1.Text);
            dataGridView1.Rows.Clear();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(ds.Tables[0].Rows[i]["reg"].ToString(), ds.Tables[0].Rows[i]["nombre"].ToString(), ds.Tables[0].Rows[i]["suma"].ToString(), ds.Tables[0].Rows[i]["gastos"].ToString(), ds.Tables[0].Rows[i]["deducible"].ToString(), ds.Tables[0].Rows[i]["vrMensual"].ToString(), ds.Tables[0].Rows[i]["vrTrimestral"].ToString(), ds.Tables[0].Rows[i]["vrSemestral"].ToString(), ds.Tables[0].Rows[i]["x21"].ToString(), ds.Tables[0].Rows[i]["x32"].ToString(), ds.Tables[0].Rows[i]["x64"].ToString());
                    }
                }
            }

        }

        private void frmCoberturas_Load(object sender, EventArgs e)
        {
            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("coberturas");
            
            button2.Visible = Convert.ToBoolean(this.access[1]);
            button3.Visible = Convert.ToBoolean(this.access[1]);
            button4.Visible = Convert.ToBoolean(this.access[2]);

            this.llenar_grid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmNuevaCobertura frm = new frmNuevaCobertura();
            frm.edit = true;
            frm.txtNombre.ReadOnly = true;
            frm.reg = dataGridView1.CurrentRow.Cells["reg"].Value.ToString();
            frm.txtNombre.Text = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
            frm.txtSuma.Text = dataGridView1.CurrentRow.Cells["suma"].Value.ToString();
            frm.txtGastos.Text = dataGridView1.CurrentRow.Cells["gastos"].Value.ToString();
            frm.txtDeducible.Text = dataGridView1.CurrentRow.Cells["deducible"].Value.ToString();
            frm.txtVrMensual.Text = dataGridView1.CurrentRow.Cells["vrMensual"].Value.ToString();
            frm.txtVrTrimestral.Text = dataGridView1.CurrentRow.Cells["vrTrimestral"].Value.ToString();
            frm.txtVrSemestral.Text = dataGridView1.CurrentRow.Cells["vrSemestral"].Value.ToString();
            frm.txtX21.Text = dataGridView1.CurrentRow.Cells["x21"].Value.ToString();
            frm.txtX32.Text = dataGridView1.CurrentRow.Cells["x32"].Value.ToString();
            frm.txtX64.Text = dataGridView1.CurrentRow.Cells["x64"].Value.ToString();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.llenar_grid();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            coberturas cob = new coberturas();
            cob.reg = dataGridView1.CurrentRow.Cells["reg"].Value.ToString();
            cob.nombre = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
            cob.delete();
            this.llenar_grid();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.llenar_grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.llenar_grid();
        }
    }
}
