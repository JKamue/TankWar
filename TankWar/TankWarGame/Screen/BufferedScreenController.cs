﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            _graphicsBuffer.Graphics.FillRectangle(new SolidBrush(p.Color), new Rectangle(1,1,10,10) );
        }
    }
}
