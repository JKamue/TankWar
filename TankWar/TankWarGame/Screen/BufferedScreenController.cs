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
        public List<Explosion> _explosions = new List<Explosion>();
        public Map Map = new Map(new List<Line>(), Size.Empty, 0, new List<PointF>());

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

        public void Close()
        {
            _timer.Stop();
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
            _explosions.ForEach(DrawObject);
            _graphicsBuffer.Render(_panelGraphics);
        }

        private void DrawObject(Explosion explosion)
        {
            _graphicsBuffer.Graphics.FillEllipse(new SolidBrush(explosion.Color), new RectangleF(explosion.Position, explosion.Size));
        }

        private void DrawObject(Map m)
        {
            m.Lines.ForEach(DrawObject);
        }

        private void DrawObject(Line l)
        {
            _graphicsBuffer.Graphics.DrawLine(new Pen(l.Color, 2), l.Start, l.End);
        }

        private void DrawObject(Bullet b)
        {
            _graphicsBuffer.Graphics.FillEllipse(new SolidBrush(Color.Black), b.Position.X-2, b.Position.Y-2, 4,4);
        }

        private void DrawObject(Player p)
        {
            _graphicsBuffer.Graphics.FillPolygon(new SolidBrush(p.Color), p.GetBodyPolygon().ToArray());
            _graphicsBuffer.Graphics.FillPolygon(new SolidBrush(Color.Gray), p.GetTurretPolygon().ToArray());
        }
    }
}
