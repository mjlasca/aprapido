namespace ProyectoBrokerDelPuerto
{
    partial class frmLibreDeuda
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerarCertificado = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.empresa_txt = new System.Windows.Forms.TextBox();
            this.departamento_txt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.responsable_txt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(28, 91);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(511, 184);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cuerpo certificado ";
            // 
            // btnGenerarCertificado
            // 
            this.btnGenerarCertificado.Location = new System.Drawing.Point(427, 26);
            this.btnGenerarCertificado.Name = "btnGenerarCertificado";
            this.btnGenerarCertificado.Size = new System.Drawing.Size(112, 23);
            this.btnGenerarCertificado.TabIndex = 2;
            this.btnGenerarCertificado.Text = "Generar Certificado";
            this.btnGenerarCertificado.UseVisualStyleBackColor = true;
            this.btnGenerarCertificado.Click += new System.EventHandler(this.btnGenerarCertificado_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Empresa";
            // 
            // empresa_txt
            // 
            this.empresa_txt.Location = new System.Drawing.Point(28, 304);
            this.empresa_txt.Name = "empresa_txt";
            this.empresa_txt.Size = new System.Drawing.Size(138, 20);
            this.empresa_txt.TabIndex = 5;
            // 
            // departamento_txt
            // 
            this.departamento_txt.Location = new System.Drawing.Point(172, 304);
            this.departamento_txt.Name = "departamento_txt";
            this.departamento_txt.Size = new System.Drawing.Size(138, 20);
            this.departamento_txt.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(169, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Departamento";
            // 
            // responsable_txt
            // 
            this.responsable_txt.Location = new System.Drawing.Point(316, 304);
            this.responsable_txt.Name = "responsable_txt";
            this.responsable_txt.Size = new System.Drawing.Size(138, 20);
            this.responsable_txt.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(313, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Responsable";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(28, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(315, 21);
            this.comboBox1.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Selecciine el cliente";
            // 
            // frmLibreDeuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 344);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.responsable_txt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.departamento_txt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.empresa_txt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGenerarCertificado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "frmLibreDeuda";
            this.Text = "Plantilla Libre Deuda";
            this.Load += new System.EventHandler(this.frmLibreDeuda_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerarCertificado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox empresa_txt;
        private System.Windows.Forms.TextBox departamento_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox responsable_txt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
    }
}