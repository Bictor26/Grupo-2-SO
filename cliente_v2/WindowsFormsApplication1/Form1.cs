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

        delegate void DelegadoParaEscribir(string mensaje);

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           
        }
        
        public void PonContador (string contador)
        {
            contLbl.Text = contador;
        }
        


        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos la respuesta del servidor
                byte[] msg = new byte[80];
                server.Receive(msg);

                string[] trozos = Encoding.ASCII.GetString(msg).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje = mensaje = trozos[1].Split('\0')[0];

                switch (codigo)
                {

                    case 1:  //Consulta 1 (Jugador con mas puntos)

                        MessageBox.Show(mensaje);
                        break;
                    case 2:      //Consulta 2 (Color de Roberta)

                        MessageBox.Show(mensaje);

                        break;
                    case 3:       //Consulta 3 (Tiempo partida de Juan)

                        MessageBox.Show(mensaje);
                        break;
                    case 4:     //Registrarse

                        int respuesta = Convert.ToInt32(mensaje);
                        if (respuesta == 0)
                        {
                            //En el caso de que el usuario no esté en la base de datos
                            //lo cremos
                            MessageBox.Show("Registrado correctamente");
                        }
                        else if (respuesta == 1)
                        {
                            //Si el usuario ya está en la base de datos no lo introducimos de nuevo
                            MessageBox.Show("Ya estabas registrado, inicia sesión!");
                        }
                        //Se deconecta del servidor
                        mensaje = "0/";
                        msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);

                        // Nos desconectamos del servidor
                        this.BackColor = Color.Gray;
                        server.Shutdown(SocketShutdown.Both);
                        server.Close();

                        break;

                    case 5: // Conectarse
                        respuesta = Convert.ToInt32(mensaje);
                        if (respuesta == 1)
                        {   //En el caso de que el jugador exista 
                            JugMasPunt.Visible = true;
                            ColRob3.Visible = true;
                            Tiempo.Visible = true;
                            enviar.Visible = true;
                            desconectar.Visible = true;
                            label2.Visible = false;
                            contrasena.Visible = false;
                            nombre.Visible = false;
                            BoxContrasena.Visible = false;
                            conectar.Visible = false;
                            MessageBox.Show("Conectado correctamente");

                        }
                        else if (respuesta == 0)
                        {
                            //El jugador necesita registrarse primero
                            MessageBox.Show("No estas registrado");

                        }


                        break;
                    case 6:  //Lista de conectados

                        string[] listaconectados = mensaje.Split('/');
                        int numconectados = Convert.ToInt32(listaconectados[0]);
                        dataGridView1.ColumnCount = 1;
                        dataGridView1.RowCount = numconectados;

                        for (int i = 1; i <= numconectados; i++)
                        {

                            dataGridView1.Rows[i - 1].Cells[0].Value = listaconectados[i];

                        }
                        break;

                    case 7: //Recibimos notificacion

                        //contLbl.Text = mensaje;
                        DelegadoParaEscribir delegado = new DelegadoParaEscribir(PonContador);
                        contLbl.Invoke(delegado, new object[] {mensaje});
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
                if (ColRob3.Checked)
                {   
                    //Encontrar color de Roberta en la partida 3
                    //Enviamos nombre y contraseña
                    string mensaje = "2/" + nombre.Text + "/" + BoxContrasena.Text;
                    
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    
                }
                else if (JugMasPunt.Checked)
                {   
                    //Encontrar jugador con record de puntos
                    //Enviamos nombre y contraseña
                    string mensaje = "1/" + nombre.Text + "/" + BoxContrasena.Text;
                    
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                }
                else if (Tiempo.Checked)
                {   
                    //Encontrar el timepo de la partida de Juan
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

                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();

            }

            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }


        }

        private void desconectar_Click(object sender, EventArgs e)
        {
            string mensaje = "0/" + nombre.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            

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

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void conectados_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;

            //enviamos mensaje al sevidor
            string mensaje = "6/" + nombre.Text + "/" + contrasena.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

           

            


        }
    }
}
