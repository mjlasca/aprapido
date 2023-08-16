using Newtonsoft.Json;
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
    public partial class frmControlVentas : Form
    {
        int[] access;
        string ruta = "";
        string[] nombreMeses = {"","ENERO","FEBRERO","MARZO","ABRIL","MAYO","JUNIO","JULIO","AGOSTO","SEPTIEMBRE","OCTUBRE","NOVIEMBRE","DICIEMBRE" };
        configuraciones configprosimport = new configuraciones();
        public frmControlVentas()
        {
            InitializeComponent();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode != (int)Keys.Back)
            {
                validaciones val = new validaciones();
                textBox1.Text = val.fecha_textbox(textBox1.Text);
                textBox1.Select(textBox1.Text.Length, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            ruta = "";
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Archivos Excel|*.xls;*.xlsx";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in openfile.FileNames)
                {
                    ruta = @file;
                    textBox2.Text = ruta;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && ruta != "")
            {
                try
                {
                    controlventas cv = new controlventas();
                    if (!cv.get_date(Convert.ToDateTime(textBox1.Text).ToString("yyyy-MM-dd")))
                    {
                        try
                        {
                            
                                excelDocuments excel = new excelDocuments();
                                List<RegistroRef> regRef = new List<RegistroRef>();
                                regRef = excel.RegistroControlVentas(ruta, Convert.ToDateTime(textBox1.Text).ToString("yyyy-MM-dd"));
                                if ( regRef.Count > 0)
                                {
                                    fecha1.Value = Convert.ToDateTime(textBox1.Text);
                                    fecha2.Value = Convert.ToDateTime(textBox1.Text);
                                    this.llenarGrilla();
                                    Task.Run(() => {
                                        ApiPropuestas apPro = new ApiPropuestas();
                                            var resultados = new { registros = regRef };
                                            var jsonData = JsonConvert.SerializeObject(resultados);
                                            apPro.SetRef(jsonData);
                                    });
                                    MessageBox.Show("Registros guardados con éxito");


                                }

                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("No se pudo procesar el archivo \n" + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("El registro de ésta fecha ( " + textBox1.Text + " ) ya fue ingresado", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("Hay un error en el procesamiento de los datos \n"+ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show("La fecha y la ruta del archivo deben haberse seleccionado", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void fecha1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void llenarGrilla()
        {
            dataGridView1.Rows.Clear();

            lineas_propuestas li = new lineas_propuestas();
            li.user_edit = comboBox1.Text;
            DataSet dd = li.get_entrefechaslineas(fecha1.Value.ToString("yyyy-MM-dd"), fecha2.Value.ToString("yyyy-MM-dd"), txtReferencia.Text.Trim());

            string mesAno = "";
            int filaTotalMEs = 0;
            double totalPrima = 0;
            double totalPremio = 0;
            double totalNota = 0;
            for (int k = 0; k < dd.Tables[0].Rows.Count; k++)
            {
                
                        if (Convert.ToDateTime(dd.Tables[0].Rows[k]["ultmod"]).ToString("MM") != mesAno)
                        {

                    
                            mesAno = Convert.ToDateTime(dd.Tables[0].Rows[k]["ultmod"]).ToString("MM");
                    
                            dataGridView1.Rows.Add(
                                nombreMeses[Convert.ToInt16(mesAno)], "","","TOTAL ->",
                                "", 
                                Math.Round(totalPrima), 
                                Math.Round(totalPremio), 
                                Math.Round(totalNota)
                            );
                            
                            filaTotalMEs = dataGridView1.Rows.Count-1;
                            dataGridView1.Rows[filaTotalMEs].DefaultCellStyle.BackColor = Color.LightGray;
                            totalPremio = 0;
                            totalPrima = 0;
                            totalNota = 0;
                        }


                        double primaTemp = 0;
                        if (dd.Tables[0].Rows[k]["prima"].ToString() != "")
                        {
                            primaTemp = Convert.ToDouble(dd.Tables[0].Rows[k]["premio"].ToString()) / Convert.ToDouble(dd.Tables[0].Rows[k]["premio_total"].ToString());
                            primaTemp = primaTemp * Convert.ToDouble(dd.Tables[0].Rows[k]["prima"].ToString());
                        }
                            
                        double notaCredTemp = 0;
                        if (dd.Tables[0].Rows[k]["nota"].ToString() != "")
                            notaCredTemp = Convert.ToDouble(dd.Tables[0].Rows[k]["nota"].ToString());

                        dataGridView1.Rows.Add(
                            
                            Convert.ToDateTime(dd.Tables[0].Rows[k]["ultmod"]).ToString("dd-MM-yyyy"),
                            dd.Tables[0].Rows[k]["prefijo"].ToString()+ dd.Tables[0].Rows[k]["id_propuesta"].ToString(),
                            dd.Tables[0].Rows[k]["referencia"].ToString(),
                            dd.Tables[0].Rows[k]["tipo_documento"].ToString() + "-" + dd.Tables[0].Rows[k]["documento"],
                            dd.Tables[0].Rows[k]["apellidos"].ToString() + " " + dd.Tables[0].Rows[k]["nombres"].ToString(),
                            primaTemp,
                            Math.Round(Convert.ToDouble(dd.Tables[0].Rows[k]["premio"].ToString())),
                            notaCredTemp, 
                            dd.Tables[0].Rows[k]["id_propuesta"].ToString(),
                            dd.Tables[0].Rows[k]["prefijo"].ToString(),
                            dd.Tables[0].Rows[k]["user_edit"].ToString()

                        );

                        totalPrima += primaTemp;
                        totalPremio += Convert.ToDouble(dd.Tables[0].Rows[k]["premio"].ToString());
                        totalNota += notaCredTemp;

                    

                    if (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Rows[filaTotalMEs].Cells["prima"].Value = totalPrima;
                        dataGridView1.Rows[filaTotalMEs].Cells["premio"].Value = totalPremio;
                        dataGridView1.Rows[filaTotalMEs].Cells["nota"].Value = totalNota;
                    }
                    

            }

        }

        private void llenarGrilla1()
        {
            try
            {
                dataGridView1.Rows.Clear();
                controlventas cv = new controlventas();
                DataSet ds = cv.get_all_fecha_registros(fecha1.Value.ToString("yyyy-MM-dd"), fecha2.Value.ToString("yyyy-MM-dd"));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["primaemitida"] != null)
                    {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dataGridView1.Rows.Add(
                            Convert.ToDateTime(ds.Tables[0].Rows[i]["fecha"]).ToString("dd-MM-yyyy"),
                            ds.Tables[0].Rows[i]["referencia"],
                            ds.Tables[0].Rows[i]["cliente"],
                            ds.Tables[0].Rows[i]["titular"],
                            Math.Round(Convert.ToDouble(ds.Tables[0].Rows[i]["primaemitida"])),
                            Math.Round(Convert.ToDouble(ds.Tables[0].Rows[i]["premioemitido"])),
                            ds.Tables[0].Rows[i]["notacredito"]

                            );
                        }

                    }
                }

            }
            catch
            {
                //error
            }
        }

        private void fecha2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void frmControlVentas_Load(object sender, EventArgs e)
        {

            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("controlventas_nuevo");

            groupBox1.Visible = Convert.ToBoolean(this.access[1]);
            groupBox2.Visible = Convert.ToBoolean(this.access[2]);

            groupBox2.Visible = false;

            usuarios us = new usuarios();
            us.loggin = MDIParent1.sesionUser;
            DataSet ds = us.get();

            ds = us.get_all();

            comboBox1.Items.Add("");
            for(int i = 0; i < ds.Tables[0].Rows.Count ; i++){
                comboBox1.Items.Add(ds.Tables[0].Rows[i]["loggin"].ToString());
            }
            
            if(ds.Tables[0].Rows.Count > 0)
            {
                if(ds.Tables[0].Rows[0]["allow"] != null && ds.Tables[0].Rows[0]["allow"].ToString() != "")
                {
                    groupBox2.Visible = true;
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
                controlventas cont = new controlventas();
                if (cont.get_date(Convert.ToDateTime(txtFechaBorrar.Text).ToString("yyyy-MM-dd")))
                {
                    if (MessageBox.Show("¿Segur@ desea borrar los registros?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        cont.delete_date(Convert.ToDateTime(txtFechaBorrar.Text).ToString("yyyy-MM-dd"));
                        MessageBox.Show("Los registros de "+txtFechaBorrar.Text+" han sido borrados con éxito");
                        txtFechaBorrar.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("No hay registros para esa fecha");
                }

            
        }

        private void txtFechaBorrar_KeyUp(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode != (int)Keys.Back)
            {
                validaciones val = new validaciones();
                txtFechaBorrar.Text = val.fecha_textbox(txtFechaBorrar.Text);
                txtFechaBorrar.Select(txtFechaBorrar.Text.Length, 0);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.llenarGrilla();
        }

        public string exigencias(string dato, int op)
        {
            string espacios = "";
            if (dato.Length < 8)
            {
                for (int i = 0; i < 8 - dato.Length; i++)
                {
                    espacios += "  ";
                }
                if (dato.Length < 7)
                {
                    espacios += " ";
                }
            }
            try
            {
                if (dato != "")
                {
                    dato = Convert.ToDouble(dato).ToString("#,##0");
                }
            }
            catch
            {

            }


            string res = espacios + dato;


            return res;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["idpropuesta"].Value != null && Convert.ToBoolean(this.access[1]))
            {
                frmNuevaPropuesta frm = new frmNuevaPropuesta();
                propuestas pro = new propuestas();
                DataSet ds = pro.getprefijo(dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString(),dataGridView1.CurrentRow.Cells["idpropuesta"].Value.ToString());
                frm.lblConsecutivo.Text = dataGridView1.CurrentRow.Cells["idpropuesta"].Value.ToString();
                frm.lblPrefijo.Text = ds.Tables[0].Rows[0]["prefijo"].ToString();
                frm.lblidpropuesta.Text = ds.Tables[0].Rows[0]["idpropuesta"].ToString();
                frm.referencianum_txt.Text = ds.Tables[0].Rows[0]["referencia"].ToString();
                frm.edit = true;
                informes info = new informes();
                DataSet infoDs = info.get_tipo_informe("FINDIA", Convert.ToDateTime(ds.Tables[0].Rows[0]["ultmod"]).ToString("yyyy-MM-dd HH:mm:ss"));
                if (infoDs.Tables[0].Rows.Count > 0)
                {
                    if (DateTime.Compare(Convert.ToDateTime(ds.Tables[0].Rows[0]["ultmod"].ToString()), Convert.ToDateTime(infoDs.Tables[0].Rows[0]["diacierre"].ToString())) >= 0)
                    {
                        frm.findeldia = true;
                    }
                }

                frm.codpostalactivo = false;

                frm.textBox1.Text = ds.Tables[0].Rows[0]["documento"].ToString().Trim();
                frm.datosInciales();

                lineas_propuestas li = new lineas_propuestas();
                controlventas controlventas = new controlventas();
                DataSet lineas = li.get_idpropuesta(ds.Tables[0].Rows[0]["idpropuesta"].ToString(), ds.Tables[0].Rows[0]["prefijo"].ToString());

                for (int i = 0; i < lineas.Tables[0].Rows.Count; i++)
                {
                    frm.dataGridView1.Rows.Add();
                    actividades ac = new actividades();
                    clasificaciones cla = new clasificaciones();
                    DataSet dd = ac.get(lineas.Tables[0].Rows[i]["id_actividad"].ToString());

                    DataGridViewComboBoxCell comboboxCell = frm.dataGridView1.Rows[i].Cells["clasificacion"] as DataGridViewComboBoxCell;
                    /*
                    * Espacio para llenar el combo de clasificacion
                    */
                    clasificaciones cla1 = new clasificaciones();
                    cla1.id_actividad = lineas.Tables[0].Rows[i]["id_actividad"].ToString();
                    DataSet ds1 = cla1.get_all_actividad();
                    //MessageBox.Show("ds. "+ds.Tables.Count);

                    if (ds1.Tables.Count > 0)
                    {
                        comboboxCell.Items.Clear();
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            comboboxCell.Items.Add(ds1.Tables[0].Rows[j]["cod"].ToString() + " - " + ds1.Tables[0].Rows[j]["nombre"].ToString());
                        }
                    }
                    /***/
                    frm.dataGridView1.Rows[i].Cells["idpropuesta"].Value = i;
                    frm.dataGridView1.Rows[i].Cells["nodocumento"].Value = lineas.Tables[0].Rows[i]["documento"].ToString();
                    frm.dataGridView1.Rows[i].Cells["documento"].Value = lineas.Tables[0].Rows[i]["tipo_documento"].ToString();
                    frm.dataGridView1.Rows[i].Cells["apellido"].Value = lineas.Tables[0].Rows[i]["apellidos"].ToString();
                    frm.dataGridView1.Rows[i].Cells["nombre"].Value = lineas.Tables[0].Rows[i]["nombres"].ToString();
                    frm.dataGridView1.Rows[i].Cells["fecha"].Value = Convert.ToDateTime(lineas.Tables[0].Rows[i]["fecha_nacimiento"].ToString()).ToString("dd/MM/yyyy");
                    frm.dataGridView1.Rows[i].Cells["actividad"].Value = dd.Tables[0].Rows[0]["cod"].ToString() + " - " + dd.Tables[0].Rows[0]["nombre"].ToString();
                    frm.dataGridView1.Rows[i].Cells["clasificacion"].Value = cla.get(lineas.Tables[0].Rows[i]["id_clasificacion"].ToString()).Tables[0].Rows[0]["cod"].ToString() + " - " + cla.get_nombre(lineas.Tables[0].Rows[i]["id_clasificacion"].ToString());
                    //frm.dataGridView1.Rows.Add(i,lineas.Tables[0].Rows[i]["documento"].ToString(), lineas.Tables[0].Rows[i]["tipo_documento"].ToString(), lineas.Tables[0].Rows[i]["apellidos_nombres"].ToString(), lineas.Tables[0].Rows[i]["fecha_nacimiento"].ToString(), dd.Tables[0].Rows[0]["nombre"].ToString());

                }



                frm.fechaDesde.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["fechaDesde"].ToString());
                frm.fechaHasta.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["fechaHasta"].ToString());

                frm.comboBox4.Text = ds.Tables[0].Rows[0]["meses"].ToString();
                frm.checkBox1.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["clausula"]);
                frm.checkBox2.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["barrio_beneficiario"]);
                frm.comboCobertura.Text = ds.Tables[0].Rows[0]["id_cobertura"].ToString();
                barrios ba = new barrios();

                //frm.comboBarrios.Text = ba.get_nombre(ds.Tables[0].Rows[0]["id_barrio"].ToString()); 
                frm.txtPremio.Text = string.Format("{0:c}", Convert.ToDouble(ds.Tables[0].Rows[0]["premio"].ToString()));
                frm.txtPremioTotal.Text = string.Format("{0:c}", Convert.ToDouble(ds.Tables[0].Rows[0]["premio_total"].ToString()));

                if (ds.Tables[0].Rows[0]["nueva_poliza"].ToString() == "1")
                {
                    frm.radAltaPoliza.Checked = true;
                }
                if (ds.Tables[0].Rows[0]["nueva_poliza"].ToString() == "2")
                {
                    frm.radNuevaPoliza.Checked = true;
                }
                if (Convert.ToDateTime(dataGridView1.CurrentRow.Cells["fecha"].Value).Date != DateTime.Now.Date || frm.findeldia)
                {
                    frm.formEdit(ds.Tables[0].Rows[0]["ultmod"].ToString());
                    frm.dataGridView1.ReadOnly = true;
                    frm.botEliminarFila = false;
                }




                /*
                * ListBox seleccionados
                */

                if (ds.Tables[0].Rows[0]["data_barrios"] != null && ds.Tables[0].Rows[0]["data_barrios"].ToString() != "")
                {
                    frm.set_json_barrios_listbox(ds.Tables[0].Rows[0]["data_barrios"].ToString());
                }
                else
                {

                    barrios_propuesta baProp = new barrios_propuesta();
                    baProp.idprefijo = ds.Tables[0].Rows[0]["idpropuesta"].ToString();
                    baProp.prefijo = ds.Tables[0].Rows[0]["prefijo"].ToString();
                    DataSet barrds = baProp.get_idpropuesta(baProp.idprefijo, baProp.prefijo);
                    for (int j = 0; j < barrds.Tables[0].Rows.Count; j++)
                    {
                        frm.listBox1.Items.Add(barrds.Tables[0].Rows[j]["nombre"].ToString());

                        barrios barr = new barrios();
                        barr.id = barr.get_id(barrds.Tables[0].Rows[j]["nombre"].ToString());
                        DataSet dsBarr = barr.get();

                        if (dsBarr.Tables[0].Rows.Count > 0)
                        {
                            frm.listBox2.Items.Add(
                                   " Mte: " + this.exigencias(dsBarr.Tables[0].Rows[0]["suma_muerte"].ToString(), 1) +
                                   "   -   GM:" +
                                   this.exigencias(dsBarr.Tables[0].Rows[0]["suma_gm"].ToString(), 2)
                               );
                        }

                    }
                }

                //totales
                frm.premio_ = ds.Tables[0].Rows[0]["premio"].ToString();
                frm.premioTotal_ = ds.Tables[0].Rows[0]["premio_total"].ToString();

                frm.ShowDialog();
                
            }

        }

        private void btnexportar_Click(object sender, EventArgs e)
        {
            excelDocuments xls = new excelDocuments();
            string ruta = "";


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.xlsx)|*.xls|Todos los archivos (*.*)|*.*";
            saveFileDialog.FileName = "Archivo Informe ventas " + Convert.ToDateTime(fecha1.Value).ToString("dd-MM-yyyy") + "-" + Convert.ToDateTime(fecha2.Value).ToString("dd-MM-yyyy") ;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                ruta = @FileName;
            }

            if (ruta != "")
            {
                /*try
                {*/
                    DataTable dt2 = new DataTable();
                    int[] veccampfechas = { 1, 6,10, 23, 24, 25 };
                    xls.ExcelInfoVentas(ruta, this.exceltablas(fecha1.Value, fecha2.Value), dt2, veccampfechas);
                    MessageBox.Show("Archivo guardado con éxtio en la carpeta del programa \n " + ruta);
                /*}
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha podido generar el informe de ventas " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
            }
        }

        public DataTable exceltablas_efectivo(DateTime fechaInicial, DateTime fechaFinal)
        {
            DataTable dt = new DataTable();

            //columnas
            dt.Columns.Add("Fecha Emisión", typeof(string));
            dt.Columns.Add("Fecha Paga", typeof(string));
            dt.Columns.Add("Prefijo", typeof(string));
            dt.Columns.Add("Idpropuesta", typeof(string));
            dt.Columns.Add("Referencia", typeof(string));
            dt.Columns.Add("Tomador", typeof(string));
            dt.Columns.Add("Asegurado(a)", typeof(string));
            dt.Columns.Add("Fecha_nacimiento", typeof(string));
            dt.Columns.Add("Premio", typeof(double));
            dt.Columns.Add("Ramo", typeof(int));
            dt.Columns.Add("Producto", typeof(int));
            dt.Columns.Add("No. Producto", typeof(string));



            Configmaster conf = new Configmaster();
            string aseguradora = conf.get("ASEGURADORA").Tables[0].Rows[0]["nomcodigo"].ToString();

            //propuestas pro = new propuestas();
            //DataSet ds = pro.get_all_date_entre(fechaInicial.ToString("yyyy-MM-dd"), fechaFinal.ToString("yyyy-MM-dd"));

            lineas_propuestas li = new lineas_propuestas();
            li.user_edit = comboBox1.Text == "TODOS" ? "" : comboBox1.Text;
            DataSet dd = li.get_entre_controlventas_efectivo(fechaInicial.ToString("yyyy-MM-dd"), fechaFinal.ToString("yyyy-MM-dd"), txtReferencia.Text);
            for (int i = 0; i < dd.Tables[0].Rows.Count; i++)
            {

                dt.Rows.Add(
                    Convert.ToDateTime(dd.Tables[0].Rows[i]["ultmod"]).ToString("dd/MM/yyyy"),
                    dd.Tables[0].Rows[i]["fecha_paga"].ToString() != "" ? Convert.ToDateTime(dd.Tables[0].Rows[i]["fecha_paga"].ToString()).ToString("dd/MM/yyyy") : "",
                    dd.Tables[0].Rows[i]["prefijo"].ToString(),
                    dd.Tables[0].Rows[i]["idpropuesta"].ToString(),
                    dd.Tables[0].Rows[i]["referencia"].ToString(),
                    dd.Tables[0].Rows[i]["nombre"].ToString(),
                    dd.Tables[0].Rows[i]["nombres"].ToString() + " " + dd.Tables[0].Rows[i]["apellidos"].ToString(),
                    Convert.ToDateTime(dd.Tables[0].Rows[i]["fecha_nacimiento"]).ToString("dd/MM/yyyy"),
                    dd.Tables[0].Rows[i]["premio"].ToString() != "" ? Convert.ToDouble(dd.Tables[0].Rows[i]["premio"].ToString()) : 0,
                    "600",
                    "27",
                    "AP. OP. ESP. O. Trabajo"
                    );
            }

            return dt;
        }

        public DataTable exceltablas(DateTime fechaInicial, DateTime fechaFinal)
        {
            DataTable dt = new DataTable();

            //columnas
            dt.Columns.Add("fecha", typeof(DateTime));
            dt.Columns.Add("idpropuesta", typeof(string));
            dt.Columns.Add("referencia", typeof(string));
            dt.Columns.Add("documento", typeof(string));
            dt.Columns.Add("Tomador", typeof(string));
            dt.Columns.Add("Fecha.Nacimiento.Tomador", typeof(DateTime));
            dt.Columns.Add("Email.Tomador", typeof(string));
            dt.Columns.Add("Telf.Tomador", typeof(string));
            dt.Columns.Add("asegurado(a)", typeof(string));
            dt.Columns.Add("Fecha.Nacimiento.Asegurado", typeof(DateTime));
            dt.Columns.Add("meses", typeof(int));
            dt.Columns.Add("cobertura", typeof(string));
            dt.Columns.Add("actividad", typeof(string));
            dt.Columns.Add("clasficiacion", typeof(string));
            dt.Columns.Add("prima", typeof(double));
            dt.Columns.Add("premio", typeof(double));
            dt.Columns.Add("Nota Crédito Total Póliza", typeof(double));
            dt.Columns.Add("usuario", typeof(string));
            dt.Columns.Add("paga", typeof(string));
            dt.Columns.Add("Tipo_pago", typeof(string));
            dt.Columns.Add("Forma_pago", typeof(string));
            dt.Columns.Add("Comprobante", typeof(string));
            dt.Columns.Add("fecha_pago", typeof(DateTime));
            dt.Columns.Add("inicio_vigencia", typeof(DateTime));
            dt.Columns.Add("fin_vigencia", typeof(DateTime));
            dt.Columns.Add("aseguradora", typeof(string));
            dt.Columns.Add("cod_productor", typeof(string));
            dt.Columns.Add("cod_organizador", typeof(string));



            Configmaster conf = new Configmaster();
            string aseguradora = conf.get("ASEGURADORA").Tables[0].Rows[0]["nomcodigo"].ToString();

            //propuestas pro = new propuestas();
            //DataSet ds = pro.get_all_date_entre(fechaInicial.ToString("yyyy-MM-dd"), fechaFinal.ToString("yyyy-MM-dd"));

            lineas_propuestas li = new lineas_propuestas();
            li.user_edit = comboBox1.Text == "TODOS" ? "" : comboBox1.Text;
            DataSet dd = li.get_entre_controlventas(fechaInicial.ToString("yyyy-MM-dd"), fechaFinal.ToString("yyyy-MM-dd"), txtReferencia.Text);
            DateTime? fecpagadefault = null;
            for (int i = 0; i < dd.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add(
                    dd.Tables[0].Rows[i]["ultmod"].ToString() != "" ? Convert.ToDateTime(dd.Tables[0].Rows[i]["ultmod"]) : fecpagadefault,
                    dd.Tables[0].Rows[i]["prefijo"].ToString() + "" + dd.Tables[0].Rows[i]["idpropuesta"].ToString(),
                    dd.Tables[0].Rows[i]["referencia"].ToString(),
                    dd.Tables[0].Rows[i]["documento"].ToString(),
                    dd.Tables[0].Rows[i]["nombre"].ToString(),
                    dd.Tables[0].Rows[i]["nacimientotomador"].ToString() != "" ? Convert.ToDateTime(dd.Tables[0].Rows[i]["nacimientotomador"]) : fecpagadefault,
                    dd.Tables[0].Rows[i]["correo"].ToString(),
                    dd.Tables[0].Rows[i]["telefono"].ToString(),
                    dd.Tables[0].Rows[i]["nombres"].ToString()+" "+ dd.Tables[0].Rows[i]["apellidos"].ToString(),
                    Convert.ToDateTime(dd.Tables[0].Rows[i]["fecha_nacimiento"]), 
                    dd.Tables[0].Rows[i]["meses"].ToString(),
                    dd.Tables[0].Rows[i]["id_cobertura"].ToString(),
                    dd.Tables[0].Rows[i]["actividad"].ToString(),
                    dd.Tables[0].Rows[i]["clasificacion"].ToString(),
                    dd.Tables[0].Rows[i]["prima"].ToString() != "" ? Convert.ToDouble(dd.Tables[0].Rows[i]["prima"].ToString()) : 0,
                    dd.Tables[0].Rows[i]["premio"].ToString() != "" ? Convert.ToDouble(dd.Tables[0].Rows[i]["premio"].ToString()) : 0,
                    dd.Tables[0].Rows[i]["nota"].ToString() != "" ? Convert.ToDouble(dd.Tables[0].Rows[i]["nota"].ToString()) : 0,
                    dd.Tables[0].Rows[i]["user_edit"].ToString(),
                    dd.Tables[0].Rows[i]["paga"].ToString() == "1" ? "SI" : "NO",
                    dd.Tables[0].Rows[i]["formadepago"].ToString(),
                    dd.Tables[0].Rows[i]["tipopago"].ToString(),
                    dd.Tables[0].Rows[i]["compformapago"].ToString(),
                    dd.Tables[0].Rows[i]["fecha_paga"].ToString() != "1/1/1000 1:00:00 AM" ? Convert.ToDateTime(dd.Tables[0].Rows[i]["fecha_paga"]) : fecpagadefault,
                    dd.Tables[0].Rows[i]["fechaDesde"].ToString() != "" ? Convert.ToDateTime(dd.Tables[0].Rows[i]["fechaDesde"]) : fecpagadefault,
                    dd.Tables[0].Rows[i]["fechaHasta"].ToString() != "" ? Convert.ToDateTime(dd.Tables[0].Rows[i]["fechaHasta"]) : fecpagadefault,
                    aseguradora,
                    dd.Tables[0].Rows[i]["productor"].ToString(),
                    dd.Tables[0].Rows[i]["organizador"].ToString()
                    );
            }

            return dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            excelDocuments xls = new excelDocuments();
            string ruta = "";


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.xls)|*.xlsx|Todos los archivos (*.*)|*.*";
            saveFileDialog.FileName = "Archivo Informe ventas Efectivo " + Convert.ToDateTime(fecha1.Value).ToString("dd-MM-yyyy") + "-" + Convert.ToDateTime(fecha2.Value).ToString("dd-MM-yyyy");
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                ruta = @FileName;
            }

            if (ruta != "")
            {
                try
                {
                    
                    DataTable dt2 = new DataTable();
                    xls.ExcelInfoVentas(ruta, this.exceltablas_efectivo(fecha1.Value, fecha2.Value), dt2,  new int[0]);
                    MessageBox.Show("Archivo guardado con éxtio en la carpeta del programa \n " + ruta);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha podido generar el informe de ventas en Efectivo " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
