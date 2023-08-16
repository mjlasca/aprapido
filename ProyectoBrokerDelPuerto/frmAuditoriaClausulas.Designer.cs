namespace ProyectoBrokerDelPuerto
{
    partial class frmAuditoriaClausulas
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
            this.prefijo_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.idpropuesta_txt = new System.Windows.Forms.TextBox();
            this.empleado_txt = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ultmod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prefijo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idpropuesta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_edit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barrios_antes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barrios_despues = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ultmod,
            this.prefijo,
            this.idpropuesta,
            this.user_edit,
            this.barrios_antes,
            this.barrios_despues});
            this.dataGridView1.Location = new System.Drawing.Point(35, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(438, 311);
            this.dataGridView1.TabIndex = 0;
            // 
            // prefijo_txt
            // 
            this.prefijo_txt.Location = new System.Drawing.Point(35, 54);
            this.prefijo_txt.Name = "prefijo_txt";
            this.prefijo_txt.Size = new System.Drawing.Size(64, 20);
            this.prefijo_txt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Prefijo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "ID propuesta";
            // 
            // idpropuesta_txt
            // 
            this.idpropuesta_txt.Location = new System.Drawing.Point(111, 54);
            this.idpropuesta_txt.Name = "idpropuesta_txt";
            this.idpropuesta_txt.Size = new System.Drawing.Size(104, 20);
            this.idpropuesta_txt.TabIndex = 3;
            // 
            // empleado_txt
            // 
            this.empleado_txt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.empleado_txt.FormattingEnabled = true;
            this.empleado_txt.Location = new System.Drawing.Point(228, 53);
            this.empleado_txt.Name = "empleado_txt";
            this.empleado_txt.Size = new System.Drawing.Size(155, 21);
            this.empleado_txt.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(225, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Empleado";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(398, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "Consultar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ultmod
            // 
            this.ultmod.FillWeight = 50F;
            this.ultmod.HeaderText = "Fecha";
            this.ultmod.Name = "ultmod";
            this.ultmod.ReadOnly = true;
            // 
            // prefijo
            // 
            this.prefijo.FillWeight = 30F;
            this.prefijo.HeaderText = "Prefijo";
            this.prefijo.Name = "prefijo";
            this.prefijo.ReadOnly = true;
            // 
            // idpropuesta
            // 
            this.idpropuesta.FillWeight = 40F;
            this.idpropuesta.HeaderText = "ID Propuesta";
            this.idpropuesta.Name = "idpropuesta";
            this.idpropuesta.ReadOnly = true;
            // 
            // user_edit
            // 
            this.user_edit.HeaderText = "Empleado";
            this.user_edit.Name = "user_edit";
            this.user_edit.ReadOnly = true;
            // 
            // barrios_antes
            // 
            this.barrios_antes.HeaderText = "Barrios antes";
            this.barrios_antes.Name = "barrios_antes";
            this.barrios_antes.ReadOnly = true;
            this.barrios_antes.Visible = false;
            // 
            // barrios_despues
            // 
            this.barrios_despues.HeaderText = "Barrios después";
            this.barrios_despues.Name = "barrios_despues";
            this.barrios_despues.ReadOnly = true;
            this.barrios_despues.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(603, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 23);
            this.button2.TabIndex = 29;
            this.button2.Text = "Ver Comparación de barrios";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(491, 114);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(194, 277);
            this.listBox1.TabIndex = 30;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(691, 114);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(200, 277);
            this.listBox2.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(488, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Barrios antes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(688, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Barrios después";
            // 
            // frmAuditoriaClausulas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 418);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.empleado_txt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.idpropuesta_txt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prefijo_txt);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmAuditoriaClausulas";
            this.Text = "Registro de cláusulas modificadas";
            this.Load += new System.EventHandler(this.frmAuditoriaClausulas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox prefijo_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox idpropuesta_txt;
        private System.Windows.Forms.ComboBox empleado_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ultmod;
        private System.Windows.Forms.DataGridViewTextBoxColumn prefijo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idpropuesta;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_edit;
        private System.Windows.Forms.DataGridViewTextBoxColumn barrios_antes;
        private System.Windows.Forms.DataGridViewTextBoxColumn barrios_despues;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}