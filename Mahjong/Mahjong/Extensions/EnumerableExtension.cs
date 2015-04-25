using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Execute<TSource>(this IEnumerable<TSource> source)
        {
            if (source is ICollection<TSource>)
                return source;

            return source.ToArray();
        }

        public static IReadOnlyDictionary<TKey, TSource> ToReadOnlyDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentException();

            IDictionary<TKey, TSource> dictionary = source.ToDictionary(keySelector);

            return new ReadOnlyDictionary<TKey, TSource>(dictionary);
        }

        public static IReadOnlyDictionary<TKey, TElement> ToReadOnlyDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            if (source == null)
                throw new ArgumentException();

            IDictionary<TKey, TElement> dictionary = source.ToDictionary(keySelector, elementSelector);

            return new ReadOnlyDictionary<TKey, TElement>(dictionary);
        }

        public static void Foreach<TElement>(this IEnumerable<TElement> source, Action<TElement> elementAction)
        {
            if (source == null)
                throw new ArgumentException();

            foreach (TElement element in source)
            {
                elementAction(element);
            }
        }
    }
}