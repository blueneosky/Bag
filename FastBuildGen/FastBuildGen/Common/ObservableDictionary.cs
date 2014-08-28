using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;

namespace FastBuildGen.Common
{
    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region Membres

        private readonly Dictionary<TKey, TValue> _data;

        #endregion Membres

        #region ctor

        public ObservableDictionary()
        {
            _data = new Dictionary<TKey, TValue>();
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
                if (_data.ContainsKey(key))
                {
                    // existe déjà
                    TValue oldValue = _data[key];
                    _data[key] = value;
                    NotifierCollectionChanged(new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Replace
                        , new KeyValuePair<TKey, TValue>(key, value)
                        , new KeyValuePair<TKey, TValue>(key, oldValue)));
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
            NotifierCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value)));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _data.Clear();
            NotifierCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
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
            TValue value;
            bool resultat = _data.TryGetValue(key, out value);
            if (resultat)
            {
                resultat = _data.Remove(key);
                Debug.Assert(resultat);
                NotifierCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, value)));
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

        private void NotifierCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged.Notify(this, e);
        }

        #endregion INotifyCollectionChanged
    }

    #region ObservableDictionaryExtension

    public static class ObservableDictionaryExtension
    {
        public static NotifyCollectionChangedEventArgs TranslateObservableDictionaryEventArgs<TKey, TValue, TItem>(this NotifyCollectionChangedEventArgs source, Func<KeyValuePair<TKey, TValue>, TItem> translator)
        {
            NotifyCollectionChangedEventArgs result = source;

            IList oldItems = source.OldItems;
            IList newItems = source.NewItems;

            if (oldItems != null)
            {
                oldItems = oldItems
                    .OfType<KeyValuePair<TKey, TValue>>()
                    .Select(translator)
                    .ToList();
            }
            if (newItems != null)
            {
                newItems = newItems
                    .OfType<KeyValuePair<TKey, TValue>>()
                    .Select(translator)
                    .ToList();
            }

            switch (source.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    result = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItems);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    result = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItems);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    result = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItems, oldItems);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    result = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                    break;

                case NotifyCollectionChangedAction.Move:
                default:
                    Trace.Fail("Non managed case.");
                    break;
            }

            return result;
        }
    }

    #endregion ObservableDictionaryExtension
}