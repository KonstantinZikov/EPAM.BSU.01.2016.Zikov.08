using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1;

namespace Task1Tests
{
    [TestClass]
    public class MatrixExtensionsTests
    {
        [TestMethod]
        public void Add_TwoIntTypedMatrixWithSameSize_CorrectResult()
        {
            IMatrix<int> matrix = new SquareMatrix<int>
            (new int[,]{
                {5, 7, 9 },
                {12, 11, 3 },
                {-5 , 17, 2}
            });
            IMatrix<int> matrix2 = new SquareMatrix<int>
            (new int[,]{
                {5, 7, 9 },
                {12, 11, 3 },
                {-5 , 17, 2}
            });
            var array = new int[][]
            {
                new[]{10, 14, 18 },
                new[]{24,22,6 },
                new[]{-10,34,4 }
            };
            IMatrix<int> result = MatrixExtensions.Add(matrix, matrix2);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Assert.AreEqual(array[i][j], result[i, j]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_TwoIntTypedMatrixWithDifferentSize_Exception()
        {
            IMatrix<int> matrix = new SquareMatrix<int>
            (new int[,]{
                {5, 7, 9 },
                {12, 11, 3 },
                {-5 , 17, 2}
            });
            IMatrix<int> matrix2 = new SquareMatrix<int>
            (new int[,]{
                {5, 7},
                {12, 11},
            });
            var array = new int[][]
            {
                new[]{10, 14, 18 },
                new[]{24,22,6 },
                new[]{-10,34,4 }
            };
            IMatrix<int> result = MatrixExtensions.Add(matrix, matrix2);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Assert.AreEqual(array[i][j], result[i, j]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_TwoObjectTypedMatrix_Exception()
        {
            IMatrix<object> matrix = new SquareMatrix<object>
            (new object[,]{
                { new object() }
            });
            IMatrix<object> matrix2 = new SquareMatrix<object>
            (new object[,]{
                { new object() }
            });
            IMatrix<object> result = MatrixExtensions.Add(matrix, matrix2);
        }
    }
}
