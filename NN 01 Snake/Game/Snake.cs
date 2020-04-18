using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_01_Snake {
    class Snake {
        private const int InitialLength = 6;
        private const int InitialTimeRemaining = 200;
        private const int BonusTimePerApple = 100;

        public Board Board { get; }
        public Brain Brain { get; }

        public bool IsAlive { get; set; } = true;
        public int Lifetime { get; set; }
        public int Apples { get; set; }
        public int TimeRemaining { get; set; } = InitialTimeRemaining;

        public double Score =>
            Lifetime * Lifetime * Math.Pow (2, Math.Min (Apples, 10)) * Math.Max (1, Apples - 9);

        private IReadOnlyList<Pos> body;

        public Pos Head => body[0];

        public Snake (Board board) : this (board, Brain.Random ()) { }

        public Snake (Board board, Brain brain) {
            Board = board;
            Brain = brain;

            Pos pos = board.RandomPosition ();
            body = Enumerable.Repeat (pos, InitialLength).ToList ();
        }

        public void Draw (Graphics g) {
            if (!IsAlive)
                return;

            foreach (Pos cell in body)
                Board.DrawCell (g, Brushes.White, cell.X, cell.Y);
        }

        public void Step () {
            if (!IsAlive)
                return;

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
            if (!Board.Contains (newHead) || body.Contains (newHead) || TimeRemaining == 0) {
                IsAlive = false;
                return;
            }
            Lifetime++;
            TimeRemaining--;

            int take = body.Count - 1;
            if (Board.Apple == newHead) {
                take++;
                Apples++;
                TimeRemaining += BonusTimePerApple;
                Board.PlaceApple ();
            }
            body = new[] { newHead }.Concat (body.Take (take)).ToList ();
        }

        public string Report () =>
            $"Время жизни: {Lifetime}\r\nОсталось: {TimeRemaining}\r\nЯблоки: {Apples}\r\nОчки: {Score}";
    }
}
