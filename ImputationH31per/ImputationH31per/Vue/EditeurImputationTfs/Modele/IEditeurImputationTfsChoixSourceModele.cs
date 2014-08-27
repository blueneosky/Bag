using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Vue.EditeurImputationTfs.Modele
{
#warning TODO - point DELTA - ajouter 2 event de notification

    public interface IEditeurImputationTfsChoixSourceModele
    {
        IEnumerable<string> ObtenirChoixNomGroupements();

        IEnumerable<int?> ObtenirChoixNumeroComplementaires();
    }
}