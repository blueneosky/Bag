using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common
{
    public static class ExtensionIEnumerable
    {
        public static IEnumerable<TSource> Execute<TSource>(this IEnumerable<TSource> source)
        {
            IEnumerable<TSource> result = source as ICollection<TSource>;
            if (result == null)
                result = source.ToArray();

            return result;
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (TSource item in source)
            {
                action(item);
            }
        }

        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            int result = -1;

            int index = 0;
            IEnumerator<TSource> enumerator = source.GetEnumerator();
            while ((result == -1) && enumerator.MoveNext())
            {
                TSource current = enumerator.Current;
                if (Object.Equals(current, element))
                {
                    result = index;
                }
                index++;
            }

            return result;
        }

        public static TSource NextOrPrevious<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            TSource result = default(TSource);
            TSource previous = default(TSource);

            IEnumerator<TSource> enumerator = source.GetEnumerator();
            bool continueSearch = true; ;
            while (continueSearch && enumerator.MoveNext())
            {
                TSource current = enumerator.Current;
                if (Object.Equals(current, element))
                {
                    continueSearch = false;
                }
                else
                {
                    previous = current;
                }
            }
            if (enumerator.MoveNext())
            {
                result = enumerator.Current;
            }
            else if (false == continueSearch)
            {
                result = previous;
            }

            return result;
        }

        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source)
        {
            return new HashSet<TSource>(source);
        }

        public static string UniqName(this IEnumerable<string> source, string baseName)
        {
            HashSet<string> set = new HashSet<string>(source);

            string name = baseName;
            bool used = set.Contains(name);

            int index = 0;
            while (used)
            {
                name = baseName + index++;
                used = set.Contains(name);
            }

            return name;
        }
    }
}