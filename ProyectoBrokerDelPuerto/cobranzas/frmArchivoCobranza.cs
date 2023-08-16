using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Xls;
using SpreadsheetLight;

namespace ProyectoBrokerDelPuerto.cobranzas
{
    
    public partial class frmArchivoCobranza : Form
    {
        int[] access;
        string ruta = "";
        List<string> listclientes_mails = new List<string>();
        List<string> listclientes_ = new List<string>();
        int WidhtInit = 0, dataGWidhtInit = 0, dataGHeihtgInit = 0, HeightInit = 0;
        DataTable tableDataExcel = new DataTable();
        DataSet dsperfil;
        List<int> listCheck = new List<int>();
        public frmArchivoCobranza()
        {
            InitializeComponent();
        }

        private void perfilesDeArchivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPerfilesArchivosCobranzas frm = new frmPerfilesArchivosCobranzas();
            frm.Show();
        }

        void alcerrrar(object sender, FormClosedEventArgs e)
        {
            string aux = cbAseguradora.Text;
            this.datosIniciales();
            cbAseguradora.Text = aux;
        }

        private void aseguradoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAseguradoras frm = new frmAseguradoras();
            frm.FormClosed += new FormClosedEventHandler(alcerrrar);
            frm.ShowDialog();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (this.validacion())
            {
                try
                {
                    this.tableDataExcel = new DataTable();
                    await this.traerdatosarchivo();
                    //dataGridView2.Columns[0].Visible = false;
                    //dataGridView2.Columns[1].Visible = false;
                    this.ocultaCols();
                    dataGridView2.Columns[0].Width = 30;
                    this.agregarmails(dataGridView2);
                    MessageBox.Show("Se ha cargado el archivo con éxito");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Algo no está bien "+ex);
                }
                
            }
        }

        private void ocultaCols()
        {
            int[] arrin =  { 0, 1, 3, 4, 5,  10, 12, 13, 21,30,31,33 };
            for(int i = 0; i< dataGridView2.Columns.Count; i++)
            {
                if (Array.IndexOf(arrin, i) < 0)
                {
                    dataGridView2.Columns[i].Visible = false;
                }
                
            }
        }

        private void agregarmails(DataGridView dgv)
        {
            clientes_cobranzas cli = new clientes_cobranzas();
            string mail_ = "";
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if(dgv.Rows[i].Cells["Email"].Value != null)
                {
                    if(cbCoincidencia.Text == "Nombre")
                        mail_ = cli.get_mail_cliente(dgv.Rows[i].Cells["Nombre"].Value.ToString(), cbCoincidencia.Text);
                    if (cbCoincidencia.Text == "DNI")
                        mail_ = cli.get_mail_cliente(dgv.Rows[i].Cells["DNI_Cliente"].Value.ToString(), cbCoincidencia.Text);
                    if (cbCoincidencia.Text == "Email")
                        mail_ = cli.get_mail_cliente(dgv.Rows[i].Cells["Email"].Value.ToString(), cbCoincidencia.Text);
                    if (mail_ != "")
                        dgv.Rows[i].Cells["Email"].Value = mail_.Trim();
                }

                if(dgv.Rows[i].Cells["Email"].Value == null)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                    dgv.Rows[i].Cells[0].Value = 0;
                }
                    
                if (dgv.Rows[i].Cells["Email"].Value != null)
                {
                    if (dgv.Rows[i].Cells["Email"].Value.ToString().Trim() == "")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        dgv.Rows[i].Cells[0].Value = 0;
                    }
                        
                }
            }

            if(dgv.Rows.Count > 0)
            {
                for(int i = 1; i < dgv.Columns.Count; i++)
                    dgv.Columns[i].ReadOnly = true;
            }

            
        }

        public void convertxlstoxlsx(string path)
        {
            Workbook wb = new Workbook();
            wb.LoadFromFile(path);
            wb.SaveToFile(path+"x");
        }

        public void mostrar_busqueda(bool est = true)
        {
            if (dataGridView2.Rows.Count > 0 &&  Convert.ToBoolean( access[1] ) )
            {
                label2.Visible = est;
                label3.Visible = est;
                buscar_dato_txt.Visible = est;
                columna_busqueda_cb.Visible = est;
                button3.Visible = est;
                button2.Visible = est;
                button4.Visible = est;
                enviar_btn.Visible = est;
                actualiza_btn.Visible = false;
                chEnvio.Checked = est;
                btnCrearModificar.Visible = est;
                button5.Visible = est;
            }
        }

        private async Task<bool> traerdatosarchivo()
        {
            bool resb = false;

            if (this.ruta != "" && cbAseguradora.Text != "") { 

                perfilesaseguradoras perf = new perfilesaseguradoras();
                perf.idaseguradora = cbAseguradora.Text.Substring(0, cbAseguradora.Text.IndexOf("|")).Trim().Trim();
                dsperfil = perf.get();
                if (dsperfil.Tables[0].Rows.Count > 0)
                {

                    int rowStar = Convert.ToInt16(dsperfil.Tables[0].Rows[0]["filacomienzo"].ToString());
                    excelDocuments excel = new excelDocuments();
                    dataGridView1.Rows.Clear();
                    
                    pictureBox3.Visible = true;
                    await Task.Run(() => {
                        this.llenarGridDatosImportados(excel.importarDatosPerfilAseguradora(this.ruta), rowStar, dsperfil);
                    });
                    
                    pictureBox3.Visible = false;

                    this.llenarDataDb();
                    this.mostrar_busqueda();

                    resb = true;
                }
                else
                {
                    MessageBox.Show("No se ha creado el perfil del archivo de la aseguradora",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    resb = false;
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado el archivo y la aseguradora, por favor hágalo dando clic en el ícono de Excel",
                "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resb = false;
            }

            return resb;

        }

        private async void llenarDataDb()
        {
            DataTable dttt = this.asignar_base_table().Tables[0];
            this.tableDataExcel.Rows.Clear();

            for (int i = 0; i < dttt.Rows.Count; i++)
            {
                this.tableDataExcel.Rows.Add(
                 this.listCheck.Count > 0 ? Convert.ToInt16(this.listCheck[i]) : 1,
                 dttt.Rows[i]["codigoproductor"].ToString(),
                 dttt.Rows[i]["matricula"].ToString(),
                 dttt.Rows[i]["nombre"].ToString(),
                 dttt.Rows[i]["ramo"].ToString(),
                 dttt.Rows[i]["poliza"].ToString(),
                 dttt.Rows[i]["certificado"].ToString(),
                 dttt.Rows[i]["cuotagarantizada"].ToString(),
                 dttt.Rows[i]["sms"].ToString(),
                 dttt.Rows[i]["colorcobertura"].ToString(),
                 dttt.Rows[i]["fechavencimiento"].ToString(),
                 dttt.Rows[i]["esprimeracuota"].ToString(),
                 dttt.Rows[i]["moneda"].ToString(),
                 dttt.Rows[i]["total_monto"].ToString(),
                 dttt.Rows[i]["agenterecaudador"].ToString(),//14
                 dttt.Rows[i]["pagotramite"].ToString(),
                 dttt.Rows[i]["cobrador"].ToString(),
                 dttt.Rows[i]["saldovencido"].ToString(),
                 dttt.Rows[i]["montopagotramite"].ToString(),
                 dttt.Rows[i]["referencia"].ToString(),
                 dttt.Rows[i]["producto"].ToString(),
                 dttt.Rows[i]["formapago"].ToString(),
                 dttt.Rows[i]["fechaemisionrecibo"].ToString(),
                 dttt.Rows[i]["fechainiciovigenciapoliza"].ToString(),
                 dttt.Rows[i]["fechafinvigenciapoliza"].ToString(),
                 dttt.Rows[i]["organizador"].ToString(),
                 dttt.Rows[i]["riesgoasegurado"].ToString(),
                 dttt.Rows[i]["endoso"].ToString(),
                 dttt.Rows[i]["factura"].ToString(),
                 dttt.Rows[i]["fechabaja"].ToString(),
                 dttt.Rows[i]["cuotan"].ToString(),
                 dttt.Rows[i]["dnicliente"].ToString(),
                 dttt.Rows[i]["periodo"].ToString(),
                 dttt.Rows[i]["email"].ToString(),
                 dttt.Rows[i]["telefono"].ToString());
            }

            dataGridView2.DataSource = this.tableDataExcel;
        }

        private DataSet asignar_base_table()
        {
            DataSet ds = new DataSet();

            datauploadtemp dtemp = new datauploadtemp();
            dtemp.delete(cbAseguradora.Text.Substring(0, cbAseguradora.Text.IndexOf("|")).Trim());
            //List<datauploadtemp> collrows = new List<datauploadtemp>();
            clientes_cobranzas clicob = new clientes_cobranzas();
            for (int i = 0; i < this.tableDataExcel.Rows.Count; i++)
            {
                dtemp = new datauploadtemp();

                dtemp.idaseguradora = cbAseguradora.Text.Substring(0, cbAseguradora.Text.IndexOf("|")).Trim();
                dtemp.codigoproductor = this.tableDataExcel.Rows[i][1].ToString();
                dtemp.matricula = this.tableDataExcel.Rows[i][2].ToString();
                dtemp.nombre = this.tableDataExcel.Rows[i][3].ToString(); 
                dtemp.ramo = this.tableDataExcel.Rows[i][4].ToString(); 
                dtemp.poliza = this.tableDataExcel.Rows[i][5].ToString(); 
                dtemp.certificado = this.tableDataExcel.Rows[i][6].ToString(); 
                dtemp.cuotagarantizada = this.tableDataExcel.Rows[i][7].ToString(); 
                dtemp.sms = this.tableDataExcel.Rows[i][8].ToString(); 
                dtemp.colorcobertura = this.tableDataExcel.Rows[i][9].ToString(); 
                dtemp.fechavencimiento = this.tableDataExcel.Rows[i][10].ToString();
                dtemp.esprimeracuota = this.tableDataExcel.Rows[i][11].ToString();
                dtemp.moneda = this.tableDataExcel.Rows[i][12].ToString();
                dtemp.monto = this.tableDataExcel.Rows[i][13].ToString() != "" ? this.tableDataExcel.Rows[i][13].ToString() : "0";
                dtemp.agenterecaudador = this.tableDataExcel.Rows[i][14].ToString();
                dtemp.pagotramite = this.tableDataExcel.Rows[i][15].ToString();
                dtemp.cobrador = this.tableDataExcel.Rows[i][16].ToString();
                dtemp.saldovencido = this.tableDataExcel.Rows[i][17].ToString();
                dtemp.montopagotramite = this.tableDataExcel.Rows[i][18].ToString();
                dtemp.referencia = this.tableDataExcel.Rows[i][19].ToString();
                dtemp.producto = this.tableDataExcel.Rows[i][20].ToString();
                dtemp.formapago = this.tableDataExcel.Rows[i][21].ToString();
                dtemp.fechaemisionrecibo = this.tableDataExcel.Rows[i][22].ToString();
                dtemp.fechainiciovigenciapoliza = this.tableDataExcel.Rows[i][23].ToString();
                dtemp.fechafinvigenciapoliza = this.tableDataExcel.Rows[i][24].ToString();
                dtemp.organizador = this.tableDataExcel.Rows[i][25].ToString();
                dtemp.riesgoasegurado = this.tableDataExcel.Rows[i][26].ToString();
                dtemp.endoso = this.tableDataExcel.Rows[i][27].ToString();
                dtemp.factura = this.tableDataExcel.Rows[i][28].ToString();
                dtemp.fechabaja = this.tableDataExcel.Rows[i][29].ToString();
                dtemp.cuotan = this.tableDataExcel.Rows[i][30].ToString();
                dtemp.dnicliente = this.tableDataExcel.Rows[i][31].ToString();
                dtemp.periodo = this.tableDataExcel.Rows[i][32].ToString();
                dtemp.email = this.tableDataExcel.Rows[i][33].ToString() != "" ? this.tableDataExcel.Rows[i][33].ToString() : clicob.get_mail_cliente(cbCoincidencia.Text == "DNI" ? dtemp.dnicliente : cbCoincidencia.Text == "Email" ? dtemp.email : dtemp.nombre, cbCoincidencia.Text);
                dtemp.telefono = this.tableDataExcel.Rows[i][34].ToString();
                //dtemp.pesoargentino = this.tableDataExcel.Rows[i][35].ToString();
                //dtemp.dolartipodato = this.tableDataExcel.Rows[i][36].ToString();

                dtemp.save();

            }

            ds = dtemp.get_all(cbAseguradora.Text.Substring(0, cbAseguradora.Text.IndexOf("|")).Trim());
            

            return ds;
            
        }


        private void llenarGridDatosImportados(SLDocument oSImport, int rowStar, DataSet ds)
        {
            int fildatatable = 0;

            var stats = oSImport.GetWorksheetStatistics();
            int rowsCount = stats.NumberOfRows + 1 - rowStar;

            if(rowStar <= stats.NumberOfRows)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if(i == 0)
                        this.tableDataExcel.Columns.Add(dataGridView1.Columns[i].HeaderText, typeof(bool));
                    else
                        this.tableDataExcel.Columns.Add(dataGridView1.Columns[i].HeaderText);
                }

                validaciones val = new validaciones();
                string fecvenc = "";
                string fecemi = "";

                while (fildatatable < rowsCount)
                {
                    
                    if (oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["nombre"].ToString() + rowStar) != "")
                    {

                        fecvenc = val.esnumero(oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["fechavencimiento"].ToString() + rowStar)) ? DateTime.FromOADate(Convert.ToInt32(oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["fechavencimiento"].ToString() + rowStar))).ToString("dd/MM/yyyy") : oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["fechavencimiento"].ToString() + rowStar);
                        fecemi = val.esnumero(oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["fechaemisionrecibo"].ToString() + rowStar)) ? DateTime.FromOADate(Convert.ToInt32(oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["fechaemisionrecibo"].ToString() + rowStar))).ToString("dd/MM/yyyy") : oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["fechavencimiento"].ToString() + rowStar);
                        Console.WriteLine("COLUMN "+ ds.Tables[0].Rows[0]["fechavencimiento"].ToString() + " FEC "+fecvenc + " / "+ oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["fechavencimiento"].ToString() + rowStar));

                        this.tableDataExcel.Rows.Add(

                            1,
                            ds.Tables[0].Rows[0]["codigoproductor"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["codigoproductor"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["matricula"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["matricula"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["nombre"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["nombre"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["ramo"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["ramo"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["poliza"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["poliza"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["certificado"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["certificado"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["cuotagarantizada"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["cuotagarantizada"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["sms"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["sms"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["colorcobertura"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["colorcobertura"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["fechavencimiento"].ToString() != "" ? fecvenc : null,
                            ds.Tables[0].Rows[0]["esprimeracuota"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["esprimeracuota"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["moneda"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["moneda"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["monto"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["monto"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["agenterecaudador"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["agenterecaudador"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["pagotramite"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["pagotramite"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["cobrador"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["cobrador"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["saldovencido"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["saldovencido"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["montopagotramite"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["montopagotramite"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["referencia"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["referencia"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["producto"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["producto"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["formapago"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["formapago"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["fechaemisionrecibo"].ToString() != "" ? fecemi : null,
                            ds.Tables[0].Rows[0]["fechainiciovigenciapoliza"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["fechainiciovigenciapoliza"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["fechafinvigenciapoliza"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["fechafinvigenciapoliza"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["organizador"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["organizador"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["riesgoasegurado"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["riesgoasegurado"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["endoso"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["endoso"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["factura"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["factura"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["fechabaja"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["fechabaja"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["cuotan"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["cuotan"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["dnicliente"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["dnicliente"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["periodo"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["periodo"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["email"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["email"].ToString() + rowStar) : null,
                            ds.Tables[0].Rows[0]["telefono"].ToString() != "" ? oSImport.GetCellValueAsString(ds.Tables[0].Rows[0]["telefono"].ToString() + rowStar) : null

                        );
                    }
                    fildatatable++;
                    rowStar++;
                }

            }
            else
            {
                Invoke(new Action(() => MessageBox.Show("La fila establecida para empezar la lectura es mayor a la cantidad de filas que hay en el documento") ));
            }
            
        }

        private bool validacion()
        {
            string errores = "";

            if (this.ruta == "")
            {
                errores += "\nNo se ha cargado el archivo .xlsx";
            }

            if (cbAseguradora.Text.IndexOf("|") < 0)
            {
                errores += "\nNo se ha seleccionado ninguna Aseguradora";
            }

            if(errores != "")
            {
                MessageBox.Show("Debe revisar lo siguiente : \n"+errores,
                "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            string extensionFile = "";
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Archivos Excel|*.xls;*.xlsx";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in openfile.FileNames)
                {
                    extensionFile = Path.GetExtension(openfile.FileName);
                    this.ruta = @file;
                    if (extensionFile == ".xls")
                    {
                        this.convertxlstoxlsx(this.ruta);
                        this.ruta = this.ruta + "x";
                    }

                }
            }
        }

        private void datosIniciales()
        {
            cbCoincidencia.SelectedIndex = 2;

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

        public void disablefields()
        {
            
              

            
        }

        private void frmArchivoCobranza_Load(object sender, EventArgs e)
        {
            perfiles perf = new perfiles();
            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("cobranzas_envios");

            configuracionesToolStripMenuItem.Visible = Convert.ToBoolean(this.access[1]);
            mostrar_busqueda(Convert.ToBoolean(this.access[2]));

            this.datosIniciales();
            
            this.WidhtInit = this.Width;
            this.dataGWidhtInit = dataGridView1.Width;
            this.dataGHeihtgInit = dataGridView1.Height;
            this.HeightInit = this.Height;
        }

        private void chEnvio_CheckedChanged(object sender, EventArgs e)
        {
            int che = 0;
            if (chEnvio.Checked)
                che = 1;

            for(int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Cells["ch"].Value = che; 
            }
        }

        private void listcheckadd()
        {
            this.listCheck.Clear();
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                this.listCheck.Add( Convert.ToInt16( dataGridView2.Rows[i].Cells["ch"].Value) );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView2.Rows.Count > 0)
            {
                if (!System.IO.File.Exists("cobranzas_adjuntos"))
                    System.IO.Directory.CreateDirectory("cobranzas_adjuntos");
                this.generar_carpetas_adjuntos(dataGridView2);
            }
        }

        private void generar_carpetas_adjuntos(DataGridView dgv)
        {

            try
            {
                string pathString = "";

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Cells["nombre"].Value != null)
                    {
                        if (dgv.Rows[i].Cells["nombre"].Value.ToString() != "")
                        {

                            pathString = @"cobranzas_adjuntos/" + dgv.Rows[i].Cells["nombre"].Value.ToString();
                            if (!System.IO.File.Exists(pathString))
                            {
                                System.IO.Directory.CreateDirectory(pathString);
                            }
                        }
                    }
                }

                MessageBox.Show("Se han generado las carpetas");
                System.Diagnostics.Process.Start(@"cobranzas_adjuntos");
            }
            catch(Exception ex)
            {
                logs log = new logs();
                log.newError("COB101", "Error al generar carpetas de archivos adjuntos "+ex.Message);
                MessageBox.Show("Ha ocurrido un error al generar las carpetas",
                "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*dataGridView1.DataSource = ( from DataRow dr in  this.tableDataExcel.AsEnumerable().ToList()
                                         select  dr[3]);*/
            //this.acumular_premio();
            this.filtros_dgv(buscar_dato_txt.Text.Trim(), dataGridView2);
            this.agregarmails(dataGridView2);
        }

        private void asignalistcheck()
        {
            for(int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Cells["ch"].Value = Convert.ToInt16(listCheck[i]);
            }
        }

        private void filtros_dgv(string dato , DataGridView dgv )
        {
            if(columna_busqueda_cb.Text != "")
            {
                DataView dv = this.tableDataExcel.DefaultView;
                dv.RowFilter = columna_busqueda_cb.Text + " LIKE '%" + dato + "%'";
                dgv.DataSource = dv;
            }

        }

        private async Task<string> listado_adjuntos_mails(string path, bool validation = false)
        {
            string res = "";
            try
            {
                DirectoryInfo di = new DirectoryInfo(@path);

                foreach (var fi in di.GetFiles())
                {
                    if (validation)
                    {
                        if ((path + @"\" + System.IO.Path.GetFileName(fi.Name)).Length > 250)
                            res += path + @"\" + System.IO.Path.GetFileName(fi.Name) + "\n\n";
                    }
                    else
                    {
                        res += path + @"\" + System.IO.Path.GetFileName(fi.Name) + ",";
                    }

                }
            }
            catch
            {
                //
                res = ""; 
            }
            
            return res;
        }

        //Función para agregar un mensaje diferente a un cliente específico
        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView2.Rows.Count > 0)
            {
                if(dataGridView2.CurrentRow.Cells["nombre"].Value != null)
                {
                    frmModificaEmail frm = new frmModificaEmail();
                    frm.cliente_id = dataGridView2.CurrentRow.Cells["nombre"].Value.ToString();
                    if (this.listclientes_.IndexOf(frm.cliente_id) >  -1)
                    {
                        frm.nuevo_mensaje = this.listclientes_mails[this.listclientes_.IndexOf(frm.cliente_id)];
                    }
                    
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        
                        this.listclientes_mails.Add(frm.nuevo_mensaje);
                        this.listclientes_.Add(frm.cliente_id);
                    }
                }

            }

            /*for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells["nombre"].Value != null)
                {
                    if (dataGridView2.Rows[i].Cells["nombre"].Value.ToString() != "")
                    {
                        this.listado_adjuntos_mails(@"cobranzas_adjuntos/" + dataGridView2.Rows[i].Cells["nombre"].Value.ToString());
                    }
                }
            }*/
        }




        private void acumular_premio()
        {
            /*this.tableDataExcel =
                from tt in this.tableDataExcel.AsEnumerable().ToList()
                orderby tt["Email"];
              */

            /*var newDt = this.tableDataExcel.AsEnumerable()
            .GroupBy(r => r.Field<string>("Email"))
            .Select(g =>
            {
                
                var row = this.tableDataExcel.NewRow();
                row["Email"] = g.Key;

                return row;
            }).CopyToDataTable();


            for (int i = 0; i < newDt.Rows.Count; i++)
            {
                Console.WriteLine(i+"--> "+newDt.Rows[i]["Email"]);
            }*/



            /*for (int i = 0; i < this.tableDataExcel.Columns.Count; i++)
            {
                Console.WriteLine(this.tableDataExcel.Columns[i]);
            }*/
            
        }

        private async void enviar_btn_Click(object sender, EventArgs e)
        {
            await Task.Run(() => {
                Invoke(new Action(() =>
                    {
                        pictureBox3.Visible = true;
                        send_lbl.Visible = true;
                    }
                ));
            });

            bool send_ = await this.enviar_mails();
            pictureBox3.Visible = false;
            send_lbl.Visible = false;

        }

   

        private async Task<bool> enviar_mails()
        {

            string temidaseguradora = cbAseguradora.Text.Substring(0, cbAseguradora.Text.IndexOf("|")).Trim();
            /*datauploadtemp dataup = new datauploadtemp();
            DataSet dsClientes = dataup.get_group_name(cbAseguradora.Text.Substring(0, cbAseguradora.Text.IndexOf("|")).Trim());
            */


            /*try
            {*/
                
                mailBarrios mb = new mailBarrios("cobranza");
                string paths = "";
                List<string> nom_enviados = new List<string>();
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (nom_enviados.IndexOf(dataGridView2.Rows[i].Cells["Nombre"].Value.ToString()) < 0)
                    {
                        if (dataGridView2.Rows[i].Cells["Email"].Value.ToString() != "")
                        {
                            paths += await this.listado_adjuntos_mails(System.IO.Directory.GetCurrentDirectory() + @"\cobranzas_adjuntos\" 
                                    + dataGridView2.Rows[i].Cells["Nombre"].Value.ToString().Trim(), true);
                            nom_enviados.Add(dataGridView2.Rows[i].Cells["Nombre"].Value.ToString());
                        }
                    }
                    
                }

                if (paths != "")
                {
                    MessageBox.Show("Hay rutas de archivos demasiado largas, por favor trate reducir el nombre del archivo:\n"+paths,
                    "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string concat = "";
                int enviados = 0;
                logs log = new logs();

                nom_enviados = new List<string>();
                List<mail_send_client> msc = new List<mail_send_client>();

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if(Convert.ToInt16(dataGridView2.Rows[i].Cells[0].Value) > 0)
                    {
                        

                    if (dataGridView2.Rows[i].Cells["Email"].Value.ToString() != "")
                        {
                            string dsClientesCompara = dataGridView2.Rows[i].Cells["Nombre"].Value.ToString();
                            /*if (cbCoincidencia.Text == "DNI")
                                dsClientesCompara = dataGridView2.Rows[i].Cells["dnicliente"].Value.ToString();
                            if (cbCoincidencia.Text == "Email")
                                dsClientesCompara = dataGridView2.Rows[i].Cells["email"].Value.ToString();
                                */
                            //int valRegsitro = this.revisarregistro(dsClientesCompara);
                            int valRegsitro = nom_enviados.IndexOf(dsClientesCompara);

                            mail_send_client msc0 = new mail_send_client
                            {
                                idclient = dataGridView2.Rows[i].Cells["DNI_Cliente"].Value.ToString(),
                                nombre = dataGridView2.Rows[i].Cells["nombre"].Value.ToString(),
                                mails = dataGridView2.Rows[i].Cells["email"].Value.ToString(),
                                enviado = "SI",
                                razon = ""
                            };


                        if (valRegsitro < 0)
                            {
                                string msg = await this.mensaje_mail(dataGridView2.Rows[i].Cells["nombre"].Value.ToString());
                                string adj = await this.listado_adjuntos_mails(System.IO.Directory.GetCurrentDirectory() + @"\cobranzas_adjuntos\" + dataGridView2.Rows[i].Cells["nombre"].Value.ToString());

                                //concat += $"\n{enviados} | {dsClientesCompara} | NOMBRE | {dataGridView2.Rows[i].Cells["nombre"].Value.ToString()} | VAL REGSISTRO | {valRegsitro}";

                                //enviados++;

                                nom_enviados.Add(dsClientesCompara);
                                
                                if (mb.enviarMail(
                                    dataGridView2.Rows[i].Cells["email"].Value.ToString(),
                                    "Estado de cuenta "+ " | " + dataGridView2.Rows[i].Cells["nombre"].Value.ToString() + " | "+cbAseguradora.Text.Substring(cbAseguradora.Text.IndexOf("|") + 1, (cbAseguradora.Text.Length - cbAseguradora.Text.IndexOf("|") - 1)).Trim(),
                                    msg,
                                    adj,
                                    "Cobranzas BDP"
                                ))
                                {
                                    mb.objmail.Attachments.Dispose();
                                    enviados++;
                                    msc.Add(msc0);

                                    try
                                    {
                                        logaseguradora logase = new logaseguradora();
                                        logase.idaseguradora = temidaseguradora;
                                        logase.nombre = dataGridView2.Rows[i].Cells["nombre"].Value.ToString();
                                        logase.mensaje = adj == "" ? msg : msg + "<br><br><h4>Archivos adjuntos enviados</h4><br><br>" + adj.Replace(",", "<br>");
                                        logase.email = dataGridView2.Rows[i].Cells["email"].Value.ToString();

                                        if (logase.save() != true)
                                        {
                                            concat += "No se pudo registrar " + logase.nombre + "\n" + logase.email + "\n"+
                                                 adj == "" ? msg : msg + "<br><br><h4>Archivos adjuntos enviados</h4><br><br>" + adj.Replace(",", "<br>") + "\n\n\n";
                                        }
                                    }catch(Exception ex)
                                    {
                                        concat += "No se pudo registrar " + dataGridView2.Rows[i].Cells["nombre"].Value.ToString()
                                            + "\n" + dataGridView2.Rows[i].Cells["email"].Value.ToString() + "\n" + "\n" +
                                                adj == "" ? msg : msg + "<br><br><h4>Archivos adjuntos enviados</h4><br><br>" + adj.Replace(",", "<br>") + "\n\n\n";
                                    }
                                    

                                }else
                                {
                                    

                                    log.coderror = "COBRANZA 101";
                                    log.mensaje = "Error al enviar el correo | " 
                                        + dataGridView2.Rows[i].Cells["email"].Value.ToString()
                                        + " | "+ dsClientesCompara + " | "+ dataGridView2.Rows[i].Cells["nombre"].Value.ToString();

                                
                                
                                    msc0.enviado = "NO";
                                    msc0.razon = log.mensaje;
                                    msc.Add(msc0);
                                    log.save();

                                }

                            }
                            else
                            {
                                log.coderror = "COBRANZA 102";
                                log.mensaje = "Revisión de registro <=0 " + dataGridView2.Rows[i].Cells["Nombre"].Value.ToString() + " - " + dataGridView2.Rows[i].Cells["email"].Value.ToString();
                                log.save();
                            }
                        }
                        else
                        {
                            log.coderror = "COBRANZA 103";
                            log.mensaje = "Email vacío de " + dataGridView2.Rows[i].Cells["nombre"].Value.ToString();

                            msc.Add(new mail_send_client
                            {
                                idclient = dataGridView2.Rows[i].Cells["DNI_Cliente"].Value.ToString(),
                                nombre = dataGridView2.Rows[i].Cells["nombre"].Value.ToString(),
                                mails = dataGridView2.Rows[i].Cells["email"].Value.ToString(),
                                enviado = "NO",
                                razon = log.mensaje
                            });

                            log.save();
                        }
                    }
                    
                }
                
                

                if (enviados > 0)
                {
                    await Task.Run(() => {
                        Invoke(new Action(() =>
                        {
                            pictureBox3.Visible = false;
                            send_lbl.Visible = false;
                            if (concat != "")
                            {
                                txtLogAse.Text = concat;
                                txtLogAse.Visible = true;
                            }

                            dataGridView3.DataSource = msc;
                            logsEnvio_btn.Visible = true;

                        }
                        ));

                    });
                    MessageBox.Show("Se han enviado los correos con éxito");
                    
                }
                else
                {
                    MessageBox.Show("No se ha enviado ningún correo");
                }



                return true;

            /*}
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido enviar todos los correos "+ex.Message,
                "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }*/

            
        }

        private void deletefolders()
        {
            List<string> strDirectories = Directory.GetDirectories("cobranzas_adjuntos", "*", SearchOption.AllDirectories).ToList();

            foreach (string directorio in strDirectories)
            {
                Directory.Delete(directorio, true);
            }
        }

        public int revisarregistro(string data)
        {
            string compara = "Nombre";
            if (cbCoincidencia.Text == "DNI")
                compara = "DNI_cliente";
            if (cbCoincidencia.Text == "Email")
                compara = "Email";
            var row2 = (from DataGridViewRow r in dataGridView2.Rows
                        where r.Cells[compara].Value.ToString() == data
                        where Convert.ToBoolean(r.Cells["ch"].Value) == true
                        select r.Cells[compara].Value);

            return row2.Count();
        }


        public async Task<string> mensaje_mail(string cli)
        {
            string res = "";

            parametrosmailinforme parammail = new parametrosmailinforme();
            parammail.plantillatipo = "mailcobranza";
            if (parammail.get())
            {
                string mensaje_base = parammail.mensaje;
                int item_list = listclientes_.IndexOf(cli);
                if (item_list > -1)
                {
                    mensaje_base = listclientes_mails[item_list];
                }
                res = mensaje_base.Replace("[CLIENTE]", cli);
                res = res.Replace("[ASEGURADORA]", cbAseguradora.Text.Substring(cbAseguradora.Text.IndexOf("|") + 1, (cbAseguradora.Text.Length - cbAseguradora.Text.IndexOf("|") - 1)).Trim());
                res = res.Replace("Medios de pago", "Medios de pago CUIT : " + cbAseguradora.Text.Substring(0, cbAseguradora.Text.IndexOf("|")).Trim() )  ;
                res = res.Replace("[DETALLE_COBRANZA]", await this.detalle_cobranza(cli));
                res = res.Replace("[MEDIOS_PAGO]", await this.medios_pago());
               /* res = res.Replace("Recordar", "<b>Recordar");
                res = res.Replace("aseguradora.", "aseguradora.</b>");*/
                res = res.Replace("\n", "<br>");
            }

            return res;
        }

        public async Task<string> detalle_cobranza(string cli)
        {

            string res = "";

            res = "<br><table><thead style='background-color : black;padding : 5px;color : white;'>" +
               "<tr><th>Ramo</th><th>Nombre</th><th>Póliza</th><th>Cuota</th><th>Moneda</th>" +
               "<th>Forma de Pago</th><th>Monto</th><th style='background-color : black;color : white;'>Vencimiento</th>"+
               "<th style='background-color : white;color : white;'>.</th></tr></thead>" +
               "<tbody style='border : 1px solid #ddd;background-color  : #ddd;'>";

            double total_monto_d = 0;
            double total_monto_p = 0;

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView2.Rows[i].Cells["ch"].Value))
                {
                    if (dataGridView2.Rows[i].Cells["Nombre"].Value != null)
                    {

                        string montoExcel = "0";

                        if (dataGridView2.Rows[i].Cells[13].Value != null)
                        {
                            if (montoExcel.All(char.IsNumber))
                            {
                                montoExcel = String.Format("{0:N}", Convert.ToDouble(dataGridView2.Rows[i].Cells[13].Value) ) ;
                            }
                            else
                            {
                                montoExcel = dataGridView2.Rows[i].Cells[13].Value.ToString();
                            }
                        }

                        

                        if (dataGridView2.Rows[i].Cells["Nombre"].Value.ToString() != "" && dataGridView2.Rows[i].Cells["Nombre"].Value.ToString() == cli)
                        {

                            if (dsperfil.Tables[0].Rows[0]["dolartipodato"].ToString() == dataGridView2.Rows[i].Cells[12].Value.ToString())
                            {
                                montoExcel = "US$ " + montoExcel;
                                total_monto_d += Convert.ToDouble(dataGridView2.Rows[i].Cells[13].Value.ToString().Replace(".", ","));
                            }
                            else
                            {
                                montoExcel = "$ " + montoExcel;
                                total_monto_p += Convert.ToDouble(dataGridView2.Rows[i].Cells[13].Value.ToString().Replace(".", ","));
                            }

                            res += "<tr><td>" + dataGridView2.Rows[i].Cells[4].Value + "</td><td>" + dataGridView2.Rows[i].Cells[3].Value +
                          "</td><td>" + dataGridView2.Rows[i].Cells[5].Value + "</td><td>" + dataGridView2.Rows[i].Cells[30].Value +
                          "</td><td>" + dataGridView2.Rows[i].Cells[12].Value + "</td><td>" + dataGridView2.Rows[i].Cells[21].Value +
                          "</td><td>" + montoExcel + "</td><td>" + dataGridView2.Rows[i].Cells[10].Value +
                          "</td></tr>";
                        }


                    }
                }

            }


            string html_ = "";

            if (total_monto_d > 0)
            {
                html_ += "<div style='background-color: #eee;padding : 2px; width : 300px;' ><h3>Valor Total Dólares : US$ " + String.Format("{0:N}", total_monto_d ) + " </h3></div><br>";
                total_monto_d = 0;
            }

            if (total_monto_p > 0)
            {
                html_ += "<div style='background-color: #eee;padding : 2px; width : 300px;' ><h3>Valor Total Pesos : $ " + String.Format("{0:N}", total_monto_p ) + " </h3></div>";
                total_monto_p = 0;
            }

            res += "</tbody></table> <br>"+html_+"";

            return res;
        }

        public async Task<string> medios_pago()
        {
            string res = "";

            bancosaseguradora banc = new bancosaseguradora();
            DataSet dsbac =  banc.get_all_aseguradora(cbAseguradora.Text.Substring(0, cbAseguradora.Text.IndexOf("|")).Trim());


            string cuenta = "";
            string cbu = "";
            string direccion= "";
            string sucursal = "";
            string titularcuenta = "";


            res = "<table><thead></thead><tbody>";
            for(int i = 0; i < dsbac.Tables[0].Rows.Count; i++)
            {

                cuenta = dsbac.Tables[0].Rows[i]["cuenta"].ToString() != "" ? " <b>Cuenta</b> : " + dsbac.Tables[0].Rows[i]["cuenta"].ToString() : "";
                cbu = dsbac.Tables[0].Rows[i]["cbu"].ToString() != "" ? " <b>CBU</b> : " + dsbac.Tables[0].Rows[i]["cbu"].ToString() : "";
                direccion = dsbac.Tables[0].Rows[i]["direccion"].ToString() != "" ? " <b>Dirección</b> : " + dsbac.Tables[0].Rows[i]["direccion"].ToString() + " - Sucursal : " + dsbac.Tables[0].Rows[i]["sucursal"].ToString() : "";
                sucursal = dsbac.Tables[0].Rows[i]["sucursal"].ToString() != "" ? " <b>Sucursal</b> : "  + dsbac.Tables[0].Rows[i]["sucursal"].ToString() : "";
                titularcuenta = dsbac.Tables[0].Rows[i]["titularcuenta"].ToString() != "" ? " <b>Titular</b> : " + dsbac.Tables[0].Rows[i]["titularcuenta"].ToString() : "";

                res += "<tr ><td style='background-color : black;padding : 5px;color : white;'>"+
                    dsbac.Tables[0].Rows[i]["categoriamediopago"].ToString() + "</td></tr>" +
                "<tr style='background-color : #ddd;border : 1px solid #ddd;'><td>"+
                "<b> Entidad Recaudadora</b> : " + dsbac.Tables[0].Rows[i]["nombrebanco"].ToString() +
                cuenta +
                titularcuenta +
                "<br>"+ cbu +
                direccion +
                sucursal +
                "</td></tr>" 
                ;
            }
            

            res += "</tbody></table>";

            return res;
        }

        private void importarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientesCobranzas frm = new frmClientesCobranzas();
            frm.access = new int[] { 1, 1, 1, 1 } ;
            frm.ShowDialog();
        }

        private void frmArchivoCobranza_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(dataGridView2.Rows.Count > 0)
            {
                if (MessageBox.Show("¿Está Segur@ de cerrar el módulo?", "Cerrar ventana", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            
        }

        private void actualiza_btn_Click(object sender, EventArgs e)
        {
            if (this.validacion())
            {
                this.listcheckadd();
                this.llenarDataDb();
                this.agregarmails(dataGridView2);
            }
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue < 10)
                chEnvio.Visible = true;
            else
                chEnvio.Visible = false;
        }

        private void btnCrearModificar_Click(object sender, EventArgs e)
        {
            if(dataGridView2.Rows.Count > 0)
            {
                frmNuevoCliente frm = new frmNuevoCliente();

                clientes_cobranzas cli = new clientes_cobranzas();
                DataTable dt = new DataTable();


                frm.txtIdentificacion.Text = dataGridView2.CurrentRow.Cells["DNI_Cliente"].Value.ToString();
                frm.txtNombres.Text = dataGridView2.CurrentRow.Cells["Nombre"].Value.ToString();
                frm.txtMail.Text = dataGridView2.CurrentRow.Cells["Email"].Value.ToString();
                frm.txtTelefono.Text = dataGridView2.CurrentRow.Cells[34].Value.ToString();

                if (cbCoincidencia.Text == "Nombre")
                    dt = cli.get_cliente_(frm.txtNombres.Text, cbCoincidencia.Text);
                if (cbCoincidencia.Text == "DNI")
                    dt = cli.get_cliente_(frm.txtIdentificacion.Text, cbCoincidencia.Text);
                if (cbCoincidencia.Text == "Email")
                    dt = cli.get_cliente_(frm.txtMail.Text, cbCoincidencia.Text);

                

                if (dt.Rows.Count > 0)
                {
                    frm.comboBox1.Text = dt.Rows[0]["tipo_id"].ToString();
                    frm.idSeleccion = dt.Rows[0]["id"].ToString();
                    frm.txtNombres.Text = dt.Rows[0]["nombres"].ToString();
                    frm.txtApellidos.Text = dt.Rows[0]["apellidos"].ToString();
                    frm.txtTelefono.Text = dt.Rows[0]["telefono"].ToString();
                    frm.txtDireccion.Text = dt.Rows[0]["direccion"].ToString();
                    frm.txtMail.Text = dt.Rows[0]["email"].ToString();
                    frm.txtCodpostal.Text = dt.Rows[0]["codpostal"].ToString();
                    frm.txtLocalidad.Text = dt.Rows[0]["localidad"].ToString();
                    frm.txtCiudad.Text = dt.Rows[0]["ciudad"].ToString();

                    if (dt.Rows[0]["fecha_nacimiento"].ToString() != "" && dt.Rows[0]["fecha_nacimiento"].ToString() != "00/00/0000")
                        frm.txtFechaNacimiento.Text = Convert.ToDateTime(dt.Rows[0]["fecha_nacimiento"]).ToString("dd/MM/yyyy");
                    frm.txtSexo.Text = dt.Rows[0]["sexo"].ToString();
                    frm.txtSituacion.Text = dt.Rows[0]["situacion"].ToString();
                    frm.txtCategoria.Text = dt.Rows[0]["categoria"].ToString();
                    frm.txtMail.Text = dt.Rows[0]["email"].ToString();
                    frm.txtTelefono.Text = dt.Rows[0]["telefono"].ToString();
                }

                
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    
                    string mail_ = "";
                    if (frm.txtMail.Text != "")
                    {
                        mail_ = frm.txtMail.Text;
                    }
                    else
                    {
                        if (cbCoincidencia.Text == "Nombre")
                            mail_ = cli.get_mail_cliente(frm.txtNombres.Text, cbCoincidencia.Text);
                        if (cbCoincidencia.Text == "DNI")
                            mail_ = cli.get_mail_cliente(frm.txtIdentificacion.Text, cbCoincidencia.Text);
                        if (cbCoincidencia.Text == "Email")
                            mail_ = cli.get_mail_cliente(frm.txtMail.Text, cbCoincidencia.Text);
                        
                    }


                    dataGridView2.CurrentRow.Cells["Email"].Value = mail_.Trim();
                    datauploadtemp dut = new datauploadtemp();
                    dut.update_mail(frm.txtNombres.Text, mail_);
                    dataGridView2.CurrentRow.DefaultCellStyle.BackColor = Color.Orange;
                    dataGridView2.CurrentRow.Cells[0].Value = 0;


                    if (dataGridView2.CurrentRow.Cells["Email"].Value != null)
                    {
                        if (dataGridView2.CurrentRow.Cells["Email"].Value.ToString() != "")
                        {
                            dataGridView2.CurrentRow.DefaultCellStyle.BackColor = Color.White;
                            dataGridView2.CurrentRow.Cells[0].Value = 0;
                        }
                            
                    }
                        
                }

            }
        }

        private void cbAseguradora_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbAseguradora.Text != "")
            {
                frmLogAseguradora frm = new frmLogAseguradora();
                frm.inicialdata();
                frm.cbAseguradora.Text = cbAseguradora.Text;
                frm.search_aseguradora(cbAseguradora.Text.Substring(0, cbAseguradora.Text.IndexOf("|")).Trim());
                frm.ShowDialog();
            }
        }

        private void registrosDeEnvíosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogAseguradora logase = new frmLogAseguradora();
            logase.ShowDialog();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.mostrar_busqueda(false);
            this.tableDataExcel.Rows.Clear();
            buscar_dato_txt.Text = "";
            this.listCheck.Clear();
            dataGridView2.DataSource = new DataGridView();
            this.deletefolders();
            logsEnvio_btn.Visible = false;
            dataGridView3.Rows.Clear();
        }

        private void logsEnvio_btn_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void frmArchivoCobranza_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.dataGWidhtInit + (this.Width - this.WidhtInit);
            dataGridView1.Height = this.dataGHeihtgInit + (this.Height - this.HeightInit);
        }


        private void configuraciónEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfCorreoCobranza frm = new frmConfCorreoCobranza();
            frm.ShowDialog();
        }

        private void frmArchivoCobranza_ResizeEnd(object sender, EventArgs e)
        {
            
        }
    }


    public class mail_send_client{
        public string idclient { get; set; }
        public string nombre { get; set; }
        public string mails { get; set; }
        public string enviado { get; set; }
        public string razon { get; set; }
    }


}


