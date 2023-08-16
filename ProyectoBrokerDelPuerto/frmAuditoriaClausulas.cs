using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ProyectoBrokerDelPuerto
{
    public partial class frmAuditoriaClausulas : Form
    {
        public frmAuditoriaClausulas()
        {
            InitializeComponent();
        }

        private void frmAuditoriaClausulas_Load(object sender, EventArgs e)
        {
            usuarios usus = new usuarios();
            DataSet dsu = usus.get_all();
            empleado_txt.Items.Add("Todos");
            for (int i = 0; i < dsu.Tables[0].Rows.Count; i++)
            {
                empleado_txt.Items.Add(dsu.Tables[0].Rows[i]["nombre"].ToString());
            }

            
        }

        public void verBarrios( string json_barrios_, ListBox list)
        {

            JsonTextReader reader = new JsonTextReader(new StringReader(@json_barrios_));
            JObject obj = JObject.Load(reader);
            if (obj["barrios"] != null)
            {
             List<barrios_propuesta>  json_barrios_propuesta = (from dynamic val in obj["barrios"].AsEnumerable().ToList()
                                          select new barrios_propuesta()
                                          {
                                              id_barrio = val["id_barrio"],
                                              nombre = val["nombre"],
                                              sumamuerte = val["sumamuerte"],
                                              sumagm = val["sumagm"],
                                          }).ToList();

                foreach (barrios_propuesta bp in json_barrios_propuesta)
                {
                    list.Items.Add(bp.nombre);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuditoriaBarrios aud = new AuditoriaBarrios();

            

            if (prefijo_txt.Text != "")
                aud.prefijo = prefijo_txt.Text;
            if (idpropuesta_txt.Text != "")
                aud.idpropuesta = idpropuesta_txt.Text;
            if (empleado_txt.Text != "")
                aud.user_edit = empleado_txt.Text;

            if (empleado_txt.Text == "Todos")
                aud.user_edit = "";

            DataSet ds = aud.get_all();
            dataGridView1.Rows.Clear();
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dataGridView1.Rows.Add(
                    ds.Tables[0].Rows[i]["ultmod"].ToString(),
                    ds.Tables[0].Rows[i]["prefijo"].ToString(),
                    ds.Tables[0].Rows[i]["idpropuesta"].ToString(),
                    ds.Tables[0].Rows[i]["user_edit"].ToString(),
                    ds.Tables[0].Rows[i]["barrios_antes"].ToString(),
                    ds.Tables[0].Rows[i]["barrios_despues"].ToString()
                );
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow.Cells["prefijo"] != null && dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString() != "")
            {
                this.verBarrios(dataGridView1.CurrentRow.Cells["barrios_antes"].Value.ToString(), listBox1);
                this.verBarrios(dataGridView1.CurrentRow.Cells["barrios_despues"].Value.ToString(), listBox2);
            }else
            {
                MessageBox.Show("Seleccione un registro en la grilla");
            }
        }
    }
}
