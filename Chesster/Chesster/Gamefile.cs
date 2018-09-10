using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Chesster
{
    class Gamefile
    {
        private StreamWriter sw;
        private StreamReader sr;
        private string filename;
        public Gamefile(string filename)
        {
            this.filename = filename;
        }

        public void SaveGame(Pieces pieceFrom,int x,int y, string enemy)
        {
            sw = new StreamWriter(filename, true);

            sw.WriteLine(pieceFrom.ToString() + "," + pieceFrom.Team + "," + pieceFrom.X + "," + pieceFrom.Y + "," + x + "," + y +"," + enemy);

            sw.Close();
        }

        public int numberOFRows(string filename)
        {
            int row = 0;
            sr = new StreamReader(filename);
            string asd;
            while ((asd = sr.ReadLine()) != null)
            {
                row++;
            }
            sr.Close();

            return row;
        }

        public string[,] LoadGame(string filename)
        {
            string[,] filehandle = new string[numberOFRows(filename), 7];
            sr = new StreamReader(filename);
            string asd;
            int i = 0;
            while ((asd = sr.ReadLine()) != null)
            {
                string[] split = asd.Split(',');

                for (int j = 0; j < 7; j++)
                {
                    filehandle[i, j] = split[j];
                }

                i++;
            }
            sr.Close();
            
            return filehandle;
        }
    }
}
