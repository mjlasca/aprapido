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
    public partial class frmNuevoUsuario : Form
    {
        string errores = "";
        public string idSeleccionado = "";
        public string perfilCombo = "";
        public frmNuevoUsuario()
        {
            InitializeComponent();
        }

        public void llenarCombo()
        {
            perfiles per = new perfiles();
            comboPerfil.Items.Clear();
            DataSet ds = per.get_all();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        comboPerfil.Items.Add(ds.Tables[0].Rows[i]["nombre"].ToString());
                    }
                }
            }


        }

        private bool validacion()
        {
            errores = "";
            validaciones val = new validaciones();


            string[] campos = { "txtPass", "codProducto_txt", "comisionprima_txt", "comisionpremio_txt" };

            if (!val.camposvacios_condescartados(this,campos))
            {
                    errores += "\nNo debe haber campos vacíos";
            }

            if(txtLoggin.Text != "" && idSeleccionado == "")
            {
                usuarios usu = new usuarios();
                usu.loggin = txtLoggin.Text;
                DataSet ds = usu.get();
                if(ds.Tables.Count > 0)
                {
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        errores += "\nEl nombre de usuario ya existe";
                    }
                }
            }

            if (!val.correo(txtMail.Text))
            {
                errores += "\nEl correo electrónico no es válido";
            }

            if (comisionpremio_txt.Text != "")
            {
                if (!val.esnumero(comisionpremio_txt.Text))
                {
                    errores += "\nLa comisión premio no es un número";
                }
                else
                {
                    if (Convert.ToDouble(comisionpremio_txt.Text) < 0 || Convert.ToDouble(comisionpremio_txt.Text) > 100)
                        errores += "\nLa comisión premio es un porcentaje debe estar entre 0 y 100";
                }
            }

            if(comisionprima_txt.Text != "")
            {
                if (!val.esnumero(comisionprima_txt.Text))
                {
                    errores += "\nLa comisión prima no es un número";
                }
                else
                {
                    if (Convert.ToDouble(comisionprima_txt.Text) < 0 || Convert.ToDouble(comisionprima_txt.Text) > 100)
                        errores += "\nLa comisión prima es un porcentaje debe estar entre 0 y 100";
                }
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
                usuarios user = new usuarios();
                if (idSeleccionado != "")
                    user.id = idSeleccionado;
                user.nombre = txtNombre.Text;
                user.loggin = txtLoggin.Text;
                user.pass = txtPass.Text;
                user.mail = txtMail.Text;
                user.perfil = comboPerfil.Text;
                user.allow = Convert.ToInt16( checkBox1.Checked ).ToString();
                if (comisionpremio_txt.Text.Trim() == "")
                    comisionpremio_txt.Text = "0";
                user.comisionpremio = comisionpremio_txt.Text;
                if (comisionprima_txt.Text.Trim() == "")
                    comisionprima_txt.Text = "0";
                user.comisionprima = comisionprima_txt.Text;
                user.codigoproductor = codProducto_txt.Text;    
                user.codorganizador = organizador_txt.Text.Trim();
                user.adminempresa = Convert.ToInt16(checkBox2.Checked).ToString();
                if (user.save())
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show("No puede guardar los datos por lo siguiente : " + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void frmNuevoUsuario_Load(object sender, EventArgs e)
        {
            

            Configmaster conf = new Configmaster();
            conf.user_edit = MDIParent1.sesionUser;
            if (conf.user_allow())
            {
                button2.Visible = true;
            }

            this.llenarCombo();
            if(perfilCombo != "")
            {
                comboPerfil.Text = perfilCombo;
            }

            if(idSeleccionado != "")
            {
                txtLoggin.Enabled = false;
            }

            this.set_commission();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Accesos extendidos de usuario : \n\n1. Poder anular una propuesta así haya pasado el cierre" +
                            "\n\n2. Recibir dinero en el cierre cuando quien cierra no es quien se queda con el dinero" +
                            "\n\n3. Poder eliminar los datos de un archivo que haya remitido sancor "+
                            "\n\n4. Poder liquidar comisiones"+
                            "\n\n5. Poder migrar datos a la nube"+
                            "\n\n6. Poder generar certificado libre deuda",
            "Accesos Extendidos", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmOrganizadores frm = new frmOrganizadores();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El usuario administrador del sitio puede ser uno o varios, estos serán los responsables de migrar información de perfiles y usuarios de la empresa",
            "Usuario Admin Sitio", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void new_commission_btn_Click(object sender, EventArgs e)
        {
            frmNewCommission frm = new frmNewCommission();
            frm.user_txt.Text = txtLoggin.Text;
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                this.set_commission();
            }

        }

        private void set_commission()
        {
            if (txtLoggin.Text != "")
            {
                commission_user cus = new commission_user();
                cus = cus.get(txtLoggin.Text);
                if (cus.comm_premio.ToString() != "")
                    comisionpremio_txt.Text = cus.comm_premio.ToString();
                if (cus.comm_prima.ToString() != "")
                    comisionprima_txt.Text = cus.comm_prima.ToString();
            }
        }
    }
}
