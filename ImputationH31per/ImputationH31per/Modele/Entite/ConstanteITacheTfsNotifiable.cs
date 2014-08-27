using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public static class ConstanteITacheTfsNotifiable
    {
        #region TypeNotifiable

        public static Type ConstanteTypeNotifiable = typeof(ITacheTfsNotifiable<ITicketTfsNotifiable<IImputationTfsNotifiable>, IImputationTfsNotifiable>);

        #endregion TypeNotifiable

        // ConstanteProprieteNom
        // ConstanteProprieteNomGroupement

        public const string ConstanteProprieteNom = ConstanteIInformationTacheTfsNotifiable.ConstanteProprieteNom;
        public const string ConstanteProprieteNomGroupement = ConstanteIInformationTacheTfsNotifiable.ConstanteProprieteNomGroupement;
    }
}