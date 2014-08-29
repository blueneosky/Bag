using System.Collections.Generic;
using System;

namespace FastBuildGen.Common
{
    public static class ExtensionIDictionary
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            foreach (KeyValuePair<TKey, TValue> item in items)
            {
                source.Add(item.Key, item.Value);
            }
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<TValue> values, Func<TValue, TKey> keySelector)
        {
            foreach (TValue v in values)
            {
                source.Add(keySelector(v), v);
            }
        }
    }
}