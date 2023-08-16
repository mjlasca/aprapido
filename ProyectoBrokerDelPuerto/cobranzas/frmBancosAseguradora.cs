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
    public partial class frmBancosAseguradora : Form
    {

        public frmBancosAseguradora()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            this.savedata();
            
        }

        public void searchdata()
        {
            bancosaseguradora banas = new bancosaseguradora();
            DataSet ds = banas.get_all_aseguradora(txtidaseg.Text);

            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                string aux = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    
                    if(aux != ds.Tables[0].Rows[i]["categoriamediopago"].ToString())
                    {
                        aux = ds.Tables[0].Rows[i]["categoriamediopago"].ToString();
                        dataGridView1.Rows.Add("","","","",aux.ToUpper());
                    }
                    
                    dataGridView1.Rows.Add(
                        ds.Tables[0].Rows[i]["reg"].ToString(),
                        ds.Tables[0].Rows[i]["idaseguradora"].ToString(),
                        ds.Tables[0].Rows[i]["cuit"].ToString(),
                        ds.Tables[0].Rows[i]["categoriamediopago"].ToString(),
                        ds.Tables[0].Rows[i]["nombrebanco"].ToString(),
                        ds.Tables[0].Rows[i]["titularcuenta"].ToString(),
                        ds.Tables[0].Rows[i]["cuenta"].ToString(),
                        ds.Tables[0].Rows[i]["cbu"].ToString(),
                        ds.Tables[0].Rows[i]["sucursal"].ToString(),
                        ds.Tables[0].Rows[i]["direccion"].ToString(),
                        ds.Tables[0].Rows[i]["telefono"].ToString()
                    );
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["cuit"].Value.ToString() == "")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
            
        }

        private bool validate()
        {
            bool res = true;

            string errores = "";

            if (cbMedioPago.Text == "")
                errores += "\nDebe seleccionar el medio de pago";
            if (cbNombreBanco.Text == "")
                errores += "\nDebe seleccionar una entidad";
           

            if (errores != "")
            {
                MessageBox.Show("Error en la validación, por favor revise los siguientes errores: "+errores,
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return res;
        }

        private void savedata()
        {
            if (this.validate())
            {
                bancosaseguradora banAs = new bancosaseguradora();
                if(reg_txt.Text != "")
                    banAs.reg = reg_txt.Text;
                banAs.cuit = txtCuit.Text;
                banAs.idaseguradora = txtidaseg.Text;
                banAs.categoriamediopago = cbMedioPago.Text ;
                banAs.nombrebanco = cbNombreBanco.Text;
                banAs.titularcuenta = txtTitularCuenta.Text;
                banAs.cuenta = txtNumCuenta.Text;
                banAs.cbu = txtCbu.Text;
                banAs.sucursal = txtSucursal.Text;
                banAs.direccion = txtDireccion.Text;
                banAs.telefono = txtTelefono.Text;
                
                if (banAs.save())
                {
                    this.searchdata();
                    this.clean();
                }
                
            }
        }

        private void clean()
        {
            txtCuit.Text = "";
            cbMedioPago.Text = "";
            cbNombreBanco.Text = "";
            txtTitularCuenta.Text = "";
            txtNumCuenta.Text = "";
            txtCbu.Text = "";
            txtSucursal.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            reg_txt.Text = "";
        }

        private void frmBancosAseguradora_Load(object sender, EventArgs e)
        {
            this.searchdata();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow.Cells["idaseguradora"].Value != null)
            {
                if(dataGridView1.CurrentRow.Cells["reg"].Value.ToString() != "")
                {
                    if (MessageBox.Show("¿Está seguro(a) de eliminar el registro?", "Eliminar Registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        bancosaseguradora banAs = new bancosaseguradora();
                        banAs.reg = dataGridView1.CurrentRow.Cells["reg"].Value.ToString();
                        banAs.delete();
                        this.searchdata();
                    }
                }
                
            }
            
        }

        private void modificar_btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["idaseguradora"].Value != null)
            {
                if (dataGridView1.CurrentRow.Cells["reg"].Value.ToString() != "")
                {
                    reg_txt.Text = dataGridView1.CurrentRow.Cells["reg"].Value.ToString();
                    txtCuit.Text = dataGridView1.CurrentRow.Cells["cuit"].Value.ToString();
                    txtidaseg.Text = dataGridView1.CurrentRow.Cells["idaseguradora"].Value.ToString();
                    cbMedioPago.Text = dataGridView1.CurrentRow.Cells["mediopago"].Value.ToString();
                    cbNombreBanco.Text = dataGridView1.CurrentRow.Cells["nombrebanco"].Value.ToString();
                    txtTitularCuenta.Text = dataGridView1.CurrentRow.Cells["titularcuenta"].Value.ToString();
                    txtNumCuenta.Text = dataGridView1.CurrentRow.Cells["numcuenta"].Value.ToString();
                    txtCbu.Text = dataGridView1.CurrentRow.Cells["cbu"].Value.ToString();
                    txtSucursal.Text = dataGridView1.CurrentRow.Cells["sucursal"].Value.ToString();
                    txtDireccion.Text = dataGridView1.CurrentRow.Cells["direccion"].Value.ToString();
                    txtTelefono.Text = dataGridView1.CurrentRow.Cells["telefono"].Value.ToString();
                }
            }
        }
    }
}
