using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_01_Snake {
    public static class Rng {
        private static Random random = new Random ();

        public static int GetInt (int min, int maxEx) => random.Next (min, maxEx);

        public static float GetFloat (float min, float max) => (float) random.NextDouble () * (max - min) + min;
    }
}
