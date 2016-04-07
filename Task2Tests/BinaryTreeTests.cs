using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Task2;

namespace Task2Tests
{
    [TestClass]
    public class BinaryTreeTests
    {
        int[] seq = new[] { 9, 15, 4, 21, 31, 17, 5, 0 };
        int[] pre = new[] { 9, 4, 0, 5, 15, 21, 17, 31 };
        int[] inr = new[] { 0, 4, 5, 9, 15, 17, 21, 31 };
        int[] post = new[] { 0, 5, 4, 17, 31, 21, 15, 9 };

        string[] stringSeq = new[] { "3", "4", "2", "6", "7", "5", "1", "0" };
        string[] stringPre = new[] { "3", "2", "1", "0", "4", "6", "5", "7" };
        string[] stringInr = new[] { "0", "1", "2", "3", "4", "5", "6", "7" };
        string[] stringPost = new[] { "0", "1", "2", "5", "7", "6", "4", "3" };

        [TestMethod]
        public void HeavyTest_Int32andDefaultComparer_CorrectResults()
        {
            //arrange
            var tree = new BinaryTree<int>();
            //act
            SampleTest(tree, seq, pre, inr, post);
            //assert is handled by SampleTest
        }

        [TestMethod]
        public void HeavyTest_Int32andSpecifiedComparer_CorrectResults()
        {
            //arrange
            var tree = new BinaryTree<int>(Comparer<int>.Create(CompareInt));
            //act
            SampleTest(tree, seq, pre, inr, post);
            //assert is handled by SampleTest
        }

        [TestMethod]
        public void HeavyTest_StringandDefaultComparer_CorrectResults()
        {
            //arrange
            var tree = new BinaryTree<string>();
            //act
            SampleTest(tree, stringSeq, stringPre, stringInr, stringPost);
            //assert is handled by SampleTest
        }

        [TestMethod]
        public void HeavyTest_StringandSpecifiedComparer_CorrectResults()
        {
            //arrange
            var tree = new BinaryTree<string>(Comparer<string>.Create(CompareString));           
            //act
            SampleTest(tree, stringSeq, stringPre, stringInr, stringPost);
            //assert is handled by SampleTest
        }

        [TestMethod]
        public void HeavyTest_BookDefaultComparer_CorrectResults()
        {
            //arrange
            var tree = new BinaryTree<Book>();
            List<Book> books = new List<Book>();
            foreach (var i in stringSeq)
                books.Add(new Book {Title =  i });
            List<Book> pre = new List<Book>();
            foreach (var i in stringPre)
                pre.Add(new Book { Title = i });
            List<Book> inr = new List<Book>();
            foreach (var i in stringInr)
                inr.Add(new Book { Title = i });
            List<Book> post = new List<Book>();
            foreach (var i in stringPost)
                post.Add(new Book { Title = i });
            //act
            SampleTest(tree, books, pre, inr, post);
            //assert is handled by SampleTest
        }

        [TestMethod]
        public void HeavyTest_BookSpecifiedComparer_CorrectResults()
        {
            //arrange
            var tree = new BinaryTree<Book>(Comparer<Book>.Create(CompareBook));
            List<Book> books = new List<Book>();
            foreach (var i in stringSeq)
                books.Add(new Book { Title = i });
            List<Book> pre = new List<Book>();
            foreach (var i in stringPre)
                pre.Add(new Book { Title = i });
            List<Book> inr = new List<Book>();
            foreach (var i in stringInr)
                inr.Add(new Book { Title = i });
            List<Book> post = new List<Book>();
            foreach (var i in stringPost)
                post.Add(new Book { Title = i });
            //act
            SampleTest(tree, books, pre, inr, post);
            //assert is handled by SampleTest
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void HeavyTest_PointDefaultComparer_Exception()
        {
            //arrange is skiped
            //act
            var tree = new BinaryTree<Point>();
            //assert is handled by exception
        }

        [TestMethod]
        public void HeavyTest_PointSpecifiedComparer_CorrectResults()
        {
            //arrange
            var tree = new BinaryTree<Point>(Comparer<Point>.Create(ComparePoint));
            List<Point> points = new List<Point>();
            foreach (var i in seq)
                points.Add(new Point { X = i, Y = i });
            List<Point> Ppre = new List<Point>();
            foreach (var i in pre)
                Ppre.Add(new Point { X = i, Y = i });
            List<Point> Pinr = new List<Point>();
            foreach (var i in inr)
                Pinr.Add(new Point { X = i, Y = i });
            List<Point> Ppost = new List<Point>();
            foreach (var i in post)
                Ppost.Add(new Point { X = i, Y = i });
            //act
            SampleTest(tree, points, Ppre, Pinr, Ppost);
            //assert is handled by SampleTest
        }

        private void SampleTest<T>(BinaryTree<T> tree, 
            IList<T> seq, IList<T> pre, IList<T> inr, IList<T> post)
        {
            Random r = new Random(42);     
            foreach(var el in seq)
                tree.Insert(el);
            int i = 0;
            // walk tests
            foreach(var el in tree.Preorder())
                Assert.AreEqual(pre[i++], el);
            i = 0;
            foreach (var el in tree.Inorder())
                Assert.AreEqual(inr[i++], el);
            i = 0;
            foreach (var el in tree.Postorder())
                Assert.AreEqual(post[i++], el);

            // only get
            int index = r.Next(0, seq.Count);
            Assert.AreEqual(seq[index], tree.Get(seq[index]));
            // get with contains check
            index = r.Next(0, seq.Count);
            if (tree.Contains(seq[index]))
                tree.Get(seq[index]);
            Assert.AreEqual(seq[index], tree.Get(seq[index]));
        }

        int CompareInt(int x, int y)
        {
            int result = x - y;
            if (result < 0) return -1;
            if (result == 0) return 0;
            return 1;
        }

        int CompareString(string a, string b)
        {
            return StringComparer.Ordinal.Compare(a, b);
        }

        int CompareBook(Book a, Book b)
        {
            return StringComparer.Ordinal.Compare(a?.Title, b?.Title);
        }

        int ComparePoint(Point a, Point b)
        {
            if (a == null && b == null)
                return 0;
            if (a == null)
                return 1;
            if (b == null)
                return -1;
            int result = a.X * a.Y - b.X * b.Y;
            if (result < 0) return -1;
            if (result == 0) return 0;
            return 1;
        }

    }

    class Book : IComparable<Book>
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public int CompareTo(Book other)
        {
            return StringComparer.Ordinal.Compare(Title, other?.Title);
        }

        public override bool Equals(object obj)
        {
            Book other = obj as Book;
            if (other != null)
            {
                return Title == other.Title;
            }
            return false;
        }
    }

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            Point other = obj as Point;
            if (other != null)
            {
                return X == other.X;
            }
            return false;
        }

    }
}
