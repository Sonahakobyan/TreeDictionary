using System;
using System.Collections;
using System.Collections.Generic;
using TreeDictionary.Trees;
using TreeDictionary.Trees.AvlTree;

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
                    treeContainer = new AvlTree<DictionaryPair<TKey, TValue>>();
                    break;

                case TreeType.RB:
                    //treeContainer = new AvlTree<DictionaryPair<TKey, TValue>>();
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
                throw new NotImplementedException();
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Int32 Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(TKey key, TValue value)
        {
            DictionaryPair<TKey, TValue> pair = new DictionaryPair<TKey, TValue>(key, value);
            treeContainer.Insert(pair);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            treeContainer.Clear();
        }

        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public Boolean ContainsKey(TKey key)
        {
            DictionaryPair<TKey, TValue> pair = new DictionaryPair<TKey, TValue>(key, default(TValue));
            Boolean contains = treeContainer.Search(ref pair);

            return contains;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return GetEnumerator();
        }

        public Boolean Remove(TKey key)
        {
            return treeContainer.Delete(new DictionaryPair<TKey, TValue>(key, default(TValue)));
        }

        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
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
            return treeContainer.GetEnumerator();
        }
    }
}