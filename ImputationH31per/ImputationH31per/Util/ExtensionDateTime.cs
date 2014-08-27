using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImputationH31per.Util
{
    public static class ExtensionDateTime
    {
        public static DateTimeOffset? AsDateTimeOffset(this DateTime? sourceUtc)
        {
            return AsDateTimeOffsetCore(sourceUtc);
        }

        public static DateTimeOffset AsDateTimeOffset(this DateTime sourceUtc)
        {
            return AsDateTimeOffsetCore(sourceUtc);
        }

        public static DateTimeOffset? ToDateTimeOffset(this DateTime? source)
        {
            return ToDateTimeOffsetCore(source);
        }

        public static DateTimeOffset ToDateTimeOffset(this DateTime source)
        {
            return ToDateTimeOffsetCore(source);
        }

        #region Core

        internal static DateTimeOffset? AsDateTimeOffsetCore(DateTime? sourceUtc)
        {
            DateTimeOffset? resultat = null;

            if (sourceUtc.HasValue)
            {
                resultat = AsDateTimeOffsetCore(sourceUtc.Value);
            }

            return resultat;
        }

        internal static DateTimeOffset AsDateTimeOffsetCore(DateTime sourceUtc)
        {
            DateTimeOffset resultat = new DateTimeOffset(sourceUtc);

            return resultat;
        }

        internal static DateTimeOffset? ToDateTimeOffsetCore( DateTime? source)
        {
            DateTimeOffset? resultat = null;

            if (source.HasValue)
            {
                resultat = ToDateTimeOffsetCore(source.Value);
            }

            return resultat;
        }

        internal static DateTimeOffset ToDateTimeOffsetCore( DateTime source)
        {
            DateTimeOffset resultat = AsDateTimeOffsetCore(source.ToUniversalTime());

            return resultat;
        }

        #endregion Core
    }
}