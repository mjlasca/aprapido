namespace ProyectoBrokerDelPuerto
{
    partial class frmFormadepago
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ValorPagado_txt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.fecha_comprobante = new System.Windows.Forms.DateTimePicker();
            this.ValorPoliza_txt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.search = new System.Windows.Forms.Button();
            this.configSavve = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.imageProcess = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "CBU",
            "TRANSFERENCIA",
            "MERCADO PAGO",
            "EFECTIVO",
            "OTRO"});
            this.comboBox1.Location = new System.Drawing.Point(12, 41);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(255, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Forma de pago";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 79);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(255, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "¿Cuál?";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "No. Comprobante";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 115);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(255, 20);
            this.textBox2.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 225);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(255, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ValorPagado_txt
            // 
            this.ValorPagado_txt.Location = new System.Drawing.Point(12, 154);
            this.ValorPagado_txt.Name = "ValorPagado_txt";
            this.ValorPagado_txt.Size = new System.Drawing.Size(128, 20);
            this.ValorPagado_txt.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Valor pagado";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Fecha del comprobante";
            // 
            // fecha_comprobante
            // 
            this.fecha_comprobante.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fecha_comprobante.Location = new System.Drawing.Point(12, 194);
            this.fecha_comprobante.MaxDate = new System.DateTime(3000, 2, 20, 0, 0, 0, 0);
            this.fecha_comprobante.Name = "fecha_comprobante";
            this.fecha_comprobante.Size = new System.Drawing.Size(128, 20);
            this.fecha_comprobante.TabIndex = 11;
            this.fecha_comprobante.Value = new System.DateTime(2024, 2, 29, 0, 0, 0, 0);
            // 
            // ValorPoliza_txt
            // 
            this.ValorPoliza_txt.Location = new System.Drawing.Point(151, 154);
            this.ValorPoliza_txt.Name = "ValorPoliza_txt";
            this.ValorPoliza_txt.ReadOnly = true;
            this.ValorPoliza_txt.Size = new System.Drawing.Size(116, 20);
            this.ValorPoliza_txt.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(148, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Valor póliza";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(273, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(15, 38);
            this.button2.TabIndex = 14;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(307, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(199, 39);
            this.label7.TabIndex = 15;
            this.label7.Text = "Configure la ruta donde se guardarán las\r\nimágenes, sólo necesita configurarla un" +
    "a\r\nvez y cambiarla cuando sea necesario";
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(310, 131);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(196, 20);
            this.search.TabIndex = 14;
            this.search.Text = "Buscar ruta";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // configSavve
            // 
            this.configSavve.Location = new System.Drawing.Point(310, 157);
            this.configSavve.Name = "configSavve";
            this.configSavve.Size = new System.Drawing.Size(196, 20);
            this.configSavve.TabIndex = 16;
            this.configSavve.Text = "Guardar configuración";
            this.configSavve.UseVisualStyleBackColor = true;
            this.configSavve.Click += new System.EventHandler(this.configSavve_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(310, 67);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(196, 58);
            this.textBox3.TabIndex = 17;
            // 
            // imageProcess
            // 
            this.imageProcess.BackColor = System.Drawing.Color.Gray;
            this.imageProcess.ForeColor = System.Drawing.Color.White;
            this.imageProcess.Location = new System.Drawing.Point(151, 180);
            this.imageProcess.Name = "imageProcess";
            this.imageProcess.Size = new System.Drawing.Size(116, 34);
            this.imageProcess.TabIndex = 18;
            this.imageProcess.Text = "Guardar Imagen";
            this.imageProcess.UseVisualStyleBackColor = false;
            this.imageProcess.Click += new System.EventHandler(this.imageProcess_Click);
            // 
            // frmFormadepago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 260);
            this.Controls.Add(this.imageProcess);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.configSavve);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.search);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ValorPoliza_txt);
            this.Controls.Add(this.fecha_comprobante);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ValorPagado_txt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "frmFormadepago";
            this.Text = "Forma de Pago";
            this.Load += new System.EventHandler(this.frmFormadepago_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox ValorPoliza_txt;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.Button configSavve;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button imageProcess;
        public System.Windows.Forms.TextBox ValorPagado_txt;
        public System.Windows.Forms.DateTimePicker fecha_comprobante;
    }
}