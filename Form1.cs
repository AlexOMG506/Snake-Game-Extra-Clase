using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game_Extra_Clase
{
    public partial class Form1 : Form
    {
        private const int tamCelda = 20;
        private const int anchoTablero = 20;
        private const int alturaTablero = 20;
        private const int tamInicialSerpiente = 3;

        private PictureBox pictureBox;
        
        private Point comidita;
        private Direction direccion;
        private Thread hiloJuego;
        private bool jugando;

        private enum Direction { Up, Down, Left, Right }

        Food comida = new Food();
        Snake snake = new Snake();
        public List<Point> serpiente;
        public Form1()
        {
            InitializeComponent();
            snake.Inicializar(); // Asegúrate de que la serpiente esté inicializada
            Jugar();
        }
        private void PintarJuego(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            snake.DibujarSerpiente(g);
           comida.DibujarComidita(g);
        }
        private void BucleJuego()
        {
            while (jugando)
            {
                MoverSerpiente();
                revisarChoque();
                pictureBox.Invalidate();
                Thread.Sleep(300); // Velocidad del juego
            }
        }
        private void Jugar()
        {
            jugando = true;
            direccion = Direction.Right;
            serpiente = new List<Point>();
            serpiente.Add(new Point(anchoTablero / 2, alturaTablero / 2));
            comida.GenerarComidita();
            pictureBox = new PictureBox();
            pictureBox.Size = new Size(anchoTablero * tamCelda, alturaTablero * tamCelda);
            pictureBox.Location = new Point(10, 10);
            pictureBox.BackColor = Color.Black;
            pictureBox.Paint += PintarJuego;
            Controls.Add(pictureBox);

            hiloJuego = new Thread(BucleJuego);
            hiloJuego.Start();
        }
        private void MoverSerpiente()
        {
            Point cabeza = serpiente[0];
            Point nuevaCabeza = new Point(cabeza.X, cabeza.Y);

            switch (direccion)
            {
                case Direction.Up:
                    nuevaCabeza.Y--;
                    break;
                case Direction.Down:
                    nuevaCabeza.Y++;
                    break;
                case Direction.Left:
                    nuevaCabeza.X--;
                    break;
                case Direction.Right:
                    nuevaCabeza.X++;
                    break;
            }
            serpiente.Insert(0, nuevaCabeza);
            if (nuevaCabeza != comidita)
            {
                serpiente.RemoveAt(serpiente.Count - 1);
            }
            else
            {
                comida.GenerarComidita();
            }
        }
        private void revisarChoque()
        {
            Point cabeza = serpiente[0];

            if (cabeza.X < 0 || cabeza.X >= anchoTablero || cabeza.Y < 0 || cabeza.Y >= alturaTablero)
            {
                jugando = false;
                MessageBox.Show("Perdiste!!!!");
                return;
            }

            for (int i = 1; i < serpiente.Count; i++)
            {
                if (cabeza == serpiente[i])
                {
                    jugando = false;
                    MessageBox.Show("Perdiste!!!!");
                    return;
                }
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    if (direccion != Direction.Down)
                        direccion = Direction.Up;
                    break;
                case Keys.Down:
                    if (direccion != Direction.Up)
                        direccion = Direction.Down;
                    break;
                case Keys.Left:
                    if (direccion != Direction.Right)
                        direccion = Direction.Left;
                    break;
                case Keys.Right:
                    if (direccion != Direction.Left)
                        direccion = Direction.Right;
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnJugar_Click(object sender, EventArgs e)
        {
            
        }
    }
}
