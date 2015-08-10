using System;
using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class StringExtension
    {
        public static string NewUnique(this string baseName, IEnumerable<string> excluded)
        {
            const char ConstSplitter = '_';

            excluded = excluded ?? new HashSet<string>();
            baseName = baseName ?? String.Empty;
            HashSet<string> set = excluded as HashSet<string>;
            if (set == null)
                set = excluded.ToHashSet();

            int count = 1;
            string[] splitted = baseName.Split(ConstSplitter);
            if (splitted.Length > 1)
            {
                string valueText = splitted[splitted.Length - 1];
                int value;
                if (Int32.TryParse(valueText, out value))
                {
                    baseName = baseName.Substring(0, baseName.Length - valueText.Length - 1);
                    count = value + 1;
                }
            }

            string result;
            while (set.Contains(result = String.Concat(baseName, ConstSplitter, count++))) ;

            return result;
        }

        public static string Unique(this string name, IEnumerable<string> excluded)
        {
            string result = name;

            excluded = excluded ?? new HashSet<string>();
            name = name ?? String.Empty;
            HashSet<string> set = excluded as HashSet<string>;
            if (set == null)
                set = excluded.ToHashSet();

            if (set.Contains(result))
            {
                result = NewUnique(name, set);
            }

            return result;
        }
    }
}