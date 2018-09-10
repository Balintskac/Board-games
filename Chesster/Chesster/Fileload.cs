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
    public partial class Fileload : Form
    {
        public static string filename;
        public Fileload()
        {
            InitializeComponent();
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
          

        }

        private void Fileload_Load(object sender, EventArgs e)
        {
            string path = "C:\\Users\\Rápolthy Bálint\\source\\repos\\Chesster\\Chesster\\bin";
            string name = "";
            bool ok = false;
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
                                    ok = true;
                                    name += filename[i];
                                    i++;
                                }
                            }                        
                        }
                    }
                    if (ok)
                    {
                        listView1.Items.Add(new ListViewItem(name));
                    }
                    name = "";
                    ok = false;
                }
            }
        }

        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            ListView b = sender as ListView;
            Form1 form1 = new Form1(b.FocusedItem.Text);
            form1.Show();
        }
    }
}
