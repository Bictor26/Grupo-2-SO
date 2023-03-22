namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.desconectar = new System.Windows.Forms.Button();
            this.conectar = new System.Windows.Forms.Button();
            this.Tiempo = new System.Windows.Forms.CheckBox();
            this.contrasena = new System.Windows.Forms.Label();
            this.BoxContrasena = new System.Windows.Forms.TextBox();
            this.ColRob3 = new System.Windows.Forms.RadioButton();
            this.JugMasPunt = new System.Windows.Forms.RadioButton();
            this.registrar = new System.Windows.Forms.Label();
            this.iniciarsesion = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nombreregistro = new System.Windows.Forms.TextBox();
            this.contrasenaregistro = new System.Windows.Forms.TextBox();
            this.Registrate = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre";
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(155, 38);
            this.nombre.Margin = new System.Windows.Forms.Padding(4);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(217, 22);
            this.nombre.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(193, 236);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 5;
            this.button2.Text = "Enviar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.desconectar);
            this.groupBox1.Controls.Add(this.conectar);
            this.groupBox1.Controls.Add(this.Tiempo);
            this.groupBox1.Controls.Add(this.contrasena);
            this.groupBox1.Controls.Add(this.BoxContrasena);
            this.groupBox1.Controls.Add(this.ColRob3);
            this.groupBox1.Controls.Add(this.JugMasPunt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.nombre);
            this.groupBox1.Location = new System.Drawing.Point(16, 140);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(484, 347);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peticion";
            // 
            // desconectar
            // 
            this.desconectar.Location = new System.Drawing.Point(287, 282);
            this.desconectar.Name = "desconectar";
            this.desconectar.Size = new System.Drawing.Size(172, 47);
            this.desconectar.TabIndex = 7;
            this.desconectar.Text = "Desconectar";
            this.desconectar.UseVisualStyleBackColor = true;
            this.desconectar.Click += new System.EventHandler(this.desconectar_Click);
            // 
            // conectar
            // 
            this.conectar.Location = new System.Drawing.Point(16, 282);
            this.conectar.Name = "conectar";
            this.conectar.Size = new System.Drawing.Size(183, 47);
            this.conectar.TabIndex = 7;
            this.conectar.Text = "Conectar";
            this.conectar.UseVisualStyleBackColor = true;
            this.conectar.Click += new System.EventHandler(this.conectar_Click);
            // 
            // Tiempo
            // 
            this.Tiempo.AutoSize = true;
            this.Tiempo.Location = new System.Drawing.Point(139, 193);
            this.Tiempo.Name = "Tiempo";
            this.Tiempo.Size = new System.Drawing.Size(215, 21);
            this.Tiempo.TabIndex = 11;
            this.Tiempo.Text = "Tiempo de la partida de Juan";
            this.Tiempo.UseVisualStyleBackColor = true;
            // 
            // contrasena
            // 
            this.contrasena.AutoSize = true;
            this.contrasena.Location = new System.Drawing.Point(34, 84);
            this.contrasena.Name = "contrasena";
            this.contrasena.Size = new System.Drawing.Size(85, 17);
            this.contrasena.TabIndex = 10;
            this.contrasena.Text = "Contraseña:";
            // 
            // BoxContrasena
            // 
            this.BoxContrasena.Location = new System.Drawing.Point(155, 84);
            this.BoxContrasena.Name = "BoxContrasena";
            this.BoxContrasena.Size = new System.Drawing.Size(217, 22);
            this.BoxContrasena.TabIndex = 9;
            // 
            // ColRob3
            // 
            this.ColRob3.AutoSize = true;
            this.ColRob3.Location = new System.Drawing.Point(139, 165);
            this.ColRob3.Margin = new System.Windows.Forms.Padding(4);
            this.ColRob3.Name = "ColRob3";
            this.ColRob3.Size = new System.Drawing.Size(217, 21);
            this.ColRob3.TabIndex = 7;
            this.ColRob3.TabStop = true;
            this.ColRob3.Text = "Color de Roberta en partida 3";
            this.ColRob3.UseVisualStyleBackColor = true;
            // 
            // JugMasPunt
            // 
            this.JugMasPunt.AutoSize = true;
            this.JugMasPunt.Location = new System.Drawing.Point(139, 137);
            this.JugMasPunt.Margin = new System.Windows.Forms.Padding(4);
            this.JugMasPunt.Name = "JugMasPunt";
            this.JugMasPunt.Size = new System.Drawing.Size(185, 21);
            this.JugMasPunt.TabIndex = 8;
            this.JugMasPunt.TabStop = true;
            this.JugMasPunt.Text = "Jugador con más puntos";
            this.JugMasPunt.UseVisualStyleBackColor = true;
            // 
            // registrar
            // 
            this.registrar.AutoSize = true;
            this.registrar.Location = new System.Drawing.Point(561, 119);
            this.registrar.Name = "registrar";
            this.registrar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.registrar.Size = new System.Drawing.Size(100, 17);
            this.registrar.TabIndex = 7;
            this.registrar.Text = "REGISTRATE:";
            // 
            // iniciarsesion
            // 
            this.iniciarsesion.AutoSize = true;
            this.iniciarsesion.Location = new System.Drawing.Point(29, 119);
            this.iniciarsesion.Name = "iniciarsesion";
            this.iniciarsesion.Size = new System.Drawing.Size(110, 17);
            this.iniciarsesion.TabIndex = 8;
            this.iniciarsesion.Text = "INICIAR SESIÓN";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox2.Controls.Add(this.nombreregistro);
            this.groupBox2.Controls.Add(this.contrasenaregistro);
            this.groupBox2.Controls.Add(this.Registrate);
            this.groupBox2.Controls.Add(this.password);
            this.groupBox2.Controls.Add(this.name);
            this.groupBox2.Location = new System.Drawing.Point(564, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(477, 348);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rellena los datos necesarios para registrarte";
            // 
            // nombreregistro
            // 
            this.nombreregistro.Location = new System.Drawing.Point(167, 83);
            this.nombreregistro.Name = "nombreregistro";
            this.nombreregistro.Size = new System.Drawing.Size(264, 22);
            this.nombreregistro.TabIndex = 9;
            // 
            // contrasenaregistro
            // 
            this.contrasenaregistro.Location = new System.Drawing.Point(167, 129);
            this.contrasenaregistro.Name = "contrasenaregistro";
            this.contrasenaregistro.Size = new System.Drawing.Size(264, 22);
            this.contrasenaregistro.TabIndex = 8;
            // 
            // Registrate
            // 
            this.Registrate.Location = new System.Drawing.Point(167, 212);
            this.Registrate.Name = "Registrate";
            this.Registrate.Size = new System.Drawing.Size(206, 53);
            this.Registrate.TabIndex = 7;
            this.Registrate.Text = "Registrate";
            this.Registrate.UseVisualStyleBackColor = true;
            this.Registrate.Click += new System.EventHandler(this.Registrate_Click);
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Location = new System.Drawing.Point(44, 134);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(85, 17);
            this.password.TabIndex = 6;
            this.password.Text = "Contraseña:";
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(44, 83);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(62, 17);
            this.name.TabIndex = 5;
            this.name.Text = "Nombre:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 720);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.iniciarsesion);
            this.Controls.Add(this.registrar);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton ColRob3;
        private System.Windows.Forms.RadioButton JugMasPunt;
        private System.Windows.Forms.Button conectar;
        private System.Windows.Forms.Label contrasena;
        private System.Windows.Forms.TextBox BoxContrasena;
        private System.Windows.Forms.CheckBox Tiempo;
        private System.Windows.Forms.Button desconectar;
        private System.Windows.Forms.Label registrar;
        private System.Windows.Forms.Label iniciarsesion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox nombreregistro;
        private System.Windows.Forms.TextBox contrasenaregistro;
        private System.Windows.Forms.Button Registrate;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.Label name;
    }
}

