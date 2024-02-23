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

        private static Food instance;
        private const int anchoTablero = 20;
        private const int alturaTablero = 20;
        private Point position;

        public Food()
        {
            // Puedes inicializar la posición inicial de la comida aquí o en otro método según tus necesidades.
            GenerateRandomPosition();
        }
        public static Food Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Food();
                }
                return instance;
            }
        }

        public Point Position => position;

        public void GenerateRandomPosition()
        {
            Random random = new Random();
            position = new Point(random.Next(anchoTablero), random.Next(alturaTablero));
        }

    }
}
