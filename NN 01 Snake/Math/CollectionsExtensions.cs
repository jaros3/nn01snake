using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_01_Snake {
    static class CollectionsExtensions {

        public static IReadOnlyList<float> AttachOne (this IReadOnlyList<float> values) =>
            values.Concat (new[] { 1f }).ToList ();

        public static Matrix VectorToColumnMatrix (this IReadOnlyList<float> vector) {
            float[, ] column = new float[vector.Count, 1];
            for (int i = 0; i < vector.Count; i++)
                column[i, 0] = vector[i];
            return new Matrix (column);
        }

        public static IReadOnlyList<float> ColumnMatrixToVector (this Matrix matrix) =>
            matrix.Cells.Cast<float> ().ToList ();
    }
}
