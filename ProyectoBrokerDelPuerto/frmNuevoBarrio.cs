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
    public partial class frmNuevoBarrio : Form
    {
        string errores = "";
        public string idSeleccion = "";
        public string cuitBarr = "";
        public frmNuevoBarrio()
        {
            InitializeComponent();
        }
        private bool validacion()
        {
            errores = "";
            validaciones val = new validaciones();


            /*if (!val.camposvacios_grupos(groupBox1))
            {
                errores += "\nNo debe haber campos vacíos";
            }*/

            if(txtIdentificacion.Text == "")
            {
                errores += "\nEl CUIT es obligatorio";
            }
            if (txtNombres.Text == "")
            {
                errores += "\nEl nombre es obligatorio";
            }

            if (!val.esnumero(txtIdentificacion.Text))
            {
                errores += "\nLa identificación debe tener sólo números";
            }

            /*if (!val.estexto(txtNombres.Text))
            {
                errores += "\nLos nombres deben llevar solo letras";
            }*/

            if (!val.esnumero(txtTelefono.Text) && txtTelefono.Text != "")
            {
                errores += "\nEl teléfono debe tener sólo números";
            }

            if (!val.correo(txtMail.Text) && txtMail.Text != "")
            {
                errores += "\nEl correo electrónico está mal escrito";
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
                barrios ba = new barrios();
                ba.id = txtIdentificacion.Text.TrimStart().Trim();
                ba.nombre = txtNombres.Text.Trim();
                ba.telefono = txtTelefono.Text.Trim();
                ba.direccion = txtDireccion.Text.Trim();
                ba.email = txtMail.Text.Trim();
                ba.sub_barrio = txtSubBarrio.Text.Trim();
                ba.clase_barrio = txtClaseBarrio.Text.Trim();

                ba.suma_muerte = txtSumaMuerte.Text.Trim();
                ba.suma_gm = txtSumaGm.Text.Trim();
                ba.suma_rc = txtSumaRc.Text.Trim();
                ba.exige = txtExige.Text.Trim();
                ba.observaciones = txtObservaciones.Text.Trim();

                ba.codestado = "1";
                DateTime dt = DateTime.Now;
                ba.ultmod = dt.ToString("yyyy-MM-dd HH:mm:ss");
                ba.user_edit = MDIParent1.sesionUser;
                ba.envionube = 1;
                if (ba.save())
                {
                    this.cuitBarr = ba.nombre;
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show("No puede guardar los datos por lo siguiente : " + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmNuevoBarrio_Load(object sender, EventArgs e)
        {
            if (this.idSeleccion != "")
            {
                txtIdentificacion.Text = this.idSeleccion;
                txtIdentificacion.ReadOnly = true;
            }
        }

        public bool exist()
        {
            
            barrios ba = new barrios();
            if (idSeleccion == "")
            {
                ba.id = txtIdentificacion.Text;
                if (ba.validar_id())
                    return true;
                
            }
            return false;
        }

        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {
            txtIdentificacion.BackColor = Color.White;
            label2.Text = "CUIT";
            label2.ForeColor = Color.Black;
        }

        private void txtIdentificacion_Leave(object sender, EventArgs e)
        {
            if(txtIdentificacion.Text != "")
            {
                txtIdentificacion.BackColor = Color.White;
                label2.Text = "CUIT";
                label2.ForeColor = Color.Black;
                if (exist())
                {
                    txtIdentificacion.BackColor = Color.LightSalmon;
                    txtIdentificacion.Select();
                    label2.Text = "Éste CUIT ya existe";
                    label2.ForeColor = Color.Red;
                }
                

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
