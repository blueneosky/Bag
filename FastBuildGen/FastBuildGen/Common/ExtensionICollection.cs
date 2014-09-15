using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common
{
    public static class ExtensionICollection
    {
        public static void AddRange<TSource>(this ICollection<TSource> source, IEnumerable<TSource> items)
        {
            foreach (TSource item in items)
            {
                source.Add(item);
            }
        }

        public static void RemoveRange<TSource>(this ICollection<TSource> source, IEnumerable<TSource> items)
        {
            foreach (TSource item in items)
            {
                source.Remove(item);
            }
        }
    }
}