using System;
using System.Collections.Generic;

namespace TreeDictionary.TreeDictionary
{
    public class DictionaryPair<TKey, TValue> : IComparable<TKey>, IComparable where TKey : IComparable
    {
        public TKey Key;
        public TValue Value;

        public DictionaryPair()
        {
            Key = default(TKey);
            Value = default(TValue);
        }

        public DictionaryPair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public Int32 CompareTo(TKey other)
        {
            return Key.CompareTo(other);
        }

        public Int32 CompareTo(Object obj)
        {
            if (obj is DictionaryPair<TKey, TValue> pair)
            {
                return CompareTo(pair.Key);
            }
            else if (obj is KeyValuePair<TKey, TValue> kvPair)
            {
                return CompareTo(kvPair.Key);
            }

            throw new Exception("Obj must be TKey");
        }
    }
}