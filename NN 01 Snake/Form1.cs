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
        private Game game = new Game ();
        private float interval;

        public Form1 () {
            InitializeComponent ();
        }

        private void timer_Tick (object sender, EventArgs e) {
            game.Step ();
            Invalidate ();
        }

        private void Form1_Paint (object sender, PaintEventArgs e) =>
            game.Draw (e.Graphics);

        private void Form1_KeyPress (object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '=' || e.KeyChar == '+')
                Faster ();
            else if (e.KeyChar == '-')
                Slower ();
            else if (e.KeyChar == '[')
                game.ProcessFrames = Math.Max (1, game.ProcessFrames - 1);
            else if (e.KeyChar == ']')
                game.ProcessFrames++;
        }
        private void Faster () {
            interval = Math.Max (interval * 0.8f, 10);
            timer.Interval = (int) interval;
        }
        private void Slower () {
            interval = interval / 0.8f;
            timer.Interval = (int) interval;
        }
    }
}
