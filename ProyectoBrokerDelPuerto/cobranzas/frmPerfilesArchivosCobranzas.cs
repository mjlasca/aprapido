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
    public partial class frmPerfilesArchivosCobranzas : Form
    {
        public frmPerfilesArchivosCobranzas()
        {
            InitializeComponent();
        }

        private void frmPerfilesArchivosCobranzas_Load(object sender, EventArgs e)
        {
            this.DatosIniciales();

        }

        private void encabezadocolumnas()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add("CÓDIGO PRODUCTOR");
            dataGridView1.Rows.Add("MATRÍCULA");
            dataGridView1.Rows.Add("NOMBRE");
            dataGridView1.Rows.Add("RAMO");
            dataGridView1.Rows.Add("PÓLIZA");
            dataGridView1.Rows.Add("CERTIFICADO");
            dataGridView1.Rows.Add("CUOTA GARANTIZADA");
            dataGridView1.Rows.Add("SMS");
            dataGridView1.Rows.Add("COLOR COBERTURA");
            dataGridView1.Rows.Add("FECHA VENCIMIENTO");//9
            dataGridView1.Rows.Add("ES PRIMERA CUOTA");
            dataGridView1.Rows.Add("MONEDA");
            dataGridView1.Rows.Add("MONTO");
            dataGridView1.Rows.Add("AGENTE RECAUDADOR");
            dataGridView1.Rows.Add("PAGO EN TRÁMITE");
            dataGridView1.Rows.Add("COBRADOR");
            dataGridView1.Rows.Add("SALDO VENCIDO");
            dataGridView1.Rows.Add("MONTO PAGO EN TRÁMITE");
            dataGridView1.Rows.Add("REFERENCIA");//18
            dataGridView1.Rows.Add("PRODUCTO");
            dataGridView1.Rows.Add("FORMA PAGO");
            dataGridView1.Rows.Add("FECHA EMISIÓN DEL RECIBO");
            dataGridView1.Rows.Add("FECHA INICIO VIGENCIA PÓLIZA");
            dataGridView1.Rows.Add("FECHA FIN VIGENCIA PÓLIZA");
            dataGridView1.Rows.Add("ORGANIZADOR");
            dataGridView1.Rows.Add("RIESGO ASEGURADO");
            dataGridView1.Rows.Add("ENDOSO ");
            dataGridView1.Rows.Add("FACTURA ");
            dataGridView1.Rows.Add("FECHA BAJA");
            dataGridView1.Rows.Add("CUOTA");
            dataGridView1.Rows.Add("DNI/CUIT CLIENTE");
            dataGridView1.Rows.Add("PERIODO ");
            dataGridView1.Rows.Add("E-MAIL");
            dataGridView1.Rows.Add("TELEFONO");
            dataGridView1.Rows.Add("PESO ARGENTINO");
            dataGridView1.Rows.Add("DÓLAR TIPO DATO");


            dataGridView1.Rows[1].Visible = false;
            //dataGridView1.Rows[3].Visible = false;
            dataGridView1.Rows[5].Visible = false;
            dataGridView1.Rows[6].Visible = false;
            dataGridView1.Rows[7].Visible = false;
            dataGridView1.Rows[8].Visible = false;
            dataGridView1.Rows[10].Visible = false;
            dataGridView1.Rows[13].Visible = false;
            dataGridView1.Rows[14].Visible = false;
            dataGridView1.Rows[15].Visible = false;
            dataGridView1.Rows[16].Visible = false;
            dataGridView1.Rows[17].Visible = false;
            dataGridView1.Rows[18].Visible = false;
            dataGridView1.Rows[19].Visible = false;
            //dataGridView1.Rows[20].Visible = false;
            dataGridView1.Rows[21].Visible = false;
            dataGridView1.Rows[22].Visible = false;
            dataGridView1.Rows[23].Visible = false;
            dataGridView1.Rows[24].Visible = false;
            dataGridView1.Rows[25].Visible = false;
            dataGridView1.Rows[26].Visible = false;
            dataGridView1.Rows[27].Visible = false;
            dataGridView1.Rows[28].Visible = false;
            dataGridView1.Rows[31].Visible = false;
            dataGridView1.Rows[34].Visible = false;


        }

        private void DatosIniciales()
        {
            this.encabezadocolumnas();


            aseguradoras ase = new aseguradoras();
            DataSet ds = ase.get_all();
            cbAseguradora.Items.Clear();
            cbAseguradora.Items.Add("Seleccione la aseguradora");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cbAseguradora.Items.Add(
                    ds.Tables[0].Rows[i]["id"].ToString()+ " | " +
                    ds.Tables[0].Rows[i]["razonsocial"].ToString()
                );
            }

            cbAseguradora.Text = "Seleccione la aseguradora";

        }

        public string obtenerid(string texto)
        {
            if (cbAseguradora.Text != "" && cbAseguradora.Text != "Seleccione la aseguradora")
                return texto.Substring(0, cbAseguradora.Text.IndexOf("|")).Trim().Trim();
            else
                return "";
        }


        private void guardarperfil()
        {
            if(cbAseguradora.Text != "")
            {
                perfilesaseguradoras per = new perfilesaseguradoras();
                per.filacomienzo = txtFilaComienzo.Text;
                per.idaseguradora = this.obtenerid( cbAseguradora.Text );
                per.codigoproductor = dataGridView1.Rows[0].Cells["columna"].Value == null ? "" : dataGridView1.Rows[0].Cells["columna"].Value.ToString();
                per.matricula = dataGridView1.Rows[1].Cells["columna"].Value == null ? "" : dataGridView1.Rows[1].Cells["columna"].Value.ToString(); 
                per.nombre = dataGridView1.Rows[2].Cells["columna"].Value == null ? "" : dataGridView1.Rows[2].Cells["columna"].Value.ToString(); 
                per.ramo = dataGridView1.Rows[3].Cells["columna"].Value == null ? "" : dataGridView1.Rows[3].Cells["columna"].Value.ToString(); 
                per.poliza = dataGridView1.Rows[4].Cells["columna"].Value == null ? "" : dataGridView1.Rows[4].Cells["columna"].Value.ToString(); 
                per.certificado = dataGridView1.Rows[5].Cells["columna"].Value == null ? "" : dataGridView1.Rows[5].Cells["columna"].Value.ToString(); 
                per.cuotagarantizada = dataGridView1.Rows[6].Cells["columna"].Value == null ? "" : dataGridView1.Rows[6].Cells["columna"].Value.ToString(); 
                per.sms = dataGridView1.Rows[7].Cells["columna"].Value == null ? "" : dataGridView1.Rows[7].Cells["columna"].Value.ToString(); 
                per.colorcobertura = dataGridView1.Rows[8].Cells["columna"].Value == null ? "" : dataGridView1.Rows[8].Cells["columna"].Value.ToString(); 
                per.fechavencimiento = dataGridView1.Rows[9].Cells["columna"].Value == null ? "" : dataGridView1.Rows[9].Cells["columna"].Value.ToString(); 
                per.esprimeracuota = dataGridView1.Rows[10].Cells["columna"].Value == null ? "" : dataGridView1.Rows[10].Cells["columna"].Value.ToString(); 
                per.moneda = dataGridView1.Rows[11].Cells["columna"].Value == null ? "" : dataGridView1.Rows[11].Cells["columna"].Value.ToString(); 
                per.monto = dataGridView1.Rows[12].Cells["columna"].Value == null ? "" : dataGridView1.Rows[12].Cells["columna"].Value.ToString(); 
                per.agenterecaudador = dataGridView1.Rows[13].Cells["columna"].Value == null ? "" : dataGridView1.Rows[13].Cells["columna"].Value.ToString(); 
                per.pagotramite = dataGridView1.Rows[14].Cells["columna"].Value == null ? "" : dataGridView1.Rows[14].Cells["columna"].Value.ToString(); 
                per.cobrador = dataGridView1.Rows[15].Cells["columna"].Value == null ? "" : dataGridView1.Rows[15].Cells["columna"].Value.ToString(); 
                per.saldovencido = dataGridView1.Rows[16].Cells["columna"].Value == null ? "" : dataGridView1.Rows[16].Cells["columna"].Value.ToString(); 
                per.montopagotramite = dataGridView1.Rows[17].Cells["columna"].Value == null ? "" : dataGridView1.Rows[17].Cells["columna"].Value.ToString(); 
                per.referencia = dataGridView1.Rows[18].Cells["columna"].Value == null ? "" : dataGridView1.Rows[18].Cells["columna"].Value.ToString(); 
                per.producto = dataGridView1.Rows[19].Cells["columna"].Value == null ? "" : dataGridView1.Rows[19].Cells["columna"].Value.ToString(); 
                per.formapago = dataGridView1.Rows[20].Cells["columna"].Value == null ? "" : dataGridView1.Rows[20].Cells["columna"].Value.ToString(); 
                per.fechaemisionrecibo = dataGridView1.Rows[21].Cells["columna"].Value == null ? "" : dataGridView1.Rows[21].Cells["columna"].Value.ToString(); 
                per.fechainiciovigenciapoliza = dataGridView1.Rows[22].Cells["columna"].Value == null ? "" : dataGridView1.Rows[22].Cells["columna"].Value.ToString(); 
                per.fechafinvigenciapoliza = dataGridView1.Rows[23].Cells["columna"].Value == null ? "" : dataGridView1.Rows[23].Cells["columna"].Value.ToString(); 
                per.organizador = dataGridView1.Rows[24].Cells["columna"].Value == null ? "" : dataGridView1.Rows[24].Cells["columna"].Value.ToString(); 
                per.riesgoasegurado = dataGridView1.Rows[25].Cells["columna"].Value == null ? "" : dataGridView1.Rows[25].Cells["columna"].Value.ToString(); 
                per.endoso = dataGridView1.Rows[26].Cells["columna"].Value == null ? "" : dataGridView1.Rows[26].Cells["columna"].Value.ToString(); 
                per.factura = dataGridView1.Rows[27].Cells["columna"].Value == null ? "" : dataGridView1.Rows[27].Cells["columna"].Value.ToString(); 
                per.fechabaja = dataGridView1.Rows[28].Cells["columna"].Value == null ? "" : dataGridView1.Rows[28].Cells["columna"].Value.ToString(); 
                per.cuotan = dataGridView1.Rows[29].Cells["columna"].Value == null ? "" : dataGridView1.Rows[29].Cells["columna"].Value.ToString(); 
                per.dnicliente = dataGridView1.Rows[30].Cells["columna"].Value == null ? "" : dataGridView1.Rows[30].Cells["columna"].Value.ToString(); 
                per.periodo = dataGridView1.Rows[31].Cells["columna"].Value == null ? "" : dataGridView1.Rows[31].Cells["columna"].Value.ToString(); 
                per.email= dataGridView1.Rows[32].Cells["columna"].Value == null ? "" : dataGridView1.Rows[32].Cells["columna"].Value.ToString(); 
                per.telefono = dataGridView1.Rows[33].Cells["columna"].Value == null ? "" : dataGridView1.Rows[33].Cells["columna"].Value.ToString(); 
                per.pesoargentino = dataGridView1.Rows[34].Cells["columna"].Value == null ? "" : dataGridView1.Rows[34].Cells["columna"].Value.ToString(); 
                per.dolartipodato = dataGridView1.Rows[35].Cells["columna"].Value == null ? "" : dataGridView1.Rows[35].Cells["columna"].Value.ToString(); 
                per.save();
            }
        }

        private bool validacion()
        {
            string errorvalidacion = "";

            if (txtFilaComienzo.Text == "")
            {
                errorvalidacion += "\nLa fila para comenzar la lectura del archivo es obligatorio, no debe quedar vacío";
            }

            if (cbAseguradora.Text == "Seleccione la aseguradora")
            {
                errorvalidacion += "\nDebe seleccionar una aseguradora";
            }

            if (errorvalidacion != "")
            {
                MessageBox.Show("Error de validación"+errorvalidacion, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


                
                
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.validacion())
            {
                if (MessageBox.Show("¿Está Segur@ de aplicar los cambios?", "Guardar Perfil", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.guardarperfil();
                    MessageBox.Show("Se ha guardado con éxito");
                }
            }
        }

        private void seleccionaraseguradora()
        {
            if(cbAseguradora.Text != "")
            {
                perfilesaseguradoras perfase = new perfilesaseguradoras();
                perfase.idaseguradora = this.obtenerid(cbAseguradora.Text); 
                DataSet ds = perfase.get();

                txtFilaComienzo.Text = "";
                this.encabezadocolumnas();

                if(ds.Tables[0].Rows.Count> 0)
                {
                    
                    txtFilaComienzo.Text = ds.Tables[0].Rows[0]["filacomienzo"].ToString();

                    for (int i = 0; i< dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells["columna"].Value = ds.Tables[0].Rows[0][i+2].ToString();
                    }
                }
            }
        }

        private void cbAseguradora_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.seleccionaraseguradora();
        }

        public void reset_components()
        {
            cbAseguradora.Text = "Seleccione la aseguradora";
            txtFilaComienzo.Text = "";
            this.encabezadocolumnas();
        }

        private void nuevo_perfil_btn_Click(object sender, EventArgs e)
        {
            this.reset_components();
        }
    }
}
