using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.ImportDepuisExcel.Modele
{
    public interface IImportDepuisExcelFormModele
    {
        event NotifyCollectionChangedEventHandler ImputationTfssAChange;

        IImputationH31perModele ImputationH31perModele { get; }

        IEnumerable<IImputationTfsNotifiable> ImputationTfss { get; }

        void AjouterImputationTfs(IImputationTfsNotifiable imputationTfs);

        void NettoyerImputationTfs();

        void SupprimerImputationTfs(IImputationTfsNotifiable imputationTfs);
    }
}