using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesster
{
    class Rook : Pieces
    {
        public bool Casting // Rosáláskor, léptünk már el vele
        {
            get;
            set;
        }
        public Rook(int x, int y, char team) : base(x, y, team)
        {
            Casting = true;
        }
    
        public override List<Pieces> Move(int x, int y, Chess c)
        {
            List<Pieces> possMove = new List<Pieces>();
            int ox = x;
            int oy = y;
            char team = c.pieces[x, y].Team;
            while (x - 1 >= 0)
            {
                if (c.pieces[x - 1, y] is null)
                {
                    possMove.Add(new Rook(x - 1, y, team));
                } else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x - 1, y].Team == 'B')
                    {
                        possMove.Add(new Rook(x - 1, y, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x - 1, y].Team == 'W')
                    {
                        possMove.Add(new Rook(x - 1, y, team));
                    }
                    x = 1;
                }
                   
                x = x - 1;
            }
            x = ox;
            y = oy;
            while (x + 1 < 8)
            {
                if (c.pieces[x + 1, y] is null)
                {
                    possMove.Add(new Rook(x + 1, y, team));
                } else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x + 1, y].Team == 'B')
                    {
                        possMove.Add(new Rook(x + 1, y, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x + 1, y].Team == 'W')
                    {
                        possMove.Add(new Rook(x + 1, y, team));
                    }
                    x = 6;
                }
                x = x + 1;
            }
            x = ox;
            y = oy;
            while (y - 1 >= 0)
            {
                if (c.pieces[x, y - 1] is null)
                {
                    possMove.Add(new Rook(x, y - 1, team));
                } else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x, y - 1].Team == 'B')
                    {
                        possMove.Add(new Rook(x, y - 1, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x, y - 1].Team == 'W')
                    {
                        possMove.Add(new Rook(x, y - 1, team));
                    }
                    y = 1;
                }
                y = y - 1;
            }
            x = ox;
            y = oy;
            while (y + 1 < 8)
            {
                if (c.pieces[x, y + 1] is null)
                {
                    possMove.Add(new Rook(x, y + 1, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x, y + 1].Team == 'B')
                    {
                        possMove.Add(new Rook(x, y + 1, team));
                    }
                    else if(c.pieces[ox, oy].Team == 'B' && c.pieces[x, y + 1].Team == 'W')
                    {
                        possMove.Add(new Rook(x, y + 1, team));
                    }
                    y = 6;
                }
                y = y + 1;
            }
            return possMove;
        }
    }
}
