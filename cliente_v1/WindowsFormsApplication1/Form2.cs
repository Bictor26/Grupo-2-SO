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
    public partial class Form2 : Form
    {
        List<PictureBox> Lista = new List<PictureBox>();
        int TamañoJugador = 26, tiempo = 10;
        PictureBox Pnts = new PictureBox();
        String Direccion = "right"; // Dirección del jugador
        public Form2()
        {
            InitializeComponent();
            EmpezarPartida();
        }
        Socket server;

        private void EmpezarPartida()
        {
            tiempo = 10;
            Direccion = "right";
            timer1.Interval = 200;
            Pnts.Text = "0";
            Lista = new List<PictureBox>();
            for (int i = 2; 0 >= i; i++)
            {
                CrearJugador(Lista, this, (i * TamañoJugador) + 70, 80);
            }
        }

        public void CrearJugador(List<PictureBox> ListaPelota, Form formulario, int posicionx, int posiciony)
        {
            PictureBox pb = new PictureBox();
            pb.Location = new Point(posicionx, posiciony);
            pb.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("cola");
            pb.BackColor = Color.Transparent;
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            ListaPelota.Add(pb);
            formulario.Controls.Add(pb);
        }
        //Teclas de movimiento de Jugador
        private void MoverJugador(object sender, KeyEventArgs e)
        {
            Direccion = ((e.KeyCode & Keys.Up) == Keys.Up) ? "up" : Direccion;
            Direccion = ((e.KeyCode & Keys.Down) == Keys.Down) ? "down" : Direccion;
            Direccion = ((e.KeyCode & Keys.Left) == Keys.Left) ? "left" : Direccion;
            Direccion = ((e.KeyCode & Keys.Right) == Keys.Right) ? "right" : Direccion;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int nx = 2;
            int ny = 2;
            for (int i = Lista.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    if (Direccion == "right") nx = nx + TamañoJugador;
                    else if (Direccion == "up") ny = ny - TamañoJugador;
                    else if (Direccion == "down") ny = ny + TamañoJugador;
                    else if (Direccion == "left") nx = nx - TamañoJugador;
                   // Lista[0].Image = (Bitmap).Properties.Resources.ResourceManager.GetObject("head" + Direccion);
                    Lista[0].Location = new Point(nx, ny);

                }
                else
                {
                   // Lista[i].Location = new Point((Lista[i - 1].Location.X), (Lista[i].Location.Y));
                   // Lista[i].Location = new Point((Lista[i].Location.X), (Lista[i - 1].Location.Y));
                }
            }
        }
    }
}
