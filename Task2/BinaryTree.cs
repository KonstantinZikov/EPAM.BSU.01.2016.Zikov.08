using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class BinaryTree<T>
    {
        //fields       
        Comparison<T> comparison;
        private Node<T> root;
        private T caсhe;


        private class Node<U>
        {
            public U Value { get; set; }
            public Node<U> Right { get; set; }
            public Node<U> Left { get; set; }
        }

        //constructors
        public BinaryTree()
        {
            if (typeof(T).GetInterface("IComparable")!= null ||
                typeof(T).GetInterface("IComparable`1") != null)
                comparison = Comparer<T>.Default.Compare;
            else
            {
                throw new InvalidOperationException("Target type must implement IComparable or IComparable<T>.");
            }
        }

        public BinaryTree(IComparer<T> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException($"{nameof(comparer)} is null.");
            comparison = comparer.Compare;
        }

        public BinaryTree(Comparison<T> comparison)
        {
            if (comparison == null)
                throw new ArgumentNullException($"{nameof(comparison)} is null.");
            this.comparison = comparison;
        }

        //main methods
        public void Insert(T value)
        {
            var inserted = new Node<T> { Value = value };
            if (root == null)
            {
                root = inserted;
                return;
            }
            Node<T> node = root;
            while (true)
            {
                int result = comparison(node.Value, value);
                if (result == 0)
                {
                    node.Value = inserted.Value;
                    return;
                }
                if (result < 0)
                {
                    if (node.Right == null)
                    {
                        node.Right = inserted;
                        return;
                    }
                    node = node.Right;
                }
                if (result > 0)
                {
                    if (node.Left == null)
                    {
                        node.Left = inserted;
                        return;
                    }
                    node = node.Left;
                }
            }         
        }

        public bool Contains(T value)
        {
            if (comparison(caсhe, value) == 0)
                return true;
            Node<T> node = root;
            while ((node != null))
            {
                int result = comparison(node.Value, value);
                if (result == 0)
                {
                    caсhe = node.Value;
                    return true;
                }
                if (result > 0) node = node.Left;
                if (result < 0) node = node.Right;
            }
            return false;
        }

        public T Get(T value)
        {
            if (Contains(value))
                return caсhe;      
            throw new InvalidOperationException
                ("Tree doesn't contain the specified value");
        }

        //walk methods
        public IEnumerable<T> Preorder()
            => PreorderRecursive(root);

        public IEnumerable<T> Inorder()
            => InorderRecursive(root);

        public IEnumerable<T> Postorder()
            => PostorderRecursive(root);

        private IEnumerable<T> PreorderRecursive(Node<T> node)
        {
            yield return node.Value;
            if (node.Left != null)
                foreach (var value in PreorderRecursive(node.Left))
                    yield return value;
            if (node.Right != null)
                foreach (var value in PreorderRecursive(node.Right))
                    yield return value;
        }
              
        private IEnumerable<T> InorderRecursive(Node<T> node)
        {
            if (node.Left != null)
                foreach (var value in InorderRecursive(node.Left))
                    yield return value;
            yield return node.Value;
            if (node.Right != null)
                foreach (var value in InorderRecursive(node.Right))
                    yield return value;
        }
       
        private IEnumerable<T> PostorderRecursive(Node<T> node)
        {
            if (node.Left != null)
                foreach (var value in PostorderRecursive(node.Left))
                    yield return value;
            if (node.Right != null)
                foreach (var value in PostorderRecursive(node.Right))
                    yield return value;
            yield return node.Value;
        }        
    }
}
