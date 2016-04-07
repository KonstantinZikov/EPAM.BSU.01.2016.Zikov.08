using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Matrix<T> : IMatrix<T>
    {
        T[][] matrix;
        public int Height { get; private set; }
        public int Width { get; private set; }

        public event EventHandler<ElementChangedEventArgs> ElementChanged = delegate { };

        public T this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= Width || j >= Height)
                    throw new ArgumentOutOfRangeException
                        ($"Element [{i},{j}] is located outside the matrix");
                if (matrix[i] == null)
                    return default(T);
                return matrix[i][j];
            }
            set
            {
                if (i < 0 || j < 0 || i >= Width || j >= Height)
                    throw new ArgumentOutOfRangeException
                        ($"Element [{i},{j}] is located outside the matrix");
                if (matrix[i] == null)
                    matrix[i] = new T[Height];
                matrix[i][j] = value;
                OnElementChanged(new ElementChangedEventArgs(i, j));
            }
        }
        
        public Matrix(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException($"{nameof(width)} must be greater then zero");
            if (height <= 0)
                throw new ArgumentOutOfRangeException($"{nameof(height)} must be greater then zero");
            matrix = new T[width][];
            Width = width;
            Height = height;
        }

        public Matrix(T[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException($"{nameof(matrix)} is null.");
            if (matrix.Length == 0)
                throw new ArgumentException($"{nameof(matrix)} can't have a zero size.");
             
            Width = matrix.GetLength(0);
            Height = matrix.GetLength(1);
            this.matrix = new T[matrix.Length][];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                this.matrix[i] = new T[Height];
                for (int j = 0; j < matrix.GetLength(1); j++)
                    this.matrix[i][j] = matrix[i, j];
            }
        }

        private void OnElementChanged(ElementChangedEventArgs e)
            =>ElementChanged(this, e);

    }
}
