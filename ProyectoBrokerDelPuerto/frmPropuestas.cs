using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProyectoBrokerDelPuerto
{
    public partial class frmPropuestas : Form
    {
        string query_estado = "";
        string[] query_Vector = { "","",""};
        int cont = 0;
        bool vencidas_mostrar = true;
        int[] access;
        string urlapi = "";
        paypro pay = new paypro();
        public frmPropuestas()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            /*bool arqueocerrrado = false;
            arqueos ar = new arqueos();
            if (ar.get_fecha_user(DateTime.Now.ToString("yyyy-MM-dd"), MDIParent1.sesionUser))
            {
                if(ar.supervisor != "" && ar.supervisor != null)
                {
                    arqueocerrrado = true;
                }
            }*/


            propuestaencurso proencurso = new propuestaencurso();
            proencurso.usuario = MDIParent1.sesionUser;
            proencurso.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            proencurso.ip = this.ipequipo();
            proencurso.save();

            frmNuevaPropuesta frm = new frmNuevaPropuesta();
            frm.ShowDialog();

           

            if (frm.DialogResult == DialogResult.OK)
            {
                proencurso = new propuestaencurso();
                proencurso.usuario = MDIParent1.sesionUser;
                proencurso.ip = this.ipequipo();
                proencurso.delete();
                this.busqueda_grid();
            }

        }


        private string ipequipo()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            Console.WriteLine(hostName);
            // Get the IP  
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

            return myIP;
        }

        private void chTodas_CheckedChanged(object sender, EventArgs e)
        {
            if (chTodas.Checked)
            {
                chVigentes.Checked = true;
                chNoVigentes.Checked = true;
                chAnuladas.Checked = true;
            }else {
                chVigentes.Checked = false;
                chNoVigentes.Checked = false;
                chAnuladas.Checked = false;
            }
        }

        private void busqueda_grid()
        {
            

            for(int i = 0; i < 3; i++)
            {
            
                if (query_Vector[i] != "")
                {
                    if (i == 1){
                        if(query_Vector[0] != "")
                        {
                            query_estado += " OR " + query_Vector[i];
                        }
                        else
                        {
                            query_estado += query_Vector[i];
                        }
                    }else if (i == 2)
                    {
                        if (query_Vector[0] != "" || query_Vector[1] != "")
                        {
                            query_estado += " OR " + query_Vector[i];
                        }
                        else
                        {
                            query_estado += query_Vector[i];
                        }
                    }
                    else
                    {
                        query_estado += query_Vector[i];
                    }

                }


            }



            if (pagado_ch.Checked)
            {
                if (query_estado != "")
                    query_estado += " AND t1.paga = 1 ";
                else
                    query_estado = " t1.paga = 1 ";

            }
                
            if (sinpagar_ch.Checked)
            {
                if (query_estado != "")
                {
                    query_estado += " AND t1.paga = 0 ";

                }
                else
                    query_estado = " t1.paga = 0 ";

            }
                

            if (query_estado != "")
            {
                query_estado = "( " + query_estado + " ) AND ";
            }
                


            propuestas pro = new propuestas();
            pro.user_edit = empleado_txt.Text;
            if (empleado_txt.Text == "Todos")
                pro.user_edit = "";
            pro.referencia = referencia_txt.Text;
            DataSet ds = pro.get_all_busqueda(busqueda.Text, query_estado, fec1.Value.ToString("yyyy-MM-dd"), fec2.Value.ToString("yyyy-MM-dd"));
            dataGridView1.Rows.Clear();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string pagado = "PAGADO";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        pagado = "PAGADO";
                        if (ds.Tables[0].Rows[i]["paga"].ToString() == "0")
                            pagado = "SIN PAGAR";
                       dataGridView1.Rows.Add(
                           ds.Tables[0].Rows[i]["referencia"].ToString(),
                           ds.Tables[0].Rows[i]["prefijo"].ToString(),
                           ds.Tables[0].Rows[i]["idpropuesta"].ToString(),
                           ds.Tables[0].Rows[i]["idpropuesta"].ToString(), 
                           ds.Tables[0].Rows[i]["documento"].ToString(),
                           ds.Tables[0].Rows[i]["nombres"].ToString() + " "+ ds.Tables[0].Rows[i]["apellidos"].ToString(),
                           ds.Tables[0].Rows[i]["ultmod"].ToString(), 
                           ds.Tables[0].Rows[i]["id_cobertura"].ToString(),
                           ds.Tables[0].Rows[i]["nombreuser"].ToString(),
                           ds.Tables[0].Rows[i]["fechaHasta"].ToString(),
                           ds.Tables[0].Rows[i]["premio_total"].ToString(),
                           ds.Tables[0].Rows[i]["codestado"].ToString(),
                           ds.Tables[0].Rows[i]["formadepago"].ToString(),
                           pagado
                          );
                    }
                }
            }

           
            

            query_estado = "";

            this.color_filas();
        }

        private void color_filas()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["estado"].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells["estado"].Value.ToString() == "2")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    }
                    if (dataGridView1.Rows[i].Cells["estado"].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }

            }
        }

        private void visibleLibreDeuda()
        {
            usuarios usuallow = new usuarios();
            usuallow.loggin = MDIParent1.sesionUser;
            DataSet dsUsu = usuallow.get();
            if (dsUsu.Tables[0].Rows[0]["allow"].ToString() != "1")
            {
                libreDeuda_btn.Visible = false;
            }
        }

        private void frmPropuestas_Load(object sender, EventArgs e)
        {
            usuarios usuallow = new usuarios();
            usuallow.loggin = MDIParent1.sesionUser;
            this.visibleLibreDeuda();

            this.access = usuallow.accessperfil("propuestas");

            button2.Visible = Convert.ToBoolean(this.access[1]);
            btnVer.Visible = Convert.ToBoolean(this.access[1]);
            btnPagar.Visible = Convert.ToBoolean(this.access[1]);
            btnAnular.Visible = Convert.ToBoolean(this.access[2]);


            

            usuarios usus = new usuarios();
            DataSet dsu = usus.get_all();
            empleado_txt.Items.Add("Todos");
            for(int i = 0; i < dsu.Tables[0].Rows.Count; i++)
            {
                empleado_txt.Items.Add(dsu.Tables[0].Rows[i]["nombre"].ToString());
            }

            propuestas pro = new propuestas();
            pro.no_vigente_all(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            dataGridView1.Rows.Clear();

            frmListadoVencimiento frm = new frmListadoVencimiento();
            frm.ShowDialog();

            fec1.Value = fec1.Value.AddDays(-1);
            this.busqueda_grid();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool anular = true;
            if (dataGridView1.CurrentRow.Cells["idPropuesta"].Value != null)
            {
                //Estado 1 Vigente 2 No vigente 0 Anulada
                if (dataGridView1.CurrentRow.Cells["estado"].Value.ToString() == "0")
                {
                    btnAnular.Enabled = false;
                    btnVer.Enabled = false;
                    btnPagar.Enabled = false;
                    libreDeuda_btn.Enabled = false;
                    return;
                }

                propuestas pro = new propuestas();
                DataSet ds = pro.getprefijo(dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString(), dataGridView1.CurrentRow.Cells["idPropuesta"].Value.ToString());
                informes info = new informes();
                DataSet infoDs = info.get_tipo_informe("FINDIA", Convert.ToDateTime(dataGridView1.CurrentRow.Cells["fecha"].Value).ToString("yyyy-MM-dd"));

                if (infoDs.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (DateTime.Compare(Convert.ToDateTime(ds.Tables[0].Rows[0]["ultmod"].ToString()).Date, DateTime.Now.Date) < 0)
                            anular = false;
                    }

                }

                //Estado 1 Vigente 2 No vigente 0 Anulada
                if (dataGridView1.CurrentRow.Cells["estado"].Value.ToString() == "1")
                {
                    btnAnular.Enabled = anular;
                    btnVer.Enabled = true;
                }

                

                if (dataGridView1.CurrentRow.Cells["paga"].Value.ToString() == "SIN PAGAR")
                {
                    btnPagar.Enabled = true;
                    libreDeuda_btn.Enabled = false;
                }
                else
                {
                    btnPagar.Enabled = false;
                    libreDeuda_btn.Enabled = true;
                }


                if (dataGridView1.CurrentRow.Cells["estado"].Value.ToString() == "2")
                {
                    btnAnular.Enabled = false;
                    btnVer.Enabled = true;
                }

                

                usuarios usu = new usuarios();
                usu.loggin = MDIParent1.sesionUser;
                DataSet dn = usu.get();

                if (dn.Tables[0].Rows[0]["allow"] != null)
                {

                    if (dn.Tables[0].Rows[0]["allow"].ToString() != "0" && dn.Tables[0].Rows[0]["allow"].ToString() != "")
                    {
                        btnAnular.Enabled = true;
                    }
                }
            }
            
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            puntodeventa punt = new puntodeventa();

            if (!punt.get_principal())
            {
                if (!punt.get_colaborador())
                {
                    MessageBox.Show("No se ha generado el prefijo para el punto de venta, debe informa al administrador", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }
            

            if (dataGridView1.CurrentRow.Cells["idPropuesta"].Value != null)
            {
                
                frmNuevaPropuesta frm = new frmNuevaPropuesta();
                frm.referencianum_txt.Text = "REF. "+dataGridView1.CurrentRow.Cells["referencia"].Value.ToString();
                propuestas pro = new propuestas();
                DataSet ds = pro.getprefijo(dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString(),dataGridView1.CurrentRow.Cells["idPropuesta"].Value.ToString());

                if(ds.Tables[0].Rows.Count> 0)
                {
                    frm.lblConsecutivo.Text = dataGridView1.CurrentRow.Cells["idPropuesta"].Value.ToString();
                    frm.lblidpropuesta.Text = dataGridView1.CurrentRow.Cells["idpropuestaprefijo"].Value.ToString();
                    frm.formpago_ = ds.Tables[0].Rows[0]["formadepago"].ToString();

                    frm.edit = true;

                    informes info = new informes();
                    DataSet infoDs = info.get_tipo_informe("FINDIA", Convert.ToDateTime(ds.Tables[0].Rows[0]["ultmod"]).ToString("yyyy-MM-dd HH:mm:ss"));
                    if (infoDs.Tables[0].Rows.Count > 0)
                    {
                        //if (DateTime.Compare(Convert.ToDateTime(ds.Tables[0].Rows[0]["ultmod"]), Convert.ToDateTime(infoDs.Tables[0].Rows[0]["ultmod"])) >= 0)
                        //{
                        frm.findeldia = true;
                        //}
                    }

                    frm.codpostalactivo = false;

                    frm.textBox1.Text = ds.Tables[0].Rows[0]["documento"].ToString().Trim();
                    frm.lblPrefijo.Text = dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString();
                    frm.datosInciales();
                    if (ds.Tables[0].Rows[0]["paga"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[0]["paga"].ToString() == "1")
                            frm.paga_ch.Checked = true;
                        else
                            frm.paga_ch.Checked = false;
                    }



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
                        //MessageBox.Show("FEC NAC"+ lineas.Tables[0].Rows[i]["fecha_nacimiento"].ToString());
                        /***/
                        frm.dataGridView1.Rows[i].Cells["idPropuesta"].Value = i;
                        frm.dataGridView1.Rows[i].Cells["nodocumento"].Value = lineas.Tables[0].Rows[i]["documento"].ToString() != "" ? lineas.Tables[0].Rows[i]["documento"].ToString() : "";
                        frm.dataGridView1.Rows[i].Cells["documento"].Value = lineas.Tables[0].Rows[i]["tipo_documento"].ToString() != "" ? lineas.Tables[0].Rows[i]["tipo_documento"].ToString() : "";
                        frm.dataGridView1.Rows[i].Cells["apellido"].Value = lineas.Tables[0].Rows[i]["apellidos"].ToString() != "" ? lineas.Tables[0].Rows[i]["apellidos"].ToString() : "";
                        frm.dataGridView1.Rows[i].Cells["nombre"].Value = lineas.Tables[0].Rows[i]["nombres"].ToString() != "" ? lineas.Tables[0].Rows[i]["nombres"].ToString() : "";
                        frm.dataGridView1.Rows[i].Cells["fecha"].Value = lineas.Tables[0].Rows[i]["fecha_nacimiento"].ToString() != "" ? Convert.ToDateTime(lineas.Tables[0].Rows[i]["fecha_nacimiento"].ToString()).ToString("dd/MM/yyyy") : "";
                        frm.dataGridView1.Rows[i].Cells["actividad"].Value = lineas.Tables[0].Rows[0]["actividad"].ToString();
                        frm.dataGridView1.Rows[i].Cells["clasificacion"].Value = lineas.Tables[0].Rows[0]["clasificacion"].ToString();
                        //frm.dataGridView1.Rows.Add(i,lineas.Tables[0].Rows[i]["documento"].ToString(), lineas.Tables[0].Rows[i]["tipo_documento"].ToString(), lineas.Tables[0].Rows[i]["apellidos_nombres"].ToString(), lineas.Tables[0].Rows[i]["fecha_nacimiento"].ToString(), dd.Tables[0].Rows[0]["nombre"].ToString());

                    }




                    frm.fechapropuesta = Convert.ToDateTime(ds.Tables[0].Rows[0]["ultmod"].ToString());

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

                    /*MessageBox.Show("--> 1 "+ Convert.ToDateTime(dataGridView1.CurrentRow.Cells["fecha"].Value).Date.ToString("yyyy-MM-dd")+
                        " / 2 "+ DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd")); 
                    if (Convert.ToDateTime(dataGridView1.CurrentRow.Cells["fecha"].Value).Date.ToString("yyyy-MM-dd") != DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd"))
                    {*/
                    if ((Convert.ToDateTime(dataGridView1.CurrentRow.Cells["fecha"].Value).Date != DateTime.Now.Date &&
                    Convert.ToDateTime(dataGridView1.CurrentRow.Cells["fecha"].Value).Date.ToString("yyyy-MM-dd") != DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd")) ||
                     frm.findeldia)
                    {
                        frm.formEdit(ds.Tables[0].Rows[0]["ultmod"].ToString());

                        frm.dataGridView1.ReadOnly = true;
                        frm.botEliminarFila = false;

                    }
                    //}




                    /*
                    * ListBox barrios seleccionados seleccionados
                    */

                    if (ds.Tables[0].Rows[0]["data_barrios"] != null && ds.Tables[0].Rows[0]["data_barrios"].ToString() != "")
                    {
                        frm.set_json_barrios_listbox(ds.Tables[0].Rows[0]["data_barrios"].ToString());
                    }
                    else
                    {

                        barrios_propuesta baProp = new barrios_propuesta();
                        baProp.idprefijo = dataGridView1.CurrentRow.Cells["idpropuestaprefijo"].Value.ToString();
                        baProp.prefijo = dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString();
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

                                frm.set_list_barrios(dsBarr.Tables[0]);

                            }

                        }
                    }


                    //totales
                    frm.premio_ = ds.Tables[0].Rows[0]["premio"].ToString();
                    frm.premioTotal_ = ds.Tables[0].Rows[0]["premio_total"].ToString();
                    frm.ocultarBotonGuardar();
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("El estado de la propuesta ha cambiado, actualiza la búsqueda");
                }
                
                
            }
            
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

        private void llenar_grid_estado(string esta)
        {
            propuestas pro = new propuestas();
            DataSet ds = pro.get_all_estado(esta);
            dataGridView1.Rows.Clear();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        clientes cl = new clientes();
                        DataSet dd = cl.get(ds.Tables[0].Rows[i]["documento"].ToString());
                        dataGridView1.Rows.Add(ds.Tables[0].Rows[i]["id"].ToString(), ds.Tables[0].Rows[i]["documento"].ToString(), dd.Tables[0].Rows[0]["apellidos"].ToString() + " " + dd.Tables[0].Rows[0]["nombres"].ToString(), ds.Tables[0].Rows[i]["ultmod"].ToString(), ds.Tables[0].Rows[i]["id_cobertura"].ToString(), ds.Tables[0].Rows[i]["codestado"].ToString());
                    }
                }
            }

            this.color_filas();
        }

        private void busqueda_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void chVigentes_CheckedChanged(object sender, EventArgs e)
        {
            if (chVigentes.Checked)
            {
                query_Vector[0] = " t1.codestado = '1' ";
                this.busqueda_grid();
            }
            else
            {
                query_Vector[0] = "";
                this.busqueda_grid();
            }
        }

        private void chNoVigentes_CheckedChanged(object sender, EventArgs e)
        {
            if (chNoVigentes.Checked)
            {
                query_Vector[1] = " t1.codestado = '2' ";
                this.busqueda_grid();
            }
            else
            {
                query_Vector[1] = "";
                this.busqueda_grid();
            }
        }

        private void chAnuladas_CheckedChanged(object sender, EventArgs e)
        {
            if (chAnuladas.Checked)
            {
                query_Vector[2] = " t1.codestado = '0' ";
                this.busqueda_grid();
            }
            else
            {
                query_Vector[2] = "";
                this.busqueda_grid();
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow.Cells["estado"].Value.ToString() != "0")
            {
                propuestas pro = new propuestas();
                DataSet ds = pro.getprefijo(dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString(), dataGridView1.CurrentRow.Cells["idPropuesta"].Value.ToString());
                informes info = new informes();
                DataSet infoDs = info.get_tipo_informe("FINDIA", Convert.ToDateTime(ds.Tables[0].Rows[0]["ultmod"]).ToString("yyyy-MM-dd"));
                usuarios usu = new usuarios();
                usu.loggin = MDIParent1.sesionUser;
                DataSet dn = usu.get();

                if (infoDs.Tables[0].Rows.Count < 1 || dn.Tables[0].Rows[0]["allow"].ToString() != "0" && dn.Tables[0].Rows[0]["allow"].ToString() != "")
                {
                    if (MessageBox.Show("¿Está seguro(a) de anular la propuesta?", "Anular propuesta", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        pro.anular_propuesta(dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString(), dataGridView1.CurrentRow.Cells["idPropuesta"].Value.ToString());
                        
                        Task.Run(() => {
                            frmMigraciones frmmig = new frmMigraciones();
                            frmmig.exportarPropuestas();
                        });
                        this.busqueda_grid();
                    }
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmListadoVencimiento frm = new frmListadoVencimiento();
            frm.ShowDialog();
        }

        private void fec1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void fec2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.busqueda_grid();
        }

        private void pagado_ch_CheckedChanged(object sender, EventArgs e)
        {
            this.busqueda_grid();
        }

        private void sinpagar_ch_CheckedChanged(object sender, EventArgs e)
        {
            this.busqueda_grid();
        }

        private async void btnPagar_Click(object sender, EventArgs e)
        {

            bool res = await this.pagopropuesta_(dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString(),dataGridView1.CurrentRow.Cells["idpropuestaprefijo"].Value.ToString());
            this.busqueda_grid();
        }

        public async Task<bool> pagopropuesta_(string prefijo_ , string idpropuesta_)
        {
            string formapago = "";
            string compformapago = "";

            frmFormadepago frmpaga = new frmFormadepago();

            frmpaga.ShowDialog();

            if (frmpaga.DialogResult == DialogResult.OK)
            {
                formapago = frmpaga.comboBox1.Text;
                compformapago = frmpaga.textBox2.Text;
            }
            else
            {
                return false;
            }

            propuestas pro = new propuestas();
            pro.idpropuesta = idpropuesta_;
            pro.prefijo = prefijo_;

            DataSet ds = pro.getprefijo(pro.prefijo, pro.idpropuesta);
            if(ds.Tables[0].Rows[0]["version"] != null && ds.Tables[0].Rows[0]["version"].ToString() != "")
            {
                pro.version = Convert.ToInt16(ds.Tables[0].Rows[0]["version"].ToString()) + 2;
                
            }
                

            //pro.pagar(pro.idpropuesta, pro.prefijo, formapago, compformapago);

            frmNuevaPropuesta frm = new frmNuevaPropuesta();
            propuestas pro1 = new propuestas();

            string fechapagar = "";

            if (frm.polizamanana(false))
            {
                fechapagar = DateTime.Now.ToString("yyyyy-MM-dd HH:mm:ss");

                this.pay.tipopago = formapago;
                this.pay.idpropuesta = idpropuesta_;
                this.pay.prefijopropuesta = prefijo_;
                this.pay.compformapago = compformapago;
                this.pay.usuariopaga = MDIParent1.sesionUser;
                this.pay.fecha_paga = fechapagar;
                this.pay.version = pro.version ;

                bool resimport = await this.paypropuesta();
                if (resimport)
                {
                    pro1.pagar(
                        idpropuesta_,
                        prefijo_,
                        formapago,
                        compformapago
                    );
                    MessageBox.Show("El pago se ha hecho con éxito");
                }
                else
                {
                    MessageBox.Show("No se ha podido pagar la propuesta en línea", "Error al pagar propuesta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                arqueos ar = new arqueos();
                usuarios us = new usuarios();
                ar.fechadia = frm.fechacierresiguientediahabil.ToString("yyyy-MM-dd");
                ar.usuario = MDIParent1.sesionUser;
                ar.valorinicial = "0";
                ar.dinerorealcaja = "0";
                ar.valormanual = "0";
                ar.cuadredescuadre = "0";
                ar.nombre = us.get_nombre(ar.usuario);
                ar.user_edit = MDIParent1.sesionUser;
                ar.ultmod = frm.fechacierresiguientediahabil.ToString("yyyy-MM-dd HH:mm:ss");
                ar.codestado = "1";
                ar.save_in();

                fechapagar = frm.fechacierresiguientediahabil.ToString("yyyy-MM-dd HH:mm:ss");


                this.pay.tipopago = formapago;
                this.pay.idpropuesta = idpropuesta_;
                this.pay.prefijopropuesta = prefijo_;
                this.pay.compformapago = compformapago;
                this.pay.usuariopaga = MDIParent1.sesionUser;
                this.pay.fecha_paga = fechapagar;
                this.pay.version = pro.version;

                bool resimport = await this.paypropuesta();
                if (resimport)
                {
                    pro1.pagar(
                        idpropuesta_,
                        prefijo_,
                        formapago,
                        compformapago,
                        fechapagar
                    );
                    MessageBox.Show("El pago se ha hecho con éxito");
                }
                else
                {
                    MessageBox.Show("No se ha podido pagar la propuesta en línea", "Error al pagar propuesta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return true;
        }


        public async Task<bool> paypropuesta()
        {
            puntodeventa punt = new puntodeventa();
            if (punt.get_principal())
            {
                this.pay.api_token = punt.apitoken;
                this.pay.rolpuntodeventa = punt.rol;

                this.urlapi = punt.urlapi;
            }
            else if (punt.get_colaborador())
            {
                this.pay.api_token = punt.apitoken;
                this.pay.rolpuntodeventa = punt.rol;
                this.urlapi = punt.urlapi;
            }
            else
            {
                MessageBox.Show("Por favor debe validar el token, el sistema no encuentra un asociado a éste punto de venta",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            
            this.pay.apiversion = MDIParent1.apiversion;
            this.pay.codempresa = MDIParent1.codempresa;
            this.pay.prefijositio = MDIParent1.prefijo;
            

            string jsonlistpropuestas = JsonConvert.SerializeObject(this.pay);
            bool res = await this.enviardatos(jsonlistpropuestas);
            if (res)
            {
                return true;
            }


            return false;
        }



        private async Task<bool> enviardatos(string json)
        {
            Console.WriteLine("\nJSON PAGO\n"+json);


            try
            {
                string url = MDIParent1.apiuri + "/api/paypro";
                
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
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }



        }

        private void libreDeuda_btn_Click(object sender, EventArgs e)
        {
            this.visibleLibreDeuda();
            if(libreDeuda_btn.Visible == true)
            {
                propuestas pro = new propuestas();
                if (pro.get_paga(dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString(), dataGridView1.CurrentRow.Cells["idPropuesta"].Value.ToString()))
                {
                    frmLibreDeuda frm = new frmLibreDeuda();
                    frm.dsPropuesta = pro.getprefijo(dataGridView1.CurrentRow.Cells["prefijo"].Value.ToString(), dataGridView1.CurrentRow.Cells["idPropuesta"].Value.ToString());
                    frm.ShowDialog();
                }
                else {
                    MessageBox.Show("No puede generar libre deuda de una propuesta no paga", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

class paypro
{
    public string apiversion { get; set; }
    public string tipopago { get; set; }
    public string compformapago { get; set; }
    public string usuariopaga { get; set; }
    public string fecha_paga { get; set; }
    public string prefijositio { get; set; }
    public string idpropuesta { get; set; }
    public string prefijopropuesta { get; set; }
    public string codempresa { get; set; }
    public string api_token { get; set; }
    public string rolpuntodeventa { get; set; }
    public int version { get; set; }
}
