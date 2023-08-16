namespace ProyectoBrokerDelPuerto
{
    partial class frmNewCommission
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
            this.save_btn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.comisionpremio_txt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comisionprima_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.date1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.user_txt = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.reg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comprima = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.compremio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delete_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(12, 154);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(205, 24);
            this.save_btn.TabIndex = 35;
            this.save_btn.Text = "Guardar";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(110, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Comisión premio %";
            // 
            // comisionpremio_txt
            // 
            this.comisionpremio_txt.Location = new System.Drawing.Point(113, 81);
            this.comisionpremio_txt.Name = "comisionpremio_txt";
            this.comisionpremio_txt.Size = new System.Drawing.Size(104, 20);
            this.comisionpremio_txt.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Comisión prima %";
            // 
            // comisionprima_txt
            // 
            this.comisionprima_txt.Location = new System.Drawing.Point(12, 81);
            this.comisionprima_txt.Name = "comisionprima_txt";
            this.comisionprima_txt.Size = new System.Drawing.Size(95, 20);
            this.comisionprima_txt.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Usuario";
            // 
            // date1
            // 
            this.date1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.date1.Location = new System.Drawing.Point(12, 124);
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(205, 20);
            this.date1.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Fecha incio";
            // 
            // user_txt
            // 
            this.user_txt.Location = new System.Drawing.Point(12, 35);
            this.user_txt.Name = "user_txt";
            this.user_txt.ReadOnly = true;
            this.user_txt.Size = new System.Drawing.Size(205, 20);
            this.user_txt.TabIndex = 39;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.reg,
            this.date,
            this.comprima,
            this.compremio});
            this.dataGridView1.Location = new System.Drawing.Point(12, 184);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(205, 205);
            this.dataGridView1.TabIndex = 40;
            // 
            // reg
            // 
            this.reg.HeaderText = "reg";
            this.reg.Name = "reg";
            this.reg.ReadOnly = true;
            this.reg.Visible = false;
            // 
            // date
            // 
            this.date.FillWeight = 129.4416F;
            this.date.HeaderText = "FECHA";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // comprima
            // 
            this.comprima.FillWeight = 85.27919F;
            this.comprima.HeaderText = "COM PRIMA.";
            this.comprima.Name = "comprima";
            this.comprima.ReadOnly = true;
            // 
            // compremio
            // 
            this.compremio.FillWeight = 85.27919F;
            this.compremio.HeaderText = "COM. PREMIO";
            this.compremio.Name = "compremio";
            this.compremio.ReadOnly = true;
            // 
            // delete_btn
            // 
            this.delete_btn.Location = new System.Drawing.Point(12, 395);
            this.delete_btn.Name = "delete_btn";
            this.delete_btn.Size = new System.Drawing.Size(205, 24);
            this.delete_btn.TabIndex = 41;
            this.delete_btn.Text = "Eliminar";
            this.delete_btn.UseVisualStyleBackColor = true;
            this.delete_btn.Click += new System.EventHandler(this.delete_btn_Click);
            // 
            // frmNewCommission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 428);
            this.Controls.Add(this.delete_btn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.user_txt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.date1);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comisionpremio_txt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comisionprima_txt);
            this.Controls.Add(this.label2);
            this.Name = "frmNewCommission";
            this.Text = "Asignación Comisión";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewCommission_FormClosing);
            this.Load += new System.EventHandler(this.frmNewCommission_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox comisionpremio_txt;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox comisionprima_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker date1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox user_txt;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button delete_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn reg;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn comprima;
        private System.Windows.Forms.DataGridViewTextBoxColumn compremio;
    }
}