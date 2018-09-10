using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesster
{
    class Knight : Pieces
    {


        public Knight(int x, int y, char team) : base(x, y, team)
        {
        }

        public override List<Pieces> Move(int x, int y, Chess c)
        {
            List<Pieces> possMove = new List<Pieces>();
            if (x - 2 >= 0 && y - 1 >= 0)
            {
                if (!(c.pieces[x - 2, y - 1] is null))
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x - 2, y - 1].Team == 'B')
                    {
                        possMove.Add(new Knight(x - 2, y - 1, c.pieces[x, y].Team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x - 2, y - 1].Team == 'W')
                    {
                        possMove.Add(new Knight(x - 2, y - 1, c.pieces[x, y].Team));
                    }
                }
                else
                {
                    possMove.Add(new Knight(x - 2, y - 1, c.pieces[x, y].Team));
                }
            }
            if (x - 1 >= 0 && y - 2 >= 0)
            {
                if (!(c.pieces[x - 1, y - 2] is null))
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x - 1, y - 2].Team == 'B')
                    {
                        possMove.Add(new Knight(x - 1, y - 2, c.pieces[x, y].Team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x - 1, y - 2].Team == 'W')
                    {
                        possMove.Add(new Knight(x - 1, y - 2, c.pieces[x, y].Team));
                    }
                   
                   
                }
                else
                {
                    possMove.Add(new Knight(x - 1, y - 2, c.pieces[x, y].Team));
                }
            }
            if (x + 1 < 8 && y - 2 >= 0)
            {
                if (!(c.pieces[x + 1, y - 2] is null))
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x + 1, y - 2].Team == 'B')
                    {
                        possMove.Add(new Knight(x + 1, y - 2, c.pieces[x, y].Team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x + 1, y - 2].Team == 'W')
                    {
                        possMove.Add(new Knight(x + 1, y - 2, c.pieces[x, y].Team));
                    }
                   
                }
                else
                {
                    possMove.Add(new Knight(x + 1, y - 2, c.pieces[x, y].Team));
                }
            }
            if (x + 2 < 8 && y - 1 >= 0)
            {
                if (!(c.pieces[x + 2, y - 1] is null))
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x + 2, y - 1].Team == 'B')
                    {
                        possMove.Add(new Knight(x + 2, y - 1, c.pieces[x, y].Team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x + 2, y - 1].Team == 'W')
                    {
                        possMove.Add(new Knight(x + 2, y - 1, c.pieces[x, y].Team));
                    }
                }
                else
                {
                    possMove.Add(new Knight(x + 2, y - 1, c.pieces[x, y].Team));
                }
            }
            if (x - 2 >= 0 && y + 1 < 8)
            {
                if (!(c.pieces[x - 2, y + 1] is null))
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x - 2, y + 1].Team == 'B')
                    {
                        possMove.Add(new Knight(x - 2, y + 1, c.pieces[x, y].Team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x - 2, y + 1].Team == 'W')
                    {
                        possMove.Add(new Knight(x - 2, y + 1, c.pieces[x, y].Team));
                    }
                }
                else
                {
                    possMove.Add(new Knight(x - 2, y + 1, c.pieces[x, y].Team));
                }
            }
            if (x - 1 >= 0 && y + 2 < 8)
            {
                if (!(c.pieces[x - 1, y + 2] is null))
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x - 1, y + 2].Team == 'B')
                    {
                        possMove.Add(new Knight(x - 1, y + 2, c.pieces[x, y].Team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x - 1, y + 2].Team == 'W')
                    {
                        possMove.Add(new Knight(x - 1, y + 2, c.pieces[x, y].Team));
                    }
                }
                else
                {
                    possMove.Add(new Knight(x - 1, y + 2, c.pieces[x, y].Team));
                }
            }
            if (x + 1 < 8 && y + 2 < 8)
            {
                if (!(c.pieces[x + 1, y + 2] is null))
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x + 1, y + 2].Team == 'B')
                    {
                        possMove.Add(new Knight(x + 1, y + 2, c.pieces[x, y].Team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x + 1, y + 2].Team == 'W')
                    {
                        possMove.Add(new Knight(x + 1, y + 2, c.pieces[x, y].Team));
                    }
                }
                else
                {
                    possMove.Add(new Knight(x + 1, y + 2, c.pieces[x, y].Team));
                }
            }
            if (x + 2 < 8 && y + 1 < 8)
            {
                if (!(c.pieces[x + 2, y + 1] is null))
                {
                    if (c.pieces[x, y].Team == 'W' && c.pieces[x + 2, y + 1].Team == 'B')
                    {
                        possMove.Add(new Knight(x + 2, y + 1, c.pieces[x, y].Team));
                    }
                    else if (c.pieces[x, y].Team == 'B' && c.pieces[x + 2, y + 1].Team == 'W')
                    {
                        possMove.Add(new Knight(x + 2, y + 1, c.pieces[x, y].Team));
                    }
                }
                else
                {
                    possMove.Add(new Knight(x + 2, y + 1, c.pieces[x, y].Team));
                }
            }

            return possMove;
        }
    }
}
