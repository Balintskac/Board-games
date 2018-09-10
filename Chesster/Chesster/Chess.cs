using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesster
{
    class Chess
    {
        public Pieces[,] pieces; // Eltárolja osztály szinten mi hol van a táblán
        public Pieces Attacker; // Ellenfél bábuja ha sakkot ad
        public List<Pieces> opponentSteps;

        public Chess(int size)
        {
            opponentSteps = new List<Pieces>();
            // Black pieces 
            pieces = new Pieces[size, size];
            for (int i = 0; i < size; i++)
            {
                pieces[1, i] = new Pawn(1, i, 'B');
            }

            pieces[0, 0] = new Rook(0, 0, 'B');
            pieces[0, 7] = new Rook(0, 7, 'B');
            pieces[0, 1] = new Knight(0, 1, 'B');
            pieces[0, 6] = new Knight(0, 6, 'B');
            pieces[0, 2] = new Bishop(0, 2, 'B');
            pieces[0, 5] = new Bishop(0, 5, 'B');
            pieces[0, 4] = new Queen(0, 4, 'B');
            pieces[0, 3] = new King(0, 3, 'B');

            // White pieces 

            for (int i = 0; i < size; i++)
            {
               pieces[6, i] = new Pawn(1, i, 'W');
            }

            pieces[7, 0] = new Rook(7, 0, 'W');
            pieces[7, 7] = new Rook(7, 7, 'W');
            pieces[7, 1] = new Knight(7, 1, 'W');
            pieces[7, 6] = new Knight(7, 6, 'W');
            pieces[7, 2] = new Bishop(7, 2, 'W');
            pieces[7, 5] = new Bishop(7, 5, 'W');
            pieces[7, 4] = new Queen(7, 4, 'W');
            pieces[7, 3] = new King(7, 3, 'W');
        }
     
        // Sakk van-e
        public bool Kingchecked(List<Pieces> isCheck,Chess c,bool turn)
        {
                for (int i = 0; i < isCheck.Count; i++)
                {
                    if (pieces[isCheck[i].X, isCheck[i].Y] is King && pieces[isCheck[i].X, isCheck[i].Y].Team == 'W' && turn)
                    {
                        return true;
                    }
                    else if (pieces[isCheck[i].X, isCheck[i].Y] is King && pieces[isCheck[i].X, isCheck[i].Y].Team == 'B' && !turn)
                    {
                        return true;
                    }
                }
            
            return false;
        }

        // Ellenfél összes bábuját megnézzük
        public List<Pieces> IsNotChecked(Chess c, bool turn)
        {
            opponentSteps.Clear();
            List<Pieces> Attackers = new List<Pieces>();
            int j = 0;
            for (int i = 0; i < c.pieces.GetLength(0); i++)
            {
                for (int k = 0; k < c.pieces.GetLength(1); k++)
                {
                    if (!(c.pieces[i, k] is null))
                    {
                        if (c.pieces[i, k].Team == 'B' && turn)
                        {
                            if (pieces[i, k] is Pawn)
                            {
                                if (i + 1 < 8 && k + 1 < 8)
                                {
                                   Attackers.Add(new Pawn(i + 1, k + 1, pieces[i, k].Team));
                                }
                                if (i + 1 < 8 && k - 1 >= 0)
                                {
                                   Attackers.Add(new Pawn(i + 1, k - 1, pieces[i, k].Team));
                                }
                            }
                            else
                            {
                                Attackers = c.pieces[i, k].Move(i, k, c);
                            }
                            for (j = 0; j < Attackers.Count; j++)
                            {
                                if (c.pieces[Attackers[j].X, Attackers[j].Y] is King && c.pieces[Attackers[j].X, Attackers[j].Y].Team == 'W')
                                {
                                    if (Attackers[j] is Pawn)
                                    {
                                        Attacker = new Pawn(i, k, Attackers[j].Team);
                                    }
                                    else if (Attackers[j] is Rook)
                                    {
                                        Attacker = new Rook(i, k, Attackers[j].Team);
                                    }
                                    else if (Attackers[j] is Knight)
                                    {
                                        Attacker = new Knight(i, k, Attackers[j].Team);

                                    }
                                    else if (Attackers[j] is Bishop)
                                    {
                                        Attacker = new Bishop(i, k, Attackers[j].Team);

                                    }
                                    else if (Attackers[j] is Queen)
                                    {
                                        Attacker = new Queen(i, k, Attackers[j].Team);
                                    }
                                }
                                if (Attackers[j] is Rook)
                                {
                                    opponentSteps.Add(new Rook(Attackers[j].X, Attackers[j].Y, Attackers[j].Team));

                                }
                                else if (Attackers[j] is Knight)
                                {
                                    opponentSteps.Add(new Knight(Attackers[j].X, Attackers[j].Y, Attackers[j].Team));

                                }
                                else if (Attackers[j] is Bishop)
                                {
                                    opponentSteps.Add(new Bishop(Attackers[j].X, Attackers[j].Y, Attackers[j].Team));

                                }
                                else if (Attackers[j] is Queen)
                                {
                                    opponentSteps.Add(new Queen(Attackers[j].X, Attackers[j].Y, Attackers[j].Team));

                                }
                                else if (Attackers[j] is King)
                                {
                                    opponentSteps.Add(new King(Attackers[j].X, Attackers[j].Y, Attackers[j].Team));

                                }
                                else if(Attackers[j] is Pawn)
                                {
                                    opponentSteps.Add(new Pawn(Attackers[j].X, Attackers[j].Y, Attackers[j].Team));

                                }
                            }
                        }
                        else if (c.pieces[i, k].Team == 'W' && !turn)
                        {
                            if (pieces[i, k] is Pawn)
                            {
                                if (i - 1 >= 0 && k + 1 < 8)
                                {
                                    Attackers.Add(new Pawn(i - 1, k + 1, pieces[i, k].Team));
                                }
                                if (i - 1 >= 0 && k - 1 >= 0)
                                {
                                    Attackers.Add(new Pawn(i - 1, k - 1, pieces[i, k].Team));
                                }
                            }
                            else
                            {
                                Attackers = c.pieces[i, k].Move(i, k, c);
                            }

                            for (j = 0; j < Attackers.Count; j++)
                            {
                                if (c.pieces[Attackers[j].X, Attackers[j].Y] is King && c.pieces[Attackers[j].X, Attackers[j].Y].Team == 'B')
                                {
                                    if (Attackers[j] is Pawn)
                                    {
                                        Attacker = new Pawn(i, k, Attackers[j].Team);
                                    }
                                    else if (Attackers[j] is Rook)
                                    {
                                        Attacker = new Rook(i, k, Attackers[j].Team);
                                    }
                                    else if (Attackers[j] is Knight)
                                    {
                                        Attacker = new Knight(i, k, Attackers[j].Team);

                                    }
                                    else if (Attackers[j] is Bishop)
                                    {
                                        Attacker = new Bishop(i, k, Attackers[j].Team);

                                    }
                                    else if (Attackers[j] is Queen)
                                    {
                                        Attacker = new Queen(i, k, Attackers[j].Team);
                                    }
                                }
                                if (Attackers[j] is Pawn)
                                {
                                    opponentSteps.Add(new Pawn(Attackers[j].X, Attackers[j].Y, Attackers[j].Team));
                                }
                                else if (Attackers[j] is Rook)
                                {
                                    opponentSteps.Add(new Rook(Attackers[j].X, Attackers[j].Y, Attackers[j].Team));

                                }
                                else if (Attackers[j] is Knight)
                                {
                                    opponentSteps.Add(new Knight(Attackers[j].X, Attackers[j].Y, Attackers[j].Team));

                                }
                                else if (Attackers[j] is Bishop)
                                {
                                    opponentSteps.Add(new Bishop(Attackers[j].X, Attackers[j].Y, Attackers[j].Team));

                                }
                                else if (Attackers[j] is Queen)
                                {
                                    opponentSteps.Add(new Queen(Attackers[j].X, Attackers[j].Y, Attackers[j].Team));

                                }
                                else if (Attackers[j] is King)
                                {
                                    opponentSteps.Add(new King(Attackers[j].X, Attackers[j].Y, Attackers[j].Team));

                                }
                            }
                        }
                    }
                }
            }
            return opponentSteps;
        }
    }
}
