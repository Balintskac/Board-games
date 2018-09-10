using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesster
{
    class King : Pieces
    {
        public bool kingInCheck // Király sakkban van-e (ezt se használom jelenleg)
        {
            get;
            set;
        }

        public King(int x, int y, char team) : base(x, y, team)
        {
            kingInCheck = false;
        }

        public override List<Pieces> Move(int x, int y, Chess c)
        {
            List<Pieces> possMove = new List<Pieces>();
            char team = c.pieces[x, y].Team;
            if(x == 7 && y == 3 && team == 'W')
            {
                if (y + 2 >= 0)
                {
                    if (c.pieces[x, y + 2] is null)
                    {
                        if (c.pieces[7, 7] is Rook)
                        {
                            if (!(c.pieces[7, 7] is null))
                            {
                                if ((c.pieces[7, 7] as Rook).Casting)
                                {
                                    possMove.Add(new King(x, y + 2, c.pieces[x, y].team));
                                }
                            }
                        }
                    }
                }
                if (y - 2 >= 0)
                {
                    if (c.pieces[x, y + 2] is null)
                    {
                        if (c.pieces[7, 7] is Rook)
                        {
                            if (!(c.pieces[7, 0] is null))
                            {
                                if ((c.pieces[7, 0] as Rook).Casting)
                                {
                                    possMove.Add(new King(x, y - 2, c.pieces[x, y].team));
                                }
                            }
                        }
                    }
                }
            }
            if (x - 1 >= 0)
            {
                if (c.pieces[x - 1, y] is null)
                {
                    possMove.Add(new King(x - 1, y, c.pieces[x, y].team));
                }
                else
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x - 1, y].Team == 'B')
                    {
                        possMove.Add(new King(x - 1, y, team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x - 1, y].Team == 'W')
                    {
                        possMove.Add(new King(x - 1, y, team));
                    }
                }
            }
            if (x + 1 < 8)
            {
                if (c.pieces[x + 1, y] is null)
                {
                    possMove.Add(new King(x + 1, y, c.pieces[x, y].team));
                }
                else
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x + 1, y].Team == 'B')
                    {
                        possMove.Add(new King(x + 1, y, team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x + 1, y].Team == 'W')
                    {
                        possMove.Add(new King(x + 1, y, team));
                    }
                }
            }
            if (y + 1 < 8)
            {
                if (c.pieces[x, y + 1] is null)
                {
                    possMove.Add(new King(x, y + 1, c.pieces[x, y].team));
                }
                else
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x , y + 1].Team == 'B')
                    {
                        possMove.Add(new King(x, y + 1, team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x, y + 1].Team == 'W')
                    {
                        possMove.Add(new King(x, y + 1, team));
                    }
                }
            }
            if (y - 1 >= 0)
            {
                if (c.pieces[x, y - 1] is null)
                {
                    possMove.Add(new King(x, y - 1, c.pieces[x, y].team));
                }
                else
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x, y - 1].Team == 'B')
                    {
                        possMove.Add(new King(x, y - 1, team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x, y - 1].Team == 'W')
                    {
                        possMove.Add(new King(x, y - 1, team));
                    }
                }
            }
            if (x + 1 < 8 && y - 1 >= 0)
            {
                if (c.pieces[x + 1, y - 1] is null)
                {
                    possMove.Add(new King(x + 1,y - 1, c.pieces[x, y].team));
                }
                else
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x + 1, y - 1].Team == 'B')
                    {
                        possMove.Add(new King(x + 1, y - 1, team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x + 1, y - 1].Team == 'W')
                    {
                        possMove.Add(new King(x + 1, y - 1, team));
                    }
                }
            }
            if (x + 1 < 8 && y + 1 < 8)
            {
                if (c.pieces[x + 1, y + 1] is null)
                {
                    possMove.Add(new King(x + 1, y + 1, c.pieces[x, y].team));
                }
                else
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x + 1, y + 1].Team == 'B')
                    {
                        possMove.Add(new King(x + 1, y + 1, team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x + 1, y + 1].Team == 'W')
                    {
                        possMove.Add(new King(x + 1, y + 1, team));
                    }
                }
            }
            if (x - 1 >= 0 && y + 1 < 8)
            {
                if (c.pieces[x - 1, y + 1] is null)
                {
                    possMove.Add(new King(x - 1, y + 1, c.pieces[x, y].team));
                }
                else
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x - 1, y + 1].Team == 'B')
                    {
                        possMove.Add(new King(x - 1, y + 1, team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x - 1, y + 1].Team == 'W')
                    {
                        possMove.Add(new King(x - 1, y + 1, team));
                    }
                }
            }
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                if (c.pieces[x - 1, y - 1] is null)
                {
                    possMove.Add(new King(x - 1, y - 1, c.pieces[x, y].team));
                }
                else
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x - 1, y - 1].Team == 'B')
                    {
                        possMove.Add(new King(x - 1, y - 1, team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x - 1, y - 1].Team == 'W')
                    {
                        possMove.Add(new King(x - 1, y - 1, team));
                    }
                }
            }

            return possMove;
        }
    }
}
