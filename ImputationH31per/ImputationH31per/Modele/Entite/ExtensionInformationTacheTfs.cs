using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public static class ExtensionInformationTacheTfs
    {
        public static void DefinirProprietes(this IInformationTacheTfs destination, IInformationTacheTfs source)
        {
            DefinirProprietesCore(destination, source);
        }

        public static void DefinirProprietesTacheTfs(this IInformationTacheTfs destination, IInformationTacheTfs source)
        {
            DefinirProprietesTacheTfsCore(destination, source);
        }

        public static bool EstLiee(this IInformationTacheTfs source, IInformationTacheTfs value)
        {
            return EstLieeCore(source, value);
        }

        public static string NomComplet(this IInformationTacheTfs source)
        {
            return NomCompletCore(source);
        }

        public static string NumeroComplet(this IInformationTacheTfs source)
        {
            return NumeroCompletCore(source);
        }

        #region Core

        internal static void DefinirProprietesCore(IInformationTacheTfs destination, IInformationTacheTfs source)
        {
            DefinirProprietesTacheTfsCore(destination, source);
        }

        internal static void DefinirProprietesTacheTfsCore(IInformationTacheTfs destination, IInformationTacheTfs source)
        {
            destination.Nom = source.Nom;
            destination.NomGroupement = source.NomGroupement;
        }

        internal static bool EstLieeCore(IInformationTacheTfs x, IInformationTacheTfs y)
        {
            if (Object.ReferenceEquals(x, y))
                return true;

            if ((x == null) || (y == null))
                return false;

            bool resultat = (x.Numero == y.Numero);

            return resultat;
        }

        internal static string NomCompletCore(IInformationTacheTfs source)
        {
            return source.Nom;
        }

        internal static string NumeroCompletCore(IInformationTacheTfs source)
        {
            string resultat = String.Empty + source.Numero;

            return resultat;
        }

        #endregion Core
    }
}