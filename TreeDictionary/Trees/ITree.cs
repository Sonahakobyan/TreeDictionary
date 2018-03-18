using System;
using System.Collections.Generic;

namespace TreeDictionary.Trees
{
    /// <summary>
    /// Main definition of binary search tree.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITree<T> : IEnumerable<T> where T : IComparable
    {
        Int32 Count { get;}
        void Insert(T info);

        Boolean Delete(T info);

        Boolean Search(ref T info);

        void Clear();
    }
}