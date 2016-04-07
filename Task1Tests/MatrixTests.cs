using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1;

namespace Task1Tests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void Constructor_SomeSize_HasExpectedSizeAndDefaultValues()
        {
            //arrange
            int width = 5;
            int height = 5;            
            int expected = default(int);
            //act
            Matrix<int> matrix = new Matrix<int>(width, height);
            //assert
            Assert.AreEqual(width, matrix.Width);
            Assert.AreEqual(height, matrix.Height);
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    Assert.AreEqual(expected, matrix[i, j]);           
        }

        [TestMethod]
        public void Indexer_SomeValue_GetterReturnsTheSameValue()
        {
            //arrange
            Matrix<int> matrix = new Matrix<int>
            (new int[,]{
                {5, 7, 9 },
                {12, 11, 3 },
                {-5 , 17, 2}
            });
            int expected = 17;
            //act
            matrix[1, 0] = expected;
            //assert
            Assert.AreEqual(expected, matrix[1,0]);
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
