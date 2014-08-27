using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public static class ExtensionTicketTfs
    {
        public static TImputationTfs ObtenirDernierImputationTfs<TImputationTfs>(this ITicketTfs<TImputationTfs> ticketTfs)
            where TImputationTfs : IImputationTfs
        {
            return ObtenirDernierImputationTfsCore(ticketTfs);
        }

        public static IEnumerable<TImputationTfs> ObtenirImputationTfssOrdonneeCroissant<TImputationTfs>(this ITicketTfs<TImputationTfs> ticketTfs)
            where TImputationTfs : IImputationTfs
        {
            return ObtenirImputationTfssOrdonneeCroissantCore(ticketTfs);
        }

        public static IEnumerable<TImputationTfs> ObtenirImputationTfssOrdonneeDecroissant<TImputationTfs>(this ITicketTfs<TImputationTfs> ticketTfs)
            where TImputationTfs : IImputationTfs
        {
            return ObtenirImputationTfssOrdonneeDecroissantCore(ticketTfs);
        }

        public static bool SupprimerImputationTfs<TImputationTfs>(this TicketTfsBase<TImputationTfs> ticketTfs, TImputationTfs imputationTfs)
            where TImputationTfs : ImputationTfsBase
        {
            return ticketTfs.SupprimerImputationTfs(imputationTfs.DateHorodatage);
        }

        #region Core

        public static TImputationTfs ObtenirDernierImputationTfsCore<TImputationTfs>(ITicketTfs<TImputationTfs> ticketTfs)
            where TImputationTfs : IImputationTfs
        {
            IEnumerable<TImputationTfs> imputationTfss = ObtenirImputationTfssOrdonneeDecroissantCore(ticketTfs);
            TImputationTfs resultat = imputationTfss.FirstOrDefault();

            return resultat;
        }

        public static IEnumerable<TImputationTfs> ObtenirImputationTfssOrdonneeCroissantCore<TImputationTfs>(ITicketTfs<TImputationTfs> ticketTfs)
           where TImputationTfs : IImputationTfs
        {
            return ObtenirImputationTfssOrdonneeDecroissantCore(ticketTfs).Reverse();
        }

        public static IEnumerable<TImputationTfs> ObtenirImputationTfssOrdonneeDecroissantCore<TImputationTfs>(ITicketTfs<TImputationTfs> ticketTfs)
            where TImputationTfs : IImputationTfs
        {
            IEnumerable<TImputationTfs> resultat = ticketTfs.ImputationTfss
                .OrderBy(i => i.DateHorodatage, ImputationTfs.ComparateurDateHorodatageDecroissant);

            return resultat;
        }

        #endregion Core
    }
}