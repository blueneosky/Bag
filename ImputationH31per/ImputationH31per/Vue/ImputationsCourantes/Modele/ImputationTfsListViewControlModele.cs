using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Controle.ImputationTfsListView.Modele;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.ImputationsCourantes.Modele
{
    public class ImputationTfsListViewControlModele : ImputationTfsListViewControlModeleBase
    {
        #region Membres

        private readonly IImputationsCourantesFormModele _imputationsCourantesFormModele;

        #endregion Membres

        #region ctor

        public ImputationTfsListViewControlModele(IImputationsCourantesFormModele imputationsCourantesFormModele)
        {
            _imputationsCourantesFormModele = imputationsCourantesFormModele;
            _imputationsCourantesFormModele.ImputationTfssCourantesAChange += NotifierImputationTfssAChange;

            EstImputationTfsModifiable = true;
            EstImputationTfsSupprimable = true;
            ComparateurTriAffichage = ImputationTfs.ComparateurImputationTfsCroissant;
        }

        ~ImputationTfsListViewControlModele()
        {
            _imputationsCourantesFormModele.ImputationTfssCourantesAChange -= NotifierImputationTfssAChange;
        }

        #endregion ctor

        #region ImputationTfsListViewControlModeleBase

        public override IEnumerable<IImputationTfsNotifiable> ImputationTfss
        {
            get { return _imputationsCourantesFormModele.ImputationTfssCourantes; }
        }

        #endregion ImputationTfsListViewControlModeleBase
    }
}