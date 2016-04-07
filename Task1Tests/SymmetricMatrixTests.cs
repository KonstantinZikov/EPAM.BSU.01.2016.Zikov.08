using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1;

namespace Task1Tests
{
    [TestClass]
    public class SymmetricMatrixTests
    {
        [TestMethod]
        public void Constructor_SomeSize_HasExpectedSizeAndDefaultValues()
        {
            //arrange
            int side = 5;
            int expected = default(int);
            //act
            IMatrix<int> matrix = new SymmetricMatrix<int>(side);
            //assert
            Assert.AreEqual(side, matrix.Width);
            Assert.AreEqual(side, matrix.Height);
            Assert.AreEqual(side, ((SymmetricMatrix<int>)matrix).Side);
            for (int i = 0; i < side; i++)
                for (int j = 0; j < side; j++)
                    Assert.AreEqual(expected, matrix[i, j]);
        }

        [TestMethod]
        public void Indexer_SomeValue_GetterReturnsTheSameValue()
        {
            //arrange
            SymmetricMatrix<int> matrix = new SymmetricMatrix<int>
            (new []{
                new []{5, 7, 9},
                new []{  11, 3},
                new []{      2}
            });
            int expected = 17;
            //act
            matrix[1,0] = expected;
            //assert
            Assert.AreEqual(expected, matrix[1, 0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Indexer_ElementMiss_Exception()
        {
            //arrange
            Matrix<int> matrix = new Matrix<int>
            (new int[,]{
                {5, 7, 9 },
                {12, 11, 3 },
                {-5 , 17, 2}
            });
            //act
            matrix[12, 0] = 1;
            //assert is handled by exception
        }

        [TestMethod]
        public void ElementChanged_AddListener_CallOnElementChanged()
        {
            //arrange
            Matrix<int> matrix = new Matrix<int>(3, 3);
            bool flag = false;
            EventHandler<ElementChangedEventArgs> method =
                (sender, e) => flag = true;
            //act
            matrix.ElementChanged += method;
            matrix[0, 0] = 2;
            //assert
            Assert.IsTrue(flag);
        }
    }
}
