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
        public readonly Color Color;
        public readonly PointF Start;
        public readonly PointF End;

        private readonly float A;
        private readonly float B;
        private readonly float C;

        public Line(Color color, PointF start, PointF end)
        {
            Color = color;
            Start = start;
            End = end;
            A = End.Y - Start.Y;
            B = Start.X - End.X;
            C = A * Start.X + B * Start.Y;
        }

        public PointF GetIntersection(Line otherLine)
        {
            // Calculations by https://pastebin.com/iQDhQTFN
            var determinant = A * otherLine.B - otherLine.A * B;

            if (Math.Abs(determinant) < 2.22044604925031E-15) // https://stackoverflow.com/a/35418564/14181760
                return new PointF(-100,-100);

            //Cramer's Rule
            var x = (otherLine.B * C - B * otherLine.C) / determinant;
            var y = (A * otherLine.C - otherLine.A * C) / determinant;

            var point = new PointF(x, y);

            if (!otherLine.IncludesPoint(point) || !IncludesPoint(point))
                return new PointF(-100, -100);

            return point;
        }

        public bool IncludesPoint(PointF p) =>
            GetDistance(p, Start) + GetDistance(p, End) - GetDistance(Start, End) < 0.00001;

        private static float GetDistance(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2));
        }
    }
}
