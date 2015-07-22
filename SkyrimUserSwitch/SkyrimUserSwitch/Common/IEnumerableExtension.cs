using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Generic
{
    public static class IEnumerableExtension
    {
        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source)
        {
            return new HashSet<TSource>(source);
        }
    }
}