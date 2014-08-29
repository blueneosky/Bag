using System;
using System.Collections.Generic;

namespace FastBuildGen.Common
{
    public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _source;

        public ReadOnlyDictionary(IDictionary<TKey, TValue> source)
        {
            _source = source;
        }

        public bool ContainsKey(TKey key)
        {
            return _source.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get { return _source.Keys; }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _source.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values
        {
            get { return _source.Values; }
        }

        public TValue this[TKey key]
        {
            get
            {
                return _source[key];
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _source.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _source.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _source.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                return this[key];
            }
            set
            {
                throw ReadOnlyException;
            }
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            throw ReadOnlyException;
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw ReadOnlyException;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            throw ReadOnlyException;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw ReadOnlyException;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw ReadOnlyException;
        }

        private static Exception ReadOnlyException
        {
            get { return new NotSupportedException("ReadOnlyDictionnary means 'read only' ..."); }
        }
    }
}