using System;

namespace TreeDictionary.Trees.RBTree
{
    /// <summary>
    /// The RedBlackNode class encapsulates a node in the tree.
    /// Each node has linkes to children and parents.
    /// Each node is either red or black.
    /// </summary>
    /// <typeparam name="T"> Generic type of info</typeparam>
    public sealed class RBNode<T> where T : IComparable
    {
        private RBNode<T> left;
        private RBNode<T> right;
        private RBNode<T> parent;

        public RBNode<T> Parent
        {
            get
            {
                return parent ?? NIL;
            }
            set
            {
                parent = value;
            }
        }

        public RBNode<T> Left
        {
            get
            {
                return left ?? NIL;
            }
            set
            {
                left = value;
            }
        }

        public RBNode<T> Right
        {
            get
            {
                return right ?? NIL;
            }
            set
            {
                right = value;
            }
        }

        public static RBNode<T> NIL;

        /// <summary>
        /// Generic object held by each node
        /// </summary>
        public T Info;

        public Color Color;

        public RBNode()
        {
            Parent = NIL;
            Left = NIL;
            Right = NIL;
        }

        static RBNode()
        {
            NIL = new RBNode<T>
            {
                Color = Color.Black,
                Left = NIL,
                Right = NIL,
                Parent = NIL,
            };
        }

    }
}
