using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class SymmetricMatrix<T> : IMatrix<T>
    {
        T[][] matrix;
        public event EventHandler<ElementChangedEventArgs> ElementChanged = delegate { };

        public T this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= Side || j >= Side)
                    throw new ArgumentOutOfRangeException
                        ($"Element [{i},{j}] is located outside the matrix");
                if (matrix[i] == null)
                    return default(T);
                if (j < i)
                {
                    if (matrix[j] == null)
                        return default(T);
                    return matrix[j][i-j];
                }
                return matrix[i][j-i];
            }
            set
            {
                if (i < 0 || j < 0 || i >= Side || j >= Side)
                    throw new ArgumentOutOfRangeException
                        ($"Element [{i},{j}] is located outside the matrix");
                if (matrix[i] == null)
                    matrix[i] = new T[Side - i];
                if (j < i)
                    matrix[j][i-j] = value;
                else
                    matrix[i][j-i] = value;
                OnElementChanged(new ElementChangedEventArgs(i, j));
                if (i != j) OnElementChanged(new ElementChangedEventArgs(j, i));
            }
        }

        public int Height { get { return Side; } }

        public int Width { get { return Side; } }

        public int Side { get; private set; }

        public SymmetricMatrix(int side)
        {
            if (side <= 0)
                throw new ArgumentOutOfRangeException($"{nameof(side)} must be greater then zero");
            matrix = new T[side][];
            Side = side;
        }

        public SymmetricMatrix(T[][] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException($"{nameof(matrix)} is null.");
            if ((Side = matrix.Length) == 0)
                throw new ArgumentException($"{nameof(matrix)} can't have a zero size.");
            this.matrix = new T[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i].Length != Side - i)
                    throw new ArgumentException($"{nameof(matrix)} must be ledder-type.");
                this.matrix[i] = new T[matrix[i].Length];
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    this.matrix[i][j] = matrix[i][j];
                }
                
            }         
        }

        private void OnElementChanged(ElementChangedEventArgs e)
        {
            ElementChanged(this, e);
        }
    }
}
