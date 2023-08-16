using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBrokerDelPuerto.cobranzas
{
    public partial class frmAseguradoras : Form
    {
        public frmAseguradoras()
        {
            InitializeComponent();
        }

        public bool validacion()
        {
            string errores = "";
            if (txtTipoId.Text == "")
                errores += "\nEl tipo identificación no puede estar vacío";
            if (txtIdentificacion.Text == "")
                errores += "\nEl número identificación no puede estar vacío";
            if (txtRazonSocial.Text == "")
                errores += "\nLa razón social no puede estar vacío";
            if (txtTitular.Text == "")
                errores += "\nEl titular no puede estar vacío";
            validaciones val = new validaciones();
            if(!val.esnumero(txtIdentificacion.Text.Trim()))
                errores += "\nEn el campo identificación se esperaba sólo números";
            if(txtEmail.Text.Trim() != "")
            {
                if (val.esnumero(txtEmail.Text.Trim()))
                    errores += "\nEl Email está mal escrito";
            }


            if (errores != "")
            {
                MessageBox.Show("Hay errores en la validación"+errores, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.validacion())
            {
                this.guardaraseguradora();
            }
        }

        private void guardaraseguradora()
        {
            aseguradoras ase = new aseguradoras();
            ase.razonsocial =  txtRazonSocial.Text;
            ase.id = txtIdentificacion.Text;
            ase.tipo_id = txtTipoId.Text;
            ase.descripcion = txtDescripcion.Text;
            ase.titular = txtTitular.Text;
            ase.telefono = txtTelefono.Text;
            ase.direccion = txtDireccion.Text;
            ase.email = txtEmail.Text;
            if (ase.save())
            {
                this.datosiniciales();
                this.limpiarcampos();
            }
            
        }

        private void limpiarcampos()
        {
            groupBox1.Controls.OfType<TextBox>().ToList().ForEach(tb => tb.Text = String.Empty);
        }

        private void datosiniciales()
        {
            aseguradoras ase = new aseguradoras();
            DataSet ds = ase.get_all();
            dataGridView1.Rows.Clear();
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dataGridView1.Rows.Add(
                    ds.Tables[0].Rows[i]["id"].ToString(),
                    ds.Tables[0].Rows[i]["tipo_id"].ToString(),
                    ds.Tables[0].Rows[i]["razonsocial"].ToString(),
                    ds.Tables[0].Rows[i]["titular"].ToString(),
                    ds.Tables[0].Rows[i]["descripcion"].ToString(),
                    ds.Tables[0].Rows[i]["telefono"].ToString(),
                    ds.Tables[0].Rows[i]["direccion"].ToString(),
                    ds.Tables[0].Rows[i]["email"].ToString()
                );
            }
        }

        private void frmAseguradoras_Load(object sender, EventArgs e)
        {
            this.datosiniciales();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.CurrentCell != null)
            {
                frmBancosAseguradora frm = new frmBancosAseguradora();
                frm.txtidaseg.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                frm.ShowDialog();
            }
        }

        private void editar_aseguradora()
        {
            if (dataGridView1.CurrentRow.Cells["id"].Value != null)
            {
                txtIdentificacion.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                txtTipoId.Text = dataGridView1.CurrentRow.Cells["tipoid"].Value.ToString();
                txtRazonSocial.Text = dataGridView1.CurrentRow.Cells["razonsocial"].Value.ToString();
                txtTitular.Text = dataGridView1.CurrentRow.Cells["titular"].Value.ToString();
                txtDescripcion.Text = dataGridView1.CurrentRow.Cells["descripcion"].Value.ToString();
                txtTelefono.Text = dataGridView1.CurrentRow.Cells["telefono"].Value.ToString();
                txtDireccion.Text = dataGridView1.CurrentRow.Cells["direccion"].Value.ToString();
                txtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["id"].Value != null)
            {

                aseguradoras ase = new aseguradoras();
                ase.id = dataGridView1.CurrentRow.Cells["id"].Value.ToString().Trim();
                ase.delete();
                this.datosiniciales();
                this.limpiarcampos();
            }
        }

        private void editar_btn_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                this.editar_aseguradora();
                txtIdentificacion.Enabled = false;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.xlsx)|*.xls|Todos los archivos (*.*)|*.*";
            saveFileDialog.FileName = "Aseguradoras_db";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try {
                    this.generar_xls_aseguguradoras(saveFileDialog.FileName);
                    MessageBox.Show("Se ha generado el archivo con éxito");
                    System.Diagnostics.Process.Start(@saveFileDialog.FileName);

                }
                catch(Exception ex)
                {
                    Console.WriteLine("No se ha podido generar el archivo" + ex.Message);
                }
            }
            
        }

        private void generar_xls_aseguguradoras(string ruta)
        {
            aseguradoras ase = new aseguradoras();
            DataTable dt = new DataTable();

            dt.Columns.Add("tipo_id", typeof(string));
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("razonsocial", typeof(string));
            dt.Columns.Add("titular", typeof(string));
            dt.Columns.Add("descripcion", typeof(string));
            dt.Columns.Add("direccion", typeof(string));
            dt.Columns.Add("telefono", typeof(string));
            dt.Columns.Add("email", typeof(string));
            DataSet ds = ase.get_all();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add(
                    ds.Tables[0].Rows[i]["tipo_id"].ToString(),
                    ds.Tables[0].Rows[i]["id"].ToString(),
                    ds.Tables[0].Rows[i]["razonsocial"].ToString(),
                    ds.Tables[0].Rows[i]["titular"].ToString(),
                    ds.Tables[0].Rows[i]["descripcion"].ToString(),
                    ds.Tables[0].Rows[i]["direccion"].ToString(),
                    ds.Tables[0].Rows[i]["telefono"].ToString(),
                    ds.Tables[0].Rows[i]["email"].ToString()
                );
            }

            excelDocuments excel = new excelDocuments();
            excel.baseExcel(@ruta, dt);
        }
    }
}
