using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBrokerDelPuerto
{
    public partial class frmInformeControlVentas : Form
    {
        string campografico = "cantemisiones";
        string []arregoanos = {"", "ENERO","FEBRERO","MARZO","ABRIL","MAYO","JUNIO","JULIO","AGOSTO","SEPTIEMBRE","OCTUBRE","NOVIEMBRE","DICIEMBRE" };
        public frmInformeControlVentas()
        {
            InitializeComponent();
        }

        private void ejecucion()
        {
            
        }

        private void grilla1(string fec1, string fec2)
        {
            try
            {
                controlventas cv = new controlventas();
                DataSet ds = cv.get_all_fecha(fec1, fec2);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["primaemitida"] != null)
                    {
                        
                           
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            dataGridView1.Rows.Add(
                                ds.Tables[0].Rows[i]["fecha"],
                                ds.Tables[0].Rows[i]["cantemisiones"],
                                Math.Round(Convert.ToDouble(ds.Tables[0].Rows[i]["primaemitida"])),
                                Math.Round(Convert.ToDouble(ds.Tables[0].Rows[i]["premioemitido"])),
                                ds.Tables[0].Rows[i]["contnota"],
                                ds.Tables[0].Rows[i]["notacredito"]
                            );

                            
                            chart1.Series["Series1"].Points.AddXY(ds.Tables[0].Rows[i]["fecha"], Convert.ToInt16(ds.Tables[0].Rows[i][campografico]));
                            chart1.Series["Series1"].Label = Convert.ToInt16(ds.Tables[0].Rows[i][campografico]).ToString();



                        }



                            chart1.Series["Series1"].LegendText = "Del " + fecha1.Value.ToShortDateString() + " al " + fecha2.Value.ToShortDateString();
                        if (fecha1.Value.Year == fecha2.Value.Year)
                        {
                            if (fecha1.Value.Month == fecha2.Value.Month)
                                chart1.Series["Series1"].LegendText = arregoanos[fecha2.Value.Month] + " del " + fecha2.Value.Year;
                        }
                        chart1.Visible = true;

                    }
                }

            }
            catch
            {
                //error
            }





        }

        private void grilla2(string fec1, string fec2)
        {
            try
            {
                controlventas cv = new controlventas();
                DataSet ds = cv.get_all_fecha(fec1, fec2);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["primaemitida"] != null)
                    {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dataGridView2.Rows.Add(
                                ds.Tables[0].Rows[i]["fecha"],
                                ds.Tables[0].Rows[i]["cantemisiones"],
                                Math.Round(Convert.ToDouble(ds.Tables[0].Rows[i]["primaemitida"])),
                                Math.Round(Convert.ToDouble(ds.Tables[0].Rows[i]["premioemitido"])),
                                ds.Tables[0].Rows[i]["contnota"],
                                ds.Tables[0].Rows[i]["notacredito"]
                            );
                            chart1.Series["Series2"].Points.AddXY(ds.Tables[0].Rows[i]["fecha"], Convert.ToInt16(ds.Tables[0].Rows[i][campografico]));
                            chart1.Series["Series2"].Label = Convert.ToInt16(ds.Tables[0].Rows[i][campografico]).ToString();
                        }
                        chart1.Series["Series2"].LegendText = "Del " + fecha3.Value.ToShortDateString() + " al " + fecha4.Value.ToShortDateString();
                        if (fecha3.Value.Year == fecha4.Value.Year)
                        {
                            if (fecha3.Value.Month == fecha4.Value.Month)
                                chart1.Series["Series2"].LegendText = arregoanos[fecha4.Value.Month] + " del " + fecha4.Value.Year;
                        }
                        chart1.Visible = true;
                    }
                }
            }
            catch
            {
                //error
            }
            
            

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            campografico = "cantemisiones";
            /*this.funfecha1();
            this.funfecha2();
            this.funfecha3();
            this.funfecha4();*/
            this.generarGraficaConDatosListos();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            campografico = "premioemitido";
            /*this.funfecha1();
            this.funfecha2();
            this.funfecha3();
            this.funfecha4();*/
            this.generarGraficaConDatosListos();
        }

        private void fecha1_ValueChanged(object sender, EventArgs e)
        {
            //this.funfecha1();
        }

        public void funfecha1()
        {
            if (DateTime.Compare(fecha1.Value, fecha2.Value) <= 0)
            {
                //chart1.Series["Series1"].Points.Clear();
                //this.grilla1(fecha1.Value.ToString("yyyy-MM-dd"), fecha2.Value.ToString("yyyy-MM-dd"));
                
                    dataGridView1.Visible = false;
                    this.llenarGrilla(dataGridView3, fecha1.Value,fecha2.Value);
                    
            }

        }

        private void llenarGrilla(DataGridView dgv, DateTime fechaInicial, DateTime fechaFinal)
        {
            dgv.Visible = true;
            dgv.Rows.Clear();

            controlventas control = new controlventas();
            propuestas pro = new propuestas();
            pro.user_edit = comboBox1.Text == "TODOS" ? "" : comboBox1.Text;
            DataSet ds = new DataSet();

            lineas_propuestas li = new lineas_propuestas();
            li.user_edit = comboBox1.Text == "TODOS" ? "" : comboBox1.Text;

            if (radioTodo.Checked)
                ds = li.get_entre_controlventas(fechaInicial.ToString("yyyy-MM-dd"), fechaFinal.ToString("yyyy-MM-dd"));
            if (radioDias.Checked)
                ds = pro.get_all_date_dias(fechaInicial.ToString("yyyy-MM-dd"), fechaFinal.ToString("yyyy-MM-dd"));
            if (radioMeses.Checked)
                ds = pro.get_all_date_meses(fechaInicial.ToString("yyyy-MM-dd"), fechaFinal.ToString("yyyy-MM-dd"));
            if (radioAnos.Checked)
                ds = pro.get_all_date_anos(fechaInicial.ToString("yyyy-MM-dd"), fechaFinal.ToString("yyyy-MM-dd"));


            CultureInfo culture = new CultureInfo("es-ES");
            culture.NumberFormat.NumberDecimalDigits = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (radioTodo.Checked){

                    decimal prima_d = 0;
                    decimal premio_d = 0;
                    if (ds.Tables[0].Rows[i]["prima"].ToString() != "")
                        prima_d = Convert.ToDecimal(ds.Tables[0].Rows[i]["prima"].ToString());
                    if (ds.Tables[0].Rows[i]["premio_total"].ToString() != "")
                        premio_d = Convert.ToDecimal(ds.Tables[0].Rows[i]["premio_total"].ToString());

                    dgv.Rows.Add(
                        ds.Tables[0].Rows[i]["ultmod"].ToString(),
                        ds.Tables[0].Rows[i]["id"].ToString(),
                        ds.Tables[0].Rows[i]["documento"].ToString(),
                        ds.Tables[0].Rows[i]["nombre"].ToString(),
                        ds.Tables[0].Rows[i]["referencia"].ToString(),
                        "$"+prima_d.ToString("N", culture),
                        "$" + premio_d.ToString("N", culture),
                        ds.Tables[0].Rows[i]["nota"].ToString(),
                        ds.Tables[0].Rows[i]["user_edit"].ToString()

                    );
                }
                else
                {
                    string comparando = ds.Tables[0].Rows[i]["fecha"].ToString();

                    if (radioMeses.Checked)
                        comparando = arregoanos[Convert.ToInt16(comparando)];

                    decimal prima_d = 0;
                    decimal premio_d = 0;
                    if (ds.Tables[0].Rows[i]["prima"].ToString() != "")
                        prima_d = Convert.ToDecimal(ds.Tables[0].Rows[i]["prima"].ToString());
                    if (ds.Tables[0].Rows[i]["totalpremio"].ToString() != "")
                        premio_d = Convert.ToDecimal(ds.Tables[0].Rows[i]["totalpremio"].ToString());

                    dgv.Rows.Add(
                        comparando,
                        ds.Tables[0].Rows[i]["cantidad"].ToString(),
                        "$" + prima_d.ToString("N", culture),
                        "$" + premio_d.ToString("N", culture),
                        ds.Tables[0].Rows[i]["nota"].ToString()
                    );
                }
                    
            }

            if (!radioTodo.Checked)
            {
                if (dgv.Rows.Count > 0)
                {
                    if (dgv == dataGridView3)
                        this.graficar(1, dgv);
                    if (dgv == dataGridView4)
                        this.graficar(2, dgv);
                }
            }

        }


        private void generarGraficaConDatosListos()
        {

            /*if (dataGridView1.Rows.Count > 0 && radioDias.Checked)
                this.graficar(1, dataGridView1);
            if (dataGridView3.Rows.Count > 0 && !radioDias.Checked)
                this.graficar(1, dataGridView3);
            if (dataGridView2.Rows.Count > 0 && radioDias.Checked)
                this.graficar(2, dataGridView2);
            if (dataGridView4.Rows.Count > 0 && !radioDias.Checked)
                this.graficar(2, dataGridView4);*/
            if (dataGridView3.Rows.Count > 0)
                this.graficar(1, dataGridView3);
            if (dataGridView4.Rows.Count > 0)
                this.graficar(2, dataGridView4);

        }

        private void graficar(int ch, DataGridView dgv)
        {
            if (radioTodo.Checked)
            {
                return;
            }

            if (ch == 1)
            {

                chart1.Visible = true;
                chart1.Series[0].Points.Clear();

                chart1.Series[0].LegendText = "Del " + fecha1.Value.ToShortDateString() + "\n al " + fecha2.Value.ToShortDateString();
                if (fecha1.Value.Year == fecha2.Value.Year)
                {
                    if (fecha1.Value.Month == fecha2.Value.Month)
                        chart1.Series[0].LegendText = arregoanos[fecha2.Value.Month] + " del " + fecha2.Value.Year;
                    if (fecha1.Value == fecha2.Value)
                        chart1.Series[0].LegendText = fecha2.Value.Day+ "-" + arregoanos[fecha2.Value.Month] + " del " + fecha2.Value.Year;
                }

                double valor = 0;
                double valorAux = 0;
                string ejeX = "";
                int punto = 0;
                int contador = 0;

                string[,] puntosDias = new string[dgv.Rows.Count, dgv.Rows.Count];

                
                for (int i = 0; i < dgv.Rows.Count; i++)
                {


                    if (!radioDias.Checked)
                        valor = Convert.ToDouble(dgv.Rows[i].Cells[1].Value.ToString());
                    if (radioButton1.Checked && radioDias.Checked)
                        valor = Convert.ToDouble(dgv.Rows[i].Cells[1].Value.ToString());



                    if (radioButton2.Checked)
                    {
                        /* if(radioDias.Checked)
                             valor = Convert.ToDouble(dgv.Rows[i].Cells[5].Value.ToString());
                         else*/
                            valor = Convert.ToDouble(dgv.Rows[i].Cells[3].Value.ToString());
                    }
                    
                    if (radioButton3.Checked)
                    {

                        /*if (radioDias.Checked)
                            valor = Convert.ToDouble(dgv.Rows[i].Cells[4].Value.ToString());
                        else*/
                        if (dgv.Rows[i].Cells[2].Value.ToString() == "")
                            dgv.Rows[i].Cells[2].Value = 0;
                            valor = Convert.ToDouble(dgv.Rows[i].Cells[2].Value.ToString());
                    }
                        

                    
                    /*if (radioDias.Checked)
                    {
                        
                        if (ejeX != dgv.Rows[i].Cells[0].Value.ToString())
                        {
                            puntosDias[punto,0] = dgv.Rows[i].Cells[0].Value.ToString();
                            puntosDias[punto, 1] = valor.ToString();
                            /*chart1.Series[0].Points.AddXY(dgv.Rows[i].Cells[0].Value, valorAux);
                            chart1.Series[0].Points[punto].Label = valorAux.ToString();*/
                   /*         ejeX = dgv.Rows[i].Cells[0].Value.ToString();
                            
                            punto++;
                        }
                        else
                        {
                            valor += valor;
                            puntosDias[punto-1,1] = valor.ToString();
                            
                        }
                        
                    }
                    else
                    {*/
                        chart1.Series[0].Points.AddXY(dgv.Rows[i].Cells[0].Value, valor);
                        chart1.Series[0].Points[i].Label = valor.ToString();
                    //}
                }

                if (radioDias.Checked)
                {
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        if(puntosDias[i,0] != null && puntosDias[i, 1] != null)
                        {
                            chart1.Series[0].Points.AddXY(puntosDias[i,0], puntosDias[i,1]);
                            chart1.Series[0].Points[i].Label = puntosDias[i, 1];
                        }
                    }
                }
                
                

            }
            if (ch == 2)
            {
                chart2.Visible = true;
                chart2.Series[0].Points.Clear();
                chart2.Series[0].LegendText = "Del " + fecha3.Value.ToShortDateString() + "\n al " + fecha4.Value.ToShortDateString();
                if (fecha3.Value.Year == fecha4.Value.Year)
                {
                    if (fecha3.Value.Month == fecha4.Value.Month)
                        chart2.Series[0].LegendText = arregoanos[fecha4.Value.Month] + " del " + fecha4.Value.Year;
                    if (fecha3.Value == fecha4.Value)
                        chart2.Series[0].LegendText = fecha4.Value.Day + "-" + arregoanos[fecha4.Value.Month] + " del " + fecha4.Value.Year;
                }

                double valor = 0;
                double valorAux = 0;
                string ejeX = "";
                int punto = 0;
                int contador = 0;

                string[,] puntosDias = new string[dgv.Rows.Count, dgv.Rows.Count];

                for (int i = 0; i < dgv.Rows.Count; i++)
                {


                    if (!radioDias.Checked)
                        valor = Convert.ToDouble(dgv.Rows[i].Cells[1].Value.ToString());
                    if (radioButton1.Checked && radioDias.Checked)
                        valor = Convert.ToDouble(dgv.Rows[i].Cells[1].Value.ToString());



                    if (radioButton2.Checked)
                    {
                        /* if(radioDias.Checked)
                             valor = Convert.ToDouble(dgv.Rows[i].Cells[5].Value.ToString());
                         else*/
                        valor = Convert.ToDouble(dgv.Rows[i].Cells[3].Value.ToString());
                    }

                    if (radioButton3.Checked)
                    {

                        /*if (radioDias.Checked)
                            valor = Convert.ToDouble(dgv.Rows[i].Cells[4].Value.ToString());
                        else*/
                        if (dgv.Rows[i].Cells[2].Value.ToString() == "")
                            dgv.Rows[i].Cells[2].Value = "0";
                        valor = Convert.ToDouble(dgv.Rows[i].Cells[2].Value.ToString());
                    }



                    /*if (radioDias.Checked)
                    {
                        
                        if (ejeX != dgv.Rows[i].Cells[0].Value.ToString())
                        {
                            puntosDias[punto,0] = dgv.Rows[i].Cells[0].Value.ToString();
                            puntosDias[punto, 1] = valor.ToString();
                            /*chart1.Series[0].Points.AddXY(dgv.Rows[i].Cells[0].Value, valorAux);
                            chart1.Series[0].Points[punto].Label = valorAux.ToString();*/
                    /*         ejeX = dgv.Rows[i].Cells[0].Value.ToString();

                             punto++;
                         }
                         else
                         {
                             valor += valor;
                             puntosDias[punto-1,1] = valor.ToString();

                         }

                     }
                     else
                     {*/
                    chart2.Series[0].Points.AddXY(dgv.Rows[i].Cells[0].Value, valor);
                    chart2.Series[0].Points[i].Label = valor.ToString();
                    //}
                }

                if (radioDias.Checked)
                {
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        if (puntosDias[i, 0] != null && puntosDias[i, 1] != null)
                        {
                            chart2.Series[0].Points.AddXY(puntosDias[i, 0], puntosDias[i, 1]);
                            chart2.Series[0].Points[i].Label = puntosDias[i, 1];
                        }
                    }
                }
            }



        }

        private void fecha2_ValueChanged(object sender, EventArgs e)
        {
            //this.funfecha2();
        }
        public void funfecha2()
        {
            
            if (DateTime.Compare(fecha1.Value, fecha2.Value) <= 0)
            {
                /*chart1.Series["Series1"].Points.Clear();
                this.grilla1(fecha1.Value.ToString("yyyy-MM-dd"), fecha2.Value.ToString("yyyy-MM-dd"));*/
                /*if (radioDias.Checked)
                    this.llenarGrilla(dataGridView1, fecha1.Value, fecha2.Value);
                if (radioMeses.Checked || radioAnos.Checked)
                {*/
                    dataGridView1.Visible = false;
                    this.llenarGrilla(dataGridView3, fecha1.Value, fecha2.Value);
                //}
            }

        }

        private void fecha3_ValueChanged(object sender, EventArgs e)
        {
            //this.funfecha3();
        }
        public void funfecha3()
        {
            if (DateTime.Compare(fecha3.Value, fecha4.Value) <= 0)
            {
                
                /*if (radioDias.Checked)
                    this.llenarGrilla(dataGridView2, fecha3.Value, fecha4.Value);
                if (radioMeses.Checked || radioAnos.Checked)
                {*/
                    dataGridView2.Visible = false;
                    this.llenarGrilla(dataGridView4,fecha3.Value, fecha4.Value);
                //}
            }
        }

        private void fecha4_ValueChanged(object sender, EventArgs e)
        {
            //this.funfecha4();
        }
        public void funfecha4()
        {
            if (DateTime.Compare(fecha3.Value, fecha4.Value) <= 0)
            {
                /*if (radioDias.Checked)
                    this.llenarGrilla(dataGridView2, fecha3.Value, fecha4.Value);
                if (radioMeses.Checked || radioAnos.Checked)
                {*/
                    dataGridView2.Visible = false;
                    this.llenarGrilla(dataGridView4, fecha3.Value, fecha4.Value);
                //}
            }
        }

        private void frmInformeControlVentas_Load(object sender, EventArgs e)
        {
            usuarios users = new usuarios();
            DataSet ds = users.get_all();
            comboBox1.Items.Add("TODOS");

            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[i]["loggin"].ToString());
            }

            chart1.Visible = false;
            chart2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 || dataGridView3.Rows.Count > 0)
            {

                excelDocuments xls = new excelDocuments();
                string ruta = "";


                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                saveFileDialog.Filter = "Archivos de texto (*.xlsx)|*.xls|Todos los archivos (*.*)|*.*";
                saveFileDialog.FileName = "Archivo Informe ventas" + Convert.ToDateTime(fecha1.Value).ToString("dd-MM-yyyy") + "-" + Convert.ToDateTime(fecha2.Value).ToString("dd-MM-yyyy") +
                    "A" + Convert.ToDateTime(fecha3.Value).ToString("dd-MM-yyyy") + "-" + Convert.ToDateTime(fecha4.Value).ToString("dd-MM-yyyy") + ".xlsx";
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string FileName = saveFileDialog.FileName;
                    ruta = @FileName;
                }

                if(ruta != "")
                {
                    try
                    {
                        
                        xls.ExcelInfoVentas(ruta, this.exceltablas(fecha1.Value, fecha2.Value), this.exceltablas(fecha3.Value, fecha4.Value), new int[0]);
                        MessageBox.Show("Archivo guardado con éxtio en la carpeta del programa \n " + ruta);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se ha podido generar el informe de ventas " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
            
        }


        public DataTable exceltablas(DateTime fechaInicial, DateTime fechaFinal)
        {
            DataTable dt = new DataTable();

            //columnas
            dt.Columns.Add("fecha", typeof(string));
            dt.Columns.Add("idpropuesta", typeof(string));
            dt.Columns.Add("referencia", typeof(string));
            dt.Columns.Add("documento", typeof(string));
            dt.Columns.Add("tomador", typeof(string));
            dt.Columns.Add("asegurado(a)", typeof(string));
            dt.Columns.Add("fecha_nacimiento", typeof(string));
            dt.Columns.Add("meses", typeof(int));
            dt.Columns.Add("cobertura", typeof(string));
            dt.Columns.Add("actividad", typeof(string));
            dt.Columns.Add("clasficiacion", typeof(string));
            dt.Columns.Add("prima", typeof(double));
            dt.Columns.Add("premio", typeof(double));
            dt.Columns.Add("Nota Crédito", typeof(double));
            dt.Columns.Add("usuario", typeof(string));
            dt.Columns.Add("paga", typeof(string));
            dt.Columns.Add("forma_pago", typeof(string));
            dt.Columns.Add("fecha_pago", typeof(string));
            dt.Columns.Add("inicio_vigencia", typeof(string));
            dt.Columns.Add("fin_vigencia", typeof(string));
            dt.Columns.Add("aseguradora", typeof(string));
            


            Configmaster conf = new Configmaster();
            string aseguradora = conf.get("ASEGURADORA").Tables[0].Rows[0]["nomcodigo"].ToString();

            //propuestas pro = new propuestas();
            //DataSet ds = pro.get_all_date_entre(fechaInicial.ToString("yyyy-MM-dd"), fechaFinal.ToString("yyyy-MM-dd"));

            lineas_propuestas li = new lineas_propuestas();
            li.user_edit = comboBox1.Text == "TODOS" ? "" : comboBox1.Text;
            DataSet dd = li.get_entre_controlventas(fechaInicial.ToString("yyyy-MM-dd"), fechaFinal.ToString("yyyy-MM-dd"));
            for (int i = 0; i < dd.Tables[0].Rows.Count; i++)
            {

                        dt.Rows.Add(
                            Convert.ToDateTime(dd.Tables[0].Rows[i]["ultmod"]).ToString("dd/MM/yyyy"),
                            dd.Tables[0].Rows[i]["prefijo"].ToString()+""+dd.Tables[0].Rows[i]["idpropuesta"].ToString(),
                            dd.Tables[0].Rows[i]["referencia"].ToString(),
                            dd.Tables[0].Rows[i]["documento"].ToString(),
                            dd.Tables[0].Rows[i]["nombre"].ToString(),
                            dd.Tables[0].Rows[i]["nombres"].ToString()+" " + dd.Tables[0].Rows[i]["apellidos"].ToString(),
                            Convert.ToDateTime(dd.Tables[0].Rows[i]["fecha_nacimiento"]).ToString("dd/MM/yyyy"),
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
                            dd.Tables[0].Rows[i]["fecha_paga"].ToString(),
                            dd.Tables[0].Rows[i]["fechaDesde"].ToString(),
                            dd.Tables[0].Rows[i]["fechaHasta"].ToString(),
                            aseguradora
                            );
            }
            
            return dt;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            campografico = "primaemitida";
            /*this.funfecha1();
            this.funfecha2();
            this.funfecha3();
            this.funfecha4();*/
            this.generarGraficaConDatosListos();
        }

        private void radioDias_CheckedChanged(object sender, EventArgs e)
        {
            /*
            dataGridView3.Visible = false;
            dataGridView4.Visible = false;
            this.llenarGrilla(dataGridView1, fecha1.Value, fecha2.Value);
            this.llenarGrilla(dataGridView2, fecha3.Value, fecha4.Value);
            */
            if (radioDias.Checked)
            {
                dataGridView1.Visible = false;
                dataGridView2.Visible = false;
                this.llenarGrilla(dataGridView3, fecha1.Value, fecha2.Value);
                this.llenarGrilla(dataGridView4, fecha3.Value, fecha4.Value);
            }
            
        }

        private void radioMeses_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMeses.Checked)
            {
                dataGridView1.Visible = false;
                dataGridView2.Visible = false;
                this.llenarGrilla(dataGridView3, fecha1.Value, fecha2.Value);
                this.llenarGrilla(dataGridView4, fecha3.Value, fecha4.Value);
            }
        }

        private void radioAnos_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMeses.Checked)
            {
                dataGridView1.Visible = false;
                dataGridView2.Visible = false;
                this.llenarGrilla(dataGridView3, fecha1.Value, fecha2.Value);
                this.llenarGrilla(dataGridView4, fecha3.Value, fecha4.Value);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.funfecha1();
            this.funfecha2();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.funfecha3();
            this.funfecha4();
        }

        private void frmInformeControlVentas_Resize(object sender, EventArgs e)
        {
            chart1.Width = this.Width - 752;
            chart2.Width = this.Width - 752;

            dataGridView1.Height = this.Height - 120;
            dataGridView2.Height = this.Height - 120;
            dataGridView3.Height = this.Height - 120;
            dataGridView4.Height = this.Height - 120;
        }

        private void radioTodo_CheckedChanged(object sender, EventArgs e)
        {

            if (radioTodo.Checked)
            {
                dataGridView3.Visible = false;
                dataGridView4.Visible = false;
                this.llenarGrilla(dataGridView1, fecha1.Value, fecha2.Value);
                this.llenarGrilla(dataGridView2, fecha3.Value, fecha4.Value);
            }
            
        }
    }
}
