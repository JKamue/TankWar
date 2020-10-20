using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TankWarLib;
using TankWarLib.Objects;

namespace TankWarGame.Screen
{
    public class BufferedScreenController
    {
        private readonly Panel _panel;
        private readonly Timer _timer;
        private readonly Color _color;

        private BufferedGraphicsContext _context;
        private BufferedGraphics _graphicsBuffer;
        private Graphics _panelGraphics;

        public List<Player> _players = new List<Player>();
        public List<Bullet> _bullets = new List<Bullet>();
        public Map Map = new Map(new List<Line>(), Size.Empty);

        public BufferedScreenController(Panel panel, Color color)
        {
            _panel = panel;
            _color = color;

            SetupGraphics();

            // Setup Timer
            _timer = new Timer();
            _timer.Interval = 1000 / 60;
            _timer.Tick += Redraw;
            _timer.Start();

            _panel.SizeChanged += PanelResizeEvent;
        }

        public void PanelResizeEvent(object sender, EventArgs e) => SetupGraphics();

        public void SetupGraphics()
        {
            _context = BufferedGraphicsManager.Current;
            _panelGraphics = _panel.CreateGraphics();
            _graphicsBuffer = _context.Allocate(_panelGraphics, _panel.DisplayRectangle);
        }

        public void Redraw(object sender, EventArgs e)
        {
            _graphicsBuffer.Graphics.Clear(_color);
            DrawObject(Map);
            _players.ForEach(DrawObject);
            _bullets.ForEach(DrawObject);
            _graphicsBuffer.Render(_panelGraphics);
        }

        private void DrawObject(Map m)
        {
            m.Lines.ForEach(DrawObject);
        }

        private void DrawObject(Line l)
        {
            _graphicsBuffer.Graphics.DrawLine(new Pen(Color.Black), l.Start, l.End);
        }

        private void DrawObject(Bullet b)
        {
            _graphicsBuffer.Graphics.FillEllipse(new SolidBrush(Color.Black), b.Position.X, b.Position.Y, 5,5);
        }

        private void DrawObject(Player p)
        {
            var topLeft = PointRotator.RotatePoint(new PointF(p.Position.X - 8, p.Position.Y - 15), p.Position, p.Rotation);
            var topRight = PointRotator.RotatePoint(new PointF(p.Position.X - 8, p.Position.Y + 15), p.Position, p.Rotation);
            var bottomLeft = PointRotator.RotatePoint(new PointF(p.Position.X + 8, p.Position.Y - 15), p.Position, p.Rotation);
            var bottomRight = PointRotator.RotatePoint(new PointF(p.Position.X + 8, p.Position.Y + 15), p.Position, p.Rotation);

            var polygon = (new List<PointF>{topLeft, topRight, bottomRight, bottomLeft}).ToArray();

            _graphicsBuffer.Graphics.FillPolygon(new SolidBrush(p.Color), polygon);

            topLeft = PointRotator.RotatePoint(new PointF(p.Position.X - 4, p.Position.Y - 4), p.Position, p.TurretRotation);
            topRight = PointRotator.RotatePoint(new PointF(p.Position.X - 4, p.Position.Y + 18), p.Position, p.TurretRotation);
            bottomLeft = PointRotator.RotatePoint(new PointF(p.Position.X + 4, p.Position.Y - 4), p.Position, p.TurretRotation);
            bottomRight = PointRotator.RotatePoint(new PointF(p.Position.X + 4, p.Position.Y + 18), p.Position, p.TurretRotation);

            polygon = (new List<PointF> { topLeft, topRight, bottomRight, bottomLeft }).ToArray();

            _graphicsBuffer.Graphics.FillPolygon(new SolidBrush(Color.Gray), polygon);
        }
    }
}
