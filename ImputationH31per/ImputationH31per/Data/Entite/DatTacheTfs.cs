using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;

namespace ImputationH31per.Data.Entite
{
    public class DatTacheTfs : IDatTacheTfs
    {
        #region ctor

        public DatTacheTfs(ITacheTfs<ITicketTfs<IImputationTfs>, IImputationTfs> tache)
            : this(tache, tache.TicketTfss.Select(i => new DatTicketTfs(i)))
        {
        }

        public DatTacheTfs(IInformationTacheTfs informationTache, IEnumerable<IDatTicketTfs> ticketTfss)
        {
            Numero = informationTache.Numero;

            this.DefinirProprietes(informationTache);

            TicketTfss = ticketTfss.Execute();
        }

        #endregion ctor

        #region Propriété

        public string Nom { get; set; }

        public string NomGroupement { get; set; }

        public int Numero { get; set; }

        public IEnumerable<IDatTicketTfs> TicketTfss { get; set; }

        #endregion Propriété
    }
}