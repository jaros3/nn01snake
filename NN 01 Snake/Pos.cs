using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_01_Snake {
    class Pos {
        public static Pos Up { get; } = new Pos (0, -1);
        public static Pos UpRight { get; } = new Pos (1, -1);
        public static Pos Right { get; } = new Pos (1, 0);
        public static Pos DownRight { get; } = new Pos (1, 1);
        public static Pos Down { get; } = new Pos (0, 1);
        public static Pos DownLeft { get; } = new Pos (-1, 1);
        public static Pos Left { get; } = new Pos (-1, 0);
        public static Pos UpLeft { get; } = new Pos (-1, -1);

        public static IReadOnlyList<Pos> FourDircections { get; } = new[] { Up, Right, Down, Left };
        public static IReadOnlyList<Pos> EightDirections { get; } = new[] {
            Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft
        };


        public int X { get; }
        public int Y { get; }

        public Pos (int x, int y) {
            X = x;
            Y = y;
        }

        public static Pos operator + (Pos a, Pos b) => new Pos (a.X + b.X, a.Y + b.Y);
        public static Pos operator * (Pos a, int n) => new Pos (a.X * n, a.Y * n);

        public static bool operator == (Pos a, Pos b) => a.Equals (b);
        public static bool operator != (Pos a, Pos b) => !a.Equals (b);


        public override bool Equals (object other) =>
            other is Pos that && this.X == that.X && this.Y == that.Y;

        public override int GetHashCode () => throw new NotSupportedException ();

        public override string ToString () => $"({X}, {Y})";
    }
}
