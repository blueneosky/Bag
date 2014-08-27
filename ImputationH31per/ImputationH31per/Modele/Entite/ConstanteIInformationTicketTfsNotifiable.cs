using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public static class ConstanteIInformationTicketTfsNotifiable
    {
        #region TypeNotifiable

        public static Type ConstanteTypeNotifiable = typeof(IInformationTicketTfsNotifiable);

        #endregion TypeNotifiable

        // ConstanteProprieteEstTacheAvecEstim
        // ConstanteProprieteNom
        // ConstanteProprieteNomComplementaire
        // ConstanteProprieteNomGroupement

        public const string ConstanteProprieteEstTacheAvecEstim = "EstTacheAvecEstim";
        public const string ConstanteProprieteNom = ConstanteIInformationTacheTfsNotifiable.ConstanteProprieteNom;
        public const string ConstanteProprieteNomComplementaire = "NomComplementaire";
        public const string ConstanteProprieteNomGroupement = ConstanteIInformationTacheTfsNotifiable.ConstanteProprieteNomGroupement;
    }
}