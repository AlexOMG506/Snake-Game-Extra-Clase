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
        private List<Point> segments;
        private Direction direction;

        Food comida = new Food();

        public Snake(int initialSize, Point initialPosition)
        {
            segments = new List<Point>();

            direction = Direction.Right;

            for (int i = 0; i < initialSize; i++)
            {
                segments.Add(new Point(initialPosition.X - i, initialPosition.Y));
            }
        }

        public List<Point> Segments => segments;

        public void Move()
        {
            Point head = segments[0];
            Point newHead = new Point(head.X, head.Y);

            switch (direction)
            {
                case Direction.Up:
                    newHead.Y--;
                    break;
                case Direction.Down:
                    newHead.Y++;
                    break;
                case Direction.Left:
                    newHead.X--;
                    break;
                case Direction.Right:
                    newHead.X++;
                    break;
            }

            segments.Insert(0, newHead);
            if (newHead != Food.Instance.Position)
            {

                segments.RemoveAt(segments.Count - 1);
            }
            //else
            //{

            //    Grow();  // Hacer crecer la serpiente
            //    Food.Instance.GenerateRandomPosition();
            //}

        }

        public void SetDirection(Direction newDirection)
        {
            // Evitar que la serpiente se mueva en la dirección opuesta.
            if ((direction == Direction.Up && newDirection != Direction.Down) ||
                (direction == Direction.Down && newDirection != Direction.Up) ||
                (direction == Direction.Left && newDirection != Direction.Right) ||
                (direction == Direction.Right && newDirection != Direction.Left))
            {
                direction = newDirection;

            }
        }


        public enum Direction { Up, Down, Left, Right }
        public void Grow()
        {
            // Obtén la posición actual de la cabeza de la serpiente
            Point head = segments[0];

            // Calcula la nueva posición del segmento basado en la dirección actual
            Point newSegment;

            switch (direction)
            {
                case Direction.Up:
                    newSegment = new Point(head.X, head.Y - 1);
                    break;
                case Direction.Down:
                    newSegment = new Point(head.X, head.Y + 1);
                    break;
                case Direction.Left:
                    newSegment = new Point(head.X - 1, head.Y);
                    break;
                case Direction.Right:
                    newSegment = new Point(head.X + 1, head.Y);
                    break;
                default:
                    // La dirección debería ser siempre una de las cuatro posibles.
                    throw new InvalidOperationException("Dirección de serpiente no válida.");
            }

            // Agrega el nuevo segmento a la lista de segmentos de la serpiente
            segments.Insert(0, newSegment);
        }



    }
}
