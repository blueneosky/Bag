using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ImputationH31per.Controle.ImputationTfsListView.Modele;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.Main.Modele
{
    public class ImputationTfsListViewControlModele : ImputationTfsListViewControlModeleBase
    {
        #region Membres

        private readonly IMainFormModele _mainFormModele;

        #endregion Membres

        #region ctor

        public ImputationTfsListViewControlModele(IMainFormModele mainFormModele)
        {
            _mainFormModele = mainFormModele;
            _mainFormModele.ImputationH31perModele.ImputationTfssAChange += ImputationH31perModele_ImputationTfssAChange;

            EstImputationTfsModifiable = true;
            EstImputationTfsSupprimable = true;
            ComparateurTriAffichage = ImputationTfs.ComparateurImputationTfsDecroissant;
        }

        ~ImputationTfsListViewControlModele()
        {
            _mainFormModele.ImputationH31perModele.ImputationTfssAChange -= ImputationH31perModele_ImputationTfssAChange;
        }

        #endregion ctor

        #region Abonnement

        private void ImputationH31perModele_ImputationTfssAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifierImputationTfssAChange(this, e);
        }

        #endregion Abonnement

        #region ImputationTfsListViewControlModeleBase

        public override IEnumerable<IImputationTfsNotifiable> ImputationTfss
        {
            get { return _mainFormModele.ImputationTfss; }
        }

        #endregion ImputationTfsListViewControlModeleBase
    }
}