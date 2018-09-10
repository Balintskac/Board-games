using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesster
{
    class Bishop : Pieces
    {
        List<Pieces> possMove = new List<Pieces>();
        public Bishop(int x, int y, char team) : base(x, y, team)
        {
        }

        public override List<Pieces> Move(int x, int y, Chess c)
        {
            int ox = x;
            int oy = y;
            char team = c.pieces[x, y].Team;
            while (x + 1 < 8 && y + 1 < 8)
            {
                if (c.pieces[x + 1, y + 1] is null)
                {
                    possMove.Add(new Bishop(x + 1, y + 1, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x + 1, y + 1].Team == 'B')
                    {
                        possMove.Add(new Bishop(x + 1, y + 1, team));

                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x + 1, y + 1].Team == 'W')
                    {
                        possMove.Add(new Bishop(x + 1, y + 1, team));
                    }
                    x = 8;
                    y = 8;
                }
                y = y + 1;
                x = x + 1;
            }
            x = ox;
            y = oy;
            while (x + 1 < 8 && y - 1 >= 0)
            {
                if (c.pieces[x + 1, y - 1] is null)
                {
                    possMove.Add(new Bishop(x + 1, y - 1, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x + 1, y - 1].Team == 'B')
                    {
                        possMove.Add(new Bishop(x + 1, y - 1, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x + 1, y - 1].Team == 'W')
                    {
                        possMove.Add(new Bishop(x + 1, y - 1, team));
                    }
                    x = 8;

                }
                y = y - 1;
                x = x + 1;
            }
            x = ox;
            y = oy;
            while (x - 1 >= 0 && y + 1 < 8)
            {
                if (c.pieces[x - 1, y + 1] is null)
                {
                    possMove.Add(new Bishop(x - 1, y + 1, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x - 1, y + 1].Team == 'B')
                    {
                        possMove.Add(new Bishop(x - 1, y + 1, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x - 1, y + 1].Team == 'W')
                    {
                        possMove.Add(new Bishop(x - 1, y + 1, team));
                    }
                    x = 0;
                }
                y = y + 1;
                x = x - 1;
            }
            x = ox;
            y = oy;
            while (x - 1 >= 0 && y - 1 >= 0)
            {
                if (c.pieces[x - 1, y - 1] is null)
                {
                    possMove.Add(new Bishop(x - 1, y - 1, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x - 1, y - 1].Team == 'B')
                    {
                        possMove.Add(new Bishop(x - 1, y - 1, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x - 1, y - 1].Team == 'W')
                    {
                        possMove.Add(new Bishop(x - 1, y - 1, team));
                    }
                    x = 0;
                }
                y = y - 1;
                x = x - 1;
            }
            return possMove; 
        }
    }
}
