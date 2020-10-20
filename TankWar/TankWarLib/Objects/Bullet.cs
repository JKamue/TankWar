using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Objects
{
    public class Bullet
    {
        public PointF Position;

        public Bullet(PointF position)
        {
            Position = position;
        }
    }
}
