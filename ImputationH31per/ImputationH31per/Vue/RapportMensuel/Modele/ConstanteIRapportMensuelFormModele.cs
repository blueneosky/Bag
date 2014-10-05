using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImputationH31per.Vue.RapportMensuel.Modele
{
    public static class ConstanteIRapportMensuelFormModele
    {
        #region TypeNotifiable

        public static Type ConstanteTypeNotifiable = typeof(IRapportMensuelFormModele);

        #endregion TypeNotifiable

        // ConstanteProprieteDateMoisAnnee
        // ConstanteProprieteImputationsDuMois
        // ConstanteProprieteImputationRestantes
        // ConstanteProprieteImputationPourGroupes
        // ConstanteProprieteGroupes
        // ConstanteProprieteGroupeSelectionne
        // ConstanteProprieteImputationPourTaches
        // ConstanteProprieteTaches
        // ConstanteProprieteTacheSelectionnee
        // ConstanteProprieteImputationPourTickets
        // ConstanteProprieteTickets
        // ConstanteProprieteTicketSelectionne

        public const string ConstanteProprieteDateMoisAnnee = "DateMoisAnnee";
        public const string ConstanteProprieteImputationsDuMois = "ImputationsDuMois";
        public const string ConstanteProprieteImputationRestantes = "ImputationRestantes";
        public const string ConstanteProprieteImputationPourGroupes = "ImputationPourGroupes";
        public const string ConstanteProprieteGroupes = "Groupes";
        public const string ConstanteProprieteGroupeSelectionne = "GroupeSelectionne";
        public const string ConstanteProprieteImputationPourTaches = "ImputationPourTaches";
        public const string ConstanteProprieteTaches = "Taches";
        public const string ConstanteProprieteImputationPourTickets = "ImputationPourTickets";
        public const string ConstanteProprieteTacheSelectionnee = "TacheSelectionnee";
        public const string ConstanteProprieteTickets = "Tickets";
        public const string ConstanteProprieteTicketSelectionne = "TicketSelectionne";
    }
}