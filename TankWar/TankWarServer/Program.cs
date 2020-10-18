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
            var line1 = new Line(Color.Green, new PointF(10,10), new PointF(20,80));
            var lines = new List<Line> {line1};
            var map = new Map(lines);

            var server = new TankWarLib.TankWarServer(map, 50000, true);
            Console.WriteLine("Server started\n--------------");
            Console.ReadKey();
        }
    }
}
