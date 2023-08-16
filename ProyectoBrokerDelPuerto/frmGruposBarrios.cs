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
    public partial class frmGruposBarrios : Form
    {

        public frmGruposBarrios()
        {
            InitializeComponent();
        }

        private void frmGruposBarrios_Load(object sender, EventArgs e)
        {
            this.datosIniciales();
        }

        private void datosIniciales()
        {
            gruposbarrios grup = new gruposbarrios();
            DataSet ds = grup.get_all();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboGrupo.Items.Add(ds.Tables[0].Rows[i]["nombre"].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void comboGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            gruposbarrios gru = new gruposbarrios();
            gru.nombre = comboGrupo.Text;
            DataSet ds = gru.get_all_id();
            dataGridView1.Rows.Clear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dataGridView1.Rows.Add(ds.Tables[0].Rows[i]["idbarrio"].ToString(), ds.Tables[0].Rows[i]["nombrebarrio"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está segur@ de eliminar el grupo?", "Elimar Grupo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                gruposbarrios gr = new gruposbarrios();
                gr.nombre = comboGrupo.Text;
                gr.get();
                gr.delete();
                dataGridView1.Rows.Clear();
                comboGrupo.Items.Clear();
                this.datosIniciales();
            }

        }
    }
}
