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

        private PointF _spawnPosition;

        private float move = 0;
        private float turn = 0;
        private float turret = 0;

        private int tick = 0;
        private int stunnedTick = -1;

        public Player(string id, PointF position, Color color)
        {
            Id = id;
            Position = position;
            Color = color;
            _spawnPosition = position;
        }

        public void MarkHit()
        {
            stunnedTick = tick + 240;
        }
        public bool IntersectsWith(List<Line> lines)
        {
            foreach (var line in lines)
            {
                foreach (var mapLine in GetBodyLines())
                {
                    if (mapLine.GetIntersection(line) != new Point(-100, -100))
                        return true;
                }
            }

            return false;
        }

        public List<PointF> GetBodyPolygon()
        {
            var topLeft = PointRotator.RotatePoint(new PointF(Position.X - 8, Position.Y - 15), Position, Rotation);
            var topRight = PointRotator.RotatePoint(new PointF(Position.X - 8, Position.Y + 15), Position, Rotation);
            var bottomLeft = PointRotator.RotatePoint(new PointF(Position.X + 8, Position.Y - 15), Position, Rotation);
            var bottomRight = PointRotator.RotatePoint(new PointF(Position.X + 8, Position.Y + 15), Position, Rotation);

            return new List<PointF> {topLeft, topRight, bottomRight, bottomLeft};
        }

        public List<Line> GetBodyLines()
        {
            var polygon = GetBodyPolygon();
            return new List<Line>
            {
                new Line(Color.White, polygon[0], polygon[1]),
                new Line(Color.White, polygon[1], polygon[2]),
                new Line(Color.White, polygon[2], polygon[3]),
                new Line(Color.White, polygon[3], polygon[0])
            };
        }

        public List<PointF> GetTurretPolygon()
        {
            var topLeft = PointRotator.RotatePoint(new PointF(Position.X - 4, Position.Y - 4), Position, TurretRotation);
            var topRight = PointRotator.RotatePoint(new PointF(Position.X - 4, Position.Y + 18), Position, TurretRotation);
            var bottomLeft = PointRotator.RotatePoint(new PointF(Position.X + 4, Position.Y - 4), Position, TurretRotation);
            var bottomRight = PointRotator.RotatePoint(new PointF(Position.X + 4, Position.Y + 18), Position, TurretRotation);

            return new List<PointF> { topLeft, topRight, bottomRight, bottomLeft };
        }

        public List<Line> GetHitboxLines()
        {
            var polygon = GetBodyPolygon();
            return new List<Line>
            {
                new Line(Color.Red, polygon[0], polygon[2]),
                new Line(Color.Red, polygon[1], polygon[3])
            };
        }

        public void Tick()
        {
            tick++;

            if (stunnedTick > tick)
                return;

            if (stunnedTick == tick)
                Position = _spawnPosition;

            var positionVector = PointRotator.RotatePoint(new PointF(0, move), new PointF(0, 0), Rotation);
            Position = new PointF(Position.X + positionVector.X, Position.Y + positionVector.Y);
            Rotation += turn;
            TurretRotation += turret;
        }

        public void BackTick()
        {
            var positionVector = PointRotator.RotatePoint(new PointF(0, move), new PointF(0, 0), Rotation);
            Position = new PointF(Position.X - positionVector.X, Position.Y - positionVector.Y);
            Rotation -= turn;
            TurretRotation -= turret;
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
