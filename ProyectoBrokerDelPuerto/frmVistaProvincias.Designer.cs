namespace ProyectoBrokerDelPuerto
{
    partial class frmVistaProvincias
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
            this.busqueda_txt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.reg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codpostal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.provincia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ciudad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.agregar_btn = new System.Windows.Forms.Button();
            this.seleccionar_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ciudad_txt = new System.Windows.Forms.TextBox();
            this.provincia_txt = new System.Windows.Forms.TextBox();
            this.codpostal_txt = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.eliminar_btn = new System.Windows.Forms.Button();
            this.modificar_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // busqueda_txt
            // 
            this.busqueda_txt.Location = new System.Drawing.Point(23, 32);
            this.busqueda_txt.Name = "busqueda_txt";
            this.busqueda_txt.Size = new System.Drawing.Size(324, 20);
            this.busqueda_txt.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(353, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 1;
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
            this.reg,
            this.codpostal,
            this.provincia,
            this.ciudad});
            this.dataGridView1.Location = new System.Drawing.Point(23, 67);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(444, 261);
            this.dataGridView1.TabIndex = 2;
            // 
            // reg
            // 
            this.reg.HeaderText = "reg";
            this.reg.Name = "reg";
            this.reg.ReadOnly = true;
            this.reg.Visible = false;
            // 
            // codpostal
            // 
            this.codpostal.FillWeight = 30F;
            this.codpostal.HeaderText = "Cód. Postal";
            this.codpostal.Name = "codpostal";
            this.codpostal.ReadOnly = true;
            // 
            // provincia
            // 
            this.provincia.HeaderText = "Localidad";
            this.provincia.Name = "provincia";
            this.provincia.ReadOnly = true;
            // 
            // ciudad
            // 
            this.ciudad.HeaderText = "Ciudad";
            this.ciudad.Name = "ciudad";
            this.ciudad.ReadOnly = true;
            // 
            // agregar_btn
            // 
            this.agregar_btn.Location = new System.Drawing.Point(23, 328);
            this.agregar_btn.Name = "agregar_btn";
            this.agregar_btn.Size = new System.Drawing.Size(75, 23);
            this.agregar_btn.TabIndex = 3;
            this.agregar_btn.Text = "Agregar";
            this.agregar_btn.UseVisualStyleBackColor = true;
            this.agregar_btn.Click += new System.EventHandler(this.agregar_btn_Click);
            // 
            // seleccionar_btn
            // 
            this.seleccionar_btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.seleccionar_btn.Location = new System.Drawing.Point(392, 328);
            this.seleccionar_btn.Name = "seleccionar_btn";
            this.seleccionar_btn.Size = new System.Drawing.Size(75, 23);
            this.seleccionar_btn.TabIndex = 3;
            this.seleccionar_btn.Text = "Seleccionar";
            this.seleccionar_btn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Coincidencia aproximada codpostal/provincia/ciudad";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ciudad_txt);
            this.panel1.Controls.Add(this.provincia_txt);
            this.panel1.Controls.Add(this.codpostal_txt);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(23, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 339);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Ciudad";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Localidad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Código postal";
            // 
            // ciudad_txt
            // 
            this.ciudad_txt.Location = new System.Drawing.Point(53, 212);
            this.ciudad_txt.Name = "ciudad_txt";
            this.ciudad_txt.Size = new System.Drawing.Size(228, 20);
            this.ciudad_txt.TabIndex = 5;
            // 
            // provincia_txt
            // 
            this.provincia_txt.Location = new System.Drawing.Point(53, 160);
            this.provincia_txt.Name = "provincia_txt";
            this.provincia_txt.Size = new System.Drawing.Size(228, 20);
            this.provincia_txt.TabIndex = 3;
            // 
            // codpostal_txt
            // 
            this.codpostal_txt.Location = new System.Drawing.Point(53, 105);
            this.codpostal_txt.Name = "codpostal_txt";
            this.codpostal_txt.Size = new System.Drawing.Size(228, 20);
            this.codpostal_txt.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(319, 138);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Guardar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(319, 180);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // eliminar_btn
            // 
            this.eliminar_btn.Location = new System.Drawing.Point(181, 328);
            this.eliminar_btn.Name = "eliminar_btn";
            this.eliminar_btn.Size = new System.Drawing.Size(75, 23);
            this.eliminar_btn.TabIndex = 6;
            this.eliminar_btn.Text = "Eliminar";
            this.eliminar_btn.UseVisualStyleBackColor = true;
            this.eliminar_btn.Click += new System.EventHandler(this.eliminar_btn_Click);
            // 
            // modificar_btn
            // 
            this.modificar_btn.Location = new System.Drawing.Point(100, 328);
            this.modificar_btn.Name = "modificar_btn";
            this.modificar_btn.Size = new System.Drawing.Size(75, 23);
            this.modificar_btn.TabIndex = 6;
            this.modificar_btn.Text = "Modificar";
            this.modificar_btn.UseVisualStyleBackColor = true;
            this.modificar_btn.Click += new System.EventHandler(this.modificar_btn_Click);
            // 
            // frmVistaProvincias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 363);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eliminar_btn);
            this.Controls.Add(this.modificar_btn);
            this.Controls.Add(this.seleccionar_btn);
            this.Controls.Add(this.agregar_btn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.busqueda_txt);
            this.Name = "frmVistaProvincias";
            this.Text = "Vista Codpostal/provincia/ciudad";
            this.Load += new System.EventHandler(this.frmVistaProvincias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox busqueda_txt;
        private System.Windows.Forms.Button agregar_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox ciudad_txt;
        private System.Windows.Forms.TextBox provincia_txt;
        private System.Windows.Forms.TextBox codpostal_txt;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn reg;
        private System.Windows.Forms.DataGridViewTextBoxColumn codpostal;
        private System.Windows.Forms.DataGridViewTextBoxColumn provincia;
        private System.Windows.Forms.DataGridViewTextBoxColumn ciudad;
        private System.Windows.Forms.Button eliminar_btn;
        private System.Windows.Forms.Button modificar_btn;
        public System.Windows.Forms.Button seleccionar_btn;
    }
}