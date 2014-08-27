using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Controle.ImputationTfsListView.Modele;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.ImportDepuisExcel.Modele
{
    public class ImputationTfsListViewControlModele : ImputationTfsListViewControlModeleBase
    {
        #region Membres

        private readonly IImportDepuisExcelFormModele _importDepuisExcelFormModele;

        #endregion Membres

        #region ctor

        public ImputationTfsListViewControlModele(IImportDepuisExcelFormModele importDepuisExcelFormModele)
        {
            _importDepuisExcelFormModele = importDepuisExcelFormModele;
            _importDepuisExcelFormModele.ImputationTfssAChange += NotifierImputationTfssAChange;

            EstImputationTfsModifiable = true;
            EstImputationTfsSupprimable = true;
            ComparateurTriAffichage = ImputationTfs.ComparateurImputationTfsCroissant;
        }

        ~ImputationTfsListViewControlModele()
        {
            _importDepuisExcelFormModele.ImputationTfssAChange -= NotifierImputationTfssAChange;
        }

        #endregion ctor

        #region ImputationTfsListViewControlModeleBase

        public override IEnumerable<IImputationTfsNotifiable> ImputationTfss
        {
            get { return _importDepuisExcelFormModele.ImputationTfss; }
        }

        #endregion ImputationTfsListViewControlModeleBase
    }
}