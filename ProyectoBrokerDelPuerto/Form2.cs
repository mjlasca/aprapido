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
    public partial class frmClientes : Form
    {
        int[] access;
        public frmClientes()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmNuevoCliente frm = new frmNuevoCliente();
            frm.ShowDialog();
            if(frm.DialogResult == DialogResult.OK)
            {
                this.llenarGrid();
            }
        }


        public void llenarGrid()
        {
            clientes cl = new clientes();
            DataSet ds = cl.get_all_clientes();

            dataGridView1.Rows.Clear();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(
                            ds.Tables[0].Rows[i]["tipo_id"].ToString(),
                            ds.Tables[0].Rows[i]["id"].ToString(),
                            ds.Tables[0].Rows[i]["nombres"].ToString(),
                            ds.Tables[0].Rows[i]["apellidos"].ToString(),
                            ds.Tables[0].Rows[i]["telefono"].ToString(),
                            ds.Tables[0].Rows[i]["direccion"].ToString(),
                            ds.Tables[0].Rows[i]["email"].ToString() != "" ? ds.Tables[0].Rows[i]["email"].ToString() : ""
                        );
                    }
                }
            }
            
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("clientes");

            button2.Visible = Convert.ToBoolean(this.access[1]);
            button3.Visible = Convert.ToBoolean(this.access[1]);
            button4.Visible = Convert.ToBoolean(this.access[2]);
            button5.Visible = Convert.ToBoolean(this.access[3]);
            this.llenarGrid();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.descarga();

        }

        public void descarga(bool plantilla = false)
        {
            excelDocuments xls = new excelDocuments();
            string ruta = "";

            MDIParent1.rutaInformes_global = ruta;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.xlsx)|*.xls|Todos los archivos (*.*)|*.*";
            saveFileDialog.FileName = "Base de datos de clientes barrios privados " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                ruta = @FileName;
                //Función para generar base de datos de la tabla en cuestión
                xls.baseExcel(ruta, this.generar_informe(plantilla));
            }

            
        }

        public DataTable generar_informe(bool plantilla = false)
        {
            DataTable dt = new DataTable();

            //columnas
            dt.Columns.Add("tipo_id", typeof(string));
            dt.Columns.Add("no_identificacion", typeof(string));
            dt.Columns.Add("nombres", typeof(string));
            dt.Columns.Add("apellidos", typeof(string));
            dt.Columns.Add("telefono", typeof(string));
            dt.Columns.Add("direccion", typeof(string));
            dt.Columns.Add("codpostal", typeof(string));
            dt.Columns.Add("localidad", typeof(string));
            dt.Columns.Add("ciudad", typeof(string));
            dt.Columns.Add("fecha_nacimiento", typeof(string));
            dt.Columns.Add("sexo", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("situacionimpo", typeof(string));
            dt.Columns.Add("categoria", typeof(string));

            if (plantilla)
                return dt;

            clientes cl = new clientes();
            DataSet ds = cl.get_all();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                
                string fechita = ds.Tables[0].Rows[i]["fecha_nacimiento"].ToString();
                if(fechita != "" && fechita != "0000-00-00" && fechita != "00/00/0000")
                {
                    fechita = Convert.ToDateTime(ds.Tables[0].Rows[i]["fecha_nacimiento"].ToString()).ToString("dd-MM-yyyy");
                }
                dt.Rows.Add(
                    ds.Tables[0].Rows[i]["tipo_id"].ToString(),
                    ds.Tables[0].Rows[i]["id"].ToString(),
                    ds.Tables[0].Rows[i]["nombres"].ToString(),
                    ds.Tables[0].Rows[i]["apellidos"].ToString(),
                    ds.Tables[0].Rows[i]["telefono"].ToString(),
                    ds.Tables[0].Rows[i]["direccion"].ToString(),
                    ds.Tables[0].Rows[i]["codpostal"].ToString(),
                    ds.Tables[0].Rows[i]["localidad"].ToString(),
                    ds.Tables[0].Rows[i]["ciudad"].ToString(),
                    fechita,
                    ds.Tables[0].Rows[i]["sexo"].ToString(),
                    ds.Tables[0].Rows[i]["email"].ToString(),
                    ds.Tables[0].Rows[i]["situacion"].ToString(),
                    ds.Tables[0].Rows[i]["categoria"].ToString()
                );
            }
            return dt;
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmNuevoCliente frm = new frmNuevoCliente();
            clientes cl = new clientes();
            DataSet ds = cl.get(dataGridView1.CurrentRow.Cells["idCliente"].Value.ToString());
            frm.comboBox1.Text = ds.Tables[0].Rows[0]["tipo_id"].ToString();
            frm.idSeleccion = ds.Tables[0].Rows[0]["id"].ToString();
            frm.txtNombres.Text = ds.Tables[0].Rows[0]["nombres"].ToString();
            frm.txtApellidos.Text = ds.Tables[0].Rows[0]["apellidos"].ToString();
            frm.txtTelefono.Text = ds.Tables[0].Rows[0]["telefono"].ToString();
            frm.txtDireccion.Text = ds.Tables[0].Rows[0]["direccion"].ToString();
            frm.txtMail.Text = ds.Tables[0].Rows[0]["email"].ToString();
            frm.txtCodpostal.Text = ds.Tables[0].Rows[0]["codpostal"].ToString();
            frm.txtLocalidad.Text = ds.Tables[0].Rows[0]["localidad"].ToString();
            frm.txtCiudad.Text = ds.Tables[0].Rows[0]["ciudad"].ToString();
            
            if(ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString() != "" && ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString() != "00/00/0000")
                frm.txtFechaNacimiento.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["fecha_nacimiento"]).ToString("dd/MM/yyyy")  ;
            frm.txtSexo.Text = ds.Tables[0].Rows[0]["sexo"].ToString();
            frm.txtSituacion.Text = ds.Tables[0].Rows[0]["situacion"].ToString();
            frm.txtCategoria.Text = ds.Tables[0].Rows[0]["categoria"].ToString();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.busqueda_grid();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está segur@ de eliminar el cliente?", "Eliminar cliente", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                clientes cl = new clientes();
                cl.id = dataGridView1.CurrentRow.Cells["idCliente"].Value.ToString(); 
                cl.delete();
                this.llenarGrid();
            }
        }

        private void busqueda_grid()
        {
            clientes cl = new clientes();
            DataSet ds =  cl.get_all_busqueda(busqueda.Text);
            dataGridView1.Rows.Clear();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(ds.Tables[0].Rows[i]["tipo_id"].ToString(), 
                            ds.Tables[0].Rows[i]["id"].ToString(), 
                            ds.Tables[0].Rows[i]["nombres"].ToString(), 
                            ds.Tables[0].Rows[i]["apellidos"].ToString(), 
                            ds.Tables[0].Rows[i]["telefono"].ToString(), 
                            ds.Tables[0].Rows[i]["direccion"].ToString(), 
                            ds.Tables[0].Rows[i]["email"].ToString()
                        );
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.busqueda_grid();
        }

        private void busqueda_TextChanged(object sender, EventArgs e)
        {
            if(busqueda.Text.Length > 3)
                this.busqueda_grid();
        }
    }
}
