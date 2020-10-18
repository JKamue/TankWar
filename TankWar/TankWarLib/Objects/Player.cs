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
        public int TurretRotation = 0;
        public Color Color { get; }
        public PointF Position { get; private set; }

        private int move = 0;
        private int turn = 0;
        private int targetTurret = 0;

        public Player(string id, PointF position, Color color)
        {
            Id = id;
            Position = position;
            Color = color;
        }

        public void Tick()
        {
            var positionVector = PointRotator.RotatePoint(new PointF(move, 0), new PointF(0, 0), Rotation);
            Position = new PointF(Position.X + positionVector.X, Position.Y + positionVector.Y);
            Rotation += turn;

            if (TurretRotation > targetTurret)
                TurretRotation -= turn;

            if (TurretRotation < targetTurret)
                TurretRotation += turn;

        }

        public void SetMovement(Movement movement)
        {
            movement.CheckAllNumbers();
            move = movement.Move;
            turn = movement.Turn;
            targetTurret = movement.Turret;
        }
    }
}
