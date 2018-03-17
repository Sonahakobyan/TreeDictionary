using System;
using System.Collections;
using System.Collections.Generic;
using TreeDictionary.Trees;
using TreeDictionary.Trees.AvlTree;
using TreeDictionary.Trees.RBTree;

namespace TreeDictionary.TreeDictionary
{
    public class TreeDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable
    {
        private ITree<DictionaryPair<TKey, TValue>> treeContainer;

        public TreeDictionary(ITree<DictionaryPair<TKey, TValue>> treeContainer)
        {
            this.treeContainer = treeContainer;
        }

        public TreeDictionary(TreeType treeType)
        {
            switch (treeType)
            {
                case TreeType.AVL:
                    this.treeContainer = new AvlTree<DictionaryPair<TKey, TValue>>();
                    break;

                case TreeType.RB:
                    treeContainer = new RBTree<DictionaryPair<TKey, TValue>>();
                    break;
            }
        }

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

        public Int32 Count => this.treeContainer.Count;
        public Boolean IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            DictionaryPair<TKey, TValue> pair = new DictionaryPair<TKey, TValue>(key, value);
            this.treeContainer.Insert(pair);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            this.treeContainer.Clear();
        }

        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.ContainsKey(item.Key);
        }

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

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return GetEnumerator();
        }

        public Boolean Remove(TKey key)
        {
            return this.treeContainer.Delete(new DictionaryPair<TKey, TValue>(key, default(TValue)));
        }

        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Remove(item.Key);
        }

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