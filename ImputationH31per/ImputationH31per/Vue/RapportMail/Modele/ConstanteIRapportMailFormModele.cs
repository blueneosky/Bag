using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Vue.RapportMail.Modele
{
    public static class ConstanteIRapportMailFormModele
    {
        #region TypeNotifiable

        public static Type ConstanteTypeNotifiable = typeof(IRapportMailFormModele);

        #endregion TypeNotifiable

        // ConstanteProprieteTempsDebut
        // ConstanteProprieteTempsFin
        // ConstanteProprieteTexteRapport
        // ConstanteProprieteImputationTfsDisponibles
        // ConstanteProprieteImputationTfsSelectionnees
        // ConstanteProprieteSommeDifferenceHeureConsommee

        public const string ConstanteProprieteImputationTfsDisponibles = "ImputationTfsDisponibles";
        public const string ConstanteProprieteImputationTfsSelectionnees = "ImputationTfsSelectionnees";
        public const string ConstanteProprieteTempsDebut = "TempsDebut";
        public const string ConstanteProprieteTempsFin = "TempsFin";
        public const string ConstanteProprieteTexteRapport = "TexteRapport";
        public const string ConstanteProprieteSommeDifferenceHeureConsommee = "SommeDifferenceHeureConsommee";
    }
}