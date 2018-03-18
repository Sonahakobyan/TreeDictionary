using System;
using System.Collections;
using System.Collections.Generic;
using TreeDictionary.Trees;
using TreeDictionary.Trees.AvlTree;
using TreeDictionary.Trees.RBTree;

namespace TreeDictionary.TreeDictionary
{
    /// <summary>
    /// Dictionary implemented via binary search tree.
    /// </summary>
    /// <typeparam name="TKey">Unique TKey</typeparam>
    /// <typeparam name="TValue">TValue</typeparam>
    public class TreeDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable
    {
        /// <summary>
        /// The container.
        /// </summary>
        private ITree<DictionaryPair<TKey, TValue>> treeContainer;

        /// <summary>
        /// Create dictionary by given collection.
        /// </summary>
        /// <param name="treeContainer"> The collection</param>
        public TreeDictionary(ITree<DictionaryPair<TKey, TValue>> treeContainer)
        {
            this.treeContainer = treeContainer;
        }

        /// <summary>
        /// Create empty dictionary of given tree type
        /// </summary>
        /// <param name="treeType"> Tree type. RB or AVL</param>
        public TreeDictionary(TreeType treeType)
        {
            switch (treeType)
            {
                case TreeType.AVL:
                    this.treeContainer = new AvlTree<DictionaryPair<TKey, TValue>>();
                    break;

                case TreeType.RB:
                    this.treeContainer = new RBTree<DictionaryPair<TKey, TValue>>();
                    break;
            }
        }

        /// <summary>
        /// Return value of given key.
        /// Add a new pair with given key and value to the dictionary.
        /// If the key is invalid, generate exception.
        /// </summary>
        /// <param name="key"> The key</param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                DictionaryPair<TKey, TValue> item = new DictionaryPair<TKey, TValue>(key, default(TValue));
                Boolean contains = treeContainer.Search(ref item);
                if (contains)
                {
                    return item.Value;
                }

                throw new Exception("Invalid key");
            }

            set
            {
                DictionaryPair<TKey, TValue> item = new DictionaryPair<TKey, TValue>(key, default(TValue));
                Boolean contains = treeContainer.Search(ref item);
                if (contains)
                {
                    item.Value = value;
                }

                throw new Exception("Invalid key");
            }
        }

        /// <summary>
        /// Return all the keys of the dictionary.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                ICollection<TKey> keys = new List<TKey>();
                foreach (var pair in treeContainer)
                {
                    keys.Add(pair.Key);
                }
                return keys;
            }
        }

        /// <summary>
        /// Return all the values of the dictionary.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                ICollection<TValue> values = new List<TValue>();
                foreach (var pair in treeContainer)
                {
                    values.Add(pair.Value);
                }
                return values;
            }
        }

        /// <summary>
        /// Return count of pairs of the dictionary.
        /// </summary>
        public Int32 Count => this.treeContainer.Count;

        /// <summary>
        /// Return true if the dictionary is readonly, and false otherwise
        /// </summary>
        public Boolean IsReadOnly => false;

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void Add(TKey key, TValue value)
        {
            DictionaryPair<TKey, TValue> pair = new DictionaryPair<TKey, TValue>(key, value);
            this.treeContainer.Insert(pair);
        }

        /// <summary>
        /// Adds the specified item to the dictionary.
        /// </summary>
        /// <param name="item">The pair</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes all keys and values from the dictionary.
        /// </summary>
        public void Clear()
        {
            this.treeContainer.Clear();
        }

        /// <summary>
        /// True if the dictionary contains the specified element; otherwise, false.
        /// </summary>
        /// <param name="item">The pair</param>
        /// <returns></returns>
        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.ContainsKey(item.Key);
        }

        /// <summary>
        /// True if the dictionary contains an element with the specified key; otherwise, false.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns></returns>
        public Boolean ContainsKey(TKey key)
        {
            DictionaryPair<TKey, TValue> pair = new DictionaryPair<TKey, TValue>(key, default(TValue));
            Boolean contains = this.treeContainer.Search(ref pair);

            return contains;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex)
        {
            foreach (var pair in treeContainer)
            {
                if (arrayIndex >= array.Length || arrayIndex < 0)
                {
                    return;
                }
                array[arrayIndex] = new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
            }
        }


        /// <summary>
        /// Returns an enumerator that iterates through the dictionary.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (DictionaryPair<TKey, TValue> item in this.treeContainer)
            {
                yield return new KeyValuePair<TKey, TValue>(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Removes the value with the specified key from the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns></returns>
        public Boolean Remove(TKey key)
        {
            return this.treeContainer.Delete(new DictionaryPair<TKey, TValue>(key, default(TValue)));
        }

        /// <summary>
        /// Removes the value with the specified item from the dictionary
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Remove(item.Key);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">TKey</param>
        /// <param name="value">TValue</param>
        /// <returns></returns>
        public Boolean TryGetValue(TKey key, out TValue value)
        {
            try
            {
                value = this[key];
                return true;
            }
            catch
            {
                value = default(TValue);
                return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.treeContainer.GetEnumerator();
        }
    }
}