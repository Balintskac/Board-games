using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesster
{
    class Pawn : Pieces
    {
       public bool StepTwo
        {
            get;
            set;
        }


        public Pawn(int x, int y, char team) : base(x, y, team)
        {
            StepTwo = false;
        }


        public override List<Pieces> Move(int x, int y, Chess c)
        {
            List<Pieces> possMove = new List<Pieces>();
            if (c.pieces[x,y].Team == 'W')
            {
                if (x - 1 >= 0)
                {
                    if (x == 3)
                    {
                        if (!(c.pieces[x, y - 1] is null))
                        {
                            if (c.pieces[3, y - 1].Team == 'B' && c.pieces[3, y - 1] is Pawn)
                            {
                                if ((c.pieces[3, y - 1] as Pawn).StepTwo)
                                {
                                    possMove.Add(new Pawn(x - 1, y - 1, c.pieces[x, y].Team));
                                }
                            }
                        }
                    }
                    if (x == 3)
                    {
                        if (!(c.pieces[x, y + 1] is null))
                        {
                            if (c.pieces[3, y + 1].Team == 'B' && c.pieces[3, y + 1] is Pawn)
                            {
                                if ((c.pieces[3, y + 1] as Pawn).StepTwo)
                                {
                                    possMove.Add(new Pawn(x - 1, y + 1, c.pieces[x, y].Team));
                                }
                            }
                        }
                    }

                    if (c.pieces[x - 1, y] is null)
                    {
                        possMove.Add(new Pawn(x - 1, y, c.pieces[x, y].Team));
                    }
                    if (x == 6)
                    {
                        if (c.pieces[x - 2, y] is null && c.pieces[x - 1, y] is null)
                        {
                            possMove.Add(new Pawn(x - 2, y, c.pieces[x, y].Team));
                        }
                    }
                    if (y - 1 >= 0)
                    {
                        if (!(c.pieces[x - 1, y - 1] is null))
                        {
                            if (c.pieces[x - 1, y - 1].Team == 'B')
                            {
                                possMove.Add(new Pawn(x - 1, y - 1, c.pieces[x, y].Team));
                            }

                        }
                    }
                    if (y + 1 < 8)
                    {
                        if (!(c.pieces[x - 1, y + 1] is null))
                        {
                            if (c.pieces[x - 1, y + 1].Team == 'B')
                            {
                                possMove.Add(new Pawn(x - 1, y + 1, c.pieces[x, y].Team));
                            }
                        }
                    }
                }
            }
            else
            {
                if (x + 1 < 8)
                {
                    if (c.pieces[x + 1, y] is null)
                    {
                        possMove.Add(new Pawn(x + 1, y, c.pieces[x, y].Team));
                    }
                    if (x == 4)
                    {
                        if (!(c.pieces[x, y - 1] is null))
                        {
                            if (c.pieces[x, y - 1].Team == 'W' && c.pieces[x, y - 1] is Pawn)
                            {
                                if ((c.pieces[x, y - 1] as Pawn).StepTwo)
                                {
                                    possMove.Add(new Pawn(x - 1, y - 1, c.pieces[x, y].Team));
                                }
                            }
                        }
                    }
                    if (x == 4)
                    {
                        if (!(c.pieces[x, y + 1] is null))
                        {
                            if (c.pieces[4, y + 1].Team == 'W' && c.pieces[4, y + 1] is Pawn)
                            {
                                if ((c.pieces[4, y + 1] as Pawn).StepTwo)
                                {
                                    possMove.Add(new Pawn(x - 1, y + 1, c.pieces[x, y].Team));
                                }
                            }
                        }
                    }

                    if (x == 1)
                    {
                        if (c.pieces[x + 2, y] is null && c.pieces[x + 1, y] is null)
                        {
                            possMove.Add(new Pawn(x + 2, y, c.pieces[x, y].Team));
                        }
                    }
                    if (y - 1 >= 0)
                    {
                        if (!(c.pieces[x + 1, y - 1] is null))
                        {
                            if (c.pieces[x + 1, y - 1].Team == 'W')
                            {
                                possMove.Add(new Pawn(x + 1, y - 1, c.pieces[x, y].Team));
                            }
                        }
                    }
                    if(y + 1 < 8)
                    { 
                        if (!(c.pieces[x + 1, y + 1] is null))
                        {
                            if (c.pieces[x + 1, y + 1].Team == 'W')
                            {
                                possMove.Add(new Pawn(x + 1, y + 1, c.pieces[x, y].Team));
                            }
                        }
                    }
                }
            }
            return possMove;
        }
    }
}
