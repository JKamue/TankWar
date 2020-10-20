using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Objects
{
    public class Movement
    {
        public float Move;
        public float Turn;
        public float Turret;

        public Movement(float move, float turn, float turret)
        {
            Move = move;
            Turn = turn;
            Turret = turret;
        }

        private float CheckNumber(float number, float value = 2)
        {
            if (number > 0)
                return value;

            if (number < 0)
                return -value;

            return 0;
        }

        public void CheckAllNumbers()
        {
            Move = CheckNumber(Move, 0.5f);
            Turn = CheckNumber(Turn);
            Turret = CheckNumber(Turret);
        }
    }
}
