using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public static class ConstanteITicketTfsNotifiable
    {
        #region TypeNotifiable

        public static Type ConstanteTypeNotifiable = typeof(ITicketTfsNotifiable<IImputationTfsNotifiable>);

        #endregion TypeNotifiable

        // ConstanteProprieteEstTacheAvecEstim
        // ConstanteProprieteNom
        // ConstanteProprieteNomComplementaire
        // ConstanteProprieteNomGroupement

        public const string ConstanteProprieteEstTacheAvecEstim = ConstanteIInformationTicketTfsNotifiable.ConstanteProprieteEstTacheAvecEstim;
        public const string ConstanteProprieteNom = ConstanteIInformationTicketTfsNotifiable.ConstanteProprieteNom;
        public const string ConstanteProprieteNomComplementaire = ConstanteIInformationTicketTfsNotifiable.ConstanteProprieteNomComplementaire;
        public const string ConstanteProprieteNomGroupement = ConstanteIInformationTicketTfsNotifiable.ConstanteProprieteNomGroupement;
    }
}