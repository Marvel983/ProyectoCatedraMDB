namespace ProyectoCatedraMDB
{
    partial class FormEditarUsuarios
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
            this.pnlBody = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.gb_Agregar_Producto = new System.Windows.Forms.GroupBox();
            this.lblIdUsuario = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbActivo = new System.Windows.Forms.ComboBox();
            this.cbCargo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtContra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.pnlBody.SuspendLayout();
            this.gb_Agregar_Producto.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.SkyBlue;
            this.pnlBody.Controls.Add(this.label7);
            this.pnlBody.Controls.Add(this.gb_Agregar_Producto);
            this.pnlBody.Controls.Add(this.label2);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(631, 456);
            this.pnlBody.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(240, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 32);
            this.label7.TabIndex = 23;
            this.label7.Text = "USUARIOS";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // gb_Agregar_Producto
            // 
            this.gb_Agregar_Producto.BackColor = System.Drawing.Color.Wheat;
            this.gb_Agregar_Producto.Controls.Add(this.btnEditar);
            this.gb_Agregar_Producto.Controls.Add(this.lblIdUsuario);
            this.gb_Agregar_Producto.Controls.Add(this.label4);
            this.gb_Agregar_Producto.Controls.Add(this.cbActivo);
            this.gb_Agregar_Producto.Controls.Add(this.cbCargo);
            this.gb_Agregar_Producto.Controls.Add(this.label8);
            this.gb_Agregar_Producto.Controls.Add(this.txtContra);
            this.gb_Agregar_Producto.Controls.Add(this.label1);
            this.gb_Agregar_Producto.Controls.Add(this.btnRegresar);
            this.gb_Agregar_Producto.Controls.Add(this.label6);
            this.gb_Agregar_Producto.Controls.Add(this.label3);
            this.gb_Agregar_Producto.Controls.Add(this.txtNombre);
            this.gb_Agregar_Producto.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_Agregar_Producto.Location = new System.Drawing.Point(33, 71);
            this.gb_Agregar_Producto.Name = "gb_Agregar_Producto";
            this.gb_Agregar_Producto.Size = new System.Drawing.Size(565, 348);
            this.gb_Agregar_Producto.TabIndex = 22;
            this.gb_Agregar_Producto.TabStop = false;
            this.gb_Agregar_Producto.Text = "Editar";
            // 
            // lblIdUsuario
            // 
            this.lblIdUsuario.AutoSize = true;
            this.lblIdUsuario.BackColor = System.Drawing.Color.White;
            this.lblIdUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIdUsuario.Location = new System.Drawing.Point(129, 47);
            this.lblIdUsuario.Name = "lblIdUsuario";
            this.lblIdUsuario.Size = new System.Drawing.Size(19, 27);
            this.lblIdUsuario.TabIndex = 35;
            this.lblIdUsuario.Text = ".";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 21);
            this.label4.TabIndex = 34;
            this.label4.Text = "ID:";
            // 
            // cbActivo
            // 
            this.cbActivo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cbActivo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbActivo.FormattingEnabled = true;
            this.cbActivo.Items.AddRange(new object[] {
            "Lacteos",
            "Carmes",
            "Verduras",
            "Frutas",
            "Cereales",
            "Desinfectantes",
            "Enlatatos"});
            this.cbActivo.Location = new System.Drawing.Point(129, 228);
            this.cbActivo.Name = "cbActivo";
            this.cbActivo.Size = new System.Drawing.Size(122, 29);
            this.cbActivo.TabIndex = 33;
            // 
            // cbCargo
            // 
            this.cbCargo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cbCargo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCargo.FormattingEnabled = true;
            this.cbCargo.Location = new System.Drawing.Point(129, 133);
            this.cbCargo.Name = "cbCargo";
            this.cbCargo.Size = new System.Drawing.Size(151, 29);
            this.cbCargo.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(19, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 21);
            this.label8.TabIndex = 31;
            this.label8.Text = "Cargo:";
            // 
            // txtContra
            // 
            this.txtContra.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContra.Location = new System.Drawing.Point(129, 175);
            this.txtContra.Name = "txtContra";
            this.txtContra.Size = new System.Drawing.Size(151, 29);
            this.txtContra.TabIndex = 28;
            this.txtContra.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 21);
            this.label1.TabIndex = 27;
            this.label1.Text = "Contraseña:";
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.Teal;
            this.btnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegresar.ForeColor = System.Drawing.Color.White;
            this.btnRegresar.Location = new System.Drawing.Point(457, 299);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(93, 32);
            this.btnRegresar.TabIndex = 26;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 228);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 21);
            this.label6.TabIndex = 24;
            this.label6.Text = "Activo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 21);
            this.label3.TabIndex = 18;
            this.label3.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(129, 86);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(151, 29);
            this.txtNombre.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 25);
            this.label2.TabIndex = 17;
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.Teal;
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(358, 299);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(93, 32);
            this.btnEditar.TabIndex = 36;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // FormEditarUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 456);
            this.Controls.Add(this.pnlBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEditarUsuarios";
            this.Text = "FormEditarUsuarios";
            this.Load += new System.EventHandler(this.FormEditarUsuarios_Load);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.gb_Agregar_Producto.ResumeLayout(false);
            this.gb_Agregar_Producto.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gb_Agregar_Producto;
        private System.Windows.Forms.ComboBox cbActivo;
        private System.Windows.Forms.ComboBox cbCargo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtContra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIdUsuario;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEditar;
    }
}