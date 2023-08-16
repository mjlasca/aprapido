using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProyectoBrokerDelPuerto
{
    public partial class frmPuntodeventa : Form
    {
        bool edit = false;
        string[] vec = { "",""} ;
        public bool exito = false;
        public bool carga = false;
        string aseguradora, master, organizador, codmaster, codorganizador = "";
        public frmPuntodeventa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtRol.Text != "")
            {
                if (txtRol.Text == "COLABORADOR")
                {
                    if (MessageBox.Show("¿Segur@ desea modificar la información del punto de venta?", "Confirmar validación", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            button1.Enabled = false;
            this.Height = 452;
            if(MDIParent1.versionwindows != "7")
            {
                pictureBox1.Visible = true;
            }
            
                
            this.carga = true;

            this.enviartoken();
            //txtPrefijo.Text = this.prefijo;
        }
        private async void enviartoken()
        {
            MDIParent1.installing = true;
             await Task.Run(() => {
                bool res = this.enviar_json(txtApitoken.Text);
                if (res)
                {
                    puntodeventa punt = new puntodeventa();
                    punt.nombre = txtResponsable.Text;
                    punt.rol = txtRol.Text;
                    punt.apitoken = txtApitoken.Text;
                    punt.prefijo = txtPrefijo.Text;
                    punt.usuario = txtUsuario.Text;
                    punt.user_edit = MDIParent1.sesionUser;
                    punt.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    punt.codestado = "1";
                    punt.urlapi = MDIParent1.apiuri;
                    punt.codempresa = txtCodEmpresa.Text;
                    punt.perfil = txtPerfil.Text;
                    punt.aseguradora = aseguradora;
                    punt.master = master;
                    punt.organizador = organizador;
                    punt.codmaster = codmaster;
                    punt.codorganizador = codorganizador;

                     if (!this.edit)
                    {
                        if (punt.save()){

                            MDIParent1.rolPuntodeventa = punt.rol;
                            MDIParent1.codempresa = txtCodEmpresa.Text;
                            MDIParent1.apiversion = "3";
                            MDIParent1.prefijo = txtPrefijo.Text;

                            Configmaster conf = new Configmaster();
                            conf.codempresa = MDIParent1.codempresa;
                            this.organizadores(conf);
                            conf.updateCodempresa();
                             
                        }
                    }
                    else
                    {
                        punt.update();
                        MDIParent1.rolPuntodeventa = punt.rol;
                        MDIParent1.codempresa = txtCodEmpresa.Text;
                        MDIParent1.apiversion = "3";
                        MDIParent1.prefijo = txtPrefijo.Text;
                    }
                        

                    this.exito = true;
                    usuarios usu = new usuarios();
                    
                    if (usu.get_nombre(punt.usuario) == "")
                    {
                        usu.loggin = punt.usuario;
                        usu.pass = punt.usuario;
                        usu.nombre = punt.nombre;
                        usu.mail = punt.usuario;
                        usu.allow = "1";
                        usu.perfil = punt.perfil;
                        if (punt.perfil == "")
                            usu.perfil = "productorSenior";
                        usu.codestado = "1";
                        usu.adminempresa = "1";
                        usu.save();

                        /*
                        Usuario administrador
                        */
                        usu.loggin = "admin";
                        usu.pass = "1234";
                        usu.nombre = "Administrador punto";
                        usu.mail = "mauriciotamayo@yahoo.com";
                        usu.allow = "1";
                        usu.perfil = "adminpunto";
                        if (punt.perfil != "")
                            usu.perfil = punt.perfil;
                        usu.codestado = "1";
                        usu.adminempresa = "1";
                        usu.save();

                    }

                     


                 }

            });


            /*MDIParent1 f1 = (MDIParent1)this.MdiParent;
            f1.pictureBox3.Visible = true;*/
            frmMigraciones frmMig = new frmMigraciones();
            frmMig.flaginstalacion = true;
            frmMig.cbReset.Checked = true;
            solicitudes s = new solicitudes();
            s.solicitud_propuestas = true;
            s.solicitud_lineas_propuestas = true;
            s.solicitud_clientes = true;
            //s.solicitud_usuarios = true;
            s.solicitud_perfiles = true;
            //s.solicitud_arqueos = true;
            //s.solicitud_rendiciones = true;
            //s.solicitud_lineas_rendiciones = true;
            //s.solicitud_actividades = true;
            //s.solicitud_coberturas = true;
            //s.solicitud_clasificaciones = true;
            s.solicitud_barrios = true;
            //s.solicitud_gruposbarrios = true;
            s.solicitud_provincias = true;
            //s.solicitud_barrios_propuestas = true;

            bool ps = await frmMig.importarData(s,false,true);

            s = new solicitudes();
            s.solicitud_propuestas = true;
            s.solicitud_lineas_propuestas = true;
            //s.solicitud_barrios_propuestas = true;

            bool ps0 = await frmMig.importarData(s);

            /*if (ps)
            {*/
            this.Height = 312;
                pictureBox1.Visible = false;
                this.carga = false;
                this.DialogResult = DialogResult.OK;
            // }
            MDIParent1.installing = false;

        }

        private void organizadores(Configmaster conf)
        {
            conf.nomcodigo = codmaster;
            conf.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            conf.user_edit = "admin";
            conf.nombre = master;
            conf.codestado = "1";
            conf.codempresa = txtCodEmpresa.Text;
            conf.save("MASTER");

            conf = new Configmaster();
            conf.nomcodigo = codorganizador;
            conf.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            conf.user_edit = "admin";
            conf.nombre = organizador;
            conf.codestado = "1";
            conf.codempresa = txtCodEmpresa.Text;
            conf.save("ORGANIZADOR");

            conf = new Configmaster();
            conf.nomcodigo = aseguradora;
            conf.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            conf.user_edit = "admin";
            conf.nombre = aseguradora;
            conf.codestado = "1";
            conf.codempresa = txtCodEmpresa.Text;
            conf.save("ASEGURADORA");
        }

        public bool validar_token(string token, string op = "abierto")
        {
            puntodeventa punt = new puntodeventa();
            punt.getaccesos();

            if(punt.accesos < 0)
            {
                punt.actualizarmenor(op);
            }

            if (op == "cerrado" && punt.accesos == 1)
            {
                punt.accesos = 0;
            }
                
            
            if (punt.accesos > 0)
            {
                punt.actualizaraccesos(op);
                return true;
            }

            try
            {
                

                string url = MDIParent1.apiuri + "/api/confirmarpuntodeventa?op=" + op+ "&api_token=" + token;
                Console.WriteLine("VALIDA TOK " + url);

                WebRequest _request = WebRequest.Create(url);
                _request.Method = "GET";
                _request.ContentType = "application/json;charset=UTF-8";

                /*using (var osw = new StreamWriter(_request.GetRequestStream()))
                {
                    osw.Write(json);
                    osw.Flush();
                    osw.Close();
                }*/

                WebResponse _response = _request.GetResponse();



                using (var ors = new StreamReader(_response.GetResponseStream()))
                {
                    string res = ors.ReadToEnd().Trim();
                }

                usuarios usuaronline = new usuarios();

                if(usuaronline.get_nombre("online") != "")
                {
                    usuaronline.loggin = "online";
                    usuaronline.nombre = "online";
                    usuaronline.pass = "as4f5as46f54as";
                    usuaronline.allow = "1";
                    usuaronline.save();
                }

                

                punt.actualizaraccesos(op);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error, el Api Token es inválido o no se ha asignado");
                return false;
            }

        }

        public bool enviar_json(string token)
        {

            puntodeventa punt = new puntodeventa();
            punt.getaccesos();
            if (punt.accesos > 0)
            {
                punt.actualizaraccesos("abierto");
                return true;
            }

            try
            {
                string url = MDIParent1.apiuri+"/api/confirmarpuntodeventa?api_token=" + token+"&op=abierto";

                
                WebRequest _request = WebRequest.Create(url);
                _request.Method = "GET";
                _request.ContentType = "application/json;charset=UTF-8";

                /*using (var osw = new StreamWriter(_request.GetRequestStream()))
                {
                    osw.Write(json);
                    osw.Flush();
                    osw.Close();
                }*/

                WebResponse _response = _request.GetResponse();



                using (var ors = new StreamReader(_response.GetResponseStream()))
                {
                    string res = ors.ReadToEnd().Trim();
                    
                    Invoke(new MethodInvoker(() =>
                    {
                        this.tarea(res);
                    }));

                    MessageBox.Show("Se ha hecho la confirmación con éxito, más adelante necesitará ingresar con usuario y contraseña\nPor favor ingrese con "+txtUsuario.Text+
                        " como usuario y también contraseña.");
                }

                punt.actualizaraccesos("abierto");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, el Api Token es inválido o no se ha asignado", "Error de validación token", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            

            
        }
        
        private void tarea(string json)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            JObject obj = JObject.Load(reader);

            txtPrefijo.Text = obj["0"]["prefijo"].ToString();
            txtRol.Text = obj["0"]["rol"].ToString();
            txtResponsable.Text = obj["0"]["name"].ToString();
            txtUsuario.Text = obj["0"]["email"].ToString();
            txtCodEmpresa.Text = obj["0"]["codempresa"].ToString();
            txtPerfil.Text = obj["0"]["perfil"].ToString();
            aseguradora = obj["0"]["aseguradora"].ToString();
            master = obj["0"]["master"].ToString();
            organizador = obj["0"]["organizador"].ToString();
            codmaster = obj["0"]["codmaster"].ToString();
            codorganizador = obj["0"]["codorganizador"].ToString();
        }

        private void frmPuntodeventa_Load(object sender, EventArgs e)
        {
            puntodeventa punt = new puntodeventa();
            if (punt.get_principal())
            {
                txtApitoken.Text = punt.apitoken;
                txtPrefijo.Text = punt.prefijo;
                txtResponsable.Text = punt.nombre;
                txtRol.Text = punt.rol;
                txtUsuario.Text = punt.usuario;
                txtCodEmpresa.Text = punt.codempresa;
                //button1.Enabled = false;
            }
            else if (punt.get_colaborador())
            {
                txtApitoken.Text = punt.apitoken;
                txtPrefijo.Text = punt.prefijo;
                txtResponsable.Text = punt.nombre;
                txtRol.Text = punt.rol;
                txtUsuario.Text = punt.usuario;
                txtCodEmpresa.Text = punt.codempresa;
                this.edit = true;
            }
            
        }

        private void frmPuntodeventa_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmPuntodeventa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.carga == true)
                e.Cancel = true;
        }
    }

    
}
