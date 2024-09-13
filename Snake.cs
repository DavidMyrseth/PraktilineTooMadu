using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraktilineTööMadu
{
    class Snake : Figure
    {
        Direction direction;
        public Snake(Point tail, int lenght, Direction direction)
        {
            direction = _direction;
            pList = new List<Point>();
            for (int i = 0; i < lenght; i++)
            {
                Point p = new Point(tail);
                p.Move(i, direction);
                pList.Add(p);
            }
        }

        internal void Move()
        {
            Point tail = pList.First();
            pList.Remove( tail );
            Point head = GetNextPoint();
            pList.Add( head );

            tail.Clear();
            head.Draw();
        }

        public Point GetNextPoint() 
        { 

        }
    }
}

// Десятый урок
//internal void Move()
//{
//    throw new NotImplementedException();
//}