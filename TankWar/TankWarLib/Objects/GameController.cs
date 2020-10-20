using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Objects
{
    public class GameController
    {
        public List<Player> Players { get; } = new List<Player>();
        public List<Explosion> Explosions { get; } = new List<Explosion>();
        public List<Bullet> Bullets { get; }  = new List<Bullet>();

        private Dictionary<string, DateTime> _lastShot { get; } = new Dictionary<string, DateTime>();

        public Map Map { get; }

        private Random rnd = new Random();

        public GameController(Map map)
        {
            Map = map;
        }

        public void Tick()
        {
            Players.ForEach(p => p.Tick());
            Bullets.ForEach(b => b.Tick());
        }

        public void AddNewPlayer(string id)
        {
            var newPlayer = new Player(id, new PointF(rnd.Next(50), rnd.Next(50)),
                Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)));
            Players.Add(newPlayer);
            _lastShot[newPlayer.Id] = DateTime.MinValue;
        }

        public void PlayerShoot(Player player)
        {
            if (_lastShot[player.Id] > DateTime.Now.Subtract(new TimeSpan(0, 0, 0, 2)))
                return;

            Bullets.Add(new Bullet(player.Position, player.TurretRotation));
            _lastShot[player.Id] = DateTime.Now;
        }

        public void RemovePlayer(string id) => Players.RemoveAll(p => p.Id == id);
    }
}
