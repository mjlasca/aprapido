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
    public partial class frmClasificaciones : Form
    {
        string id_ = "";
        string errores = "";
        int[] access;
        public frmClasificaciones()
        {
            InitializeComponent();
        }

        private bool validacion()
        {
            errores = "";
            validaciones val = new validaciones();


            clasificaciones cla = new clasificaciones();
            if (cla.codigo_exist(txtCodigo.Text) && txtCodigo.Text != "" && id_ == "")
            {
                actividades ac = new actividades();
                if(cla.codigo_actividad_exist(txtCodigo.Text, ac.get_nombre(comboBox1.Text).Tables[0].Rows[0]["id"].ToString()))
                {
                    txtCodigo.BackColor = Color.MediumVioletRed;
                    txtCodigo.Select();
                    errores += "\nEl código ingresado con la actividad seleccionada ya existe";
                }
                
            }
            if (txtNombre.Text == "")
            {
                errores += "\nEl nombre de la clasificación no puede estar vacío";
            }
            if(comboBox1.Text == "")
            {
                errores += "\nDebe seleccionar la actividad a la que pertenece";
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
                clasificaciones cla = new clasificaciones();
                actividades ac = new actividades();
                if (id_ != "")
                    cla.id = id_;
                //ac.id = txtIdentificacion.Text;
                cla.nombre = txtNombre.Text;
                cla.cod = txtCodigo.Text;
                cla.id_actividad = ac.get_nombre(comboBox1.Text).Tables[0].Rows[0]["id"].ToString();
                DateTime dt = DateTime.Now;
                cla.ultmod = dt.ToString("yyyy-MM-dd HH:mm:ss");
                cla.user_edit = MDIParent1.sesionUser;
                cla.codestado = "1";
                if (cla.save())
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
            clasificaciones cla = new clasificaciones();

            actividades act = new actividades();

            cla.cod = busqueda_txt.Text;
            cla.nombre = busqueda_txt.Text;
            cla.id_actividad = act.get_id(comboBox1.Text);
            DataSet ds = cla.get_all();

            dataGridView1.Rows.Clear();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        actividades ac = new actividades();
                        dataGridView1.Rows.Add(ds.Tables[0].Rows[i]["id"].ToString(), ds.Tables[0].Rows[i]["cod"].ToString(), ds.Tables[0].Rows[i]["nombre"].ToString(), ac.get( ds.Tables[0].Rows[i]["id_actividad"].ToString() ).Tables[0].Rows[0]["nombre"].ToString());
                    }
                }
            }
        }

        /*
        * Combo con los nombres de los Actividades
        */
        private void llenarComboActividad()
        {
            actividades ac = new actividades();
            DataSet ds = ac.get_all();
            
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboBox1.Items.Add(ds.Tables[0].Rows[i]["cod"].ToString() +" - "+ ds.Tables[0].Rows[i]["nombre"].ToString());
                }
            }

        }


        private void frmClasificaciones_Load(object sender, EventArgs e)
        {
            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("clasificaciones");

            button2.Visible = Convert.ToBoolean(this.access[1]);
            button3.Visible = Convert.ToBoolean(this.access[1]);
            button4.Visible = Convert.ToBoolean(this.access[2]);

            this.llenar_grid();
            this.llenarComboActividad();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            id_ = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            txtCodigo.Text = dataGridView1.CurrentRow.Cells["cod"].Value.ToString();
            txtNombre.Text = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
            if(dataGridView1.CurrentRow.Cells["actividad"].Value.ToString() != "")
            {
                actividades act = new actividades();
                actividades ac = new actividades();
                comboBox1.Text = ac.get( act.get_id(dataGridView1.CurrentRow.Cells["actividad"].Value.ToString()) ).Tables[0].Rows[0]["cod"].ToString() + " - " + dataGridView1.CurrentRow.Cells["actividad"].Value.ToString();
            }
                
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está Segur@ de eliminar la clasificación?", "Eliminar clasificación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                clasificaciones cla = new clasificaciones();
                cla.id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                cla.delete();
                this.llenar_grid();
            }
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            lblmensaje.Text = "";
            txtCodigo.BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.llenar_grid();
        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 1)
            {
                if (double.Parse(e.CellValue1.ToString()) > double.Parse(e.CellValue2.ToString()))
                {
                    e.SortResult = 1;
                }
                else if (double.Parse(e.CellValue1.ToString()) < double.Parse(e.CellValue2.ToString()))
                {
                    e.SortResult = -1;
                }
                else
                {
                    e.SortResult = 0;
                }
                e.Handled = true;
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            
        }
    }
}
