using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImputationH31per.Controle.ImputationTfsListView.Modele;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.ImputationsCourantes.Modele
{
    public class ImputationTfsListViewControlControleur : ImputationTfsListViewControlControleurBase
    {
        #region Membres

        private readonly IImputationsCourantesFormControleur _imputationsCourantesFormControleur;
        private readonly IImputationTfsListViewControlModele _modele;

        #endregion Membres

        #region ctor

        public ImputationTfsListViewControlControleur(IImputationTfsListViewControlModele modele, IImputationsCourantesFormControleur imputationsCourantesFormControleur)
            : base(modele)
        {
            _modele = modele;
            _imputationsCourantesFormControleur = imputationsCourantesFormControleur;
        }

        #endregion ctor

        #region ImputationTfsListViewControlControleurBase

        public override void SupprimerImputationTfs(IImputationTfsNotifiable imputationTfs, bool avecModifieur)
        {
            Debug.Assert(_modele.EstImputationTfsSupprimable);
            _imputationsCourantesFormControleur.SupprimerImputationTfs(imputationTfs);
        }

        #endregion ImputationTfsListViewControlControleurBase
    }
}