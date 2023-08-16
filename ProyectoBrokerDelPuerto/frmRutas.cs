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
    public partial class frmRutas : Form
    {
        public frmRutas()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            rutasarchivos ruta = new rutasarchivos();
            ruta.rutacertificados = txtRutaCertificados.Text;
            ruta.rutarecibos = txtRutaRecibos.Text;
            ruta.rutainformes = txtRutaInformes.Text;
            ruta.ipestacion = ruta.GetLocalIPAddress();
            ruta.codestado = "1";
            ruta.user_edit = MDIParent1.sesionUser;
            ruta.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (ruta.save())
                MessageBox.Show("Registro de rutas guardado con éxito");
        }

        private void frmRutas_Load(object sender, EventArgs e)
        {
            rutasarchivos ruta = new rutasarchivos();
            DataSet ds = ruta.get(ruta.GetLocalIPAddress());
            txtRutaCertificados.Text = ds.Tables[0].Rows[0]["rutacertificados"].ToString();
            txtRutaRecibos.Text = ds.Tables[0].Rows[0]["rutarecibos"].ToString();
            txtRutaInformes.Text = ds.Tables[0].Rows[0]["rutainformes"].ToString();
        }
    }
}
