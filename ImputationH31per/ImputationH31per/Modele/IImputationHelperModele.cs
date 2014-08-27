using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using ImputationH31per.Data;
using IImputationTfsModele = ImputationH31per.Modele.Entite.IImputationTfsNotifiable;
using ITacheTfsModele = ImputationH31per.Modele.Entite.ITacheTfsNotifiable<ImputationH31per.Modele.Entite.ITicketTfsNotifiable<ImputationH31per.Modele.Entite.IImputationTfsNotifiable>, ImputationH31per.Modele.Entite.IImputationTfsNotifiable>;
using ITicketTfsModele = ImputationH31per.Modele.Entite.ITicketTfsNotifiable<ImputationH31per.Modele.Entite.IImputationTfsNotifiable>;

namespace ImputationH31per.Modele
{
    public interface IImputationH31perModele : INotifyPropertyChanged
    {
        #region Propriétés

        bool EstModifie { get; }

        IEnumerable<IImputationTfsModele> ImputationTfss { get; }

        IServiceData ServiceDataActif { get; set; }

        IEnumerable<ITacheTfsModele> TacheTfss { get; }

        IEnumerable<ITicketTfsModele> TicketTfss { get; }

        #endregion Propriétés

        #region Evennements

        event NotifyCollectionChangedEventHandler ImputationTfssAChange;

        event NotifyCollectionChangedEventHandler TacheTfssAChange;

        event NotifyCollectionChangedEventHandler TicketTfssAChange;

        #endregion Evennements

        #region Méthodes

        bool ContientImputationTfs(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage);

        bool ContientTacheTfs(int numero);

        bool ContientTicketTfs(int numero, int? numeroComplementaire);

        IImputationTfsModele CreerImputationTfs(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage);

        IImputationTfsModele ObtenirImputationTfs(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage);

        ITacheTfsModele ObtenirTacheTfs(int numero);

        ITicketTfsModele ObtenirTicketTfs(int numero, int? numeroComplementaire);

        void ReinitialisetEstModifier();

        bool SupprimerImputationTfs(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage);

        void ViderImputationTfss();

        #endregion Méthodes
    }
}