using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBrokerDelPuerto.cobranzas
{
    public partial class frmLogAseguradora : Form
    {
        public frmLogAseguradora()
        {
            InitializeComponent();
        }

        private void frmLogAseguradora_Load(object sender, EventArgs e)
        {
            this.inicialdata();
        }

        public void inicialdata()
        {
            aseguradoras ase = new aseguradoras();
            DataSet ds = ase.get_all();
            cbAseguradora.Items.Clear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cbAseguradora.Items.Add(
                    ds.Tables[0].Rows[i]["id"].ToString() + " | " +
                    ds.Tables[0].Rows[i]["razonsocial"].ToString()
                );
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(cbAseguradora.Text != "")
            {
                this.search_aseguradora(cbAseguradora.Text.Substring(0, cbAseguradora.Text.IndexOf("|")).Trim());
            }
            
        }

        public void search_aseguradora(string idaseguradora)
        {
            logaseguradora logase = new logaseguradora();
            DataSet ds = logase.get_aseguradora(idaseguradora, nombreSearch_txt.Text);
            dataGridView1.Rows.Clear();
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dataGridView1.Rows.Add(
                    ds.Tables[0].Rows[i]["ultmod"].ToString(),
                    ds.Tables[0].Rows[i]["nombre"].ToString(),
                    ds.Tables[0].Rows[i]["mensaje"].ToString()
                );
            }
        }

        private void btnSeeDetails_Click(object sender, EventArgs e)
        {
            
            if(dataGridView1.Rows.Count > 0)
            {
                this.Width = 977;
                webBrowser1.Navigate("about:blank");
                webBrowser1.Document.OpenNew(false);
                webBrowser1.Document.Write(dataGridView1.CurrentRow.Cells["mensaje"].Value.ToString());
                webBrowser1.Refresh();
            }
        }

        private void frmLogAseguradora_Resize(object sender, EventArgs e)
        {
            webBrowser1.Width =  Convert.ToInt16( ( this.Width *  0.48 ));
            webBrowser1.Height = Convert.ToInt16((this.Height * 0.88));

        }
    }
}
