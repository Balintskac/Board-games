using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesster
{
    class Queen : Pieces
    {
        public Queen(int x, int y, char team) : base(x, y, team)
        {

        }

        public override List<Pieces> Move(int x, int y, Chess c)
        {
            List<Pieces> possMove = new List<Pieces>();

            int ox = x;
            int oy = y;
            char team = c.pieces[x, y].Team;

            //Movement of Bishop

            while (x + 1 < 8 && y + 1 < 8)
            {
                if (c.pieces[x + 1, y + 1] is null)
                {
                    possMove.Add(new Queen(x + 1, y + 1, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x + 1, y + 1].Team == 'B')
                    {
                        possMove.Add(new Queen(x + 1, y + 1, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x + 1, y + 1].Team == 'W')
                    {
                        possMove.Add(new Queen(x + 1, y + 1, team));
                    }
                    x = 6;
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
                    possMove.Add(new Queen(x + 1, y - 1, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x + 1, y - 1].Team == 'B')
                    {
                        possMove.Add(new Queen(x + 1, y - 1, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x + 1, y - 1].Team == 'W')
                    {
                        possMove.Add(new Queen(x + 1, y - 1, team));
                    }
                    x = 6;
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
                    possMove.Add(new Queen(x - 1, y + 1, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x - 1, y + 1].Team == 'B')
                    {
                        possMove.Add(new Queen(x - 1, y + 1, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x - 1, y + 1].Team == 'W')
                    {
                        possMove.Add(new Queen(x - 1, y + 1, team));
                    }
                    x = 1;
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
                    possMove.Add(new Queen(x - 1, y - 1, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x - 1, y - 1].Team == 'B')
                    {
                        possMove.Add(new Queen(x - 1, y - 1, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x - 1, y - 1].Team == 'W')
                    {
                        possMove.Add(new Queen(x - 1, y - 1, team));
                    }
                    x = 1;
                }
                y = y - 1;
                x = x - 1;
            }
            x = ox;
            y = oy;
            // Movement of Rook
            while (x - 1 >= 0)
            {
                if (c.pieces[x - 1, y] is null)
                {
                    possMove.Add(new Queen(x - 1, y, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x - 1, y].Team == 'B')
                    {
                        possMove.Add(new Queen(x - 1, y, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x - 1, y].Team == 'W')
                    {
                        possMove.Add(new Queen(x - 1, y, team));
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
                    possMove.Add(new Queen(x + 1, y, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x + 1, y].Team == 'B')
                    {
                        possMove.Add(new Queen(x + 1, y, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x + 1, y].Team == 'W')
                    {
                        possMove.Add(new Queen(x + 1, y, team));
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
                    possMove.Add(new Queen(x, y - 1, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x, y - 1].Team == 'B')
                    {
                        possMove.Add(new Queen(x, y - 1, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x, y - 1].Team == 'W')
                    {
                        possMove.Add(new Queen(x, y - 1, team));
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
                    possMove.Add(new Queen(x, y + 1, team));
                }
                else
                {
                    if (c.pieces[ox, oy].Team == 'W' && c.pieces[x, y + 1].Team == 'B')
                    {
                        possMove.Add(new Queen(x, y + 1, team));
                    }
                    else if (c.pieces[ox, oy].Team == 'B' && c.pieces[x, y + 1].Team == 'W')
                    {
                        possMove.Add(new Queen(x, y + 1, team));
                    }
                    y = 6;
                }
                y = y + 1;
            }
            return possMove;
        }
    }
}
