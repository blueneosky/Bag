using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImputationH31per.Controle.ImputationTfsListView.Modele;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.ImportDepuisExcel.Modele
{
    internal class ImputationTfsListViewControlControleur : ImputationTfsListViewControlControleurBase
    {
        #region Membres

        private readonly IImportDepuisExcelFormControleur _importDepuisExcelFormControleur;
        private readonly IImputationTfsListViewControlModele _modele;

        #endregion Membres

        #region ctor

        public ImputationTfsListViewControlControleur(IImputationTfsListViewControlModele modele, IImportDepuisExcelFormControleur importDepuisExcelFormControleur)
            : base(modele)
        {
            _modele = modele;
            _importDepuisExcelFormControleur = importDepuisExcelFormControleur;
        }

        #endregion ctor

        #region ImputationTfsListViewControlControleurBase

        public override void SupprimerImputationTfs(IImputationTfsNotifiable imputationTfs, bool avecModifieur)
        {
            Debug.Assert(_modele.EstImputationTfsSupprimable);
            _importDepuisExcelFormControleur.SupprimerImputationTfs(imputationTfs);
        }

        #endregion ImputationTfsListViewControlControleurBase
    }
}