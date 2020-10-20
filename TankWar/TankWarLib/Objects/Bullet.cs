using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Objects
{
    public class Bullet
    {
        public PointF Position;
        public float Direction;
        public string ShooterId;
        public Bullet(PointF position, float direction, string shooterId)
        {
            Position = position;
            Direction = direction;
            ShooterId = shooterId;
        }

        public void Tick()
        {
            var turned = PointRotator.RotatePoint(new PointF(0, 10), new PointF(0, 0), Direction);
            Position.X += turned.X;
            Position.Y += turned.Y;
        }
    }
}
