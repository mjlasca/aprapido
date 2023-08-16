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
    public partial class frmNuevaCobertura : Form
    {
        string errores = "";
        public bool edit = false;
        public string reg = "";
        public frmNuevaCobertura()
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.validacion())
            {
                coberturas cob = new coberturas();
                if(this.reg != "")
                    cob.reg = this.reg;
                cob.nombre = txtNombre.Text;
                cob.suma = txtSuma.Text;
                cob.gastos = txtGastos.Text;
                cob.deducible = txtDeducible.Text;
                cob.vrMensual = txtVrMensual.Text;
                cob.vrTrimestral = txtVrTrimestral.Text;
                cob.vrSemestral = txtVrSemestral.Text;
                cob.x21 = txtX21.Text;
                cob.x32 = txtX32.Text;
                cob.x64 = txtX64.Text;
                DateTime dt = DateTime.Now;
                cob.ultmod = dt.ToString("yyyy-MM-dd HH:mm:ss");
                cob.user_edit = MDIParent1.sesionUser;
                cob.codestado = "1";
                if (cob.save())
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show("No puede guardar los datos por lo siguiente : " + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            txtNombre.BackColor = Color.White;
            coberturas cob = new coberturas();
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && edit == false)
            {
                txtNombre.BackColor = Color.White;
                coberturas cob = new coberturas();
                cob.nombre = txtNombre.Text;
                if (cob.exist_vista())
                {
                    txtNombre.Select();
                    txtNombre.BackColor = Color.MediumVioletRed;
                }
            }
        }
    }
}
