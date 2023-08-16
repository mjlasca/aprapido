namespace ProyectoBrokerDelPuerto
{
    partial class frmMigraciones
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
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pass_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cargadatos = new System.Windows.Forms.Label();
            this.cbReset = new System.Windows.Forms.CheckBox();
            this.expimp = new System.Windows.Forms.ComboBox();
            this.chPropuestas = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chProvincias = new System.Windows.Forms.CheckBox();
            this.chTodo = new System.Windows.Forms.CheckBox();
            this.chCoberturas = new System.Windows.Forms.CheckBox();
            this.chClasificaciones = new System.Windows.Forms.CheckBox();
            this.chActividades = new System.Windows.Forms.CheckBox();
            this.chArqueos = new System.Windows.Forms.CheckBox();
            this.chRendiciones = new System.Windows.Forms.CheckBox();
            this.chPerfiles = new System.Windows.Forms.CheckBox();
            this.chUsuarios = new System.Windows.Forms.CheckBox();
            this.chBarrios = new System.Windows.Forms.CheckBox();
            this.chClientes = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(148, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Migrar Datos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(21, 36);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(145, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Contraseña";
            // 
            // pass_txt
            // 
            this.pass_txt.Location = new System.Drawing.Point(148, 37);
            this.pass_txt.Name = "pass_txt";
            this.pass_txt.PasswordChar = '*';
            this.pass_txt.Size = new System.Drawing.Size(115, 20);
            this.pass_txt.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Responsable";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(21, 94);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(320, 217);
            this.textBox1.TabIndex = 9;
            // 
            // cargadatos
            // 
            this.cargadatos.AutoSize = true;
            this.cargadatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cargadatos.Location = new System.Drawing.Point(21, 318);
            this.cargadatos.Name = "cargadatos";
            this.cargadatos.Size = new System.Drawing.Size(307, 13);
            this.cargadatos.TabIndex = 19;
            this.cargadatos.Text = "Se están procesando los datos, por favor no cierre la ventana...";
            this.cargadatos.Visible = false;
            // 
            // cbReset
            // 
            this.cbReset.AutoSize = true;
            this.cbReset.Location = new System.Drawing.Point(270, 39);
            this.cbReset.Name = "cbReset";
            this.cbReset.Size = new System.Drawing.Size(71, 17);
            this.cbReset.TabIndex = 20;
            this.cbReset.Text = "Info Base";
            this.cbReset.UseVisualStyleBackColor = true;
            this.cbReset.CheckedChanged += new System.EventHandler(this.cbReset_CheckedChanged);
            // 
            // expimp
            // 
            this.expimp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.expimp.FormattingEnabled = true;
            this.expimp.Items.AddRange(new object[] {
            "IMPORT / EXPORT",
            "SÓLO IMPORTAR",
            "SÓLO EXPORTAR"});
            this.expimp.Location = new System.Drawing.Point(21, 66);
            this.expimp.Name = "expimp";
            this.expimp.Size = new System.Drawing.Size(121, 21);
            this.expimp.TabIndex = 21;
            // 
            // chPropuestas
            // 
            this.chPropuestas.AutoSize = true;
            this.chPropuestas.Location = new System.Drawing.Point(18, 36);
            this.chPropuestas.Name = "chPropuestas";
            this.chPropuestas.Size = new System.Drawing.Size(79, 17);
            this.chPropuestas.TabIndex = 22;
            this.chPropuestas.Text = "Propuestas";
            this.chPropuestas.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chProvincias);
            this.groupBox1.Controls.Add(this.chTodo);
            this.groupBox1.Controls.Add(this.chCoberturas);
            this.groupBox1.Controls.Add(this.chClasificaciones);
            this.groupBox1.Controls.Add(this.chActividades);
            this.groupBox1.Controls.Add(this.chArqueos);
            this.groupBox1.Controls.Add(this.chRendiciones);
            this.groupBox1.Controls.Add(this.chPerfiles);
            this.groupBox1.Controls.Add(this.chUsuarios);
            this.groupBox1.Controls.Add(this.chBarrios);
            this.groupBox1.Controls.Add(this.chClientes);
            this.groupBox1.Controls.Add(this.chPropuestas);
            this.groupBox1.Location = new System.Drawing.Point(369, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(128, 290);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // chProvincias
            // 
            this.chProvincias.AutoSize = true;
            this.chProvincias.Location = new System.Drawing.Point(18, 266);
            this.chProvincias.Name = "chProvincias";
            this.chProvincias.Size = new System.Drawing.Size(75, 17);
            this.chProvincias.TabIndex = 33;
            this.chProvincias.Text = "Provincias";
            this.chProvincias.UseVisualStyleBackColor = true;
            // 
            // chTodo
            // 
            this.chTodo.AutoSize = true;
            this.chTodo.Location = new System.Drawing.Point(18, 13);
            this.chTodo.Name = "chTodo";
            this.chTodo.Size = new System.Drawing.Size(15, 14);
            this.chTodo.TabIndex = 32;
            this.chTodo.UseVisualStyleBackColor = true;
            this.chTodo.CheckedChanged += new System.EventHandler(this.chTodo_CheckedChanged);
            // 
            // chCoberturas
            // 
            this.chCoberturas.AutoSize = true;
            this.chCoberturas.Location = new System.Drawing.Point(18, 243);
            this.chCoberturas.Name = "chCoberturas";
            this.chCoberturas.Size = new System.Drawing.Size(77, 17);
            this.chCoberturas.TabIndex = 31;
            this.chCoberturas.Text = "Coberturas";
            this.chCoberturas.UseVisualStyleBackColor = true;
            // 
            // chClasificaciones
            // 
            this.chClasificaciones.AutoSize = true;
            this.chClasificaciones.Location = new System.Drawing.Point(18, 220);
            this.chClasificaciones.Name = "chClasificaciones";
            this.chClasificaciones.Size = new System.Drawing.Size(96, 17);
            this.chClasificaciones.TabIndex = 30;
            this.chClasificaciones.Text = "Clasificaciones";
            this.chClasificaciones.UseVisualStyleBackColor = true;
            // 
            // chActividades
            // 
            this.chActividades.AutoSize = true;
            this.chActividades.Location = new System.Drawing.Point(18, 197);
            this.chActividades.Name = "chActividades";
            this.chActividades.Size = new System.Drawing.Size(81, 17);
            this.chActividades.TabIndex = 29;
            this.chActividades.Text = "Actividades";
            this.chActividades.UseVisualStyleBackColor = true;
            // 
            // chArqueos
            // 
            this.chArqueos.AutoSize = true;
            this.chArqueos.Location = new System.Drawing.Point(18, 151);
            this.chArqueos.Name = "chArqueos";
            this.chArqueos.Size = new System.Drawing.Size(65, 17);
            this.chArqueos.TabIndex = 28;
            this.chArqueos.Text = "Arqueos";
            this.chArqueos.UseVisualStyleBackColor = true;
            // 
            // chRendiciones
            // 
            this.chRendiciones.AutoSize = true;
            this.chRendiciones.Location = new System.Drawing.Point(18, 174);
            this.chRendiciones.Name = "chRendiciones";
            this.chRendiciones.Size = new System.Drawing.Size(85, 17);
            this.chRendiciones.TabIndex = 27;
            this.chRendiciones.Text = "Rendiciones";
            this.chRendiciones.UseVisualStyleBackColor = true;
            // 
            // chPerfiles
            // 
            this.chPerfiles.AutoSize = true;
            this.chPerfiles.Location = new System.Drawing.Point(18, 128);
            this.chPerfiles.Name = "chPerfiles";
            this.chPerfiles.Size = new System.Drawing.Size(60, 17);
            this.chPerfiles.TabIndex = 26;
            this.chPerfiles.Text = "Perfiles";
            this.chPerfiles.UseVisualStyleBackColor = true;
            // 
            // chUsuarios
            // 
            this.chUsuarios.AutoSize = true;
            this.chUsuarios.Location = new System.Drawing.Point(18, 105);
            this.chUsuarios.Name = "chUsuarios";
            this.chUsuarios.Size = new System.Drawing.Size(67, 17);
            this.chUsuarios.TabIndex = 25;
            this.chUsuarios.Text = "Usuarios";
            this.chUsuarios.UseVisualStyleBackColor = true;
            // 
            // chBarrios
            // 
            this.chBarrios.AutoSize = true;
            this.chBarrios.Location = new System.Drawing.Point(18, 82);
            this.chBarrios.Name = "chBarrios";
            this.chBarrios.Size = new System.Drawing.Size(58, 17);
            this.chBarrios.TabIndex = 24;
            this.chBarrios.Text = "Barrios";
            this.chBarrios.UseVisualStyleBackColor = true;
            // 
            // chClientes
            // 
            this.chClientes.AutoSize = true;
            this.chClientes.Location = new System.Drawing.Point(18, 59);
            this.chClientes.Name = "chClientes";
            this.chClientes.Size = new System.Drawing.Size(63, 17);
            this.chClientes.TabIndex = 23;
            this.chClientes.Text = "Clientes";
            this.chClientes.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(346, 167);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(15, 23);
            this.button2.TabIndex = 24;
            this.button2.Text = "<";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // frmMigraciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 337);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.expimp);
            this.Controls.Add(this.cbReset);
            this.Controls.Add(this.cargadatos);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pass_txt);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Name = "frmMigraciones";
            this.Text = "Migraciones";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMigraciones_FormClosing);
            this.Load += new System.EventHandler(this.frmMigraciones_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox pass_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label cargadatos;
        private System.Windows.Forms.ComboBox expimp;
        private System.Windows.Forms.CheckBox chPropuestas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chClientes;
        private System.Windows.Forms.CheckBox chArqueos;
        private System.Windows.Forms.CheckBox chRendiciones;
        private System.Windows.Forms.CheckBox chPerfiles;
        private System.Windows.Forms.CheckBox chUsuarios;
        private System.Windows.Forms.CheckBox chBarrios;
        private System.Windows.Forms.CheckBox chActividades;
        private System.Windows.Forms.CheckBox chCoberturas;
        private System.Windows.Forms.CheckBox chClasificaciones;
        private System.Windows.Forms.CheckBox chTodo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chProvincias;
        public System.Windows.Forms.CheckBox cbReset;
    }
}