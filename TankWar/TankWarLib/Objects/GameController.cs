using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Objects
{
    class GameController
    {
        public List<Player> Players { get; } = new List<Player>();
        public Map Map { get; }

        private Random rnd = new Random();

        public GameController(Map map)
        {
            Map = map;
        }

        public void Tick()
        {

        }

        public void AddNewPlayer(string id)
        {
            Players.Add(new Player(id, new PointF(rnd.Next(50), rnd.Next(50)), Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))));
        }

        public void RemovePlayer(string id) => Players.RemoveAll(p => p.Id == id);
    }
}
