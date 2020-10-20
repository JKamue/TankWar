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
        public float Rotation = 0;
        public float TurretRotation = 0;
        public Color Color { get; }
        public PointF Position { get; private set; }

        private float move = 0;
        private float turn = 0;
        private float turret = 0;

        public Player(string id, PointF position, Color color)
        {
            Id = id;
            Position = position;
            Color = color;
        }

        public void Tick()
        {
            var positionVector = PointRotator.RotatePoint(new PointF(0, move), new PointF(0, 0), Rotation);
            Position = new PointF(Position.X + positionVector.X, Position.Y + positionVector.Y);
            Rotation += turn;
            TurretRotation += turret;
        }

        public void SetMovement(Movement movement)
        {
            movement.CheckAllNumbers();
            move = movement.Move;
            turn = movement.Turn;
            turret = movement.Turret;
        }
    }
}
