using System;
using System.Collections.Generic;

namespace TreeDictionary.TreeDictionary
{
    /// <summary>
    /// Definition of Key/Value Pair.
    /// </summary>
    /// <typeparam name="TKey">TKey</typeparam>
    /// <typeparam name="TValue">TValue</typeparam>
    public class DictionaryPair<TKey, TValue> : IComparable<TKey>, IComparable where TKey : IComparable
    {
        public TKey Key;
        public TValue Value;

        /// <summary>
        /// Creates a new pair with default values.
        /// </summary>
        public DictionaryPair()
        {
            Key = default(TKey);
            Value = default(TValue);
        }

        /// <summary>
        /// Creates a new pair with given key and value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public DictionaryPair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Compares the current key to another key.
        /// </summary>
        /// <param name="other"> The given key</param>
        /// <returns></returns>
        public Int32 CompareTo(TKey other)
        {
            return Key.CompareTo(other);
        }

        /// <summary>
        /// Compares the current key to a comparable element.
        /// </summary>
        /// <param name="obj"> The given element</param>
        /// <returns></returns>
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