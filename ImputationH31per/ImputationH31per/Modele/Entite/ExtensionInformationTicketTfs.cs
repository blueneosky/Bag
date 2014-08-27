using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public static class ExtensionInformationTicketTfs
    {
        public static void DefinirProprietes(this IInformationTicketTfs destination, IInformationTicketTfs source)
        {
            DefinirProprietesCore(destination, source);
        }

        public static void DefinirProprietesTicketTfs(this IInformationTicketTfs destination, IInformationTicketTfs source)
        {
            DefinirProprietesTicketTfsCore(destination, source);
        }

        public static bool EstLiee(this IInformationTicketTfs source, IInformationTicketTfs value)
        {
            return EstLieeCore(source, value);
        }

        public static string NomComplet(this IInformationTicketTfs source)
        {
            return NomCompletCore(source);
        }

        public static string NumeroComplet(this IInformationTicketTfs source)
        {
            return NumeroCompletCore(source);
        }

        #region Core

        internal static void DefinirProprietesCore(IInformationTicketTfs destination, IInformationTicketTfs source)
        {
            ExtensionInformationTacheTfs.DefinirProprietesCore(destination, source);
            DefinirProprietesTicketTfsCore(destination, source);
        }

        internal static void DefinirProprietesTicketTfsCore(IInformationTicketTfs destination, IInformationTicketTfs source)
        {
            destination.EstTacheAvecEstim = source.EstTacheAvecEstim;
            destination.NomComplementaire = source.NomComplementaire;
        }

        internal static bool EstLieeCore(IInformationTicketTfs x, IInformationTicketTfs y)
        {
            bool resultat =
                ExtensionInformationTacheTfs.EstLieeCore(x, y)
                && (x.NumeroComplementaire == y.NumeroComplementaire)
                ;

            return resultat;
        }

        internal static string NomCompletCore(IInformationTicketTfs source)
        {
            string resultat = ExtensionInformationTacheTfs.NomCompletCore(source);

            if (source.NumeroComplementaire.HasValue)
            {
                string nomComplementaire = source.NomComplementaire;
                if (false == String.IsNullOrEmpty(nomComplementaire))
                {
                    resultat += " - " + nomComplementaire;
                }
            }

            return resultat;
        }

        internal static string NumeroCompletCore(IInformationTicketTfs source)
        {
            string resultat = ExtensionInformationTacheTfs.NumeroCompletCore(source);

            int? numeroComplementaire = source.NumeroComplementaire;
            if (numeroComplementaire.HasValue)
            {
                resultat += "_" + numeroComplementaire;
            }

            return resultat;
        }

        #endregion Core
    }
}