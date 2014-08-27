using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Controle.ImputationTfsListView.Modele
{
    public interface IImputationTfsListViewControlControleur
    {
        void ModifierImputationTfs(IImputationTfsNotifiable imputationTfs);

        void SupprimerImputationTfs(IImputationTfsNotifiable imputationTfs, bool avecModifieur);
    }
}