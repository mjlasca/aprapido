using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics;

namespace ProyectoBrokerDelPuerto
{
    class generarPdfs
    {
        public void pdfEmisionBarriosPrivados(string idpropuesta, clientes tomador, DataGridView dgv, DateTime vigenciaDesde, DateTime vigenciaHasta, ListBox barrio, coberturas cobertura, bool norepeticion, bool benefbarrios, bool abrir = true, string ruta = "", bool pagado = false)
        {

            try
            {
                /*rutasarchivos r = new rutasarchivos();
                DataSet dsRutas = r.get(r.GetLocalIPAddress());

                if(dsRutas.Tables[0].Rows.Count < 1)
                {
                    MessageBox.Show("No existe ruta para certificados ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;    
                }
                */

                DateTime datett = DateTime.Now;
                Document doc = new Document(PageSize.LETTER,30f,20f,50f,105f);
                //string ruta = @"EmisionesBasrrios\EmisiónBarriosPrivados-No" + idpropuesta + ".pdf";
                ruta = ruta + @"\EmisiónBarriosPrivados-No" + idpropuesta + ".pdf";

                //ruta = @dsRutas.Tables[0].Rows[0]["rutacertificados"].ToString()+ @"\" + idpropuesta + ".pdf";

                if (File.Exists(ruta))
                {
                    try
                    {
                        File.Delete(ruta);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"No se ha podido generar el PDF {e.Message}");
                    }
                }
                PdfWriter pw = PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.OpenOrCreate));

                

                doc.Open();

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("imgsancor2.jpg");
                jpg.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
                // Creamos el tipo de Font que vamos utilizar
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _TituloFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLDITALIC, BaseColor.BLACK);
                iTextSharp.text.Font _SubTituloFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _encabezadosTabla = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFontBold1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);


                PdfPTable tblEncabezado = new PdfPTable(2);//dgv.ColumnCount cantidad de campos del dataGrid
                tblEncabezado.WidthPercentage = 100;
                tblEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                float[] widths0 = new float[] { 70f, 30f };
                tblEncabezado.SetWidths(widths0);

                PdfPCell clLogo = new PdfPCell(new Phrase(" ", _standardFont));
                clLogo.BorderWidth = 0;

                clLogo = new PdfPCell(jpg);
                clLogo.BorderWidth = 0;
                clLogo.BorderWidth = 0;
                clLogo.HorizontalAlignment = Element.ALIGN_CENTER;


                PdfPCell titulo = new PdfPCell(new Phrase("SEGURO DE ACCIDENTES PERSONALES EN OCASIÓN DEL TRABAJO - BARRIOS PRIVADOS", _TituloFont));
                titulo.BorderWidth = 0;
                titulo.HorizontalAlignment = Element.ALIGN_CENTER;

                tblEncabezado.AddCell(titulo);
                tblEncabezado.AddCell(clLogo);

                Paragraph constancia = new Paragraph("Constancia de Póliza - P N°: " + idpropuesta, _SubTituloFont);
                constancia.Alignment = Element.ALIGN_CENTER;

                doc.Add(tblEncabezado);
                doc.Add(constancia);
                Paragraph parrafo = new Paragraph("Por medio del presente, damos constancia que se otorga cobertura en el seguro de Accidentes Personales (con motivo y ocasión del trabajo) de Sancor Cooperativa de Seguros Ltda. las personas que se detallan a continuación y en las condiciones descriptas seguidamente, encontrándose la correspondiente póliza en trámite de emisión. \n\n", _standardFont);
                parrafo.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(parrafo);


                PdfPTable tablaPrimaria = new PdfPTable(4);//dgv.ColumnCount cantidad de campos del dataGrid
                tablaPrimaria.WidthPercentage = 100;
                widths0 = new float[] { 30f, 20f, 20f, 30f };
                tablaPrimaria.SetWidths(widths0);

                //fila1
                PdfPCell celdasEncabezado = new PdfPCell(new Phrase("DATOS DEL TOMADOR", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Colspan = 4;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celdasEncabezado);

                //fila2
                celdasEncabezado = new PdfPCell(new Phrase("Nombres y Apellidos/Razón Social", _standardFont));
                celdasEncabezado.Padding = 7;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celdasEncabezado);


                celdasEncabezado = new PdfPCell(new Phrase(tomador.apellidos + ", " + tomador.nombres, _standardFont));
                celdasEncabezado.Padding = 7;
                celdasEncabezado.Colspan = 3;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;

                tablaPrimaria.AddCell(celdasEncabezado);

                //fila3
                celdasEncabezado = new PdfPCell(new Phrase("Tipo y número de documento", _standardFont));
                celdasEncabezado.Padding = 7;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(tomador.tipo_id + " " + tomador.id, _standardFont));
                celdasEncabezado.Padding = 7;
                celdasEncabezado.Colspan = 3;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;

                tablaPrimaria.AddCell(celdasEncabezado);

                //fila4
                celdasEncabezado = new PdfPCell(new Phrase("BARRIOS PRIVADOS \n en los que realizará la tarea declarada", _standardFont));
                celdasEncabezado.Padding = 7;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celdasEncabezado);

                
                string nombarrio = "";
                string subbarrios = "";
                string cuitbarrios = "";
                string concatbarrios = "";
                string beneficiarios = "";

                if (barrio.Items.Count > 0)
                {
                    barrios bar = new barrios();
                    nombarrio = barrio.Items[0].ToString();
                    bar.id = bar.get_id(nombarrio);
                    DataSet ds = bar.get();
                    cuitbarrios = bar.id;
                    if(ds.Tables[0].Rows.Count > 0)
                        subbarrios = ds.Tables[0].Rows[0]["sub_barrio"].ToString();

                    concatbarrios = nombarrio + " -  " + cuitbarrios + " \t " + subbarrios;
                    nombarrio = nombarrio + " -  " + cuitbarrios + " \t " + subbarrios;
                    
                    for (int i = 1; i < barrio.Items.Count; i++)
                    {

                        barrios barr = new barrios();

                        barr.id = barr.get_id(barrio.Items[i].ToString());
                        ds = barr.get();
                        cuitbarrios = barr.id;
                        if(ds.Tables[0].Rows.Count > 0)
                            subbarrios = ds.Tables[0].Rows[0]["sub_barrio"].ToString();

                        if (i < 4)
                        {
                            nombarrio += ", " + barrio.Items[i].ToString().Trim() + " | " + cuitbarrios + " | " + subbarrios;
                        }


                        concatbarrios += ", " + barrio.Items[i].ToString().Trim() + " | " + cuitbarrios + " | " + subbarrios;
                    }

                    
                    /*if (barrio.Items.Count > 3)
                        nombarrio += "\nVer listado completo de barrios en la parte de abajo";*/


                    if (benefbarrios)
                        beneficiarios = nombarrio;
                }

                string barrioRecortado = "";

                

                if (barrio.Items.Count > 3 || nombarrio.Length > 250)
                {
                    
                    if (nombarrio.Length > 250)
                    {
                        barrioRecortado = nombarrio.Substring(0, 245) + "...";
                        celdasEncabezado = new PdfPCell(new Phrase("A QUIEN CORRESPONDA \n" + barrioRecortado + "\nVer listado completo de barrios en la parte de abajo", _standardFont));
                    }
                    else
                    {
                        celdasEncabezado = new PdfPCell(new Phrase("A QUIEN CORRESPONDA \n" + nombarrio + "\nVer listado completo de barrios en la parte de abajo", _standardFont));
                    }

                }
                else
                    celdasEncabezado = new PdfPCell(new Phrase("A QUIEN CORRESPONDA \n" + nombarrio , _standardFont));
                celdasEncabezado.Padding = 7;
                celdasEncabezado.Colspan = 3;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaPrimaria.AddCell(celdasEncabezado);

                //fila5
                celdasEncabezado = new PdfPCell(new Phrase("DATOS DEL BENEFICIARIO", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Colspan = 4;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celdasEncabezado);

                //fila6
                celdasEncabezado = new PdfPCell(new Phrase(beneficiarios + "\n" + "Herederos Legales", _standardFont));
                celdasEncabezado.Colspan = 4;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;

                tablaPrimaria.AddCell(celdasEncabezado);

                //fila7
                celdasEncabezado = new PdfPCell(new Phrase("Detalle de Personas a Asegurar", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Colspan = 4;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celdasEncabezado);

                //fila8
                celdasEncabezado = new PdfPCell(new Phrase("Apellido y Nombre", _standardFont));
                celdasEncabezado.Padding = 7;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Tipo y No. Documento", _standardFont));
                celdasEncabezado.Padding = 7;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Fecha Nacimiento (*)", _standardFont));
                celdasEncabezado.Padding = 7;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Actividad \n Tarea que realiza (**)", _standardFont));
                celdasEncabezado.Padding = 7;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celdasEncabezado);

                for (int i = 0; i < dgv.Rows.Count - 1; i++)
                {
                    celdasEncabezado = new PdfPCell(new Phrase(dgv.Rows[i].Cells["apellido"].Value.ToString()+ " " +dgv.Rows[i].Cells["nombre"].Value.ToString(), _standardFont));
                    celdasEncabezado.Padding = 2;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;

                    tablaPrimaria.AddCell(celdasEncabezado);

                    celdasEncabezado = new PdfPCell(new Phrase(dgv.Rows[i].Cells["documento"].Value.ToString() + " | " + dgv.Rows[i].Cells["nodocumento"].Value.ToString(), _standardFont));
                    celdasEncabezado.Padding = 2;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;

                    tablaPrimaria.AddCell(celdasEncabezado);

                    celdasEncabezado = new PdfPCell(new Phrase(dgv.Rows[i].Cells["fecha"].Value.ToString().Substring(0, 10), _standardFont));
                    celdasEncabezado.Padding = 2;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;

                    tablaPrimaria.AddCell(celdasEncabezado);
                    actividades ac = new actividades();
                    celdasEncabezado = new PdfPCell(new Phrase(ac.corregir_nombre(dgv.Rows[i].Cells["actividad"].Value.ToString()), _standardFont));
                    celdasEncabezado.Padding = 2;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;

                    tablaPrimaria.AddCell(celdasEncabezado);
                }


                /*
                    Espacio para la imagen y la fecha de pago
                */
                iTextSharp.text.Image imgPago = iTextSharp.text.Image.GetInstance("imgpago.png");
                imgPago.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                imgPago.ScaleAbsolute(180, 55);
                Paragraph fechPAgo = new Paragraph(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), _standardFontBold);
                fechPAgo.Alignment = Element.ALIGN_CENTER;

                //doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea
                doc.Add(tablaPrimaria);



                Paragraph vigencia = new Paragraph("VIGENCIA :  DEL " +(vigenciaDesde).ToString("dd/MM/yyyy HH:mm:ss") + " A " + vigenciaHasta.ToString("dd/MM/yyyy HH:mm:ss") , _standardFontBold);
                vigencia.Alignment = Element.ALIGN_CENTER;
                doc.Add(vigencia);

                string parr = "Nota: Verificar la exigencia del barrio y la cobertura ya que se dará cobertura a los barrios conforme Suma asegurada mencionada en el presente certificado. Si no adquieres la suma asegurada correcta el barrio puede no dejarte ingresar y tendrás que volver a aumentar la suma asegurada.";
                    
                Paragraph parCobertura = new Paragraph(parr, _standardFontBold1);
                parCobertura.Alignment = Element.ALIGN_LEFT;
                doc.Add(parCobertura);

                parr = "(*) Se aclara que son asegurables personas de 14 a 70 años inclusive.\n" +
                    "(**) Se deja constancia que se dará cobertura a la actividad declarada hasta 15 metros de altura.se deberá cumplir además con el resto de condiciones de asegurabilidad de Sancor Coop.de Seguros Ltda.\n" +
                    "Coberturas y Capitales Asegurados\n" +
                    "Hechos ocurridos a causa de las actividades y/o tareas declaradas en la correspondiente solicitud, exclusivamente cuando las mismas sean desempeñadas por el asegurado o los asegurados en los Barrios Privados declarados, incluido los trayectos para trasladarse de un barrio Privado a otro y/o in itinere.-\n" +
                    "- MUERTE ACCIDENTAL " + string.Format("{0:c}", Math.Round(Convert.ToDouble(cobertura.suma))) + "\n" +
                    "- INVALIDEZ TOTAL Y PARCIAL PERMANENTE POR ACCIDENTE " + string.Format("{0:c}", Math.Round(Convert.ToDouble(cobertura.suma))) + "\n" +
                    "- ASISTENCIA MEDICO FARMACÉUTICA POR REINTEGRO " + string.Format("{0:c}", Math.Round(Convert.ToDouble(cobertura.gastos))) + "(con deducible de " + string.Format("{0:c}", Math.Round(Convert.ToDouble(cobertura.deducible))) + ")"+
                    "\nCobertura in itinere incluyendo casos en que el vehículo de traslado sea motocicletas y/o bicicletas y/o vehículos similares";
                parCobertura = new Paragraph(parr, _standardFont1);
                parCobertura.Alignment = Element.ALIGN_LEFT;
                doc.Add(parCobertura);


           if (norepeticion)
            {
                if(nombarrio.Length < 200)
                {
                    parr = "NO REPETICIÓN ";
                    parCobertura = new Paragraph(parr, _standardFontBold);
                    parCobertura.Alignment = Element.ALIGN_LEFT;
                    doc.Add(parCobertura);

                    parr = "" +
                            "Se deja expresa constancia por medio de este endoso, que formará parte integrante de la póliza / certificado, que Sancor Cooperativas " +
                                "de Seguros Limitada renuncia forma expresa a iniciar toda acción de repetición contra " + nombarrio +
                            " ya sea con fundamentos en la Ley 24.557 o en cualquier otra norma jurídica, con motivo de las prestaciones en especie o dinerarias " +
                            "que se vea obligada a otorgar o abonar al Asegurado declarado en la presente Póliza / Certificado, comprendido en la cobertura de la" +
                            " presente Póliza/ Certificado de Accidentes Personales con motivo de la profesión o actividad declarada e In Itinere." +
                            "\nSe extiende el presente en Benavidez, " + datett.ToString("dd/MM/yyyy") +
                            ". Esta constancia tendrá validez si se presenta con el correspondiente recibo de pago.";

                    parCobertura = new Paragraph(parr, _standardFont1);
                    parCobertura.Alignment = Element.ALIGN_LEFT;
                    doc.Add(parCobertura);
                }
               
            }

                PdfPTable tablaPagado = new PdfPTable(1);//dgv.ColumnCount cantidad de campos del dataGrid
                tablaPagado.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
                tablaPagado.WidthPercentage = 100;
                float[] ancho = new float[] { 100f };
                tablaPagado.SetWidths(ancho);

                PdfPCell _cellPago = new PdfPCell(imgPago);
                _cellPago.Border = 0;
                _cellPago.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPagado.AddCell(_cellPago);
                _cellPago = new PdfPCell(fechPAgo);
                _cellPago.Border = 0;
                _cellPago.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPagado.AddCell(_cellPago);
                /*
                 else {


                     for (int i = 0; i < 8; i++)
                         doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea
                 }*/

                if (pagado)
                {
                    //doc.Add(imgPago);
                    //doc.Add(fechPAgo);
                    tablaPagado.WriteSelectedRows(0,-1,doc.LeftMargin, pw.PageSize.GetBottom(doc.BottomMargin)+60, pw.DirectContent);


                    if(nombarrio.Length > 200)
                    {
                        /*for (int i = 0; i < 4; i++)
                            doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea*/
                    }
                    
                }
                else
                {
                   /* for (int i = 0; i < 12; i++)
                        doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea*/
                }
            


            //doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea

            tablaPrimaria = new PdfPTable(4);//dgv.ColumnCount cantidad de campos del dataGrid
                tablaPrimaria.WidthPercentage = 100;
                widths0 = new float[] { 20f, 30f, 40f, 10f };
                tablaPrimaria.SetWidths(widths0);

                iTextSharp.text.Image logoBroker = iTextSharp.text.Image.GetInstance("brokerlogo.png");
                

                //fila1

                PdfPCell celLogoBroker = new PdfPCell();
                celLogoBroker.Padding = 5;
                celLogoBroker.Border = 0;
                tablaPrimaria.AddCell(celLogoBroker);

                celLogoBroker = new PdfPCell(logoBroker);
                celLogoBroker.Padding = 5;
                celLogoBroker.Border = 0;
                celLogoBroker.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celLogoBroker);

                PdfPCell celInfo = new PdfPCell(new Phrase("BROKER DEL PUERTO ...\nTU TRANQUILIDAD VALE\nwww.brokerdelpuerto.com\nbarriosprivados@brokerdelpuerto.com\nTel. (03327-485189) Cel. 15-55841038\nSarmiento 3314 (1621 - Benavidez)", _standardFont));
                celInfo.Padding = 5;
                celInfo.Border = 0;
                celInfo.PaddingRight = 20;
                celInfo.VerticalAlignment = Element.ALIGN_MIDDLE;
                celInfo.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celInfo);

                
            
                
                

                celLogoBroker = new PdfPCell();
                celLogoBroker.Padding = 5;
                celLogoBroker.Border = 0;
                tablaPrimaria.AddCell(celLogoBroker);

                /*
                    **** Total Páginas ***
                */
                if (concatbarrios.Length > 3330)
                    pw.PageEvent = new headerFooter(3);
                else if(concatbarrios.Length <= 3072 && concatbarrios.Length > 180)
                    pw.PageEvent = new headerFooter(2);
                else
                    pw.PageEvent = new headerFooter(1);


                //****ANEXO de barrios ****///
                if (norepeticion)
                {

                    

                    if (barrio.Items.Count > 3 || nombarrio.Length > 250)
                    {


                        doc.NewPage();

                        iTextSharp.text.Image cabecera = iTextSharp.text.Image.GetInstance("cabeceraanexo.png");
                        Console.WriteLine(cabecera);
                        doc.Add(cabecera);

                        parCobertura = new Paragraph("SEGURO DE ACCIDENTES PERSONALES\nEN OCASIÓN DEL TRABAJO - BARRIOS PRIVADOS", _TituloFont);
                        parCobertura.Alignment = Element.ALIGN_CENTER;
                        doc.Add(parCobertura);

                        doc.Add(vigencia);

                        parr = "PROPUESTA EN EMISIÓN : " + idpropuesta + "\n" +
                            "Se deja expresa constancia por el presente que las personas que se detallan en la P N.  " + idpropuesta + " se encuentran cubiertas en esta aseguradora, amparadas por los riesgos de MUERTE e INVALIDEZ (total o parcial permanente) por ACCIDENTE y Asistencia Médica Farmacéutica según las condiciones contratadas\n\n" +
                            "Destino: Barrios Privados\n" +
                            "ANEXO DE NO REPETICIÓN:\n";


                        parCobertura = new Paragraph(parr, _standardFontBold);
                        parCobertura.Alignment = Element.ALIGN_LEFT;
                        doc.Add(parCobertura);

                        
                        

                        parCobertura = new Paragraph("Se deja expresa constancia por medio de este endoso, que formará parte integrante de la póliza/certificado, que Sancor Cooperativas de  Seguros  Limitada renuncia forma expresa a  iniciar  toda  acción  de repetición  contra: " + concatbarrios + ". ", _standardFont);
                        parCobertura.Alignment = Element.ALIGN_JUSTIFIED;
                        doc.Add(parCobertura);

                        /*if (concatbarrios.Length > 3745 && concatbarrios.Length < 3905)
                        {
                            doc.Add(cabecera);
                            doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea
                        }*/


                        parr = "Ya sea con fundamentos en la Ley 24.557 o en  cualquier otra norma jurídica, con motivo  de las prestaciones en especie o dinerarias que se  vea obligada a otorgar o abonar al Asegurado  declarado en la presente Póliza/Certificado, comprendido en la cobertura de la presente  Póliza/Certificado de Accidentes  Personales  con motivo de  la profesión o  actividad declarada e  In Itinere.";

                        parCobertura = new Paragraph(parr, _standardFont);
                        parCobertura.Alignment = Element.ALIGN_JUSTIFIED;
                        doc.Add(parCobertura);

                        /*for (int i = barrio.Items.Count; i < 20; i++)
                        {
                            doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea
                        }*/

                        //doc.Add(tablaPrimaria);
                    }
                }
                

                doc.Close();
                if (abrir) {
                    Process myProcess = new Process();
                    myProcess.StartInfo.UseShellExecute = true;
                    myProcess.StartInfo.FileName = Environment.CurrentDirectory + @"\" + ruta;
                    
                    
                    myProcess.Start();
                }

                

                //}//fin de la condición para saber si el usuario si guardo el pdf o no


            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        class headerFooter : PdfPageEventHelper
        {
            decimal totalPage = 1;
            public headerFooter(decimal val)
            {
                this.totalPage = val;
            }
            public override void OnEndPage(PdfWriter writer, Document doc)
            {

                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                PdfPTable tablaPrimaria = new PdfPTable(4);//dgv.ColumnCount cantidad de campos del dataGrid
                tablaPrimaria.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
                tablaPrimaria.WidthPercentage = 100;
                float[] widths0 = new float[] { 20f, 30f, 40f, 10f };
                tablaPrimaria.SetWidths(widths0);

                iTextSharp.text.Image logoBroker = iTextSharp.text.Image.GetInstance("brokerlogo.png");


                //fila1

                PdfPCell celLogoBroker = new PdfPCell();
                celLogoBroker.Padding = 5;
                celLogoBroker.Border = 0;
                tablaPrimaria.AddCell(celLogoBroker);

                celLogoBroker = new PdfPCell(logoBroker);
                celLogoBroker.Padding = 5;
                celLogoBroker.Border = 0;
                celLogoBroker.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celLogoBroker);

                PdfPCell celInfo = new PdfPCell(new Phrase("BROKER DEL PUERTO ...\nTU TRANQUILIDAD VALE\nwww.brokerdelpuerto.com\nbarriosprivados@brokerdelpuerto.com\nTel. (03327-485189) Cel. 15-55841038\nSarmiento 3314 (1621 - Benavidez)", _standardFont));
                celInfo.Padding = 5;
                celInfo.Border = 0;
                celInfo.PaddingRight = 20;
                celInfo.VerticalAlignment = Element.ALIGN_MIDDLE;
                celInfo.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaPrimaria.AddCell(celInfo);

                celLogoBroker = new PdfPCell(new Phrase("Página "+writer.PageNumber+"/"+ this.totalPage, _standardFont));
                //celLogoBroker = new PdfPCell();
                celLogoBroker.Padding = 5;
                celLogoBroker.VerticalAlignment = Element.ALIGN_BOTTOM;
                celLogoBroker.Border = 0;
                tablaPrimaria.AddCell(celLogoBroker);

                tablaPrimaria.WriteSelectedRows(0,-1,doc.LeftMargin,writer.PageSize.GetBottom(doc.BottomMargin), writer.DirectContent);
            }
        }

        public void pdfRecibos(string idpropuesta, clientes tomador, DataGridView dgv, string vigenciaDesde, string vigenciaHasta, string barrio, coberturas cobertura, double premioTotal, bool abrir = true, bool pagado = false, string path = @"RecibosEmitidos")
        {
            try { 

                DateTime datett = DateTime.Now;
                Document doc = new Document(PageSize.LETTER);
                string ruta = path + @"\ReciboPropuesta-No"+ idpropuesta + ".pdf";
                string[] prefIdPropuesta = idpropuesta.Split('-');
                PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.OpenOrCreate));
                propuestas proCons = new propuestas();
                DataSet dsCosPro = new DataSet();

                if(prefIdPropuesta.Count() > 0)
                {
                    dsCosPro = proCons.getprefijo(prefIdPropuesta[0].Trim(), prefIdPropuesta[1].Trim());
                }

                string organizador_ = "150430";
                string productor_ = "212376";

                if (dsCosPro.Tables.Count > 0)
                {
                    organizador_ = dsCosPro.Tables[0].Rows[0]["organizador"].ToString();
                    productor_ = dsCosPro.Tables[0].Rows[0]["productor"].ToString();
                }

                doc.Open();

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("imgsancor1.png");
                jpg.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
                // Creamos el tipo de Font que vamos utilizar
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _TituloFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLDITALIC, BaseColor.BLACK);
                iTextSharp.text.Font _SubTituloFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _encabezadosTabla = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _textPago = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24, iTextSharp.text.Font.BOLD, BaseColor.RED);

                /****************************************************/
                PdfPTable tablaGeneral = new PdfPTable(2);//dgv.ColumnCount cantidad de campos del dataGrid
                tablaGeneral.WidthPercentage = 100;
                tablaGeneral.HorizontalAlignment = Element.ALIGN_CENTER;

                float[] widths0 = new float[] { 50f, 50f };
                tablaGeneral.SetWidths(widths0);
            
                /****************************************************/

                PdfPTable tablaPrimaria = new PdfPTable(6);//dgv.ColumnCount cantidad de campos del dataGrid
                tablaPrimaria.WidthPercentage = 95;
                tablaPrimaria.HorizontalAlignment = Element.ALIGN_CENTER;
            
                widths0 = new float[] { 15f, 10f, 20f, 20f, 15f, 20f };
                tablaPrimaria.SetWidths(widths0);



                //fila1

                PdfPCell clLogo = new PdfPCell(jpg);
                clLogo.Padding = 5;
                clLogo.Colspan = 3;
                clLogo.Border = 0;

                tablaPrimaria.AddCell(clLogo);

                PdfPCell celdasEncabezado = new PdfPCell(new Phrase("N°: " + idpropuesta+"\nAccidentes Personales", _encabezadosTabla));
                celdasEncabezado.Colspan = 3;
                celdasEncabezado.Padding = 10;
                celdasEncabezado.Border = 0;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;

            
                tablaPrimaria.AddCell(celdasEncabezado);

                doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea
                //fila2
                celdasEncabezado = new PdfPCell(new Phrase("Ramo", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Prod", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Referencia", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("No.Poliza", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Certif.", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Propuesta", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);


                //fila3
                celdasEncabezado = new PdfPCell(new Phrase("600", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("27", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("en trámite", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("en trámite", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("0", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(idpropuesta, _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea

                //fila4
                celdasEncabezado = new PdfPCell(new Phrase("Organización", _encabezadosTabla));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Productor", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Cliente", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Asociado", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);


                //fila5

                celdasEncabezado = new PdfPCell(new Phrase(organizador_, _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(productor_, _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(tomador.tipo_id +" "+tomador.id, _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("0", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea

                //fila6
                celdasEncabezado = new PdfPCell(new Phrase("Sr/es         : " + tomador.apellidos +", "+tomador.nombres+"\nDomicilio   : "+ tomador.direccion+"\nLocalidad  : "+ tomador.codpostal + " - " + tomador.localidad+"\n\n", _standardFont));
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Colspan = 6;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaPrimaria.AddCell(celdasEncabezado);

                doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea

                //fila7
                celdasEncabezado = new PdfPCell(new Phrase("Vencimiento", _encabezadosTabla));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Cuota", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("", _encabezadosTabla));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.Border = 0;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Importe", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                //fila8
                celdasEncabezado = new PdfPCell(new Phrase(vigenciaHasta, _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("1/1", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("", _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.Border = 0;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(string.Format("{0:c}", premioTotal), _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                iTextSharp.text.Image imgPago = iTextSharp.text.Image.GetInstance("imgpago.png");
                imgPago.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                imgPago.ScaleAbsolute(180, 55);

                PdfPCell celImgPago = new PdfPCell(imgPago);
                celImgPago.Border = 0;
                celImgPago.Colspan = 6;
                celImgPago.HorizontalAlignment = Element.ALIGN_CENTER;
            
                
                    PdfPCell fila1General = new PdfPCell();
                    fila1General.PaddingBottom = 100;
                    PdfPCell fechPAgo = new PdfPCell(new Phrase(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), _standardFont));
                    fechPAgo.Border = 0;
                    fechPAgo.HorizontalAlignment = Element.ALIGN_CENTER;
                    fechPAgo.Colspan = 6;
                    if (pagado)
                    {
                    
                        tablaPrimaria.AddCell(celImgPago);
                        fila1General.PaddingBottom = 20;
                        tablaPrimaria.AddCell(fechPAgo);
                    }
                
                    fila1General.AddElement(tablaPrimaria);
                    tablaGeneral.AddCell(fila1General);







                    doc.Add(tablaGeneral);


                tablaPrimaria = new PdfPTable(6);//dgv.ColumnCount cantidad de campos del dataGrid
                tablaPrimaria.WidthPercentage = 95;
                tablaPrimaria.HorizontalAlignment = Element.ALIGN_CENTER;

                widths0 = new float[] { 15f, 10f, 20f, 20f, 15f, 20f };
                tablaPrimaria.SetWidths(widths0);

                //fila1

                clLogo = new PdfPCell(jpg);
                clLogo.Padding = 5;
                clLogo.Colspan = 3;
                clLogo.Border = 0;

                tablaPrimaria.AddCell(clLogo);

                celdasEncabezado = new PdfPCell(new Phrase("N°: " + idpropuesta + "\nAccidentes Personales", _encabezadosTabla));
                celdasEncabezado.Colspan = 3;
                celdasEncabezado.Padding = 10;
                celdasEncabezado.Border = 0;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;


                tablaPrimaria.AddCell(celdasEncabezado);

                doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea
                //fila2
                celdasEncabezado = new PdfPCell(new Phrase("Ramo", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Prod", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Referencia", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("No.Poliza", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Certif.", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Propuesta", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);


                //fila3
                celdasEncabezado = new PdfPCell(new Phrase("600", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("13", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("en trámite", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("en trámite", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("0", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(idpropuesta, _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea

                //fila4
                celdasEncabezado = new PdfPCell(new Phrase("Organización", _encabezadosTabla));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Productor", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Cliente", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Asociado", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);


                //fila5

                celdasEncabezado = new PdfPCell(new Phrase(organizador_, _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(productor_, _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(tomador.tipo_id + " " + tomador.id, _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("0", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea

                //fila6
                celdasEncabezado = new PdfPCell(new Phrase("Sr/es         : " + tomador.apellidos + ", " + tomador.nombres + "\nDomicilio   : " + tomador.direccion + "\nLocalidad  : " + tomador.codpostal + " - " + tomador.localidad + "\n\n", _standardFont));
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Colspan = 6;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaPrimaria.AddCell(celdasEncabezado);

                doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea

                //fila7
                celdasEncabezado = new PdfPCell(new Phrase("Vencimiento", _encabezadosTabla));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Cuota", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("", _encabezadosTabla));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.Border = 0;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Importe", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                //fila8
                celdasEncabezado = new PdfPCell(new Phrase(vigenciaHasta, _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("1/1", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("", _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.Border = 0;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(string.Format("{0:c}", premioTotal), _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);


                fila1General = new PdfPCell();
                fila1General.PaddingBottom = 100;
                    if (pagado)
                    {

                        tablaPrimaria.AddCell(celImgPago);
                        fila1General.PaddingBottom = 20;
                        tablaPrimaria.AddCell(fechPAgo);

                    }

                    fila1General.AddElement(tablaPrimaria);
                tablaGeneral.AddCell(fila1General);

                doc.Add(tablaGeneral);



                /****************************************************/
                tablaGeneral = new PdfPTable(2);//dgv.ColumnCount cantidad de campos del dataGrid
                tablaGeneral.WidthPercentage = 100;
                tablaGeneral.HorizontalAlignment = Element.ALIGN_CENTER;

                widths0 = new float[] { 50f, 50f};
                tablaGeneral.SetWidths(widths0);


                /****************************************************/

                tablaPrimaria = new PdfPTable(12);//dgv.ColumnCount cantidad de campos del dataGrid
                tablaPrimaria.WidthPercentage = 100;
                tablaPrimaria.HorizontalAlignment = Element.ALIGN_CENTER;

                widths0 = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                tablaPrimaria.SetWidths(widths0);


                clLogo = new PdfPCell(jpg);
                clLogo.Padding = 5;
                clLogo.Colspan = 2;
                clLogo.Border = 0;
                clLogo.VerticalAlignment = Element.ALIGN_MIDDLE;
                clLogo.HorizontalAlignment = Element.ALIGN_LEFT;

                tablaPrimaria.AddCell(clLogo);

                string infoEmpresa = "CASA CENTRAL Ruta 34 km. 257\n" +
                                    "Tel. (03493) 428500 (Alternativo 420151)\n" +
                                    "FAX(03492) 490979\n" +
                                    "2322 - SUNCHALES(Sta.Fe)";

                string datosCuitEmpresa =   "C.U.I.T N° 30-50004946-0\n" +
                                            "Ingresos Brutos: C.M. 921-740719-3\n" +
                                            "Caja Previsión N°: 50004946";

                celdasEncabezado = new PdfPCell(new Phrase(infoEmpresa, _standardFont));
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Colspan = 4;
                celdasEncabezado.Border = 0;
                celdasEncabezado.BorderWidthRight = 0.5f;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(datosCuitEmpresa, _standardFont));
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Colspan = 4;
                celdasEncabezado.Border = 0;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("N°: " + idpropuesta+ "\n\nAccidentes Personales", _standardFontBold));
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.Border = 0;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;

                tablaPrimaria.AddCell(celdasEncabezado);


                //fila2
                celdasEncabezado = new PdfPCell(new Phrase("Ramo", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Prod.", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Referencia.", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("No.Poliza.", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);


                celdasEncabezado = new PdfPCell(new Phrase("Certif.", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Propuesta", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Organización", _encabezadosTabla));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Producto", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Cliente", _encabezadosTabla));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Asociado", _encabezadosTabla));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);


                //fila3
                celdasEncabezado = new PdfPCell(new Phrase("600", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("13", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("en trámite", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("en trámite", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("0", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(idpropuesta, _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(organizador_, _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(productor_, _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(tomador.tipo_id + " " + tomador.id, _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("0", _standardFont));
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);


                //fila4
                celdasEncabezado = new PdfPCell(new Phrase("Sr/es         : " + tomador.apellidos + ", " + tomador.nombres + "\nDomicilio   : " + tomador.direccion + "\nLocalidad  : " + tomador.codpostal + " - "+ tomador.localidad + "\nProvincia   : " + tomador.ciudad + "\n\n", _standardFont));
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Colspan = 12;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaPrimaria.AddCell(celdasEncabezado);

                doc.Add(Chunk.NEWLINE);//espacio de línea o salto de línea

                //fila5
                celdasEncabezado = new PdfPCell(new Phrase("No.Cuota", _encabezadosTabla));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Cant.Cuotas", _encabezadosTabla));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Vencimiento", _encabezadosTabla));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);


                celdasEncabezado = new PdfPCell(new Phrase("", _encabezadosTabla));
                celdasEncabezado.Colspan = 4;
                celdasEncabezado.Border = 0;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("Importe", _encabezadosTabla));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                //fila6
                celdasEncabezado = new PdfPCell(new Phrase("1", _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("1", _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(vigenciaHasta, _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("", _standardFont));
                celdasEncabezado.Colspan = 4;
                celdasEncabezado.Border = 0;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(string.Format("{0:c}", premioTotal), _standardFont));
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);


                fila1General = new PdfPCell();
                fila1General.Colspan = 2;
                fila1General.PaddingBottom = 100;
                    if (pagado)
                    {
                        celImgPago.Colspan = 12;
                        tablaPrimaria.AddCell(celImgPago);
                        fila1General.PaddingBottom = 30;
                        fechPAgo.Colspan = 12;
                        tablaPrimaria.AddCell(fechPAgo);

                    }
                    fila1General.AddElement(tablaPrimaria);
                tablaGeneral.AddCell(fila1General);

                doc.Add(tablaGeneral);

            

                    doc.Close();

                
                    if (abrir)
                    {
                        Process myProcess = new Process();
                        myProcess.StartInfo.UseShellExecute = true;
                        myProcess.StartInfo.FileName = Environment.CurrentDirectory + @"\" + ruta;
                        myProcess.Start();
                    }
                    

                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        public void pdfLiquidacion(DataGridView dgv, DateTime fec1, DateTime fec2, double totalprima,
            double totalpremio, double comprima, double compremio, liquidaciones liq, string path, string usuario, string fecharecibo)
        {
            try
            {

                Document doc = new Document(PageSize.LETTER);
                
                File.Delete(path);
                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.OpenOrCreate));
                doc.Open();

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("imgsancor1.png");
                jpg.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
                // Creamos el tipo de Font que vamos utilizar
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _TituloFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLDITALIC, BaseColor.BLACK);
                iTextSharp.text.Font _SubTituloFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _encabezadosTabla = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _textPago = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24, iTextSharp.text.Font.BOLD, BaseColor.RED);

                Configmaster conf = new Configmaster();

                

                

                


                /****************************************************/

                PdfPTable tablaPrimaria = new PdfPTable(9);//dgv.ColumnCount cantidad de campos del dataGrid
                tablaPrimaria.WidthPercentage = 95;
                tablaPrimaria.HorizontalAlignment = Element.ALIGN_CENTER;

                float[] widths0 = new float[] { 16f, 10f, 15f, 15f, 15f, 13f, 13f, 15f, 15f };
                tablaPrimaria.SetWidths(widths0);

                PdfPCell clLogo = new PdfPCell(jpg);
                clLogo.Padding = 5;
                clLogo.Colspan = 9;
                clLogo.Border = 0;
                clLogo.VerticalAlignment = Element.ALIGN_MIDDLE;
                clLogo.HorizontalAlignment = Element.ALIGN_LEFT;

                tablaPrimaria.AddCell(clLogo);

                usuarios usu = new usuarios();

                Paragraph parr = new Paragraph("MASTER : " + conf.get("MASTER").Tables[0].Rows[0]["nombre"].ToString() + "\n" +
                    "ORGANIZADOR : " + usu.get_codorganizador(usuario) + "\n" +
                    "PRODUCTOR : "+ usu.get_codproductor(usuario), _SubTituloFont);
                parr.Alignment = Element.ALIGN_LEFT;

                PdfPCell clmaster = new PdfPCell(parr);
                clmaster.Padding = 5;
                clmaster.Colspan = 5;
                clmaster.Border = 0;
                clmaster.VerticalAlignment = Element.ALIGN_MIDDLE;
                clmaster.HorizontalAlignment = Element.ALIGN_LEFT;

                tablaPrimaria.AddCell(clmaster);

                parr = new Paragraph("Fecha: "+fecharecibo+" \nRANGO DE FECHA \nDESDE " + fec1.ToString("dd/MM/yyyy")+ " HASTA "+ fec2.ToString("dd/MM/yyyy")+ "\n", _SubTituloFont);
                parr.Alignment = Element.ALIGN_LEFT;

                clmaster = new PdfPCell(parr);
                clmaster.Padding = 5;
                clmaster.Colspan = 4;
                clmaster.Border = 0;
                clmaster.VerticalAlignment = Element.ALIGN_MIDDLE;
                clmaster.HorizontalAlignment = Element.ALIGN_LEFT;

                tablaPrimaria.AddCell(clmaster);

                //ENCABEZADO

                PdfPCell celdasEncabezado = new PdfPCell(new Phrase("PROPUESTA", _standardFont));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Border = 1;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("REF.", _standardFont));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Border = 1;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("TOMADOR", _standardFont));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Border = 1;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("PRIMA", _standardFont));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Border = 1;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("PREMIO", _standardFont));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Border = 1;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("%PRIMA", _standardFont));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Border = 1;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("%PREMIO", _standardFont));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Border = 1;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("COM PRIMA", _standardFont));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Border = 1;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase("COM PREMIO", _standardFont));
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Border = 1;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaPrimaria.AddCell(celdasEncabezado);

                for(int i = 0; i < dgv.Rows.Count; i++)
                {
                    celdasEncabezado = new PdfPCell(new Phrase(dgv.Rows[i].Cells["prefijo"].Value.ToString() + dgv.Rows[i].Cells["propuesta"].Value.ToString(), _standardFont));
                    celdasEncabezado.Border = 1;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                    celdasEncabezado.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaPrimaria.AddCell(celdasEncabezado);

                    celdasEncabezado = new PdfPCell(new Phrase(dgv.Rows[i].Cells["referencia"].Value.ToString(), _standardFont));
                    celdasEncabezado.Border = 1;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_CENTER;
                    celdasEncabezado.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaPrimaria.AddCell(celdasEncabezado);

                    celdasEncabezado = new PdfPCell(new Phrase(dgv.Rows[i].Cells["tomador"].Value.ToString(), _standardFont));
                    celdasEncabezado.Border = 1;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;
                    celdasEncabezado.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaPrimaria.AddCell(celdasEncabezado);

                    celdasEncabezado = new PdfPCell(new Phrase(String.Format("{0:C}", Convert.ToDouble(dgv.Rows[i].Cells["prima"].Value)), _standardFont));
                    celdasEncabezado.Border = 1;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;
                    celdasEncabezado.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaPrimaria.AddCell(celdasEncabezado);

                    celdasEncabezado = new PdfPCell(new Phrase(String.Format("{0:C}", Convert.ToDouble(dgv.Rows[i].Cells["premio"].Value)), _standardFont));
                    celdasEncabezado.Border = 1;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;
                    celdasEncabezado.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaPrimaria.AddCell(celdasEncabezado);

                    celdasEncabezado = new PdfPCell(new Phrase(dgv.Rows[i].Cells["porcprima"].Value.ToString(), _standardFont));
                    celdasEncabezado.Border = 1;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;
                    celdasEncabezado.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaPrimaria.AddCell(celdasEncabezado);

                    celdasEncabezado = new PdfPCell(new Phrase(dgv.Rows[i].Cells["porpremio"].Value.ToString(), _standardFont));
                    celdasEncabezado.Border = 1;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;
                    celdasEncabezado.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaPrimaria.AddCell(celdasEncabezado);

                    celdasEncabezado = new PdfPCell(new Phrase(String.Format("{0:C}", Convert.ToDouble(dgv.Rows[i].Cells["comprima"].Value)), _standardFont));
                    celdasEncabezado.Border = 1;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;
                    celdasEncabezado.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaPrimaria.AddCell(celdasEncabezado);

                    celdasEncabezado = new PdfPCell(new Phrase(String.Format("{0:C}", Convert.ToDouble(dgv.Rows[i].Cells["compremio"].Value)), _standardFont));
                    celdasEncabezado.Border = 1;
                    celdasEncabezado.BorderWidthBottom = 0.5f;
                    celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;
                    celdasEncabezado.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaPrimaria.AddCell(celdasEncabezado);

                }

                parr = new Paragraph("TOTALES => ", _standardFont);
                celdasEncabezado = new PdfPCell(parr);
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.Colspan = 3;
                celdasEncabezado.BorderWidthBottom = 0.5f;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(String.Format("{0:C}", Convert.ToDouble(totalprima)), _standardFont));
                celdasEncabezado.Colspan = 1;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.BorderWidthBottom = 0.5f;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(String.Format("{0:C}", Convert.ToDouble(totalpremio)), _standardFont));
                celdasEncabezado.Colspan = 1;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.BorderWidthBottom = 0.5f;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaPrimaria.AddCell(celdasEncabezado);

                parr = new Paragraph("", _standardFont);
                celdasEncabezado = new PdfPCell(parr);
                celdasEncabezado.Colspan = 2;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.BorderWidthBottom = 0.5f;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(String.Format("{0:C}", Convert.ToDouble(comprima)), _standardFont));
                celdasEncabezado.Colspan = 1;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.BorderWidthBottom = 0.5f;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaPrimaria.AddCell(celdasEncabezado);

                celdasEncabezado = new PdfPCell(new Phrase(String.Format("{0:C}", Convert.ToDouble(compremio)), _standardFont));
                celdasEncabezado.Colspan = 1;
                celdasEncabezado.BackgroundColor = new iTextSharp.text.BaseColor(192, 192, 192);
                celdasEncabezado.Padding = 5;
                celdasEncabezado.BorderWidthBottom = 0.5f;
                celdasEncabezado.HorizontalAlignment = Element.ALIGN_RIGHT;
                tablaPrimaria.AddCell(celdasEncabezado);

                doc.Add(tablaPrimaria);


                parr = new Paragraph("\n\n______________________________               ______________________________", _standardFont);
                parr.Alignment = Element.ALIGN_CENTER;
                doc.Add(parr);

                parr = new Paragraph("FIRMA RECIBIDO                                        FIRMA LIQUIDADOR", _standardFont);
                parr.Alignment = Element.ALIGN_CENTER;
                doc.Add(parr);




                doc.Close();
                Process.Start(path);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        public void pdfCertificadoLibreDeuda(string path, string texto, string empresa, string departamento, string responsable, DataSet dsp, int cantclientes = 1)
        {
            /*try
            {*/
                string[] arregoanos = { "", "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };

                Document doc = new Document(PageSize.LETTER);

                File.Delete(path);
                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.OpenOrCreate));
                doc.Open();

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("sancorcerlibre.png");
                jpg.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
                jpg.ScaleAbsolute(250f, 50f);
                // Creamos el tipo de Font que vamos utilizar
                iTextSharp.text.Font _TituloFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font _standardFontM = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            /****************************************************/

            PdfPTable tablaPrimaria = new PdfPTable(9);//dgv.ColumnCount cantidad de campos del dataGrid
                tablaPrimaria.WidthPercentage = 95;
                tablaPrimaria.HorizontalAlignment = Element.ALIGN_CENTER;

                float[] widths0 = new float[] { 16f, 10f, 15f, 15f, 15f, 13f, 13f, 15f, 15f };
                tablaPrimaria.SetWidths(widths0);

                PdfPCell clLogo = new PdfPCell(jpg);
                clLogo.Padding = 5;
                clLogo.Colspan = 9;
                clLogo.Border = 0;
                clLogo.BorderWidthBottom = 0.5f;
                clLogo.VerticalAlignment = Element.ALIGN_MIDDLE;
                clLogo.HorizontalAlignment = Element.ALIGN_LEFT;

                tablaPrimaria.AddCell(clLogo);

                doc.Add(tablaPrimaria);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                
                Paragraph parr = new Paragraph("\n\n\n\n\n\nCERTIFICADO DE LIBRE DEUDA", _TituloFont);

                if (cantclientes > 5)
                    parr = new Paragraph("\nCERTIFICADO DE LIBRE DEUDA", _TituloFont);

                parr.Alignment = Element.ALIGN_LEFT;
                doc.Add(parr);
                DateTime dd = DateTime.Now;

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                parr = new Paragraph("Buenos Aires, "+ dd.ToString("dd")+" "+ arregoanos[Convert.ToInt16(dd.ToString("MM"))]+ " de "+dd.ToString("yyyy")+".", _standardFont);
                parr.Alignment = Element.ALIGN_RIGHT;
                doc.Add(parr);
                doc.Add(Chunk.NEWLINE);

                parr = new Paragraph(texto, _standardFont);
                parr.Alignment = Element.ALIGN_LEFT;
                doc.Add(parr);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);


                iTextSharp.text.Image imgPago = iTextSharp.text.Image.GetInstance("imgpago.png");
                imgPago.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
                imgPago.ScaleAbsolute(180, 55);

                doc.Add(imgPago);

                iTextSharp.text.Image firma = iTextSharp.text.Image.GetInstance("firmaCeLibre.png");
                firma.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
                firma.ScaleAbsoluteWidth(120f);
                firma.ScaleAbsoluteHeight(50f);

                doc.Add(firma);

                parr = new Paragraph(empresa+"\n"+departamento+"\n"+responsable, _standardFontM);
                parr.Alignment = Element.ALIGN_RIGHT;
                doc.Add(parr);

                parr = new Paragraph("Organizador : "+dsp.Tables[0].Rows[0]["organizador"].ToString()+"\n"+ "Productor : " + dsp.Tables[0].Rows[0]["productor"].ToString(), _standardFontM);
                parr.Alignment = Element.ALIGN_LEFT;
                doc.Add(parr);

                doc.Close();
                Process.Start(path);

            /*}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/




        }
    }
}
