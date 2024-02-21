using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game_Extra_Clase
{
    public class Snake
    {
        private const int tamCelda = 20;
        private const int anchoTablero = 20;
        private const int alturaTablero = 20;
        private const int tamInicialSerpiente = 3;

        
        public List<Point> serpiente;
        private Point comidita;




        public void Inicializar()
        {
            serpiente = new List<Point>();

            // Agregar segmentos iniciales a la serpiente
            for (int i = 0; i < tamInicialSerpiente; i++)
            {
                serpiente.Add(new Point(anchoTablero / 2 - i, alturaTablero / 2));
            }
        }


        public void DibujarSerpiente(Graphics g)
        {
            foreach (Point segment in serpiente)
            {
                g.FillRectangle(Brushes.Green, segment.X * tamCelda, segment.Y * tamCelda, tamCelda, tamCelda);
            }
        }

        

    }
}
