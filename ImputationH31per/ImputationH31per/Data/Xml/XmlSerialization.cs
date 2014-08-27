using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImputationH31per.Data.Xml
{
    internal static class XmlSerialization
    {

        internal static string Convertir<T>(Nullable<T> source)
                 where T : struct
        {
            string resultat;

            if (source.HasValue)
            {
                resultat = source.Value.ToString();
            }
            else
            {
                resultat = String.Empty;
            }

            return resultat;
        }

        internal static string Convertir(DateTimeOffset? source)
        {
            string resultat;

            if (source.HasValue)
            {
                resultat = source.Value.ToString("u");
            }
            else
            {
                resultat = String.Empty;
            }

            return resultat;
        }

        internal static DateTimeOffset? ConvertirDateTime(string source)
        {
            DateTimeOffset? resultat = null;
            DateTimeOffset valeur;

            if (false == String.IsNullOrEmpty(source))
            {
                if (DateTimeOffset.TryParse(source, out valeur))
                {
                    resultat = valeur;
                }
            }

            return resultat;
        }

        internal static int? ConvertirInt(string source)
        {
            int? resultat = null;
            int valeur;

            if (false == String.IsNullOrEmpty(source))
            {
                if (Int32.TryParse(source, out valeur))
                {
                    resultat = valeur;
                }
            }

            return resultat;
        }

        internal static long? ConvertirLong(string source)
        {
            long? resultat = null;
            long valeur;

            if (false == String.IsNullOrEmpty(source))
            {
                if (Int64.TryParse(source, out valeur))
                {
                    resultat = valeur;
                }
            }

            return resultat;
        }

        internal static double? ConvertirDouble(string source)
        {
            double? resultat = null;
            double valeur;

            if (false == String.IsNullOrEmpty(source))
            {
                if (Double.TryParse(source, out valeur))
                {
                    resultat = valeur;
                }
            }

            return resultat;
        }
    }
}
