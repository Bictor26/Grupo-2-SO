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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           
        }

   
        private void button2_Click(object sender, EventArgs e)
        {


                if (ColRob3.Checked)
                {
                    // Quiere saber la longitud
                    string mensaje = "2/" + nombre.Text + "/" + BoxContrasena.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    //Recibimos la respuesta del servidor
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2);
                    MessageBox.Show(mensaje);
                }
                else if (JugMasPunt.Checked)
                {
                    // Quiere saber si el nombre es bonito
                    string mensaje = "1/" + nombre.Text + "/" + BoxContrasena.Text;
                    // Enviamos al servidor el nombre tecleado
                     byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                
                
                    //Recibimos la respuesta del servidor
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2);

                    
                    MessageBox.Show(mensaje);


                }
                else
                {
                    //Enviamos nombre y altura
                     string mensaje = "3/" + nombre.Text + "/" + BoxContrasena.Text;
                    //Enviamos al servidor el nombre del teclado:
                      byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                      server.Send(msg);

                     //Rebicibimos la respiuesta del servidor
                      byte[] msg2 = new byte[80];
                      server.Receive(msg2);
                      mensaje = Encoding.ASCII.GetString(msg2);
                      MessageBox.Show(mensaje);

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
                MessageBox.Show("Conectado correctamente");
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
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void Registrate_Click(object sender, EventArgs e)
        {


            // Quiere saber la longitud
            string mensaje = "4/" + nombreregistro.Text + "/" + contrasenaregistro.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2);
            MessageBox.Show(mensaje);

        }
    }
}
