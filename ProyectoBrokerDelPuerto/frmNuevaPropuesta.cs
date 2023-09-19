using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBrokerDelPuerto
{
    public partial class frmNuevaPropuesta : Form
    {
        public bool edit = false;
        public string formpago_ = "";
        public bool duplicado_ = false;
        DateTime fechacorrida = DateTime.Now;
        public bool findeldia = false;
        public bool botEliminarFila = true;
        public string premio_, premioTotal_ = "";
        string errores = "";
        string valComboActividad = "";
        double premioSINviejito = 0;
        int viejitos = 0;
        public bool codpostalactivo = false;
        string path = "";
        string indicadorPromocion = "";
        bool guardamanana = false;
        public DateTime fechacierresiguientediahabil;
        public DateTime fechapropuesta;
        private List<barrios_propuesta> json_barrios_propuesta = new List<barrios_propuesta>();

        public frmNuevaPropuesta()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            this.boton_eliminar_default();
        }

        public void set_json_barrios_listbox(string json_barrios_)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(@json_barrios_));
            JObject obj = JObject.Load(reader);
            if (obj["barrios"] != null)
            {
                json_barrios_propuesta = (from dynamic val in obj["barrios"].AsEnumerable().ToList()
                                                                select new barrios_propuesta()
                                                                {
                                                                    id_barrio = val["id_barrio"],
                                                                    nombre = val["nombre"],
                                                                    sumamuerte = val["sumamuerte"],
                                                                    sumagm = val["sumagm"],
                                                                }).ToList();

                foreach (barrios_propuesta bp in json_barrios_propuesta)
                {
                    listBox1.Items.Add(bp.nombre);
                    listBox2.Items.Add(
                            " Mte: " + this.exigencias(bp.sumamuerte, 1) +
                            "   -   GM:" +
                            this.exigencias(bp.sumagm, 2)
                        );
                }

            }
        }

        public void set_list_barrios(DataTable itemBarrio)
        {
            barrios bar = new barrios();
            json_barrios_propuesta.Add(new barrios_propuesta() {
                id = bar.formated_string_query(itemBarrio.Rows[0]["id"].ToString()),
                nombre = bar.formated_string_query(itemBarrio.Rows[0]["nombre"].ToString()),
                sumamuerte = bar.formated_string_query(itemBarrio.Rows[0]["suma_muerte"].ToString()),
                sumagm = bar.formated_string_query(itemBarrio.Rows[0]["suma_gm"].ToString()),
            });
        }

        public void formEdit(string ultmod = "")
        {
            DateTime fech = DateTime.Now;


            groupBox1.Enabled = true;
            groupBox3.Enabled = true;
            btnGuardar.Enabled = true;

            btnRecibo.Enabled = false;
            btnEmitir.Enabled = false;
            button3.Enabled = false;
            btnDuplicar.Enabled = false;
            whatsapp.Visible = false;
            mail_btn.Enabled = false;

            if (ultmod != "")
            {
                this.edit = true;

                //MessageBox.Show(" RESTA "+ (Convert.ToDateTime(ultmod).Date - fech.Date).TotalDays);

                if ((Convert.ToDateTime(ultmod).Date - fech.Date).TotalDays < 0 || (Convert.ToDateTime(ultmod).Date - fech.Date).TotalDays > 1
                    || (DateTime.Compare(this.fechapropuesta.Date, DateTime.Now.Date) == 0 && !this.polizamanana(false, "edicionpostfindeldia")))
                {
                    if (DateTime.Compare(Convert.ToDateTime(ultmod).Date, fech.Date) != 0 || this.findeldia)
                    {
                        //MessageBox.Show("--> " + Convert.ToDateTime(ultmod).Date + " / " + fech.Date + " COMPARACIÖN " + DateTime.Compare(Convert.ToDateTime(ultmod).Date, fech.Date));
                        groupBox1.Enabled = false;
                        //groupBox3.Enabled = false;
                        fechaDesde.Enabled = false;
                        fechaHasta.Enabled = false;
                        listBox1.Enabled = false;
                        comboCobertura.Enabled = false;
                        groupBox2.Enabled = false;
                        comboBox4.Enabled = false;
                        //button2.Enabled = false;
                        checkBox1.Enabled = true;
                        checkBox2.Enabled = true;
                        btnGuardar.Enabled = false;
                        button5.Enabled = false;
                        
                    }
                }
                



            }

        }

        private void boton_eliminar_default()
        {
            btnEliminar.Enabled = false;
            btnEliminar.BackColor = Color.LightGray;
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

        private void frmNuevaPropuesta_Load(object sender, EventArgs e)
        {
            this.visibleLibreDeuda();
            this.datosInciales();
            this.revisar_actualizacion_datos();

        }

        private void revisar_actualizacion_datos()
        {

            if (this.edit)
            {
                configuraciones confiprosimport = new configuraciones();
                if (dataGridView1.Rows.Count < 2 && confiprosimport.get_prosimport() != "0")
                {
                    MessageBox.Show("Se está actualizando la información de ésta propuesta,\npor favor vuelva a abrirla en 1 minuto");
                    logs log = new logs();
                    log.coderror = "LOAD101";
                    log.mensaje = "Se consulta la propuesta " + lblPrefijo.Text + "-" + lblidpropuesta.Text + " al mismo tiempo de su actualización";
                    log.save();
                }
            }
        }

        /*
        * Combo con los nombres de las coberturas
        */
        private void llenarComboCobertura()
        {
            coberturas co = new coberturas();
            DataSet ds = co.get_all();
            if (ds.Tables.Count > 0 && comboCobertura.Items.Count < 1)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboCobertura.Items.Add(ds.Tables[0].Rows[i]["nombre"].ToString());
                }
            }

        }

        /*
       * Combo con los nombres de los barrios
       */
        private void llenarComboBarrios()
        {
            barrios ba = new barrios();

            DataSet ds = ba.get_all();
            if (ds.Tables.Count > 0 && listBox1.Items.Count < 1)
            {
                listBox1.MultiColumn = true;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    listBox1.Items.Add(ds.Tables[0].Rows[i]["nombre"].ToString());
                }
            }

        }

        /*
        * Combo con los nombres de los barrios
        */
        private void llenarComboActividad()
        {
            actividades ac = new actividades();
            DataSet ds = ac.get_all();
            if (ds.Tables.Count > 0 && actividad.Items.Count < 1)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    actividad.Items.Add(ds.Tables[0].Rows[i]["cod"].ToString() + " - " + ds.Tables[0].Rows[i]["nombre"].ToString());
                }
            }
        }

        public void datosInciales()
        {

            puntodeventa punt = new puntodeventa();

            if(lblPrefijo.Text == ".")
            {
                if (punt.get_principal())
                {
                    lblPrefijo.Text = punt.prefijo;
                }
                else if (punt.get_colaborador())
                {

                    lblPrefijo.Text = punt.prefijo;
                }
                else
                {
                    MessageBox.Show("No se ha generado el prefijo para el punto de venta, debe informa al administrador", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }
            

            this.llenarComboCobertura();
            //this.llenarComboBarrios();
            comboSituacion.SelectedIndex = 0;
            this.llenarComboActividad();
            btnRecibo.Enabled = false;
            btnEmitir.Enabled = false;
            button3.Enabled = false;
            btnDuplicar.Enabled = false;
            whatsapp.Visible = false;
            mail_btn.Enabled = false;
            DateTime fech = DateTime.Now;

            if (DateTime.Compare(fechaHasta.Value, fech) >= 0 || edit)
            {
                btnRecibo.Enabled = true;
                btnEmitir.Enabled = true;
                button3.Enabled = true;
                btnDuplicar.Enabled = true;
                whatsapp.Visible = true;
                mail_btn.Enabled = true;
            }

            path = @"EmisionesBasrrios\" + textBox1.Text.Trim();


            /*LOAD*/
            this.boton_eliminar_default();
            textBox1.Select();
            fecha.MaxInputLength = 10;




            propuestas pro = new propuestas();
            if (this.edit == false && lblConsecutivo.Text == "")
            {
                //lblConsecutivo.Text = pro.consecutivo().ToString();
            }

            this.Text = this.Text + " No. " + lblidpropuesta.Text;
            if (this.edit && this.duplicado_ == false)
            {
                this.Text = "Propuesta No. " + lblidpropuesta.Text;
                paga_ch.Enabled = false;
            }
            else
            {
                radNuevaPoliza.Checked = true;
            }

            if (!this.edit)
            {
                this.polizamanana();
            }
            /*LOAD*/

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {




        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                //GENERAR EL TERMINADO DE LA COLUMNA TERMINADO
                if (dataGridView1.CurrentCell.ColumnIndex == 1)
                {
                    int fil = dataGridView1.CurrentRow.Index;
                    if (dataGridView1.CurrentRow.Cells["nodocumento"].Value != null)
                    {
                        //MessageBox.Show(dataGridView1.CurrentRow.Cells["nodocumento"].Value.ToString() + " -> " + this.revisarTomadorEnPoliza(dataGridView1.CurrentRow.Cells["nodocumento"].Value.ToString()));
                        if (!this.revisarTomadorEnPoliza(dataGridView1.CurrentRow.Cells["nodocumento"].Value.ToString()))
                        {
                            clientes cl = new clientes();
                            DataSet ds = cl.get(dataGridView1.CurrentRow.Cells["nodocumento"].Value.ToString());

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    dataGridView1.CurrentRow.Cells["documento"].Value = ds.Tables[0].Rows[0]["tipo_id"].ToString();
                                    dataGridView1.CurrentRow.Cells["apellido"].Value = ds.Tables[0].Rows[0]["apellidos"].ToString();
                                    dataGridView1.CurrentRow.Cells["nombre"].Value = ds.Tables[0].Rows[0]["nombres"].ToString();
                                    dataGridView1.CurrentRow.Cells["fecha"].Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString()).ToString("dd/MM/yyyy");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("El documento que acaba de ingresar ya está en el listado de pólizas ", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridView1.Rows[fil].Cells[1].Value = null;
                        }

                    }
                }

            }
            catch
            {
                Console.Write("error lectura, change");
            }

        }


        private void borrar_campos()
        {
            txtTipoid.Text = "";
            txtApellidos.Text = "";
            txtNombres.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCodpostal.Text = "";
            txtLocalidad.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboSituacion.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();

            if (textBox1.Text.Trim() != "")
            {

                clientes cl = new clientes();
                DataSet ds = cl.get(textBox1.Text.Trim());
                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0) {
                        this.borrar_campos();
                        txtTipoid.Text = ds.Tables[0].Rows[0]["tipo_id"].ToString();
                        txtApellidos.Text = ds.Tables[0].Rows[0]["apellidos"].ToString();
                        txtNombres.Text = ds.Tables[0].Rows[0]["nombres"].ToString();
                        txtDireccion.Text = ds.Tables[0].Rows[0]["direccion"].ToString();
                        txtTelefono.Text = ds.Tables[0].Rows[0]["telefono"].ToString();
                        txtCodpostal.Text = ds.Tables[0].Rows[0]["codpostal"].ToString();
                        txtLocalidad.Text = ds.Tables[0].Rows[0]["localidad"].ToString();
                        txtCiudad.Text = ds.Tables[0].Rows[0]["ciudad"].ToString();
                        if (ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString() != "" && ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString() != "00/00/0000")
                            txtFechanacimiento.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["fecha_nacimiento"].ToString()).ToString("dd/MM/yyyy");
                        if (ds.Tables[0].Rows[0]["sexo"] != null)
                        {
                            if (ds.Tables[0].Rows[0]["sexo"].ToString() == "MASCULINO")
                                radioButton2.Checked = true;
                            if (ds.Tables[0].Rows[0]["sexo"].ToString() == "FEMENINO")
                                radioButton1.Checked = true;
                        }
                        txtEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();

                        comboSituacion.Text = ds.Tables[0].Rows[0]["situacion"].ToString();
                    }
                }
            }
        }

        private void txtFechanacimiento_ValueChanged(object sender, EventArgs e)
        {

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
            } catch (Exception ex)
            {
                Console.WriteLine("ERROR..." + ex.Message);
            }


            return res;
        }

        private int revisar_cant_polizas()
        {
            int sum_viejitos = 0;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["nodocumento"].Value == null)
                    return -1;
                if (dataGridView1.Rows[i].Cells["documento"].Value == null)
                    return -1;
                if (dataGridView1.Rows[i].Cells["apellido"].Value == null)
                    return -1;
                if (dataGridView1.Rows[i].Cells["nombre"].Value == null)
                    return -1;
                if (dataGridView1.Rows[i].Cells["fecha"].Value == null)
                    return -1;
                if (dataGridView1.Rows[i].Cells["actividad"].Value == null)
                    return -1;
                if (dataGridView1.Rows[i].Cells["clasificacion"].Value == null)
                    return -1;

                dataGridView1.Rows[i].Cells["apellido"].Value = dataGridView1.Rows[i].Cells["apellido"].Value.ToString().ToUpper();
                dataGridView1.Rows[i].Cells["nombre"].Value = dataGridView1.Rows[i].Cells["nombre"].Value.ToString().ToUpper();

                sum_viejitos = sum_viejitos + this.edadVieja(dataGridView1.Rows[i].Cells["fecha"].Value.ToString());


                //if (dataGridView1.Rows[i].Cells["clasificacion"].Value == null)
                //  return -1;

                clientes cli = new clientes();
                cli.id = dataGridView1.Rows[i].Cells["nodocumento"].Value.ToString();

                if (!cli.validar_id())
                {
                    cli.tipo_id = dataGridView1.Rows[i].Cells["documento"].Value.ToString();
                    cli.apellidos = dataGridView1.Rows[i].Cells["apellido"].Value.ToString();
                    cli.nombres = dataGridView1.Rows[i].Cells["nombre"].Value.ToString();

                    try
                    {
                        cli.fecha_nacimiento = Convert.ToDateTime(dataGridView1.Rows[i].Cells["fecha"].Value.ToString()).ToString("yyyy-MM-dd");
                    }
                    catch
                    {
                        MessageBox.Show("Fecha no válida" + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.Rows[i].Cells["fecha"].Value = null;
                        dataGridView1.Rows[i].Cells["fecha"].Selected = true;
                        return -1;
                    }

                    cli.user_edit = MDIParent1.sesionUser;
                    cli.codestado = "1";
                    cli.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cli.save();
                }
            }

            if( (dataGridView1.Rows.Count - 1) < 2 )
                radNuevaPoliza.Checked = true;
            if ((dataGridView1.Rows.Count - 1) > 1)
                radAltaPoliza.Checked = true;

            viejitos = sum_viejitos;

            return dataGridView1.Rows.Count - 1;
        }

        public void suma_premio()
        {
            if (btnGuardar.Enabled == false && button5.Enabled == false)
            {
                return;
            }
            else {
                int cant_polizas = revisar_cant_polizas();
                this.indicadorPromocion = "";

                if (comboCobertura.Text != "" && comboBox4.Text != "" && textBox1.Text.Trim() != "" && cant_polizas > -1)
                {

                    coberturas cob = new coberturas();

                    DataSet ds = cob.get(comboCobertura.Text);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            double resultado_suma = Convert.ToDouble(ds.Tables[0].Rows[0]["vrMensual"].ToString()) * Convert.ToUInt16(comboBox4.Text);

                            if (Convert.ToUInt16(comboBox4.Text) == 2 && ds.Tables[0].Rows[0]["x21"].ToString() != "" && ds.Tables[0].Rows[0]["x21"].ToString() != "0")
                            {
                                resultado_suma = Convert.ToDouble(ds.Tables[0].Rows[0]["x21"].ToString());
                                this.indicadorPromocion = "2x1 - " + resultado_suma;
                            }
                            if (Convert.ToUInt16(comboBox4.Text) == 3 && ds.Tables[0].Rows[0]["vrTrimestral"].ToString() != "" && ds.Tables[0].Rows[0]["vrTrimestral"].ToString() != "0")
                            {
                                resultado_suma = Convert.ToDouble(ds.Tables[0].Rows[0]["vrTrimestral"].ToString());
                            }


                            if (Convert.ToUInt16(comboBox4.Text) == 3 && ds.Tables[0].Rows[0]["x32"].ToString() != "" && ds.Tables[0].Rows[0]["x32"].ToString() != "0")
                            {
                                resultado_suma = Convert.ToDouble(ds.Tables[0].Rows[0]["x32"].ToString());
                                this.indicadorPromocion = "3x2 - " + resultado_suma;
                            }

                            if (Convert.ToUInt16(comboBox4.Text) == 6 && ds.Tables[0].Rows[0]["vrSemestral"].ToString() != "" && ds.Tables[0].Rows[0]["vrSemestral"].ToString() != "0")
                            {
                                resultado_suma = Convert.ToDouble(ds.Tables[0].Rows[0]["vrSemestral"].ToString());
                            }

                            if (Convert.ToUInt16(comboBox4.Text) == 6 && ds.Tables[0].Rows[0]["x64"].ToString() != "" && ds.Tables[0].Rows[0]["x64"].ToString() != "0")
                            {
                                resultado_suma = Convert.ToDouble(ds.Tables[0].Rows[0]["x64"].ToString());
                                this.indicadorPromocion = "6x4 - " + resultado_suma;
                            }



                            premioSINviejito = resultado_suma;
                            //Se suma el total y se suma por cada persona mayor
                            resultado_suma = resultado_suma * cant_polizas + (viejitos * resultado_suma);


                            premio_ = ds.Tables[0].Rows[0]["vrMensual"].ToString().Replace(",", ".");
                            txtPremio.Text = string.Format("{0:c}", Convert.ToDouble(ds.Tables[0].Rows[0]["vrMensual"].ToString()));
                            premioTotal_ = resultado_suma.ToString().Replace(",", ".");
                            txtPremioTotal.Text = string.Format("{0:c}", resultado_suma);
                        }
                    }
                }

                if (comboCobertura.Text == "") {
                    premio_ = "0";
                    txtPremio.Text = "0";
                    premioTotal_ = "0";
                    txtPremioTotal.Text = string.Format("{0:c}", 0);
                }
            }




        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "")
            {
                fechaHasta.Value = fechaDesde.Value.AddMonths(Convert.ToUInt16(comboBox4.Text));
            }
            this.suma_premio();
        }

        private void comboCobertura_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.suma_premio();
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private bool revisarTomadorEnPoliza(string cod)
        {
            int sumaFilas = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["nodocumento"].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells["nodocumento"].Value.ToString() == cod)
                        sumaFilas++;
                }
            }

            if (sumaFilas > 1)
                return true;

            return false;
        }

        private void agregar_tomador()
        {
            int fila = dataGridView1.Rows.Count - 1;
            bool agregado = true;
            int filaEncontrada = 0;

            validaciones val = new validaciones();

            string[] descartados = { "txtTelefono", "txtCiudad", "txtEmail" };

            if (val.camposvacios_grupos_condescartados(this.groupBox1, descartados) && txtTipoid.Text != "")
            {
                this.guardar_cliente();



                bool valida = true;
                DateTime fechDia;
                try
                {
                    fechDia = Convert.ToDateTime(txtFechanacimiento.Text);
                    if (DateTime.Now.Year - fechDia.Year < 18)
                        valida = false;
                    if (DateTime.Now.Year - fechDia.Year == 18)
                    {
                        if (DateTime.Now.Month - fechDia.Month < 0)
                            valida = false;
                        if (DateTime.Now.Month - fechDia.Month == 0)
                        {
                            if (DateTime.Now.Day - fechDia.Day < 0)
                                valida = false;
                        }
                    }

                    if (!valida)
                        MessageBox.Show("El tomador no puede ser menor de edad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    valida = false;
                    MessageBox.Show("Fecha de nacimiento inválida ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (valida)
                {
                    for (int i = 0; i < fila; i++)
                    {
                        if (dataGridView1.Rows[i].Cells["nodocumento"].Value != null)
                        {
                            if (dataGridView1.Rows[i].Cells["nodocumento"].Value.ToString() == textBox1.Text.Trim())
                            {
                                filaEncontrada = i;
                                agregado = false;
                            }
                        }
                    }

                    if (agregado)
                    {
                        if (dataGridView1.Rows[fila].Cells["nodocumento"].Value != null)
                        {
                            dataGridView1.Rows[fila].Cells["nodocumento"].Value = textBox1.Text.Trim();
                        }
                        else
                        {
                            dataGridView1.Rows.Add("", textBox1.Text.Trim(), txtTipoid.Text, txtApellidos.Text, txtNombres.Text, txtFechanacimiento.Text);
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[filaEncontrada].Cells["documento"].Value = txtTipoid.Text;
                        dataGridView1.Rows[filaEncontrada].Cells["nodocumento"].Value = textBox1.Text;
                        dataGridView1.Rows[filaEncontrada].Cells["apellido"].Value = txtApellidos.Text;
                        dataGridView1.Rows[filaEncontrada].Cells["nombre"].Value = txtNombres.Text;
                        dataGridView1.Rows[filaEncontrada].Cells["fecha"].Value = txtFechanacimiento.Text;
                    }
                }


            }
            else {
                MessageBox.Show("Hay campos del Tomador sin diligenciar : " + errores, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.agregar_tomador();
        }

        private bool guardar_propuesta(bool duplicado)
        {
            bool res = false;

            propuestas pro = new propuestas();
            if (duplicado)
            {
                lblConsecutivo.Text = "";
                lblidpropuesta.Text = "";
                pro.idpropuesta = "";
            }
            else
            {
                pro.id = lblConsecutivo.Text;
                pro.idpropuesta = lblidpropuesta.Text;
            }


            pro.documento = textBox1.Text;
            pro.num_polizas = (dataGridView1.Rows.Count - 1).ToString();
            pro.meses = comboBox4.Text;

            pro.id_cobertura = comboCobertura.Text;
            if (comboCobertura.Text != "")
            {
                coberturas cob = new coberturas();
                DataSet dsCobertura = cob.get(pro.id_cobertura);
                pro.cobertura_suma = dsCobertura.Tables[0].Rows[0]["suma"].ToString();
                pro.cobertura_deducible = dsCobertura.Tables[0].Rows[0]["deducible"].ToString();
                pro.cobertura_gastos = dsCobertura.Tables[0].Rows[0]["gastos"].ToString();
            }
                

            pro.id_barrio = lblidpropuesta.Text;
            pro.data_barrios =  "{\"barrios\":"+ JsonConvert.SerializeObject(json_barrios_propuesta) + "}";

            if (radNuevaPoliza.Checked)
                pro.nueva_poliza = "2";
            if (radAltaPoliza.Checked)
                pro.nueva_poliza = "1";

            pro.premio = premio_;
            pro.premio_total = premioTotal_;

            if (duplicado)
            {
                fechaDesde.Value = DateTime.Now;
                fechaHasta.Value = fechaDesde.Value.AddMonths(Convert.ToUInt16(comboBox4.Text));
                this.polizamanana();
            }

            pro.fechaDesde = fechaDesde.Value.ToString("yyyy-MM-dd HH:mm:ss");
            pro.fechaHasta = fechaHasta.Value.ToString("yyyy-MM-dd HH:mm:ss");


            DateTime dat = DateTime.Now;
            pro.ultmod = dat.ToString("yyyy-MM-dd HH:mm:ss");
            if (this.guardamanana || duplicado)
            {
                pro.ultmod = this.fechacorrida.ToString("yyyy-MM-dd HH:mm:ss");
            }
            pro.codestado = "1";
            pro.user_edit = MDIParent1.sesionUser;
            pro.clausula = Convert.ToInt16(Convert.ToBoolean(checkBox1.Checked)).ToString();
            pro.barrio_beneficiario = Convert.ToInt16(Convert.ToBoolean(checkBox2.Checked)).ToString();
            pro.promocion = this.indicadorPromocion;

            pro.prefijo = lblPrefijo.Text;
            if (duplicado)
            {
                pro.prefijo = MDIParent1.prefijo;
                lblPrefijo.Text = pro.prefijo;
            }
                

                if (!paga_ch.Checked)
            {
                pro.paga = "0";
                pro.formadepago = "CREDITO";
                pro.fecha_paga = "1000-01-01 01:00:00";
                pro.usuariopaga = "";
            }
            else
            {
                pro.paga = "1";
                pro.usuariopaga = pro.user_edit;
                pro.fecha_paga = pro.ultmod;
            }


            Configmaster conf = new Configmaster();
            usuarios us = new usuarios();
            us.loggin = MDIParent1.sesionUser;
            DataSet dsUserCom = us.get();
            pro.productor = us.get_codproductor(us.loggin);
            pro.master = conf.get("MASTER").Tables[0].Rows[0]["nomcodigo"].ToString();
            pro.organizador = us.get_codorganizador(us.loggin);


            if (pro.organizador == "")
            {
                pro.organizador = conf.get("ORGANIZADOR").Tables[0].Rows[0]["nomcodigo"].ToString();
            }

            if (pro.organizador == "")
            {
                
                MessageBox.Show("El usuario " + us.loggin + " no tiene un código de organizador asignado, por favor informe al administrador" +
                    " espere que el administrador actualice el código y luego vuelva a guardar", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            

            if (pro.productor == "")
            {
                MessageBox.Show("El usuario " + us.loggin + " no tiene un código de productor asignado, por favor informe al administrador\nNo necesita cerrar esta propuesta" +
                    " espere que el administrador actualice el código y luego vuelva a guardar", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (pro.save())
            {
                
                this.fechapropuesta = Convert.ToDateTime(pro.ultmod);

                if (lblidpropuesta.Text == "")
                {
                    lblidpropuesta.Text = pro.idpropuesta;
                }



                if (!this.edit || lblConsecutivo.Text == "")
                {
                    lblConsecutivo.Text = pro.maxId().ToString();
                    pro.id = lblConsecutivo.Text;
                    pro.id_barrio = lblidpropuesta.Text;
                    pro.save();
                }


                lineas_propuestas li = new lineas_propuestas();
                li.delete_idpropuesta(lblidpropuesta.Text, pro.prefijo);
                li.id_propuesta = pro.idpropuesta;
                li.idprefijo = pro.idpropuesta;
                li.prefijo = pro.prefijo;

                lineas_propuestas_aux li_aux = new lineas_propuestas_aux();
                li_aux.delete_idpropuesta(lblidpropuesta.Text, pro.prefijo);
                li_aux.id_propuesta = pro.idpropuesta;
                li_aux.idprefijo = pro.idpropuesta;
                li_aux.prefijo = pro.prefijo;

                try {

                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        actividades ac = new actividades();
                        clasificaciones cla = new clasificaciones();
                        li.documento = dataGridView1.Rows[i].Cells["nodocumento"].Value.ToString();
                        li.tipo_documento = dataGridView1.Rows[i].Cells["documento"].Value.ToString();
                        li.apellidos = dataGridView1.Rows[i].Cells["apellido"].Value.ToString();
                        li.nombres = dataGridView1.Rows[i].Cells["nombre"].Value.ToString();
                        li.fecha_nacimiento = Convert.ToDateTime(dataGridView1.Rows[i].Cells["fecha"].Value.ToString()).ToString("yyyy-MM-dd");
                        li.id_actividad = ac.get_id(dataGridView1.Rows[i].Cells["actividad"].Value.ToString());
                        li.premio = premioSINviejito.ToString();
                        if (this.edadVieja(dataGridView1.Rows[i].Cells["fecha"].Value.ToString()) > 0)
                            li.premio = (premioSINviejito * 2).ToString();
                        li.id_clasificacion = cla.get_id(dataGridView1.Rows[i].Cells["clasificacion"].Value.ToString());
                        li.actividad = dataGridView1.Rows[i].Cells["actividad"].Value.ToString();
                        li.clasificacion = dataGridView1.Rows[i].Cells["clasificacion"].Value.ToString();
                        li.ultmod = pro.ultmod;
                        li.codestado = "1";
                        li.user_edit = MDIParent1.sesionUser;
                        li.fechaDesde = pro.fechaDesde;
                        li.fechaHasta = pro.fechaHasta;

                        if (!li.save())
                        {
                            return false;
                        }

                        li_aux.documento = dataGridView1.Rows[i].Cells["nodocumento"].Value.ToString();
                        li_aux.tipo_documento = dataGridView1.Rows[i].Cells["documento"].Value.ToString();
                        li_aux.apellidos = dataGridView1.Rows[i].Cells["apellido"].Value.ToString();
                        li_aux.nombres = dataGridView1.Rows[i].Cells["nombre"].Value.ToString();
                        li_aux.fecha_nacimiento = Convert.ToDateTime(dataGridView1.Rows[i].Cells["fecha"].Value.ToString()).ToString("yyyy-MM-dd");
                        li_aux.id_actividad = ac.get_id(dataGridView1.Rows[i].Cells["actividad"].Value.ToString());
                        li_aux.premio = premioSINviejito.ToString();
                        if (this.edadVieja(dataGridView1.Rows[i].Cells["fecha"].Value.ToString()) > 0)
                            li_aux.premio = (premioSINviejito * 2).ToString();
                        li_aux.id_clasificacion = cla.get_id(dataGridView1.Rows[i].Cells["clasificacion"].Value.ToString());
                        li_aux.actividad = dataGridView1.Rows[i].Cells["actividad"].Value.ToString();
                        li_aux.clasificacion = dataGridView1.Rows[i].Cells["clasificacion"].Value.ToString();
                        li_aux.ultmod = pro.ultmod;
                        li_aux.codestado = "1";
                        li_aux.user_edit = MDIParent1.sesionUser;
                        li_aux.fechaDesde = pro.fechaDesde;
                        li_aux.fechaHasta = pro.fechaHasta;
                        li_aux.save();

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hay un error en uno de los registros de póliza \n" + ex.Message);
                    return false;

                }


                if(json_barrios_propuesta.Count() < 1)
                {
                    barrios_propuesta bar = new barrios_propuesta();
                    bar.delete_idpropuesta(lblidpropuesta.Text, pro.prefijo);
                    bar.id_propuesta = pro.idpropuesta;
                    bar.idprefijo = pro.idpropuesta;
                    bar.prefijo = pro.prefijo;

                    foreach (var item in listBox1.Items)
                    {
                        barrios barrio = new barrios();
                        bar.id_barrio = barrio.get_id(item.ToString());
                        bar.nombre = item.ToString();
                        bar.user_edit = MDIParent1.sesionUser;
                        bar.ultmod = pro.ultmod;

                        bar.codestado = "1";
                        if (!bar.save())
                        {
                            return false;
                        }
                    }

                }


                if (dsUserCom.Tables[0].Rows[0]["comisionprima"].ToString() != "" && dsUserCom.Tables[0].Rows[0]["comisionpremio"].ToString() != "")
                {
                    if (dsUserCom.Tables[0].Rows[0]["comisionprima"].ToString() != "0" || dsUserCom.Tables[0].Rows[0]["comisionpremio"].ToString() != "0")
                    {
                        comisiones com = new comisiones();
                        com.porc_compremio = dsUserCom.Tables[0].Rows[0]["comisionpremio"].ToString().Replace(",", ".");
                        com.porc_comprima = dsUserCom.Tables[0].Rows[0]["comisionprima"].ToString().Replace(",", ".");
                        com.idpropuesta = pro.idpropuesta;
                        com.prefijo = pro.prefijo;
                        com.user = us.loggin;
                        com.fechacomision = pro.ultmod;
                        com.ultmod = pro.ultmod;
                        com.save();
                    }
                }

                if (duplicado == false)
                {
                    Task.Run(() => {
                        frmMigraciones frmmig = new frmMigraciones();
                        frmmig.exportarPropuestas();
                    });
                }

                res = true;
            }else
            {
                res = false;
            }

            
            

            return res;
        }

        private void btnRecibo_Click(object sender, EventArgs e)
        {
            /*if (paga_ch.Checked)
            {*/
                if (MessageBox.Show("¿Recibo pagado?", "PDF Recibo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.generarReciboPdf(true, true);
                }
                else
                {
                    this.generarReciboPdf(true, false);
                }
           /* }
            else
            {
                this.generarReciboPdf(true, false);
            }*/
        }

        private void btnEmitir_Click(object sender, EventArgs e)
        {

            if (this.validacion(false)) {
              /*  if (paga_ch.Checked)
                {*/
                    if (MessageBox.Show("¿Certificado pagado?", "PDF Certificado", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.generarEmisionPdf(true, true);
                    }
                    else
                    {
                        this.generarEmisionPdf(true, false);
                    }
                /*}
                else
                {
                    this.generarEmisionPdf(true, false);
                }*/
            }
            else
            {
                MessageBox.Show("No se puede emitir por : " + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public void generarReciboPdf(bool abrirArchivo, bool pagado = false)
        {
            generarPdfs gen = new generarPdfs();

            propuestas pro = new propuestas();
            DataSet dsPropuesta = pro.getprefijo(lblPrefijo.Text, lblidpropuesta.Text);

            coberturas cob = new coberturas();

            cob.suma = dsPropuesta.Tables[0].Rows[0]["cobertura_suma"].ToString();
            cob.deducible = dsPropuesta.Tables[0].Rows[0]["cobertura_deducible"].ToString();
            cob.gastos = dsPropuesta.Tables[0].Rows[0]["cobertura_gastos"].ToString();

            clientes cl = new clientes();
            cl.direccion = txtDireccion.Text;
            cl.localidad = txtLocalidad.Text;
            cl.apellidos = txtApellidos.Text;
            cl.nombres = txtNombres.Text;
            cl.tipo_id = txtTipoid.Text;
            cl.id = textBox1.Text.Trim();
            cl.ciudad = txtCiudad.Text;
            cl.codpostal = txtCodpostal.Text;

            this.crearCarpeta(path);



            gen.pdfRecibos(lblPrefijo.Text + "-" + lblidpropuesta.Text, cl, dataGridView1, fechaDesde.Text, fechaHasta.Value.ToString("dd/MM/yyyy"), comboBarrios.Text, cob, Convert.ToDouble(premioTotal_), abrirArchivo, pagado, path);
            if (File.Exists(Environment.CurrentDirectory + @"\" + path))
                File.Delete(Environment.CurrentDirectory + @"\" + path);
        }

        public void generarEmisionPdf(bool abrirArchivo, bool pagado = false)
        {
            generarPdfs gen = new generarPdfs();

            propuestas pro = new propuestas();


            DataSet dsPropuesta = pro.getprefijo(lblPrefijo.Text, lblidpropuesta.Text);


            coberturas cob = new coberturas();
            cob.suma = dsPropuesta.Tables[0].Rows[0]["cobertura_suma"].ToString();
            cob.deducible = dsPropuesta.Tables[0].Rows[0]["cobertura_deducible"].ToString();
            cob.gastos = dsPropuesta.Tables[0].Rows[0]["cobertura_gastos"].ToString();


            clientes cl = new clientes();
            cl.apellidos = txtApellidos.Text;
            cl.nombres = txtNombres.Text;
            cl.tipo_id = txtTipoid.Text;
            cl.id = textBox1.Text.Trim();
            bool norepeticion = false;
            if (checkBox1.Checked)
                norepeticion = true;

            bool benefiBarrios = false;
            if (checkBox2.Checked)
                benefiBarrios = true;

            this.crearCarpeta(path);

            gen.pdfEmisionBarriosPrivados(lblPrefijo.Text + "-" + lblidpropuesta.Text, cl, dataGridView1, fechaDesde.Value, fechaHasta.Value, listBox1, cob, norepeticion, benefiBarrios, abrirArchivo, path, pagado);
            if (File.Exists(Environment.CurrentDirectory + @"\" + path))
                File.Delete(Environment.CurrentDirectory + @"\" + path);
        }

        private bool validacion(bool COB = true)
        {
            errores = "";
            validaciones val = new validaciones();

            string[] descartados = { "txtTelefono", "txtCiudad", "txtEmail" };

            if (!val.camposvacios_grupos_condescartados(this.groupBox1, descartados))
            {
                errores += "\nHay campos vacíos obligatorios en la zona del tomador";
            }

            /*if (!val.esnumero(textBox1.Text) && textBox1.Text != "" )
            {
                errores += "\nLa identificación debe tener sólo números";
            }*/

            if (this.revisar_cant_polizas() < 0 || dataGridView1.Rows.Count < 2)
            {
                errores += "\nNo se ha ingresado ninguna persona en la grilla o alguno de los registros no está totalmente diligenciado";
            }

            if (!val.camposvacios_grupos(this.groupBox3))
            {
                errores += "\nNo se ha fijado los parámetros para generar el premio";
            }
            txtEmail.Text = txtEmail.Text.Trim();
            if (txtEmail.Text != "")
            {
                if(this.getMailsClient(txtEmail.Text).Count() == 0)
                    errores += "\nHay algún correo mal escrito";
            }


            if (comboCobertura.Text == "" && COB)
            {
                errores += "\nNo ha seleccionado la cobertura o fue deshabilitada";
            }



            if (errores != "")
            {
                return false;
            }

            txtNombres.Text = txtNombres.Text.ToUpper();
            txtApellidos.Text = txtApellidos.Text.ToUpper();
            txtCiudad.Text = txtCiudad.Text.ToUpper();
            txtLocalidad.Text = txtLocalidad.Text.ToUpper();

            return true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count - 1 > dataGridView1.CurrentRow.Index)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
            if(dataGridView1.Rows.Count == 2)
            {
                radNuevaPoliza.Checked = true;
            }
            this.suma_premio();

        }

        private void dataGridView1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
            this.boton_eliminar_default();

        }



        private void txtFechanacimiento_KeyUp(object sender, KeyEventArgs e)
        {
            validaciones val = new validaciones();

            if ((int)e.KeyCode != (int)Keys.Back)
            {
                txtFechanacimiento.Text = val.fecha_textbox(txtFechanacimiento.Text);
                txtFechanacimiento.Select(txtFechanacimiento.Text.Length, 0);
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {


            if (dataGridView1.Rows.Count - 1 > dataGridView1.CurrentRow.Index)
            {
                if (botEliminarFila)
                {
                    btnEliminar.Enabled = true;
                    btnEliminar.BackColor = Color.DarkRed;
                }

            }


            dataGridView1.BeginEdit(false);
        }

        void text_KeyUp(object sender, KeyEventArgs e)
        {

            if (dataGridView1.CurrentCell.ColumnIndex == 5)
            {
                if ((e.KeyCode > Keys.NumPad0 && e.KeyCode > Keys.NumPad9) || (e.KeyCode > Keys.D0 && e.KeyCode > Keys.D9))
                {
                    string valText = ((DataGridViewTextBoxEditingControl)(sender)).Text;
                    validaciones val = new validaciones();
                    ((DataGridViewTextBoxEditingControl)(sender)).Text = val.fecha_textbox(valText);
                    ((DataGridViewTextBoxEditingControl)(sender)).Select(((DataGridViewTextBoxEditingControl)(sender)).Text.Length, 0);
                    if (valText.Length >= 10)
                    {
                        try
                        {
                            string conca = "";
                            if (Convert.ToInt16(valText.Substring(0, 2)) > 31 || Convert.ToInt16(valText.Substring(0, 2)) < 1)
                                conca += "Día inválido ";
                            if (Convert.ToInt16(valText.Substring(3, 2)) > 12 || Convert.ToInt16(valText.Substring(3, 2)) < 1)
                                conca += "Mes inválido ";
                            if (Convert.ToInt16(valText.Substring(6, 4)) > DateTime.Now.Year - 10 || Convert.ToInt16(valText.Substring(6, 4)) < DateTime.Now.Year - 100)
                                conca += "Año inválido ";
                            if (conca != "")
                            {
                                MessageBox.Show("Fecha no válida" + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ((DataGridViewTextBoxEditingControl)(sender)).Text = "";
                            }
                            Convert.ToDateTime(valText);


                        }
                        catch
                        {
                            MessageBox.Show("Fecha no válida" + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ((DataGridViewTextBoxEditingControl)(sender)).Text = "";
                        }
                    }
                }
            }


        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 5)
            {

                DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;
                dText.KeyUp -= new KeyEventHandler(text_KeyUp);
                dText.KeyUp += new KeyEventHandler(text_KeyUp);
            }


            if (dataGridView1.CurrentCell.ColumnIndex == 6)
            {

                /*DataGridViewComboBoxEditingControl cobtext = (DataGridViewComboBoxEditingControl)e.Control;
                cobtext.Items.Clear();
                if (dataGridView1.CurrentRow.Cells["actividad"].Value != null)
                {
                    //DataGridViewComboBoxCell comboboxCell = dataGridView1.CurrentRow.Cells["clasificacion"] as DataGridViewComboBoxCell;
                    clasificaciones cla = new clasificaciones();
                    actividades ac = new actividades();
                    
                    cla.id_actividad = ac.get_nombre(dataGridView1.CurrentRow.Cells["actividad"].Value.ToString()).Tables[0].Rows[0]["id"].ToString();
                    DataSet ds = cla.get_all_actividad();
                    cobtext.Items.Add("HOLA MUNDO");
                    cobtext.Items.Add("HOLA MUNDO 1");
                    //MessageBox.Show("ds. "+ds.Tables.Count);
                    /*if (ds.Tables.Count > 0)
                    {
                        cobtext.Items.Clear();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            cobtext.Items.Add(ds.Tables[0].Rows[i]["nombre"].ToString());
                        }
                    }*/

                ComboBox combo = e.Control as ComboBox;
                if (combo != null)
                {
                    // Fix the black background on the drop down menu
                    e.CellStyle.BackColor = this.dataGridView1.DefaultCellStyle.BackColor;
                }
                /*if (dataGridView1.CurrentRow.Cells["actividad"].Value != null)
                {
                    if(((ComboBox)sender).Items.Count > 0)
                    {
                        ((ComboBox)sender).Items.Clear();
                    }
                    ((ComboBox)sender).Items.Add("OPCION1");
                    ((ComboBox)sender).Items.Add("OPCION2");
                }*/

                //MessageBox.Show("valComboActividad " + valComboActividad + " combo " + combo.Text);
                if (combo.Text != null && (valComboActividad != combo.Text || valComboActividad == ""))
                {
                    combo.SelectedIndexChanged -= new EventHandler(comboDataGrid);
                    combo.SelectedIndexChanged += new EventHandler(comboDataGrid);
                }
            }

            /*if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                ComboBox combo = e.Control as ComboBox;
                combo.SelectedIndexChanged -= new EventHandler(combocla);
                combo.SelectedIndexChanged += new EventHandler(combocla);
              
            }*/
            this.suma_premio();
        }
        /*void combocla(object sender, EventArgs e)
        {
            //dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells["nodocumento"].Selected = true;
            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                ((ComboBox)sender).Text = ((ComboBox)sender).Text;
            }
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells["nodocumento"];
            this.suma_premio();
        }*/



        /*
        En éste espacio se envía la opción seleccionada en actividades para 
        generar las opciones de clasificación
        */
        void comboDataGrid(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 6)
            {
                this.adicionar_items_clasificacion(dataGridView1.CurrentRow.Index, ((ComboBox)sender).Text);
            }

            //Si hay más de una poliza el sistema revisará si hay más de una poliza y colocará alta en poliza seleccionada
            if (dataGridView1.CurrentRow.Index >= 1)
                radAltaPoliza.Checked = true;

        }

        public void adicionar_items_clasificacion(int i, string val)
        {

            DataGridViewComboBoxCell comboboxCell = dataGridView1.Rows[i].Cells["clasificacion"] as DataGridViewComboBoxCell;


            clasificaciones cla = new clasificaciones();
            actividades ac = new actividades();

            cla.id_actividad = ac.get_nombre(val).Tables[0].Rows[0]["id"].ToString();
            DataSet ds = cla.get_all_actividad();
            if (ds.Tables.Count > 0)
            {

                //MessageBox.Show("1 "+ comboboxCell.Items.Count);
                if (comboboxCell.Items.Count > 0)
                {
                    comboboxCell.Items.Clear();
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (opcion_ya(comboboxCell, ds.Tables[0].Rows[0]["nombre"].ToString()))
                    {
                        for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                        {
                            comboboxCell.Items.Add(ds.Tables[0].Rows[k]["cod"].ToString() + " - " + ds.Tables[0].Rows[k]["nombre"].ToString());
                        }
                    }
                }

                valComboActividad = val;
            }
        }

        private bool opcion_ya(DataGridViewComboBoxCell com, string dato)
        {
            for (int k = 0; k < com.Items.Count; k++)
            {
                if (com.Items[k].ToString() == dato)
                    return false;
            }
            return true;
        }

        private void frmNuevaPropuesta_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void guardar_cliente()
        {
            clientes cli = new clientes();
            cli.id = textBox1.Text.Trim();
            DataSet ds = cli.get(cli.id);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cli.email = ds.Tables[0].Rows[0]["email"].ToString();
                }
            }
            cli.tipo_id = txtTipoid.Text;
            cli.nombres = txtNombres.Text;
            cli.apellidos = txtApellidos.Text;
            cli.ciudad = txtCiudad.Text;
            cli.codpostal = txtCodpostal.Text;
            cli.localidad = txtLocalidad.Text;
            cli.direccion = txtDireccion.Text;
            cli.telefono = txtTelefono.Text;
            cli.situacion = comboSituacion.Text;
            
            try
            {
                cli.fecha_nacimiento = Convert.ToDateTime(txtFechanacimiento.Text).ToString("yyyy-MM-dd");
            }catch(Exception e)
            {
                MessageBox.Show("La fecha de nacimiento está mal escrita");
                return;
            }
            
            if (radioButton1.Checked)
                cli.sexo = "FEMENINO";
            if (radioButton2.Checked)
                cli.sexo = "MASCULINO";
            cli.email = txtEmail.Text;
            cli.user_edit = MDIParent1.sesionUser;
            cli.codestado = "1";
            cli.ultmod = (DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");

            cli.save();
        }

        private void fechaDesde_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "")
            {
                fechaHasta.Value = fechaDesde.Value.AddMonths(Convert.ToUInt16(comboBox4.Text));
            }
        }

        private void comboBarrios_MouseEnter(object sender, EventArgs e)
        {
            listBox1.Visible = true;
            listBox2.Visible = true;
            vScrollBar1.Visible = true;
        }

        private void listBox1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void groupBox3_MouseHover(object sender, EventArgs e)
        {
            this.suma_premio();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.Visible)
            {
                listBox1.Visible = false;
                listBox2.Visible = false;
                vScrollBar1.Visible = false;
                button1.Text = "▲";
            }
            else
            {
                listBox1.Visible = true;
                listBox2.Visible = true;
                vScrollBar1.Visible = true;
                vScrollBar1.BringToFront();
                button1.Text = "▼";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<barrios_propuesta> listAuxBarr = json_barrios_propuesta;
            string barrios_antes = "{\"barrios\":" + JsonConvert.SerializeObject(listAuxBarr) + "}";
            if (this.edit)
            {
                if (btnGuardar.Enabled == false && button5.Enabled == false)
                {
                    if (MessageBox.Show("¿Está seguro(a) de modificar las cláusulas de ésta propuesta?", "Modificar cláusulas", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }else
                    {

                    }
                }
            }
            frmClientesBusqueda frm = new frmClientesBusqueda();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                barrios barr = new barrios();
                barr.id = barr.get_id(listBox1.Items[i].ToString());
                DataSet dsBarr = barr.get();
                if (dsBarr.Tables[0].Rows.Count > 0)
                {
                    frm.dataGridView1.Rows.Add(
                        dsBarr.Tables[0].Rows[0]["id"].ToString(),
                        listBox1.Items[i].ToString(),
                        dsBarr.Tables[0].Rows[0]["suma_muerte"].ToString(),
                        dsBarr.Tables[0].Rows[0]["suma_gm"].ToString(),
                        dsBarr.Tables[0].Rows[0]["email"].ToString()
                    );

                    
                }
                else
                {
                    frm.dataGridView1.Rows.Add(barr.get_id(listBox1.Items[i].ToString()), listBox1.Items[i].ToString());
                }

            }

            
            if (frm.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                json_barrios_propuesta.Clear();

                for (int i = 0; i < frm.dataGridView1.Rows.Count; i++)
                {
                    if (frm.dataGridView1.Rows[i].Cells["nombre"].Value != null)
                    {
                        listBox1.Items.Add(
                            frm.dataGridView1.Rows[i].Cells["nombre"].Value.ToString()
                        );

                        if (frm.dataGridView1.Rows[i].Cells["muerte"].Value != null && frm.dataGridView1.Rows[i].Cells["gm"].Value != null) {
                            listBox2.Items.Add(
                                " Mte: " + this.exigencias(frm.dataGridView1.Rows[i].Cells["muerte"].Value.ToString(), 1) +
                                "   -   GM:" +
                                this.exigencias(frm.dataGridView1.Rows[i].Cells["gm"].Value.ToString(), 2)
                            );
                            barrios bar = new barrios();
                            json_barrios_propuesta.Add(
                                new barrios_propuesta()
                                {
                                    id_barrio = bar.formated_string_query(frm.dataGridView1.Rows[i].Cells["id"].Value.ToString()),
                                    nombre = bar.formated_string_query(frm.dataGridView1.Rows[i].Cells["nombre"].Value.ToString()),
                                    sumamuerte = bar.formated_string_query(frm.dataGridView1.Rows[i].Cells["muerte"].Value.ToString()),
                                    sumagm = bar.formated_string_query(frm.dataGridView1.Rows[i].Cells["gm"].Value.ToString()),
                                    email = bar.formated_string_query(frm.dataGridView1.Rows[i].Cells["email"].Value.ToString()),
                                }
                            );
                        }
                        else
                        {
                            listBox2.Items.Add("");
                        }

                    }
                }

            }

            if (listBox1.Items.Count > 0)
            {
                checkBox1.Checked = true;
                vScrollBar1.Minimum = 0;
                vScrollBar1.Maximum = listBox1.Items.Count - 3;
            }

            if (this.edit)
            {
                if (btnGuardar.Enabled == false && button5.Enabled == false)
                {
                    
                    propuestas pro1 = new propuestas();
                    pro1.data_barrios = "{\"barrios\":" + JsonConvert.SerializeObject(json_barrios_propuesta) + "}";
                    pro1.modificacionClausulas(lblPrefijo.Text, lblidpropuesta.Text);

                    AuditoriaBarrios aud = new AuditoriaBarrios();
                    aud.prefijo = lblPrefijo.Text;
                    aud.idpropuesta = lblidpropuesta.Text;
                    aud.user_edit = MDIParent1.sesionUser;
                    aud.barrios_despues = "{\"barrios\":" + JsonConvert.SerializeObject(json_barrios_propuesta) + "}";
                    aud.barrios_antes = barrios_antes;
                    aud.save();
                    

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
            } catch {

            }


            string res = espacios + dato;


            return res;
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*int li = listBox1.SelectedIndex;
            if (li >= 0)
            {
                listBox1.Items.RemoveAt(li);
                listBox2.Items.RemoveAt(li);
            }*/
            if (listBox1.Items.Count > 0)
            {
                listBox2.SelectedIndex = listBox1.SelectedIndex;
                listBox2.TopIndex = listBox1.TopIndex;
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.correoargentino.com.ar/formularios/cpa");
            codpostalactivo = true;
            this.buscarCodPostal(txtCodpostal.Text);
        }

        private void buscarCodPostal(string texto)
        {
            if (codpostalactivo)
            {
                frmVistaProvincias frm = new frmVistaProvincias();
                frm.busqueda_txt.Text = texto;
                frm.busqueda();
                frm.seleccionar_btn.Select();
                codpostalactivo = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (dataGridView1.Rows.Count > 0)
                    {
                        txtCodpostal.Text = frm.dataGridView1.CurrentRow.Cells["codpostal"].Value.ToString().Trim();
                        txtLocalidad.Text = frm.dataGridView1.CurrentRow.Cells["provincia"].Value.ToString().Trim();
                        txtCiudad.Text = frm.dataGridView1.CurrentRow.Cells["ciudad"].Value.ToString().Trim();
                    }
                }
            }

        }


        public bool polizamanana(bool avisos = true, string tipovalidacion = "")
        {
            bool res = true;

            DateTime horacerrar = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 18:00:00"));
            bool arqueocerrrado = false;
            arqueos ar = new arqueos();
            if (ar.get_fecha_user(DateTime.Now.ToString("yyyy-MM-dd"), MDIParent1.sesionUser))
            {
                if (ar.supervisor != "" && ar.supervisor != null)
                {
                    arqueocerrrado = true;
                }
            }

            informes info = new informes();
            bool findeldia = false;
            DataSet infoDs = info.get_tipo_informe("FINDIA", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (infoDs.Tables[0].Rows.Count > 0)
            {
                findeldia = true;
                if (avisos && Convert.ToBoolean(Properties.Settings.Default["avisoCierre"]) == false)
                {
                    MessageBox.Show("Ya se ha generado el informe fin del día");
                    Properties.Settings.Default["avisoCierre"] = true;
                    res = false;
                }

            }

            //MessageBox.Show("--> Arqueo "+ arqueocerrrado+ " Findeldia "+
            //findeldia+ " MEnor cero "+ DateTime.Compare(horacerrar, DateTime.Now)+ " DÍA "+ DateTime.Now.DayOfWeek.ToString());

            if (!avisos)
            {
                if ((DateTime.Compare(horacerrar, DateTime.Now) < 0 || arqueocerrrado || findeldia
                || DateTime.Now.DayOfWeek.ToString() == "Saturday"
                || DateTime.Now.DayOfWeek.ToString() == "Sunday"))
                {
                    if (tipovalidacion != "edicionpostfindeldia")
                        MessageBox.Show("El arqueo ya ha sido cerrado o se ha generado el archivo fin del día, se pagará la propuesta con fecha del próximo día hábil");
                    res = false;
                }
            }

            if ((DateTime.Compare(horacerrar, DateTime.Now) < 0 || arqueocerrrado || findeldia
                || DateTime.Now.DayOfWeek.ToString() == "Saturday"
                || DateTime.Now.DayOfWeek.ToString() == "Sunday"))
            {

                festivos fes = new festivos();



                fes.fecha = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                bool festivomanana = fes.get_fecha();
                int contador = 0;

                if (horacerrar.DayOfWeek.ToString() == "Friday")
                {
                    contador = 3;
                    fes.fecha = DateTime.Now.AddDays(contador).ToString("yyyy-MM-dd");

                    while (fes.get_fecha())
                    {
                        fes.fecha = DateTime.Now.AddDays(contador++).ToString("yyyy-MM-dd");

                    }
                }

                if (horacerrar.DayOfWeek.ToString() == "Saturday")
                {
                    contador = 2;
                    fes.fecha = DateTime.Now.AddDays(contador).ToString("yyyy-MM-dd");
                    while (fes.get_fecha())
                    {
                        fes.fecha = DateTime.Now.AddDays(contador++).ToString("yyyy-MM-dd");
                    }
                }

                if (horacerrar.DayOfWeek.ToString() == "Sunday")
                {
                    contador = 1;
                    fes.fecha = DateTime.Now.AddDays(contador).ToString("yyyy-MM-dd");
                    while (fes.get_fecha())
                    {
                        fes.fecha = DateTime.Now.AddDays(contador++).ToString("yyyy-MM-dd");
                    }
                }

                this.guardamanana = true;
                if (tipovalidacion == "")
                {
                    if(DateTime.Compare(fechaDesde.Value, DateTime.Now) < 0)
                        fechaDesde.Value = Convert.ToDateTime(fes.fecha + " 08:00:00");
                    this.fechacorrida = Convert.ToDateTime(fes.fecha + " 08:00:00");
                }


                if (avisos && Convert.ToBoolean(Properties.Settings.Default["avisoOtroDia"]) == false)
                {
                    MessageBox.Show("Ésta propuesta se guardará con fecha de " + fes.fecha);
                    Properties.Settings.Default["avisoOtroDia"] = true;
                }




            }

            if (this.guardamanana)
            {
                fechacierresiguientediahabil = fechaDesde.Value;
                res = false;
            }


            return res;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            this.formpago_ = "CONTADO";
            paga_ch.Checked = true;
            this.guardarPropuesta();
            this.ocultarBotonGuardar();

        }

        private void guardarPropuesta()
        {
            if (!this.polizamanana() && this.edit)
            {
                if (DateTime.Compare(this.fechapropuesta.Date, DateTime.Now.Date) == 0)
                {
                    MessageBox.Show("No puede editar ésta propuesta, ya se ha cerrado el día o generado el informe", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            this.suma_premio();

            if (revisar_cant_polizas() < 0 || dataGridView1.Rows.Count < 2)
            {
                MessageBox.Show("No se ha agregado pólizas o no están bien diligenciadas", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (edit)
            {
                if (MessageBox.Show("¿Está seguro(a) que desea alterar la propuesta ya grabada previamente?", "Guardar cambios", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.guardado();
                }
            }
            else
            {
                this.guardado();
            }

        }

        private void frmNuevaPropuesta_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (edit == false)
            {
                validaciones val = new validaciones();
                bool secierra = false;
                string[] descartados = { "comboSituacion" };
                //MessageBox.Show("TOMADOR " + val.alguncampocondatos_grupos(this.groupBox1, descartados));
                if (val.alguncampocondatos_grupos(this.groupBox1, descartados))
                {

                    secierra = true;
                }

                if (this.revisar_cant_polizas() > 0)
                {
                    secierra = true;
                }
                string[] descartados1 = { "fechaDesde", "fechaHasta", "txtFechanacimiento" };
                //MessageBox.Show("Premio " + val.alguncampocondatos_grupos(this.groupBox3, descartados1));
                if (val.alguncampocondatos_grupos(this.groupBox3, descartados1))
                {

                    secierra = true;
                }



                if (secierra == true)
                {
                    if (MessageBox.Show("¿Está seguro(a) de cerrar el formulario sin guardar los cambios?", "Cerrando formulario", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }

            if(this.duplicado_ && btnGuardar.Enabled == true && button5.Enabled == true)
            {
                this.suma_premio();
                MessageBox.Show("Por favor elige una forma de pago antes de cerrar", "Error forma de pago", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }

        }

        private void txtCodpostal_TextChanged(object sender, EventArgs e)
        {
            if (txtCodpostal.Text.Length > 2)
            {
                this.buscarCodPostal(txtCodpostal.Text);
            }
            /*if (txtCodpostal.Text.Length < 4)
            {
                codpostalactivo = true;
            }*/
        }

        private void txtLocalidad_TextChanged(object sender, EventArgs e)
        {
            if (txtLocalidad.Text.Length > 2)
            {
                this.buscarCodPostal(txtLocalidad.Text);
            }
            /*if(txtLocalidad.Text.Length < 3)
            {
                codpostalactivo = true;
            }*/
        }

        private void txtCiudad_TextChanged(object sender, EventArgs e)
        {
            if (txtCiudad.Text.Length > 2)
            {
                this.buscarCodPostal(txtCiudad.Text);
            }
            /* if (txtCiudad.Text.Length < 3)
             {
                 codpostalactivo = true;
             }*/
            if (txtCiudad.Text == "")
            {
                codpostalactivo = true;
            }
        }

        private void txtCodpostal_Click(object sender, EventArgs e)
        {
            codpostalactivo = true;
        }

        private void txtLocalidad_Click(object sender, EventArgs e)
        {
            codpostalactivo = true;
        }

        private void txtCiudad_Click(object sender, EventArgs e)
        {
            codpostalactivo = true;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message == "DataGridviewComboBoxCell value is not valid.")
            {
                object value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (!((DataGridViewComboBoxColumn)dataGridView1.Columns[e.ColumnIndex]).Items.Contains(value))
                {
                    ((DataGridViewComboBoxColumn)dataGridView1.Columns[e.ColumnIndex]).Items.Add(value);
                    e.ThrowException = false;
                }
            }
        }

        private void mail_btn_Click(object sender, EventArgs e)
        {
            this.enviarMail();
        }

        public void enviarMail()
        {

            mailBarrios enviar = new mailBarrios();
            clientes cl1 = new clientes();
            DataSet clientes = cl1.get(textBox1.Text.Trim());
            string url = path + @"\EmisiónBarriosPrivados-No" + lblPrefijo.Text + "-" + lblidpropuesta.Text + ".pdf";
            string url1 = path + @"\ReciboPropuesta-No" + lblPrefijo.Text + "-" + lblidpropuesta.Text + ".pdf";
            string correosBarrios = this.revisarMailBarrios();
            if (txtEmail.Text.Trim() != "" || correosBarrios != "")
            {
                
                bool exitoEnvio = false;

                /*if (paga_ch.Checked)
                {*/
                    if (MessageBox.Show("¿Recibo y emisión pagado?", "PDF's para email", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.generarReciboPdf(false, true);
                        this.generarEmisionPdf(false, true);
                    }
                    else
                    {
                        this.generarReciboPdf(false, false);
                        this.generarEmisionPdf(false, false);
                    }
                /*}
                else
                {
                    this.generarReciboPdf(false, false);
                    this.generarEmisionPdf(false, false);
                }*/


                string mensaje = "Estimado cliente, muy buen día!</br>" +
                    "<p>Es un placer informarle que a partir de la fecha esta asegurado(a) y en anexo econtrará el comprobante del seguro adquirido.</p>" +
                    "<p>Muchas gracias por la confianza y quedamos a su entera disposición. Su tranquilidad vale!</p>" +
                    "<p>Gracias, saludos cordiales.</p>" +
                    "<p><br> Tel. (03327 - 485189) Cel. 15 - 55841038 <br> Sarmiento 3314(1621 - Benavidez) <br> <a href='https://brokerdelpuerto.com/'> www.brokerdelpuerto.com</a></p>" +
                    "<p><div ><img  src='http://apibarrios.3enterprise.online/piecorreo.jpg' ></div></p>";
                if (listBox1.Items.Count == 0 || (listBox1.Items.Count > 0 && correosBarrios != ""))
                {
                    if(txtEmail.Text != "")
                    {
                        string[] mails = this.getMailsClient(txtEmail.Text);

                        if (mails.Count() == 0)
                        {
                            txtEmail.Focus();
                            return;
                        }

                        foreach (string mail in mails)
                        {
                            if (mail.Trim() != "")
                            {
                                //Para el cliente
                                exitoEnvio = enviar.enviarMail(mail.Trim(), "Referencia Emisión Sr.(ra) " +
                                    clientes.Tables[0].Rows[0]["id"].ToString() + " | " + clientes.Tables[0].Rows[0]["nombres"].ToString() +
                                    " " + clientes.Tables[0].Rows[0]["apellidos"].ToString(), mensaje, url + "," + url1, "Certificado Bróker del Puerto");
                            }
                        }
                    }
                }

                mensaje = "Buen día!</br>" +
                    "<p>Informamos que a partir de la fecha se ha asegurado poliza a la(s) persona(s) en el anexo descrita(s), econtrará el comprobante del seguro adquirido.</p>" +
                    "<p>Gracias, saludos cordiales.</p>" +
                    "<p><br> Tel. (03327 - 485189) Cel. 15 - 55841038 <br> Sarmiento 3314(1621 - Benavidez) <br> <a href='https://brokerdelpuerto.com/'> www.brokerdelpuerto.com</a></p>" +
                    "<p><div ><img  src='http://apibarrios.3enterprise.online/piecorreo.jpg' ></div></p>";

                if (listBox1.Items.Count > 0)
                {

                    if (correosBarrios != "")
                    {
                        //Para el barrio
                        exitoEnvio = enviar.enviarMail(
                            correosBarrios, 
                            "Referencia Emisión Sr.(ra) " + clientes.Tables[0].Rows[0]["id"].ToString() + " | " + clientes.Tables[0].Rows[0]["nombres"].ToString() + " " + clientes.Tables[0].Rows[0]["apellidos"].ToString(),
                            mensaje, 
                            url + "," + url1, 
                            "Certificado Bróker del Puerto"
                            );
                    }
                }


                if (exitoEnvio)
                {
                    MessageBox.Show("Correo(s) enviado(s) con éxito");
                    enviar.objmail.Attachments.Dispose();
                }
                else
                {
                    MessageBox.Show("Error al enviar el correo ", "Error email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                    

            }
            else
            {
                MessageBox.Show("El cliente no tiene un correo electrónico  registrado", "Error email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public string[] getMailsClient(string mailtxt)
        {
            string[] mails;
            string mailerrores = "";
            if (mailtxt != "")
            {
                mails = mailtxt.Split(',');
                validaciones val = new validaciones();
                foreach (string mail in mails)
                {
                    if (!val.correo(mail.Trim()) && mailtxt != "")
                    {
                        mailerrores += "\n" + mail.Trim();
                    }
                }
            }
            else
            {
                mails = new string[0];
            }

            if (mailerrores != "")
            {
                mails = new string[0];
                MessageBox.Show("El o los correos " + mailerrores + "\nestá mal escrito, corríjalo por favor", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return mails;
        }

        public string revisarMailBarrios()
        {
            string id = "";
            string concatenar = "";
            int cont = 0;
            string correosNo = "";
            ListBox lis = new ListBox();

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                barrios bar = new barrios();
                id = bar.get_id(listBox1.Items[i].ToString());
                bar = new barrios();
                bar.id = id;
                DataSet ds = bar.get();


                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["email"].ToString() != "")
                    {
                        if (!this.revisarSiExisteCorreo(lis, ds.Tables[0].Rows[0]["email"].ToString()))
                        {
                            if (cont == 0)
                                concatenar += ds.Tables[0].Rows[0]["email"].ToString();
                            if (cont > 0)
                                concatenar += "," + ds.Tables[0].Rows[0]["email"].ToString();

                            cont++;

                            lis.Items.Add(ds.Tables[0].Rows[0]["email"].ToString());
                        }
                    }
                    else
                    {
                        correosNo += ds.Tables[0].Rows[0]["nombre"].ToString() + "\n";
                    }
                }


            }

            if (correosNo != "")
            {
                MessageBox.Show("No hay la misma cantidad de correos como barrios falta correo de : \n" + correosNo, "Error email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return concatenar;
        }

        private bool revisarSiExisteCorreo(ListBox li, string dato)
        {
            if (li.Items.Count > 0)
            {
                for (int i = 0; i < li.Items.Count; i++)
                {
                    //Console.WriteLine("-> LIS "+ li.Items[i].ToString().Trim() + " DAT "+ dato.Trim());
                    if (li.Items[i].ToString().Trim() == dato.Trim())
                        return true;
                }
            }

            return false;
        }

        private void crearCarpeta(string path_)
        {
            try
            {
                if (Directory.Exists(@path_))
                {
                    Console.WriteLine("La carpeta ya existe");
                    return;
                }
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(@path_);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string sexoMensaje = "Estimado ";
            if (radioButton1.Checked)
                sexoMensaje = "Estimada ";


            string mensaje = sexoMensaje + txtNombres.Text
                + " " + txtApellidos.Text + ", Tenga un Excelente día!. Está Recibiendo su certificado de Cobertura y Recibo. Gracias por Preferirnos." +
                " No olvide que puede renovar su póliza o emitir cualquier seguro que necesite, contactándonos por este medio \n – Equipo Broker del Puerto.";

            mensaje = mensaje.Replace(" ", "%20");
            this.crearCarpeta(path);
            bool pago = false;
            if (paga_ch.Checked)
            {
                if (MessageBox.Show("¿Recibo y emisión paga?", "PAGADO", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    pago = true;
            }
            this.generarEmisionPdf(false, pago);
            this.generarReciboPdf(false, pago);
            abrirRutaFacturaScripts("https://api.whatsapp.com/send?phone=54" + txtTelefono.Text + "&text=" + mensaje);
            abrirRutaFacturaScripts(path);
        }


        public void abrirRutaFacturaScripts(string ruta)
        {
            if (ruta != "")
            {
                Process pros = new Process();
                try
                {
                    pros.StartInfo.FileName = @ruta;
                    pros.Start();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es posible que la ruta " + ruta + " no exista, que no se haya generado ningún documento del tomador\n" + e.Message);
                }

            }

        }

        private void btnInfoBarrios_Click(object sender, EventArgs e)
        {
            frmInfoBarrios frm = new frmInfoBarrios();
            frm.ShowDialog();
        }

        private void btnDuplicar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está Segur@ de duplicar la propuesta?", "Duplicar Propuesta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                btnGuardar.Enabled = true;
                button5.Enabled = true;
                this.suma_premio();
                if (this.guardado(true)){
                    paga_ch.Enabled = true;
                    this.duplicado_ = true;
                    dataGridView1.ReadOnly = false;
                    botEliminarFila = true;
                    groupBox1.Enabled = true;
                    //groupBox3.Enabled = false;
                    fechaDesde.Enabled = true;
                    fechaHasta.Enabled = true;
                    listBox1.Enabled = true;
                    comboCobertura.Enabled = true;
                    groupBox2.Enabled = true;
                    comboBox4.Enabled = true;
                    button2.Enabled = true;
                    checkBox1.Enabled = true;
                    checkBox2.Enabled = true;
                    referencianum_txt.Text = "";
                }
                else
                {
                    btnGuardar.Enabled = false;
                    button5.Enabled = false;
                }
            }

        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() != "")
            {
                this.getMailsClient(txtEmail.Text);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            int li = listBox1.SelectedIndex;
            if (li >= 0)
            {
                listBox1.Items.RemoveAt(li);
                listBox2.Items.RemoveAt(li);
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            listBox1.TopIndex = vScrollBar1.Value;
            listBox2.TopIndex = vScrollBar1.Value;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = listBox2.SelectedIndex;
                listBox1.TopIndex = listBox2.TopIndex;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.abrirRutaFacturaScripts(path);
        }

        private void libreDeuda_btn_Click(object sender, EventArgs e)
        {

            this.visibleLibreDeuda();
            if(libreDeuda_btn.Visible == true)
            {
                propuestas pro = new propuestas();
                if (pro.get_paga(lblPrefijo.Text, lblidpropuesta.Text))
                {
                    frmLibreDeuda frm = new frmLibreDeuda();
                    frm.dsPropuesta = pro.getprefijo(lblPrefijo.Text, lblidpropuesta.Text);
                    frm.ShowDialog();
                }
                else {
                    MessageBox.Show("No puede generar libre deuda de una propuesta no paga", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public void ocultarBotonGuardar()
        {
            if (this.formpago_ == "CREDITO")
            {
                btnGuardar.Enabled = false;
                paga_ch.Enabled = false;
            }
                
            if (this.formpago_ == "CONTADO")
            {
                button5.Enabled = false;
                paga_ch.Enabled = true;
            }
                
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.formpago_ = "CREDITO";
            paga_ch.Checked = false;
            this.guardarPropuesta();
            this.ocultarBotonGuardar();
        }

        private bool guardado(bool duplicar = false)
        {
            if (this.validacion(!duplicar))
            {
                this.guardar_cliente();
                
                if (duplicar)
                {
                    if ( this.guardar_propuesta(duplicar))
                    {
                        MessageBox.Show("Propuesta duplicada con éxito");
                    }
                }
                else
                {
                    if ( this.guardar_propuesta(duplicar))
                    {
                        MessageBox.Show("La propuesta se ha guardado con éxito");
                    }
                }

                edit = true;
                this.datosInciales();
                return true;

            }
            else
            {
                
                MessageBox.Show("No puede guardar los datos por lo siguiente : " + errores, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
