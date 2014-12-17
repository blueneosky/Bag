using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImputationH31per.Util
{
    public static class ExtensionIDictionary
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dest, IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            AddRangeCore(dest, source);
        }

        #region Core

        internal static void AddRangeCore<TKey, TValue>(IDictionary<TKey, TValue> dest, IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            foreach (KeyValuePair<TKey, TValue> kvp in source)
            {
                dest.Add(kvp);
            }
        }

        #endregion Core
    }
}