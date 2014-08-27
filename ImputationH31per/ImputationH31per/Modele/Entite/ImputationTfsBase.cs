using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public abstract class ImputationTfsBase : InformationImputationTfsBase, IImputationTfsNotifiable
    {
        #region ctor

        protected ImputationTfsBase(IInformationTicketTfs ticketTfs, DateTimeOffset dateHorodatage)
            : this(ticketTfs.Numero, ticketTfs.NumeroComplementaire, dateHorodatage)
        {
        }

        protected ImputationTfsBase(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage)
            : base(numero, numeroComplementaire, dateHorodatage)
        {
        }

        #endregion ctor
    }
}