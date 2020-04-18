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

        public const float MutationChance = 0.02f;

        private Matrix inputToHidden;
        private Matrix hiddenToOutput;

        public Brain (Matrix inputToHidden, Matrix hiddenToOutput) {
            this.inputToHidden = inputToHidden;
            this.hiddenToOutput = hiddenToOutput;
        }

        public static Brain Random () =>
            new Brain (
                Matrix.Random (HiddenSize, InputSize + 1),
                Matrix.Random (OutputSize, HiddenSize + 1));

        public IReadOnlyList<float> Think (IReadOnlyList<float> inputs) {
            IReadOnlyList<float> inputsWith1 = inputs.AttachOne ();
            IReadOnlyList<float> sums = (inputToHidden * inputsWith1.VectorToColumnMatrix ()).ColumnMatrixToVector ();
            IReadOnlyList<float> hiddens = sums.Select (ReLU).ToList ();

            IReadOnlyList<float> hiddensWith1 = hiddens.AttachOne ();
            IReadOnlyList<float> outputs = (hiddenToOutput * hiddensWith1.VectorToColumnMatrix ()).ColumnMatrixToVector ();
            return outputs;
        }

        private static float ReLU (float x) => Math.Max (0, x);
        private static float Sigmoid (float x) => 1 / (1 + (float) Math.Exp (-x));

        public static Brain Cross (Brain mom, Brain dad) =>
            new Brain (
                Matrix.Cross (mom.inputToHidden, dad.inputToHidden, MutationChance),
                Matrix.Cross (mom.hiddenToOutput, dad.hiddenToOutput, MutationChance));
    }
}
