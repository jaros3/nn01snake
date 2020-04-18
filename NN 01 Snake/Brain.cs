using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_01_Snake {
    class Brain {
        public const int InputSize = 24;
        public const int HiddenSize = 12;
        public const int OutputSize = 4;

        private Matrix inputToHidden = Matrix.Random (HiddenSize, InputSize + 1);
        private Matrix hiddenToOutput = Matrix.Random (OutputSize, HiddenSize + 1);

        public IReadOnlyList<float> Think (IReadOnlyList<float> inputs) {
            IReadOnlyList<float> inputsWith1 = inputs.AttachOne ();
            IReadOnlyList<float> sums = (inputToHidden * inputsWith1.VectorToColumnMatrix ()).ColumnMatrixToVector ();
            IReadOnlyList<float> hiddens = sums.Select (ReLU).ToList ();

            IReadOnlyList<float> hiddensWith1 = hiddens.AttachOne ();
            IReadOnlyList<float> outputs = (hiddenToOutput * hiddensWith1.VectorToColumnMatrix ()).ColumnMatrixToVector ();
            return outputs;
        }

        private static float ReLU (float x) => Math.Max (0, x);
    }
}
