namespace ProyectoBrokerDelPuerto.cobranzas
{
    partial class frmPerfilesArchivosCobranzas
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
            this.encabezado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAseguradora = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFilaComienzo = new System.Windows.Forms.TextBox();
            this.nuevo_perfil_btn = new System.Windows.Forms.Button();
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
            this.encabezado,
            this.columna});
            this.dataGridView1.Location = new System.Drawing.Point(13, 91);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(305, 467);
            this.dataGridView1.TabIndex = 0;
            // 
            // encabezado
            // 
            this.encabezado.HeaderText = "Encabezado";
            this.encabezado.Name = "encabezado";
            this.encabezado.ReadOnly = true;
            // 
            // columna
            // 
            this.columna.HeaderText = "Columna";
            this.columna.Name = "columna";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Aseguradora";
            // 
            // cbAseguradora
            // 
            this.cbAseguradora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAseguradora.FormattingEnabled = true;
            this.cbAseguradora.Location = new System.Drawing.Point(16, 40);
            this.cbAseguradora.Name = "cbAseguradora";
            this.cbAseguradora.Size = new System.Drawing.Size(302, 21);
            this.cbAseguradora.TabIndex = 2;
            this.cbAseguradora.SelectedIndexChanged += new System.EventHandler(this.cbAseguradora_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 562);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Fila comienzo de datos";
            // 
            // txtFilaComienzo
            // 
            this.txtFilaComienzo.Location = new System.Drawing.Point(134, 65);
            this.txtFilaComienzo.Name = "txtFilaComienzo";
            this.txtFilaComienzo.Size = new System.Drawing.Size(79, 20);
            this.txtFilaComienzo.TabIndex = 5;
            // 
            // nuevo_perfil_btn
            // 
            this.nuevo_perfil_btn.Location = new System.Drawing.Point(13, 562);
            this.nuevo_perfil_btn.Name = "nuevo_perfil_btn";
            this.nuevo_perfil_btn.Size = new System.Drawing.Size(114, 23);
            this.nuevo_perfil_btn.TabIndex = 6;
            this.nuevo_perfil_btn.Text = "Nuevo Perfil";
            this.nuevo_perfil_btn.UseVisualStyleBackColor = true;
            this.nuevo_perfil_btn.Visible = false;
            this.nuevo_perfil_btn.Click += new System.EventHandler(this.nuevo_perfil_btn_Click);
            // 
            // frmPerfilesArchivosCobranzas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 597);
            this.Controls.Add(this.nuevo_perfil_btn);
            this.Controls.Add(this.txtFilaComienzo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbAseguradora);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmPerfilesArchivosCobranzas";
            this.Text = "Perfil de cobranzas";
            this.Load += new System.EventHandler(this.frmPerfilesArchivosCobranzas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAseguradora;
        private System.Windows.Forms.DataGridViewTextBoxColumn encabezado;
        private System.Windows.Forms.DataGridViewTextBoxColumn columna;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFilaComienzo;
        private System.Windows.Forms.Button nuevo_perfil_btn;
    }
}