using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImputationH31per.Modele.Entite;
using IImputationTfsModele = ImputationH31per.Modele.Entite.IImputationTfsNotifiable;
using ITacheTfsModele = ImputationH31per.Modele.Entite.ITacheTfsNotifiable<ImputationH31per.Modele.Entite.ITicketTfsNotifiable<ImputationH31per.Modele.Entite.IImputationTfsNotifiable>, ImputationH31per.Modele.Entite.IImputationTfsNotifiable>;
using ITicketTfsModele = ImputationH31per.Modele.Entite.ITicketTfsNotifiable<ImputationH31per.Modele.Entite.IImputationTfsNotifiable>;

namespace ImputationH31per.Modele
{
    public static class ExtensionImputationH31perModele
    {
        public static bool ContientImputationTfs(this IImputationH31perModele modele, IInformationImputationTfs imputationTfs)
        {
            return modele.ContientImputationTfs(imputationTfs.Numero, imputationTfs.NumeroComplementaire, imputationTfs.DateHorodatage);
        }

        public static bool ContientTacheTfs(this IImputationH31perModele modele, IInformationTacheTfs tacheTfs)
        {
            return modele.ContientTacheTfs(tacheTfs.Numero);
        }

        public static bool ContientTicketTfs(this IImputationH31perModele modele, IInformationTicketTfs ticketTfs)
        {
            return modele.ContientTicketTfs(ticketTfs.Numero, ticketTfs.NumeroComplementaire);
        }

        public static IImputationTfsModele CreerImputationTfs(this IImputationH31perModele modele, IInformationImputationTfs imputationTfs)
        {
            return modele.CreerImputationTfs(imputationTfs.Numero, imputationTfs.NumeroComplementaire, imputationTfs.DateHorodatage);
        }

        public static IImputationTfsModele ObtenirDerniereImputationTfs(this IImputationH31perModele modele, int numero, int? numerocomplementaire)
        {
            return ObtenirDerniereImputationTfsCore(modele, numero, numerocomplementaire);
        }

        public static IImputationTfsModele ObtenirDerniereImputationTfs(this IImputationH31perModele modele, IInformationTicketTfs ticketTfs)
        {
            return ObtenirDerniereImputationTfsCore(modele, ticketTfs.Numero, ticketTfs.NumeroComplementaire);
        }

        public static double? ObtenirDifferenceConsommee(this IImputationH31perModele modele, IInformationImputationTfs imputationTfs)
        {
            return ObtenirDifferenceConsommeeCore(modele, imputationTfs);
        }

        public static IImputationTfsModele ObtenirImputationTfs(this IImputationH31perModele modele, IInformationImputationTfs imputationTfs)
        {
            return ObtenirImputationTfsCore(modele, imputationTfs);
        }

        public static IEnumerable<IImputationTfsModele> ObtenirImputationTfsCroissants(this IImputationH31perModele modele)
        {
            return ObtenirImputationTfsCroissantsCore(modele);
        }

        public static IEnumerable<IImputationTfsModele> ObtenirImputationTfsDecroissants(this IImputationH31perModele modele)
        {
            return ObtenirImputationTfsDecroissantsCore(modele);
        }

        public static IImputationTfsModele ObtenirImputationTfsPrecedente(this IImputationH31perModele modele, int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage)
        {
            return ObtenirImputationTfsPrecedenteCore(modele, numero, numeroComplementaire, dateHorodatage);
        }

        public static IImputationTfsModele ObtenirImputationTfsPrecedente(this IImputationH31perModele modele, IInformationImputationTfs imputationTfs)
        {
            return ObtenirImputationTfsPrecedenteCore(modele, imputationTfs.Numero, imputationTfs.NumeroComplementaire, imputationTfs.DateHorodatage);
        }

        public static ITacheTfsModele ObtenirTacheTfs(this IImputationH31perModele modele, IInformationTacheTfs tacheTfs)
        {
            return ObtenirTacheTfsCore(modele, tacheTfs);
        }

        public static ITicketTfsModele ObtenirTicketTfs(this IImputationH31perModele modele, IInformationTicketTfs ticketTfs)
        {
            return ObtenirTicketTfsCore(modele, ticketTfs);
        }

        public static bool SupprimerImputationTfs(this IImputationH31perModele modele, IInformationImputationTfs imputationTfs)
        {
            return SupprimerImputationTfsCore(modele, imputationTfs);
        }

        public static bool SupprimerImputationTfs(this IImputationH31perModele modele, IEnumerable<IInformationImputationTfs> imputationTfss)
        {
            return SupprimerImputationTfsCore(modele, imputationTfss);
        }

        public static bool SupprimerTicketTfs(this  IImputationH31perModele modele, int numero, int? numeroComplementaire)
        {
            return SupprimerTicketTfsCore(modele, numero, numeroComplementaire);
        }

        public static bool SupprimerTicketTfs(this  IImputationH31perModele modele, IInformationTicketTfs ticketTfs)
        {
            return SupprimerTicketTfsCore(modele, ticketTfs);
        }

        public static bool SupprimerTicketTfs(this  IImputationH31perModele modele, IEnumerable<IInformationTicketTfs> ticketTfss)
        {
            return SupprimerTicketTfsCore(modele, ticketTfss);
        }

        #region Core

        internal static IImputationTfsModele ObtenirDerniereImputationTfsCore(IImputationH31perModele modele, int numero, int? numerocomplementaire)
        {
            ITicketTfsModele ticketTfs = modele.ObtenirTicketTfs(numero, numerocomplementaire);
            if (ticketTfs == null)
                return null;

            IImputationTfsModele resultat = ticketTfs.ObtenirDernierImputationTfs();

            return resultat;
        }

        internal static double? ObtenirDifferenceConsommeeCore(IImputationH31perModele modele, IInformationImputationTfs imputationTfs)
        {
            double? resultat = null;

            bool estConsommeDefinie = imputationTfs.EstConsommeDefinie();
            if (estConsommeDefinie)
            {
                IInformationImputationTfs imputationTfsPrecedente = modele.ObtenirImputationTfsPrecedente(imputationTfs);
                double sommeConsommeePrecedente = (imputationTfsPrecedente != null) ? imputationTfsPrecedente.SommeConsommee ?? 0 : 0;
                resultat = (imputationTfs.SommeConsommee.Value - sommeConsommeePrecedente);
            }

            return resultat;
        }

        internal static IImputationTfsModele ObtenirImputationTfsCore(IImputationH31perModele modele, IInformationImputationTfs imputationTfs)
        {
            return modele.ObtenirImputationTfs(imputationTfs.Numero, imputationTfs.NumeroComplementaire, imputationTfs.DateHorodatage);
        }

        internal static IEnumerable<IImputationTfsModele> ObtenirImputationTfsCroissantsCore(IImputationH31perModele modele)
        {
            IEnumerable<IImputationTfsModele> resultat = ObtenirImputationTfsDecroissantsCore(modele)
                .Reverse()
                ;
            return resultat;
        }

        internal static IEnumerable<IImputationTfsModele> ObtenirImputationTfsDecroissantsCore(IImputationH31perModele modele)
        {
            IEnumerable<IImputationTfsModele> resultat = modele.ImputationTfss
                .OrderBy(i => i.DateHorodatage, ImputationTfs.ComparateurDateHorodatageDecroissant)
                ;
            return resultat;
        }

        internal static IImputationTfsModele ObtenirImputationTfsPrecedenteCore(IImputationH31perModele modele, int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage)
        {
            IImputationTfsModele resultat = null;

            ITicketTfsModele ticket = modele.ObtenirTicketTfs(numero, numeroComplementaire);
            Debug.Assert(ticket != null);

            resultat = ticket.ImputationTfss
                .OrderBy(i => i, ImputationTfs.ComparateurImputationTfsDecroissant)
                .SkipWhile(i => i.DateHorodatage >= dateHorodatage)
                .FirstOrDefault();

            return resultat;
        }

        internal static ITacheTfsModele ObtenirTacheTfsCore(IImputationH31perModele modele, IInformationTacheTfs tacheTfs)
        {
            return modele.ObtenirTacheTfs(tacheTfs.Numero);
        }

        internal static ITicketTfsModele ObtenirTicketTfsCore(IImputationH31perModele modele, IInformationTicketTfs ticketTfs)
        {
            return modele.ObtenirTicketTfs(ticketTfs.Numero, ticketTfs.NumeroComplementaire);
        }

        internal static bool SupprimerImputationTfsCore(IImputationH31perModele modele, IInformationImputationTfs imputationTfs)
        {
            return SupprimerImputationTfsCore(modele, new[] { imputationTfs });
        }

        internal static bool SupprimerImputationTfsCore(IImputationH31perModele modele, IEnumerable<IInformationImputationTfs> imputationTfss)
        {
            bool resultat = true;

            imputationTfss = imputationTfss.ToArray();

            foreach (IInformationImputationTfs imputationTfs in imputationTfss)
            {
                resultat = resultat && modele.SupprimerImputationTfs(imputationTfs.Numero, imputationTfs.NumeroComplementaire, imputationTfs.DateHorodatage);
            }

            return resultat;
        }

        internal static bool SupprimerTacheTfsCore(IImputationH31perModele modele, int numero)
        {
            IInformationTacheTfs tacheTfs = new InformationTacheTfs(numero);

            bool resultat = SupprimerTacheTfsCore(modele, tacheTfs);

            return resultat;
        }

        internal static bool SupprimerTacheTfsCore(IImputationH31perModele modele, IInformationTacheTfs tacheTfs)
        {
            return SupprimerTacheTfsCore(modele, new[] { tacheTfs });
        }

        internal static bool SupprimerTicketTfsCore(IImputationH31perModele modele, int numero, int? numeroComplementaire)
        {
            IInformationTicketTfs ticketTfs = new InformationTicketTfs(numero, numeroComplementaire);

            bool resultat = SupprimerTicketTfsCore(modele, ticketTfs);

            return resultat;
        }

        internal static bool SupprimerTicketTfsCore(IImputationH31perModele modele, IInformationTicketTfs ticketTfs)
        {
            return SupprimerTicketTfsCore(modele, new[] { ticketTfs });
        }

        internal static bool SupprimerTicketTfsCore(IImputationH31perModele modele, IEnumerable<IInformationTicketTfs> ticketTfss)
        {
            IEnumerable<ITicketTfsModele> source = ticketTfss
                .Select(t => ObtenirTicketTfsCore(modele, t));

            IEnumerable<IInformationImputationTfs> imputationTfss = ObtenirImputationTfsPourSuppressionCore(source);

            bool resultat = SupprimerImputationTfsCore(modele, imputationTfss);

            return resultat;
        }

        private static IEnumerable<IInformationImputationTfs> ObtenirImputationTfsPourSuppressionCore(IEnumerable<ITicketTfsModele> ticketTfss)
        {
            IEnumerable<IInformationImputationTfs> imputationTfss = ticketTfss
                .SelectMany(t => (t != null) ? t.ImputationTfss : new IImputationTfsModele[] { null });

            return imputationTfss;
        }

        private static IEnumerable<IInformationImputationTfs> ObtenirImputationTfsPourSuppressionCore(IEnumerable<ITacheTfsModele> tacheTfss)
        {
            IEnumerable<ITicketTfsModele> ticketTfss = tacheTfss
                .SelectMany(t => (t != null) ? t.TicketTfss : new ITicketTfsModele[] { null });

            IEnumerable<IInformationImputationTfs> imputationTfss = ObtenirImputationTfsPourSuppressionCore(ticketTfss);

            return imputationTfss;
        }

        private static bool SupprimerTacheTfsCore(IImputationH31perModele modele, IEnumerable<IInformationTacheTfs> tacheTfss)
        {
            IEnumerable<ITacheTfsModele> source = tacheTfss
                 .Select(t => ObtenirTacheTfsCore(modele, t));

            IEnumerable<IInformationImputationTfs> imputationTfss = ObtenirImputationTfsPourSuppressionCore(source);

            bool resultat = SupprimerImputationTfsCore(modele, imputationTfss);

            return resultat;
        }

        #endregion Core
    }
}