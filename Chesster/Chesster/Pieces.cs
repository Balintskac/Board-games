using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesster
{
    abstract class Pieces
    {
        public char Team
        {
            get { return team; }
            set { team = value; }
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        protected int x;
        protected int y;
        public char team;

        public Pieces(int x, int y, char team)
        {
            this.x = x;
            this.y = y;
            this.team = team;
        }

        public abstract List<Pieces> Move(int x, int y, Chess c);
    }
}
