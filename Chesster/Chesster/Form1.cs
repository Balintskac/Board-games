using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Chesster
{
    
    public partial class Form1 : Form
    {
        private int n,size; // 8*8-as tábla n változó tárolja
        private  int s; // csak a gombok elrendezéséhez változó, növeljük az értékét
        private  int x; // paraszt amelyik a tábla túlsó végén van X koordinátáját tároljuk el
        private  int y; // paraszt amelyik a tábla túlsó végén van Y koordinátáját tároljuk el
        private static List<Pieces> PossibleMoves = new List<Pieces>(); // Lehetséges lépések Pieces osztály típusokat tárol el
        private static Chess c; // Chess osztály példánya, innen hívjuk meg a főbb elemeket
        private static Button[,]buttons; // Gombokat ebben tároljuk
        private static Pieces bFrom; // Honnan lépünk
        private static bool turns; // Melyik csapat jön 
        private static Gamefile g;
        private static string[,] match = new string [200,7];
        private static int matchi;

        public Form1() // Inicializáljuk a változókat és kirajzoljuk a táblát
        {
            InitializeComponent();
            turns = true;
            this.n = 8;
            c = new Chess(n);
            this.size = 476 / this.n;
            Game_Field(0, 0, 0);
            c.IsNotChecked(c, true);
            AddtoButtons();
        }

        public Form1(string filename) // Inicializáljuk a változókat és kirajzoljuk a táblát
        {
            InitializeComponent();
            turns = true;
            this.n = 8;
            c = new Chess(n);
            this.size = panel1.Width / this.n;
            Game_Field(0, 0, 0);
            c.IsNotChecked(c, true);
            AddtoButtons();
            g = new Gamefile(filename);
            string[,] load = g.LoadGame(filename);

            for (int i = 0; i < load.GetLength(0); i++)
            {
                if (load[i, 0] == "Chesster.Pawn")
                {
                    bFrom = new Pawn(int.Parse(load[i, 2]), int.Parse(load[i, 3]), char.Parse(load[i, 1]));
                }
                else if (load[i, 0] == "Chesster.Knight")
                {
                    bFrom = new Knight(int.Parse(load[i, 2]), int.Parse(load[i, 3]), char.Parse(load[i, 1]));
                }
                else if (load[i, 0] == "Chesster.Rook")
                {
                    bFrom = new Rook(int.Parse(load[i, 2]), int.Parse(load[i, 3]), char.Parse(load[i, 1]));
                }
                else if (load[i, 0] == "Chesster.Bishop")
                {
                    bFrom = new Bishop(int.Parse(load[i, 2]), int.Parse(load[i, 3]), char.Parse(load[i, 1]));
                }
                else if (load[i, 0] == "Chesster.Queen")
                {
                    bFrom = new Queen(int.Parse(load[i, 2]), int.Parse(load[i, 3]), char.Parse(load[i, 1]));
                }
                else if (load[i, 0] == "Chesster.King")
                {
                    bFrom = new King(int.Parse(load[i, 2]), int.Parse(load[i, 3]), char.Parse(load[i, 1]));
                }

                Step(int.Parse(load[i, 4]), int.Parse(load[i, 5]));
            }
        }
        // Gombokat hozzáadjuk a táblához
        private void AddtoButtons()
        {        
            int o = 0;
            buttons = new Button[8,8];

            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {

                    var button = panel1.Controls.OfType<Button>().ElementAt<object>(o);
                    Button b = button as Button;
                    this.x = i;
                    this.y = j;
                    buttons[i, j] = b;
                    o++;
                }
            }
            o = 0;
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (i == 1)
                    {
                        var button = panel1.Controls.OfType<Button>().ElementAt<object>(o);
                        Button b = button as Button;
                        b.Image = Image.FromFile("Pawn_B.jpg");
                        b.Image = new Bitmap(b.Image, new Size(32, 32));
                        this.x = i;
                        this.y = j;
                        buttons[i, j] = b;
        
                    }
                    if (i == 6)
                    {
                        var button = panel1.Controls.OfType<Button>().ElementAt<object>(o);
                        Button b = button as Button;
                        b.Image = Image.FromFile("Pawn.jpg");
                        b.Image = new Bitmap(b.Image, new Size(32, 32));
                        this.x = i;
                        this.y = j;
                        buttons[i, j] = b;
                    }
                    buttons[0, 0].Image = Image.FromFile("Rook_B.jpg");
                    buttons[0, 0].Image = new Bitmap(buttons[0, 0].Image, new Size(32, 32));
                    buttons[0, 7].Image = Image.FromFile("Rook_B.jpg");
                    buttons[0, 7].Image = new Bitmap(buttons[0, 7].Image, new Size(32, 32));

                    buttons[7, 0].Image = Image.FromFile("Rook_W.jpg");
                    buttons[7, 0].Image = new Bitmap(buttons[7, 0].Image, new Size(32, 32));
                    buttons[7, 7].Image = Image.FromFile("Rook_W.jpg");
                    buttons[7, 7].Image = new Bitmap(buttons[7, 7].Image, new Size(32, 32));

                    buttons[0, 1].Image = Image.FromFile("Knight_B.jpg");
                    buttons[0, 1].Image = new Bitmap(buttons[0, 1].Image, new Size(32, 32));
                    buttons[0, 6].Image = Image.FromFile("Knight_B.jpg");
                    buttons[0, 6].Image = new Bitmap(buttons[0, 6].Image, new Size(32, 32));

                    buttons[7, 1].Image = Image.FromFile("Knight_W.png");
                    buttons[7, 1].Image = new Bitmap(buttons[7, 1].Image, new Size(32, 32));
                    buttons[7, 6].Image = Image.FromFile("Knight_W.png");
                    buttons[7, 6].Image = new Bitmap(buttons[7, 6].Image, new Size(32, 32));

                    buttons[0, 2].Image = Image.FromFile("Bishop_B.png");
                    buttons[0, 2].Image = new Bitmap(buttons[0, 2].Image, new Size(32, 32));
                    buttons[0, 5].Image = Image.FromFile("Bishop_B.png");
                    buttons[0, 5].Image = new Bitmap(buttons[0, 5].Image, new Size(32, 32));

                    buttons[7, 2].Image = Image.FromFile("Bishop_W.jpg");
                    buttons[7, 2].Image = new Bitmap(buttons[7, 2].Image, new Size(32, 32));
                    buttons[7, 5].Image = Image.FromFile("Bishop_W.jpg");
                    buttons[7, 5].Image = new Bitmap(buttons[7, 5].Image, new Size(32, 32));

                    buttons[0, 4].Image = Image.FromFile("Queen_B.jpg");
                    buttons[0, 4].Image = new Bitmap(buttons[0, 4].Image, new Size(32, 32));

                    buttons[7, 4].Image = Image.FromFile("Queen_W.jpg");
                    buttons[7, 4].Image = new Bitmap(buttons[7, 4].Image, new Size(32, 32));

                    buttons[0, 3].Image = Image.FromFile("King_B.jpg");
                    buttons[0, 3].Image = new Bitmap(buttons[0, 3].Image, new Size(32, 32));

                    buttons[7, 3].Image = Image.FromFile("King_W.jpg");
                    buttons[7, 3].Image = new Bitmap(buttons[7, 3].Image, new Size(32, 32));

                    o++;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var button in panel1.Controls.OfType<Button>())
            {
                button.MouseClick += button_Click;
            }
        }
        // Sakkmatt-e a helyzet, ezt igen/nem-mel tudjuk meg
        public static bool IsCheckMate()
        {
            List<Pieces> possMove = new List<Pieces>();
            for (int i = 0; i < c.pieces.GetLength(0); i++)
            {
                for (int j = 0; j < c.pieces.GetLength(1); j++)
                {
                    if (!(c.pieces[i, j] is null))
                    {
                        if (c.pieces[i, j].Team == 'W' && turns)
                        {
                            if (c.pieces[i, j] is Pawn)
                            {
                                bFrom = new Pawn(i, j, 'W');
                            }
                            else if (c.pieces[i, j] is Rook)
                            {
                                bFrom = new Rook(i, j, 'W');
                            }
                            else if (c.pieces[i, j] is Knight)
                            {
                                bFrom = new Knight(i, j, 'W');
                            }
                            else if (c.pieces[i, j] is Bishop)
                            {
                                bFrom = new Bishop(i, j, 'W');
                            }
                            else if (c.pieces[i, j] is Queen)
                            {
                                bFrom = new Queen(i, j, 'W');
                            }
                            else if (c.pieces[i, j] is King)
                            {
                                bFrom = new King(i, j, 'W');
                            }
                            possMove = c.pieces[i, j].Move(i, j, c);

                            for (int q = 0; q < possMove.Count; q++)
                            {
                                if (!(TestStep(possMove[q].X, possMove[q].Y)))
                                {
                                    return false;
                                }
                            }
                        }
                        else if (c.pieces[i, j].Team == 'B' && !turns)
                        {
                            if (c.pieces[i, j] is Pawn)
                            {
                                bFrom = new Pawn(i, j, 'B');
                            }
                            else if (c.pieces[i, j] is Rook)
                            {
                                bFrom = new Rook(i, j, 'B');
                            }
                            else if (c.pieces[i, j] is Knight)
                            {
                                bFrom = new Knight(i, j, 'B');
                            }
                            else if (c.pieces[i, j] is Bishop)
                            {
                                bFrom = new Bishop(i, j, 'B');
                            }
                            else if (c.pieces[i, j] is Queen)
                            {
                                bFrom = new Queen(i, j, 'B');
                            }
                            else if (c.pieces[i, j] is King)
                            {
                                bFrom = new King(i, j, 'B');
                            }
                            possMove = c.pieces[i, j].Move(i, j, c);

                            for (int q = 0; q < possMove.Count; q++)
                            {
                                if (!(TestStep(possMove[q].X, possMove[q].Y)))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
        // Teszt lépések melyek okoznak-e sakkot, ha nem akkor zöld lesz az a (x,y) koordinátájú gomb.
        private static bool TestStep(int x, int y)
        {
            c.pieces[bFrom.X, bFrom.Y] = null;
            Pieces enemy = null;
            if (turns == true)
            {
                if (c.pieces[x, y] is Pawn)
                {
                    enemy = new Pawn(x, y, 'B');
                }
                else if (c.pieces[x, y] is Rook)
                {
                    enemy = new Rook(x, y, 'B');
                }
                else if (c.pieces[x, y] is Knight)
                {
                    enemy = new Knight(x, y, 'B');
                }
                else if (c.pieces[x, y] is Bishop)
                {
                    enemy = new Bishop(x, y, 'B');
                }
                else if (c.pieces[x, y] is Queen)
                {
                    enemy = new Queen(x, y, 'B');
                }
                else if (c.pieces[x, y] is King)
                {
                    enemy = new King(x, y, 'B');
                }

                if (bFrom is Pawn)
                {
                    c.pieces[x, y] = new Pawn(x, y, 'W');
                }
                else if (bFrom is Rook)
                {
                    c.pieces[x, y] = new Rook(x, y, 'W');
                }
                else if (bFrom is Knight)
                {
                    c.pieces[x, y] = new Knight(x, y, 'W');
                }
                else if (bFrom is Bishop)
                {
                    c.pieces[x, y] = new Bishop(x, y, 'W');
                }
                else if (bFrom is Queen)
                {
                    c.pieces[x, y] = new Queen(x, y, 'W');
                }
                else if (bFrom is King)
                {
                    c.pieces[x, y] = new King(x, y, 'W');
                }

  
                bool enemyhit = false;

                if (bFrom is King && enemy != null)
                {
                    c.IsNotChecked(c, turns);
                    if (c.Kingchecked(c.opponentSteps, c, true))
                    {
                        enemyhit = true;
                    }
                }
                
                if (c.Attacker != null)
                {
                    List<Pieces> ds = new List<Pieces>();

                    ds = c.Attacker.Move(c.Attacker.X, c.Attacker.Y, c);

                    if (c.Kingchecked(ds,c,true))
                    {
                        enemyhit = true;
                    }
                    for (int i = 0; i < c.opponentSteps.Count; i++)
                    {
                        if (c.opponentSteps[i].X == x && c.opponentSteps[i].Y == y && bFrom is King)
                        {
                            enemyhit = true;
                        } 
                    }
                    for (int i = 0; i < ds.Count; i++)
                    {
                        if(ds[i].X == x && ds[i].Y == y && bFrom is King)
                        {
                            enemyhit = true;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < c.opponentSteps.Count; i++)
                    {
                        if (c.opponentSteps[i].X == x && c.opponentSteps[i].Y == y && bFrom is King)
                        {
                            enemyhit = true;
                        }

                        if (c.opponentSteps[i].X == bFrom.X && c.opponentSteps[i].Y == bFrom.Y)
                        {
                            List<Pieces> ds = new List<Pieces>();

                            if (c.opponentSteps[i] is Rook)
                            {
                                List<Pieces> asd = new List<Pieces>();
                                for (int k = 0; k < c.pieces.GetLength(0); k++)
                                {
                                    for (int j = 0; j < c.pieces.GetLength(1); j++)
                                    {
                                        if (c.pieces[k, j] is Rook && c.pieces[k, j].Team == 'B')
                                        {
                                            asd = c.pieces[k, j].Move(k, j, c);
                                            for (int n = 0; n < asd.Count; n++)
                                            {
                                                ds.Add(new Rook(asd[n].X, asd[n].Y, 'B'));
                                            }
                                        }
                                    }
                                }
                            }
                            else if (c.opponentSteps[i] is Bishop)
                            {
                                List<Pieces> asd = new List<Pieces>();
                                for (int k = 0; k < c.pieces.GetLength(0); k++)
                                {
                                    for (int j = 0; j < c.pieces.GetLength(1); j++)
                                    {
                                        if (c.pieces[k, j] is Bishop && c.pieces[k, j].Team == 'B')
                                        {
                                            asd = c.pieces[k, j].Move(k, j, c);
                                            for (int n = 0; n < asd.Count; n++)
                                            {
                                                ds.Add(new Bishop(asd[n].X, asd[n].Y, 'B'));
                                            }
                                        }
                                    }
                                }
                            }
                            else if (c.opponentSteps[i] is Queen)
                            {
                                for (int k = 0; k < c.pieces.GetLength(0); k++)
                                {
                                    for (int j = 0; j < c.pieces.GetLength(1); j++)
                                    {
                                        if (c.pieces[k, j] is Queen && c.pieces[k, j].Team == 'B')
                                        {
                                            ds = c.pieces[k, j].Move(k, j, c);
                                        }
                                    }
                                }
                            }
                            if (c.Kingchecked(ds, c, true))
                            {
                                enemyhit = true;
                            } 
                        }
                    }
                }
                c.pieces[x, y] = null;

                if (enemy is Pawn)
                {
                    c.pieces[x, y] = new Pawn(x, y, 'B');
                }
                else if (enemy is Rook)
                {
                    c.pieces[x, y] = new Rook(x, y, 'B');
                }
                else if (enemy is Knight)
                {
                    c.pieces[x, y] = new Knight(x, y, 'B');
                }
                else if (enemy is Bishop)
                {
                    c.pieces[x, y] = new Bishop(x, y, 'B');
                }
                else if (enemy is Queen)
                {
                    c.pieces[x, y] = new Queen(x, y, 'B');
                }
                else if (enemy is King)
                {
                    c.pieces[x, y] = new King(x, y, 'B');
                }

                if (bFrom is Pawn)
                {
                    c.pieces[bFrom.X, bFrom.Y] = new Pawn(x, y, 'W');
                }
                else if (bFrom is Rook)
                {
                    c.pieces[bFrom.X, bFrom.Y] = new Rook(x, y, 'W');
                }
                else if (bFrom is Knight)
                {
                    c.pieces[bFrom.X, bFrom.Y] = new Knight(x, y, 'W');
                }
                else if (bFrom is Bishop)
                {
                    c.pieces[bFrom.X, bFrom.Y] = new Bishop(x, y, 'W');
                }
                else if (bFrom is Queen)
                {
                    c.pieces[bFrom.X, bFrom.Y] = new Queen(x, y, 'W');
                }
                else if (bFrom is King)
                {
                    c.pieces[bFrom.X, bFrom.Y] = new King(x, y, 'W');
                }

                if (bFrom is King && enemy != null)
                {
                    c.IsNotChecked(c, true);
                }

                if (enemyhit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (c.pieces[x, y] is Pawn)
                {
                    enemy = new Pawn(x, y, 'W');
                }
                else if (c.pieces[x, y] is Rook)
                {
                    enemy = new Rook(x, y, 'W');
                }
                else if (c.pieces[x, y] is Knight)
                {
                    enemy = new Knight(x, y, 'W');
                }
                else if (c.pieces[x, y] is Bishop)
                {
                    enemy = new Bishop(x, y, 'W');
                }
                else if (c.pieces[x, y] is Queen)
                {
                    enemy = new Queen(x, y, 'W');
                }
                else if (c.pieces[x, y] is King)
                {
                    enemy = new King(x, y, 'W');
                }


                if (bFrom is Pawn)
                {
                    c.pieces[x, y] = new Pawn(x, y, 'B');
                }
                else if (bFrom is Rook)
                {
                    c.pieces[x, y] = new Rook(x, y, 'B');
                }
                else if (bFrom is Knight)
                {
                    c.pieces[x, y] = new Knight(x, y, 'B');
                }
                else if (bFrom is Bishop)
                {
                    c.pieces[x, y] = new Bishop(x, y, 'B');
                }
                else if (bFrom is Queen)
                {
                    c.pieces[x, y] = new Queen(x, y, 'B');
                }
                else if (bFrom is King)
                {
                    c.pieces[x, y] = new King(x, y, 'B');
                }

                bool enemyhit = false;

                if (bFrom is King && enemy != null)
                {
                    c.IsNotChecked(c, false);
                    if (c.Kingchecked(c.opponentSteps, c, false))
                    {
                        enemyhit = true;
                    }
                }

                if (c.Attacker != null)
                {
                    List<Pieces> ds = new List<Pieces>();

                    ds = c.Attacker.Move(c.Attacker.X, c.Attacker.Y, c);

                    if (c.Kingchecked(ds, c, false))
                    {
                        enemyhit = true;
                    }
                    for (int i = 0; i < c.opponentSteps.Count; i++)
                    {
                        if (c.opponentSteps[i].X == x && c.opponentSteps[i].Y == y && bFrom is King)
                        {
                            enemyhit = true;
                        }
                    }
                    for (int i = 0; i < ds.Count; i++)
                    {
                        if (ds[i].X == x && ds[i].Y == y && bFrom is King)
                        {
                            enemyhit = true;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < c.opponentSteps.Count; i++)
                    {
                        if (c.opponentSteps[i].X == x && c.opponentSteps[i].Y == y && bFrom is King)
                        {
                            enemyhit = true;
                        }

                        if (c.opponentSteps[i].X == bFrom.X && c.opponentSteps[i].Y == bFrom.Y)
                        {
                            List<Pieces> ds = new List<Pieces>();

                            if (c.opponentSteps[i] is Rook)
                            {
                                List<Pieces> asd = new List<Pieces>();
                                for (int k = 0; k < c.pieces.GetLength(0); k++)
                                {
                                    for (int j = 0; j < c.pieces.GetLength(1); j++)
                                    {
                                        if (c.pieces[k, j] is Rook && c.pieces[k, j].Team == 'W')
                                        {
                                            asd = c.pieces[k, j].Move(k, j, c);
                                            for (int n = 0; n < asd.Count; n++)
                                            {
                                                ds.Add(new Rook(asd[n].X, asd[n].Y, 'W'));
                                            }
                                        }
                                    }
                                }
                            }
                            else if (c.opponentSteps[i] is Bishop)
                            {
                                List<Pieces> asd = new List<Pieces>();
                                for (int k = 0; k < c.pieces.GetLength(0); k++)
                                {
                                    for (int j = 0; j < c.pieces.GetLength(1); j++)
                                    {
                                        if (c.pieces[k, j] is Bishop && c.pieces[k, j].Team == 'W')
                                        {
                                            asd = c.pieces[k, j].Move(k, j, c);
                                            for (int n = 0; n < asd.Count; n++)
                                            {
                                                ds.Add(new Bishop(asd[n].X, asd[n].Y, 'W'));
                                            }
                                        }
                                    }
                                }
                            }
                            else if (c.opponentSteps[i] is Queen)
                            {
                                for (int k = 0; k < c.pieces.GetLength(0); k++)
                                {
                                    for (int j = 0; j < c.pieces.GetLength(1); j++)
                                    {
                                        if (c.pieces[k, j] is Queen && c.pieces[k, j].Team == 'W')
                                        {
                                            ds = c.pieces[k, j].Move(k, j, c);
                                        }
                                    }
                                }
                            }
                            if (c.Kingchecked(ds, c, false))
                            {
                                enemyhit = true;
                            }
                        }
                    }
                }
                c.pieces[x, y] = null;

                if (enemy is Pawn)
                {
                    c.pieces[x, y] = new Pawn(x, y, 'W');
                }
                else if (enemy is Rook)
                {
                    c.pieces[x, y] = new Rook(x, y, 'W');
                }
                else if (enemy is Knight)
                {
                    c.pieces[x, y] = new Knight(x, y, 'W');
                }
                else if (enemy is Bishop)
                {
                    c.pieces[x, y] = new Bishop(x, y, 'W');
                }
                else if (enemy is Queen)
                {
                    c.pieces[x, y] = new Queen(x, y, 'W');
                }
                else if (enemy is King)
                {
                    c.pieces[x, y] = new King(x, y, 'W');
                }

                if (bFrom is Pawn)
                {
                    c.pieces[bFrom.X, bFrom.Y] = new Pawn(x, y, 'B');
                }
                else if (bFrom is Rook)
                {
                    c.pieces[bFrom.X, bFrom.Y] = new Rook(x, y, 'B');
                }
                else if (bFrom is Knight)
                {
                    c.pieces[bFrom.X, bFrom.Y] = new Knight(x, y, 'B');
                }
                else if (bFrom is Bishop)
                {
                    c.pieces[bFrom.X, bFrom.Y] = new Bishop(x, y, 'B');
                }
                else if (bFrom is Queen)
                {
                    c.pieces[bFrom.X, bFrom.Y] = new Queen(x, y, 'B');
                }
                else if (bFrom is King)
                {
                    c.pieces[bFrom.X, bFrom.Y] = new King(x, y, 'B');
                }

                if (bFrom is King && enemy != null)
                {
                    c.IsNotChecked(c, false);
                }

                if (enemyhit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        // Lépések az összes bábuhoz
        private static void Step(int x, int y)
        {
            buttons[bFrom.X, bFrom.Y].Image = null;
            c.pieces[bFrom.X, bFrom.Y] = null;

            if (bFrom is Pawn)
            {
                if (bFrom.Team == 'W')
                {
                    buttons[x, y].Image = Image.FromFile("Pawn.jpg");
                    buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));
                    c.pieces[x, y] = new Pawn(bFrom.X, bFrom.Y, bFrom.Team);
                    if(x == 2) {
                        if (!(c.pieces[x + 1, y] is null))
                        {
                            if (c.pieces[x + 1, y] is Pawn && c.pieces[x + 1, y].Team == 'B')
                            {
                                if ((c.pieces[x + 1, y] as Pawn).StepTwo)
                                {
                                    c.pieces[x + 1, y] = null;
                                    buttons[x + 1, y].Image = null;
                                }
                            }
                        }
                    }
                    if (x == 4 && bFrom.X == 6)
                    {
                        (c.pieces[x, y] as Pawn).StepTwo = true;
                    }
                    turns = false;
                } else
                {
                    buttons[x, y].Image = Image.FromFile("Pawn_B.jpg");
                    buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));
                    c.pieces[x, y] = new Pawn(bFrom.X, bFrom.Y, bFrom.Team);
                    if (x == 5)
                    {
                        if (!(c.pieces[x - 1, y] is null))
                        {
                            if (c.pieces[x - 1, y] is Pawn && c.pieces[x + 1, y].Team == 'B')
                            {
                                if ((c.pieces[x - 1, y] as Pawn).StepTwo)
                                {
                                    c.pieces[x - 1, y] = null;
                                    buttons[x - 1, y].Image = null;
                                }
                            }
                        }
                    }
                    if (x == 3 && bFrom.X == 1)
                    {
                        (c.pieces[x, y] as Pawn).StepTwo = true;
                    }
                    turns = true;
                }
            } else if(bFrom is Rook)
            {
                if (bFrom.Team == 'W')
                {
                    buttons[x, y].Image = Image.FromFile("Rook_W.jpg");
                    buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));
                    c.pieces[x, y] = new Rook(bFrom.X, bFrom.Y, bFrom.Team);
                    turns = false;
                }
                else
                {
                    buttons[x, y].Image = Image.FromFile("Rook_B.jpg");
                    buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));
                    c.pieces[x, y] = new Rook(bFrom.X, bFrom.Y, bFrom.Team);
                    turns = true;
                }
            }
            else if (bFrom is Knight)
            {
                if (bFrom.Team == 'W')
                {
                    buttons[x, y].Image = Image.FromFile("Knight_W.png");
                    buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));
                    c.pieces[x, y] = new Knight(bFrom.X, bFrom.Y, bFrom.Team);
                    turns = false;
                }
                else
                {
                    buttons[x, y].Image = Image.FromFile("Knight_B.jpg");
                    buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));
                    c.pieces[x, y] = new Knight(bFrom.X, bFrom.Y, bFrom.Team);
                    turns = true;
                }
            }
            else if (bFrom is Bishop)
            {
                if (bFrom.Team == 'W')
                {
                    buttons[x, y].Image = Image.FromFile("Bishop_W.jpg");
                    buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));
                    c.pieces[x, y] = new Bishop(bFrom.X, bFrom.Y, bFrom.Team);
                    turns = false;
                }
                else
                {
                    buttons[x, y].Image = Image.FromFile("Bishop_B.png");
                    buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));
                    c.pieces[x, y] = new Bishop(bFrom.X, bFrom.Y, bFrom.Team);
                    turns = true;
                }
            }
            else if (bFrom is Queen)
            {
                if (bFrom.Team == 'W')
                {
                    buttons[x, y].Image = Image.FromFile("Queen_W.jpg");
                    buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));
                    c.pieces[x, y] = new Queen(bFrom.X, bFrom.Y, bFrom.Team);
                    turns = false;
                }
                else
                {
                    buttons[x, y].Image = Image.FromFile("Queen_B.jpg");
                    buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));
                    c.pieces[x, y] = new Queen(bFrom.X, bFrom.Y, bFrom.Team);
                    turns = true;
                }
            }
            else if (bFrom is King)
            {
                if (bFrom.Team == 'W')
                {
                    buttons[x, y].Image = Image.FromFile("King_W.jpg");
                    buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));
                    c.pieces[x, y] = new King(bFrom.X, bFrom.Y, bFrom.Team);
                    turns = false;
                    // Ha Castle van, azaz rossálás, helyet cserélnük azzal a bástyával.
                    if (x == 7 && y == 1 && bFrom.Y == 3 && bFrom.X == 7)
                    {
                        c.pieces[7, 0] = null;
                        buttons[7, 0].Image = null;
                        buttons[7, 2].Image = Image.FromFile("Rook_W.jpg");
                        buttons[7, 2].Image = new Bitmap(buttons[7, 2].Image, new Size(32, 32));
                        c.pieces[7, 2] = new Rook(bFrom.X, bFrom.Y, bFrom.Team);
                        turns = false;
                    }
                    else if (x == 7 && y == 5 && bFrom.Y == 3 && bFrom.X == 7)
                    {
                        c.pieces[7, 7] = null;
                        buttons[7, 7].Image = null;
                        buttons[7, 4].Image = Image.FromFile("Rook_W.jpg");
                        buttons[7, 4].Image = new Bitmap(buttons[7, 4].Image, new Size(32, 32));
                        c.pieces[7, 4] = new Rook(bFrom.X, bFrom.Y, bFrom.Team);
                        turns = false;
                    }
                }
                else
                {
                    buttons[x, y].Image = Image.FromFile("King_B.jpg");
                    buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));
                    c.pieces[x, y] = new King(bFrom.X, bFrom.Y, bFrom.Team);
                    turns = true;
                }
            }
        }
        // Eseménykezelő a gomb lenyomására.
        private void button_Click(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            int Bx = b.TabIndex;
            int By = int.Parse(b.Name);
            Button choosen = new Button(); // ahova majd szeretnénk lépni, nem is használom lol!

            if (panel2.Visible == false && b != null && (buttons[Bx, By].BackColor == Color.Green || buttons[Bx, By].BackColor == Color.Yellow || !(c.pieces[Bx, By] is null)))
            {
                // Gombok kiszínezése 
                if (buttons[Bx, By].BackColor == Color.Green || buttons[Bx, By].BackColor == Color.Yellow)
                {
                    for (int i = 0; i < buttons.GetLength(0); i++)
                    {
                        for (int j = 0; j < buttons.GetLength(1); j++)
                        {
                            if (buttons[i, j].BackColor == Color.Green || buttons[i, j].BackColor == Color.Yellow ||  buttons[i, j].BackColor == Color.Red)
                            {
                                if ((i + j) % 2 == 0)
                                {
                                    buttons[i, j].BackColor = Color.White;
                                }
                                else
                                {
                                    buttons[i, j].BackColor = Color.DarkSalmon;
                                }
                            }
                        }
                    }
                    // Ha a paraszt elér a végére (fehér bábu)
                    if (c.pieces[bFrom.X,bFrom.Y].Team == 'W' && c.pieces[bFrom.X, bFrom.Y] is Pawn && Bx == 0)
                    {
                        x = Bx;
                        y = By;
                        panel2.Visible = true;
                        pictureBox1.Visible = true;
                        pictureBox2.Visible = true;
                        pictureBox3.Visible = true;
                        pictureBox4.Visible = true;
                    }
                    // Lépünk a bx,by koordinátájú gombon elhelyezett képpel
                    match[matchi, 0] = bFrom.ToString();
                    match[matchi, 1] = bFrom.X.ToString();
                    match[matchi, 2] = bFrom.Y.ToString();
                    if (c.pieces[x, y] is null)
                    {
                        match[matchi, 3] = "null";
                    } else
                    {
                        match[matchi, 3] = c.pieces[x,y].ToString();
                    }
                    match[matchi, 4] = Bx.ToString();
                    match[matchi, 5] = By.ToString();
                    match[matchi, 6] = bFrom.Team.ToString();
                    matchi++;
                    char[] abc = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
                    listView1.Items.Add(new ListViewItem(bFrom.ToString() + ", " + bFrom.Team + " (" + abc[By] + "," + (By+1) + ")"));

                    /*if (c.pieces[x, y] is null)
                    {
                        g.SaveGame(bFrom, x, y, "-");
                    }
                    else
                    {
                        g.SaveGame(bFrom, x, y, c.pieces[x, y].ToString());
                    }*/
                    Step(Bx, By);
                    // Ha van sakk akkor megnézzük van-e lehetőség azt megelőzni és kiszínezzük pirosra a királyt
                    if (c.Kingchecked(c.IsNotChecked(c,turns),c,turns))
                    {
                        if (IsCheckMate())
                        {
                            MessageBox.Show("vége , bassza meg !");
                        }
                        else
                        {
                            for (int i = 0; i < c.pieces.GetLength(0); i++)
                            {
                                for (int j = 0; j < c.pieces.GetLength(1); j++)
                                {
                                    if (!(c.pieces[i, j] is null))
                                    {
                                        if (c.pieces[i, j] is King && c.pieces[i, j].Team == 'W' && turns)
                                        {
                                            buttons[i, j].BackColor = Color.Red;
                                        }
                                        else if (c.pieces[i, j] is King && c.pieces[i, j].Team == 'B' && !turns)
                                        {
                                            buttons[i, j].BackColor = Color.Red;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        c.Attacker = null;
                    }
                }
                else
                {
                    for (int i = 0; i < buttons.GetLength(0); i++)
                    {
                        for (int j = 0; j < buttons.GetLength(1); j++)
                        {
                            if (buttons[i, j].BackColor == Color.Green || buttons[i, j].BackColor == Color.Yellow)
                            {
                                if ((i + j) % 2 == 0)
                                {
                                    buttons[i, j].BackColor = Color.White;
                                }
                                else
                                {
                                    buttons[i, j].BackColor = Color.DarkSalmon;
                                }
                            }
                        }
                    }
                    // Ha fehérek jönnek
                    if (turns && c.pieces[Bx, By].Team == 'W')
                    {
                        // Lehetséges lépéseket listába teszem
                        PossibleMoves = c.pieces[Bx, By].Move(Bx, By, c);

                        // Ahol nincs sakkban
                        List<Pieces> kingstep = new List<Pieces>();

                        // Végig megyünk a lehetséges lépéseken és megvizsgáljuk ha lépnénk velük akkor lenne-e sakk
                        for (int i = 0; i < PossibleMoves.Count; i++)
                        {
                            if (PossibleMoves[i] is Pawn)
                            {
                                bFrom = new Pawn(Bx, By, 'W');
                            }
                            else if (PossibleMoves[i] is Rook)
                            {
                                bFrom = new Rook(Bx, By, 'W');
                            }
                            else if (PossibleMoves[i] is Knight)
                            {
                                bFrom = new Knight(Bx, By, 'W');
                            }
                            else if (PossibleMoves[i] is Bishop)
                            {
                                bFrom = new Bishop(Bx, By, 'W');
                            }
                            else if (PossibleMoves[i] is Queen)
                            {
                                bFrom = new Queen(Bx, By, 'W');
                            }
                            else  if (PossibleMoves[i] is King)
                            {
                                bFrom = new King(Bx, By, 'W');                       
                            }
                            if (!(TestStep(PossibleMoves[i].X, PossibleMoves[i].Y)))
                            {
                                if (!(c.pieces[PossibleMoves[i].X, PossibleMoves[i].Y] is null))
                                {
                                    buttons[PossibleMoves[i].X, PossibleMoves[i].Y].BackColor = Color.Yellow;                                        }
                                else 
                                {
                                    // Ha az ellenfél parasztja úgy lép hogy 2-t az elején és mellette áll a fehér bábunk akkor keresztben tudunk ütni.
                                    if (bFrom is Pawn && bFrom.X != PossibleMoves[i].X + 1)
                                    {
                                        if (!(c.pieces[PossibleMoves[i].X + 1, PossibleMoves[i].Y] is null))
                                        {
                                            if (c.pieces[PossibleMoves[i].X + 1, PossibleMoves[i].Y].Team == 'B' && c.pieces[PossibleMoves[i].X + 1, PossibleMoves[i].Y] is Pawn)
                                            {
                                                buttons[PossibleMoves[i].X, PossibleMoves[i].Y].BackColor = Color.Yellow;
                                            }
                                        }
                                    }
                                    if (bFrom is Pawn && bFrom.X != PossibleMoves[i].X + 1)
                                    {
                                        if (!(c.pieces[PossibleMoves[i].X - 1, PossibleMoves[i].Y] is null))
                                        {
                                            if (c.pieces[PossibleMoves[i].X - 1, PossibleMoves[i].Y].Team == 'B' && c.pieces[PossibleMoves[i].X - 1, PossibleMoves[i].Y] is Pawn)
                                            {
                                                buttons[PossibleMoves[i].X, PossibleMoves[i].Y].BackColor = Color.Yellow;
                                            }
                                        }
                                    }
                                    buttons[PossibleMoves[i].X, PossibleMoves[i].Y].BackColor = Color.Green;
                                }
                            }
                        }
                    }
                    else if(!turns && c.pieces[Bx, By].Team == 'B')
                    {
                        // Lehetséges lépéseket listába teszem
                        PossibleMoves = c.pieces[Bx, By].Move(Bx, By, c);

                        // Ahol nincs sakkban
                        List<Pieces> kingstep = new List<Pieces>();
                      
                        // Végig megyünk a listán és csak azokat a lehetőségeket színezük ki zöldre melyek nem okoznak sakkot
                        for (int i = 0; i < PossibleMoves.Count; i++)
                        {
                            if (PossibleMoves[i] is Pawn)
                            {
                                bFrom = new Pawn(Bx, By, 'B');
                            }
                            else if (PossibleMoves[i] is Rook)
                            {
                                bFrom = new Rook(Bx, By, 'B');
                            }
                            else if (PossibleMoves[i] is Knight)
                            {
                                bFrom = new Knight(Bx, By, 'B');
                            }
                            else if (PossibleMoves[i] is Bishop)
                            {
                                bFrom = new Bishop(Bx, By, 'B');
                            }
                            else if (PossibleMoves[i] is Queen)
                            {
                                bFrom = new Queen(Bx, By, 'B');
                            }
                            else if (PossibleMoves[i] is King)
                            {
                                bFrom = new King(Bx, By, 'B');
                            }
                            if (!(TestStep(PossibleMoves[i].X, PossibleMoves[i].Y)))
                            {
                                if (!(c.pieces[PossibleMoves[i].X, PossibleMoves[i].Y] is null))
                                {
                                    buttons[PossibleMoves[i].X, PossibleMoves[i].Y].BackColor = Color.Yellow;
                                }
                                else
                                {
                                    buttons[PossibleMoves[i].X, PossibleMoves[i].Y].BackColor = Color.Green;
                                }
                            }
                        }
                    }
                } 
            }// Ha másik bábura nyomunk, tűnjön el a zöld 
            else if(!(c.pieces[Bx, By] is null))
            {
                for (int i = 0; i < buttons.GetLength(0); i++)
                {
                    for (int j = 0; j < buttons.GetLength(1); j++)
                    {
                        if (buttons[i, j].BackColor == Color.Green || buttons[i, j].BackColor == Color.Yellow)
                        {
                            if ((i + j) % 2 == 0)
                            {
                                buttons[i, j].BackColor = Color.White;
                            }
                            else
                            {
                                buttons[i, j].BackColor = Color.DarkSalmon;
                            }
                        }
                    }
                }
            }
            else // Ha olyan mezőre nyomnuk ahol nincs bábu
            {
                for (int i = 0; i < buttons.GetLength(0); i++)
                {
                    for (int j = 0; j < buttons.GetLength(1); j++)
                    {
                        if (buttons[i, j].BackColor == Color.Green || buttons[i, j].BackColor == Color.Yellow)
                        {
                            if ((i + j) % 2 == 0)
                            {
                                buttons[i, j].BackColor = Color.White;
                            }
                            else
                            {
                                buttons[i, j].BackColor = Color.DarkSalmon;
                            }
                        }
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        // Tábla kiszínezése és panelhez hozzáadása rekurzívan
        private void Game_Field(int i, int j,int s)
        {
            if (i < 8)
            {
                if ((i + j) % 2 == 0)
                {
                    panel1.Controls.Add(ButtonCreate(i, j,s, Color.White));
                    if (j == 7)
                    {
                        j = 0;
                        i++;
                    }
                    else
                    {
                        j++;
                    }
                    s++;
                    Game_Field(i, j, s);
                }
                else
                {
                    panel1.Controls.Add(ButtonCreate(i, j,s, Color.DarkSalmon));
                    if (j == 7)
                    {
                        j = 0;
                        i++;
                    }
                    else
                    {
                        j++;
                    }
                    s++;
                    Game_Field(i, j, s);
                }
            }
        }

        // Gomb létrehozása
        public Button ButtonCreate(int x, int y,int s, Color color)
        {
            Button button1 = new Button();
            button1.TabIndex = x;
            button1.Name = y.ToString();
            this.s = s;
            button1.Top = SzamitTop();
            button1.Left = SzamitLeft();
            button1.Height = size;
            button1.Width = size;
            button1.BackColor = color;
            button1.Font = new Font("Courier New", 10, FontStyle.Bold);
            button1.ForeColor = Color.Black;
            return button1;
        }

        private int SzamitTop()  // Kiszámolja a sorszámból, és a gomb adataiból a függőleges pozíciót 
        {
            return (s / n) * size;
        }

        // Paraszt átváltozásához gombok
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            c.pieces[x, y] = new Queen(x, y, 'W');
            buttons[x, y].Image = Image.FromFile("Queen_W.jpg");
            buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));

            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            panel2.Visible = false;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            c.pieces[x, y] = new Rook(x, y, 'W');
            buttons[x, y].Image = Image.FromFile("Rook_W.jpg");
            buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));

            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            panel2.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            c.pieces[x, y] = new Knight(x, y, 'W');
            buttons[x, y].Image = Image.FromFile("Knight_W.png");
            buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));

            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            panel2.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            c.pieces[x, y] = new Bishop(x, y, 'W');
            buttons[x, y].Image = Image.FromFile("Bishop_W.jpg");
            buttons[x, y].Image = new Bitmap(buttons[x, y].Image, new Size(32, 32));

            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            panel2.Visible = false;
            
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            string path = "C:\\Users\\Rápolthy Bálint\\source\\repos\\Chesster\\Chesster\\bin";
            string name = "";
            bool ok = false;
            List<ListViewItem> list = new List<ListViewItem>();
            foreach (string dirFile in Directory.GetDirectories(path))
            {
                foreach (var filename in Directory.GetFiles(dirFile))
                {
                    for (int i = 0; i < filename.Length; i++)
                    {
                        if (i + 1 < filename.Length)
                        {
                            if (filename[i] == 'c' && filename[i + 1] == 'h')
                            {
                                while (i < filename.Length)
                                {
                                    name += filename[i];
                                    i++;
                                    ok = true;
                                }
                            }
                        }
                    }
                    if (ok)
                    {
                        list.Add(new ListViewItem(name));
                        name = "";
                        ok = false;
                    }
                }
            }
            string a = list[list.Count - 1].Text;
            string num = "";
            for (int i = 0; i < a.Length; i++)
            {
               if(a[i] == '_')
                {
                    num += a[i+1];
                }
            }
            
            g = new Gamefile("chess_"+(Int32.Parse(num)+1)+".txt");
            */
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (match[matchi, 0] == "Chesster.Pawn")
            {
                bFrom = new Pawn(int.Parse(match[matchi, 4]), int.Parse(match[matchi, 5]), char.Parse(match[matchi, 6]));
            }
            else if (match[matchi, 0] == "Chesster.Knight")
            {
                bFrom = new Knight(int.Parse(match[matchi, 4]), int.Parse(match[matchi, 5]), char.Parse(match[matchi, 6]));
            }
            else if (match[matchi, 0] == "Chesster.Rook")
            {
                bFrom = new Rook(int.Parse(match[matchi, 4]), int.Parse(match[matchi, 5]), char.Parse(match[matchi, 6]));
            }
            else if (match[matchi, 0] == "Chesster.Bishop")
            {
                bFrom = new Bishop(int.Parse(match[matchi, 4]), int.Parse(match[matchi, 5]), char.Parse(match[matchi, 6]));
            }
            else if (match[matchi, 0] == "Chesster.Queen")
            {
                bFrom = new Queen(int.Parse(match[matchi, 4]), int.Parse(match[matchi, 5]), char.Parse(match[matchi, 6]));
            }
            else if (match[matchi, 0] == "Chesster.King")
            {
                bFrom = new King(int.Parse(match[matchi, 4]), int.Parse(match[matchi, 5]), char.Parse(match[matchi, 6]));
            }
           // c.pieces[int.Parse(match[matchi, 1]), int.Parse(match[matchi, 2])] = null;
           // buttons[int.Parse(match[matchi, 1]), int.Parse(match[matchi, 2])].Image = null;

            Step(bFrom.X, bFrom.Y);

            matchi -= 1;
            listView1.Items.RemoveAt(listView1.Items.Count - 1);
            if (turns)
            {
                turns = false;
            }
            else
            {
                turns = true;
            }
        }
        
        private int SzamitLeft() // Kiszámolja a sorszámból, és a gomb adataiból a vízszintes pozíciót
        {
            return (s % n) * size;
        }
    }
    
 }
