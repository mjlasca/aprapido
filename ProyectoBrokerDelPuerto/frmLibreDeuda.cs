using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBrokerDelPuerto
{
    public partial class frmLibreDeuda : Form
    {
        public DataSet dsPropuesta = new DataSet();
        public DataSet dsLineas = new DataSet();
        string documento_ = "";


        public frmLibreDeuda()
        {
            InitializeComponent();
        } 
        

        private void iniciales()
        {
            lineas_propuestas li = new lineas_propuestas();
            this.dsLineas = li.get_idprefijo(this.dsPropuesta.Tables[0].Rows[0]["idpropuesta"].ToString(), this.dsPropuesta.Tables[0].Rows[0]["prefijo"].ToString());
            comboBox1.Items.Add("TODOS");
            clientes cli = new clientes();
            DataSet ds = cli.get(this.dsPropuesta.Tables[0].Rows[0]["documento"].ToString() );
            comboBox1.Items.Add(ds.Tables[0].Rows[0]["tipo_id"].ToString().ToUpper() + " : "+this.dsPropuesta.Tables[0].Rows[0]["documento"].ToString() + " | " + ds.Tables[0].Rows[0]["apellidos"].ToString() + " " + ds.Tables[0].Rows[0]["nombres"].ToString());
            for (int i = 0; i < this.dsLineas.Tables[0].Rows.Count; i++)
            {
                if (this.dsLineas.Tables[0].Rows[i]["documento"].ToString() != this.dsPropuesta.Tables[0].Rows[0]["documento"].ToString())
                    comboBox1.Items.Add(this.dsLineas.Tables[0].Rows[i]["tipo_documento"].ToString() + " : " + this.dsLineas.Tables[0].Rows[i]["documento"].ToString()+ " | "+ this.dsLineas.Tables[0].Rows[i]["apellidos"].ToString()+" "+ this.dsLineas.Tables[0].Rows[i]["nombres"].ToString());
            }
        }

        private void frmLibreDeuda_Load(object sender, EventArgs e)
        {
            this.iniciales();

            parametrosmailinforme para = new parametrosmailinforme();
            para.plantillatipo = "cerficidao_libre_deuda";
            if (para.get())
            {
                try
                {
                    validaciones val = new validaciones();
                    textBox1.Text = para.mensaje.Substring(0, para.mensaje.IndexOf("1#"));
                    empresa_txt.Text = val.fs_query(para.mensaje.Substring(para.mensaje.IndexOf("1#") + 2, (para.mensaje.IndexOf("2#") - 2 - para.mensaje.IndexOf("1#"))));
                    departamento_txt.Text = val.fs_query( para.mensaje.Substring(para.mensaje.IndexOf("2#") + 2, (para.mensaje.IndexOf("3#")- 2 - para.mensaje.IndexOf("2#"))));
                    responsable_txt.Text = val.fs_query( para.mensaje.Substring(para.mensaje.IndexOf("3#") + 2, (para.mensaje.IndexOf("4#") - 2 - para.mensaje.IndexOf("3#"))));
                }
                catch
                {

                    textBox1.Text = "Por intermedio de la presente certificamos, que la Póliza del Ramo Accidentes Personales [CONSECUTIVO] contratada por el asegurado" +
                    " [NOMBRE_ASEGURADO]  [DOCUMENTO] vigente entre el [FECHA_DESDE] y el [FECHA_HASTA], de facturación n 1 Cuota, conforme a nuestro registros se abona" +
                    " mediante efectivo, encontrándose abonada en su totalidad.\n Se extiende el presente certificado para ser presentado ante quien corresponda. ";
                }
            }
            else
            {
                textBox1.Text = "Por intermedio de la presente certificamos, que la Póliza del Ramo Accidentes Personales [CONSECUTIVO] contratada por el asegurado" +
                " [NOMBRE_ASEGURADO] [DOCUMENTO] vigente entre el [FECHA_DESDE] y el [FECHA_HASTA], de facturación n 1 Cuota, conforme a nuestro registros se abona" +
                " mediante efectivo, encontrándose abonada en su totalidad.\n Se extiende el presente certificado para ser presentado ante quien corresponda. ";
            }

        }

        private void btnGenerarCertificado_Click(object sender, EventArgs e)
        {
            
            if (comboBox1.Text != "")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                saveFileDialog.Filter = "Documento PDF (*.pdf)|*.pdf";
                saveFileDialog.FileName = "Certificado Libre deuda"+ this.dsPropuesta.Tables[0].Rows[0]["prefijo"].ToString() + this.dsPropuesta.Tables[0].Rows[0]["idpropuesta"].ToString() + ".pdf";
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string FileName = saveFileDialog.FileName;
                    this.generar_certificado(FileName);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cliente al menos", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string covnertir_texto()
        {
            
            string texto = textBox1.Text;

            texto = texto.Replace("[CONSECUTIVO]", "No. " + this.dsPropuesta.Tables[0].Rows[0]["prefijo"].ToString() + this.dsPropuesta.Tables[0].Rows[0]["idpropuesta"].ToString());
            if(comboBox1.Text != "")
            {
                int longtexto = comboBox1.Text.Length - 1;

                if (comboBox1.Text != "TODOS")
                {
                    longtexto = comboBox1.Text.Length - 1;

                    //MessageBox.Show("---> "+ comboBox1.Text.IndexOf("|") + " / "+ longtexto + " / "+ comboBox1.Text.Substring(comboBox1.Text.IndexOf("|") + 1, (longtexto - comboBox1.Text.IndexOf("|"))).Trim());
                    documento_ = comboBox1.Text.Substring(0, comboBox1.Text.IndexOf("|")).Trim();
                    texto = texto.Replace("[NOMBRE_ASEGURADO]", comboBox1.Text.Substring(comboBox1.Text.IndexOf("|") +1, (longtexto - comboBox1.Text.IndexOf("|"))).Trim());
                    texto = texto.Replace("[DOCUMENTO]", documento_);
                }
                else
                {

                    string concat = "";
                    for (int i = 1; i < comboBox1.Items.Count; i++)
                    {
                        longtexto = comboBox1.Items[i].ToString().Length - 1;
                        if (i == 1)
                            concat += comboBox1.Items[i].ToString().Substring(comboBox1.Items[i].ToString().IndexOf("|") + 1, (longtexto - comboBox1.Items[i].ToString().IndexOf("|"))).Trim() +
                                " " + comboBox1.Items[i].ToString().Substring(0, comboBox1.Items[i].ToString().IndexOf("|")).Trim() + " ";
                        else
                            concat += ", "+comboBox1.Items[i].ToString().Substring(comboBox1.Items[i].ToString().IndexOf("|") + 1, (longtexto - comboBox1.Items[i].ToString().IndexOf("|"))).Trim() +
                            " " + comboBox1.Items[i].ToString().Substring(0, comboBox1.Items[i].ToString().IndexOf("|")).Trim() + " ";
                    }

                    texto = texto.Replace("[NOMBRE_ASEGURADO]", concat);
                    texto = texto.Replace("[DOCUMENTO]", "");
                }
            }
            
                
            texto = texto.Replace("[FECHA_DESDE]", Convert.ToDateTime(this.dsPropuesta.Tables[0].Rows[0]["fechaDesde"].ToString()).ToString("dd/MM/yyyy"));
            texto = texto.Replace("[FECHA_HASTA]", Convert.ToDateTime(this.dsPropuesta.Tables[0].Rows[0]["fechaHasta"].ToString()).ToString("dd/MM/yyyy"));

            return texto;
            
            
        }

        private void generar_certificado( string path)
        {
            //try
            //{
                generarPdfs pdf = new generarPdfs();
                if(comboBox1.Text == "TODOS")
                    pdf.pdfCertificadoLibreDeuda(path, this.covnertir_texto(), empresa_txt.Text, departamento_txt.Text, responsable_txt.Text, dsPropuesta, comboBox1.Items.Count);
                else
                    pdf.pdfCertificadoLibreDeuda(path, this.covnertir_texto(), empresa_txt.Text, departamento_txt.Text, responsable_txt.Text, dsPropuesta);
            /*}
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido generar el certificado por \n"+ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

            parametrosmailinforme para = new parametrosmailinforme();
            para.mensaje = textBox1.Text + " 1# "+empresa_txt.Text+" 2# "+departamento_txt.Text+" 3# "+responsable_txt.Text+" 4# ";
            para.plantillatipo = "cerficidao_libre_deuda";
            para.ultmod = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            para.useredit = MDIParent1.sesionUser;
            para.save();

        }
    }
}
