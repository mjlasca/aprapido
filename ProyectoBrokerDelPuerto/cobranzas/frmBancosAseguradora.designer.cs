namespace ProyectoBrokerDelPuerto.cobranzas
{
    partial class frmBancosAseguradora
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.reg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idaseguradora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cuit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mediopago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombrebanco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titularcuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numcuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sucursal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.reg_txt = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.cbMedioPago = new System.Windows.Forms.ComboBox();
            this.txtTitularCuenta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCbu = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtSucursal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNumCuenta = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCuit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtidaseg = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.modificar_btn = new System.Windows.Forms.Button();
            this.cbNombreBanco = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.reg,
            this.idaseguradora,
            this.cuit,
            this.mediopago,
            this.nombrebanco,
            this.titularcuenta,
            this.numcuenta,
            this.cbu,
            this.sucursal,
            this.direccion,
            this.telefono});
            this.dataGridView1.Location = new System.Drawing.Point(22, 132);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(824, 358);
            this.dataGridView1.TabIndex = 20;
            // 
            // reg
            // 
            this.reg.HeaderText = "reg";
            this.reg.Name = "reg";
            this.reg.ReadOnly = true;
            this.reg.Visible = false;
            // 
            // idaseguradora
            // 
            this.idaseguradora.HeaderText = "idaseguradora";
            this.idaseguradora.Name = "idaseguradora";
            this.idaseguradora.ReadOnly = true;
            this.idaseguradora.Visible = false;
            // 
            // cuit
            // 
            this.cuit.FillWeight = 50F;
            this.cuit.HeaderText = "CUIT";
            this.cuit.Name = "cuit";
            this.cuit.ReadOnly = true;
            // 
            // mediopago
            // 
            this.mediopago.HeaderText = "Medio Pago";
            this.mediopago.Name = "mediopago";
            this.mediopago.ReadOnly = true;
            this.mediopago.Visible = false;
            // 
            // nombrebanco
            // 
            this.nombrebanco.FillWeight = 150F;
            this.nombrebanco.HeaderText = "Nombre Banco";
            this.nombrebanco.Name = "nombrebanco";
            this.nombrebanco.ReadOnly = true;
            // 
            // titularcuenta
            // 
            this.titularcuenta.HeaderText = "Titular Cuenta";
            this.titularcuenta.Name = "titularcuenta";
            this.titularcuenta.ReadOnly = true;
            // 
            // numcuenta
            // 
            this.numcuenta.HeaderText = "#Cuenta";
            this.numcuenta.Name = "numcuenta";
            this.numcuenta.ReadOnly = true;
            // 
            // cbu
            // 
            this.cbu.HeaderText = "CBU";
            this.cbu.Name = "cbu";
            this.cbu.ReadOnly = true;
            // 
            // sucursal
            // 
            this.sucursal.HeaderText = "Sucursal";
            this.sucursal.Name = "sucursal";
            this.sucursal.ReadOnly = true;
            // 
            // direccion
            // 
            this.direccion.HeaderText = "Dirección";
            this.direccion.Name = "direccion";
            this.direccion.ReadOnly = true;
            // 
            // telefono
            // 
            this.telefono.HeaderText = "Teléfono";
            this.telefono.Name = "telefono";
            this.telefono.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbNombreBanco);
            this.groupBox1.Controls.Add(this.reg_txt);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnGuardar);
            this.groupBox1.Controls.Add(this.cbMedioPago);
            this.groupBox1.Controls.Add(this.txtTitularCuenta);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCbu);
            this.groupBox1.Controls.Add(this.txtTelefono);
            this.groupBox1.Controls.Add(this.txtDireccion);
            this.groupBox1.Controls.Add(this.txtSucursal);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtNumCuenta);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtCuit);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(824, 114);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Medio de Pago";
            // 
            // reg_txt
            // 
            this.reg_txt.Location = new System.Drawing.Point(680, 11);
            this.reg_txt.Name = "reg_txt";
            this.reg_txt.Size = new System.Drawing.Size(138, 20);
            this.reg_txt.TabIndex = 20;
            this.reg_txt.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(18, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "(*)";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(769, 37);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(37, 61);
            this.btnGuardar.TabIndex = 18;
            this.btnGuardar.Text = "+";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cbMedioPago
            // 
            this.cbMedioPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMedioPago.FormattingEnabled = true;
            this.cbMedioPago.Items.AddRange(new object[] {
            "",
            "Depósito o Transferencia en pesos",
            "Transferencia en dólares",
            "Cobro en efectivo",
            "Emitir Cheque"});
            this.cbMedioPago.Location = new System.Drawing.Point(21, 77);
            this.cbMedioPago.Name = "cbMedioPago";
            this.cbMedioPago.Size = new System.Drawing.Size(142, 21);
            this.cbMedioPago.TabIndex = 8;
            // 
            // txtTitularCuenta
            // 
            this.txtTitularCuenta.Location = new System.Drawing.Point(399, 37);
            this.txtTitularCuenta.Name = "txtTitularCuenta";
            this.txtTitularCuenta.Size = new System.Drawing.Size(228, 20);
            this.txtTitularCuenta.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(396, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Titular Cuenta";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(166, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "(*)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nombre Entidad / Detalle";
            // 
            // txtCbu
            // 
            this.txtCbu.Location = new System.Drawing.Point(635, 37);
            this.txtCbu.Name = "txtCbu";
            this.txtCbu.Size = new System.Drawing.Size(116, 20);
            this.txtCbu.TabIndex = 6;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(613, 77);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(138, 20);
            this.txtTelefono.TabIndex = 16;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(465, 78);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(142, 20);
            this.txtDireccion.TabIndex = 14;
            // 
            // txtSucursal
            // 
            this.txtSucursal.Location = new System.Drawing.Point(317, 77);
            this.txtSucursal.Name = "txtSucursal";
            this.txtSucursal.Size = new System.Drawing.Size(142, 20);
            this.txtSucursal.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(613, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Teléfono";
            // 
            // txtNumCuenta
            // 
            this.txtNumCuenta.Location = new System.Drawing.Point(169, 77);
            this.txtNumCuenta.Name = "txtNumCuenta";
            this.txtNumCuenta.Size = new System.Drawing.Size(142, 20);
            this.txtNumCuenta.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(465, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Dirección";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(632, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "CBU";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(318, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Sucursal";
            // 
            // txtCuit
            // 
            this.txtCuit.Location = new System.Drawing.Point(21, 37);
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(142, 20);
            this.txtCuit.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(167, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "No. Cuenta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Medio de Pago";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CUIT";
            // 
            // txtidaseg
            // 
            this.txtidaseg.Location = new System.Drawing.Point(22, 470);
            this.txtidaseg.Name = "txtidaseg";
            this.txtidaseg.Size = new System.Drawing.Size(228, 20);
            this.txtidaseg.TabIndex = 5;
            this.txtidaseg.Visible = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(22, 493);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(81, 22);
            this.btnEliminar.TabIndex = 21;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // modificar_btn
            // 
            this.modificar_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modificar_btn.Location = new System.Drawing.Point(109, 493);
            this.modificar_btn.Name = "modificar_btn";
            this.modificar_btn.Size = new System.Drawing.Size(81, 22);
            this.modificar_btn.TabIndex = 22;
            this.modificar_btn.Text = "Modificar";
            this.modificar_btn.UseVisualStyleBackColor = true;
            this.modificar_btn.Click += new System.EventHandler(this.modificar_btn_Click);
            // 
            // cbNombreBanco
            // 
            this.cbNombreBanco.Location = new System.Drawing.Point(169, 37);
            this.cbNombreBanco.Name = "cbNombreBanco";
            this.cbNombreBanco.Size = new System.Drawing.Size(224, 20);
            this.cbNombreBanco.TabIndex = 21;
            // 
            // frmBancosAseguradora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 530);
            this.Controls.Add(this.modificar_btn);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.txtidaseg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmBancosAseguradora";
            this.Text = "Medios de Pago";
            this.Load += new System.EventHandler(this.frmBancosAseguradora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitularCuenta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMedioPago;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCbu;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtSucursal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNumCuenta;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGuardar;
        public System.Windows.Forms.TextBox txtidaseg;
        private System.Windows.Forms.DataGridViewTextBoxColumn reg;
        private System.Windows.Forms.DataGridViewTextBoxColumn idaseguradora;
        private System.Windows.Forms.DataGridViewTextBoxColumn cuit;
        private System.Windows.Forms.DataGridViewTextBoxColumn mediopago;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombrebanco;
        private System.Windows.Forms.DataGridViewTextBoxColumn titularcuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn numcuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbu;
        private System.Windows.Forms.DataGridViewTextBoxColumn sucursal;
        private System.Windows.Forms.DataGridViewTextBoxColumn direccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn telefono;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button modificar_btn;
        private System.Windows.Forms.TextBox reg_txt;
        private System.Windows.Forms.TextBox cbNombreBanco;
    }
}