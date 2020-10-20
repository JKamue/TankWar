using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Objects
{
    public class Explosion
    {
        public PointF Position;
        public Size Size;
        public Color Color;
        private int tick;
        private PointF CenterPosition;

        public Explosion(PointF position)
        {
            tick = 0;
            Size = new Size(1,1);
            Position = position;
            CenterPosition = position;
            Color = Color.Yellow;
        }

        public bool EndOfLife() => tick > 80;

        private void SetSize(int size)
        {
            Size = new Size(size, size);
            Position = new PointF(CenterPosition.X - (float) size / 2, CenterPosition.Y - (float) size / 2);
        }
         
        public void Tick()
        {
            tick++;
            if (tick > 10 && tick < 15)
            {
                SetSize(8);
            }

            if (tick > 15 && tick < 20)
            {
                SetSize(14);
                Color = Color.Orange;
            }
            else if (tick > 20 && tick < 30)
            {
                SetSize(16);
            }
            else if (tick > 30 && tick < 40)
            {
                Color = Color.DarkOrange;
                SetSize(18);
            }
            else if (tick > 40 && tick < 55)
            {
                Color = Color.Red;
            }
            else if (tick > 55 && tick < 60)
            {
                Color = Color.DarkRed;
            }
            else if (tick > 60 && tick < 65)
            {
                Color = Color.Black;
                SetSize(15);
            }
            else if (tick > 65 && tick < 70)
            {
                SetSize(8);
            }
            else if (tick > 70)
            {
                SetSize(4);
            }
        }
    }
}
