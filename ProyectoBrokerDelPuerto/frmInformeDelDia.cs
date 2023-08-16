using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace ProyectoBrokerDelPuerto
{
    public partial class frmInformeDelDia : Form
    {
        int[] access;
        ListBox filacolor;
        ListBox filamayores;
        ListBox filapromo;
        DateTime fechaultima;
        configuraciones configprosimport = new configuraciones();

        public frmInformeDelDia()
        {
            InitializeComponent();
        }

        private void fecha_KeyUp(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode != (int)Keys.Back)
            {
                validaciones val = new validaciones();
                fecha.Text = val.fecha_textbox(fecha.Text);
                fecha.Select(fecha.Text.Length, 0);
            }
        }

        public void exportar(bool nomensaje = true)
        {
            /*propuestas propu = new propuestas();
            DataSet ds = propu.revisarPropuestasLineas(Convert.ToDateTime(fecha.Text).ToString("yyyy-MM-dd"));

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string idacorregir = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        idacorregir += ds.Tables[0].Rows[0]["id"].ToString() + "\n";
                    }
                    MessageBox.Show("Se han encontrado propuestas sin líneas de póliza para el " + fecha.Text + "\n " + ds.Tables[0].Rows[0]["id"].ToString(), "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }*/

            excelDocuments xls = new excelDocuments();
            string ruta = AppDomain.CurrentDomain.BaseDirectory + @"ExcelFinDia\FinDelDia-" + comboBox2.Text + Convert.ToDateTime(fecha.Text).ToString("dd-MM-yyyy") + ".xlsx";
            MDIParent1.rutaInformes_global = ruta.Replace("FinDelDia-" + comboBox2.Text + Convert.ToDateTime(fecha.Text).ToString("dd-MM-yyyy") + ".xlsx", "");
            if (checkBox1.Checked)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                saveFileDialog.Filter = "Archivos de texto (*.xlsx)|*.xls|Todos los archivos (*.*)|*.*";
                saveFileDialog.FileName = "FinDelDia-" + comboBox2.Text + Convert.ToDateTime(fecha.Text).ToString("dd-MM-yyyy") + ".xlsx";
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string FileName = saveFileDialog.FileName;
                    MDIParent1.rutaInformes_global = FileName.Replace("FinDelDia-" + comboBox2.Text + Convert.ToDateTime(fecha.Text).ToString("dd-MM-yyyy") + ".xlsx", "");
                    ruta = FileName;

                }else
                {
                    return;
                }

            }


            /*try
            {*/
                filacolor = new ListBox();
                filamayores = new ListBox();

                informes info = new informes();
                info.tipo_informe = "1";
                info.ruta = ruta.Replace("\\", "\\\\");
                info.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
                info.user_edit = MDIParent1.sesionUser;
                info.codestado = "1";
                info.diacierre = Convert.ToDateTime(fecha.Text).ToString("yyyy-MM-dd");
                info.nomtipo = "FINDIA-"+comboBox2.Text;

                DataTable dtInformedia = this.generar_informe( Convert.ToDateTime(fecha.Text).ToString("yyyy-MM-dd"), info);
                if(dtInformedia.Rows.Count < 1)
                {
                    return;
                }

                xls.ExcelDelDia(ruta, dtInformedia, 
                    filacolor, filamayores);

                
                

                //Primero de los informes que se van a generar
                //this.informeFinDelDia();

                //Este informe se genera en la misma ruta que el informe fin del día, el informe individual
                //tiene los registros de las emisiones de manera más específica
                this.generar_informe_individual(Convert.ToDateTime(fecha.Text).ToString("yyyy-MM-dd"), "1");


                //Este informe se genera en la misma ruta que el informe fin del día, el informe colectivo
                //tiene los registros de las emisiones con ALTA EN POLIZA
                this.generar_informe_individual(Convert.ToDateTime(fecha.Text).ToString("yyyy-MM-dd"), "2");

                if (nomensaje)
                {
                    configprosimport.valor = "0";
                    configprosimport.save();
                    MessageBox.Show("Archivo guardado con éxito en la carpeta del programa \n " + ruta);
                }
                    
            /*}
            catch (Exception ex)
            {
                configprosimport.valor = "0";
                configprosimport.save();
                MessageBox.Show("No se ha podido generar el informe de fin del día " + ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/


        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(configprosimport.get_prosimport() == "1")
            {
                MessageBox.Show("En éste momento se está actualizando la base de datos \nPor favor probar nuevamente en 1 minuto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                configprosimport.valor = "1";
                configprosimport.save();
            }

            if (fecha.Text != "")
            {
                usuarios user = new usuarios();

                

                if (DateTime.Compare(Convert.ToDateTime(fecha.Text), fechaultima) <= 0)
                {
                    user.loggin = MDIParent1.sesionUser;
                    DataSet dsUserr = user.get();
                    if(dsUserr.Tables[0].Rows.Count > 0)
                    {
                        if(dsUserr.Tables[0].Rows[0]["allow"].ToString() == "" || dsUserr.Tables[0].Rows[0]["allow"].ToString() == "0")
                        {
                            MessageBox.Show("No tiene permisos para generar nuevamente el informe, contacte a un administrador", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            configprosimport.valor = "0";
                            configprosimport.save();
                            return;
                        }
                    }
                }

                propuestas pro = new propuestas();
                DataSet dspro = pro.get_all_date_findia(Convert.ToDateTime(fecha.Text).ToString("yyyy-MM-dd"));

                if (dspro.Tables[0].Rows.Count > 0)
                {
                    informes inf = new informes();
                    DataSet dsinfoentre = inf.get_informeentre(fechaultima, Convert.ToDateTime(fecha.Text));
                    if (dsinfoentre.Tables[0].Rows.Count > 0)
                    {
                        string concat = "";
                        for (int i = 0; i < dsinfoentre.Tables[0].Rows.Count; i++)
                        {
                            concat += "\n"+dsinfoentre.Tables[0].Rows[i]["ultmod"].ToString();
                        }
                        configprosimport.valor = "0";
                        configprosimport.save();
                        MessageBox.Show("Hay informes sin generar entre " + fechaultima.ToString("dd/MM/yyyy") + " y " + fecha.Text + "\nFechas:" + concat,
                            "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    propuestaencurso procurso = new propuestaencurso();
                    DataSet ds = procurso.get_encurso();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (comboBox1.Text != "")
                        {
                            
                            if (!user.loggin_pass(comboBox1.Text,textBox1.Text))
                            {
                                configprosimport.valor = "0";
                                configprosimport.save();
                                MessageBox.Show("La clave del usuario no es válida");
                            }
                            else
                            {
                                procurso.delete_all();
                                this.exportar();
                            }
                        }
                        else
                        {
                            string concat = "";
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                concat += "Usuario : " + ds.Tables[0].Rows[i]["usuario"].ToString() + " IP " + ds.Tables[0].Rows[i]["ip"].ToString() + "\n";
                            }
                            configprosimport.valor = "0";
                            configprosimport.save();
                            MessageBox.Show("Hay 1 o varios usuarios en el módulo de propuestas \n" + concat);
                        }


                    }
                    else
                    {
                        //Envío de informes
                        this.exportar();
                    }

                    
                    fechaultima = Convert.ToDateTime(inf.get_ultdia());
                    ultdia.Text = "Último día cerrado " + inf.get_ultdia();
                    
                }
                else
                {
                    configprosimport.valor = "0";
                    configprosimport.save();
                    MessageBox.Show("No hay datos en ésta fecha "+fecha.Text);
                }

                configprosimport.valor = "0";
                configprosimport.save();
            }
            
        }


        //Informe del fin del día donde se genera las emisiones para posteriormente enviar
        private void informeFinDelDia()
        {

            
        }

        private DataTable generar_informe(string fecha_, informes info)
        {
            DataTable dt = new DataTable();

            //columnas
            dt.Columns.Add("nro_propuesta", typeof(string));
            dt.Columns.Add("cert_propuesta", typeof(string));
            dt.Columns.Add("tipodoc", typeof(string));
            dt.Columns.Add("documento", typeof(string));
            dt.Columns.Add("apellido", typeof(string));
            dt.Columns.Add("iniciovigencia", typeof(string));
            dt.Columns.Add("finvigencia", typeof(string));
            dt.Columns.Add("meses", typeof(string));
            dt.Columns.Add("costocobertura", typeof(double));
            dt.Columns.Add("costo_total", typeof(double));
            dt.Columns.Add("apellidotomador", typeof(string));
            dt.Columns.Add("tipodoctomador", typeof(string));
            dt.Columns.Add("documentotomador", typeof(string));
            dt.Columns.Add("direcciontomador", typeof(string));
            dt.Columns.Add("cptomador", typeof(string));
            dt.Columns.Add("localidadtomador", typeof(string));
            dt.Columns.Add("master", typeof(string));
            dt.Columns.Add("organizador", typeof(string));
            dt.Columns.Add("productor", typeof(string));

            int aux = 0;

            double valorTotalPremio = 0;
            double valorTotalCobertura = 0;

            propuestas pro = new propuestas();
            DataSet ds = pro.get_all_date_findia(fecha_, comboBox2.Text);
            if(ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {


                    if (ds.Tables[0].Rows[i]["codestado"].ToString() != "0")
                    {

                        
                        
                        lineas_propuestas li = new lineas_propuestas();
                        DataSet dd = li.get_idpropuesta(ds.Tables[0].Rows[i]["idpropuesta"].ToString(), ds.Tables[0].Rows[i]["prefijo"].ToString());
                        clientes cl = new clientes();
                        DataSet dcl = cl.get(ds.Tables[0].Rows[i]["documento"].ToString());

                        if(dd.Tables[0].Rows.Count < 1)
                        {
                            lineas_propuestas_aux li_aux = new lineas_propuestas_aux();
                            dd = li_aux.get_idpropuesta(ds.Tables[0].Rows[i]["idpropuesta"].ToString(), ds.Tables[0].Rows[i]["prefijo"].ToString());
                        }

                        for (int k = 0; k < dd.Tables[0].Rows.Count; k++)
                        {


                            bool promo = false;

                            if (ds.Tables[0].Rows[i]["meses"].ToString() != "" && ds.Tables[0].Rows[i]["premio_total"].ToString() != "")
                            {
                                if (ds.Tables[0].Rows[i]["premio"].ToString() != "")
                                {

                                    if ((Convert.ToDouble(ds.Tables[0].Rows[i]["meses"].ToString()) *
                                        Convert.ToDouble(ds.Tables[0].Rows[i]["num_polizas"].ToString()) *
                                        Convert.ToDouble(ds.Tables[0].Rows[i]["premio"].ToString()))
                                        > Convert.ToDouble(ds.Tables[0].Rows[i]["premio_total"].ToString()))
                                    {
                                        promo = true;
                                    }

                                }
                            }

                            if (ds.Tables[0].Rows[i]["promocion"].ToString() != "" || promo)
                            {
                                filacolor.Items.Add(aux);
                            }
                            
                                
                            if ((Convert.ToInt16(DateTime.Now.Year) - Convert.ToInt16(Convert.ToDateTime(dd.Tables[0].Rows[k]["fecha_nacimiento"].ToString()).Year)) > 65)
                            {
                                filamayores.Items.Add(aux);
                            }

                            if ((Convert.ToInt16(DateTime.Now.Year) - Convert.ToInt16(Convert.ToDateTime(dd.Tables[0].Rows[k]["fecha_nacimiento"].ToString()).Year)) == 65)
                            {
                                if(this.edadVieja(dd.Tables[0].Rows[k]["fecha_nacimiento"].ToString()) == 1)
                                    filamayores.Items.Add(aux);
                            }
                            

                            if (dd.Tables[0].Rows[k]["premio"].ToString() != "")
                            {
                                try
                                {
                                    valorTotalPremio += Convert.ToDouble(dd.Tables[0].Rows[k]["premio"].ToString());
                                }
                                catch
                                {
                                    //
                                }

                            }

                            if (ds.Tables[0].Rows[i]["premio"].ToString() != "")
                            {
                                try
                                {
                                    valorTotalCobertura += Convert.ToDouble(ds.Tables[0].Rows[i]["premio"].ToString());
                                }
                                catch
                                {
                                    //
                                }

                            }
                            

                            dt.Rows.Add(
                                ds.Tables[0].Rows[i]["prefijo"].ToString()+ds.Tables[0].Rows[i]["idpropuesta"].ToString(),
                                (k + 1),
                                dd.Tables[0].Rows[k]["tipo_documento"].ToString(),
                                dd.Tables[0].Rows[k]["documento"].ToString(),
                                dd.Tables[0].Rows[k]["apellidos"].ToString() + " " + dd.Tables[0].Rows[k]["nombres"].ToString(),
                                Convert.ToDateTime(ds.Tables[0].Rows[i]["fechaDesde"].ToString()).ToString("dd/MM/yyyy"),
                                Convert.ToDateTime(ds.Tables[0].Rows[i]["fechaHasta"].ToString()).ToString("dd/MM/yyyy"),
                                ds.Tables[0].Rows[i]["meses"].ToString(),
                                ds.Tables[0].Rows[i]["premio"].ToString(),
                                dd.Tables[0].Rows[k]["premio"].ToString(),
                                dcl.Tables[0].Rows[0]["apellidos"].ToString() + " " + dcl.Tables[0].Rows[0]["nombres"].ToString(),
                                dcl.Tables[0].Rows[0]["tipo_id"].ToString(),
                                dcl.Tables[0].Rows[0]["id"].ToString(),
                                dcl.Tables[0].Rows[0]["direccion"].ToString(),
                                dcl.Tables[0].Rows[0]["codpostal"].ToString(),
                                dcl.Tables[0].Rows[0]["localidad"].ToString(),
                                ds.Tables[0].Rows[i]["master"].ToString(),
                                ds.Tables[0].Rows[i]["organizador"].ToString(),
                                ds.Tables[0].Rows[i]["productor"].ToString()
                           
                             );
                            aux++;
                            
                        }
                    }
                }


                //Fila que tiene el TOTAL  de los premios
                dt.Rows.Add(
                            "TOTAL -> ",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            valorTotalCobertura,
                            valorTotalPremio
                );

                info.save();
            }
            else
            {
                MessageBox.Show("No hay registros para generar informe del día", "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }





            return dt;

            
        }

        public int edadVieja(string fechaUsuario)
        {
            int res = 0;
            try
            {
                if ((Convert.ToInt16(DateTime.Now.Year) - Convert.ToInt16(Convert.ToDateTime(fechaUsuario).Year)) > 65)
                    res = 1;
                if ((Convert.ToInt16(DateTime.Now.Year) - Convert.ToInt16(Convert.ToDateTime(fechaUsuario).Year)) == 65)
                {
                    if ((Convert.ToInt16(DateTime.Now.Month) - Convert.ToInt16(Convert.ToDateTime(fechaUsuario).Month)) > 0)
                        res = 1;
                    if ((Convert.ToInt16(DateTime.Now.Month) - Convert.ToInt16(Convert.ToDateTime(fechaUsuario).Month)) == 0)
                    {
                        if ((Convert.ToInt16(DateTime.Now.Day) - Convert.ToInt16(Convert.ToDateTime(fechaUsuario).Day)) >= 0)
                            res = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR..." + ex.Message);
            }


            return res;
        }


        /*
       * @ fecha será la fecha en la que se deberá generar los datos
       * @ tipo es 1 si es individula 2 si es colectivo
       */
        private DataTable generar_informe_individual(string fecha_, string tipo)
        {
            DataTable dt = new DataTable();

            //columnas
            dt.Columns.Add("certificado", typeof(string));
            dt.Columns.Add("tipodocumento", typeof(string));
            dt.Columns.Add("documento", typeof(string));
            dt.Columns.Add("apellido", typeof(string));
            dt.Columns.Add("sexo", typeof(string));
            dt.Columns.Add("fechanacimiento", typeof(string));
            dt.Columns.Add("capital", typeof(double));
            dt.Columns.Add("amf", typeof(double));
            dt.Columns.Add("subsidio", typeof(double));
            dt.Columns.Add("renta", typeof(string));
            dt.Columns.Add("fechavigencia", typeof(string));
            dt.Columns.Add("codactividad", typeof(string));
            dt.Columns.Add("codclasifactividad", typeof(string));
            dt.Columns.Add("codtarea", typeof(string));
            dt.Columns.Add("apellidobenef", typeof(string));
            dt.Columns.Add("tipodocumentobenef", typeof(string));
            dt.Columns.Add("documentobenef", typeof(string));
            dt.Columns.Add("fechanacimientobenef", typeof(string));
            dt.Columns.Add("direccionbenef", typeof(string));
            dt.Columns.Add("cpbenef", typeof(string));
            dt.Columns.Add("localidadbenef", typeof(string));
            dt.Columns.Add("direccion", typeof(string));
            dt.Columns.Add("localidad", typeof(string));
            dt.Columns.Add("cp", typeof(string));
            dt.Columns.Add("matricula", typeof(string));
            dt.Columns.Add("nrocolegiado", typeof(string));
            dt.Columns.Add("codgrupo", typeof(string));
            dt.Columns.Add("fechainiciovigencia", typeof(string));
            dt.Columns.Add("fechafinvigencia", typeof(string));
            dt.Columns.Add("antiguedad", typeof(string));
            dt.Columns.Add("edad", typeof(string));
            dt.Columns.Add("clausula_norepeticion", typeof(string));
            dt.Columns.Add("barrio", typeof(string));
            dt.Columns.Add("cuit_barrio", typeof(string));
            dt.Columns.Add("barrio_beneficiario", typeof(string));
            dt.Columns.Add("costo", typeof(string));
            dt.Columns.Add("cobertura", typeof(string));
            dt.Columns.Add("grupoestadistico", typeof(string));
            dt.Columns.Add("apellidotomador", typeof(string));
            dt.Columns.Add("tipodocumentotomador", typeof(string));
            dt.Columns.Add("documentotomador", typeof(string));
            dt.Columns.Add("fechanacimientotomador", typeof(string));
            dt.Columns.Add("direcciontomador", typeof(string));
            dt.Columns.Add("cptomador", typeof(string));
            dt.Columns.Add("localidadtomador", typeof(string));
            dt.Columns.Add("master", typeof(string));
            dt.Columns.Add("organizador", typeof(string));
            dt.Columns.Add("productor", typeof(string));

            filapromo = new ListBox();

            int aux = 0;
            propuestas pro = new propuestas();

            DataSet ds = pro.get_all_date_findia(fecha_, comboBox2.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["codestado"].ToString() != "0")
                    {
                        string clausulaNoRepeticion = "N";
                        if (ds.Tables[0].Rows[i]["clausula"].ToString() == "1")
                        {
                            clausulaNoRepeticion = "S";
                        }




                        if (ds.Tables[0].Rows[i]["nueva_poliza"].ToString() == tipo)
                        {
                            lineas_propuestas li = new lineas_propuestas();
                            DataSet dd = li.get_idpropuesta(ds.Tables[0].Rows[i]["idpropuesta"].ToString(), ds.Tables[0].Rows[i]["prefijo"].ToString());
                            clientes cl = new clientes();
                            DataSet dcl = cl.get(ds.Tables[0].Rows[i]["documento"].ToString());

                            /*coberturas cober = new coberturas();
                            DataSet dsCobert = cober.get(ds.Tables[0].Rows[i]["id_cobertura"].ToString());*/

                            for (int k = 0; k < dd.Tables[0].Rows.Count; k++)
                            {
                                /*actividades ac = new actividades();
                                DataSet dsActividad = ac.get(dd.Tables[0].Rows[k]["id_actividad"].ToString());
                                clasificaciones cla = new clasificaciones();
                                DataSet dsClasificacion = cla.get(dd.Tables[0].Rows[k]["id_clasificacion"].ToString());*/


                                bool promo = false;



                                if (ds.Tables[0].Rows[i]["meses"].ToString() != "" && ds.Tables[0].Rows[i]["premio_total"].ToString() != "")
                                {
                                    if (ds.Tables[0].Rows[i]["premio"].ToString() != "")
                                    {

                                        if ((Convert.ToDouble(ds.Tables[0].Rows[i]["meses"].ToString()) *
                                            Convert.ToDouble(ds.Tables[0].Rows[i]["num_polizas"].ToString()) *
                                            Convert.ToDouble(ds.Tables[0].Rows[i]["premio"].ToString()))
                                            > Convert.ToDouble(ds.Tables[0].Rows[i]["premio_total"].ToString()))
                                        {
                                            promo = true;
                                        }

                                    }
                                }

                                if (ds.Tables[0].Rows[i]["promocion"].ToString() != "" || promo)
                                {
                                    filapromo.Items.Add(aux);
                                    //val = (Convert.ToDouble(val) * 2).ToString();
                                }


                                string nombreBarrios = "";
                                string cuitBarrios = "";

                                if (ds.Tables[0].Rows[i]["data_barrios"].ToString() != "")
                                {
                                    JsonTextReader reader = new JsonTextReader(new StringReader(ds.Tables[0].Rows[i]["data_barrios"].ToString()));
                                    JObject obj = JObject.Load(reader);
                                    if (obj["barrios"] != null)
                                    {
                                        List<barrios_propuesta> json_barrios_propuesta = (from dynamic val in obj["barrios"].AsEnumerable().ToList()
                                                                                          select new barrios_propuesta()
                                                                                          {
                                                                                              id_barrio = val["id_barrio"],
                                                                                              nombre = val["nombre"],
                                                                                          }).ToList();

                                        foreach (barrios_propuesta bp in json_barrios_propuesta)
                                        {
                                            nombreBarrios += bp.nombre + ", ";
                                            cuitBarrios += bp.id_barrio + ", ";
                                        }
                                    }
                                } else
                                {
                                    //En ésta sección de código se concatenan los barrios y los cuits de los barrios
                                    barrios_propuesta barrrios = new barrios_propuesta();
                                    DataSet bards = barrrios.get_idpropuesta(ds.Tables[0].Rows[i]["idpropuesta"].ToString(), ds.Tables[0].Rows[i]["prefijo"].ToString());

                                    if (bards.Tables[0].Rows.Count > 0)
                                    {
                                        for (int b = 0; b < bards.Tables[0].Rows.Count; b++)
                                        {
                                            if (b == 0)
                                            {
                                                nombreBarrios += bards.Tables[0].Rows[b]["nombre"];
                                                cuitBarrios += bards.Tables[0].Rows[b]["id_barrio"];
                                            }

                                            if (b > 0)
                                            {
                                                nombreBarrios += "," + bards.Tables[0].Rows[b]["nombre"];
                                                cuitBarrios += "," + bards.Tables[0].Rows[b]["id_barrio"];
                                            }

                                        }
                                    }
                                }



                                if (nombreBarrios == "")
                                {
                                    clausulaNoRepeticion = "N";
                                }

                                /*
                                string edadCliente = "0";

                                if(dcl.Tables[0].Rows[0]["fecha_nacimiento"].ToString() != "")
                                {
                                    edadCliente = this.edadCliente(Convert.ToDateTime(dcl.Tables[0].Rows[0]["fecha_nacimiento"].ToString()));
                                }
                                */
                                //MessageBox.Show("ID y PREFIJO "+ ds.Tables[0].Rows[i]["prefijo"].ToString() + ds.Tables[0].Rows[i]["idpropuesta"].ToString() + "DD "+ dd.Tables[0].Rows.Count + 
                                // " dsActividad "+ dsActividad.Tables[0].Rows.Count + " dsClasificacion " + dsClasificacion.Tables[0].Rows.Count + " dcl " + dcl.Tables[0].Rows.Count);
                                //dt.Rows.Add(ds.Tables[0].Rows[i]["id"].ToString(), (k + 1), dd.Tables[0].Rows[k]["tipo_documento"].ToString(), dd.Tables[0].Rows[k]["documento"].ToString(), dd.Tables[0].Rows[k]["apellidos_nombres"].ToString(), Convert.ToDateTime(ds.Tables[0].Rows[i]["fechaDesde"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), Convert.ToDateTime(ds.Tables[0].Rows[i]["fechaHasta"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), ds.Tables[0].Rows[i]["meses"].ToString(), ds.Tables[0].Rows[i]["premio"].ToString(), ds.Tables[0].Rows[i]["premio_total"].ToString(), dcl.Tables[0].Rows[0]["apellidos"].ToString() + " " + dcl.Tables[0].Rows[0]["nombres"].ToString(), dcl.Tables[0].Rows[0]["tipo_id"].ToString(), dcl.Tables[0].Rows[0]["id"].ToString(), dcl.Tables[0].Rows[0]["direccion"].ToString(), dcl.Tables[0].Rows[0]["codpostal"].ToString(), dcl.Tables[0].Rows[0]["localidad"].ToString());
                                dt.Rows.Add(
                                    (k + 1),
                                    dd.Tables[0].Rows[k]["tipo_documento"].ToString(),
                                    dd.Tables[0].Rows[k]["documento"].ToString(),
                                    dd.Tables[0].Rows[k]["apellidos"].ToString() + " " + dd.Tables[0].Rows[k]["nombres"].ToString(),
                                    dcl.Tables[0].Rows[0]["sexo"].ToString(),
                                    Convert.ToDateTime(dd.Tables[0].Rows[k]["fecha_nacimiento"].ToString()).ToString("dd/MM/yyyy"),
                                    ds.Tables[0].Rows[i]["cobertura_suma"].ToString(),
                                    ds.Tables[0].Rows[i]["cobertura_gastos"].ToString(),
                                    "0",
                                    "0",
                                    Convert.ToDateTime(ds.Tables[0].Rows[i]["fechaDesde"].ToString()).ToString("dd/MM/yyyy"),
                                    dd.Tables[0].Rows[k]["actividad"].ToString().IndexOf("-") > 0 ? dd.Tables[0].Rows[k]["actividad"].ToString().Substring(0, dd.Tables[0].Rows[k]["actividad"].ToString().IndexOf("-")).Trim() : dd.Tables[0].Rows[k]["cod_actividad"].ToString(),
                                    dd.Tables[0].Rows[k]["clasificacion"].ToString().IndexOf("-") > 0 ? dd.Tables[0].Rows[k]["clasificacion"].ToString().Substring(0, dd.Tables[0].Rows[k]["clasificacion"].ToString().IndexOf("-")).Trim() : dd.Tables[0].Rows[k]["cod_clasificacion"].ToString(),
                                    "14",
                                    "Herederos Legales",
                                    "", "", "", "", "", "", "", "", "0", "0", "0",
                                    ds.Tables[0].Rows[i]["prefijo"].ToString() + ds.Tables[0].Rows[i]["idpropuesta"].ToString(),
                                    Convert.ToDateTime(ds.Tables[0].Rows[i]["fechaDesde"].ToString()).ToString("dd/MM/yyyy"),
                                    Convert.ToDateTime(ds.Tables[0].Rows[i]["fechaHasta"].ToString()).ToString("dd/MM/yyyy"),
                                    "0",
                                    this.edadCliente(Convert.ToDateTime(dd.Tables[0].Rows[0]["fecha_nacimiento"].ToString())),
                                    clausulaNoRepeticion,
                                    nombreBarrios,
                                    cuitBarrios,
                                    "N", ds.Tables[0].Rows[i]["premio"].ToString(),
                                    "Opcion " + ds.Tables[0].Rows[i]["id_cobertura"].ToString(),
                                    "484",
                                    dcl.Tables[0].Rows[0]["apellidos"].ToString() + " " + dcl.Tables[0].Rows[0]["nombres"].ToString(),
                                    dcl.Tables[0].Rows[0]["tipo_id"].ToString(),
                                    dcl.Tables[0].Rows[0]["id"].ToString(),
                                    Convert.ToDateTime(dcl.Tables[0].Rows[0]["fecha_nacimiento"].ToString()).ToString("dd/MM/yyyy"),
                                    dcl.Tables[0].Rows[0]["direccion"].ToString(),
                                    dcl.Tables[0].Rows[0]["codpostal"].ToString(),
                                    dcl.Tables[0].Rows[0]["localidad"].ToString(),
                                    ds.Tables[0].Rows[i]["master"].ToString(),
                                    ds.Tables[0].Rows[i]["organizador"].ToString(),
                                    ds.Tables[0].Rows[i]["productor"].ToString()

                                );

                                aux++;
                            }
                        }
                    }

                }

                excelDocuments xls = new excelDocuments();
                string nombre_ = "EnvíoColectivo-" + comboBox2.Text + fecha_ + ".xlsx";
                if (tipo == "2")
                    nombre_ = "EnvíoInidividual-" + comboBox2.Text + fecha_ + ".xlsx";
                string ruta = AppDomain.CurrentDomain.BaseDirectory + @"ExcelEnvioIndividual\EnvioIndividual" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                if (MDIParent1.rutaInformes_global == string.Empty)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    saveFileDialog.Filter = "Archivos de texto (*.xlsx)|*.xls|Todos los archivos (*.*)|*.*";
                    saveFileDialog.FileName = nombre_;
                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        string FileName = saveFileDialog.FileName;
                        ruta = @FileName;
                    }
                }
                else
                {
                    ruta = MDIParent1.rutaInformes_global + nombre_;
                }


                try
                {

                    xls.ExcelInfoIndividual(@ruta, dt, filapromo);
                    informes info = new informes();
                    //tipo de infomre individual
                    info.tipo_informe = "2";
                    info.ruta = ruta.Replace("\\", "\\\\");
                    info.user_edit = MDIParent1.sesionUser;
                    info.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
                    info.codestado = "1";
                    info.diacierre = Convert.ToDateTime(fecha.Text).ToString("yyyy-MM-dd");
                    info.nomtipo = "COLECTIVO-" + comboBox2.Text;
                    if (tipo == "1")
                        info.nomtipo = "INDIVIDUAL-" + comboBox2.Text;
                    info.save();
                }
                catch
                {
                    Console.Write("Error al generar el archivo de excel");
                }




            }
            else
            {
                string typereport = "individual";
                if (tipo == "2")
                    typereport = "colectivo";
                MessageBox.Show("No hay registros para generar informe "+ typereport , "Notificación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            

            return dt;
        }


        private string edadCliente(DateTime dt)
        {
            string res = "0";
            try
            {
                res = ( Convert.ToInt16((DateTime.Now -dt ).Days / 365)).ToString();
            }
            catch (Exception ex)
            {
                res = "0";
            }
            

            return res;
        }






        private void frmInformeDelDia_Load(object sender, EventArgs e)
        {
            this.initform();
        }

        public void initform()
        {

            configprosimport.dato = "prosimport";



            usuarios usu = new usuarios();
            usu.loggin = MDIParent1.sesionUser;
            this.access = usu.accessperfil("informe_findia");
            
            button1.Visible = Convert.ToBoolean(this.access[1]);

            informes info = new informes();

            fechaultima = info.get_ultdia();

            ultdia.Text += " " + fechaultima.ToString("dd/MM/yyyy");

            DataSet dss = usu.get_all_allow();
            comboBox1.Items.Add("");
            for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(dss.Tables[0].Rows[i]["loggin"].ToString());
            }


            fecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Height = 347;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fecha_TextChanged(object sender, EventArgs e)
        {
            this.listcodorganizador();
        }

        public void listcodorganizador()
        {
            if (fecha.Text != "" && fecha.Text.Length > 9)
            {
                validaciones val = new validaciones();
                if (val.validateDate(fecha.Text))
                {
                    propuestas pro = new propuestas();
                    comboBox2.DataSource = pro.get_cod_organizador(Convert.ToDateTime(fecha.Text).ToString("yyyy-MM-dd"));
                }
            }
            
        }
    }
}
