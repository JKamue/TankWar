using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankWarLib.Objects;

namespace TankWarServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = new List<Line>
            {
                // Spawn Area left
                new Line(Color.DarkGreen, new PointF(60,30), new PointF(160,90)),
                new Line(Color.DarkGreen, new PointF(60,270), new PointF(160,210)),
                new Line(Color.DarkGreen, new PointF(200,120), new PointF(200,180)),
                new Line(Color.DarkGreen, new PointF(260,80), new PointF(260,120)),
                new Line(Color.DarkGreen, new PointF(260,220), new PointF(260,180)),

                // Spawn Area right
                new Line(Color.DarkRed, new PointF(540,30), new PointF(440,90)),
                new Line(Color.DarkRed, new PointF(540,270), new PointF(440,210)),
                new Line(Color.DarkRed, new PointF(400,120), new PointF(400,180)),
                new Line(Color.DarkRed, new PointF(340,80), new PointF(340,120)),
                new Line(Color.DarkRed, new PointF(340,220), new PointF(340,180))
            };
            var map = new Map(lines, new Size(600,300));

            var server = new TankWarLib.TankWarServer(map, 50000, true);
            Console.WriteLine("Server started\n--------------");
            Console.ReadKey();
        }
    }
}
