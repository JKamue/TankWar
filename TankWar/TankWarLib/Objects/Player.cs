using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Objects
{
    public class Player
    {
        public readonly string Id;
        public float Rotation { get; } = 0f;
        public float TurretRotation { get; } = 0f;
        public Color Color { get; }
        public PointF Position { get; }

        public Player(string id, PointF position, Color color)
        {
            Id = id;
            Position = position;
            Color = color;
        }
    }
}
