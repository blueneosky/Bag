using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public static class ExtensionInformationImputationTfs
    {
        public static DateTimeOffset DateImputation(this IInformationImputationTfs source)
        {
            return DateImputationCore(source);
        }

        public static DateTimeOffset DateImputationPlusRecente(this IInformationImputationTfs source)
        {
            return DateImputationPlusRecenteCore(source);
        }

        public static void DefinirProprietes(this IInformationImputationTfs destination, IInformationImputationTfs source)
        {
            DefinirProprietesCore(destination, source);
        }

        public static void DefinirProprietesImputationTfs(this IInformationImputationTfs destination, IInformationImputationTfs source)
        {
            DefinirProprietesImputationTfsCore(destination, source);
        }

        public static bool EstConsommeDefinie(this IInformationImputationTfs source)
        {
            return EstConsommeDefinieCore(source);
        }

        public static bool EstEstimationDefinie(this IInformationImputationTfs source)
        {
            return EstEstimationDefinieCore(source);
        }

        public static bool EstLiee(this IInformationImputationTfs source, IInformationImputationTfs value)
        {
            return EstLieeCore(source, value);
        }

        #region Core

        internal static DateTimeOffset DateImputationCore(IInformationImputationTfs source)
        {
            DateTimeOffset? date = source.DateSommeConsommee;
            if (date.HasValue)
                return date.Value;

            date = source.DateEstimCourant;
            if (date.HasValue)
                return date.Value;

            date = source.DateHorodatage;

            return date.Value;
        }

        internal static DateTimeOffset DateImputationPlusRecenteCore(IInformationImputationTfs source)
        {
            DateTimeOffset resultat;

            DateTimeOffset? date1 = source.DateSommeConsommee;
            DateTimeOffset? date2 = source.DateEstimCourant;

            if ((date1 == null) && (date2 == null))
            {
                resultat = source.DateHorodatage;
            }
            else if ((date1 == null) || (date2 == null))
            {
                resultat = date1 ?? date2.Value;
            }
            else
            {
                resultat = (date1.Value > date2.Value) ? date1.Value : date2.Value; // la plus récente
            }

            return resultat;
        }

        internal static void DefinirProprietesCore(IInformationImputationTfs destination, IInformationImputationTfs source)
        {
            ExtensionInformationTicketTfs.DefinirProprietesCore(destination, source);
            DefinirProprietesImputationTfsCore(destination, source);
        }

        internal static void DefinirProprietesImputationTfsCore(IInformationImputationTfs destination, IInformationImputationTfs source)
        {
            destination.DateEstimCourant = source.DateEstimCourant;
            destination.DateSommeConsommee = source.DateSommeConsommee;
            destination.EstimCourant = source.EstimCourant;
            destination.SommeConsommee = source.SommeConsommee;
            destination.Commentaire = source.Commentaire;
        }

        internal static bool EstConsommeDefinieCore(IInformationImputationTfs source)
        {
            bool resultat = source.SommeConsommee.HasValue
                && source.DateSommeConsommee.HasValue;

            return resultat;
        }

        internal static bool EstEstimationDefinieCore(IInformationImputationTfs source)
        {
            bool resultat = source.EstTacheAvecEstim
                && source.EstimCourant.HasValue
                && source.DateEstimCourant.HasValue;

            return resultat;
        }

        internal static bool EstLieeCore(IInformationImputationTfs x, IInformationImputationTfs y)
        {
            bool resultat =
                ExtensionInformationTicketTfs.EstLieeCore(x, y)
                && (x.DateHorodatage == y.DateHorodatage)
                ;

            return resultat;
        }

        #endregion Core
    }
}