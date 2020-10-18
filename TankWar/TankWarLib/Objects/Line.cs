using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Objects
{
    public class Line
    {
        public Color Color;
        public PointF Start;
        public PointF End;

        public Line(Color color, PointF start, PointF end)
        {
            Color = color;
            Start = start;
            End = end;
        }
    }
}
