using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraktilineTööMadu
{
    public class Snake : Figure
    {
        public Snake( Point tall, int lenght, int direction )
        {
            for (int i = 0; i < lenght; i++) 
            {
                Point p = new Point( tall );
                p.Move( i, direction );
                pList.Add( p );
            }
        }
    }
}
