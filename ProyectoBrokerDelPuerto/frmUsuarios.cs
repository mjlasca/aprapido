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
    public partial class frmUsuarios : Form
    {
        int[] access;
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmNuevoUsuario frm = new frmNuevoUsuario();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.llenar_grid();
            }
        }

        private void llenar_grid()
        {
            usuarios user = new usuarios();
            DataSet ds = user.get_all();
            dataGridView1.Rows.Clear();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if(ds.Tables[0].Rows[i]["loggin"].ToString() != "online")
                        {
                            bool all = false;
                            if (ds.Tables[0].Rows[i]["allow"].ToString() == "1")
                                all = true;
                            dataGridView1.Rows.Add(
                                ds.Tables[0].Rows[i]["id"].ToString(),
                                ds.Tables[0].Rows[i]["nombre"].ToString(),
                                ds.Tables[0].Rows[i]["loggin"].ToString(),
                                ds.Tables[0].Rows[i]["mail"].ToString(),
                                ds.Tables[0].Rows[i]["perfil"].ToString(),
                                ds.Tables[0].Rows[i]["codigoproductor"].ToString(),
                                ds.Tables[0].Rows[i]["codorganizador"].ToString(),
                                ds.Tables[0].Rows[i]["comisionprima"].ToString(),
                                ds.Tables[0].Rows[i]["comisionpremio"].ToString(),
                                all,
                                ds.Tables[0].Rows[i]["adminempresa"].ToString() != "0" ? true : false
                            );
                        }
                        
                    }
                }
            }

        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("usuarios");

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
            frmNuevoUsuario frm = new frmNuevoUsuario();
            frm.idSeleccionado = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            frm.txtLoggin.Text = dataGridView1.CurrentRow.Cells["loggin"].Value.ToString();

            usuarios usu = new usuarios();
            usu.loggin = frm.txtLoggin.Text;
            DataSet ds = usu.get();

            frm.txtNombre.Text = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
            frm.txtMail.Text = dataGridView1.CurrentRow.Cells["mail"].Value.ToString();
            frm.perfilCombo = dataGridView1.CurrentRow.Cells["perfil"].Value.ToString();
            frm.comisionpremio_txt.Text = ds.Tables[0].Rows[0]["comisionpremio"].ToString();
            frm.comisionprima_txt.Text = ds.Tables[0].Rows[0]["comisionprima"].ToString();
            frm.codProducto_txt.Text = ds.Tables[0].Rows[0]["codigoproductor"].ToString();
            frm.organizador_txt.Text = ds.Tables[0].Rows[0]["codorganizador"].ToString();

            if (dataGridView1.CurrentRow.Cells["allow"].Value != null)
                frm.checkBox1.Checked = Convert.ToBoolean( dataGridView1.CurrentRow.Cells["allow"].Value );
            if (dataGridView1.CurrentRow.Cells["adminempresa"].Value != null)
                frm.checkBox2.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["adminempresa"].Value);

            frm.ShowDialog();
            
            if (frm.DialogResult == DialogResult.OK)
            {
                this.llenar_grid();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está segur@ de eliminar el usuario?", "Eliminar usuario", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                usuarios user = new usuarios();
                user.id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                user.delete();
                this.llenar_grid();
            }
        }
    }
}
