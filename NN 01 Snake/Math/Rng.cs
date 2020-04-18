using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NN_01_Snake {
    public static class Rng {
        private static int seed = Environment.TickCount;

        private static ThreadLocal<Random> random = new ThreadLocal<Random> (
            () => new Random (Interlocked.Increment (ref seed)));

        public static int GetInt (int min, int maxEx) =>
            random.Value.Next (min, maxEx);

        public static float GetFloat (float min, float max) =>
            (float) random.Value.NextDouble () * (max - min) + min;

        public static double GetDouble (double min, double max) =>
            random.Value.NextDouble () * (max - min) + min;

        public static double Gaussian (float mean, float stdDev) {
            double uniform1 = 1.0 - GetDouble (0, 1);  // uniform(0,1] random doubles
            double uniform2 = 1.0 - GetDouble (0, 1);
            double randStdNormal = Math.Sqrt (-2.0 * Math.Log (uniform1)) *
                                   Math.Sin (2.0 * Math.PI * uniform2);  // random normal(0,1)
            return mean + stdDev * randStdNormal;  // random normal(mean,stdDev^2)
        }
    }
}
