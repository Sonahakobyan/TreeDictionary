using System;

namespace TreeDictionary.Trees.AvlTree
{
    /// <summary>
    /// The AVLNode class encapsulates a node in the tree.
    /// Each node has linkes to children and parents.
    /// Each node has balance.
    /// </summary>
    /// <typeparam name="T">Generic type of info</typeparam>
    public sealed class AvlNode<T>
    {
        public AvlNode<T> Parent;
        public AvlNode<T> Left;
        public AvlNode<T> Right;

        public T Info;

        public Int32 Balance;
    }
}