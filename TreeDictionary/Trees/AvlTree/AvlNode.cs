using System;

namespace TreeDictionary.Trees.AvlTree
{
    public sealed class AvlNode<T>
    {
        public AvlNode<T> Parent;
        public AvlNode<T> Left;
        public AvlNode<T> Right;

        public T Info;

        public Int32 Balance;
    }
}