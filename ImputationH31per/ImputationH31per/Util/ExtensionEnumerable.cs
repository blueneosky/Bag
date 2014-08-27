using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace ImputationH31per.Util
{
    public static class ExtensionEnumerable
    {
        public static bool Empty<TSource>(this IEnumerable<TSource> source)
        {
            bool resultat = false == source.Any();

            return resultat;
        }

        public static IEnumerable<TSource> Execute<TSource>(this IEnumerable<TSource> source)
        {
            IEnumerable<TSource> resultat = source as ICollection<TSource>;
            if (resultat == null)
                resultat = source.ToArray();

            return resultat;
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (TSource item in source)
            {
                action(item);
            }
        }

        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            int resultat = -1;

            int index = -1;
            IEnumerator<TSource> enumerator = source.GetEnumerator();
            while ((resultat == -1) && (enumerator.MoveNext()))
            {
                index++;
                if (Object.Equals(value, enumerator.Current))
                {
                    resultat = index;
                }
            }

            return resultat;
        }

        public static IEnumerable<string> NaturalOrderBy(this IEnumerable<string> source)
        {
            return NaturalOrderByCore(source, t => t, false);
        }

        public static IOrderedEnumerable<TSource> NaturalOrderBy<TSource, TKey>(this IEnumerable<TSource> source)
        {
            return NaturalOrderByCore(source, e => e.ToString(), false);
        }

        public static IOrderedEnumerable<TSource> NaturalOrderBy<TSource>(this IEnumerable<TSource> source, Func<TSource, string> keySelector)
        {
            return NaturalOrderByCore(source, keySelector, false);
        }

        public static IEnumerable<string> NaturalOrderByDescending(this IEnumerable<string> source)
        {
            return NaturalOrderByCore(source, t => t, true);
        }

        public static IOrderedEnumerable<TSource> NaturalOrderByDescending<TSource, TKey>(this IEnumerable<TSource> source)
        {
            return NaturalOrderByCore(source, e => e.ToString(), true);
        }

        public static IOrderedEnumerable<TSource> NaturalOrderByDescending<TSource>(this IEnumerable<TSource> source, Func<TSource, string> keySelector)
        {
            return NaturalOrderByCore(source, keySelector, true);
        }

        #region Natural Order By

        private static IComparer<string[]> _naturalOrderByComprarer;

        public static IComparer<string[]> NaturalOrderByComparer
        {
            get
            {
                if (_naturalOrderByComprarer == null)
                    _naturalOrderByComprarer = new NaturalOrderByComparerClass();
                return _naturalOrderByComprarer;
            }
        }

        private static IOrderedEnumerable<TSource> NaturalOrderByCore<TSource>(IEnumerable<TSource> source, Func<TSource, string> keySelector, bool byDescending)
        {
            IEnumerable<KeyValuePair<TSource, string[]>> data = source.Select(i => ObtenirNaturalOrderByData(i, keySelector));
            IOrderedEnumerable<TSource> resultat = NaturalOrderByCore(data, byDescending);

            return resultat;
        }

        private static IOrderedEnumerable<TSource> NaturalOrderByCore<TSource>(IEnumerable<KeyValuePair<TSource, string[]>> source, bool byDescending)
        {
            IOrderedEnumerable<TSource> resultat = null;

            IEnumerable<KeyValuePair<TSource, string[]>> data;
            if (byDescending)
            {
                data = source.OrderByDescending(e => e.Value, NaturalOrderByComparer);
            }
            else
            {
                data = source.OrderBy(e => e.Value, NaturalOrderByComparer);
            }
            resultat = data
                .Select(e => e.Key)
                .OrderBy(e => 1);

            return resultat;
        }

        private static KeyValuePair<TSource, string[]> ObtenirNaturalOrderByData<TSource>(TSource source, Func<TSource, string> keySelector)
        {
            string value = keySelector(source) ?? String.Empty;
            string[] values = Regex.Split(value, "([0-9]+)");
            KeyValuePair<TSource, string[]> resultat = new KeyValuePair<TSource, string[]>(source, values);

            return resultat;
        }

        private class NaturalOrderByComparerClass : IComparer<string[]>
        {
            public int Compare(string[] x, string[] y)
            {
                Debug.Assert(x != null && y != null);
                Debug.Assert(x.Length > 0 && y.Length > 0);

                int? resultat = null;

                int i = 0;
                bool estNumerique = false;
                while (resultat == null)
                {
                    int diffX = x.Length - i;
                    int diffY = y.Length - i;
                    if ((diffX <= 0) || (diffY <= 0))
                    {
                        resultat = diffX - diffY;
                    }
                    else
                    {
                        string sX = x[i];
                        string sY = y[i];

                        if (estNumerique)
                        {
                            long iX = Int64.Parse(sX);
                            long iY = Int64.Parse(sY);
                            long diff = iX - iY;
                            if (diff != 0)
                                resultat = diff < 0 ? -1 : 1;
                        }
                        else
                        {
                            int diff = String.Compare(sX, sY);
                            if (diff != 0)
                                resultat = diff;
                        }
                    }
                    i++;
                    estNumerique = !estNumerique;
                }

                return resultat.Value;
            }
        }

        #endregion Natural Order By
    }
}