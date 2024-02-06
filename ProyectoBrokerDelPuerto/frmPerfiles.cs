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
    public partial class frmPerfiles : Form
    {
        int[] access;
        public frmPerfiles()
        {
            InitializeComponent();
        }


        private void save(string nombrePerfil)
        {
            if(comboBox1.Text != "Nombre" || txtNombre.Text != "")
            {
                if (comboBox1.Text != "Nombre")
                {
                    perfiles per = new perfiles();
                    per.nombre = comboBox1.Text;
                    per.delete();
                }
                    
                    

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    perfiles per = new perfiles();

                    if (comboBox1.Text == "Nombre")
                        per.nombre = nombrePerfil;
                    else
                        per.nombre = comboBox1.Text;

                    per.modulo = dataGridView1.Rows[i].Cells["modulo"].Value.ToString();
                    per.vista = Convert.ToInt16(dataGridView1.Rows[i].Cells["vista"].Value).ToString();
                    per.edicion = Convert.ToInt16(dataGridView1.Rows[i].Cells["edicion"].Value).ToString();
                    per.eliminar = Convert.ToInt16(dataGridView1.Rows[i].Cells["eliminar"].Value).ToString();
                    per.exportar = Convert.ToInt16(dataGridView1.Rows[i].Cells["exportar"].Value).ToString();
                    per.save();
                }

                this.llenarCombo();

                comboBox1.Text = nombrePerfil;

                this.llenarchecks(checkBox1, "vista", true);
                this.llenarchecks(checkBox2, "edicion", true);
                this.llenarchecks(checkBox3, "eliminar", true);
                this.llenarchecks(checkBox4, "exportar", true);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text != "Nombre" || ( comboBox1.Text != "" && comboBox1.Text == "Nombre" )) {
                this.save(txtNombre.Text);
            }
            else
            {
                MessageBox.Show("No hay nombre o perfil seleccionado");
            }
            
            
        }

        public void limpiar_check()
        {
         
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void chOperaciones_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void llenarCombo()
        {
            perfiles per = new perfiles();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Nombre");
            DataSet ds = per.get_all();
            if(ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    for (int i=0; i < ds.Tables[0].Rows.Count; i++ )
                    {
                        if(ds.Tables[0].Rows[i]["nombre"].ToString() != "")
                            comboBox1.Items.Add(ds.Tables[0].Rows[i]["nombre"].ToString());
                    }
                }
            }
            comboBox1.SelectedIndex = 0;
            
            
        }

        private void chClientes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmPerfiles_Load(object sender, EventArgs e)
        {
            perfiles perf = new perfiles();
            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("perfiles");

            button1.Visible = Convert.ToBoolean(this.access[1]);
            btnEliminar.Visible = Convert.ToBoolean(this.access[2]);


            this.datosModulos();
            this.llenarCombo();
            btnEliminar.Enabled = false;
            button1.Text = "Crear Perfil";
        }


        private void datosModulos()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add("usuarios", "Usuarios");
            dataGridView1.Rows.Add("perfiles", "Perfiles");
            dataGridView1.Rows.Add("actividades", "Actividades");
            dataGridView1.Rows.Add("clasificaciones", "Clasificaciones");
            dataGridView1.Rows.Add("festivos", "Festivos");
            dataGridView1.Rows.Add("coberturas", "Coberturas");
            dataGridView1.Rows.Add("confcorreo", "Configuración de correo");
            dataGridView1.Rows.Add("migraciones", "Migraciones");
            dataGridView1.Rows.Add("puntodeventa", "Parámetros Punto de venta");
            dataGridView1.Rows.Add("clientes", "Clientes");
            dataGridView1.Rows.Add("barrios", "Barrios");
            dataGridView1.Rows.Add("propuestas", "Propuestas");
            dataGridView1.Rows.Add("cierrecajas", "Cierrecajas");
            dataGridView1.Rows.Add("rendiciones", "Rendiciones");
            dataGridView1.Rows.Add("cobranzas_envios", "Cobranzas / Envíos");
            dataGridView1.Rows.Add("nuevacomision", "Nueva Comisión");
            dataGridView1.Rows.Add("listacomision", "Listado Comisiones");
            dataGridView1.Rows.Add("auditoriabarrios", "Auditoría cláusulas");
            //dataGridView1.Rows.Add("informes");
            dataGridView1.Rows.Add("informe_findia", "Generar Informe Fin del día/Envió e-mail Aseguradora");
            dataGridView1.Rows.Add("combinar_informes", "Combinar Informes");
            dataGridView1.Rows.Add("mail_findeldia", "Correo Fin del día");
            dataGridView1.Rows.Add("controlventas_nuevo", "Control Ventas Nuevo Archivo");
            dataGridView1.Rows.Add("controlventas_informe", "Control Ventas Análisis de Ventas/comparativo");
            dataGridView1.Rows.Add("ventascredito", "Ventas Crédito");
            dataGridView1.Rows.Add("imputaciones", "Imputaciones");


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.datosModulos();
            button1.Text = "Crear Perfil";
            btnEliminar.Enabled = false;
            if (comboBox1.Text != "" && comboBox1.Text != "Nombre")
            {
                this.seleccionaPerfil(comboBox1.Text);
                txtNombre.Text = "";
                btnEliminar.Enabled = true;
                button1.Text = "Guardar";
            }
        }

        private void seleccionaPerfil(string nombre)
        {
            perfiles per = new perfiles();
            per.nombre = nombre;
            DataSet ds = per.get();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    
                    for(int k = 0; k < dataGridView1.Rows.Count; k++)
                    {

                        
                        if (dataGridView1.Rows[k].Cells["modulo"].Value.ToString() == ds.Tables[0].Rows[i]["modulo"].ToString())
                        {
                            if (ds.Tables[0].Rows[i]["vista"].ToString() == "1")
                                dataGridView1.Rows[k].Cells["vista"].Value = 1;
                            if (ds.Tables[0].Rows[i]["edicion"].ToString() == "1")
                                dataGridView1.Rows[k].Cells["edicion"].Value = 1;
                            if (ds.Tables[0].Rows[i]["eliminar"].ToString() == "1")
                                dataGridView1.Rows[k].Cells["eliminar"].Value = 1;
                            if (ds.Tables[0].Rows[i]["exportar"].ToString() == "1")
                                dataGridView1.Rows[k].Cells["exportar"].Value = 1;
                            if (ds.Tables[0].Rows[i]["access"].ToString() == "1")
                                dataGridView1.Rows[k].Cells["vista"].Value = 1;
                        }

                        if( ds.Tables[0].Rows[i]["modulo"].ToString() == "administrador" && (dataGridView1.Rows[k].Cells["modulo"].Value.ToString() == "actividades" ||
                            dataGridView1.Rows[k].Cells["modulo"].Value.ToString() == "coberturas" || dataGridView1.Rows[k].Cells["modulo"].Value.ToString() == "clasificaciones" ||
                            dataGridView1.Rows[k].Cells["modulo"].Value.ToString() == "usuarios" || dataGridView1.Rows[k].Cells["modulo"].Value.ToString() == "perfiles" ) )
                        {
                            dataGridView1.Rows[k].Cells["vista"].Value = 1;
                        }

                            
                    }
                }
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text != "")
            {
                perfiles perf = new perfiles();
                perf.nombre = comboBox1.Text;
                perf.delete();
                this.datosModulos();
                this.llenarCombo();
                btnEliminar.Enabled = false;
            }
            
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if(txtNombre.Text != "")
            {
                this.datosModulos();
                comboBox1.Text = "Nombre";
                btnEliminar.Enabled = false;
            }
        }

        private void chCajas_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.llenarchecks(checkBox1, "vista");
        }

        private  void llenarchecks(CheckBox ch, string colm, bool clean = false)
        {
            if (ch.Checked)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[colm].Value = 1;
                }
            }
            if(!ch.Checked || clean)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[colm].Value = 0;
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.llenarchecks(checkBox2, "edicion");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.llenarchecks(checkBox3, "eliminar");
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            this.llenarchecks(checkBox4, "exportar");
        }
    }
}
