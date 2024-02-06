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
    public partial class MDIParent1 : Form
    {
        public static string sesionUser { get; set; } = string.Empty;
        public static string server500 { get; set; } = string.Empty;
        public static string codempresa { get; set; } = string.Empty;
        public static string baseDatos { get; set; } = string.Empty;
        public static string rolPuntodeventa { get; set; } = string.Empty;
        public static string versionwindows { get; set; } = string.Empty;
        public static string versionsistema { get; set; } = "5.6";
        DateTime flagtimer = DateTime.Now;
        configuraciones confiprosimport = new configuraciones();

        public static string apiversion { get; set; } = string.Empty;
        public static string prefijo { get; set; } = string.Empty;
        public static bool adminsitio { get; set; } = false;
        public static bool prosimport { get; set; } = false;
        public static bool installing { get; set; } = false;
        public static bool prosMigracion { get; set; } = false;

        public static bool prosimportNocierre { get; set; } = false;
        public static string apiuri { get; } = "https://barriosprivados.niveldigitalcol.com"; //http://apibarrios.3enterprise.online
        public static DateTime? importUpdate { get; set; } = null;

        public static string rutaInformes_global { get; set; } = string.Empty;
        public bool formabierto = false;
        private int childFormNumber = 0;
        

        public MDIParent1()
        {
            InitializeComponent();
        }

        

        public void establecerPerfilUsuario(string loggin)
        {
            usuarios user = new usuarios();
            user.loggin = loggin;
            DataSet ds =  user.get();

            perfiles per = new perfiles();
            per.nombre = ds.Tables[0].Rows[0]["perfil"].ToString();
            ds = per.get();
            bool primeringreso = false;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["vista"] != null && ds.Tables[0].Rows[i]["vista"].ToString() != "")
                {
                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "administrador")
                    {

                        //if (Convert.ToBoolean(ds.Tables[0].Rows[i]["access"]))
                        //{

                            MDadministracion.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["access"]);
                            configuracionesToolStripMenuItem.Visible = true;

                            perfiles pp = new perfiles();
                            pp.nombre = per.nombre;
                            pp.modulo = "usuarios";
                            pp.access = 0;
                            pp.vista = "1";
                            pp.edicion = "1";
                            pp.eliminar = "1";
                            pp.save();

                            pp = new perfiles();
                            pp.nombre = per.nombre;
                            pp.modulo = "perfiles";
                            pp.access = 0;
                            pp.vista = "1";
                            pp.edicion = "1";
                            pp.eliminar = "1";
                            pp.save();

                            pp = new perfiles();
                            pp.nombre = per.nombre;
                            pp.modulo = "coberturas";
                            pp.access = 0;
                            pp.vista = "1";
                            pp.edicion = "1";
                            pp.eliminar = "1";
                            pp.save();

                            pp = new perfiles();
                            pp.nombre = per.nombre;
                            pp.modulo = "actividades";
                            pp.access = 0;
                            pp.vista = "1";
                            pp.edicion = "1";
                            pp.eliminar = "1";
                            pp.save();

                            pp = new perfiles();
                            pp.nombre = per.nombre;
                            pp.modulo = "clasificaciones";
                            pp.access = 0;
                            pp.vista = "1";
                            pp.edicion = "1";
                            pp.eliminar = "1";
                            pp.save();

                            pp = new perfiles();
                            pp.nombre = per.nombre;
                            pp.delete_modulo("administrador");

                            coberturasToolStripMenuItem.Visible = true;
                            actividadesToolStripMenuItem.Visible = true;
                            clasificacionesToolStripMenuItem.Visible = true;
                        //}

                        primeringreso = true;

                        
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "usuarios")
                    {
                        MDadministracion.Visible = true;
                        MDusuarios.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                    }
                        
                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "perfiles")
                    {
                        MDadministracion.Visible = true;
                        MDperfiles.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                    }
                        
                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "coberturas")
                    {
                        if (Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]))
                        {
                            MDadministracion.Visible = true;
                            configuracionesToolStripMenuItem.Visible = true;
                            coberturasToolStripMenuItem.Visible = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "migraciones")
                    {
                        if (Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]))
                        {
                            /*MDadministracion.Visible = true;
                            configuracionesToolStripMenuItem.Visible = true;
                            migracionesToolStripMenuItem.Visible = true;*/
                        }
                    }


                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "puntodeventa")
                    {
                        if (Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]))
                        {
                            MDadministracion.Visible = true;
                            puntoDeVentaToolStripMenuItem.Visible = true;
                            configuraciónPropiaToolStripMenuItem.Visible = true;
                            puntodeventa punt = new puntodeventa();

                            /*if (punt.get_principal())
                            {*/
                                nuevoPuntoDeVentaToolStripMenuItem.Visible = true;
                            //}
                        }
                    }

                    

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "actividades")
                    {
                        if (Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]))
                        {
                            MDadministracion.Visible = true;
                            configuracionesToolStripMenuItem.Visible = true;
                            actividadesToolStripMenuItem.Visible = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "clasificaciones")
                    {
                        if (Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]))
                        {
                            MDadministracion.Visible = true;
                            configuracionesToolStripMenuItem.Visible = true;
                            clasificacionesToolStripMenuItem.Visible = true;
                        }
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "festivos")
                    {
                        if (Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]))
                        {
                            MDadministracion.Visible = true;
                            configuracionesToolStripMenuItem.Visible = true;
                            festivosToolStripMenuItem.Visible = true;
                        }
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "confcorreo")
                    {
                        if (Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]))
                        {
                            MDadministracion.Visible = true;
                            configuracionesToolStripMenuItem.Visible = true;
                            sMTPCorreoToolStripMenuItem.Visible = true;
                        }
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "operaciones")
                        MDoperaciones.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["access"]);


                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "clientes")
                    {
                        MDclientes.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                        MDoperaciones.Visible = true;
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "barrios")
                    {
                        MDbarrios.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                        MDoperaciones.Visible = true;
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "propuestas")
                    {
                        MDpropuestas.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                        MDoperaciones.Visible = true;
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "cierrecajas")
                    {
                        arqueosToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                        cajaToolStripMenuItem.Visible = true;
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "cobranzas_envios")
                    {
                        cobranzasToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                        importarDatosToolStripMenuItem.Visible = true;
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "rendiciones")
                    {
                        rendicionesToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                        cajaToolStripMenuItem.Visible = true;
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "nuevacomision")
                    {
                        nuevaToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                        comisionesToolStripMenuItem.Visible = true;
                        cajaToolStripMenuItem.Visible = true;
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "listacomision")
                    {
                        listaLiquidacionesToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                        comisionesToolStripMenuItem.Visible = true;
                        cajaToolStripMenuItem.Visible = true;
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "informes")
                        MDinformes.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "informe_findia")
                    {
                        MDinformes.Visible = true;
                        informeFinalDelDíaToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                        generarArchivoToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "auditoriabarrios")
                    {
                        MDinformes.Visible = true;
                        auditoríaToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                        modificaciónDeBarriosToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "mail_findeldia")
                    {
                        MDinformes.Visible = true;
                        informeFinalDelDíaToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                        enviarMailToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "combinar_informes")
                    {
                        MDinformes.Visible = true;
                        agrupaciónDeInformesToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "controlventas_nuevo")
                    {
                        MDinformes.Visible = true;
                        controlDeVentasToolStripMenuItem.Visible = true;
                        nuevoArchivoToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                    }
                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "controlventas_informe")
                    {
                        MDinformes.Visible = true;
                        controlDeVentasToolStripMenuItem.Visible = true;
                        verYCompararToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "ventascredito")
                    {
                        MDinformes.Visible = true;
                        ventasCréditoToolStripMenuItem.Visible = true;
                        verYCompararToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                    }

                    if (ds.Tables[0].Rows[i]["modulo"].ToString() == "imputaciones")
                    {
                        MDinformes.Visible = true;
                        imputacionesToolStripMenuItem.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["vista"]);
                    }
                }
            }

            if (primeringreso)
            {
                MDperfiles.Visible = Convert.ToBoolean(1);
                migracionesToolStripMenuItem.Visible = true;
                MessageBox.Show("Es posible que no aparezcan todos los módulos que necesita el administrador, por favor configure los accesos en el módulo de perfiles y reinicie el sistema", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientes frm = new frmClientes();
            frm.Show();
            frm.StartPosition = FormStartPosition.CenterScreen;
        }

        private void barriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBarrios frm = new frmBarrios();
            frm.Show();
            frm.StartPosition = FormStartPosition.CenterScreen;
        }


        private void fase3a()
        {
            Configmaster coonf = new Configmaster(true);
            codempresa = coonf.get_codempresa();

            perfiles p = new perfiles(true);
            usuarios u = new usuarios(true);
            clientes cl = new clientes(true);
            propuestas pr = new propuestas(true);
            lineas_propuestas l = new lineas_propuestas(true);
            lineas_propuestas_aux l_aux = new lineas_propuestas_aux(true);
            barrios_propuesta bar = new barrios_propuesta(true);
            barrios ba = new barrios(true);
            gruposbarrios gb = new gruposbarrios(true);
            provincias prov = new provincias(true);
            coberturas cob = new coberturas(true);
            actividades act = new actividades(true);
            clasificaciones cla = new clasificaciones(true);
            arqueos arrq = new arqueos(true);
            AuditoriaBarrios aud = new AuditoriaBarrios(true);
        }

        private void faseCobranzas()
        {
            cobranzas.bancosaseguradora banAs = new cobranzas.bancosaseguradora(true);
            ReferenceCloud refc = new ReferenceCloud(true);
            RegisterPending repen = new RegisterPending(true);
        }

        private string funti()
        {
            // USANDO REGISTRO DE WINDOWS
            string Clave1 = @"SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion";
            string Clave2 = @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment";
            //
            Microsoft.Win32.RegistryKey principal = Microsoft.Win32.Registry.LocalMachine; //rama LocalMachine
            Microsoft.Win32.RegistryKey subclave1 = principal.OpenSubKey(Clave1); //clave SOFTWARE ... CurrentVersion
            Microsoft.Win32.RegistryKey subclave2 = principal.OpenSubKey(Clave2); //clave SYSTEM ...  Environment

            
            string nombre = subclave1.GetValue("ProductName").ToString();
            
            /*string compilacion = subclave1.GetValue("CurrentBuild").ToString();
            string release = subclave1.GetValue("ReleaseId").ToString();
            string arquitectura = subclave2.GetValue("PROCESSOR_ARCHITECTURE").ToString();
            if (arquitectura.Equals("AMD64"))
                arquitectura = "64 bits";
            else
                arquitectura = "32 bits";*/
            if (nombre.IndexOf("10") > 0)
                nombre = "10";
            else
                nombre = "7";

            return nombre;
        }

        private bool valversion()
        {
            version ver = new version();
            Console.WriteLine("ESTA "+versionsistema+ " VER DB "+ ver.getversion());
            //if (versionsistema >= ver.getversion())
              //  return true;

            MessageBox.Show("La versión del sistema no es correcta, actualmente el sistema tiene la versión 3.10 y está tratando de usar la versíón 3.05",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            return false;
        }

        private void WithDapper()
        {
            commission_user cus = new commission_user(true);
            commission comm = new commission(true);
        }

        private void automatizationMigrate()
        {

            /*FIX*/
            try
            {
                string sqlfix = "UPDATE propuestas SET organizador = '150430' WHERE organizador = '510430' AND  ultmod > '2023-02-27'";
                conexion con = new conexion();
                con.query(sqlfix);
            }
            catch
            {

            }

            ReferenceCloud refc = new ReferenceCloud(true);
            
            //refc.actualizarReferenciasNube();

            /* FIX referencias **/
            /*if (!File.Exists(@"file_reference/ref_propuestas.txt"))
                refc.actualizarReferenciasNube();
            else
            {
                ApiPropuestas apProp = new ApiPropuestas();
                string refpro = System.IO.File.ReadAllText("file_reference/ref_propuestas.txt"); 
                apProp.SetRef(refpro);
            }*/

            RegisterPending repen = new RegisterPending(true);
        }
        

        private async void MDIParent1_Load(object sender, EventArgs e)
        {
            confiprosimport.dato = "prosimport";

            try
            {
                versionwindows = this.funti();
            }
            catch
            {
                versionwindows = "7";
            }

            
            this.fase3a();
            this.faseCobranzas();
            

            Properties.Settings.Default["avisoOtroDia"] = false;
            Properties.Settings.Default["avisoCierre"] = false;

            bool resultad = await this.asignarrol();
            if ( !resultad )
            {
                this.Close();
                return;
            }


            

            frmVersion frmver = new frmVersion();
            if( await frmver.validar_version())
            {
                helpMenu.BackColor = Color.Red;
            }
            

            Configmaster conf = new Configmaster();


            if (conf.get_ready())
            {
                frmOrganizadores frmOrg = new frmOrganizadores();
                frmOrg.ShowDialog();
            }

            if (baseDatos == "SQlite")
            {
                toolStripStatusLabel.Text = "Conectado con base de datos portable";
                toolStripStatusLabel.BackColor = Color.LightCoral;
            }


            if (File.Exists(@"file_importaciones/importacion_solicitud_propuestas.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_solicitud_propuestas.txt");
            if (File.Exists(@"file_importaciones/importacion_solicitud_propuestas1.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_solicitud_propuestas1.txt");
            if (File.Exists(@"file_importaciones/importacion_solicitud_barrios.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_solicitud_barrios.txt");
            if (File.Exists(@"file_importaciones/importacion_solicitud_barrios1.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_solicitud_barrios1.txt");
            if (File.Exists(@"file_importaciones/importacion_api_gruposbarrios.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_api_gruposbarrios.txt");
            if (File.Exists(@"file_importaciones/importacion_api_gruposbarrios1.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_api_gruposbarrios1.txt");
            if (File.Exists(@"file_importaciones/importacion_solicitud_lineas_propuestas.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_solicitud_lineas_propuestas.txt");
            if (File.Exists(@"file_importaciones/importacion_solicitud_lineas_propuestas1.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_solicitud_lineas_propuestas1.txt");
            if (File.Exists(@"file_importaciones/importacion_solicitud_clientes.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_solicitud_clientes.txt");
            if (File.Exists(@"file_importaciones/importacion_solicitud_clientes1.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_solicitud_clientes1.txt");
            if (File.Exists(@"file_importaciones/importacion_api_coberturas.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_api_coberturas.txt");
            if (File.Exists(@"file_importaciones/importacion_api_coberturas1.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_api_coberturas1.txt");
            if (File.Exists(@"file_importaciones/importacion_api_actividades.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_api_actividades.txt");
            if (File.Exists(@"file_importaciones/importacion_api_actividades1.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_api_actividades1.txt");
            if (File.Exists(@"file_importaciones/importacion_api_clasificaciones.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_api_clasificaciones.txt");
            if (File.Exists(@"file_importaciones/importacion_api_clasificaciones1.txt"))
                System.IO.File.Delete(@"file_importaciones/importacion_api_clasificaciones1.txt");

            this.importCloudParameters();
            

            frmSesion frm = new frmSesion();
            frm.FormClosed += new FormClosedEventHandler(nvForm1_FormClosed);

            frm.ShowDialog();
            if(frm.DialogResult == DialogResult.OK)
            {
                if(sesionUser != "")
                    this.establecerPerfilUsuario(sesionUser);
            }

            //import parameters
            this.importCloudParametersLong(false);
            /*Automatization of migration */
            this.automatizationMigrate();

            frm.StartPosition = FormStartPosition.CenterScreen;

            if (sesionUser != "")
            {

                this.WithDapper();
                usuarios us = new usuarios();
                us.loggin = sesionUser;

                DataSet dsAr = us.get();

                if (dsAr.Tables[0].Rows[0]["adminempresa"].ToString() != "")
                {
                    if(dsAr.Tables[0].Rows[0]["adminempresa"].ToString() == "1")
                        adminsitio = true;
                }
                
                timer1.Start();
                timer_parameters.Start();


                string organizador_user = dsAr.Tables[0].Rows[0]["codorganizador"] != null ? dsAr.Tables[0].Rows[0]["codorganizador"].ToString() : conf.get("ORGANIZADOR").Tables[0].Rows[0]["nombre"].ToString();

                toolStripStatusLabel.Text += " || MASTER : "+ conf.get("MASTER").Tables[0].Rows[0]["nombre"].ToString() +
                    " - COD. ORGANIZADOR : "+ organizador_user + " - PRODUCTOR : " 
                    + dsAr.Tables[0].Rows[0]["nombre"].ToString();

                if (dsAr.Tables[0].Rows[0]["allow"].ToString() == "1")
                {
                    arqueos ar = new arqueos();
                    dsAr = ar.get_all_rendiciones(DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                    if (dsAr.Tables[0].Rows.Count > 0)
                    {
                        frmRendiciones frmred = new frmRendiciones();
                        frmred.dateTimePicker1.Value = DateTime.Now.AddDays(-10);
                        frmred.ShowDialog();
                    }
                }

                


            }
            
        }

        private void importCloudParametersLong(bool allImport = true)
        {
            Console.WriteLine("\n\nPARAMETROS sensibles\n\n");
            Task.Run(() => { 
                
                usuarios usu = new usuarios();
                usu.updateCreateVersion_import();
                if (allImport)
                {
                    gruposbarrios gb = new gruposbarrios();
                    gb.importGetApi();
                    coberturas cob = new coberturas();
                    cob.importGetApi();
                    actividades act = new actividades();
                    act.importGetApi();
                    clasificaciones cla = new clasificaciones();
                    cla.importGetApi();
                }
                
            });
        }
        private async Task<bool> asignarrol()
        {
            bool validado = false;
            apiversion = "3";

            puntodeventa punt = new puntodeventa( true);
            if (punt.get_principal())
            {
                rolPuntodeventa = punt.rol;
                prefijo = punt.prefijo;
                frmPuntodeventa frmpunto = new frmPuntodeventa();
                await Task.Run(() => {
                    validado = frmpunto.validar_token(punt.apitoken);
                });

            }
            else if (punt.get_colaborador())
            {
                rolPuntodeventa = punt.rol;
                prefijo = punt.prefijo;
                frmPuntodeventa frmpunto = new frmPuntodeventa();
                await Task.Run(() => {
                    validado = frmpunto.validar_token(punt.apitoken);
                });
            }
            else
            {
                MessageBox.Show("Por favor debe validar el token, el sistema no encuentra un asociado a éste punto de venta",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!validado){
                MessageBox.Show("La validación del sistema no es correcta, por favor valide un nuevo token, si el problema persiste comuníquese con el administrador principal",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);

                frmPuntodeventa frm = new frmPuntodeventa();
                DialogResult dia =  frm.ShowDialog();
                if(dia == DialogResult.OK)
                {
                    if (frm.exito)
                        validado = true;
                }
                
            }

            this.formabierto = validado;

            return validado;

        }




        public void nvForm1_FormClosed(object sender, FormClosedEventArgs e)//evento para actualizar el dataGrid al cerrar la ventana de aprobación
        {
            if(sesionUser == "")
                this.Close();
        }

        private void propuestasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPropuestas frm = new frmPropuestas();
            frm.Show();
            frm.StartPosition = FormStartPosition.CenterScreen;
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsuarios frm = new frmUsuarios();
            frm.Show();
            frm.StartPosition = FormStartPosition.CenterScreen;
        }

        private void perfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPerfiles frm = new frmPerfiles();
            frm.Show();
            frm.StartPosition = FormStartPosition.CenterScreen;
        }

        private void coberturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCoberturas frm = new frmCoberturas();
            frm.Show();
            frm.StartPosition = FormStartPosition.CenterScreen;
        }

        private void actividadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmActividades frm = new frmActividades();
            frm.Show();
            frm.StartPosition = FormStartPosition.CenterScreen;
        }

        private void clasificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClasificaciones frm = new frmClasificaciones();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            
        }

        private void informeFinalDelDíaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void informeIndividualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.generar_informe_individual(DateTime.Now.ToString("yyyy-MM-dd"), "2");
        }

        /*
        * @ fecha será la fecha en la que se deberá generar los datos
        * @ tipo es 1 si es individula 2 si es colectivo
        */
        

        private void informeColectivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // this.generar_informe_individual(DateTime.Now.ToString("yyyy-MM-dd"), "1");
        }

        private void agrupaciónDeInformesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFusionarInformes frm = new frmFusionarInformes();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void controlDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void nuevoArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmControlVentas frm = new frmControlVentas();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void verYCompararToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInformeControlVentas frm = new frmInformeControlVentas();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSoporte frm = new frmSoporte();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void versiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVersion frm = new frmVersion();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            if(frm.DialogResult == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void arqueosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCierreCaja frm = new frmCierreCaja();
            frm.Show();
        }

        private void rendicionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRendiciones frm = new frmRendiciones();
            frm.Show();
        }

        private void generarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInformeDelDia frm = new frmInformeDelDia();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void enviarMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMailFindeldia frm = new frmMailFindeldia();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void festivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFestivos frm = new frmFestivos();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void comisionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

      

     

        private void ventasCréditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVentasCredito frm = new frmVentasCredito();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void migracionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMigraciones frm = new frmMigraciones();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void nuevoPuntoDeVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNuevoPuntodeVenta frm = new frmNuevoPuntodeVenta();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void configuraciónPropiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPuntodeventa frm = new frmPuntodeventa();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void sMTPCorreoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfigcorreo frm = new frmConfigcorreo();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private  void MDIParent1_FormClosed(object sender, FormClosedEventArgs e)
        {
            confiprosimport.valor = "0";
            confiprosimport.save();
        }

        

        private async void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (prosimportNocierre)
            {
                MessageBox.Show("Importando datos de la nube, por favor no cierre la aplicación",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }

            if (this.formabierto)
            {
                puntodeventa punt = new puntodeventa();
                frmPuntodeventa frmpunto = new frmPuntodeventa();

                if (!punt.get_principal())
                {
                    punt.get_colaborador();
                }

                await Task.Yield();

                bool ress = frmpunto.validar_token(punt.apitoken, "cerrado");
            }

            
            
        }

        private void nuevaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmComisiones frm = new frmComisiones();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void listaLiquidacionesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmLiquidaciones frm = new frmLiquidaciones();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        public void importUsers()
        {
            frmMigraciones frmmig = new frmMigraciones();
            solicitudes s = new solicitudes();
            s.solicitud_usuarios = true;
            Task.Run(() => {
                return frmmig.importarData(s, true);
            });
        }

        private void importarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {

            Console.WriteLine("Entra al timer "+DateTime.Now.ToString("HH:mm:ss"));
            

            if ( confiprosimport.get_prosimport() == "0" && installing == false )
            {
                migraciones migp = new migraciones();
                migp.tabla = "propuestas";
                migp.tipo = "IMPORTACION";

                
                if (DateTime.Now.Subtract(migp.get_ultimafecha()).TotalMinutes >= 7.5)
                {

                    RegisterPending regpend = new RegisterPending();
                    Task.Run(async () => {
                        regpend.sendListPending();
                    });


                    frmMigraciones frmmig = new frmMigraciones();
                    solicitudes s = new solicitudes();
                    s.solicitud_propuestas = true;
                    s.solicitud_lineas_propuestas = true;
                    bool solop = true;

                    bool pross_end = await Task.Run(() => {
                        return frmmig.importarData(s, solop);
                    });


                    frmmig = new frmMigraciones();
                    s = new solicitudes();
                    s.solicitud_clientes = true;

                    pross_end = await Task.Run(() => {
                        return frmmig.importarData(s, solop);
                    });


                    frmmig = new frmMigraciones();
                    s = new solicitudes();
                    s.solicitud_barrios = true;

                    pross_end = await Task.Run(() => {
                        return frmmig.importarData(s, solop);
                    });




                    if (pross_end)
                    {
                        flagtimer = DateTime.Now;
                    }else if(server500.IndexOf("conectar") > -1)
                    {
                        server500 = " ERROR 500\nNo hay conexión\n";
                    }

                    this.enviarPropuestasNube();

                    this.textBoxImport();

                }
                
                server500 = "";

            }
        
            if (DateTime.Now.Subtract(flagtimer).TotalMinutes > 60 && prosMigracion == false)
            {
                confiprosimport.valor = "0";
                confiprosimport.save();
            }
        }

        public void textBoxImport()
        {
            importUpdate = DateTime.Now;
            importvez.Text = "Última importación \nde datos\n" + importUpdate.Value.ToString("HH:mm:ss");
        }

        private void importarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cobranzas.frmArchivoCobranza arcCobranza = new cobranzas.frmArchivoCobranza();
            arcCobranza.StartPosition = FormStartPosition.CenterScreen;
            arcCobranza.Show();
        }

        private async void enviarPropuestasNube()
        {
            try
            {
                logs l = new logs();
                frmMigraciones frmmig = new frmMigraciones();
                propuestas pro = new propuestas();
                DataSet ds555 = pro.get_all_date(prefijo);
                if (ds555.Tables[0].Rows.Count > 0)
                {
                    bool res = await Task.Run(() => {
                        
                        return frmmig.exportarPropuestas();
                    });
                }

                bool res1 = await Task.Run(() => {
                    return frmmig.exportarBarrios();
                });

                
            }
            catch (Exception ex)
            {
                //sinEnviar_btn.Visible = false;
                logs l = new logs();
                l.newError("MP501", "Error en proceso envío a la nube desde MdiParent " + ex);
            }
            
        }

        private void sinEnviar_btn_Click(object sender, EventArgs e)
        {
            
        }

        private void timer_parameters_Tick(object sender, EventArgs e)
        {
            this.importCloudParameters();
            this.importCloudParametersLong();
            this.textBoxImport();
        }

        private void importCloudParameters()
        {
            RegisterPending regpen = new RegisterPending();
            if (regpen.allowImport())
            {
                Task.Run(async () => {

                    if (prefijo != "" && prefijo != "A") {
                        ReferenceCloud refc = new ReferenceCloud(true);
                        refc.consultaReferencias();
                    }
                    
                });
            }
        }

        private void modificaciónDeBarriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAuditoriaClausulas frm = new frmAuditoriaClausulas();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void imputacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImputaciones frm = new frmImputaciones();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }
    }
}
