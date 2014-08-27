using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Vue.EditeurImputationTfs.Modele;

namespace ImputationH31per.Vue.ImputationsCourantes.Modele
{
    public interface IImputationsCourantesFormModele : IEditeurImputationTfsChoixSourceModele
    {
        event NotifyCollectionChangedEventHandler ImputationTfssCourantesAChange;

        IImputationH31perModele ImputationH31perModele { get; }

        IEnumerable<IImputationTfsNotifiable> ImputationTfssCourantes { get; }

        void AjouterImputationTfs(IImputationTfsNotifiable imputationTfs);

        void NettoyerImputationTfs();

        IImputationTfsNotifiable ObtenirDerniereImputationTfs(int numero, int? numeroComplementaire);

        IInformationTacheTfsNotifiable ObtenirInformationTacheTfs(int numero);

        bool SupprimerImputationTfs(IImputationTfsNotifiable imputationTfs);
    }
}