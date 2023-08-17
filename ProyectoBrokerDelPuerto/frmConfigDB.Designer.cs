namespace ProyectoBrokerDelPuerto
{
    partial class frmConfigDB
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
            this.ip_txt = new System.Windows.Forms.TextBox();
            this.dbname_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.userDb_txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.passDb_txt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "*IP Servidor ";
            // 
            // ip_txt
            // 
            this.ip_txt.Location = new System.Drawing.Point(25, 35);
            this.ip_txt.Name = "ip_txt";
            this.ip_txt.Size = new System.Drawing.Size(175, 20);
            this.ip_txt.TabIndex = 1;
            // 
            // dbname_txt
            // 
            this.dbname_txt.Location = new System.Drawing.Point(25, 79);
            this.dbname_txt.Name = "dbname_txt";
            this.dbname_txt.Size = new System.Drawing.Size(175, 20);
            this.dbname_txt.TabIndex = 3;
            this.dbname_txt.Text = "barriosprivados";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "*Nombre base de datos";
            // 
            // userDb_txt
            // 
            this.userDb_txt.Location = new System.Drawing.Point(25, 123);
            this.userDb_txt.Name = "userDb_txt";
            this.userDb_txt.Size = new System.Drawing.Size(175, 20);
            this.userDb_txt.TabIndex = 5;
            this.userDb_txt.Text = "remoto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "*Usuario base de datos";
            // 
            // passDb_txt
            // 
            this.passDb_txt.Location = new System.Drawing.Point(25, 167);
            this.passDb_txt.Name = "passDb_txt";
            this.passDb_txt.PasswordChar = '*';
            this.passDb_txt.Size = new System.Drawing.Size(175, 20);
            this.passDb_txt.TabIndex = 7;
            this.passDb_txt.Text = "remoto";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Contraseña usuario";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 201);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmConfigDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 243);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.passDb_txt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.userDb_txt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dbname_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ip_txt);
            this.Controls.Add(this.label1);
            this.Name = "frmConfigDB";
            this.Text = "Configuración APP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ip_txt;
        private System.Windows.Forms.TextBox dbname_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox userDb_txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox passDb_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}