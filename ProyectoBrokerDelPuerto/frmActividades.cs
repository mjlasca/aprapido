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
    public partial class frmActividades : Form
    {
        string errores = "";
        string id_ = "";
        int[] access;
        public frmActividades()
        {
            InitializeComponent();
        }

        private bool validacion()
        {
            errores = "";
            validaciones val = new validaciones();


            if (!val.camposvacios(this))
            {
                errores += "\nNo debe haber campos vacíos";
            }

            if (errores != "")
            {
                return false;
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.validacion())
            {
                actividades ac = new actividades();
                if (id_ != "")
                    ac.id = id_;
                //ac.id = txtIdentificacion.Text;
                ac.nombre = txtNombre.Text;
                ac.cod = txtCodigo.Text;
                DateTime dt = DateTime.Now;
                ac.ultmod = dt.ToString("yyyy-MM-dd HH:mm:ss");
                ac.user_edit = MDIParent1.sesionUser;
                ac.codestado = "1";
                if (ac.save())
                {
                    this.llenar_grid();
                    id_ = "";
                    txtNombre.Text = "";
                    txtCodigo.Text = "";
                }
            }
            else
            {
                MessageBox.Show("No puede guardar los datos por lo siguiente : " + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llenar_grid()
        {
            actividades ac = new actividades();
            DataSet ds = ac.get_all_estado();
            dataGridView1.Rows.Clear();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(ds.Tables[0].Rows[i]["id"].ToString(), ds.Tables[0].Rows[i]["cod"].ToString(), ds.Tables[0].Rows[i]["nombre"].ToString());
                    }
                }
            }

        }

        private void frmActividades_Load(object sender, EventArgs e)
        {
            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("actividades");

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
            id_ = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            actividades ac = new actividades();
            DataSet ds = ac.get(id_);
            if(ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    txtNombre.Text = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
                    txtCodigo.Text = ds.Tables[0].Rows[0]["cod"].ToString();
                }
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está Segur@ de eliminar la actividad?", "Eliminar actividad", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                actividades ac = new actividades();
                ac.id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                ac.delete();
                this.llenar_grid();
            }
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            actividades ac = new actividades();
            if (ac.codigo_exist(txtCodigo.Text) && txtCodigo.Text != "" && id_ == "")
            {
                txtCodigo.BackColor = Color.MediumVioletRed;
                txtCodigo.Select();
                lblmensaje.Text = "El código ingresado ya existe";
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            lblmensaje.Text = "";
            txtCodigo.BackColor = Color.White;
        }
    }
}
