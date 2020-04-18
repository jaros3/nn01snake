using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_01_Snake {
    class Snake {
        private const int InitialLength = 6;

        public Board Board { get; }
        public Brain Brain { get; }

        private IReadOnlyList<Pos> body;

        public Pos Head => body[0];

        public Snake (Board board) {
            Board = board;
            Brain = new Brain ();

            Pos pos = board.RandomPosition ();
            body = Enumerable.Repeat (pos, InitialLength).ToList ();
        }

        public void Draw (Graphics g) {
            foreach (Pos cell in body)
                Board.DrawCell (g, Brushes.White, cell.X, cell.Y);
        }

        public void Step () {
            IReadOnlyList<float> observations = GatherObservations ();
            IReadOnlyList<float> actions = Brain.Think (observations);

            int maxIndex = 0;
            for (int i = 0; i < Brain.OutputSize; i++)
                if (actions[i] > actions[maxIndex])
                    maxIndex = i;

            Move (Pos.FourDircections[maxIndex]);
        }

        private IReadOnlyList<float> GatherObservations () =>
            Pos.EightDirections
                .SelectMany (dir => new[] {
                    InverseDistanceToWall (dir),
                    InverseDistanceToApple (dir),
                    InverseDistanceToTail (dir)
                }).ToList ();
        private float InverseDistanceToWall (Pos dir) {
            for (int i = 1; true; i++)
                if (!Board.Contains (Head + dir * i))
                    return 1f / i;
        }
        private float InverseDistanceToApple (Pos dir) {
            for (int i = 1; true; i++)
                if (!Board.Contains (Head + dir * i))
                    return 0;
                else if (Board.Apple == Head + dir * i)
                    return 1f / i;
        }
        private float InverseDistanceToTail (Pos dir) {
            for (int i = 1; true; i++)
                if (!Board.Contains (Head + dir * i))
                    return 0;
                else if (body.Contains (Head + dir * i))
                    return 1f / i;
        }

        private void Move (Pos dir) {
            Pos newHead = Head + dir;
            if (!Board.Contains (newHead) || body.Contains (newHead)) {
                Board.Reset ();
                return;
            }
            int take = body.Count - 1;
            if (Board.Apple == newHead)
                take++;
            body = new[] { newHead }.Concat (body.Take (take)).ToList ();
        }
    }
}
