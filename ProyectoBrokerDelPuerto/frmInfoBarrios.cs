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
    public partial class frmInfoBarrios : Form
    {
        public frmInfoBarrios()
        {
            InitializeComponent();
        }

        private void frmInfoBarrios_Load(object sender, EventArgs e)
        {
            txtBusqueda.Focus();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if(txtBusqueda.Text.Length > 2)
            {
                this.datosBarrios(txtBusqueda.Text);
            }
            
        }

        private void datosBarrios(string busqueda)
        {
            barrios bar = new barrios();
            DataSet ds = bar.get_all_busqueda(busqueda);

            if (ds.Tables[0].Rows.Count > 0)
            {
                listBox1.Items.Clear();
                listBox1.Visible = true;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    listBox1.Items.Add(ds.Tables[0].Rows[i]["nombre"]);
                }
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label7.BackColor = SystemColors.Control;
            label7.Text = "";
            button1.Enabled = false;
            if(listBox1.SelectedItem != null)
            {
                txtBusqueda.Text = listBox1.SelectedItem.ToString();
                listBox1.Items.Clear();
                listBox1.Visible = false;
                barrios br = new barrios();
                br.id = br.get_id(txtBusqueda.Text);
                DataSet ds = br.get();

                if(ds.Tables[0].Rows.Count > 0)
                {
                    txtCuit.Text = ds.Tables[0].Rows[0]["id"].ToString();
                    txtTelefono.Text = ds.Tables[0].Rows[0]["telefono"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                    txtClaseBarrio.Text = ds.Tables[0].Rows[0]["clase_barrio"].ToString();
                    txtSubBarrio.Text = ds.Tables[0].Rows[0]["sub_barrio"].ToString();

                   /* try
                    {
                        if (ds.Tables[0].Rows[0]["suma_muerte"].ToString().Trim() != "")
                            txtSumaMuerte.Text = string.Format("{0:c}", Convert.ToDouble(ds.Tables[0].Rows[0]["suma_muerte"].ToString()));
                    }
                    catch
                    {*/
                        txtSumaMuerte.Text = ds.Tables[0].Rows[0]["suma_muerte"].ToString();
                    //}

                    /*try
                    {
                        if (ds.Tables[0].Rows[0]["suma_gm"].ToString().Trim() != "")
                            txtSumaGm.Text = string.Format("{0:c}", Convert.ToDouble(ds.Tables[0].Rows[0]["suma_gm"].ToString()));
                    }
                    catch
                    {*/
                        txtSumaGm.Text = ds.Tables[0].Rows[0]["suma_gm"].ToString();
                    //}

                    /*try
                    {
                        if (ds.Tables[0].Rows[0]["suma_rc"].ToString().Trim() != "")
                            txtSumaRc.Text = string.Format("{0:c}", Convert.ToDouble(ds.Tables[0].Rows[0]["suma_rc"].ToString().Trim()));
                    }
                    catch
                    {**/
                        txtSumaRc.Text = ds.Tables[0].Rows[0]["suma_rc"].ToString().Trim();
                    //}

                    /*try
                    {
                        if (ds.Tables[0].Rows[0]["exige"].ToString().Trim() != "")
                            txtExige.Text = string.Format("{0:c}", Convert.ToDouble(ds.Tables[0].Rows[0]["exige"].ToString()));
                    }
                    catch
                    {*/
                        txtExige.Text = ds.Tables[0].Rows[0]["exige"].ToString();
                    //}


                    txtObservaciones.Text = ds.Tables[0].Rows[0]["observaciones"].ToString();

                    button1.Enabled = true;
                }

                
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            barrios br = new barrios();
            br.id = txtCuit.Text.Trim();
            br.email = txtEmail.Text.Trim();

            //br.suma_muerte = txtSumaMuerte.Text.Substring(0, txtSumaMuerte.Text.Length - 3).Replace("$","").Replace(".","").Trim();
            br.suma_muerte = txtSumaMuerte.Text;
            br.suma_gm = txtSumaGm.Text;
            br.suma_rc = txtSumaRc.Text;
            br.exige = txtExige.Text;

            if (br.update_nuevapropuesta())
            {
                label7.BackColor = Color.GreenYellow;
                label7.Text = "Se ha modificado el Email correctamente";
            }
        }
    }
}
