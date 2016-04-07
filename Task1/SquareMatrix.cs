using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class SquareMatrix<T> : Matrix<T>, IMatrix<T>
    {
        public int Side { get { return Width; }}
        public SquareMatrix(int side): base(side, side){}
        public SquareMatrix(T[,] matrix) : base(matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new ArgumentException("matrix width must be equal to the height.");
        }
    }
}
