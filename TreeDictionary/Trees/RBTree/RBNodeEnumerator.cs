﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace TreeDictionary.Trees.RBTree
{
    public sealed class RBNodeEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private RBNode<T> root;
        private Action _action;
        private RBNode<T> current;
        private RBNode<T> right;

        public RBNodeEnumerator(RBNode<T> root)
        {
            right = this.root = root;
            _action = this.root == null && this.root != RBNode<T>.NIL ? Action.End : Action.Right;
        }

        public Boolean MoveNext()
        {
            switch (_action)
            {
                case Action.Right:
                    current = right;
                    while (current.Left != null && current.Left != RBNode<T>.NIL)
                    {
                        current = current.Left;
                    }

                    right = current.Right;
                    _action = right != null && right != RBNode<T>.NIL ? Action.Right : Action.Parent;

                    return true;

                case Action.Parent:
                    while (current.Parent != null && current.Parent != RBNode<T>.NIL)
                    {
                        RBNode<T> previous = current;

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

