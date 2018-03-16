﻿using System;
using System.Collections.Generic;

namespace TreeDictionary.Trees
{
    public interface ITree<T> : IEnumerable<T> where T : IComparable
    {
        void Insert(T info);

        Boolean Delete(T info);

        Boolean Search(ref T info);

        void Clear();
    }
}