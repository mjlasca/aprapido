namespace ProyectoBrokerDelPuerto
{
    partial class frmComisiones
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.usuario_txt = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtcomPremio = new System.Windows.Forms.TextBox();
            this.claveliquidador_txt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.liquidadores_txt = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtcomPrima = new System.Windows.Forms.TextBox();
            this.txttotalPrima = new System.Windows.Forms.TextBox();
            this.txttotalPremio = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.idcomision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prefijo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propuesta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tomador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prima = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.premio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porcprima = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porpremio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comprima = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.compremio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // usuario_txt
            // 
            this.usuario_txt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.usuario_txt.FormattingEnabled = true;
            this.usuario_txt.Location = new System.Drawing.Point(28, 47);
            this.usuario_txt.Name = "usuario_txt";
            this.usuario_txt.Size = new System.Drawing.Size(137, 21);
            this.usuario_txt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Desde";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hasta";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idcomision,
            this.fecha,
            this.prefijo,
            this.propuesta,
            this.referencia,
            this.tomador,
            this.prima,
            this.premio,
            this.porcprima,
            this.porpremio,
            this.comprima,
            this.compremio});
            this.dataGridView1.Location = new System.Drawing.Point(29, 74);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(663, 365);
            this.dataGridView1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(251, 459);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Totales $";
            // 
            // txtcomPremio
            // 
            this.txtcomPremio.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcomPremio.Location = new System.Drawing.Point(607, 457);
            this.txtcomPremio.Name = "txtcomPremio";
            this.txtcomPremio.ReadOnly = true;
            this.txtcomPremio.Size = new System.Drawing.Size(85, 23);
            this.txtcomPremio.TabIndex = 8;
            this.txtcomPremio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // claveliquidador_txt
            // 
            this.claveliquidador_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.claveliquidador_txt.Location = new System.Drawing.Point(163, 32);
            this.claveliquidador_txt.Name = "claveliquidador_txt";
            this.claveliquidador_txt.PasswordChar = '*';
            this.claveliquidador_txt.Size = new System.Drawing.Size(96, 23);
            this.claveliquidador_txt.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Liquidador";
            // 
            // liquidadores_txt
            // 
            this.liquidadores_txt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.liquidadores_txt.FormattingEnabled = true;
            this.liquidadores_txt.Location = new System.Drawing.Point(15, 34);
            this.liquidadores_txt.Name = "liquidadores_txt";
            this.liquidadores_txt.Size = new System.Drawing.Size(137, 21);
            this.liquidadores_txt.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(160, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Contraseña";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.claveliquidador_txt);
            this.groupBox1.Controls.Add(this.liquidadores_txt);
            this.groupBox1.Location = new System.Drawing.Point(28, 480);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(664, 64);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(416, 32);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(132, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "Seleccionar otro usuario";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(279, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Liquidar comisión";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(421, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Ver estado";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(302, 47);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(99, 20);
            this.dateTimePicker2.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(186, 47);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(99, 20);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // txtcomPrima
            // 
            this.txtcomPrima.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcomPrima.Location = new System.Drawing.Point(516, 457);
            this.txtcomPrima.Name = "txtcomPrima";
            this.txtcomPrima.ReadOnly = true;
            this.txtcomPrima.Size = new System.Drawing.Size(85, 23);
            this.txtcomPrima.TabIndex = 15;
            this.txtcomPrima.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txttotalPrima
            // 
            this.txttotalPrima.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotalPrima.Location = new System.Drawing.Point(330, 457);
            this.txttotalPrima.Name = "txttotalPrima";
            this.txttotalPrima.ReadOnly = true;
            this.txttotalPrima.Size = new System.Drawing.Size(85, 23);
            this.txttotalPrima.TabIndex = 17;
            this.txttotalPrima.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txttotalPremio
            // 
            this.txttotalPremio.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttotalPremio.Location = new System.Drawing.Point(421, 457);
            this.txttotalPremio.Name = "txttotalPremio";
            this.txttotalPremio.ReadOnly = true;
            this.txttotalPremio.Size = new System.Drawing.Size(85, 23);
            this.txttotalPremio.TabIndex = 16;
            this.txttotalPremio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(355, 441);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Total Prima";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(441, 441);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Total Premio";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(513, 441);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Total Com. Prima";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(604, 441);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Total Com. Premio";
            // 
            // idcomision
            // 
            this.idcomision.HeaderText = "idcomision";
            this.idcomision.Name = "idcomision";
            this.idcomision.ReadOnly = true;
            this.idcomision.Visible = false;
            // 
            // fecha
            // 
            this.fecha.HeaderText = "Fecha registro pago";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            // 
            // prefijo
            // 
            this.prefijo.HeaderText = "Prefijo";
            this.prefijo.Name = "prefijo";
            this.prefijo.ReadOnly = true;
            // 
            // propuesta
            // 
            this.propuesta.HeaderText = "Propuesta";
            this.propuesta.Name = "propuesta";
            // 
            // referencia
            // 
            this.referencia.HeaderText = "Ref";
            this.referencia.Name = "referencia";
            // 
            // tomador
            // 
            this.tomador.HeaderText = "Tomador";
            this.tomador.Name = "tomador";
            // 
            // prima
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.prima.DefaultCellStyle = dataGridViewCellStyle1;
            this.prima.HeaderText = "Prima";
            this.prima.Name = "prima";
            // 
            // premio
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.premio.DefaultCellStyle = dataGridViewCellStyle2;
            this.premio.HeaderText = "Premio";
            this.premio.Name = "premio";
            // 
            // porcprima
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.porcprima.DefaultCellStyle = dataGridViewCellStyle3;
            this.porcprima.FillWeight = 50F;
            this.porcprima.HeaderText = "%Prima";
            this.porcprima.Name = "porcprima";
            this.porcprima.ReadOnly = true;
            // 
            // porpremio
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.porpremio.DefaultCellStyle = dataGridViewCellStyle4;
            this.porpremio.FillWeight = 50F;
            this.porpremio.HeaderText = "%Premio";
            this.porpremio.Name = "porpremio";
            this.porpremio.ReadOnly = true;
            // 
            // comprima
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.comprima.DefaultCellStyle = dataGridViewCellStyle5;
            this.comprima.HeaderText = "Com. Prima";
            this.comprima.Name = "comprima";
            this.comprima.ReadOnly = true;
            // 
            // compremio
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.compremio.DefaultCellStyle = dataGridViewCellStyle6;
            this.compremio.HeaderText = "Com. Premio";
            this.compremio.Name = "compremio";
            this.compremio.ReadOnly = true;
            // 
            // frmComisiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 557);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txttotalPrima);
            this.Controls.Add(this.txttotalPremio);
            this.Controls.Add(this.txtcomPrima);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtcomPremio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usuario_txt);
            this.Name = "frmComisiones";
            this.Text = "Comisiones";
            this.Load += new System.EventHandler(this.frmComisiones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox usuario_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtcomPremio;
        private System.Windows.Forms.TextBox claveliquidador_txt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox liquidadores_txt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txtcomPrima;
        private System.Windows.Forms.TextBox txttotalPrima;
        private System.Windows.Forms.TextBox txttotalPremio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn idcomision;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn prefijo;
        private System.Windows.Forms.DataGridViewTextBoxColumn propuesta;
        private System.Windows.Forms.DataGridViewTextBoxColumn referencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn tomador;
        private System.Windows.Forms.DataGridViewTextBoxColumn prima;
        private System.Windows.Forms.DataGridViewTextBoxColumn premio;
        private System.Windows.Forms.DataGridViewTextBoxColumn porcprima;
        private System.Windows.Forms.DataGridViewTextBoxColumn porpremio;
        private System.Windows.Forms.DataGridViewTextBoxColumn comprima;
        private System.Windows.Forms.DataGridViewTextBoxColumn compremio;
    }
}