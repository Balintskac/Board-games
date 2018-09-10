using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chesster
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Fileload file = new Fileload();
            file.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 file = new Form1();
            file.Show();
        }
    }
}
