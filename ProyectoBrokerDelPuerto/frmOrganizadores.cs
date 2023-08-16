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
    public partial class frmOrganizadores : Form
    {
        public frmOrganizadores()
        {
            InitializeComponent();
        }

        private void frmOrganizadores_Load(object sender, EventArgs e)
        {
            
            usuarios usu = new usuarios();
            DataSet ds = usu.get_all_allow();
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[i]["loggin"].ToString());
            }


            this.datosiniciales();
        }

        private void datosiniciales()
        {
            Configmaster conf = new Configmaster();
            DataSet ds = conf.get("ASEGURADORA");
            if (ds.Tables[0].Rows.Count > 0)
            {
                aseguradora_txt.Text = ds.Tables[0].Rows[0]["nomcodigo"].ToString();
                txtCodEmpresa.Text = ds.Tables[0].Rows[0]["codempresa"].ToString();
            }
                    

            ds = conf.get("MASTER");
            if (ds.Tables[0].Rows.Count > 0)
            {
                master_txt.Text = ds.Tables[0].Rows[0]["nomcodigo"].ToString();
                nomMaster_txt.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
                
            }
                

            ds = conf.get("ORGANIZADOR");
            if (ds.Tables[0].Rows.Count > 0)
            {
                organizador_txt.Text = ds.Tables[0].Rows[0]["nomcodigo"].ToString();
                nomOrganizador_txt.Text = ds.Tables[0].Rows[0]["nombre"].ToString();
            }
                

            ds = conf.get("PRODUCTOR");
            if (ds.Tables[0].Rows.Count > 0)
                productor_txt.Text = ds.Tables[0].Rows[0]["nomcodigo"].ToString();
        }


        private bool verificar()
        {
            usuarios user = new usuarios();
            return user.loggin_pass(comboBox1.Text, pass_txt.Text);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (this.verificar())
            {
                Configmaster conf = new Configmaster();
                conf.nomcodigo = master_txt.Text;
                conf.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                conf.user_edit = comboBox1.Text;
                conf.nombre = nomMaster_txt.Text;
                conf.codestado = "1";
                conf.codempresa = txtCodEmpresa.Text;
                conf.save("MASTER");

                conf = new Configmaster();
                conf.nomcodigo = organizador_txt.Text;
                conf.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                conf.user_edit = comboBox1.Text;
                conf.nombre = nomOrganizador_txt.Text;
                conf.codestado = "1";
                conf.codempresa = txtCodEmpresa.Text;
                conf.save("ORGANIZADOR");

                conf = new Configmaster();
                conf.nomcodigo = productor_txt.Text;
                conf.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                conf.user_edit = comboBox1.Text;
                conf.codestado = "1";
                conf.codempresa = txtCodEmpresa.Text;
                conf.save("PRODUCTOR");

                conf = new Configmaster();
                conf.nomcodigo = aseguradora_txt.Text;
                conf.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                conf.user_edit = comboBox1.Text;
                conf.codestado = "1";
                conf.codempresa = txtCodEmpresa.Text;
                conf.save("ASEGURADORA");

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            


        }
    }
}
