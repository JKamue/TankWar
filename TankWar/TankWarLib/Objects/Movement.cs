using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Objects
{
    public class Movement
    {
        public int Move;
        public int Turn;
        public int Turret;

        public Movement(int move, int turn, int turret)
        {
            Move = move;
            Turn = turn;
            Turret = turret;
        }

        private int CheckNumber(int number)
        {
            if (number > 0)
                return 1;

            if (number < 0)
                return -1;

            return 0;
        }

        public void CheckAllNumbers()
        {
            Move = CheckNumber(Move);
            Turn = CheckNumber(Turn);
        }
    }
}
