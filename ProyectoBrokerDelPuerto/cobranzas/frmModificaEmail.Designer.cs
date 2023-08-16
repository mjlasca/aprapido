namespace ProyectoBrokerDelPuerto.cobranzas
{
    partial class frmModificaEmail
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
            this.lblUsuarioModifica = new System.Windows.Forms.Label();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.guardar_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUsuarioModifica
            // 
            this.lblUsuarioModifica.AutoSize = true;
            this.lblUsuarioModifica.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuarioModifica.Location = new System.Drawing.Point(26, 37);
            this.lblUsuarioModifica.Name = "lblUsuarioModifica";
            this.lblUsuarioModifica.Size = new System.Drawing.Size(51, 20);
            this.lblUsuarioModifica.TabIndex = 0;
            this.lblUsuarioModifica.Text = "label1";
            // 
            // txtMensaje
            // 
            this.txtMensaje.Location = new System.Drawing.Point(30, 75);
            this.txtMensaje.Multiline = true;
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(738, 449);
            this.txtMensaje.TabIndex = 21;
            // 
            // guardar_btn
            // 
            this.guardar_btn.Location = new System.Drawing.Point(30, 531);
            this.guardar_btn.Name = "guardar_btn";
            this.guardar_btn.Size = new System.Drawing.Size(112, 23);
            this.guardar_btn.TabIndex = 22;
            this.guardar_btn.Text = "Guardar";
            this.guardar_btn.UseVisualStyleBackColor = true;
            this.guardar_btn.Click += new System.EventHandler(this.guardar_btn_Click);
            // 
            // frmModificaEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 564);
            this.Controls.Add(this.guardar_btn);
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.lblUsuarioModifica);
            this.Name = "frmModificaEmail";
            this.Text = "Modificar Email";
            this.Load += new System.EventHandler(this.fmModificaEmail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsuarioModifica;
        private System.Windows.Forms.TextBox txtMensaje;
        private System.Windows.Forms.Button guardar_btn;
    }
}