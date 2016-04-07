using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1;

namespace Task1Tests
{
    [TestClass]
    public class SquareMatrixTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NotSquareArray_Exception()
        {
            //arrange is skiped
            //act
            Matrix<int> matrix = new SquareMatrix<int>
            (new int[,]{
                {5, 7},
                {12, 11},
                {-5 , 17}
            });
            //assert is handled by exception
        }

        [TestMethod]
        public void Side_Nothing_ActualSide()
        {
            //arrange is skiped
            SquareMatrix<int> matrix = new SquareMatrix<int>
            (new int[,]{
                {5,  7,  3},
                {12, 11, 3},
                {-5, 17, 3}
            });
            int expected = 3;
            //act
            int actual = matrix.Side;
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
