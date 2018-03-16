using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeDictionary.Trees.RBTree
{
    class RBTree<T> : ITree<T> where T : IComparable
    {
        public void Clear() => throw new NotImplementedException();
        public bool Delete(T info) => throw new NotImplementedException();
        public IEnumerator<T> GetEnumerator() => throw new NotImplementedException();
        public void Insert(T info) => throw new NotImplementedException();
        public bool Search(ref T info) => throw new NotImplementedException();
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}
