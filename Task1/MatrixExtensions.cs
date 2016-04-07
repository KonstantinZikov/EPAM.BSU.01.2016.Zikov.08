using Microsoft.CSharp.RuntimeBinder;
using System;

namespace Task1
{
    public class MatrixExtensions
    {
        public static IMatrix<T> Add<T>(IMatrix<T> lsv, IMatrix<T> rsv)
        {
            if (lsv.Height != rsv.Height || lsv.Width != rsv.Width)
                throw new ArgumentException("Two matrix with different sizes can't be added.");
            var result = new Matrix<T>(lsv.Width,rsv.Width);
            try
            {
                for (int i = 0; i < lsv.Width; i++)
                    for (int j = 0; j < lsv.Height; j++)
                        result[i, j] = (dynamic)lsv[i, j] + (dynamic)rsv[i, j];
            }
            catch(RuntimeBinderException)
            {
                throw new InvalidOperationException
                    ($"{typeof(T)} does not support the addition operation.");
            }
            return result;
        }
    }
}
