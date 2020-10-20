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
            BulletTick();
            ExplosionTick();
        }

        private void ExplosionTick()
        {
            for (int i = 0; i < Explosions.Count; i++)
            {
                Explosions[i].Tick();

                if (!Explosions[i].EndOfLife())
                    continue;

                Explosions.Remove(Explosions[i]);
                i--;
            }
        }

        private void BulletTick()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                var b = Bullets[i];
                if (Map.Bounds.Contains(b.Position))
                {
                    var lastPosition = b.Position;
                    b.Tick();
                    var newPosition = b.Position;
                    var bulletLine = new Line(Color.Black, lastPosition, newPosition);
                    var intersects = false;
                    PointF intersection;
                    foreach (var line in Map.Lines)
                    {
                        intersection = line.GetIntersection(bulletLine);
                        if (intersection != new PointF(-100, -100))
                        {
                            intersects = true;
                            Explosions.Add(new Explosion(intersection));
                            break;
                        }
                    }

                    if (!intersects)
                        continue;

                }
                Bullets.Remove(b);
                i--;
            }
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
