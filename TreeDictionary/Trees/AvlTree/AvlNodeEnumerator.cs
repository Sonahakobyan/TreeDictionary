using System;
using System.Collections;
using System.Collections.Generic;

namespace TreeDictionary.Trees.AvlTree
{
    public sealed class AvlNodeEnumerator<T> : IEnumerator<T>
    {
        private AvlNode<T> root;
        private Action _action;
        private AvlNode<T> current;
        private AvlNode<T> right;

        public AvlNodeEnumerator(AvlNode<T> root)
        {
            right = this.root = root;
            _action = this.root == null ? Action.End : Action.Right;
        }

        public Boolean MoveNext()
        {
            switch (_action)
            {
                case Action.Right:
                    current = right;

                    while (current.Left != null)
                    {
                        current = current.Left;
                    }

                    right = current.Right;
                    _action = right != null ? Action.Right : Action.Parent;

                    return true;

                case Action.Parent:
                    while (current.Parent != null)
                    {
                        AvlNode<T> previous = current;

                        current = current.Parent;

                        if (current.Left == previous)
                        {
                            right = current.Right;
                            _action = right != null ? Action.Right : Action.Parent;

                            return true;
                        }
                    }

                    _action = Action.End;

                    return false;

                default:
                    return false;
            }
        }

        public void Reset()
        {
            right = root;
            _action = root == null ? Action.End : Action.Right;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T Current
        {
            get
            {
                return current.Info;
            }
        }

        Object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        private enum Action
        {
            Parent,
            Right,
            End
        }
    }
}