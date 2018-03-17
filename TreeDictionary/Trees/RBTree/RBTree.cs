using System;
using System.Collections;
using System.Collections.Generic;

namespace TreeDictionary.Trees.RBTree
{
    public class RBTree<T> : ITree<T> where T : IComparable
    {
        private RBNode<T> root;

        private readonly RBNode<T> NIL = new RBNode<T>
        {
            Color = Color.Black
        };

        public Int32 Count { get; private set; }

        public void Insert(T info)
        {
            if (this.root == null)
            {
                this.root = new RBNode<T>
                {
                    Info = info,
                    Color = Color.Black,
                    Right = NIL,
                    Left = NIL,
                    Parent = NIL
                };
            }
            else
            {
                RBNode<T> node = this.root;
                while (node != null)
                {
                    Int32 compare = info.CompareTo(node.Info);

                    if (compare < 0)
                    {
                        RBNode<T> left = node.Left;

                        if (left == null)
                        {
                            node.Left = new RBNode<T>
                            {
                                Info = info,
                                Color = Color.Red,
                                Parent = node,
                                Left = NIL,
                                Right = NIL
                            };
                            InsertFixup(node.Left);
                            return;
                        }
                        else
                        {
                            node = left;
                        }
                    }
                    else if (compare > 0)
                    {
                        RBNode<T> right = node.Right;

                        if (right == null)
                        {
                            node.Right = new RBNode<T>
                            {
                                Info = info,
                                Color = Color.Red,
                                Parent = node,
                                Left = NIL,
                                Right = NIL
                            };
                            InsertFixup(node.Right);
                            return;
                        }
                        else
                        {
                            node = right;
                        }
                    }
                    else
                    {
                        node.Info = info;
                        return;
                    }
                }
            }
            Count++;
        }

        private void InsertFixup(RBNode<T> node)
        {
            while (node.Parent.Color == Color.Red)
            {
                if (node.Parent == node.Parent.Parent.Left)
                {
                    RBNode<T> rightUncle = node.Parent.Parent.Right;
                    if (rightUncle.Color == Color.Red)
                    {
                        node.Parent.Color = Color.Black;
                        rightUncle.Color = Color.Black;
                        node.Parent.Parent.Color = Color.Red;
                        node = node.Parent.Parent;
                    }
                    else if (node == node.Parent.Right)
                    {
                        node = node.Parent;
                        this.LeftRotate(node);
                    }
                    node.Parent.Color = Color.Black;
                    node.Parent.Parent.Color = Color.Red;
                    this.RightRotate(node.Parent.Parent);
                }
                else
                {
                    RBNode<T> leftUncle = node.Parent.Parent.Left;
                    if (leftUncle.Color == Color.Red)
                    {
                        node.Parent.Color = Color.Black;
                        leftUncle.Color = Color.Black;
                        node.Parent.Parent.Color = Color.Red;
                        node = node.Parent.Parent;
                    }
                    else if (node == node.Parent.Left)
                    {
                        node = node.Parent;
                        this.RightRotate(node);
                    }
                    node.Parent.Color = Color.Black;
                    node.Parent.Parent.Color = Color.Red;
                    this.LeftRotate(node.Parent.Parent);
                }
            }
            this.root.Color = Color.Black;
        }

        public Boolean Delete(T info)
        {
            RBNode<T> node = root;

            while (node != null)
            {
                Int32 compare = info.CompareTo(node.Info);

                if (compare < 0)
                {
                    node = node.Left;
                }
                else if (compare > 0)
                {
                    node = node.Right;
                }
                else
                {
                    RBNode<T> y = node;
                    RBNode<T> x = null;
                    Color YOriginalColor = y.Color;
                    if (node.Left == this.NIL)
                    {
                        x = node.Right;
                        this.Transplant(node, node.Right);
                    }
                    else if (node.Right == this.NIL)
                    {
                        x = node.Left;
                        this.Transplant(node, node.Left);
                    }
                    else
                    {
                        y = this.TreeMin(node.Right);
                        YOriginalColor = y.Color;
                        x = y.Right;
                        if (y.Parent == node)
                        {
                            x.Parent = y;
                        }
                        else
                        {
                            this.Transplant(y, y.Right);
                            y.Right = node.Right;
                            y.Right.Parent = y;
                        }
                        this.Transplant(node, y);
                        y.Left = node.Left;
                        y.Left.Parent = y;
                        y.Color = node.Color;
                    }
                    if (YOriginalColor == Color.Black)
                    {
                        this.DeleteFixup(x);
                    }
                    this.Count--;
                    return true;
                }
            }
            return false;
        }

        private void DeleteFixup(RBNode<T> node)
        {
            while (node != this.root && node.Color == Color.Black)
            {
                if (node == node.Parent.Left)
                {
                    RBNode<T> sibling = node.Parent.Right;

                    if (sibling.Color == Color.Red)
                    {
                        sibling.Color = Color.Black;
                        node.Parent.Color = Color.Red;
                        this.LeftRotate(node.Parent);
                        sibling = node.Parent.Right;
                    }
                    if (sibling.Left.Color == Color.Black && sibling.Right.Color == Color.Black)
                    {
                        sibling.Color = Color.Red;
                        node = node.Parent;
                    }
                    else if (sibling.Right.Color == Color.Black)
                    {
                        sibling.Left.Color = Color.Black;
                        sibling.Color = Color.Red;
                        this.RightRotate(sibling);
                        sibling = node.Parent.Right;
                    }
                    sibling.Color = node.Parent.Color;
                    node.Parent.Color = Color.Black;
                    sibling.Right.Color = Color.Black;
                    this.LeftRotate(node.Parent);
                    node = this.root;
                }
                else
                {
                    RBNode<T> sibling = node.Parent.Left;

                    if (sibling.Color == Color.Red)
                    {
                        sibling.Color = Color.Black;
                        node.Parent.Color = Color.Red;
                        this.RightRotate(node.Parent);
                        sibling = node.Parent.Left;
                    }
                    if (sibling.Right.Color == Color.Black && sibling.Left.Color == Color.Black)
                    {
                        sibling.Color = Color.Red;
                        node = node.Parent;
                    }
                    else if (sibling.Left.Color == Color.Black)
                    {
                        sibling.Right.Color = Color.Black;
                        sibling.Color = Color.Red;
                        this.LeftRotate(sibling);
                        sibling = node.Parent.Left;
                    }
                    sibling.Color = node.Parent.Color;
                    node.Parent.Color = Color.Black;
                    sibling.Left.Color = Color.Black;
                    this.RightRotate(node.Parent);
                    node = this.root;
                }
                node.Color = Color.Black;
            }
        }

        public bool Search(ref T info)
        {
            RBNode<T> node = this.root;

            while (node != null)
            {
                Int32 compare = info.CompareTo(node.Info);

                if (compare < 0)
                {
                    node = node.Left;
                }
                else if (compare > 0)
                {
                    node = node.Right;
                }
                else
                {
                    info = node.Info;

                    return true;
                }
            }

            info = default(T);

            return false;

        }

        private void LeftRotate(RBNode<T> node)
        {
            RBNode<T> right = node.Right;
            node.Right = right.Left;
            if (right.Left != this.NIL)
            {
                right.Left.Parent = node;
            }
            right.Parent = node.Parent;
            if (node.Parent == this.NIL)
            {
                this.root = right;
            }
            else if (node == node.Parent.Left)
            {
                node.Parent.Left = right;
            }
            else
            {
                node.Parent.Right = right;
                right.Left = node;
                node.Parent = right;
            }
        }

        private void RightRotate(RBNode<T> node)
        {
            RBNode<T> left = node.Left;
            node.Left = left.Right;
            if (left.Right != this.NIL)
            {
                left.Right.Parent = node;
            }
            left.Parent = node.Parent;
            if (node.Parent == this.NIL)
            {
                this.root = left;
            }
            else if (node == node.Parent.Right)
            {
                node.Parent.Right = left;
            }
            else
            {
                node.Parent.Left = left;
                left.Right = node;
                node.Parent = left;
            }
        }

        private void Transplant(RBNode<T> u, RBNode<T> v)
        {
            if (u.Parent == this.NIL)
            {
                this.root = v;
            }
            else if (u == u.Parent.Left)
            {
                u.Parent.Left = v;
            }
            else
            {
                u.Parent.Right = v;
            }
            v.Parent = u.Parent;
        }

        private RBNode<T> TreeMin(RBNode<T> root)
        {
            RBNode<T> min = root;
            while (min.Left != this.NIL)
            {
                min = min.Left;
            }
            return min;
        }

        public void Clear()
        {
            this.root = null;
            this.Count = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new RBNodeEnumerator<T>(root);
        }
    }
}
