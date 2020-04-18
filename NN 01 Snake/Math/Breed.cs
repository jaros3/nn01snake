using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_01_Snake {
    class Breed {
        private readonly IReadOnlyList<(Brain brain, double score)> scores;
        private readonly double totalScore;

        public Breed (IEnumerable<Snake> snakes) {
            scores = snakes
                .Select (snake => (snake.Brain, snake.Score))
                .Pair_OrderBy ((brain, score) => -score)
                .ToList ();
            totalScore = scores.Pair_Select ((brain, score) => score).Sum ();
        }

        public IReadOnlyList<Brain> KeepTopN (int count) =>
            scores
                .Pair_Select ((brain, score) => brain)
                .Take (count)
                .ToList ();

        public Brain Spawn () {
            Brain mom = ChooseParent ();
            Brain dad = ChooseParent ();
            return Brain.Cross (mom, dad);
        }
        private Brain ChooseParent () {
            double seed = Rng.GetDouble (0, totalScore);
            foreach ((Brain brain, double score) pair in scores) {
                seed -= pair.score;
                if (seed < 0)
                    return pair.brain;
            }
            throw new InvalidOperationException ("No matching snake found");
        }
    }
}
