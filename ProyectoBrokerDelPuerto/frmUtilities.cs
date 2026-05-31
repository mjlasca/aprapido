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
    public partial class frmUtilities : Form
    {
        public frmUtilities()
        {
            InitializeComponent();
        }

        private void frmUtilities_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            configuraciones conf = new configuraciones();
            conf.get("url_cuil");
            textBox1.Text = conf.detail;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text.Trim();

            // Validar que no esté vacío
            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("Debe ingresar una URL.");
                return;
            }

            Uri uriResult;
            // Validar formato de URL
            bool esValida = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                            && (uriResult.Scheme == Uri.UriSchemeHttp
                                || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!esValida)
            {
                MessageBox.Show("La URL ingresada no es válida.");
                return;
            }

            configuraciones conf = new configuraciones();
            conf.detail = url;
            conf.dato = "url_cuil";
            conf.save();

            MessageBox.Show("Configuración guardada correctamente.");
        }
    }
}
