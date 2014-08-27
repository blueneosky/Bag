using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.RapportMail.Modele
{
    public interface IRapportMailFormModele : INotifyPropertyChanged
    {
        #region Propriétés

        int SommeDifferenceHeureConsommee { get; }

        IEnumerable<IInformationImputationTfs> ImputationTfsDisponibles { get; }

        IEnumerable<IInformationImputationTfs> ImputationTfsSelectionnees { get; }

        DateTimeOffset TempsDebut { get; }

        DateTimeOffset TempsFin { get; }

        string TexteRapport { get; }

        #endregion Propriétés

        #region Méthodes

        void DefinirTempsDebutEtFin(DateTimeOffset tempsDebut, DateTimeOffset tempsFin);

        #endregion Méthodes
    }
}