namespace ProyectoBrokerDelPuerto
{
    partial class frmNuevoPuntodeVenta
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
            this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.apitoken = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prefijo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activo_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codestado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codempresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtRol = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtApitoken = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrefijo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtResponsable = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboPerfil = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCodEmpresa = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCodProductor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelMaster = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.detalle1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datos1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOmitir = new System.Windows.Forms.Button();
            this.btnEnviarMaster = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panelMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.responsable,
            this.email,
            this.apitoken,
            this.prefijo,
            this.rol,
            this.activo_,
            this.usuario,
            this.codestado,
            this.codempresa});
            this.dataGridView1.Location = new System.Drawing.Point(15, 158);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(629, 382);
            this.dataGridView1.TabIndex = 22;
            // 
            // responsable
            // 
            this.responsable.FillWeight = 150F;
            this.responsable.HeaderText = "Responsable";
            this.responsable.Name = "responsable";
            this.responsable.ReadOnly = true;
            // 
            // email
            // 
            this.email.HeaderText = "Email";
            this.email.Name = "email";
            this.email.ReadOnly = true;
            // 
            // apitoken
            // 
            this.apitoken.FillWeight = 150F;
            this.apitoken.HeaderText = "Token";
            this.apitoken.Name = "apitoken";
            this.apitoken.ReadOnly = true;
            // 
            // prefijo
            // 
            this.prefijo.HeaderText = "Prefijo";
            this.prefijo.Name = "prefijo";
            this.prefijo.ReadOnly = true;
            // 
            // rol
            // 
            this.rol.HeaderText = "Rol";
            this.rol.Name = "rol";
            this.rol.ReadOnly = true;
            // 
            // activo_
            // 
            this.activo_.HeaderText = "Activo/Inactivo";
            this.activo_.Name = "activo_";
            this.activo_.ReadOnly = true;
            // 
            // usuario
            // 
            this.usuario.HeaderText = "usuario";
            this.usuario.Name = "usuario";
            this.usuario.ReadOnly = true;
            this.usuario.Visible = false;
            // 
            // codestado
            // 
            this.codestado.HeaderText = "codestado";
            this.codestado.Name = "codestado";
            this.codestado.ReadOnly = true;
            this.codestado.Visible = false;
            // 
            // codempresa
            // 
            this.codempresa.HeaderText = "Cód. Empresa";
            this.codempresa.Name = "codempresa";
            this.codempresa.ReadOnly = true;
            // 
            // txtRol
            // 
            this.txtRol.Location = new System.Drawing.Point(179, 559);
            this.txtRol.Name = "txtRol";
            this.txtRol.ReadOnly = true;
            this.txtRol.Size = new System.Drawing.Size(100, 20);
            this.txtRol.TabIndex = 20;
            this.txtRol.Text = "COLABORADOR";
            this.txtRol.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 543);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Rol";
            this.label3.Visible = false;
            // 
            // txtApitoken
            // 
            this.txtApitoken.Location = new System.Drawing.Point(328, 559);
            this.txtApitoken.Name = "txtApitoken";
            this.txtApitoken.ReadOnly = true;
            this.txtApitoken.Size = new System.Drawing.Size(341, 20);
            this.txtApitoken.TabIndex = 16;
            this.txtApitoken.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(325, 543);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Api Token";
            this.label2.Visible = false;
            // 
            // txtPrefijo
            // 
            this.txtPrefijo.Location = new System.Drawing.Point(248, 47);
            this.txtPrefijo.Name = "txtPrefijo";
            this.txtPrefijo.Size = new System.Drawing.Size(121, 20);
            this.txtPrefijo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Prefijo";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(504, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 546);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Habilita / Inhabilitar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtResponsable
            // 
            this.txtResponsable.Location = new System.Drawing.Point(19, 47);
            this.txtResponsable.Name = "txtResponsable";
            this.txtResponsable.Size = new System.Drawing.Size(223, 20);
            this.txtResponsable.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Responsable del punto";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(19, 84);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(222, 20);
            this.txtEmail.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Email";
            // 
            // comboPerfil
            // 
            this.comboPerfil.FormattingEnabled = true;
            this.comboPerfil.Location = new System.Drawing.Point(248, 83);
            this.comboPerfil.Name = "comboPerfil";
            this.comboPerfil.Size = new System.Drawing.Size(121, 21);
            this.comboPerfil.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(245, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Perfil Usuario";
            // 
            // txtCodEmpresa
            // 
            this.txtCodEmpresa.Location = new System.Drawing.Point(386, 47);
            this.txtCodEmpresa.Name = "txtCodEmpresa";
            this.txtCodEmpresa.Size = new System.Drawing.Size(100, 20);
            this.txtCodEmpresa.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(383, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Cód. Empresa";
            // 
            // txtCodProductor
            // 
            this.txtCodProductor.Location = new System.Drawing.Point(386, 84);
            this.txtCodProductor.Name = "txtCodProductor";
            this.txtCodProductor.Size = new System.Drawing.Size(100, 20);
            this.txtCodProductor.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(383, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Cód. Productor";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCodProductor);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCodEmpresa);
            this.groupBox1.Controls.Add(this.txtPrefijo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtResponsable);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboPerfil);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Location = new System.Drawing.Point(16, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(628, 129);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Nuevo Punto";
            // 
            // panelMaster
            // 
            this.panelMaster.Controls.Add(this.button3);
            this.panelMaster.Controls.Add(this.dataGridView2);
            this.panelMaster.Controls.Add(this.btnOmitir);
            this.panelMaster.Controls.Add(this.btnEnviarMaster);
            this.panelMaster.Controls.Add(this.label12);
            this.panelMaster.Location = new System.Drawing.Point(183, 217);
            this.panelMaster.Name = "panelMaster";
            this.panelMaster.Size = new System.Drawing.Size(294, 230);
            this.panelMaster.TabIndex = 30;
            this.panelMaster.Visible = false;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ColumnHeadersVisible = false;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.detalle1,
            this.datos1});
            this.dataGridView2.Location = new System.Drawing.Point(31, 41);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(240, 140);
            this.dataGridView2.TabIndex = 4;
            // 
            // detalle1
            // 
            this.detalle1.HeaderText = "detalle1";
            this.detalle1.Name = "detalle1";
            this.detalle1.ReadOnly = true;
            // 
            // datos1
            // 
            this.datos1.HeaderText = "datos1";
            this.datos1.Name = "datos1";
            // 
            // btnOmitir
            // 
            this.btnOmitir.Location = new System.Drawing.Point(113, 187);
            this.btnOmitir.Name = "btnOmitir";
            this.btnOmitir.Size = new System.Drawing.Size(75, 23);
            this.btnOmitir.TabIndex = 3;
            this.btnOmitir.Text = "Omitir";
            this.btnOmitir.UseVisualStyleBackColor = true;
            this.btnOmitir.Click += new System.EventHandler(this.btnOmitir_Click);
            // 
            // btnEnviarMaster
            // 
            this.btnEnviarMaster.Location = new System.Drawing.Point(31, 187);
            this.btnEnviarMaster.Name = "btnEnviarMaster";
            this.btnEnviarMaster.Size = new System.Drawing.Size(75, 23);
            this.btnEnviarMaster.TabIndex = 2;
            this.btnEnviarMaster.Text = "Enviar";
            this.btnEnviarMaster.UseVisualStyleBackColor = true;
            this.btnEnviarMaster.Click += new System.EventHandler(this.btnEnviarMaster_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(28, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Cofiguración Master";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(194, 187);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Cancelar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // frmNuevoPuntodeVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 581);
            this.Controls.Add(this.panelMaster);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtRol);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtApitoken);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmNuevoPuntodeVenta";
            this.Text = "Nuevo punto de venta";
            this.Load += new System.EventHandler(this.frmNuevoPuntodeVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelMaster.ResumeLayout(false);
            this.panelMaster.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtRol;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtApitoken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrefijo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtResponsable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboPerfil;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCodEmpresa;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn responsable;
        private System.Windows.Forms.DataGridViewTextBoxColumn email;
        private System.Windows.Forms.DataGridViewTextBoxColumn apitoken;
        private System.Windows.Forms.DataGridViewTextBoxColumn prefijo;
        private System.Windows.Forms.DataGridViewTextBoxColumn rol;
        private System.Windows.Forms.DataGridViewTextBoxColumn activo_;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn codestado;
        private System.Windows.Forms.DataGridViewTextBoxColumn codempresa;
        private System.Windows.Forms.TextBox txtCodProductor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelMaster;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnOmitir;
        private System.Windows.Forms.Button btnEnviarMaster;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn detalle1;
        private System.Windows.Forms.DataGridViewTextBoxColumn datos1;
        private System.Windows.Forms.Button button3;
    }
}