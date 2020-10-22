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

        public List<Line> GetCollisionLines()
        {
            var borderLines = new List<Line>
            {
                new Line(Color.White, new PointF(0,0), new PointF(0, Size.Height)),
                new Line(Color.White, new PointF(Size.Width,Size.Height), new PointF(0, Size.Height)),
                new Line(Color.White, new PointF(Size.Width,Size.Height), new PointF(Size.Width, 0)),
                new Line(Color.White, new PointF(0,0), new PointF(Size.Width, 0)),
            };
            return borderLines.Concat(Lines).ToList();
        }

        public RectangleF Bounds => new RectangleF(new Point(0,0), Size);
    }
}
