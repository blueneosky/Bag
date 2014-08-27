using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;

namespace FastBuildGen.Common
{
    public class SortedObservableCollection<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged
    {
        #region Membres

        private readonly SortedList<TKey, TValue> _data;
        private readonly ObservableCollection<TValue> _notifyData;

        #endregion Membres

        #region ctor

        public SortedObservableCollection()
        {
            _data = new SortedList<TKey, TValue>();
            _notifyData = new ObservableCollection<TValue>();

            _notifyData.CollectionChanged += _notifyData_CollectionChanged;
        }

        public SortedObservableCollection(IComparer<TKey> comparer)
        {
            _data = new SortedList<TKey, TValue>(comparer);
            _notifyData = new ObservableCollection<TValue>();

            _notifyData.CollectionChanged += _notifyData_CollectionChanged;
        }

        ~SortedObservableCollection()
        {
            _notifyData.CollectionChanged -= _notifyData_CollectionChanged;
        }

        private void _notifyData_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifierCollectionChanged(this, e);
        }

        #endregion ctor

        #region IDictionary<TKey, TValue>

        public int Count
        {
            get { return _data.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<KeyValuePair<TKey, TValue>>)_data).IsReadOnly; }
        }

        public ICollection<TKey> Keys
        {
            get { return _data.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return _data.Values; }
        }

        public TValue this[TKey key]
        {
            get
            {
                return _data[key];
            }
            set
            {
                int index = _data.IndexOfKey(key);
                if (index >= 0)
                {
                    // existe déjà
                    _data[key] = value;
                    _notifyData[index] = value;
                }
                else
                {
                    // insertion
                    Add(key, value);
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            _data.Add(key, value);
            int index = _data.IndexOfKey(key);
            _notifyData.Insert(index, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _data.Clear();
            _notifyData.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _data.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return _data.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)_data).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            int index = _data.IndexOfKey(key);
            bool resultat = _data.Remove(key);
            if (resultat)
            {
                _notifyData.RemoveAt(index);
            }

            return resultat;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            Debug.Assert(Object.ReferenceEquals(_data[item.Key], item.Value));
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _data.TryGetValue(key, out value);
        }

        #endregion IDictionary<TKey, TValue>

        #region INotifyCollectionChanged

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void NotifierCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged.Notify(sender, e);
        }

        #endregion INotifyCollectionChanged
    }
}