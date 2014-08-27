using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.Main.Modele
{
    public interface IMainFormModele : INotifyPropertyChanged
    {
        event NotifyCollectionChangedEventHandler ImputationTfssAChange;

        IImputationH31perModele ImputationH31perModele { get; }

        IEnumerable<IImputationTfsNotifiable> ImputationTfss { get; }

        string Titre { get; }
    }
}