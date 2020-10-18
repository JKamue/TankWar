using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Objects
{
    public class Map
    {
        public readonly List<Line> Lines;

        public Map(List<Line> lines)
        {
            Lines = lines;
        }
    }
}
