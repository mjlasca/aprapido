namespace ProyectoBrokerDelPuerto
{
    partial class frmRutas
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRutaCertificados = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRutaRecibos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRutaInformes = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ruta certificados";
            // 
            // txtRutaCertificados
            // 
            this.txtRutaCertificados.Location = new System.Drawing.Point(26, 36);
            this.txtRutaCertificados.Name = "txtRutaCertificados";
            this.txtRutaCertificados.Size = new System.Drawing.Size(355, 20);
            this.txtRutaCertificados.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ruta recibos";
            // 
            // txtRutaRecibos
            // 
            this.txtRutaRecibos.Location = new System.Drawing.Point(26, 78);
            this.txtRutaRecibos.Name = "txtRutaRecibos";
            this.txtRutaRecibos.Size = new System.Drawing.Size(355, 20);
            this.txtRutaRecibos.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ruta informes";
            // 
            // txtRutaInformes
            // 
            this.txtRutaInformes.Location = new System.Drawing.Point(26, 122);
            this.txtRutaInformes.Name = "txtRutaInformes";
            this.txtRutaInformes.Size = new System.Drawing.Size(355, 20);
            this.txtRutaInformes.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(355, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmRutas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 196);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtRutaInformes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRutaRecibos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRutaCertificados);
            this.Controls.Add(this.label1);
            this.Name = "frmRutas";
            this.Text = "Rutas archivos sistema";
            this.Load += new System.EventHandler(this.frmRutas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRutaCertificados;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRutaRecibos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRutaInformes;
        private System.Windows.Forms.Button button1;
    }
}