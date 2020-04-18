using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NN_01_Snake {
    public partial class Form1 : Form {
        private Board board = new Board ();

        public Form1 () {
            InitializeComponent ();
        }

        private void timer_Tick (object sender, EventArgs e) {
            board.Snake.Step ();
            Invalidate ();
        }

        private void Form1_Paint (object sender, PaintEventArgs e) =>
            board.Draw (e.Graphics);
    }
}
