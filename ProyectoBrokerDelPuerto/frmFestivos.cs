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
    public partial class frmFestivos : Form
    {
        int[] access;
        public frmFestivos()
        {
            InitializeComponent();
        }

        private void frmFestivos_Load(object sender, EventArgs e)
        {
            usuarios usuallow = new usuarios();
            usuallow.loggin = MDIParent1.sesionUser;
            this.access = usuallow.accessperfil("festivos");

            button1.Visible = Convert.ToBoolean(this.access[1]);
            button2.Visible = Convert.ToBoolean(this.access[2]);
            this.cargarDatos();
        }

        private void cargarDatos()
        {
            festivos fes = new festivos();
            DataSet ds = fes.get_all();

            dataGridView1.Rows.Clear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dataGridView1.Rows.Add(
                    ds.Tables[0].Rows[i]["fecha"].ToString(),
                    ds.Tables[0].Rows[i]["useredit"].ToString(),
                    ds.Tables[0].Rows[i]["ultmod"].ToString()
                );
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            festivos fes = new festivos();
            fes.fecha = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            fes.useredit = MDIParent1.sesionUser;
            fes.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (fes.save())
            {
                this.cargarDatos();
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.CurrentRow.Cells["fecha"].ToString() != "")
            {
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            festivos fes = new festivos();
            fes.fecha = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["fecha"].Value).ToString("yyyy-MM-dd");
            fes.delete();
            this.cargarDatos();
        }
    }
}
