using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Linq;

namespace ProyectoBrokerDelPuerto
{
    public partial class frmMigraciones : Form
    {
        jsonapi data = new jsonapi();
        List<lineas_propuestas> listlineas = new List<lineas_propuestas>();
        List<barrios_propuesta> listbarriospropuestas = new List<barrios_propuesta>();
        List<barrios_propuesta> listbarrios = new List<barrios_propuesta>();
        List<clientes> listtomador = new List<clientes>();
        List<arqueos> listarqueos = new List<arqueos>();
        List<rendiciones> listrendiciones = new List<rendiciones>();
        List<lineas_rendiciones> listlineasrendiciones = new List<lineas_rendiciones>();
        configuraciones confprosimport = new configuraciones();
        public bool flaginstalacion = false;
        
        
        string textoexpo = "";
        private string urlapi;
        migraciones miPropuestas,miActividades,miClasificacion,miCoberturas, miBarrios, miGrupoBarrios, miUsuarios, miPerfiles,miTomador,miArqueo, miRendicion,miLineaRendicion;

        public frmMigraciones()
        {
            InitializeComponent();
        }

        private bool verificar()
        {
            usuarios user = new usuarios();
            return user.loggin_pass(comboBox1.Text, pass_txt.Text);
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (this.verificar())
            {
                this.migrardata();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public async void migrardata(bool exportar = true)
        {

            cargadatos.Visible = true;
            MDIParent1.prosMigracion = true;

            if (exportar)
            {
                if (MessageBox.Show("La migración podría durar algunos minutos, ¿Desea continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    cargadatos.Visible = false;
                    return;
                }
            }

            
            comboBox1.Enabled = false;
            pass_txt.Enabled = false;
            button1.Enabled = false;
            cbReset.Enabled = false;
            expimp.Enabled = false;

            usuarios usu = new usuarios();
            usu.loggin = comboBox1.Text;
            DataSet dsss = usu.get();

            migraciones mig = new migraciones();
            mig.tabla = "propuestas";
            mig.tipo = "EXPORTACION";
            string fecha = mig.get_ultimafecha().ToString("yyyy-MM-dd HH:mm:ss");
            
            bool res1 = false;
            bool res2 = false;

            string adminempresa1 = "";


            if (dsss.Tables[0].Rows.Count > 0)
            {
                if (dsss.Tables[0].Rows[0]["adminempresa"].ToString() != "")
                {
                    if (dsss.Tables[0].Rows[0]["adminempresa"].ToString() == "1")
                    {
                        adminempresa1 = "1";
                        
                    }
                }
            }

            solicitudes s = new solicitudes();

            if(chPropuestas.Checked)
                s.solicitud_propuestas = true;
            if (chPropuestas.Checked)
                s.solicitud_lineas_propuestas = true;
            if (chPropuestas.Checked)
                s.solicitud_barrios_propuestas = true;
            if (chPropuestas.Checked || chClientes.Checked)
                s.solicitud_clientes = true;
            //if (chUsuarios.Checked)
                //s.solicitud_usuarios = true;
            if (chPerfiles.Checked)
                s.solicitud_perfiles = true;
            if (chArqueos.Checked)
                s.solicitud_arqueos = true;
            if (chRendiciones.Checked)
                s.solicitud_rendiciones = true;
            if (chRendiciones.Checked)
                s.solicitud_lineas_rendiciones = true;
            if (chActividades.Checked)
                s.solicitud_actividades = true;
            if (chCoberturas.Checked)
                s.solicitud_coberturas = true;
            if (chClasificaciones.Checked)
                s.solicitud_clasificaciones = true;
            if (chBarrios.Checked)
                s.solicitud_barrios = true;
            if (chBarrios.Checked)
                s.solicitud_gruposbarrios = true;
            if (chProvincias.Checked)
                s.solicitud_provincias = true;


            if (adminempresa1 == "1")
            {
                if (exportar && ( expimp.SelectedIndex == 2 || expimp.SelectedIndex == 0))
                {
                    
                    this.confprosimport.valor = "1";
                    this.confprosimport.save();
                    //MDIParent1.prosimport = true;
                    res1 = await this.exportarData(mig, fecha);

                    if (chClientes.Checked)
                    {
                        await Task.Run(() =>
                        {
                            return this.exportarClientes();
                        });
                    }

                    if (chRendiciones.Checked || chArqueos.Checked)
                    {
                        await Task.Run(() => {
                            return this.exportarRendiciones();
                        });
                    }


                    this.confprosimport.valor = "0";
                    this.confprosimport.save();
                    //MDIParent1.prosimport = false;
                    //textBox1.Text += textoexpo;
                    MessageBox.Show("Se ha hecho la exportación con éxito");

                }
                if((expimp.SelectedIndex == 1 || expimp.SelectedIndex == 0))
                {

                    //if (this.data.rolpuntodeventa == "COLABORADOR")
                        res2 = await this.importarData(s);
                }
                
                
            }
            else
            {
                if ((expimp.SelectedIndex == 1 || expimp.SelectedIndex == 0))
                {
                    if (this.data.rolpuntodeventa == "COLABORADOR")
                        res2 = await this.importarData(s);
                }

                if ((expimp.SelectedIndex == 2 || expimp.SelectedIndex == 0))
                {
                    if (exportar && res2)
                    {
                        this.confprosimport.valor = "1";
                        this.confprosimport.save();
                        //MDIParent1.prosimport = true;
                        res1 = await this.exportarData(mig, fecha);

                        if (chClientes.Checked)
                        {
                            await Task.Run(() =>
                            {
                                return this.exportarClientes();
                            });
                        }

                        if (chRendiciones.Checked || chArqueos.Checked)
                        {
                            await Task.Run(() =>
                            {
                                return this.exportarRendiciones();
                            });
                        }

                        this.confprosimport.valor = "0";
                        this.confprosimport.save();
                        //MDIParent1.prosimport = false;
                        //textBox1.Text += textoexpo;
                        MessageBox.Show("Se ha hecho la exportación con éxito");
                    }

                }
            }

            
            
            cargadatos.Visible = false;
            comboBox1.Enabled = true;
            pass_txt.Enabled = true;
            button1.Enabled = true;
            cbReset.Enabled = true;
            expimp.Enabled = true;
            cbReset.Checked = false;
            pass_txt.Text = "";

            MDIParent1.prosMigracion = false;

        }

        public async Task<bool> exportarClientes(string fecha_custom = "")
        {
            this.datastar();
            migraciones mig = new migraciones();
            mig.tipo = "EXPORTACION";
            mig.tabla = "clientes";
            string fecha = mig.get_ultimafecha(cbReset.Checked).ToString("yyyy-MM-dd HH:mm:ss");

            if(fecha_custom != "")
            {
                fecha = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            }
            
            clientes cli = new clientes();
            DataSet ds = cli.get_all_ExpClientesfecha(fecha);

            try
            {
                textoexpo += "EXPORTACIÓN CLIENTES / " + ds.Tables[0].Rows.Count + " Registros " + Environment.NewLine;
                Invoke(new Action(() => textBox1.Text += textoexpo));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i = i + 2000)
                {
                    textoexpo = (i + 2000) + " / " + ds.Tables[0].Rows.Count + " Registros enviados " + Environment.NewLine;
                    if ((i + 2000) > ds.Tables[0].Rows.Count)
                        textoexpo = ds.Tables[0].Rows.Count + " / " + ds.Tables[0].Rows.Count + " Registros enviados " + Environment.NewLine;
                    Invoke(new Action(() => textBox1.Text += textoexpo));
                    this.migrarClientes(fecha, i);
                    this.data.listtomador = this.listtomador;
                    this.data.fechamigracion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    string jsonlistpropuestas = JsonConvert.SerializeObject(this.data);
                    bool res = await this.enviar_json(jsonlistpropuestas);
                }

                textoexpo = Environment.NewLine + Environment.NewLine;
                Invoke(new Action(() => textBox1.Text += textoexpo));
                this.miTomador.tipo = "EXPORTACION";
                this.miTomador.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.miTomador.tabla = "clientes";
                this.miTomador.cantidad_registros = ds.Tables[0].Rows.Count.ToString();
                this.miTomador.save();
                return true;
            }
            catch(Exception ex)
            {
                logs log = new logs();
                log.coderror = "MI101";
                log.mensaje = "ERROR al exportar clientes " + ex.Message;
                Console.WriteLine("ERROR al exportar clientes " + ex.Message);
                return false;
            }

            

            
        }

        
        public async Task<bool> exportarPropuestas()
        {
            propuestas pro = new propuestas();
            pro.enviohecho_date(DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"));

            this.confprosimport.valor = "1";
            this.confprosimport.save();
            //MDIParent1.prosimport = true;
            this.datastar();

            migraciones mig = new migraciones();
            string fecha = mig.get_ultimafecha(cbReset.Checked).ToString("yyyy-MM-dd HH:mm:ss");
            mig.tipo = "EXPORTACION";
            mig.tabla = "propuestas";
            Console.WriteLine("\n\nEXPORTA PROPUESTAS\n\n");
            bool mp = this.migrarPropuestas(fecha);

            mig.tabla = "barrios";
            fecha = mig.get_ultimafecha(cbReset.Checked).ToString("yyyy-MM-dd HH:mm:ss");
            this.migrarBarrios(fecha);

            this.data.listtomador = this.listtomador;
            this.data.fechamigracion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            string jsonlistpropuestas = JsonConvert.SerializeObject(this.data);
            bool res = await this.enviar_json(jsonlistpropuestas);
            
            this.confprosimport.valor = "0";
            this.confprosimport.save();
            //MDIParent1.prosimport = false;
            if (res)
            {
                try
                {
                    if (this.miPropuestas.save())
                    {
                        pro = new propuestas();
                        pro.enviohecho();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    logs l = new logs();
                    l.newError("EXP_PRO101", "hubo un error al tratar de guardar los datos de exportación " + ex.Message);
                    return false;
                }

            }

            return false;
        }

        public async Task<bool> exportarBarrios()
        {
            this.confprosimport.valor = "1";
            this.confprosimport.save();
            //MDIParent1.prosimport = true;
            this.datastar();

            migraciones mig = new migraciones();
            mig.tipo = "EXPORTACION";
            mig.tabla = "barrios";
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.migrarBarrios(fecha);
            this.migrarGrupoBarrios(fecha);

            string jsonlistpropuestas = JsonConvert.SerializeObject(this.data);
            bool res = await this.enviar_json(jsonlistpropuestas);
            this.confprosimport.valor = "0";
            this.confprosimport.save();
            //MDIParent1.prosimport = false;
            if (res)
            {
                try
                {
                    barrios barr = new barrios();
                    barr.envionube_();
                    gruposbarrios gr = new gruposbarrios();
                    gr.actualizar_envionube();
                    this.miBarrios.save();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("hubo un error al guardar datos exportación barrios" + ex.Message);
                    return false;
                }

            }

            return false;
        }


        public async Task<bool> exportarRendiciones()
        {
            this.confprosimport.valor = "1";
            this.confprosimport.save();
            //MDIParent1.prosimport = true;
            this.datastar();

            migraciones mig = new migraciones();
            mig.tipo = "EXPORTACION";
            mig.tabla = "arqueos";
            string fecha = mig.get_ultimafecha().ToString("yyyy-MM-dd HH:mm:ss");
            bool mp = this.migrarArqueos(fecha);


            this.data.listarqueos = this.listarqueos;
            this.data.fechamigracion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            
            mig.tabla = "rendiciones";
            fecha = mig.get_ultimafecha().ToString("yyyy-MM-dd HH:mm:ss");
            mp = this.migrarRendiciones(fecha);

            this.data.listrendiciones = this.listrendiciones;
            this.data.fechamigracion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


            mig.tabla = "lineasrendiciones";
            fecha = mig.get_ultimafecha().ToString("yyyy-MM-dd HH:mm:ss");
            mp = this.migrarLineasRendiciones(fecha);
            this.data.listlineasrendiciones = this.listlineasrendiciones;
            this.data.fechamigracion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string jsonlistpropuestas = JsonConvert.SerializeObject(this.data);
            bool res = await this.enviar_json(jsonlistpropuestas);
            this.confprosimport.valor = "0";
            this.confprosimport.save();
            //MDIParent1.prosimport = false;
            if (res)
            {
                try
                {
                    this.miArqueo.save();
                    this.miRendicion.save();
                    this.miLineaRendicion.save();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("hubo un error al tratar de guardar los datos de exportación rendición " + ex.Message);
                    return false;
                }

            }

            return false;
        }

        public async Task<bool> exportarUsuariosYPerfiles()
        {
            this.confprosimport.valor = "1";
            this.confprosimport.save();
            //MDIParent1.prosimport = true;
            this.datastar();

            migraciones mig = new migraciones();
            mig.tipo = "EXPORTACION";
            mig.tabla = "usuarios";
            string fecha = mig.get_ultimafecha().ToString("yyyy-MM-dd HH:mm:ss");
            this.migrarUsuarios(fecha);

            this.data.fechamigracion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mig.tabla = "perfiles";
            fecha = mig.get_ultimafecha().ToString("yyyy-MM-dd HH:mm:ss");
            this.migrarPerfiles(fecha);

            string jsonlistpropuestas = JsonConvert.SerializeObject(this.data);
            bool res = await this.enviar_json(jsonlistpropuestas);
            this.confprosimport.valor = "0";
            this.confprosimport.save();
            //MDIParent1.prosimport = false;
            if (res)
            {
                try
                {
                    this.miUsuarios.save();
                    this.miPerfiles.save();
                    return true;
                }
                catch (Exception ex)
                {
                    logs log = new logs();
                    log.coderror = "EXP103";
                    log.mensaje = "ERROR al tratar de guardar migración de usuarios";
                    log.save();
                    return false;
                }

            }

            return false;
        }

        public async Task<bool> exportarPerfiles()
        {
            this.confprosimport.valor = "1";
            this.confprosimport.save();
            //MDIParent1.prosimport = true;
            this.datastar();

            migraciones mig = new migraciones();
            mig.tipo = "EXPORTACION";
            mig.tabla = "perfiles";
            string fecha = mig.get_ultimafecha().ToString("yyyy-MM-dd HH:mm:ss");
            this.data.fechamigracion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.migrarPerfiles(fecha);
            string jsonlistpropuestas = JsonConvert.SerializeObject(this.data);
            bool res = await this.enviar_json(jsonlistpropuestas);
            this.confprosimport.valor = "0";
            this.confprosimport.save();
            //MDIParent1.prosimport = false;
            if (res)
            {
                try
                {
                    this.miPerfiles.save();
                    perfiles perrr = new perfiles();
                    perrr.envioHecho();
                    return true;
                }
                catch (Exception ex)
                {
                    logs log = new logs();
                    log.coderror = "EXP103";
                    log.mensaje = "ERROR al tratar de guardar migración de perfiles";
                    log.save();
                    return false;
                }

            }

            return false;
        }

        private async Task<bool> exportarData( migraciones mig, string fecha)
        {

            if (chPropuestas.Checked)
            {
                mig.tipo = "EXPORTACION";
                mig.tabla = "propuestas";   
                bool mp = this.migrarPropuestas(fecha);
            }
            

            if (this.data.rolpuntodeventa == "PRINCIPAL")
            {
                if (chActividades.Checked)
                {
                    mig.tabla = "actividades";
                    fecha = mig.get_ultimafecha().ToString("yyyy-MM-dd HH:mm:ss");
                    if (cbReset.Checked)
                        fecha = "2021-01-01 00:00:00";
                    this.migrarActividades(fecha);
                }

                if (chClasificaciones.Checked)
                {
                    mig.tabla = "clasificaciones";
                    fecha = mig.get_ultimafecha().ToString("yyyy-MM-dd HH:mm:ss");
                    if (cbReset.Checked)
                        fecha = "2021-01-01 00:00:00";
                    this.migrarClasificaciones(fecha);
                }

                if (chCoberturas.Checked)
                {
                    mig.tabla = "coberturas";
                    fecha = mig.get_ultimafecha().ToString("yyyy-MM-dd HH:mm:ss");

                    if (cbReset.Checked)
                        fecha = "2021-01-01 00:00:00";

                    this.migrarCoberturas(fecha);
                }

                if (chBarrios.Checked)
                {
                    mig.tabla = "barrios";
                    fecha = mig.get_ultimafecha(cbReset.Checked).ToString("yyyy-MM-dd HH:mm:ss");
                    this.migrarBarrios(fecha);
                }

                if (chBarrios.Checked)
                {
                    mig.tabla = "gruposbarrios";
                    fecha = mig.get_ultimafecha(cbReset.Checked).ToString("yyyy-MM-dd HH:mm:ss");
                    this.migrarGrupoBarrios(fecha);
                }
            }

            
            usuarios usu = new usuarios();
            usu.loggin = comboBox1.Text;
            DataSet dsss = usu.get();

            

            if (dsss.Tables[0].Rows.Count > 0)
            {
                if(dsss.Tables[0].Rows[0]["adminempresa"].ToString() != "")
                {
                    if (dsss.Tables[0].Rows[0]["adminempresa"].ToString() == "1")
                    {
                        /*if (chUsuarios.Checked)
                        {
                            mig.tabla = "usuarios";
                            fecha = mig.get_ultimafecha(cbReset.Checked).ToString("yyyy-MM-dd HH:mm:ss");
                            this.migrarUsuarios(fecha);
                        }*/

                        if (chPerfiles.Checked)
                        {
                            mig.tabla = "perfiles";
                            fecha = mig.get_ultimafecha(cbReset.Checked).ToString("yyyy-MM-dd HH:mm:ss");
                            this.migrarPerfiles(fecha);
                        }

                    }
                }
            }

            

            this.data.listtomador = this.listtomador;

            this.data.fechamigracion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            string jsonlistpropuestas = JsonConvert.SerializeObject(this.data);

            bool res = await Task.Run(() =>
            {
                return this.enviar_json(jsonlistpropuestas);
            });
                
            if (res)
            {
                try
                {
                    
                    if (miPropuestas.save())
                    {
                        propuestas pro = new propuestas();
                        pro.enviohecho();
                    }

                


                    if (this.data.rolpuntodeventa == "PRINCIPAL")
                    {
                        if (chActividades.Checked)
                            miActividades.save();
                        if (chClasificaciones.Checked)
                            miClasificacion.save();
                        if (chCoberturas.Checked)
                            miCoberturas.save();
                        if (chBarrios.Checked)
                            miBarrios.save();
                        if (chBarrios.Checked)
                            miGrupoBarrios.save();
                    }

                    if (dsss.Tables[0].Rows.Count > 0)
                    {
                        if (dsss.Tables[0].Rows[0]["adminempresa"].ToString() != "")
                        {
                            if (dsss.Tables[0].Rows[0]["adminempresa"].ToString() == "1")
                            {
                                //if (this.miTomador.cantidad_registros != "0")
                                  //  this.miTomador.save();
                            }
                        }
                    }

                    Console.WriteLine("TEXTO EXPORTA \n\n" + textBox1.Text);
                    
                    return true;
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("hubo un error al tratar de guardar los datos de exportación " + ex.Message);
                    return false;
                }

            }

            return false;
        }

        public async Task<bool> importarData(solicitudes sol, bool solopropuestas = false, bool get_prefix_own = false)
        {
            this.confprosimport.valor = "1";
            this.confprosimport.save();
            //MDIParent1.prosimport = true;

            try
            {
                migraciones mig = new migraciones();
                mig.tipo = "IMPORTACION";
                mig.tabla = "propuestas";
            
            
                
                string fecha = mig.get_ultimafecha().ToString("yyyy-MM-dd HH:mm:ss");
                importapi impo = new importapi();

                bool resimport = false;
                Console.WriteLine("SE ESTÁ IMPORTANDO RESET ( "+cbReset.Checked+")" + DateTime.Now.ToString("HH:mm:ss"));

                
                if (fecha == "2021-01-01")
                    resimport = await impo.parametroscolaborador("2020-01-01", sol, cbReset.Checked, solopropuestas, get_prefix_own);
                else
                    resimport = await impo.parametroscolaborador(fecha, sol, cbReset.Checked, solopropuestas, get_prefix_own);


                
                textBox1.Text += "DATOS IMPORTADOS " + Environment.NewLine + Environment.NewLine;
                textBox1.Text += impo.concattextbox;

                if (resimport)
                {
                    /*MDIParent1 f1 = (MDIParent1)this.MdiParent;
                    f1.pictureBox3.Visible = false;*/
                    this.confprosimport.valor = "0";
                    this.confprosimport.save();
                    //MDIParent1.prosimport = false;
                    return true;
                }
                else
                {
                    
                    this.confprosimport.valor = "0";
                    this.confprosimport.save();
                    //MDIParent1.prosimport = false;
                    logs log = new logs();
                    log.newError("Mi101", "Error al importar sólo propuestas " + DateTime.Now.ToString("dd/MM/yyyy"));
                    return false;
                }
            }
            catch (Exception ex)
            {
                logs log = new logs();
                log.newError("Mi101A", "Error al importar sólo propuestas " + DateTime.Now.ToString("dd/MM/yyyy") + " " + ex.Message);
                this.confprosimport.valor = "0";
                this.confprosimport.save();
                //MDIParent1.prosimport = false;
                return false;
            }
            

        }


        private void migrarClasificaciones(string fecha)
        {
            migraciones mi = new migraciones();
            mi.tipo = "EXPORTACION";
            mi.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mi.tabla = "clasificaciones";
            if (!mi.exist())
            {
                int cantregistros = 0;
                string consecutivos = "";

                clasificaciones cla = new clasificaciones();
                DataSet ds = cla.get_all_fecha(fecha);

                textBox1.Text += "EXPORTACIÓN CLASIFICACIONES / " + ds.Tables[0].Rows.Count + " Registros " + Environment.NewLine;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<clasificaciones> listclasificaciones = new List<clasificaciones>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        listclasificaciones.Add(
                            new clasificaciones()
                            {
                                id = ds.Tables[0].Rows[i]["id"].ToString(),
                                cod = ds.Tables[0].Rows[i]["cod"].ToString(),
                                nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                                id_actividad = ds.Tables[0].Rows[i]["id_actividad"].ToString(),
                                ultmod = Convert.ToDateTime(ds.Tables[0].Rows[i]["ultmod"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                user_edit = ds.Tables[0].Rows[i]["user_edit"].ToString(),
                                codestado = ds.Tables[0].Rows[i]["codestado"].ToString(),
                            }
                        );


                        textBox1.Text += "Nombre: " + ds.Tables[0].Rows[i]["nombre"].ToString() + Environment.NewLine;

                        cantregistros = i + 1;
                        consecutivos += ds.Tables[0].Rows[i]["cod"].ToString() + ",";
                    }

                    this.data.listclasificaciones = listclasificaciones;

                }

                if (cantregistros > 0)
                    mi.numeracion = consecutivos;


                mi.cantidad_registros = cantregistros.ToString();
                miClasificacion = mi;

                textBox1.Text += Environment.NewLine + Environment.NewLine;

            }
            else
            {
                MessageBox.Show("La migración de Clasifición del " + fecha + " ya se ha hecho");
            }

            
        }

        private void migrarActividades(string fecha)
        {


            migraciones mi = new migraciones();
            mi.tipo = "EXPORTACION";
            mi.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mi.tabla = "actividades";
            if (!mi.exist())
            {
                int cantregistros = 0;
                string consecutivos = "";

                actividades cob = new actividades();
                DataSet ds = cob.get_all_fecha(fecha);

                textBox1.Text += "EXPORTACIÓN ACTIVIDADES / " + ds.Tables[0].Rows.Count + " Registros " + Environment.NewLine;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    List<actividades> listactividades = new List<actividades>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        listactividades.Add(
                            new actividades()
                            {
                                id = ds.Tables[0].Rows[i]["id"].ToString(),
                                cod = ds.Tables[0].Rows[i]["cod"].ToString(),
                                nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                                ultmod = Convert.ToDateTime( ds.Tables[0].Rows[i]["ultmod"].ToString() ).ToString("yyyy-MM-dd HH:mm:ss"),
                                user_edit = ds.Tables[0].Rows[i]["user_edit"].ToString(),
                                codestado = ds.Tables[0].Rows[i]["codestado"].ToString(),
                            }
                        );


                        textBox1.Text += "Nombre: " + ds.Tables[0].Rows[i]["nombre"].ToString() + Environment.NewLine;

                        cantregistros = i + 1;
                        consecutivos += ds.Tables[0].Rows[i]["cod"].ToString() + ",";
                    }

                    this.data.listactividades = listactividades;
                }

                if (cantregistros > 0)
                    mi.numeracion = consecutivos;


                mi.cantidad_registros = cantregistros.ToString();
                miActividades = mi;

                textBox1.Text += Environment.NewLine + Environment.NewLine;

            }
            else
            {
                MessageBox.Show("La migración de Actividades del " + fecha + " ya se ha hecho");
            }




        }

        private bool migrarArqueos(string fecha)
        {

            migraciones mi = new migraciones();
            mi.tipo = "EXPORTACION";
            mi.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mi.tabla = "arqueos";
            if (!mi.exist())
            {

                arqueos arq = new arqueos();
                DataSet ds = arq.get_all_fecha_export(fecha);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.listarqueos = (from DataRow dr in ds.Tables[0].AsEnumerable().ToList()
                                        select new arqueos()
                                        {
                                            id = dr["id"].ToString(),
                                            usuario = dr["usuario"].ToString(),
                                            nombre = dr["nombre"].ToString(),
                                            fechadia = Convert.ToDateTime(dr["fechadia"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                            valorinicial = dr["valorinicial"].ToString(),
                                            dinerorealcaja = dr["dinerorealcaja"].ToString(),
                                            valormanual = dr["valormanual"].ToString(),
                                            cuadredescuadre = dr["cuadredescuadre"].ToString(),
                                            supervisor = dr["supervisor"].ToString(),
                                            nombresupervisor = dr["nombresupervisor"].ToString(),
                                            observaciones = dr["observaciones"].ToString(),
                                            ultmod = Convert.ToDateTime(dr["ultmod"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                            user_edit = dr["user_edit"].ToString(),
                                            codestado = dr["codestado"].ToString(),
                                            rendicion = dr["rendicion"].ToString(),
                                            codempresa = MDIParent1.codempresa,
                                            cantpolizas = dr["cantpolizas"].ToString() != "" ? Convert.ToInt16(dr["cantpolizas"].ToString()) : 0,

                                        }).ToList();

                }

                mi.cantidad_registros = ds.Tables[0].Rows.Count.ToString();
                miArqueo = mi;

                return true;

            }
            else
            {
                return false;
            }


        }

        private bool migrarRendiciones(string fecha)
        {

            migraciones mi = new migraciones();
            mi.tipo = "EXPORTACION";
            mi.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mi.tabla = "rendiciones";
            if (!mi.exist())
            {

                rendiciones ren = new rendiciones();
                DataSet ds = ren.get_all_fecha_export(fecha);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.listrendiciones = (from DataRow dr in ds.Tables[0].AsEnumerable().ToList()
                                            select new rendiciones()
                                            {
                                                reg = dr["reg"].ToString(),
                                                idarqueos = dr["idarqueos"].ToString(),
                                                detalle = dr["detalle"].ToString(),
                                                valor = dr["valor"].ToString(),
                                                nocomprobante = dr["nocomprobante"].ToString(),
                                                entregadopor = dr["entregadopor"].ToString(),
                                                entregadoa = dr["entregadoa"].ToString(),
                                                ultmod = Convert.ToDateTime(dr["ultmod"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                                user_edit = dr["user_edit"].ToString(),
                                                codestado = dr["codestado"].ToString(),
                                                codempresa = MDIParent1.codempresa
                                        }).ToList();

                }

                mi.cantidad_registros = ds.Tables[0].Rows.Count.ToString();
                miRendicion = mi;

                return true;

            }
            else
            {
                return false;
            }


        }

        private bool migrarLineasRendiciones(string fecha)
        {
            migraciones mi = new migraciones();
            mi.tipo = "EXPORTACION";
            mi.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mi.tabla = "lineasrendiciones";
            if (!mi.exist())
            {

                lineas_rendiciones liren = new lineas_rendiciones();
                DataSet ds = liren.get_all_fecha_export(fecha);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.listlineasrendiciones = (from DataRow dr in ds.Tables[0].AsEnumerable().ToList()
                                            select new lineas_rendiciones()
                                            {
                                                id = dr["id"].ToString(),
                                                idrendicion = dr["idrendicion"].ToString(),
                                                fechaarqueo = Convert.ToDateTime(dr["fechaarqueo"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                                ultmod = Convert.ToDateTime(dr["ultmod"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                                valordia = dr["valordia"].ToString(),
                                                codempresa = MDIParent1.codempresa

                                            }).ToList();

                }

                mi.cantidad_registros = ds.Tables[0].Rows.Count.ToString();
                miLineaRendicion = mi;

                return true;

            }
            else
            {
                return false;
            }

        }

        private void migrarClientes(string fecha, int index)
        {

            migraciones mi = new migraciones();
            mi.tipo = "EXPORTACION";
            mi.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mi.tabla = "clientes";
            if (!mi.exist())
            {
                int cantregistros = 0;
                string consecutivos = "";

                clientes cli = new clientes();
                DataSet ds = cli.get_all_fecha(fecha, index);

                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.listtomador = (from DataRow dr in ds.Tables[0].AsEnumerable().ToList()
                                        select new clientes()
                                        {
                                            id = dr["id"].ToString(),
                                            nombres = dr["nombres"].ToString(),
                                            apellidos = dr["apellidos"].ToString(),
                                            telefono = dr["telefono"].ToString(),
                                            direccion = dr["direccion"].ToString(),
                                            email = dr["email"].ToString(),
                                            ciudad = dr["ciudad"].ToString(),
                                            codpostal = dr["codpostal"].ToString(),
                                            localidad = dr["localidad"].ToString(),
                                            fecha_nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"].ToString()).ToString("yyyy-MM-dd"),
                                            tipo_id = dr["tipo_id"].ToString(),
                                            sexo = dr["sexo"].ToString(),
                                            situacion = dr["situacion"].ToString(),
                                            ultmod = Convert.ToDateTime(dr["ultmod"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                            user_edit = dr["user_edit"].ToString(),
                                            codestado = dr["codestado"].ToString(),
                                            codempresa = dr["codempresa"].ToString()
                                        }).ToList();


                    /*for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Console.WriteLine("---< "+ ds.Tables[0].Rows[i]["nombres"].ToString());

                        
                        
                        

                        /*this.listtomador.Add(
                            new clientes()
                            {
                                id = ds.Tables[0].Rows[i]["id"].ToString(),
                                nombres = ds.Tables[0].Rows[i]["nombres"].ToString(),
                                apellidos = ds.Tables[0].Rows[i]["apellidos"].ToString(),
                                telefono = ds.Tables[0].Rows[i]["telefono"].ToString(),
                                direccion = ds.Tables[0].Rows[i]["direccion"].ToString(),
                                email = ds.Tables[0].Rows[i]["email"].ToString(),
                                ciudad = ds.Tables[0].Rows[i]["ciudad"].ToString(),
                                codpostal = ds.Tables[0].Rows[i]["codpostal"].ToString(),
                                localidad = ds.Tables[0].Rows[i]["localidad"].ToString(),
                                fecha_nacimiento = Convert.ToDateTime(ds.Tables[0].Rows[i]["fecha_nacimiento"].ToString()).ToString("yyyy-MM-dd"),
                                tipo_id = ds.Tables[0].Rows[i]["tipo_id"].ToString(),
                                sexo = ds.Tables[0].Rows[i]["sexo"].ToString(),
                                situacion = ds.Tables[0].Rows[i]["situacion"].ToString(),
                                ultmod = Convert.ToDateTime(ds.Tables[0].Rows[i]["ultmod"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                user_edit = ds.Tables[0].Rows[i]["user_edit"].ToString(),
                                codestado = ds.Tables[0].Rows[i]["codestado"].ToString(),
                                codempresa = ds.Tables[0].Rows[i]["codempresa"].ToString()
                            }
                        );

                    
                    

                        textBox1.Text += "Cliente: " + ds.Tables[0].Rows[i]["nombres"].ToString() + " "+ ds.Tables[0].Rows[i]["apellidos"].ToString() + Environment.NewLine;

                        cantregistros = i + 1;
                        consecutivos += ds.Tables[0].Rows[i]["id"].ToString() + ",";
                    }*/

                    //this.data.listtomador = listtomador;

                }

                if (cantregistros > 0)
                    mi.numeracion = consecutivos;


                mi.cantidad_registros = cantregistros.ToString();
                miTomador = mi;

                

            }
            else
            {
                MessageBox.Show("La migración de perfiles del " + fecha + " ya se ha hecho");
            }


        }


        private void migrarPerfiles(string fecha)
        {

            migraciones mi = new migraciones();
            mi.tipo = "EXPORTACION";
            mi.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mi.tabla = "perfiles";
            
                int cantregistros = 0;
                string consecutivos = "";

                perfiles per = new perfiles();
                DataSet ds = per.getEnvioNube();

                textBox1.Text += "EXPORTACIÓN PERFILES / " + ds.Tables[0].Rows.Count + " Registros " + Environment.NewLine;
                if (ds.Tables[0].Rows.Count > 0)
                {
                        
                    List<perfiles> listperfiles = new List<perfiles>();

                    string aux = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        listperfiles.Add(
                            new perfiles()
                            {
                                reg = ds.Tables[0].Rows[i]["reg"].ToString(),
                                nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                                modulo = ds.Tables[0].Rows[i]["modulo"].ToString(),
                                access = ds.Tables[0].Rows[i]["access"].ToString() !="" ? Convert.ToInt16(ds.Tables[0].Rows[i]["access"].ToString()) : 0,
                                vista = ds.Tables[0].Rows[i]["vista"].ToString(),
                                edicion = ds.Tables[0].Rows[i]["edicion"].ToString(),
                                eliminar = ds.Tables[0].Rows[i]["eliminar"].ToString(),
                                exportar = ds.Tables[0].Rows[i]["exportar"].ToString(),
                                codempresa = ds.Tables[0].Rows[i]["codempresa"].ToString(),
                            }
                        );

                        if(aux != ds.Tables[0].Rows[i]["nombre"].ToString())
                        {
                            textBox1.Text += "Perfil: " + ds.Tables[0].Rows[i]["nombre"].ToString() + Environment.NewLine;
                            aux = ds.Tables[0].Rows[i]["nombre"].ToString();
                        }

                        cantregistros = i + 1;
                        consecutivos += ds.Tables[0].Rows[i]["nombre"].ToString() + ",";
                    }

                    this.data.listperfiles = listperfiles;

                }

                if (cantregistros > 0)
                    mi.numeracion = consecutivos;


                mi.cantidad_registros = cantregistros.ToString();
                miPerfiles = mi;

                textBox1.Text += Environment.NewLine + Environment.NewLine;


        }

        private void migrarUsuarios(string fecha)
        {
            /*
            migraciones mi = new migraciones();
            mi.tipo = "EXPORTACION";
            mi.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mi.tabla = "usuarios";
            if (!mi.exist())
            {
                int cantregistros = 0;
                string consecutivos = "";

                usuarios user = new usuarios();
                DataSet ds = user.get_all_fecha(fecha);

                textBox1.Text += "EXPORTACIÓN USUARIOS / " + ds.Tables[0].Rows.Count + " Registros " + Environment.NewLine;
                if (ds.Tables[0].Rows.Count > 0)
                {

                    List<usuarios> listusuarios = new List<usuarios>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        listusuarios.Add(
                            new usuarios()
                            {
                                id = ds.Tables[0].Rows[i]["id"].ToString(),
                                loggin = ds.Tables[0].Rows[i]["loggin"].ToString(),
                                pass = ds.Tables[0].Rows[i]["pass"].ToString(),
                                nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                                mail = ds.Tables[0].Rows[i]["mail"].ToString(),
                                perfil = ds.Tables[0].Rows[i]["perfil"].ToString(),
                                allow = ds.Tables[0].Rows[i]["allow"].ToString(),
                                comisionprima = ds.Tables[0].Rows[i]["comisionprima"].ToString().Replace(",", "."),
                                comisionpremio = ds.Tables[0].Rows[i]["comisionpremio"].ToString().Replace(",", "."),
                                codigoproductor = ds.Tables[0].Rows[i]["codigoproductor"].ToString(),
                                codorganizador = ds.Tables[0].Rows[i]["codorganizador"].ToString(),
                                codestado = ds.Tables[0].Rows[i]["codestado"].ToString(),
                                codempresa = ds.Tables[0].Rows[i]["codempresa"].ToString(),
                                adminempresa = ds.Tables[0].Rows[i]["adminempresa"].ToString()
                            }
                        );


                        textBox1.Text += "Nombre: " + ds.Tables[0].Rows[i]["nombre"].ToString() + Environment.NewLine;

                        cantregistros = i + 1;
                        consecutivos += ds.Tables[0].Rows[i]["nombre"].ToString() + ",";
                    }

                    this.data.listusuarios = listusuarios;

                }

                if (cantregistros > 0)
                    mi.numeracion = consecutivos;


                mi.cantidad_registros = cantregistros.ToString();
                miUsuarios = mi;

                textBox1.Text += Environment.NewLine + Environment.NewLine;

            }
            else
            {
                MessageBox.Show("La migración de Usuarios del " + fecha + " ya se ha hecho");
            }
            */



        }

        private void migrarGrupoBarrios(string fecha)
        {


            migraciones mi = new migraciones();
            mi.tipo = "EXPORTACION";
            mi.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mi.tabla = "grupobarrios";
            if (!mi.exist())
            {
                int cantregistros = 0;
                string consecutivos = "";

                gruposbarrios gbar = new gruposbarrios();
                DataSet ds = gbar.get_all_fecha(fecha);

                textBox1.Text += "EXPORTACIÓN GRUPOS BARRIOS / " + ds.Tables[0].Rows.Count + " Registros " + Environment.NewLine;
                if (ds.Tables[0].Rows.Count > 0)
                {

                    List<gruposbarrios> listgrupobarrios = new List<gruposbarrios>();
                    string aux = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        listgrupobarrios.Add(
                            new gruposbarrios()
                            {
                                id = ds.Tables[0].Rows[i]["id"].ToString(),
                                nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                                idbarrio = ds.Tables[0].Rows[i]["idbarrio"].ToString(),
                                nombrebarrio = ds.Tables[0].Rows[i]["nombrebarrio"].ToString().Replace(",", ""),
                                ultmod = Convert.ToDateTime(ds.Tables[0].Rows[i]["ultmod"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                codestado = ds.Tables[0].Rows[i]["codestado"].ToString()
                            }
                        );

                        if(ds.Tables[0].Rows[i]["nombre"].ToString() != aux)
                        {
                            textBox1.Text += "Nombre: " + ds.Tables[0].Rows[i]["nombre"].ToString() + Environment.NewLine;
                            aux = ds.Tables[0].Rows[i]["nombre"].ToString();
                        }
                        

                        cantregistros = i + 1;
                        consecutivos += ds.Tables[0].Rows[i]["nombre"].ToString() + ",";
                    }

                    this.data.listgrupobarrios = listgrupobarrios;

                }

                if (cantregistros > 0)
                    mi.numeracion = consecutivos;


                mi.cantidad_registros = cantregistros.ToString();
                miGrupoBarrios = mi;

                textBox1.Text += Environment.NewLine + Environment.NewLine;

            }
            else
            {
                Console.WriteLine("La migración de GruposBarrios del " + fecha + " ya se ha hecho");
            }




        }


        private void migrarBarrios(string fecha)
        {


            migraciones mi = new migraciones();
            mi.tipo = "EXPORTACION";
            mi.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mi.tabla = "barrios";
            /*if (!mi.exist())
            {*/
                int cantregistros = 0;
                string consecutivos = "";

                barrios bar = new barrios();
                DataSet ds = bar.get_all_fecha(fecha);

                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Console.WriteLine("EXPORTACIÓN BARRIOS / " + ds.Tables[0].Rows.Count + " Registros " + Environment.NewLine);
                    textBox1.Text += "EXPORTACIÓN BARRIOS / " + ds.Tables[0].Rows.Count + " Registros " + Environment.NewLine;

                    List<barrios> listbarrios = new List<barrios>();
                    
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        listbarrios.Add(
                            new barrios()
                            {
                                id = ds.Tables[0].Rows[i]["id"].ToString(),
                                nombre = ds.Tables[0].Rows[i]["nombre"].ToString().Replace(",",""),
                                telefono = ds.Tables[0].Rows[i]["telefono"].ToString(),
                                direccion = ds.Tables[0].Rows[i]["direccion"].ToString(),
                                email = ds.Tables[0].Rows[i]["email"].ToString(),
                                sub_barrio = ds.Tables[0].Rows[i]["sub_barrio"].ToString(),
                                clase_barrio = ds.Tables[0].Rows[i]["clase_barrio"].ToString(),
                                suma_muerte = ds.Tables[0].Rows[i]["suma_muerte"].ToString(),
                                suma_gm = ds.Tables[0].Rows[i]["suma_gm"].ToString(),
                                suma_rc = ds.Tables[0].Rows[i]["suma_rc"].ToString(),
                                exige = ds.Tables[0].Rows[i]["exige"].ToString(),
                                observaciones = ds.Tables[0].Rows[i]["observaciones"].ToString(),
                                ultmod = Convert.ToDateTime(ds.Tables[0].Rows[i]["ultmod"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                user_edit = ds.Tables[0].Rows[i]["user_edit"].ToString(),
                                codestado = ds.Tables[0].Rows[i]["codestado"].ToString()
                            }
                        );


                        textBox1.Text += "Nombre: " + ds.Tables[0].Rows[i]["nombre"].ToString() + Environment.NewLine;

                        cantregistros = i + 1;
                        consecutivos += ds.Tables[0].Rows[i]["nombre"].ToString() + ",";
                    }
                    Console.WriteLine("EXPORT BARRIOS");
                    this.data.listbarrios = listbarrios;

                }

                if (cantregistros > 0)
                    mi.numeracion = consecutivos;


                mi.cantidad_registros = cantregistros.ToString();
                miBarrios = mi;

                textBox1.Text += Environment.NewLine + Environment.NewLine;

            /*}
            else
            {
                MessageBox.Show("La migración de Barrios del " + fecha + " ya se ha hecho");
            }*/




        }


        private void migrarCoberturas(string fecha)
        {


            migraciones mi = new migraciones();
            mi.tipo = "EXPORTACION";
            mi.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mi.tabla = "coberturas";
            if (!mi.exist())
            {
                int cantregistros = 0;
                string consecutivos = "";

                coberturas cob = new coberturas();
                DataSet ds = cob.get_all_fecha(fecha);

                textBox1.Text += "EXPORTACIÓN COBERTURAS / " + ds.Tables[0].Rows.Count + " Registros " + Environment.NewLine;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    List<coberturas> listcoberturas = new List<coberturas>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        listcoberturas.Add(
                            new coberturas()
                            {
                                reg = ds.Tables[0].Rows[i]["reg"].ToString(),
                                nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                                suma = ds.Tables[0].Rows[i]["suma"].ToString(),
                                gastos = ds.Tables[0].Rows[i]["gastos"].ToString(),
                                deducible = ds.Tables[0].Rows[i]["deducible"].ToString(),
                                vrMensual = ds.Tables[0].Rows[i]["vrMensual"].ToString(),
                                vrTrimestral = ds.Tables[0].Rows[i]["vrTrimestral"].ToString(),
                                vrSemestral = ds.Tables[0].Rows[i]["vrSemestral"].ToString(),
                                x21 = ds.Tables[0].Rows[i]["x21"].ToString(),
                                x32 = ds.Tables[0].Rows[i]["x32"].ToString(),
                                x64 = ds.Tables[0].Rows[i]["x64"].ToString(),
                                ultmod = Convert.ToDateTime( ds.Tables[0].Rows[i]["ultmod"].ToString() ).ToString("yyyy-MM-dd HH:mm:ss"),
                                user_edit = ds.Tables[0].Rows[i]["user_edit"].ToString(),
                                codestado = ds.Tables[0].Rows[i]["codestado"].ToString()

                            }
                        );


                        textBox1.Text += "Nombre: " + ds.Tables[0].Rows[i]["nombre"].ToString() + Environment.NewLine;

                        cantregistros = i + 1;
                        consecutivos += ds.Tables[0].Rows[i]["nombre"].ToString() + ",";
                    }

                    this.data.listcoberturas = listcoberturas;

                }

                if (cantregistros > 0)
                    mi.numeracion = consecutivos;


                mi.cantidad_registros = cantregistros.ToString();
                miCoberturas = mi;

                textBox1.Text += Environment.NewLine + Environment.NewLine;

            }
            else
            {
                MessageBox.Show("La migración de Coberturas del " + fecha + " ya se ha hecho");
            }




        }

        private void chTodo_CheckedChanged(object sender, EventArgs e)
        {
            bool ch = chTodo.Checked;
            foreach (CheckBox item in groupBox1.Controls)
            {
                item.Checked = ch;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Width = 532;
            if (button2.Text == ">")
            {
                
                button2.Text = "<";
            }
            else
            {
                if (button2.Text == "<")
                {
                    this.Width = 379;
                    button2.Text = ">";
                }
            }
            
        }

        private bool migrarPropuestas(string fecha)
        {
            migraciones mi = new migraciones();
            mi.tipo = "EXPORTACION";
            mi.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            mi.tabla = "propuestas";

            if (!mi.exist()){
                int cantregistros = 0;
                string consecutivos = "";

                puntodeventa punt = new puntodeventa();
                
                propuestas pro = new propuestas();
                DataSet ds = pro.get_all_date(MDIParent1.prefijo);

                textBox1.Text += "EXPORTACIÓN PROPUESTAS / " + ds.Tables[0].Rows.Count + " Registros " + Environment.NewLine;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    
                    List<propuestas> listpropuestas = new List<propuestas>();
                    listlineas = new List<lineas_propuestas>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        listpropuestas.Add(
                            new propuestas()
                            {
                                id = ds.Tables[0].Rows[i]["id"].ToString(),
                                documento = ds.Tables[0].Rows[i]["documento"].ToString(),
                                nombre = ds.Tables[0].Rows[i]["nombre"] == null ? "" : ds.Tables[0].Rows[i]["nombre"].ToString(),
                                num_polizas = ds.Tables[0].Rows[i]["num_polizas"].ToString(),
                                meses = ds.Tables[0].Rows[i]["meses"].ToString(),
                                id_cobertura = ds.Tables[0].Rows[i]["id_cobertura"].ToString(),
                                id_barrio = ds.Tables[0].Rows[i]["id_barrio"].ToString(),
                                nueva_poliza = ds.Tables[0].Rows[i]["nueva_poliza"].ToString(),
                                premio = ds.Tables[0].Rows[i]["premio"].ToString(),
                                premio_total = ds.Tables[0].Rows[i]["premio_total"].ToString(),
                                fechaDesde = Convert.ToDateTime( ds.Tables[0].Rows[i]["fechaDesde"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                fechaHasta = Convert.ToDateTime( ds.Tables[0].Rows[i]["fechaHasta"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                clausula = ds.Tables[0].Rows[i]["clausula"].ToString(),
                                barrio_beneficiario = ds.Tables[0].Rows[i]["barrio_beneficiario"].ToString(),
                                ultmod = Convert.ToDateTime(ds.Tables[0].Rows[i]["ultmod"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                user_edit = ds.Tables[0].Rows[i]["user_edit"].ToString(),
                                codestado = ds.Tables[0].Rows[i]["codestado"].ToString(),
                                cobertura_suma = ds.Tables[0].Rows[i]["cobertura_suma"].ToString(),
                                cobertura_deducible = ds.Tables[0].Rows[i]["cobertura_deducible"].ToString(),
                                cobertura_gastos = ds.Tables[0].Rows[i]["cobertura_gastos"].ToString(),
                                promocion = ds.Tables[0].Rows[i]["promocion"].ToString(),
                                paga = ds.Tables[0].Rows[i]["paga"].ToString(),
                                fecha_paga = Convert.ToDateTime(ds.Tables[0].Rows[i]["fecha_paga"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                                referencia = ds.Tables[0].Rows[i]["referencia"].ToString(),
                                prima = ds.Tables[0].Rows[i]["prima"].ToString(),
                                master = ds.Tables[0].Rows[i]["master"].ToString(),
                                organizador = ds.Tables[0].Rows[i]["organizador"].ToString(),
                                formadepago = ds.Tables[0].Rows[i]["formadepago"].ToString(),
                                productor = ds.Tables[0].Rows[i]["productor"].ToString(),
                                prefijo = ds.Tables[0].Rows[i]["prefijo"].ToString(),
                                idpropuesta = ds.Tables[0].Rows[i]["idpropuesta"].ToString(),
                                fecha_nacimiento = ds.Tables[0].Rows[i]["fecha_nacimiento"] != null ? Convert.ToDateTime(ds.Tables[0].Rows[i]["fecha_nacimiento"].ToString()).ToString("yyyy-MM-dd") : "1900-01-01",
                                nota = ds.Tables[0].Rows[i]["nota"].ToString(),
                                data_barrios = ds.Tables[0].Rows[i]["data_barrios"].ToString(),
                                codempresa = ds.Tables[0].Rows[i]["codempresa"].ToString(),
                                version = ds.Tables[0].Rows[i]["version"] != null ? Convert.ToInt16(ds.Tables[0].Rows[i]["version"].ToString()) : 0,
                                valor_pagado = ds.Tables[0].Rows[i]["valor_pagado"].ToString(),
                                imputacion = ds.Tables[0].Rows[i]["imputacion"].ToString(),
                                fecha_comprobante = ds.Tables[0].Rows[i]["fecha_comprobante"].ToString() != "" ? Convert.ToDateTime(ds.Tables[0].Rows[i]["fecha_comprobante"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") : "1900-01-01",
                            }
                        );

                        this.migrarListaTomador(ds.Tables[0].Rows[i]["documento"].ToString());
                        this.migrarLineasPropuestas(ds.Tables[0].Rows[i]["idpropuesta"].ToString(), ds.Tables[0].Rows[i]["prefijo"].ToString());
                        //this.migrarBarriosPropuestas(ds.Tables[0].Rows[i]["idpropuesta"].ToString(), ds.Tables[0].Rows[i]["prefijo"].ToString());
                        


                        textBox1.Text += "Id: "+ ds.Tables[0].Rows[i]["prefijo"].ToString() + ds.Tables[0].Rows[i]["idpropuesta"].ToString() +
                            " Tomador: "+ ds.Tables[0].Rows[i]["documento"].ToString()+ Environment.NewLine;

                        cantregistros = i + 1;
                        consecutivos += ds.Tables[0].Rows[i]["prefijo"].ToString() +":"+ ds.Tables[0].Rows[i]["idpropuesta"].ToString() + ",";
                    }
                    
                    this.data.listpropuestas = listpropuestas;
                    


                }

                if (cantregistros > 0)
                    mi.numeracion = consecutivos;

                mi.cantidad_registros = cantregistros.ToString();
                miPropuestas = mi;
                textBox1.Text += Environment.NewLine + Environment.NewLine;

                return true;

            }
            else
            {
                MessageBox.Show("La migración de Propuestas del " + fecha + " ya se ha hecho");
                return false;
            }

            


        }

        private void frmMigraciones_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(cargadatos.Visible == true)
            {
                e.Cancel = true;
            }
        }

        private void cbReset_CheckedChanged(object sender, EventArgs e)
        {
            if (cbReset.Checked && this.flaginstalacion == false)
            {
                if (MessageBox.Show("Si selecciona información base el sistema traerá toos los datos base como si se estuviera instalando ésta aplicación\n esto tomará varios minutos ¿Está seguro(a) de hacerlo?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    cbReset.Checked = false;
                }
            }
            
        }

        private void migrarListaTomador(string doc)
        {
            clientes cli = new clientes();
            DataSet ds = cli.get(doc);

            bool err = false;
            

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    try
                    {
                        this.listtomador.Add(
                            new clientes()
                            {
                                id = ds.Tables[0].Rows[i]["id"].ToString(),
                                nombres = ds.Tables[0].Rows[i]["nombres"].ToString(),
                                apellidos = ds.Tables[0].Rows[i]["apellidos"].ToString(),
                                telefono = ds.Tables[0].Rows[i]["telefono"].ToString(),
                                direccion = ds.Tables[0].Rows[i]["direccion"].ToString(),
                                email = ds.Tables[0].Rows[i]["email"].ToString(),
                                ciudad = ds.Tables[0].Rows[i]["ciudad"].ToString(),
                                codpostal = ds.Tables[0].Rows[i]["codpostal"].ToString(),
                                localidad = ds.Tables[0].Rows[i]["localidad"].ToString(),
                                fecha_nacimiento = ds.Tables[0].Rows[i]["fecha_nacimiento"].ToString() != "" ? Convert.ToDateTime(ds.Tables[0].Rows[i]["fecha_nacimiento"].ToString()).ToString("yyyy-MM-dd") : "1900-01-01",
                                tipo_id = ds.Tables[0].Rows[i]["tipo_id"].ToString(),
                                sexo = ds.Tables[0].Rows[i]["sexo"].ToString(),
                                situacion = ds.Tables[0].Rows[i]["situacion"].ToString(),
                                ultmod = ds.Tables[0].Rows[i]["ultmod"].ToString() != "" ? Convert.ToDateTime(ds.Tables[0].Rows[i]["ultmod"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                user_edit = ds.Tables[0].Rows[i]["user_edit"].ToString(),
                                codestado = ds.Tables[0].Rows[i]["codestado"].ToString(),
                                codempresa = ds.Tables[0].Rows[i]["codempresa"].ToString()
                            }
                        
                        );

                        err = true;
                    }
                    catch (Exception ex)
                    {
                        logs log = new logs();
                        log.newError("Tf205", "Error al enviar tomador en migraciones " + ex.Message+ "\n"+ ds.Tables[0].Rows[i]["id"].ToString()+"-"+ds.Tables[0].Rows[i]["nombres"].ToString());
                        err = false;
                    }
                    

                }

                if(err)
                    this.data.listtomador = listtomador;
            }


        }


        private void migrarLineasPropuestas(string id,string prefijo)
        {
                lineas_propuestas pro = new lineas_propuestas();
                DataSet ds = pro.get_idprefijo(id, prefijo);

                if (ds.Tables[0].Rows.Count > 0)
                {
                
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        this.listlineas.Add(
                        new lineas_propuestas()
                        {
                            id = ds.Tables[0].Rows[i]["id"].ToString(),
                            id_propuesta = ds.Tables[0].Rows[i]["id_propuesta"].ToString(),
                            documento = ds.Tables[0].Rows[i]["documento"].ToString(),
                            tipo_documento = ds.Tables[0].Rows[i]["tipo_documento"].ToString(),
                            apellidos = ds.Tables[0].Rows[i]["apellidos"].ToString(),
                            nombres = ds.Tables[0].Rows[i]["nombres"].ToString(),
                            fecha_nacimiento = Convert.ToDateTime(ds.Tables[0].Rows[i]["fecha_nacimiento"].ToString()).ToString("yyyy-MM-dd"),
                            id_actividad = ds.Tables[0].Rows[i]["id_actividad"].ToString(),
                            id_clasificacion = ds.Tables[0].Rows[i]["id_clasificacion"].ToString(),
                            premio = ds.Tables[0].Rows[i]["premio"].ToString(),
                            ultmod = Convert.ToDateTime(ds.Tables[0].Rows[i]["ultmod"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                            user_edit = ds.Tables[0].Rows[i]["user_edit"].ToString(),
                            codestado = ds.Tables[0].Rows[i]["codestado"].ToString(),
                            prefijo = ds.Tables[0].Rows[i]["prefijo"].ToString(),
                            actividad = ds.Tables[0].Rows[i]["actividad"].ToString(),
                            clasificacion = ds.Tables[0].Rows[i]["clasificacion"].ToString(),
                            fechaDesde = Convert.ToDateTime(ds.Tables[0].Rows[i]["fechaDesde"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                            fechaHasta = Convert.ToDateTime(ds.Tables[0].Rows[i]["fechaHasta"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                            codempresa = ds.Tables[0].Rows[i]["codempresa"].ToString()
                            }
                        );

                        this.migrarListaTomador(ds.Tables[0].Rows[i]["documento"].ToString());

                }

                
                this.data.listlineaspropuestas = listlineas;
            }

                
        }

        private void migrarBarriosPropuestas(string id, string prefijo)
        {

                barrios_propuesta pro = new barrios_propuesta();
                DataSet ds = pro.get_idprefijo(id,prefijo);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                    listbarriospropuestas.Add(
                            new barrios_propuesta()
                            {
                                id = ds.Tables[0].Rows[i]["id"].ToString(),
                                id_propuesta = ds.Tables[0].Rows[i]["id_propuesta"].ToString(),
                                id_barrio = ds.Tables[0].Rows[i]["id_barrio"].ToString(),
                                nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                                ultmod = Convert.ToDateTime( ds.Tables[0].Rows[i]["ultmod"].ToString() ).ToString("yyyy-MM-dd HH:mm:ss"),
                                user_edit = ds.Tables[0].Rows[i]["user_edit"].ToString(),
                                codestado = ds.Tables[0].Rows[i]["codestado"].ToString(),
                                prefijo = ds.Tables[0].Rows[i]["prefijo"].ToString(),
                                codempresa = ds.Tables[0].Rows[i]["codempresa"].ToString()
                            }
                        );
                    }
                    this.data.listbarriospropuestas = listbarriospropuestas;
                }
        }


        private async Task<bool> enviar_json(string json)
        {
            try
            {
                string url = MDIParent1.apiuri +"/api/propuestas";
                if(MDIParent1.apiuri.IndexOf("https") > -1)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                WebRequest _request = WebRequest.Create(url);
                _request.Method = "POST";
                _request.ContentType = "application/json;charset=UTF-8";
                _request.Timeout = 36000000;
                using (var osw = new StreamWriter(_request.GetRequestStream()))
                {
                    osw.Write(json);
                    osw.Flush();
                    osw.Close();
                }

                WebResponse _response = _request.GetResponse();
                using (var ors = new StreamReader(_response.GetResponseStream()))
                {
                    string res = ors.ReadToEnd().Trim();
                    logs l = new logs();
                    l.newError("EXP201", "Se genera la exportación " + (res.Length > 200 ? res.Substring(0, 200) : res));

                }
                return true;
            }
            catch (Exception ex)
            {
                logs log = new logs();
                log.newError("MIG500", "Error al enviar datos propuestas " + ex.Message + " / "+ json);
                return false;
            }
            
        }


        private bool revisarurlapi()
        {

            puntodeventa punt = new puntodeventa();
            if (punt.get_principal())
            {
                this.data.api_token = punt.apitoken;
                this.data.apiversion = MDIParent1.apiversion;
                this.urlapi = punt.urlapi;
            }
            else if (punt.get_colaborador())
            {
                this.data.api_token = punt.apitoken;
                this.data.apiversion = MDIParent1.apiversion;
                this.urlapi = punt.urlapi;
            }

            if(this.urlapi == ""){
                MessageBox.Show("La configuración del punto de venta no está completa, la dirección del servidor no existe",
                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }else{
                return true;
            }
            
        }


        private void frmMigraciones_Load(object sender, EventArgs e)
        {
            
            expimp.SelectedIndex = 1;
            usuarios usu = new usuarios();
            DataSet ds = usu.get_all_allow();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[i]["loggin"].ToString());
            }
            this.datastar();
        }

        private void datastar()
        {
            this.data.rolpuntodeventa = MDIParent1.rolPuntodeventa;
            this.data.prefpuntodeventa = MDIParent1.prefijo;

            if (this.data.rolpuntodeventa == "")
            {
                MessageBox.Show("El sistema no tiene un colaborador válido al sistema", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (!this.revisarurlapi())
            {
                this.Close();
            }


            
        }
        

        private async void button2_Click(object sender, EventArgs e)
        {

        }
    }

   
}

public class solicitudes
{
    public bool solicitud_propuestas = false;
    public bool solicitud_lineas_propuestas = false;
    public bool solicitud_barrios_propuestas = false;
    public bool solicitud_clientes = false;
    public bool solicitud_usuarios = false;
    public bool solicitud_perfiles = false;
    public bool solicitud_arqueos = false;
    public bool solicitud_rendiciones = false;
    public bool solicitud_lineas_rendiciones = false;
    public bool solicitud_actividades = false;
    public bool solicitud_coberturas = false;
    public bool solicitud_clasificaciones = false;
    public bool solicitud_barrios = false;
    public bool solicitud_gruposbarrios = false;
    public bool solicitud_provincias = false;

}


