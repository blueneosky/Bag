using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public class InformationTicketTfs : InformationTicketTfsBase
    {
        #region ctor

        public InformationTicketTfs(IInformationTacheTfs tacheTfs, int? numeroComplementaire)
            : base(tacheTfs, numeroComplementaire)
        {
        }

        public InformationTicketTfs(int numero, int? numeroComplementaire)
            : base(numero, numeroComplementaire)
        {
        }

        #endregion ctor
    }
}