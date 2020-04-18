using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_01_Snake {
    class Board {
        public const int Width = 40;
        public const int Height = 40;

        public const int Size = 20;

        // 0 .. 39 — легальный диапазон значений

        public Snake Snake { get; set; }
        public Pos Apple { get; set; }

        public Board () => Reset ();

        public void Reset () {
            Snake = new Snake (this);
            Apple = RandomPosition ();
        }
        public Pos RandomPosition () => new Pos (
            Rng.GetInt (0, Width), Rng.GetInt (0, Height));

        public void Draw (Graphics g) {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    DrawCell (g, Brushes.Black, j, i);

            DrawCell (g, Brushes.Red, Apple.X, Apple.Y);
            Snake.Draw (g);
        }

        public void DrawCell (Graphics g, Brush brush, int x, int y) =>
            g.FillRectangle (brush, x * Size, y * Size, Size, Size);

        public bool Contains (Pos pos) =>
            pos.X >= 0 && pos.Y >= 0 && pos.X < Width && pos.Y < Height;
    }
}
