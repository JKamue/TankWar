using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Objects
{
    public class Map
    {
        public readonly List<Line> Lines;
        public readonly Size Size;

        public Map(List<Line> lines, Size size)
        {
            Lines = lines;
            Size = size;
        }

        public RectangleF Bounds => new RectangleF(new Point(0,0), Size);
    }
}
