using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace ProyectoBrokerDelPuerto
{
    public partial class frmNuevoPuntodeVenta : Form
    {
        puntodeventa_envio puntenvio = new puntodeventa_envio();
        ConfMaster conMaster = new ConfMaster();
        string errores = "";
        private string urlapi;
        private string tokenapi;
        public frmNuevoPuntodeVenta()
        {
            InitializeComponent();
        }

        public string posabc()
        {
            int cantPuntos = dataGridView1.Rows.Count;
            string nuevoPrefijo = (cantPuntos++) + dataGridView1.Rows[0].Cells["prefijo"].Value.ToString();
            return nuevoPrefijo;
        }

        private async void frmNuevoPuntodeVenta_Load(object sender, EventArgs e)
        {
            importapi im = new importapi();
            bool datapuntos = await im.solicitarpuntosdeventa();
            this.listado();

            if (MDIParent1.rolPuntodeventa == "COLABORADOR")
            {
                button2.Visible = false;
                txtCodEmpresa.Text = MDIParent1.codempresa;
                txtCodEmpresa.Enabled = false;
                if(dataGridView1.Rows.Count > 0)
                {
                    //txtPrefijo.Text = this.posabc();
                    //txtPrefijo.Enabled = false;
                }
                
                
            }


            this.initMaster();

            //PERFILES
            this.llenarCombo();
            

        }

        public void initMaster()
        {
            Configmaster confi = new Configmaster();

            conMaster.nombreaseguradora = confi.get("ASEGURADORA").Tables[0].Rows[0]["nomcodigo"].ToString();
            conMaster.nombremaster = confi.get("MASTER").Tables[0].Rows[0]["nombre"].ToString();
            conMaster.nombreorganizador = confi.get("ORGANIZADOR").Tables[0].Rows[0]["nombre"].ToString();
            conMaster.codmaster = confi.get("MASTER").Tables[0].Rows[0]["nomcodigo"].ToString();
            conMaster.codorganizador = confi.get("ORGANIZADOR").Tables[0].Rows[0]["nomcodigo"].ToString();

            dataGridView2.Rows.Clear();

            dataGridView2.Rows.Add("ASEGURADORA", conMaster.nombreaseguradora);
            dataGridView2.Rows.Add("MASTER", conMaster.nombremaster);
            dataGridView2.Rows.Add("Cod. MASTER", conMaster.codmaster);
            dataGridView2.Rows.Add("ORGANIZADOR", conMaster.nombreorganizador);
            dataGridView2.Rows.Add("Cod.ORGANIZADOR", conMaster.codorganizador);
        }

        public void llenarCombo()
        {
            perfiles per = new perfiles();
            comboPerfil.Items.Clear();
            DataSet ds = per.get_all();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        comboPerfil.Items.Add(ds.Tables[0].Rows[i]["nombre"].ToString());
                    }
                }
            }


        }

        private void listado()
        {
            puntodeventa punt = new puntodeventa();
            DataSet ds = punt.get_all();
            dataGridView1.Rows.Clear();
            string activo = "ACTIVO";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                activo = "ACTIVO";
                if (ds.Tables[0].Rows[i]["codestado"].ToString() == "0")
                    activo = "INACTIVO";
                if(ds.Tables[0].Rows[i]["codempresa"].ToString() == MDIParent1.codempresa || MDIParent1.rolPuntodeventa == "PRINCIPAL")
                {
                    dataGridView1.Rows.Add(
                        ds.Tables[0].Rows[i]["nombre"].ToString(),
                        ds.Tables[0].Rows[i]["usuario"].ToString(),
                        ds.Tables[0].Rows[i]["apitoken"].ToString(),
                        ds.Tables[0].Rows[i]["prefijo"].ToString(),
                        ds.Tables[0].Rows[i]["rol"].ToString(),
                        activo,
                        ds.Tables[0].Rows[i]["usuario"].ToString(),
                        ds.Tables[0].Rows[i]["codestado"].ToString(),
                        ds.Tables[0].Rows[i]["codempresa"].ToString()
                    );
                }
                
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (this.validacion())
            {
                
                puntodeventa punt = new puntodeventa();
                
                if (punt.get_principal() || punt.get_colaborador())
                {
                    this.urlapi = punt.urlapi;
                    this.tokenapi = punt.apitoken;

                }
                this.generarPunto();
                
               
            }
            
        }

        private async void generarPunto()
        {
            bool resp = await validar_token();
            if (resp)
            {
                this.initMaster();
                puntodeventa punt = new puntodeventa();
                if (punt.get_principal())
                {
                    groupBox1.Enabled = false;
                    dataGridView1.Enabled = false;
                    button2.Enabled = false;
                    panelMaster.Visible = true;
                    if (txtCodEmpresa.Text != MDIParent1.codempresa)
                    {
                        conMaster = new ConfMaster();
                        dataGridView2.Rows[0].Cells["datos1"].Value = conMaster.nombreaseguradora;
                        dataGridView2.Rows[1].Cells["datos1"].Value = conMaster.nombremaster;
                        dataGridView2.Rows[2].Cells["datos1"].Value = conMaster.codmaster;
                        dataGridView2.Rows[3].Cells["datos1"].Value = conMaster.nombreorganizador;
                        dataGridView2.Rows[4].Cells["datos1"].Value = conMaster.codorganizador;
                    }

                }
                else
                {
                    txtApitoken.Text = this.generateroken();
                    this.guardarpunto();
                }
                
            }
        }


        public async Task<bool>  validar_token()
        {
            try
            {
                string url = MDIParent1.apiuri + "/api/confirmarpuntodeventa?userpunto=" + txtEmail.Text + "&prefijo=" + txtPrefijo.Text + "&api_token=" + this.tokenapi;
                Console.WriteLine("URL VALIDACION "+url);
                WebRequest _request = WebRequest.Create(url);
                _request.Method = "GET";
                _request.ContentType = "application/json;charset=UTF-8";

                WebResponse _response = _request.GetResponse();

                using (var ors = new StreamReader(_response.GetResponseStream()))
                {
                    string res = ors.ReadToEnd().Trim();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("El email o el prefijo ya existen", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return false;
            }

        }

        private bool validacion()
        {
            this.errores = "";
            validaciones val = new validaciones();


            string[] campos = {};

            if (!val.camposvacios_grupos_condescartados(groupBox1, campos))
            {
                this.errores += "\nNo debe haber campos vacíos";
            }

            
            if (!val.correo(txtEmail.Text))
            {
                this.errores += "\nEl correo electrónico no es válido";
            }

            

            if (this.errores != "")
            {
                MessageBox.Show("Error de validación "+this.errores, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



            return true;
        }

        private async void guardarpunto()
        {
                bool res = await this.migrarPuntodeventa();
                if (res)
                {
                    this.puntocreado();
                    
                }

           
            
        }

        private void puntocreado()
        {
            puntodeventa punt = new puntodeventa();
            punt.nombre = txtResponsable.Text;
            punt.usuario = txtEmail.Text;
            punt.prefijo = txtPrefijo.Text;
            punt.perfil = comboPerfil.Text;
            punt.apitoken = txtApitoken.Text;
            punt.rol = txtRol.Text;
            punt.codestado = "1";
            punt.user_edit = MDIParent1.sesionUser;
            punt.ultmod = DateTime.Now.ToString("yyyy-MM-dd");
            punt.codempresa = txtCodEmpresa.Text;
            punt.aseguradora = conMaster.nombreaseguradora;
            punt.master = conMaster.nombremaster;
            punt.organizador = conMaster.nombreorganizador;
            punt.codmaster = conMaster.codmaster;
            punt.codorganizador = conMaster.codorganizador;

            if (punt.save())
            {
                this.crearusuariolocal();
                this.listado();
                this.limpiarcampos();
            }
            else
            {
                MessageBox.Show("No se pudo guardar el punto de venta, el usuario o el prefijo ya existe", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void crearusuariolocal()
        {
            usuarios usu = new usuarios();
            usu.loggin = txtEmail.Text;
            usu.pass = txtEmail.Text;
            usu.nombre = txtResponsable.Text;
            usu.perfil = comboPerfil.Text;
            usu.mail = txtEmail.Text;
            usu.codestado = "1";
            if (txtCodProductor.Text != "")
                usu.codigoproductor = txtCodProductor.Text;
            usu.save(); 
            
        }



        private async Task<bool> migrarPuntodeventa()
        {
            

            puntodeventa punt = new puntodeventa();
            DataSet ds = punt.get_all();
            List<puntodeventa> listpuntos = new List<puntodeventa>();
            if (ds.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    listpuntos.Add(
                        new puntodeventa()
                        {
                            nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                            prefijo = ds.Tables[0].Rows[i]["prefijo"].ToString(),
                            apitoken = ds.Tables[0].Rows[i]["apitoken"].ToString(),
                            rol = ds.Tables[0].Rows[i]["rol"].ToString(),
                            usuario = ds.Tables[0].Rows[i]["usuario"].ToString(),
                            codempresa = ds.Tables[0].Rows[i]["codempresa"].ToString(),
                            perfil = ds.Tables[0].Rows[i]["perfil"].ToString(),
                            master = ds.Tables[0].Rows[i]["master"].ToString(),
                            organizador = ds.Tables[0].Rows[i]["organizador"].ToString(),
                            aseguradora = ds.Tables[0].Rows[i]["aseguradora"].ToString(),
                            codmaster = ds.Tables[0].Rows[i]["codmaster"].ToString(),
                            codorganizador = ds.Tables[0].Rows[i]["codorganizador"].ToString()

                        }
                    );
                }

                    listpuntos.Add(new puntodeventa()
                    {
                        nombre = txtResponsable.Text,
                        prefijo = txtPrefijo.Text,
                        apitoken = txtApitoken.Text,
                        rol = txtRol.Text,
                        usuario = txtEmail.Text,
                        codempresa = txtCodEmpresa.Text,
                        perfil = comboPerfil.Text,
                        master = conMaster.nombremaster,
                        organizador = conMaster.nombreorganizador,
                        aseguradora = conMaster.nombreaseguradora,
                        codmaster = conMaster.codmaster,
                        codorganizador = conMaster.codorganizador

                    }
                );

                this.puntenvio.listpuntosdeventa = listpuntos;
                this.puntenvio.api_token = this.tokenapi;
                
            }

            

             
            //punt.get_principal();
            //this.puntenvio.api_token = punt.apitoken;
                
            string jsonlistpropuestas = JsonConvert.SerializeObject(this.puntenvio);
            bool res = await this.enviar_json(jsonlistpropuestas);
            if (res)
            {
                return true;
            }

            return false;
                
            
        }

        private async Task<bool> enviar_json(string json)
        {
            Console.WriteLine("\n ------ \n"+json);
            

            try
            {
                string url = MDIParent1.apiuri +"/api/editarpuntodeventa";
                Console.WriteLine(url);
                if (MDIParent1.apiuri.IndexOf("https") > -1)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebRequest _request = WebRequest.Create(url);
                _request.Method = "POST";
                _request.ContentType = "application/json;charset=UTF-8";
                

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

                    MessageBox.Show("Se ha hecho la migración con éxito");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido crear el punto de venta " + ex.Message);
                return false;
            }


            return true;
        }

        public string generateroken (){
            string cadena = "";
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[60];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            cadena = new String(Charsarr);

            return cadena;
        }

        private void limpiarcampos()
        {
            txtResponsable.Text = "";
            txtEmail.Text = "";
            txtPrefijo.Text = "";
            txtApitoken.Text = "";
            txtCodProductor.Text = "";
            groupBox1.Enabled = true;
            dataGridView1.Enabled = true;
            button2.Enabled = true;
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtApitoken.Text = this.generateroken();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow.Cells["responsable"].Value != null)
            {
                if(dataGridView1.CurrentRow.Cells["rol"].Value.ToString() == "PRINCIPAL")
                {
                    MessageBox.Show("Por seguridad no puede inhabilitar el punto de venta principal", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if(MessageBox.Show("¿Segur@ desea habilitar o desahabilitar éste punto de venta?", "Confirmar Habilitación o Inhabilitación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    puntodeventa punt = new puntodeventa();
                    punt.apitoken = this.generateroken();
                    punt.usuario = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells["codestado"].Value.ToString() == "0")
                        punt.inhabilitarpunto(1);
                    else
                        punt.inhabilitarpunto();

                    this.listado();
                    this.migrarPuntodeventa();
                }

                
            }
        }



        private void btnEnviarMaster_Click(object sender, EventArgs e)
        {
            panelMaster.Visible = false;
            conMaster.nombreaseguradora = dataGridView2.Rows[0].Cells["datos1"].Value.ToString();
            conMaster.nombremaster = dataGridView2.Rows[1].Cells["datos1"].Value.ToString();
            conMaster.codmaster = dataGridView2.Rows[2].Cells["datos1"].Value.ToString();
            conMaster.nombreorganizador = dataGridView2.Rows[3].Cells["datos1"].Value.ToString();
            conMaster.codorganizador = dataGridView2.Rows[4].Cells["datos1"].Value.ToString();
            
            txtApitoken.Text = this.generateroken();
            this.guardarpunto();
            
        }

        private void btnOmitir_Click(object sender, EventArgs e)
        {
            panelMaster.Visible = false;
            conMaster = new ConfMaster();
            
            txtApitoken.Text = this.generateroken();
            this.guardarpunto();
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            panelMaster.Visible = false;
            dataGridView1.Enabled = true;
            groupBox1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
        }
    }

    class puntodeventa_envio
    {
        public string api_token { get; set; }
        public List<puntodeventa> listpuntosdeventa { get; set; }
    }

}

public class ConfMaster
{
    // Auto-implemented properties.
    public string nombremaster { get; set; } = string.Empty;
    public string nombreorganizador{ get; set; } = string.Empty;
    public string nombreaseguradora { get; set; } = string.Empty;
    public string codmaster { get; set; } = string.Empty;
    public string codorganizador { get; set; } = string.Empty;

}


