using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeDictionary.Trees.RBTree
{
    class RBNode<T>
    {
        public RBNode<T> Parent;
        public RBNode<T> Left;
        public RBNode<T> Right;

        public T Info;
        public RBNodeColor Color;

    }
}
