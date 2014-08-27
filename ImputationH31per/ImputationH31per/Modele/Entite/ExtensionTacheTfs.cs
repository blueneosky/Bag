using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public static class ExtensionTacheTfs
    {
        public static IEnumerable<TImputationTfs> ObtenirImputationTfs<TTicketTfs, TImputationTfs>(this ITacheTfs<TTicketTfs, TImputationTfs> tacheTfs)
            where TTicketTfs : ITicketTfs<TImputationTfs>
            where TImputationTfs : IImputationTfs
        {
            return ObtenirImputationTfsCore<TTicketTfs, TImputationTfs>(tacheTfs);
        }

        public static bool SupprimerTicketTfs<TTicketTfs, TImputationTfs>(this TacheTfsBase<TTicketTfs, TImputationTfs> tacheTfs, TTicketTfs ticketTfs)
            where TTicketTfs : TicketTfsBase<TImputationTfs>
            where TImputationTfs : ImputationTfsBase
        {
            return tacheTfs.SupprimerTicketTfs(ticketTfs.NumeroComplementaire);
        }

        #region Core

        internal static IEnumerable<TImputationTfs> ObtenirImputationTfsCore<TTicketTfs, TImputationTfs>(ITacheTfs<TTicketTfs, TImputationTfs> tacheTfs)
            where TTicketTfs : ITicketTfs<TImputationTfs>
            where TImputationTfs : IImputationTfs
        {
            return tacheTfs.TicketTfss
                .SelectMany(t => t.ImputationTfss);
        }

        #endregion Core
    }
}