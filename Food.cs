using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game_Extra_Clase
{
    public class Food
    {

        private Point comidita;
        private const int tamInicialSerpiente = 3;
        private const int tamCelda = 20;
        private const int anchoTablero = 20;
        private const int alturaTablero = 20;

        public void DibujarComidita(Graphics g) 
        {
            g.FillRectangle(Brushes.Red, comidita.X * tamCelda, comidita.Y * tamCelda, tamCelda, tamCelda);
        }

        public void GenerarComidita() 
        { 
            Random random = new Random();
            comidita = new Point(random.Next(anchoTablero), random.Next(alturaTablero));
        }

    }
}
