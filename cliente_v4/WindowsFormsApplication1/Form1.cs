using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
        delegate void DelegadoParaActualizarLista(string[] trozos);
       
        public Form1()
        {
            InitializeComponent();
            //Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           
        }

        public void visibilidadInicia (bool b)
        {
            if (InvokeRequired)
            Invoke(new Action(()=> this.incia_sesion.Visible = b));
        }

        public void visibilidadIniciada (bool b)
        {
            if (InvokeRequired)
                Invoke(new Action(() => this.sesion_inicidada.Visible = b));
        }
        public void ActualizaGrid(string[] trozos)
        {
            dataGridView1.Visible = true;

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            int numconectados = Convert.ToInt32(trozos[1]);
            dataGridView1.ColumnCount = 1;
            if(numconectados==0)
                dataGridView1.Visible = false;
            else
                dataGridView1.RowCount = numconectados;
            int k = 2;
            int i = 0;
            while (i < numconectados)
            {
                dataGridView1.Rows[i].Cells[0].Value = trozos[k].Split('/')[0];
                i++;
                k++;
            }
        }
    

        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos la respuesta del servidor
                
                byte[] msg = new byte[80];
                server.Receive(msg);
                
                string msg2 = System.Text.Encoding.ASCII.GetString(msg).Split('\0')[0];
                string[] trozos = msg2.Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje;
             
                MessageBox.Show(msg2);
                switch (codigo)
                {

                    case 1:  //Consulta 1 (Jugador con mas puntos)
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show(mensaje);
                        break;

                    case 2:      //Consulta 2 (Color de Roberta)
                        mensaje = trozos[1].Split('\0')[0];

                        MessageBox.Show(mensaje);

                        break;
                    case 3:       //Consulta 3 (Tiempo partida de Juan)
                        mensaje = trozos[1].Split('\0')[0];

                        MessageBox.Show(mensaje);
                        break;
                    case 4:     //Registrarse
                        mensaje = trozos[1].Split('\0')[0];

                        MessageBox.Show(mensaje);

                        // Nos desconectamos del servidor
                        atender.Abort();

                        break;

                    case 5: // Conectarse
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show(mensaje);

                        if (mensaje == "Conectado correctamente") 
                        {
                            visibilidadInicia(false);
                            visibilidadIniciada(true);
                        }
                        else 
                        {
                            atender.Abort();
                        }

                        break;

                    case 6:  //Lista de conectados

                        dataGridView1.Invoke(new DelegadoParaActualizarLista(ActualizaGrid), new object[] { trozos });
                        break;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (ColRob3.Checked)
            {   //Encontrar color de Roberta en la partida 3
                //Enviamos nombre y contraseña
                string mensaje = "2/" + nombre.Text + "/" + BoxContrasena.Text;
                    
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else if (JugMasPunt.Checked)
            {   //Encontrar jugador con record de puntos
                //Enviamos nombre y contraseña
                string mensaje = "1/";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);


            }
            else if (Tiempo.Checked)
            {   //Encontrar el timepo de la partida de Juan
                //Enviamos nombre y contraseña
                    string mensaje = "3/" + nombre.Text + "/" + BoxContrasena.Text;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
            }
        }

        private void conectar_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                

                //Enviamos nombre y contraseña
                string mensaje = "5/" + nombre.Text + "/" + BoxContrasena.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                
            }

            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

            //pongo en marcha el thread que atenderá los mensajes del servidor
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();

        }

        private void desconectar_Click(object sender, EventArgs e)
        {
            
            JugMasPunt.Visible = false;
            ColRob3.Visible = false;
            Tiempo.Visible = false;
            enviar.Visible = false;
            desconectar.Visible = false;
            label2.Visible = true;
            contrasena.Visible = true;
            nombre.Visible = true;
            BoxContrasena.Visible = true;
            conectar.Visible = true;


            //Mensaje de desconexión
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            AtenderServidor();

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }


        private void Registrate_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9050);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                
                //Enviamos los datos para realizar el registro
                string mensaje = "4/" + nombreregistro.Text + "/" + contrasenaregistro.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

            //pongo en marcha el thread que atenderá los mensajes del servidor
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Jugar_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario que se desea abrir
            Form2 Form2 = new Form2();

            // Mostrar el formulario secundario
            Form2.Show();

        }

        private void incia_sesion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sesion_inicidada_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
