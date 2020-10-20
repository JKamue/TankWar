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
        public Map Map = new Map(new List<Line>());

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
            _players.ForEach(p => DrawObject(p));
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

        private void DrawObject(Player p)
        {
            var topLeft = PointRotator.RotatePoint(new PointF(p.Position.X - 8, p.Position.Y - 15), p.Position, p.Rotation-90);
            var topRight = PointRotator.RotatePoint(new PointF(p.Position.X - 8, p.Position.Y + 15), p.Position, p.Rotation - 90);
            var bottomLeft = PointRotator.RotatePoint(new PointF(p.Position.X + 8, p.Position.Y - 15), p.Position, p.Rotation - 90);
            var bottomRight = PointRotator.RotatePoint(new PointF(p.Position.X + 8, p.Position.Y + 15), p.Position, p.Rotation - 90);

            var polygon = (new List<PointF>{topLeft, topRight, bottomRight, bottomLeft}).ToArray();

            _graphicsBuffer.Graphics.FillPolygon(new SolidBrush(p.Color), polygon);

            topLeft = PointRotator.RotatePoint(new PointF(p.Position.X - 4, p.Position.Y - 4), p.Position, p.TurretRotation - 90);
            topRight = PointRotator.RotatePoint(new PointF(p.Position.X - 4, p.Position.Y + 18), p.Position, p.TurretRotation - 90);
            bottomLeft = PointRotator.RotatePoint(new PointF(p.Position.X + 4, p.Position.Y - 4), p.Position, p.TurretRotation - 90);
            bottomRight = PointRotator.RotatePoint(new PointF(p.Position.X + 4, p.Position.Y + 18), p.Position, p.TurretRotation - 90);

            polygon = (new List<PointF> { topLeft, topRight, bottomRight, bottomLeft }).ToArray();

            _graphicsBuffer.Graphics.FillPolygon(new SolidBrush(Color.Gray), polygon);
        }
    }
}
