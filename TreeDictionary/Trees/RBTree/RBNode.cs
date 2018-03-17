using System;

namespace TreeDictionary.Trees.RBTree
{
    public sealed class RBNode<T>
    {
        public RBNode<T> Parent;
        public RBNode<T> Left;
        public RBNode<T> Right;

        public T Info;

        public Color Color;

    }
}
