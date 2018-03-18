using System;
using System.Collections;
using System.Collections.Generic;

namespace TreeDictionary.Trees.AvlTree
{
    /// <summary>
    /// AVL tree implementation
    /// </summary>
    /// <typeparam name="T">Generic type of info</typeparam>
    public class AvlTree<T> : ITree<T> where T : IComparable
    {
        /// <summary>
        /// The root of the tree
        /// </summary>
        private AvlNode<T> root;

        /// <summary>
        /// The count of nodes
        /// </summary>
        public Int32 Count { get; private set; }

        /// <summary>
        /// Insert the given node in the tree
        /// </summary>
        /// <param name="info"></param>
        public void Insert(T info)
        {
            if (this.root == null)
            {
                this.root = new AvlNode<T>
                {
                    Info = info
                };
            }
            else
            {
                AvlNode<T> node = this.root;

                while (node != null)
                {
                    Int32 compare = info.CompareTo(node.Info);

                    if (compare < 0)
                    {
                        AvlNode<T> left = node.Left;

                        if (left == null)
                        {
                            node.Left = new AvlNode<T> { Info = info, Parent = node };

                            this.InsertBalance(node, 1);

                            return;
                        }
                        else
                        {
                            node = left;
                        }
                    }
                    else if (compare > 0)
                    {
                        AvlNode<T> right = node.Right;

                        if (right == null)
                        {
                            node.Right = new AvlNode<T> { Info = info, Parent = node };

                            this.InsertBalance(node, -1);

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

        /// <summary>
        /// Return false if the tree dosn't contain a node with given info.
        /// Otherwise delete the node and return true.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Boolean Delete(T info)
        {
            AvlNode<T> node = this.root;

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
                    AvlNode<T> left = node.Left;
                    AvlNode<T> right = node.Right;

                    if (left == null)
                    {
                        if (right == null)
                        {
                            if (node == root)
                            {
                                this.root = null;
                            }
                            else
                            {
                                AvlNode<T> parent = node.Parent;

                                if (parent.Left == node)
                                {
                                    parent.Left = null;

                                    this.DeleteBalance(parent, -1);
                                }
                                else
                                {
                                    parent.Right = null;

                                    this.DeleteBalance(parent, 1);
                                }
                            }
                        }
                        else
                        {
                            Replace(node, right);

                            this.DeleteBalance(node, 0);
                        }
                    }
                    else if (right == null)
                    {
                        Replace(node, left);

                        this.DeleteBalance(node, 0);
                    }
                    else
                    {
                        AvlNode<T> successor = right;

                        if (successor.Left == null)
                        {
                            AvlNode<T> parent = node.Parent;

                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;

                            if (left != null)
                            {
                                left.Parent = successor;
                            }

                            if (node == root)
                            {
                                this.root = successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }

                            this.DeleteBalance(successor, 1);
                        }
                        else
                        {
                            while (successor.Left != null)
                            {
                                successor = successor.Left;
                            }

                            AvlNode<T> parent = node.Parent;
                            AvlNode<T> successorParent = successor.Parent;
                            AvlNode<T> successorRight = successor.Right;

                            if (successorParent.Left == successor)
                            {
                                successorParent.Left = successorRight;
                            }
                            else
                            {
                                successorParent.Right = successorRight;
                            }

                            if (successorRight != null)
                            {
                                successorRight.Parent = successorParent;
                            }

                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;
                            successor.Right = right;
                            right.Parent = successor;

                            if (left != null)
                            {
                                left.Parent = successor;
                            }

                            if (node == root)
                            {
                                root = successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }

                            this.DeleteBalance(successorParent, -1);
                        }
                    }
                    Count--;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Return false if the tree dosn't contain a node with given info.
        /// Otherwise return info by ref parameter and return true.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Boolean Search(ref T info)
        {
            AvlNode<T> node = this.root;

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

        /// <summary>
        /// Removes all nodes from the tree
        /// </summary>
        public void Clear()
        {
            this.root = null;
            Count = 0;
        }

        /// <summary>
        /// Fix tree balance after insert operation
        /// </summary>
        /// <param name="node"></param>
        /// <param name="balance"></param>
        private void InsertBalance(AvlNode<T> node, Int32 balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 0)
                {
                    return;
                }
                else if (balance == 2)
                {
                    if (node.Left.Balance == 1)
                    {
                        this.RotateRight(node);
                    }
                    else
                    {
                        this.RotateLeftRight(node);
                    }

                    return;
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance == -1)
                    {
                        this.RotateLeft(node);
                    }
                    else
                    {
                        this.RotateRightLeft(node);
                    }

                    return;
                }

                AvlNode<T> parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? 1 : -1;
                }

                node = parent;
            }
        }

        /// <summary>
        /// Fix tree balance after delete operation
        /// </summary>
        /// <param name="node"></param>
        /// <param name="balance"></param>
        private void DeleteBalance(AvlNode<T> node, Int32 balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 2)
                {
                    if (node.Left.Balance >= 0)
                    {
                        node = this.RotateRight(node);

                        if (node.Balance == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = this.RotateLeftRight(node);
                    }
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance <= 0)
                    {
                        node = this.RotateLeft(node);

                        if (node.Balance == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = this.RotateRightLeft(node);
                    }
                }
                else if (balance != 0)
                {
                    return;
                }

                AvlNode<T> parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? -1 : 1;
                }

                node = parent;
            }
        }

        private AvlNode<T> RotateLeft(AvlNode<T> node)
        {
            AvlNode<T> right = node.Right;
            AvlNode<T> rightLeft = right.Left;
            AvlNode<T> parent = node.Parent;

            right.Parent = parent;
            right.Left = node;
            node.Right = rightLeft;
            node.Parent = right;

            if (rightLeft != null)
            {
                rightLeft.Parent = node;
            }

            if (node == root)
            {
                root = right;
            }
            else if (parent.Right == node)
            {
                parent.Right = right;
            }
            else
            {
                parent.Left = right;
            }

            right.Balance++;
            node.Balance = -right.Balance;

            return right;
        }

        private AvlNode<T> RotateRight(AvlNode<T> node)
        {
            AvlNode<T> left = node.Left;
            AvlNode<T> leftRight = left.Right;
            AvlNode<T> parent = node.Parent;

            left.Parent = parent;
            left.Right = node;
            node.Left = leftRight;
            node.Parent = left;

            if (leftRight != null)
            {
                leftRight.Parent = node;
            }

            if (node == this.root)
            {
                this.root = left;
            }
            else if (parent.Left == node)
            {
                parent.Left = left;
            }
            else
            {
                parent.Right = left;
            }

            left.Balance--;
            node.Balance = -left.Balance;

            return left;
        }

        private AvlNode<T> RotateLeftRight(AvlNode<T> node)
        {
            AvlNode<T> left = node.Left;
            AvlNode<T> leftRight = left.Right;
            AvlNode<T> parent = node.Parent;
            AvlNode<T> leftRightRight = leftRight.Right;
            AvlNode<T> leftRightLeft = leftRight.Left;

            leftRight.Parent = parent;
            node.Left = leftRightRight;
            left.Right = leftRightLeft;
            leftRight.Left = left;
            leftRight.Right = node;
            left.Parent = leftRight;
            node.Parent = leftRight;

            if (leftRightRight != null)
            {
                leftRightRight.Parent = node;
            }

            if (leftRightLeft != null)
            {
                leftRightLeft.Parent = left;
            }

            if (node == root)
            {
                root = leftRight;
            }
            else if (parent.Left == node)
            {
                parent.Left = leftRight;
            }
            else
            {
                parent.Right = leftRight;
            }

            if (leftRight.Balance == -1)
            {
                node.Balance = 0;
                left.Balance = 1;
            }
            else if (leftRight.Balance == 0)
            {
                node.Balance = 0;
                left.Balance = 0;
            }
            else
            {
                node.Balance = -1;
                left.Balance = 0;
            }

            leftRight.Balance = 0;

            return leftRight;
        }

        private AvlNode<T> RotateRightLeft(AvlNode<T> node)
        {
            AvlNode<T> right = node.Right;
            AvlNode<T> rightLeft = right.Left;
            AvlNode<T> parent = node.Parent;
            AvlNode<T> rightLeftLeft = rightLeft.Left;
            AvlNode<T> rightLeftRight = rightLeft.Right;

            rightLeft.Parent = parent;
            node.Right = rightLeftLeft;
            right.Left = rightLeftRight;
            rightLeft.Right = right;
            rightLeft.Left = node;
            right.Parent = rightLeft;
            node.Parent = rightLeft;

            if (rightLeftLeft != null)
            {
                rightLeftLeft.Parent = node;
            }

            if (rightLeftRight != null)
            {
                rightLeftRight.Parent = right;
            }

            if (node == this.root)
            {
                this.root = rightLeft;
            }
            else if (parent.Right == node)
            {
                parent.Right = rightLeft;
            }
            else
            {
                parent.Left = rightLeft;
            }

            if (rightLeft.Balance == 1)
            {
                node.Balance = 0;
                right.Balance = -1;
            }
            else if (rightLeft.Balance == 0)
            {
                node.Balance = 0;
                right.Balance = 0;
            }
            else
            {
                node.Balance = 1;
                right.Balance = 0;
            }

            rightLeft.Balance = 0;

            return rightLeft;
        }

        private static void Replace(AvlNode<T> target, AvlNode<T> source)
        {
            AvlNode<T> left = source.Left;
            AvlNode<T> right = source.Right;

            target.Balance = source.Balance;
            target.Info = source.Info;
            target.Left = left;
            target.Right = right;

            if (left != null)
            {
                left.Parent = target;
            }

            if (right != null)
            {
                right.Parent = target;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new AvlNodeEnumerator<T>(this.root);
        }
    }
}