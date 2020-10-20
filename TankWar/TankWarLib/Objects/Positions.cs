using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Objects
{
    public class Positions
    {
        public List<Bullet> Bullets;
        public List<Explosion> Explosions;
        public List<Player> Players;

        public Positions(List<Bullet> bullets, List<Explosion> explosions, List<Player> players)
        {
            Bullets = bullets;
            Explosions = explosions;
            Players = players;
        }
    }
}
