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
        private const int tamInicialSerpiente = 1;

        private PictureBox pictureBox;
        private Snake snake;
        private Food food;
        private Thread hiloJuego;
        private bool jugando;

        private int score = 0; // contador de puntos
        private int velocidad = 800; // velocidad de la serpiente
        private int tiempoSinComer = 0; // temporizador de penalizacion
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; // centramos el formulario
            Jugar();
        }

        private void PintarJuego(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DibujarSerpiente(g);
            DibujarComidita(g);

            lblScore.Text = $"Score Player: {score}";
            lblSpeed.Text = $"Speed Snake: {GetSpeedText()}";
        }
        private string GetSpeedText()
        {
            // Convierte la velocidad a una representación más legible
            double velocidadSegundos = velocidad / 1000.0;
            return $"{velocidadSegundos} s";
        }

        private void BucleJuego()
        {
            while (jugando)
            {
                snake.Move();
                RevisarChoque();
                pictureBox.Invalidate();
                Thread.Sleep(velocidad); // Velocidad del juego

                tiempoSinComer++; // Incrementar el tiempo sin comer

                if (tiempoSinComer > 50 && score > 50)
                {
                    // Penalizar el puntaje y reiniciar el tiempo sin comer
                    score = Math.Max(score - 5, 0);
                    tiempoSinComer = 0;
                }
            }
        }

        private void DibujarSerpiente(Graphics g)
        {
            // Dibujar la serpiente del Jugador 
            foreach (Point segment in snake.Segments)
            {
                g.FillRectangle(Brushes.Coral, segment.X * tamCelda, segment.Y * tamCelda, tamCelda, tamCelda);
            }


        }
        private void DibujarComidita(Graphics g)
        {
            g.FillRectangle(Brushes.Red, food.Position.X * tamCelda, food.Position.Y * tamCelda, tamCelda, tamCelda);
        }
        private void Jugar()
        {


            jugando = true;
            snake = new Snake(tamInicialSerpiente, new Point(anchoTablero / 2, alturaTablero / 2)); ;
            food = new Food();

            pictureBox = new PictureBox();
            pictureBox.Size = new Size(anchoTablero * tamCelda, alturaTablero * tamCelda);
            pictureBox.Location = new Point(10, 10);
            pictureBox.BackColor = Color.Black;
            pictureBox.Paint += PintarJuego;
            Controls.Add(pictureBox);

            hiloJuego = new Thread(BucleJuego);
            hiloJuego.Start();
        }

        private void RevisarChoque()
        {
            Point head = snake.Segments[0];

            if (head.X < 0 || head.X >= anchoTablero || head.Y < 0 || head.Y >= alturaTablero)
            {
                jugando = false;
                MostrarMensajePerdiste();
                return;
            }

            for (int i = 1; i < snake.Segments.Count; i++)
            {
                if (head == snake.Segments[i])
                {
                    jugando = false;
                    MostrarMensajePerdiste();
                    return;
                }
            }

            //// Verificar si la serpiente alcanza la comida
            if (head == food.Position)
            {
                snake.Grow();
                food.GenerateRandomPosition();

                score += 5;

                if (score >= 10)
                {
                    // Reducir la velocidad hasta un límite mínimo
                    velocidad = Math.Max(velocidad - 25, 100);
                }
                // Verificar si el puntaje alcanza 100 y mostrar mensaje de ganaste
                if (score >= 150)
                {
                    jugando = false;
                    MessageBox.Show("¡Ganaste! Puntaje: 150", "¡Felicidades!");
                    DialogResult reiniciar = MessageBox.Show("¿Quieres reiniciar el juego?", "Reiniciar", MessageBoxButtons.YesNo);
                    if (reiniciar == DialogResult.Yes)
                    {
                        ReiniciarJuego();
                    }
                    else
                    {
                        Application.Exit();
                    }
                    return;
                }
                pictureBox.Invalidate();
            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    snake.SetDirection(Snake.Direction.Up);
                    break;
                case Keys.Down:
                    snake.SetDirection(Snake.Direction.Down);
                    break;
                case Keys.Left:
                    snake.SetDirection(Snake.Direction.Left);
                    break;
                case Keys.Right:
                    snake.SetDirection(Snake.Direction.Right);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void MostrarMensajePerdiste()
        {
            MessageBox.Show("Perdiste!!!!", "¡Game Over!", MessageBoxButtons.OK);

            // Ofrecer la opción de reiniciar el juego
            DialogResult reiniciar = MessageBox.Show("¿Quieres reiniciar el juego?", "Reiniciar", MessageBoxButtons.YesNo);
            if (reiniciar == DialogResult.Yes)
            {
                ReiniciarJuego();
            }
            else
            {
                Application.Exit();
            }
        }
        private void ReiniciarJuego()
        {
            jugando = false;

            score = 0;
            velocidad = 800;
            tiempoSinComer = 0;
            jugando = true;

            // Reiniciar las instancias de la serpiente y la comida
            snake = new Snake(tamInicialSerpiente, new Point(anchoTablero / 2, alturaTablero / 2));
            food = new Food();



        }
    }
}
