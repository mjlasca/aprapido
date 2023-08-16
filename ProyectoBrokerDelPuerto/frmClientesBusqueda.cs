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
    public partial class frmClientesBusqueda : Form
    {
        string cuit = "";
        string nombarrio = "";
        string summuerte = "";
        string sumgm = "";
        string email_ = "";
        string nomGrupo = "";
        public bool mostrarBoton = true;
        public frmClientesBusqueda()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
            this.encontrarBarrio();
        }

        private void encontrarBarrio()
        {
            
            if (textBox1.Text != "")
            {
                barrios barr = new barrios();
                DataSet ds = barr.busqueda_barrios(textBox1.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    listBarrios.Items.Clear();
                    for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        cuit = ds.Tables[0].Rows[i]["id"].ToString();
                        nombarrio = ds.Tables[0].Rows[i]["nombre"].ToString();
                        textBox2.Text = cuit + "/ " + nombarrio;
                        listBarrios.Items.Add(cuit + "/ " + nombarrio);
                    }
                    
                }
                else
                {
                    textBox2.Text = "No hay resultado";
                    textBox2.ForeColor = Color.Red;
                }
                    
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!this.acutalizarCorreos())
            {
                MessageBox.Show("Correo electrónico mal escrito");
            }
            else
            {
                this.seleccionarBarrios();
            }
            
        }

        private void seleccionarBarrios()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        private bool revisionid(bool op, string data)
        {
            
            if(listBarrios.SelectedItems.Count < 1 && op)
            {
                return true;
            }
            for(int i= 0; i<dataGridView1.Rows.Count; i++)
            {
                if(dataGridView1.Rows[i].Cells["id"].Value.ToString() == data)
                {
                    return true;
                }
            }
            
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (!this.revisionid(true,cuit))
            {
                dataGridView1.Rows.Add(cuit, nombarrio,summuerte,sumgm, email_);
            }
            
        }

        private void listBarrios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int indexString = listBarrios.SelectedItem.ToString().IndexOf("/");
                cuit = listBarrios.SelectedItem.ToString().Substring(0, indexString);
                nombarrio = listBarrios.SelectedItem.ToString().Substring(indexString + 2);
                barrios bar = new barrios();
                bar.id = cuit;
                DataSet ds = bar.get();
                summuerte = ds.Tables[0].Rows[0]["suma_muerte"].ToString();
                sumgm = ds.Tables[0].Rows[0]["suma_gm"].ToString();
                email_ = ds.Tables[0].Rows[0]["email"].ToString();
            }
            catch
            {
                //
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!this.acutalizarCorreos())
            {
                MessageBox.Show("Correo electrónico mal escrito");
            }
            else
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    if (button3.Text == "Actualizar Grupo")
                    {
                        this.guardarBarrios();
                    }
                    else
                    {
                        panelGrupo.Visible = true;
                        panel1.Visible = true;
                    }

                }
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.guardarBarrios();
            this.seleccionarBarrios();
        }


        private void guardarBarrios()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (button3.Text == "Actualizar Grupo")
                {
                    gruposbarrios grup = new gruposbarrios();
                    grup.nombre = this.nomGrupo;
                    grup.get();
                    grup.delete();
                    txtNombreGrupo.Text = this.nomGrupo;
                }
           

                gruposbarrios gr = new gruposbarrios();

                if (!gr.exist(txtNombreGrupo.Text))
                {

                    int consecutivo = gr.id_consecutivo();

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        gruposbarrios grupo = new gruposbarrios();
                        grupo.id = consecutivo.ToString();
                        grupo.nombre = txtNombreGrupo.Text;
                        grupo.idbarrio = dataGridView1.Rows[i].Cells["id"].Value.ToString();
                        grupo.nombrebarrio = dataGridView1.Rows[i].Cells["nombre"].Value.ToString();
                        grupo.save();
                        
                    }

                    
                    txtNombreGrupo.Text = "";
                }
                else
                {
                    MessageBox.Show("El grupo con ese nombre ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!this.acutalizarCorreos())
            {
                MessageBox.Show("Correo electrónico mal escrito");
            }
            button3.Text = "Crear grupo de barrios";
            frmGruposBarrios frm = new frmGruposBarrios();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                this.nomGrupo = frm.comboGrupo.Text;
                

                if (!frm.checkBox1.Checked){
                    dataGridView1.Rows.Clear();
                    button3.Text = "Actualizar Grupo";
                }
                    

                for (int i = 0; i < frm.dataGridView1.Rows.Count; i++)
                {
                    if (!this.revisionid(false, frm.dataGridView1.Rows[i].Cells["id"].Value.ToString()))
                    {
                        barrios bar = new barrios();
                        bar.id = frm.dataGridView1.Rows[i].Cells["id"].Value.ToString().Trim();
                        DataSet ds = bar.get();
                        if(ds.Tables[0].Rows.Count>0)
                            dataGridView1.Rows.Add(frm.dataGridView1.Rows[i].Cells["id"].Value, frm.dataGridView1.Rows[i].Cells["nombre"].Value, ds.Tables[0].Rows[0]["suma_muerte"].ToString(), ds.Tables[0].Rows[0]["suma_gm"].ToString(), ds.Tables[0].Rows[0]["email"].ToString());
                        else
                            dataGridView1.Rows.Add(frm.dataGridView1.Rows[i].Cells["id"].Value, frm.dataGridView1.Rows[i].Cells["nombre"].Value);
                    }
                    
                }

                
            }
        }

        private void frmClientesBusqueda_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panelGrupo.Visible = false;
            panel1.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmNuevoBarrio frm = new frmNuevoBarrio();
            frm.ShowDialog();
            if(frm.DialogResult == DialogResult.OK)
            {
                textBox1.Text = frm.cuitBarr;
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dataGridView1.CurrentRow.Index <= dataGridView1.Rows.Count)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        public bool acutalizarCorreos()
        {

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["id"].Value != null && dataGridView1.Rows[i].Cells["email"].Value != null)
                {
                    if(dataGridView1.Rows[i].Cells["email"].Value.ToString().Trim() != "")
                    {
                        validaciones val = new validaciones();
                        if( !val.correo(dataGridView1.Rows[i].Cells["email"].Value.ToString()) )
                        {
                            dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells["email"];
                            return false;
                        }
                        barrios br = new barrios();
                        br.id = dataGridView1.Rows[i].Cells["id"].Value.ToString();
                        br.email = dataGridView1.Rows[i].Cells["email"].Value.ToString();
                        br.update_nuevapropuesta();
                    }
                }
                
            }

            return true;
        }

        private void frmClientesBusqueda_Load(object sender, EventArgs e)
        {
            button2.Visible = mostrarBoton;
        }
    }
}
