using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetLight;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Windows.Forms;


namespace ProyectoBrokerDelPuerto
{
    class excelDocuments
    {

        /*
        * Con ésta función se generar el archivo de excel del día
        * @ entero éste parámetro si es 1 se guarda por defecto en la carpeta ExcelFinDia 2 donde esocoja el cliente
        */
        public void ExcelDelDia(string ruta, DataTable dt, System.Windows.Forms.ListBox filaanulada, System.Windows.Forms.ListBox filamayores)
        {
            

            SLDocument oSLDocument = new SLDocument();
            /*
            DataTable dt = new DataTable();

            dt = ds.Tables[0];

            //columnas
            dt.Columns.Add("nro_propuesta", typeof(string));
            dt.Columns.Add("cert_propuesta", typeof(int));
            dt.Columns.Add("tipodoc", typeof(string));
            dt.Columns.Add("documento", typeof(int));
            dt.Columns.Add("apellido", typeof(string));
            dt.Columns.Add("iniciovigencia", typeof(DateTime));
            dt.Columns.Add("finvigencia", typeof(DateTime));
            dt.Columns.Add("meses", typeof(int));
            dt.Columns.Add("costocobertura", typeof(double));
            dt.Columns.Add("costo_total", typeof(double));
            dt.Columns.Add("apellidotomador", typeof(string));
            dt.Columns.Add("tipodoctomador", typeof(string));
            dt.Columns.Add("documentotomador", typeof(int));
            dt.Columns.Add("direcciontomador", typeof(string));
            dt.Columns.Add("cptomador", typeof(string));
            dt.Columns.Add("localidadtomador", typeof(string));
            */

            
            oSLDocument.ImportDataTable(1, 1, dt, true);



            //Espacio para pintar algunos registros
            
            SLStyle style = oSLDocument.CreateStyle();
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.GreenYellow, System.Drawing.Color.Blue);
            
            for (int i = 0; i < filamayores.Items.Count; i++)
            {
                int filasuma = Convert.ToInt16(filamayores.Items[i].ToString()) + 2;
                oSLDocument.SetCellStyle("A" + filasuma, "P" + filasuma, style);
            }
            
            style = oSLDocument.CreateStyle();
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightBlue, System.Drawing.Color.Blue);

            for (int i= 0; i < filaanulada.Items.Count; i++)
            {
                int filasuma = Convert.ToInt16(filaanulada.Items[i].ToString()) + 2;
                oSLDocument.SetCellStyle( "A"+filasuma, "P"+filasuma, style);
            }

            oSLDocument.SaveAs(ruta);
        }


        /*
        * Con ésta función se generar el archivo envío individual
        * @ entero éste parámetro si es 1 se guarda por defecto en la carpeta ExcelFinDia 2 donde esocoja el cliente
        */
        public void ExcelInfoIndividual(string ruta, DataTable dt, System.Windows.Forms.ListBox filapromo)
        {
            SLDocument oSLDocument = new SLDocument();
            oSLDocument.ImportDataTable(1, 1, dt, true);

            SLStyle style = oSLDocument.CreateStyle();
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightBlue, System.Drawing.Color.Blue);

            for (int i = 0; i < filapromo.Items.Count; i++)
            {
                int filasuma = Convert.ToInt16(filapromo.Items[i].ToString()) + 2;
                oSLDocument.SetCellStyle("A" + filasuma, "P" + filasuma, style);
            }

            oSLDocument.SaveAs(ruta);
        }

        /*
        * Con ésta función se generar una archivo con dos hojas de cálculo
        * con el informe de ventas
        * @ ruta string
        * @ dt datatable
        */
        public void ExcelInfoVentas(string ruta, DataTable dt, DataTable dt1, int[] campfechas)
        {
            SLDocument oSLDocument = new SLDocument();
            
            oSLDocument.ImportDataTable(1, 1, dt, true);

            SLStyle colstyleFechaHora = new SLStyle();
            colstyleFechaHora.FormatCode = "d/mm/yyyy";

            for(int i = 0; i < campfechas.Length; i++)
            {
                oSLDocument.SetColumnStyle(campfechas[i], colstyleFechaHora);
            }
                
            

            if (dt1.Rows.Count > 0)
            {
                oSLDocument.AddWorksheet("Sheet2");
                oSLDocument.ImportDataTable(1, 1, dt1, true);
                for (int i = 0; i < campfechas.Length; i++)
                {
                    oSLDocument.SetColumnStyle(campfechas[i], colstyleFechaHora);
                }
            }
            oSLDocument.SaveAs(ruta);
        }



        /*
        * Con ésta función se generar el archivo envío individual
        * @ entero éste parámetro si es 1 se guarda por defecto en la carpeta ExcelFinDia 2 donde esocoja el cliente
        */
        public void baseExcel(string ruta, DataTable dt)
        {
            SLDocument oSLDocument = new SLDocument();
            oSLDocument.ImportDataTable(1, 1, dt, true);
            oSLDocument.SaveAs(ruta);
        }

        /*
        * Con ésta función se generar el archivo general, uniendo todos los demás archivos
        * @ listado_rutas este parámetro tendrá todas las rutas de los archivos a fusionar
        */
        public void FusionExcels(string[] listado_rutas, string ruta_para_guardar)
        {
            DataTable dt = new DataTable();
            int filaReal = 0;

            //Este ciclo cuenta cada archivo
            for (int i = 0; i < listado_rutas.Length; i++)
            {
                //creo el objeto que va a recorrer el archivo especificado
                SLDocument oSImport = new SLDocument(listado_rutas[i]);
                int filas = 1;
                int columnas = 1;
                int aux = 1;
                while (!string.IsNullOrEmpty(oSImport.GetCellValueAsString(aux, 1)))
                {
                    dt.Rows.Add();
                    aux++;
                }
                while (!string.IsNullOrEmpty(oSImport.GetCellValueAsString(filas, 1)))
                {
                    if (i == 0 && filas == 1)
                    {
                        while (!string.IsNullOrEmpty(oSImport.GetCellValueAsString(filas, columnas)))
                        {
                            dt.Columns.Add(oSImport.GetCellValueAsString(filas, columnas), typeof(string));
                            columnas++;
                        }
                        
                        
                        
                    }
                    
                    columnas = 1;
                    
                    if (filas > 1 )
                    {
                        //Console.Write("\n FILA " + filaReal + " -- > FILAS " + filas + " COL " + columnas + " : "+ oSImport.GetCellValueAsString(filas, columnas));
                        while (columnas <= dt.Columns.Count)
                        {
                            dt.Rows[filaReal][columnas - 1] = oSImport.GetCellValueAsString(filas, columnas);
                            columnas++;
                        }
                        
                        filaReal++;
                    }

                    
                    
                    filas++;
                }
                

            }

            SLDocument oSLDocument = new SLDocument();
            oSLDocument.ImportDataTable(1, 1, dt, true);
            oSLDocument.SaveAs(ruta_para_guardar);
        }


        /*
        Con ésta función se genera los registros de la tanbla controlventas,
        se selecciona el archivo y luego esos datos se registran en la base de datos
        */
        public List<RegistroRef> RegistroControlVentas( string rutaArchivo, string fecha)
        {
                SLDocument oSImport = new SLDocument(@rutaArchivo);
                List<RegistroRef> regRef = new List<RegistroRef>();


                int fila = 2;
                //este ciclo recorrere los registros de la hoja de cálculo hasta una celda vacía
                while (!string.IsNullOrEmpty(oSImport.GetCellValueAsString(fila, 1)))
                {

                    controlventas cv = new controlventas();
                    cv.fecha = fecha;
                    cv.propuesta = oSImport.GetCellValueAsString(fila, 1);
                    cv.codgrupo = oSImport.GetCellValueAsString(fila, 2);
                    cv.referencia = oSImport.GetCellValueAsString(fila, 3);
                    cv.tomador = oSImport.GetCellValueAsString(fila, 4);
                    cv.titular = oSImport.GetCellValueAsString(fila, 5);
                    cv.cliente = oSImport.GetCellValueAsString(fila, 6);
                    cv.primaemitida = oSImport.GetCellValueAsString(fila, 7);
                    cv.premioemitido = oSImport.GetCellValueAsString(fila, 8);
                    if(oSImport.GetCellValueAsString(fila, 9) != "")
                        cv.notacredito = oSImport.GetCellValueAsString(fila, 9);
                    cv.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cv.user_edit = MDIParent1.sesionUser;
                    cv.codestado = "1";

                    if (cv.save())
                    {
                        string aux = "0";
                        if (cv.notacredito != "")
                            aux = cv.notacredito;           
                    }

                    fila++;

                }

                if (fila > 0)
                {
                    propuestas pro = new propuestas();
                    pro.asignar_referencia1(fecha);
                    regRef = pro.getRefPropuestas(fecha);
                }

                return regRef;
        }


        

        /*
        Con ésta función se genera los registros de la tanbla controlventas,
        se selecciona el archivo y luego esos datos se registran en la base de datos
        */
        public int ImportaDatosClientes(string rutaArchivo)
        {

            SLDocument oSImport = new SLDocument(@rutaArchivo);
            List<string> li = oSImport.GetSheetNames();
            oSImport.SelectWorksheet(li.First());

            var stats = oSImport.GetWorksheetStatistics();

            int fila = 2;

            DataTable dt = new DataTable();
            //CI | CUIT | DNI | LC | LE
            dt.Columns.Add("Fila", typeof(string));
            dt.Columns.Add("Tipo id", typeof(string));
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Teléfono", typeof(string));
            dt.Columns.Add("Fecha nacimiento", typeof(string));
            dt.Columns.Add("Sexo", typeof(string));
            dt.Columns.Add("Email", typeof(string));

            string query="";
            validaciones val = new validaciones();
            //este ciclo recorrere los registros de la hoja de cálculo hasta una celda vacía
            clientes cli = new clientes();

            try
            {

                int contM = 0;

                while (fila <= stats.NumberOfRows)
                {

                    if (!string.IsNullOrEmpty(oSImport.GetCellValueAsString(fila, 2)))
                    {
                        string[] arrt = { "", "", "", "", "", "" };

                        cli.tipo_id = oSImport.GetCellValueAsString(fila, 1);
                        /*if (cli.tipo_id != "CI" || cli.tipo_id != "CUIT" || cli.tipo_id != "DNI" || cli.tipo_id != "LC" || cli.tipo_id != "LE")
                            arrt[0] = "Tipo ID Inválido";*/


                        //try { cli.id = oSImport.GetCellValueAsInt32(fila, 2).ToString(); } catch { arrt[1] = "Mal formato ID"; }
                        cli.id = oSImport.GetCellValueAsString(fila, 2).ToString();

                        cli.nombres = oSImport.GetCellValueAsString(fila, 3);
                        cli.apellidos = oSImport.GetCellValueAsString(fila, 4);
                        //try { cli.telefono = oSImport.GetCellValueAsInt32(fila, 5).ToString(); } catch { arrt[2] = "Formato Teléfono incorrecto"; }
                        cli.telefono = oSImport.GetCellValueAsString(fila, 5).ToString();
                        cli.direccion = oSImport.GetCellValueAsString(fila, 6);
                        cli.codpostal = oSImport.GetCellValueAsString(fila, 7);
                        cli.localidad = oSImport.GetCellValueAsString(fila, 8);
                        cli.ciudad = oSImport.GetCellValueAsString(fila, 9);

                        bool fecbool = false;

                        if (oSImport.GetCellValueAsString(fila, 10) != null)
                        {
                            if (oSImport.GetCellValueAsString(fila, 10) != "")
                            {

                                /*if (oSImport.GetCellValueAsString(fila, 10) != "00/00/0000")
                                {*/
                                    /* string ttt = oSImport.GetCellValueAsString(fila, 10).Substring(6, 4) + "-" +
                                     oSImport.GetCellValueAsString(fila, 10).Substring(3, 2) + "-" + 
                                     oSImport.GetCellValueAsString(fila, 10).Substring(0, 2);

                                     Console.WriteLine(fila + " 2. ->  " + oSImport.GetCellValueAsString(fila, 10) + " // " + ttt);

                                     cli.fecha_nacimiento =  Convert.ToDateTime(ttt).ToString("yyyy-MM-dd");*/
                                    try { cli.fecha_nacimiento = Convert.ToDateTime(oSImport.GetCellValueAsString(fila, 10)).ToString("yyyy-MM-dd"); }
                                    catch { cli.fecha_nacimiento = "1001-01-01"; }
                                    fecbool = true;
                                //}
                            }
                            else
                            {
                                cli.fecha_nacimiento = "1001-01-01";
                                fecbool = true;
                            }
                        }
                        else
                        {
                            cli.fecha_nacimiento = "1001-01-01";
                            fecbool = true;
                        }


                        cli.sexo = oSImport.GetCellValueAsString(fila, 14);
                        /*if (cli.sexo != "MASCULINO" || cli.sexo != "FEMENINO")
                            arrt[4] = "Debe ser MASCULINO o FEMENINO";*/

                        cli.email = oSImport.GetCellValueAsString(fila, 12);
                        /*if(!val.correo(cli.email))
                            arrt[5] = "Email no válido";*/
                        cli.situacion = oSImport.GetCellValueAsString(fila, 13);
                        cli.categoria = oSImport.GetCellValueAsString(fila, 14);
                        //cli.usuariosube = MDIParent1.sesionUser;

                        cli.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        cli.user_edit = MDIParent1.sesionUser;
                        cli.codestado = "1";


                        if (fecbool)
                            query += cli.compilar_save();


                        

                        
                        if (contM > 100 || ( (stats.NumberOfRows - fila) < 100 ) )
                        {
                            cli.query_compil(query);
                            query = "";
                            contM = 0;
                        }
                    }

                    fila++;
                    contM++;

                }

                System.Windows.Forms.MessageBox.Show("Se han cargado los clientes a la base de datos");

            }
                catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha generado un error al procesar los datos del cliente \n" + ex);
            }


            return fila;

        }

       

        /*
        * Función para importar archivos de las aseguradoras con los datos de cobranza
        */
        public SLDocument importarDatosPerfilAseguradora(string ruta)
        {
            SLDocument oSImport = new SLDocument();
            try {


                oSImport = new SLDocument(ruta);
                List<string> li = oSImport.GetSheetNames();
                oSImport.SelectWorksheet(li.First());

                
            }catch (Exception ex)
            {
                /*logs log = new logs();
                log.coderror = "ExD101";
                log.mensaje = "Error al importar datos archivo excel cobranza "+ex.Message;
                log.save();*/
                MessageBox.Show("Error al importar datos archivo excel cobranza " + ex.Message,
                    "Error al cargar el archivo", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }

            return oSImport;


        }


    }
}
