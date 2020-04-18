using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_01_Snake {
    class Matrix {
        public int Rows { get; }
        public int Columns { get; }

        public float[, ] Cells { get; }

        public Matrix (int rows, int columns) {
            Rows = rows;
            Columns = columns;

            Cells = new float[Rows, Columns];
        }
        public static Matrix Random (int rows, int columns) {
            Matrix matrix = new Matrix (rows, columns);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    matrix[i, j] = Rng.GetFloat (-1, 1);
            return matrix;
        }
        public Matrix (float[, ] cells) {
            Rows = cells.GetLength (0);
            Columns = cells.GetLength (1);

            Cells = cells;
        }

        public float this[int i, int j] {
            get => Cells[i, j];
            set => Cells[i, j] = value;
        }

        public static Matrix operator * (Matrix a, Matrix b) {
            if (a.Columns != b.Rows)
                throw new InvalidOperationException ("Размеры не стыкуются");
            int m = a.Rows;
            int n = a.Columns;
            int p = b.Columns;
            float[, ] cells = new float[m, p];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < p; j++) {
                    float sum = 0;
                    for (int k = 0; k < n; k++)
                        sum += a[i, k] * b[k, j];
                    cells[i, j] = sum;
                }
            return new Matrix (cells);
        }

        public static Matrix Cross (Matrix a, Matrix b, float mutationChance) {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new InvalidOperationException ("Размеры не совпадают");

            float[, ] cells = new float[a.Rows, a.Columns];
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Columns; j++) {
                    if (Rng.GetFloat (0, 1) < 0.5f)
                        cells[i, j] = a[i, j];
                    else
                        cells[i, j] = b[i, j];
                    if (Rng.GetFloat (0, 1) < mutationChance)
                        cells[i, j] = Math.Max (-1, Math.Min (1, cells[i, j] + (float) Rng.Gaussian (0, 1)));
                }

            return new Matrix (cells);
        }
    }
}
