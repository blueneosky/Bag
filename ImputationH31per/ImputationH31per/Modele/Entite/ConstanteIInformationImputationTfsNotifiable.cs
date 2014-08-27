using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public static class ConstanteIInformationImputationTfsNotifiable
    {
        #region TypeNotifiable

        public static Type ConstanteTypeNotifiable = typeof(IInformationImputationTfsNotifiable);

        #endregion TypeNotifiable

        // ConstanteProprieteEstTacheAvecEstim
        // ConstanteProprieteNom
        // ConstanteProprieteNomComplementaire
        // ConstanteProprieteNomGroupement
        // ConstanteProprieteDateEstimCourant
        // ConstanteProprieteDateSommeConsommee
        // ConstanteProprieteEstimCourant
        // ConstanteProprieteSommeConsommee
        // ConstanteProprieteCommentaire

        public const string ConstanteProprieteDateEstimCourant = "DateEstimCourant";
        public const string ConstanteProprieteDateSommeConsommee = "DateSommeConsommee";
        public const string ConstanteProprieteEstimCourant = "EstimCourant";
        public const string ConstanteProprieteEstTacheAvecEstim = ConstanteIInformationTicketTfsNotifiable.ConstanteProprieteEstTacheAvecEstim;
        public const string ConstanteProprieteNom = ConstanteIInformationTicketTfsNotifiable.ConstanteProprieteNom;
        public const string ConstanteProprieteNomComplementaire = ConstanteIInformationTicketTfsNotifiable.ConstanteProprieteNomComplementaire;
        public const string ConstanteProprieteNomGroupement = ConstanteIInformationTicketTfsNotifiable.ConstanteProprieteNomGroupement;
        public const string ConstanteProprieteSommeConsommee = "SommeConsommee";
        public const string ConstanteProprieteCommentaire = "Commentaire";
    }
}