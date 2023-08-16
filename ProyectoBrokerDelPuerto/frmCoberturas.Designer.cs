namespace ProyectoBrokerDelPuerto
{
    partial class frmCoberturas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.reg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gastos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deducible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vrMensual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vrTrimestral = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vrSemestral = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x64 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Ingrese:  Nombre Cobertura";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(31, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(298, 20);
            this.textBox1.TabIndex = 24;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(273, 413);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(115, 23);
            this.button4.TabIndex = 20;
            this.button4.Text = "Eliminar Cobertura";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(152, 413);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "Ver Cobertura";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(31, 413);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "Nueva Cobertura";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(335, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 23;
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
            this.nombre,
            this.suma,
            this.gastos,
            this.deducible,
            this.vrMensual,
            this.vrTrimestral,
            this.vrSemestral,
            this.x21,
            this.x32,
            this.x64});
            this.dataGridView1.Location = new System.Drawing.Point(31, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(818, 344);
            this.dataGridView1.TabIndex = 19;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // reg
            // 
            this.reg.HeaderText = "reg";
            this.reg.Name = "reg";
            this.reg.ReadOnly = true;
            this.reg.Visible = false;
            // 
            // nombre
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.nombre.DefaultCellStyle = dataGridViewCellStyle1;
            this.nombre.HeaderText = "Nombre Cobertura";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // suma
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.suma.DefaultCellStyle = dataGridViewCellStyle2;
            this.suma.FillWeight = 120F;
            this.suma.HeaderText = "Suma Asegurada por Muerte e Invalidez";
            this.suma.Name = "suma";
            this.suma.ReadOnly = true;
            // 
            // gastos
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.gastos.DefaultCellStyle = dataGridViewCellStyle3;
            this.gastos.HeaderText = "Gastos Médicos";
            this.gastos.Name = "gastos";
            this.gastos.ReadOnly = true;
            // 
            // deducible
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.deducible.DefaultCellStyle = dataGridViewCellStyle4;
            this.deducible.HeaderText = "Deducible";
            this.deducible.Name = "deducible";
            this.deducible.ReadOnly = true;
            // 
            // vrMensual
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.vrMensual.DefaultCellStyle = dataGridViewCellStyle5;
            this.vrMensual.HeaderText = "Vr. Mensual";
            this.vrMensual.Name = "vrMensual";
            this.vrMensual.ReadOnly = true;
            // 
            // vrTrimestral
            // 
            this.vrTrimestral.HeaderText = "Vr.Trimestral";
            this.vrTrimestral.Name = "vrTrimestral";
            this.vrTrimestral.ReadOnly = true;
            // 
            // vrSemestral
            // 
            this.vrSemestral.HeaderText = "Vr.Semestral";
            this.vrSemestral.Name = "vrSemestral";
            this.vrSemestral.ReadOnly = true;
            // 
            // x21
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.x21.DefaultCellStyle = dataGridViewCellStyle6;
            this.x21.HeaderText = "2x1";
            this.x21.Name = "x21";
            this.x21.ReadOnly = true;
            // 
            // x32
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.x32.DefaultCellStyle = dataGridViewCellStyle7;
            this.x32.HeaderText = "3x2";
            this.x32.Name = "x32";
            this.x32.ReadOnly = true;
            // 
            // x64
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.x64.DefaultCellStyle = dataGridViewCellStyle8;
            this.x64.HeaderText = "6x4";
            this.x64.Name = "x64";
            this.x64.ReadOnly = true;
            // 
            // frmCoberturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 456);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmCoberturas";
            this.Text = "Coberturas";
            this.Load += new System.EventHandler(this.frmCoberturas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn reg;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn suma;
        private System.Windows.Forms.DataGridViewTextBoxColumn gastos;
        private System.Windows.Forms.DataGridViewTextBoxColumn deducible;
        private System.Windows.Forms.DataGridViewTextBoxColumn vrMensual;
        private System.Windows.Forms.DataGridViewTextBoxColumn vrTrimestral;
        private System.Windows.Forms.DataGridViewTextBoxColumn vrSemestral;
        private System.Windows.Forms.DataGridViewTextBoxColumn x21;
        private System.Windows.Forms.DataGridViewTextBoxColumn x32;
        private System.Windows.Forms.DataGridViewTextBoxColumn x64;
    }
}