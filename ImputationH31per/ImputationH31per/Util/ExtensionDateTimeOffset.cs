using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImputationH31per.Util
{
    public static class ExtensionDateTimeOffset
    {
        public static bool EstComprisEntre(this DateTimeOffset source, DateTimeOffset minInclus, DateTimeOffset maxInclus)
        {
            return EstComprisEntreCore(source, minInclus, maxInclus);
        }

        public static DateTime? UtcDateTime(this DateTimeOffset? source)
        {
            return UtcDateTimeCore(source);
        }

        public static DateTime UtcDateTime(this DateTimeOffset source)
        {
            return UtcDateTimeCore(source);
        }

        public static long? UtcTicks(this DateTimeOffset? source)
        {
            return UtcTicksCore(source);
        }

        #region Core

        internal static bool EstComprisEntreCore(DateTimeOffset source, DateTimeOffset minInclus, DateTimeOffset maxInclus)
        {
            bool resultat = (source >= minInclus) && (source <= maxInclus);

            return resultat;
        }

        internal static DateTime? UtcDateTimeCore(DateTimeOffset? source)
        {
            DateTime? resultat = null;

            if (source.HasValue)
            {
                resultat = UtcDateTimeCore(source.Value);
            }

            return resultat;
        }

        internal static DateTime UtcDateTimeCore(DateTimeOffset source)
        {
            DateTime resultat = source.UtcDateTime;

            return resultat;
        }

        internal static long? UtcTicksCore(DateTimeOffset? source)
        {
            long? resultat = null;

            if (source.HasValue)
            {
                resultat = source.Value.UtcTicks;
            }

            return resultat;
        }

        #endregion Core
    }
}