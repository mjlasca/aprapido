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
    public partial class frmVistaProvincias : Form
    {

        string registro="";
        public frmVistaProvincias()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.busqueda();
        }

        public void busqueda()
        {
            provincias pro = new provincias();
            DataSet ds = pro.get_all_busqueda(busqueda_txt.Text);
            dataGridView1.Rows.Clear();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(
                            ds.Tables[0].Rows[i]["reg"].ToString(),
                            ds.Tables[0].Rows[i]["codpostal"].ToString(),
                            ds.Tables[0].Rows[i]["provincia"].ToString(),
                            ds.Tables[0].Rows[i]["ciudad"].ToString()
                        );
                    }
                }
            }
        }

        private void agregar_btn_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            registro = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            registro = "";
            panel1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(provincia_txt.Text != "" && ciudad_txt.Text != "" && codpostal_txt.Text != "")
            {
                provincias pro = new provincias();
                if (registro != "")
                    pro.reg = registro;
                pro.codpostal = codpostal_txt.Text;
                pro.provincia = provincia_txt.Text;
                pro.ciudad = ciudad_txt.Text;
                pro.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                pro.user_edit = MDIParent1.sesionUser;
                pro.codestado = "1";

                if (pro.save()) {
                    busqueda_txt.Text = pro.codpostal;
                    this.busqueda();
                    panel1.Visible = false;
                }

            }
        }

        private void modificar_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells["reg"].Value != null)
                {
                    registro = dataGridView1.CurrentRow.Cells["reg"].Value.ToString();
                    codpostal_txt.Text = dataGridView1.CurrentRow.Cells["codpostal"].Value.ToString();
                    provincia_txt.Text = dataGridView1.CurrentRow.Cells["provincia"].Value.ToString();
                    ciudad_txt.Text = dataGridView1.CurrentRow.Cells["ciudad"].Value.ToString();
                    panel1.Visible = true;
                }
            }catch(Exception ex)
            {
                Console.Write("No hay datos");
            }
        
            
        }

        private void eliminar_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells["reg"].Value != null)
                {
                    provincias pro = new provincias();
                    pro.reg = dataGridView1.CurrentRow.Cells["reg"].Value.ToString();
                    pro.delete();
                    busqueda();
                }
            }
            catch (Exception ex)
            {
                Console.Write("No hay datos");
            }
        }

        private void frmVistaProvincias_Load(object sender, EventArgs e)
        {
            provincias pro = new provincias();
            pro.corrcaracter();

        }
    }
}
