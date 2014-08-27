using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Controle.ImputationTfsListView.Modele
{
    public static class ConstanteIImputationTfsListViewControlModele
    {
        #region TypeNotifiable

        public static Type ConstanteTypeNotifiable = typeof(IImputationTfsListViewControlModele);

        #endregion TypeNotifiable

        // ConstanteProprieteEstImputationTfsModifiable
        // ConstanteProprieteEstImputationTfsSupprimable
        // ConstanteProprieteComparateurTriAffichage

        public const string ConstanteProprieteComparateurTriAffichage = "ComparateurTriAffichage";
        public const string ConstanteProprieteEstImputationTfsModifiable = "EstImputationTfsModifiable";
        public const string ConstanteProprieteEstImputationTfsSupprimable = "EstImputationTfsSupprimable";
    }
}