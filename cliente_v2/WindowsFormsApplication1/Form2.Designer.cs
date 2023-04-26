
namespace WindowsFormsApplication1
{
    partial class Form2
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
            this.name = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.Label();
            this.Registrate = new System.Windows.Forms.Button();
            this.contrasena = new System.Windows.Forms.TextBox();
            this.nombre = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(142, 66);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(78, 21);
            this.name.TabIndex = 0;
            this.name.Text = "Nombre:";
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Location = new System.Drawing.Point(142, 117);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(106, 21);
            this.password.TabIndex = 1;
            this.password.Text = "Contraseña:";
            // 
            // Registrate
            // 
            this.Registrate.Location = new System.Drawing.Point(265, 195);
            this.Registrate.Name = "Registrate";
            this.Registrate.Size = new System.Drawing.Size(206, 53);
            this.Registrate.TabIndex = 2;
            this.Registrate.Text = "Registrate";
            this.Registrate.UseVisualStyleBackColor = true;
            this.Registrate.Click += new System.EventHandler(this.Registrate_Click);
            // 
            // contrasena
            // 
            this.contrasena.Location = new System.Drawing.Point(265, 112);
            this.contrasena.Name = "contrasena";
            this.contrasena.Size = new System.Drawing.Size(264, 22);
            this.contrasena.TabIndex = 3;
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(265, 66);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(264, 22);
            this.nombre.TabIndex = 4;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.nombre);
            this.Controls.Add(this.contrasena);
            this.Controls.Add(this.Registrate);
            this.Controls.Add(this.password);
            this.Controls.Add(this.name);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.Button Registrate;
        private System.Windows.Forms.TextBox contrasena;
        private System.Windows.Forms.TextBox nombre;
    }
}