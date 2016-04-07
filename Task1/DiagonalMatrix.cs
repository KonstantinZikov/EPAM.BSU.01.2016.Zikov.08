using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class DiagonalMatrix<T> : IMatrix<T>
    {
        T[] diagonal;
        public int Side { get; private set; }
        public int Height { get { return Side; } }
        public int Width { get { return Side; } }

        public event EventHandler<ElementChangedEventArgs> ElementChanged = delegate { };

        public DiagonalMatrix(int side)
        {
            if (side < 0)
                throw new ArgumentOutOfRangeException
                    ($"{nameof(side)} must be greater then zero.");
            diagonal = new T[side];
            Side = side;
        }

        public DiagonalMatrix(T[] diagonal)
        {
            if (diagonal == null)
                throw new ArgumentNullException($"{nameof(diagonal)} is null.");
        }

        T IMatrix<T>.this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= Side || j >= Side)
                    throw new ArgumentOutOfRangeException
                        ($"Element [{i},{j}] is located outside the matrix");
                if (i == j)
                    return diagonal[i];
                return default(T);
            }
        }

        /// <summary>
        /// Represents the diagonal element of selected matrix column.
        /// </summary>
        /// <param name="i">column index</param>
        /// <returns>The diagonal element on row i. </returns>
        public T this[int i]
        {
            get
            {
                if (i < 0 || i >= Side)
                    throw new ArgumentOutOfRangeException
                        ($"Element [{i},{i}] is located outside the matrix");
                return diagonal[i];
            }
            set
            {
                if (i < 0 || i >= Side)
                    throw new ArgumentOutOfRangeException
                        ($"Element [{i},{i}] is located outside the matrix");
                diagonal[i] = value;
                OnElementChanged(new ElementChangedEventArgs(i, i));
            }
        }
          
        private void OnElementChanged(ElementChangedEventArgs e)
        {
            ElementChanged(this, e);
        }
    }
}
