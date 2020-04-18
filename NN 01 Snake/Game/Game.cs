using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_01_Snake {
    class Game {
        public const int Worlds = 100;
        public const int KeepTopN = 0;

        private IReadOnlyList<Board> boards;
        private Font font;
        private int generation = 1;

        public int ProcessFrames { get; set; } = 1;

        public Game () {
            boards = Enumerable.Range (0, Worlds)
                .Select (i => new Board ())
                .ToList ();
            font = new Font ("Helvetica", 16);
        }

        public void Draw (Graphics g) {
            Board.Clear (g);
            foreach (Board board in boards)
                board.DrawApple (g);
            foreach (Board board in boards)
                board.Snake.Draw (g);

            IReadOnlyList<Snake> alives = boards
                .Select (board => board.Snake)
                .Where (snake => snake.IsAlive)
                .ToList ();
            if (alives.Any ()) {
                Snake best = alives.ArgMax (board => board.Score);
                g.DrawString ($"Поколение: {generation}\r\n" + best.Report (),
                    font, Brushes.Yellow, new Point (2, 2));
            }
        }

        public void Step () {
            Parallel.ForEach (boards, board => {
                for (int i = 0; i < ProcessFrames; i++)
                    board.Snake.Step ();
            });

            if (!boards.Any (board => board.Snake.IsAlive))
                SpawnNextGeneration ();
        }
        private void SpawnNextGeneration () {
            Breed breed = new Breed (boards.Select (board => board.Snake));
            boards = breed.KeepTopN (KeepTopN)
                .Concat (Enumerable.Range (0, Worlds - KeepTopN)
                    .AsParallel ()
                    .Select (i => breed.Spawn ())
                    .ToList ())
                .Select (brain => new Board (brain))
                .ToList ();
            generation++;
        }
    }
}
