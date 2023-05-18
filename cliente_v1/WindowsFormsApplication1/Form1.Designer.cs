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
            this.enviar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.desconectar = new System.Windows.Forms.Button();
            this.JugMasPunt = new System.Windows.Forms.RadioButton();
            this.Tiempo = new System.Windows.Forms.RadioButton();
            this.ColRob3 = new System.Windows.Forms.RadioButton();
            this.conectar = new System.Windows.Forms.Button();
            this.contrasena = new System.Windows.Forms.Label();
            this.BoxContrasena = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.registrar = new System.Windows.Forms.Label();
            this.iniciarsesion = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nombreregistro = new System.Windows.Forms.TextBox();
            this.contrasenaregistro = new System.Windows.Forms.TextBox();
            this.Registrate = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.partida = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Jugar = new System.Windows.Forms.Button();
            this.invitar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // enviar
            // 
            this.enviar.BackColor = System.Drawing.Color.MidnightBlue;
            this.enviar.ForeColor = System.Drawing.Color.White;
            this.enviar.Location = new System.Drawing.Point(343, 207);
            this.enviar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.enviar.Name = "enviar";
            this.enviar.Size = new System.Drawing.Size(100, 28);
            this.enviar.TabIndex = 5;
            this.enviar.Text = "Enviar";
            this.enviar.UseVisualStyleBackColor = false;
            this.enviar.Visible = false;
            this.enviar.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Lavender;
            this.groupBox1.Controls.Add(this.enviar);
            this.groupBox1.Controls.Add(this.desconectar);
            this.groupBox1.Controls.Add(this.JugMasPunt);
            this.groupBox1.Controls.Add(this.Tiempo);
            this.groupBox1.Controls.Add(this.ColRob3);
            this.groupBox1.Controls.Add(this.conectar);
            this.groupBox1.Controls.Add(this.contrasena);
            this.groupBox1.Controls.Add(this.BoxContrasena);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nombre);
            this.groupBox1.Location = new System.Drawing.Point(44, 30);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(484, 347);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peticion";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // desconectar
            // 
            this.desconectar.BackColor = System.Drawing.Color.MidnightBlue;
            this.desconectar.ForeColor = System.Drawing.Color.White;
            this.desconectar.Location = new System.Drawing.Point(155, 282);
            this.desconectar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.desconectar.Name = "desconectar";
            this.desconectar.Size = new System.Drawing.Size(172, 47);
            this.desconectar.TabIndex = 7;
            this.desconectar.Text = "Desconectar";
            this.desconectar.UseVisualStyleBackColor = false;
            this.desconectar.Visible = false;
            this.desconectar.Click += new System.EventHandler(this.desconectar_Click);
            // 
            // JugMasPunt
            // 
            this.JugMasPunt.AutoSize = true;
            this.JugMasPunt.Location = new System.Drawing.Point(37, 182);
            this.JugMasPunt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.JugMasPunt.Name = "JugMasPunt";
            this.JugMasPunt.Size = new System.Drawing.Size(175, 20);
            this.JugMasPunt.TabIndex = 15;
            this.JugMasPunt.Text = "Jugador con más puntos";
            this.JugMasPunt.UseVisualStyleBackColor = true;
            this.JugMasPunt.Visible = false;
            // 
            // Tiempo
            // 
            this.Tiempo.AutoSize = true;
            this.Tiempo.Location = new System.Drawing.Point(37, 239);
            this.Tiempo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tiempo.Name = "Tiempo";
            this.Tiempo.Size = new System.Drawing.Size(204, 20);
            this.Tiempo.TabIndex = 14;
            this.Tiempo.Text = "Tiempo de la partida de Juan";
            this.Tiempo.UseVisualStyleBackColor = true;
            this.Tiempo.Visible = false;
            // 
            // ColRob3
            // 
            this.ColRob3.AutoSize = true;
            this.ColRob3.Location = new System.Drawing.Point(37, 210);
            this.ColRob3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ColRob3.Name = "ColRob3";
            this.ColRob3.Size = new System.Drawing.Size(204, 20);
            this.ColRob3.TabIndex = 7;
            this.ColRob3.Text = "Color de Roberta en partida 3";
            this.ColRob3.UseVisualStyleBackColor = true;
            this.ColRob3.Visible = false;
            // 
            // conectar
            // 
            this.conectar.BackColor = System.Drawing.Color.MidnightBlue;
            this.conectar.ForeColor = System.Drawing.Color.White;
            this.conectar.Location = new System.Drawing.Point(380, 54);
            this.conectar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.conectar.Name = "conectar";
            this.conectar.Size = new System.Drawing.Size(97, 70);
            this.conectar.TabIndex = 7;
            this.conectar.Text = "Iniciar sesión";
            this.conectar.UseVisualStyleBackColor = false;
            this.conectar.Click += new System.EventHandler(this.conectar_Click);
            // 
            // contrasena
            // 
            this.contrasena.AutoSize = true;
            this.contrasena.Location = new System.Drawing.Point(47, 96);
            this.contrasena.Name = "contrasena";
            this.contrasena.Size = new System.Drawing.Size(79, 16);
            this.contrasena.TabIndex = 10;
            this.contrasena.Text = "Contraseña:";
            // 
            // BoxContrasena
            // 
            this.BoxContrasena.Location = new System.Drawing.Point(155, 96);
            this.BoxContrasena.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BoxContrasena.Name = "BoxContrasena";
            this.BoxContrasena.Size = new System.Drawing.Size(217, 22);
            this.BoxContrasena.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(155, 54);
            this.nombre.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(217, 22);
            this.nombre.TabIndex = 3;
            // 
            // registrar
            // 
            this.registrar.AutoSize = true;
            this.registrar.Location = new System.Drawing.Point(597, 10);
            this.registrar.Name = "registrar";
            this.registrar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.registrar.Size = new System.Drawing.Size(94, 16);
            this.registrar.TabIndex = 7;
            this.registrar.Text = "REGISTRATE";
            // 
            // iniciarsesion
            // 
            this.iniciarsesion.AutoSize = true;
            this.iniciarsesion.Location = new System.Drawing.Point(51, 11);
            this.iniciarsesion.Name = "iniciarsesion";
            this.iniciarsesion.Size = new System.Drawing.Size(107, 16);
            this.iniciarsesion.TabIndex = 8;
            this.iniciarsesion.Text = "INICIAR SESIÓN";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Lavender;
            this.groupBox2.Controls.Add(this.nombreregistro);
            this.groupBox2.Controls.Add(this.contrasenaregistro);
            this.groupBox2.Controls.Add(this.Registrate);
            this.groupBox2.Controls.Add(this.password);
            this.groupBox2.Controls.Add(this.name);
            this.groupBox2.Location = new System.Drawing.Point(592, 28);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(477, 348);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rellena los datos necesarios para registrarte";
            // 
            // nombreregistro
            // 
            this.nombreregistro.Location = new System.Drawing.Point(167, 82);
            this.nombreregistro.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nombreregistro.Name = "nombreregistro";
            this.nombreregistro.Size = new System.Drawing.Size(264, 22);
            this.nombreregistro.TabIndex = 9;
            // 
            // contrasenaregistro
            // 
            this.contrasenaregistro.Location = new System.Drawing.Point(167, 129);
            this.contrasenaregistro.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.contrasenaregistro.Name = "contrasenaregistro";
            this.contrasenaregistro.Size = new System.Drawing.Size(264, 22);
            this.contrasenaregistro.TabIndex = 8;
            // 
            // Registrate
            // 
            this.Registrate.BackColor = System.Drawing.Color.MidnightBlue;
            this.Registrate.ForeColor = System.Drawing.Color.White;
            this.Registrate.Location = new System.Drawing.Point(167, 212);
            this.Registrate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Registrate.Name = "Registrate";
            this.Registrate.Size = new System.Drawing.Size(205, 53);
            this.Registrate.TabIndex = 7;
            this.Registrate.Text = "Registrate";
            this.Registrate.UseVisualStyleBackColor = false;
            this.Registrate.Click += new System.EventHandler(this.Registrate_Click);
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Location = new System.Drawing.Point(44, 134);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(79, 16);
            this.password.TabIndex = 6;
            this.password.Text = "Contraseña:";
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(44, 82);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(59, 16);
            this.name.TabIndex = 5;
            this.name.Text = "Nombre:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Lavender;
            this.groupBox3.Controls.Add(this.invitar);
            this.groupBox3.Controls.Add(this.partida);
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(44, 420);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(550, 284);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Usuarios conectados";
            // 
            // partida
            // 
            this.partida.AutoSize = true;
            this.partida.Location = new System.Drawing.Point(33, 41);
            this.partida.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.partida.Name = "partida";
            this.partida.Size = new System.Drawing.Size(357, 16);
            this.partida.TabIndex = 16;
            this.partida.Text = "Selecciona de uno a tres jugadores para crear una partida:";
            this.partida.Visible = false;
            this.partida.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Lavender;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.Lavender;
            this.dataGridView1.Location = new System.Drawing.Point(37, 71);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(397, 188);
            this.dataGridView1.TabIndex = 15;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Jugar
            // 
            this.Jugar.BackColor = System.Drawing.Color.MidnightBlue;
            this.Jugar.ForeColor = System.Drawing.Color.White;
            this.Jugar.Location = new System.Drawing.Point(948, 645);
            this.Jugar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Jugar.Name = "Jugar";
            this.Jugar.Size = new System.Drawing.Size(155, 59);
            this.Jugar.TabIndex = 15;
            this.Jugar.Text = "JUGAR";
            this.Jugar.UseVisualStyleBackColor = false;
            this.Jugar.Click += new System.EventHandler(this.Jugar_Click);
            // 
            // invitar
            // 
            this.invitar.BackColor = System.Drawing.Color.MidnightBlue;
            this.invitar.ForeColor = System.Drawing.Color.White;
            this.invitar.Location = new System.Drawing.Point(449, 71);
            this.invitar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.invitar.Name = "invitar";
            this.invitar.Size = new System.Drawing.Size(87, 188);
            this.invitar.TabIndex = 16;
            this.invitar.Text = "INVITAR";
            this.invitar.UseVisualStyleBackColor = false;
            this.invitar.Click += new System.EventHandler(this.invitar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.wallpaperbetter_com_1366x768;
            this.ClientSize = new System.Drawing.Size(1117, 720);
            this.Controls.Add(this.Jugar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.iniciarsesion);
            this.Controls.Add(this.registrar);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button enviar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton ColRob3;
        private System.Windows.Forms.Button desconectar;
        private System.Windows.Forms.Label registrar;
        private System.Windows.Forms.Label iniciarsesion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox nombreregistro;
        private System.Windows.Forms.TextBox contrasenaregistro;
        private System.Windows.Forms.Button Registrate;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.RadioButton Tiempo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Jugar;
        private System.Windows.Forms.RadioButton JugMasPunt;
        private System.Windows.Forms.Button conectar;
        private System.Windows.Forms.TextBox BoxContrasena;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label contrasena;
        private System.Windows.Forms.Label partida;
        private System.Windows.Forms.Button invitar;
    }
}

