namespace ProyectoBrokerDelPuerto
{
    partial class frmPropuestas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.busqueda = new System.Windows.Forms.TextBox();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnVer = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prefijo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idpropuestaprefijo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idPropuesta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cobertura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.useredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vigencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.premio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formadepago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chVigentes = new System.Windows.Forms.CheckBox();
            this.chNoVigentes = new System.Windows.Forms.CheckBox();
            this.chAnuladas = new System.Windows.Forms.CheckBox();
            this.chTodas = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.fec1 = new System.Windows.Forms.DateTimePicker();
            this.fec2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.referencia_txt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.empleado_txt = new System.Windows.Forms.ComboBox();
            this.pagado_ch = new System.Windows.Forms.CheckBox();
            this.sinpagar_ch = new System.Windows.Forms.CheckBox();
            this.btnPagar = new System.Windows.Forms.Button();
            this.libreDeuda_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Ingrese: ID o Nombre o Número de Propuesta";
            // 
            // busqueda
            // 
            this.busqueda.Location = new System.Drawing.Point(22, 36);
            this.busqueda.Name = "busqueda";
            this.busqueda.Size = new System.Drawing.Size(277, 20);
            this.busqueda.TabIndex = 16;
            this.busqueda.TextChanged += new System.EventHandler(this.busqueda_TextChanged);
            // 
            // btnAnular
            // 
            this.btnAnular.Enabled = false;
            this.btnAnular.Location = new System.Drawing.Point(264, 520);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(115, 23);
            this.btnAnular.TabIndex = 12;
            this.btnAnular.Text = "Anular Propuesta";
            this.btnAnular.UseVisualStyleBackColor = true;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnVer
            // 
            this.btnVer.Enabled = false;
            this.btnVer.Location = new System.Drawing.Point(143, 520);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(115, 23);
            this.btnVer.TabIndex = 13;
            this.btnVer.Text = "Ver Propuesta";
            this.btnVer.UseVisualStyleBackColor = true;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(22, 520);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Nueva Propuesta";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(779, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.referencia,
            this.prefijo,
            this.idpropuestaprefijo,
            this.idPropuesta,
            this.documento,
            this.nombre,
            this.fecha,
            this.cobertura,
            this.useredit,
            this.vigencia,
            this.premio,
            this.estado,
            this.formadepago,
            this.paga});
            this.dataGridView1.Location = new System.Drawing.Point(22, 83);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(859, 431);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // referencia
            // 
            this.referencia.FillWeight = 50.26117F;
            this.referencia.HeaderText = "Referencia / Póliza";
            this.referencia.Name = "referencia";
            this.referencia.ReadOnly = true;
            // 
            // prefijo
            // 
            this.prefijo.FillWeight = 30F;
            this.prefijo.HeaderText = "Prefijo";
            this.prefijo.Name = "prefijo";
            this.prefijo.ReadOnly = true;
            // 
            // idpropuestaprefijo
            // 
            this.idpropuestaprefijo.HeaderText = "No. Propuesta";
            this.idpropuestaprefijo.Name = "idpropuestaprefijo";
            this.idpropuestaprefijo.ReadOnly = true;
            // 
            // idPropuesta
            // 
            this.idPropuesta.FillWeight = 39.53878F;
            this.idPropuesta.HeaderText = "No. Propuesta";
            this.idPropuesta.Name = "idPropuesta";
            this.idPropuesta.ReadOnly = true;
            this.idPropuesta.Visible = false;
            // 
            // documento
            // 
            this.documento.FillWeight = 49.42349F;
            this.documento.HeaderText = "Documento";
            this.documento.Name = "documento";
            this.documento.ReadOnly = true;
            // 
            // nombre
            // 
            this.nombre.FillWeight = 118.6163F;
            this.nombre.HeaderText = "Apellido y Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // fecha
            // 
            this.fecha.FillWeight = 98.84701F;
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            // 
            // cobertura
            // 
            this.cobertura.FillWeight = 39.53878F;
            this.cobertura.HeaderText = "Cobertura";
            this.cobertura.Name = "cobertura";
            this.cobertura.ReadOnly = true;
            // 
            // useredit
            // 
            this.useredit.FillWeight = 98.84701F;
            this.useredit.HeaderText = "Empleado";
            this.useredit.Name = "useredit";
            this.useredit.ReadOnly = true;
            // 
            // vigencia
            // 
            this.vigencia.FillWeight = 102.0715F;
            this.vigencia.HeaderText = "Vigencia";
            this.vigencia.Name = "vigencia";
            this.vigencia.ReadOnly = true;
            // 
            // premio
            // 
            this.premio.FillWeight = 102.0715F;
            this.premio.HeaderText = "Premio";
            this.premio.Name = "premio";
            this.premio.ReadOnly = true;
            // 
            // estado
            // 
            this.estado.HeaderText = "Estado";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            this.estado.Visible = false;
            // 
            // formadepago
            // 
            this.formadepago.HeaderText = "Forma Pago";
            this.formadepago.Name = "formadepago";
            this.formadepago.ReadOnly = true;
            // 
            // paga
            // 
            this.paga.FillWeight = 51.03574F;
            this.paga.HeaderText = "Paga";
            this.paga.Name = "paga";
            this.paga.ReadOnly = true;
            // 
            // chVigentes
            // 
            this.chVigentes.AutoSize = true;
            this.chVigentes.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chVigentes.Location = new System.Drawing.Point(22, 60);
            this.chVigentes.Name = "chVigentes";
            this.chVigentes.Size = new System.Drawing.Size(67, 17);
            this.chVigentes.TabIndex = 18;
            this.chVigentes.Text = "Vigentes";
            this.chVigentes.UseVisualStyleBackColor = false;
            this.chVigentes.CheckedChanged += new System.EventHandler(this.chVigentes_CheckedChanged);
            // 
            // chNoVigentes
            // 
            this.chNoVigentes.AutoSize = true;
            this.chNoVigentes.BackColor = System.Drawing.SystemColors.ControlLight;
            this.chNoVigentes.Location = new System.Drawing.Point(92, 60);
            this.chNoVigentes.Name = "chNoVigentes";
            this.chNoVigentes.Size = new System.Drawing.Size(86, 17);
            this.chNoVigentes.TabIndex = 18;
            this.chNoVigentes.Text = "NO Vigentes";
            this.chNoVigentes.UseVisualStyleBackColor = false;
            this.chNoVigentes.CheckedChanged += new System.EventHandler(this.chNoVigentes_CheckedChanged);
            // 
            // chAnuladas
            // 
            this.chAnuladas.AutoSize = true;
            this.chAnuladas.BackColor = System.Drawing.Color.Red;
            this.chAnuladas.Location = new System.Drawing.Point(181, 61);
            this.chAnuladas.Name = "chAnuladas";
            this.chAnuladas.Size = new System.Drawing.Size(70, 17);
            this.chAnuladas.TabIndex = 18;
            this.chAnuladas.Text = "Anuladas";
            this.chAnuladas.UseVisualStyleBackColor = false;
            this.chAnuladas.CheckedChanged += new System.EventHandler(this.chAnuladas_CheckedChanged);
            // 
            // chTodas
            // 
            this.chTodas.AutoSize = true;
            this.chTodas.Location = new System.Drawing.Point(253, 60);
            this.chTodas.Name = "chTodas";
            this.chTodas.Size = new System.Drawing.Size(56, 17);
            this.chTodas.TabIndex = 18;
            this.chTodas.Text = "Todas";
            this.chTodas.UseVisualStyleBackColor = true;
            this.chTodas.CheckedChanged += new System.EventHandler(this.chTodas_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(766, 520);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Listado Vencimientos";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // fec1
            // 
            this.fec1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fec1.Location = new System.Drawing.Point(593, 36);
            this.fec1.Name = "fec1";
            this.fec1.Size = new System.Drawing.Size(80, 20);
            this.fec1.TabIndex = 20;
            this.fec1.ValueChanged += new System.EventHandler(this.fec1_ValueChanged);
            // 
            // fec2
            // 
            this.fec2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fec2.Location = new System.Drawing.Point(693, 36);
            this.fec2.Name = "fec2";
            this.fec2.Size = new System.Drawing.Size(80, 20);
            this.fec2.TabIndex = 20;
            this.fec2.ValueChanged += new System.EventHandler(this.fec2_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(679, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(305, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Referencia";
            // 
            // referencia_txt
            // 
            this.referencia_txt.Location = new System.Drawing.Point(308, 36);
            this.referencia_txt.Name = "referencia_txt";
            this.referencia_txt.Size = new System.Drawing.Size(110, 20);
            this.referencia_txt.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(424, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Empleado";
            // 
            // empleado_txt
            // 
            this.empleado_txt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.empleado_txt.FormattingEnabled = true;
            this.empleado_txt.Location = new System.Drawing.Point(427, 35);
            this.empleado_txt.Name = "empleado_txt";
            this.empleado_txt.Size = new System.Drawing.Size(155, 21);
            this.empleado_txt.TabIndex = 25;
            // 
            // pagado_ch
            // 
            this.pagado_ch.AutoSize = true;
            this.pagado_ch.Location = new System.Drawing.Point(315, 60);
            this.pagado_ch.Name = "pagado_ch";
            this.pagado_ch.Size = new System.Drawing.Size(63, 17);
            this.pagado_ch.TabIndex = 26;
            this.pagado_ch.Text = "Pagada";
            this.pagado_ch.UseVisualStyleBackColor = true;
            this.pagado_ch.CheckedChanged += new System.EventHandler(this.pagado_ch_CheckedChanged);
            // 
            // sinpagar_ch
            // 
            this.sinpagar_ch.AutoSize = true;
            this.sinpagar_ch.Location = new System.Drawing.Point(381, 60);
            this.sinpagar_ch.Name = "sinpagar_ch";
            this.sinpagar_ch.Size = new System.Drawing.Size(72, 17);
            this.sinpagar_ch.TabIndex = 27;
            this.sinpagar_ch.Text = "Sin Pagar";
            this.sinpagar_ch.UseVisualStyleBackColor = true;
            this.sinpagar_ch.CheckedChanged += new System.EventHandler(this.sinpagar_ch_CheckedChanged);
            // 
            // btnPagar
            // 
            this.btnPagar.Enabled = false;
            this.btnPagar.Location = new System.Drawing.Point(385, 520);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(115, 23);
            this.btnPagar.TabIndex = 28;
            this.btnPagar.Text = "Pagar";
            this.btnPagar.UseVisualStyleBackColor = true;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // libreDeuda_btn
            // 
            this.libreDeuda_btn.Enabled = false;
            this.libreDeuda_btn.Location = new System.Drawing.Point(506, 520);
            this.libreDeuda_btn.Name = "libreDeuda_btn";
            this.libreDeuda_btn.Size = new System.Drawing.Size(115, 23);
            this.libreDeuda_btn.TabIndex = 29;
            this.libreDeuda_btn.Text = "Libre de Deuda";
            this.libreDeuda_btn.UseVisualStyleBackColor = true;
            this.libreDeuda_btn.Click += new System.EventHandler(this.libreDeuda_btn_Click);
            // 
            // frmPropuestas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 555);
            this.Controls.Add(this.libreDeuda_btn);
            this.Controls.Add(this.btnPagar);
            this.Controls.Add(this.sinpagar_ch);
            this.Controls.Add(this.pagado_ch);
            this.Controls.Add(this.empleado_txt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.referencia_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fec2);
            this.Controls.Add(this.fec1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chTodas);
            this.Controls.Add(this.chAnuladas);
            this.Controls.Add(this.chNoVigentes);
            this.Controls.Add(this.chVigentes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.busqueda);
            this.Controls.Add(this.btnAnular);
            this.Controls.Add(this.btnVer);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmPropuestas";
            this.Text = "Propuestas";
            this.Load += new System.EventHandler(this.frmPropuestas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox busqueda;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chVigentes;
        private System.Windows.Forms.CheckBox chNoVigentes;
        private System.Windows.Forms.CheckBox chAnuladas;
        private System.Windows.Forms.CheckBox chTodas;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DateTimePicker fec1;
        private System.Windows.Forms.DateTimePicker fec2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox referencia_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox empleado_txt;
        private System.Windows.Forms.CheckBox pagado_ch;
        private System.Windows.Forms.CheckBox sinpagar_ch;
        private System.Windows.Forms.Button btnPagar;
        private System.Windows.Forms.DataGridViewTextBoxColumn referencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn prefijo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idpropuestaprefijo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPropuesta;
        private System.Windows.Forms.DataGridViewTextBoxColumn documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn cobertura;
        private System.Windows.Forms.DataGridViewTextBoxColumn useredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn vigencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn premio;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn formadepago;
        private System.Windows.Forms.DataGridViewTextBoxColumn paga;
        private System.Windows.Forms.Button libreDeuda_btn;
    }
}