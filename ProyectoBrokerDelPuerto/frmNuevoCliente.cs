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
    public partial class frmNuevoCliente : Form
    {
        string errores = "";
        public string idSeleccion = "";
        public bool codpostalactivo = false;
        public frmNuevoCliente()
        {
            InitializeComponent();
        }

        


        private bool exist()
        {
            
            if (idSeleccion == "")
            {
                clientes cl = new clientes();
                cl.id = txtIdentificacion.Text;
                if (cl.validar_id())
                    return true;
            }
            

            return false;
        }

       

        private bool validacion()
        {
            errores = "";
            validaciones val = new validaciones();

            if (exist())
            {
                errores += "\nEl número de identificación ya existe y está en uso";
            }

            string[] camposdescarte = {"txtMail", "txtApellidos", "txtTelefono", "txtFechaNacimiento" };

            if (!val.camposvacios_grupos_condescartados(groupBox1, camposdescarte))
            {
                errores += "\nCampos obligatorios están vacíos";
            }

            /*if (!val.esnumero(txtIdentificacion.Text))
            {
                errores += "\nLa identificación debe tener sólo números";
            }*/

            /*if (!val.estexto(txtApellidos.Text))
            {
                errores += "\nLos apellidos deben llevar solo letras";
            }*/

            /*if (!val.estexto(txtNombres.Text))
            {
                errores += "\nLos nombres deben llevar solo letras";
            }*/

            if (!val.esnumero(txtTelefono.Text) && txtTelefono.Text != "")
            {
                errores += "\nEl teléfono debe tener sólo números";
            }
            txtCodpostal.Text = txtCodpostal.Text.Trim();
            if (!val.esnumero(txtCodpostal.Text) && txtCodpostal.Text != "")
            {
                errores += "\nEl código postal debe tener sólo números";
            }

            if(txtMail.Text != "")
            {
                string[] correos_varios = txtMail.Text.Split(',');

                for (int i = 0; i < correos_varios.Length; i++)
                {
                    if (!val.correo(correos_varios[i].Trim()))
                    {
                        errores += "\nEl correo ("+ correos_varios[i] + ") está mal escrito";
                    }
                }
                
            }
            

            if(txtFechaNacimiento.Text != "")
            {
                try
                {
                    Convert.ToDateTime(txtFechaNacimiento.Text);
                }
                catch (Exception ex)
                {
                    errores += "\nLa fecha de nacimiento está mal escrita";
                    txtFechaNacimiento.Text = "";
                    txtFechaNacimiento.Focus();
                    Console.Write("Error "+ex.Message);
                }
            }

           
            if(errores != "")
            {
                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.validacion())
            {
                clientes cl = new clientes();
                cl.id = txtIdentificacion.Text;
                cl.tipo_id = comboBox1.Text;
                cl.nombres = txtNombres.Text.Trim();
                cl.apellidos = txtApellidos.Text.Trim();
                cl.telefono = txtTelefono.Text;
                cl.direccion = txtDireccion.Text.Trim();
                cl.email = txtMail.Text.Trim();
                cl.codpostal = txtCodpostal.Text;
                cl.localidad = txtLocalidad.Text;
                cl.ciudad = txtCiudad.Text;
                if (txtFechaNacimiento.Text != "")
                    cl.fecha_nacimiento = Convert.ToDateTime(txtFechaNacimiento.Text).ToString("yyyy-MM-dd");
                else
                    cl.fecha_nacimiento = "1800-01-01";
                cl.sexo = txtSexo.Text;
                cl.situacion = txtSituacion.Text;
                DateTime dt = DateTime.Now;
                cl.ultmod = dt.ToString("yyyy-MM-dd HH:mm:ss");
                cl.user_edit = MDIParent1.sesionUser;
                cl.codestado = "1";
                cl.categoria = txtCategoria.Text;
                if (cl.save())
                {
                    this.DialogResult = DialogResult.OK;
                }

            }
            else
            {
                MessageBox.Show("No puede guardar los datos por lo siguiente : " + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void frmNuevoCliente_Load(object sender, EventArgs e)
        {
            if(this.idSeleccion != "")
            {
                txtIdentificacion.Text = this.idSeleccion;
                txtIdentificacion.ReadOnly = true;
            }
        }

        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {
            txtIdentificacion.BackColor = Color.White;
            label2.Text = "No. Identificación";
            label2.ForeColor = Color.Black;
        }

        private void txtIdentificacion_Leave(object sender, EventArgs e)
        {
            if (txtIdentificacion.Text != "")
            {
                txtIdentificacion.BackColor = Color.White;
                label2.Text = "No. Identificación";
                label2.ForeColor = Color.Black;

                if (exist())
                {
                    txtIdentificacion.Select();
                    txtIdentificacion.BackColor = Color.LightSalmon;
                    label2.Text = "El número de identificación ya existe";
                    label2.ForeColor = Color.Red;
                }
            }
        }

        private void txtCodpostal_TextChanged(object sender, EventArgs e)
        {
            if(txtCodpostal.Text.Length > 3)
                this.buscarCodPostal(txtCodpostal.Text);
            if (txtCodpostal.Text.Length < 4)
                codpostalactivo = true;
        }

        private void buscarCodPostal(string texto)
        {
            if (codpostalactivo)
            {
                frmVistaProvincias frm = new frmVistaProvincias();
                frm.busqueda_txt.Text = texto;
                frm.busqueda();
                frm.seleccionar_btn.Select();
                codpostalactivo = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.dataGridView1.Rows.Count > 0)
                    {
                        txtCodpostal.Text = frm.dataGridView1.CurrentRow.Cells["codpostal"].Value.ToString().Trim();
                        txtLocalidad.Text = frm.dataGridView1.CurrentRow.Cells["provincia"].Value.ToString().Trim();
                        txtCiudad.Text = frm.dataGridView1.CurrentRow.Cells["ciudad"].Value.ToString().Trim();
                    }
                }
            }

        }

        private void txtLocalidad_TextChanged(object sender, EventArgs e)
        {
            if (txtLocalidad.Text.Length > 3)
                this.buscarCodPostal(txtLocalidad.Text);
            if (txtLocalidad.Text.Length < 3)
                codpostalactivo = true;
        }

        private void txtCiudad_TextChanged(object sender, EventArgs e)
        {
            if (txtCiudad.Text.Length > 2)
                this.buscarCodPostal(txtCiudad.Text);
            if (txtCiudad.Text.Length < 3)
                codpostalactivo = true;
        }

        private void txtFechaNacimiento_KeyUp(object sender, KeyEventArgs e)
        {
            validaciones val = new validaciones();

            if ((int)e.KeyCode != (int)Keys.Back)
            {
                txtFechaNacimiento.Text = val.fecha_textbox(txtFechaNacimiento.Text);
                txtFechaNacimiento.Select(txtFechaNacimiento.Text.Length, 0);
            }
        }
    }
}
