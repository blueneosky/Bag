using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;

namespace ImputationH31per.Data.Entite
{
    public class DatTicketTfs : IDatTicketTfs
    {
        #region ctor

        public DatTicketTfs(ITicketTfs<IImputationTfs> ticket)
            : this(ticket, ticket.ImputationTfss.Select(i => new DatImputationTfs(i)))
        {
        }

        public DatTicketTfs(IInformationTicketTfs informationTicket, IEnumerable<IDatImputationTfs> imputationTfss)
        {
            Numero = informationTicket.Numero;
            NumeroComplementaire = informationTicket.NumeroComplementaire;

            this.DefinirProprietes(informationTicket);

            ImputationTfss = imputationTfss.Execute();
        }

        #endregion ctor

        #region Propriété

        public bool EstTacheAvecEstim { get; set; }

        public IEnumerable<IDatImputationTfs> ImputationTfss { get; set; }

        public string Nom { get; set; }

        public string NomComplementaire { get; set; }

        public string NomGroupement { get; set; }

        public int Numero { get; set; }

        public int? NumeroComplementaire { get; set; }

        #endregion Propriété
    }
}