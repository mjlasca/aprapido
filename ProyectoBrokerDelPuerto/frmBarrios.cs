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
    public partial class frmBarrios : Form
    {
        int[] access;
        public frmBarrios()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmNuevoBarrio frm = new frmNuevoBarrio();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.busqueda_grid();
            }
        }

        private void busqueda_grid()
        {
            barrios ba = new barrios();
            DataSet ds = ba.get_all_busqueda(busqueda.Text);
            dataGridView1.Rows.Clear();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(
                            ds.Tables[0].Rows[i]["reg"].ToString(),
                            ds.Tables[0].Rows[i]["id"].ToString(),
                            ds.Tables[0].Rows[i]["nombre"].ToString(),
                            ds.Tables[0].Rows[i]["telefono"].ToString(),
                            ds.Tables[0].Rows[i]["direccion"].ToString(),
                            ds.Tables[0].Rows[i]["email"].ToString(),
                            ds.Tables[0].Rows[i]["sub_barrio"].ToString(),
                            ds.Tables[0].Rows[i]["clase_barrio"].ToString()
                         );
                    }
                }
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void frmBarrios_Load(object sender, EventArgs e)
        {
            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("barrios");

            button2.Visible = Convert.ToBoolean(this.access[1]);
            button3.Visible = Convert.ToBoolean(this.access[1]);
            button6.Visible = Convert.ToBoolean(this.access[1]);
            button4.Visible = Convert.ToBoolean(this.access[2]);
            button5.Visible = Convert.ToBoolean(this.access[3]);

            this.busqueda_grid();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            frmNuevoBarrio frm = new frmNuevoBarrio();
            barrios bar = new barrios();
            bar.id = dataGridView1.CurrentRow.Cells["idBarrio"].Value.ToString();
            DataSet ds = bar.get();

            frm.idSeleccion = dataGridView1.CurrentRow.Cells["idBarrio"].Value.ToString();
            frm.txtNombres.Text = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
            frm.txtTelefono.Text = dataGridView1.CurrentRow.Cells["telefono"].Value.ToString();
            frm.txtDireccion.Text = dataGridView1.CurrentRow.Cells["direccion"].Value.ToString();
            frm.txtMail.Text = dataGridView1.CurrentRow.Cells["mail"].Value.ToString();
            
            if(ds.Tables[0].Rows[0]["suma_muerte"] != null)
                frm.txtSumaMuerte.Text = ds.Tables[0].Rows[0]["suma_muerte"].ToString();
            if (ds.Tables[0].Rows[0]["suma_gm"] != null)
                frm.txtSumaGm.Text = ds.Tables[0].Rows[0]["suma_gm"].ToString();
            if (ds.Tables[0].Rows[0]["suma_rc"] != null)
                frm.txtSumaRc.Text = ds.Tables[0].Rows[0]["suma_rc"].ToString();
            if (ds.Tables[0].Rows[0]["exige"] != null)
                frm.txtExige.Text = ds.Tables[0].Rows[0]["exige"].ToString();
            if (ds.Tables[0].Rows[0]["observaciones"] != null)
                frm.txtObservaciones.Text = ds.Tables[0].Rows[0]["observaciones"].ToString();

            if (dataGridView1.CurrentRow.Cells["sub_barrio"].Value == null)
                frm.txtSubBarrio.Text = "";
            else
                frm.txtSubBarrio.Text = dataGridView1.CurrentRow.Cells["sub_barrio"].Value.ToString();

            if (dataGridView1.CurrentRow.Cells["clase_barrio"].Value == null)
                frm.txtClaseBarrio.Text = "";
            else
                frm.txtClaseBarrio.Text = dataGridView1.CurrentRow.Cells["clase_barrio"].Value.ToString();


            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.busqueda_grid();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está segur@ de eliminar el barrio?", "Eliminar barrio", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                barrios ba = new barrios();
                ba.reg = dataGridView1.CurrentRow.Cells["reg"].Value.ToString();
                ba.delete();
                this.busqueda_grid();
            }
        }

        private void busqueda_TextChanged(object sender, EventArgs e)
        {
            if(busqueda.Text.Length > 3)
                this.busqueda_grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.busqueda_grid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            excelDocuments xls = new excelDocuments();
            string ruta = "";

            MDIParent1.rutaInformes_global = ruta;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.xlsx)|*.xls|Todos los archivos (*.*)|*.*";
            saveFileDialog.FileName = "Base de datos de barrios " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                ruta = @FileName;

                //Función para generar base de datos de la tabla en cuestión
                xls.baseExcel(ruta, this.generar_informe());
            }

            
        }


        private DataTable generar_informe()
        {
            DataTable dt = new DataTable();

            //columnas
            dt.Columns.Add("cuit", typeof(string));
            dt.Columns.Add("nombre", typeof(string));
            dt.Columns.Add("telefono", typeof(string));
            dt.Columns.Add("direccion", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("sub_barrio", typeof(string));
            dt.Columns.Add("clase_barrio", typeof(string));
            dt.Columns.Add("suma_muerte_exigida", typeof(string));
            dt.Columns.Add("suma_gm_exigida", typeof(string));
            dt.Columns.Add("suma_rc_exigida", typeof(string));
            dt.Columns.Add("suma_exigida", typeof(string));
            dt.Columns.Add("observaciones", typeof(string));

            barrios ba = new barrios();
            DataSet ds = ba.get_all();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                dt.Rows.Add(
                    ds.Tables[0].Rows[i]["id"].ToString(),
                    ds.Tables[0].Rows[i]["nombre"].ToString(),
                    ds.Tables[0].Rows[i]["telefono"].ToString(),
                    ds.Tables[0].Rows[i]["direccion"].ToString(),
                    ds.Tables[0].Rows[i]["email"].ToString(),
                    ds.Tables[0].Rows[i]["sub_barrio"].ToString(),
                    ds.Tables[0].Rows[i]["clase_barrio"].ToString(),
                    ds.Tables[0].Rows[i]["suma_muerte"].ToString(),
                    ds.Tables[0].Rows[i]["suma_gm"].ToString(),
                    ds.Tables[0].Rows[i]["suma_rc"].ToString(),
                    ds.Tables[0].Rows[i]["exige"].ToString(),
                    ds.Tables[0].Rows[i]["observaciones"].ToString()
                );
            }
            return dt;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmClientesBusqueda frm = new frmClientesBusqueda();
            frm.mostrarBoton = false;
            frm.ShowDialog();
        }
    }
}
