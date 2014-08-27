using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;
using ImputationH31per.Vue.ImputationsCourantes.Modele;

namespace ImputationH31per.Vue.ImportDepuisExcel.Modele
{
    public class ImportDepuisExcelFormModele : IImportDepuisExcelFormModele
    {
        #region Membres

        private readonly IImputationH31perModele _ImputationH31perModele;

        private readonly IImputationsCourantesFormModele _imputationsCourantesFormModele;

        #endregion Membres

        #region ctor

        public ImportDepuisExcelFormModele(IImputationH31perModele ImputationH31perModele)
        {
            _ImputationH31perModele = ImputationH31perModele;

            _imputationsCourantesFormModele = new ImputationsCourantesFormModele(_ImputationH31perModele);
            _imputationsCourantesFormModele.ImputationTfssCourantesAChange += _imputationsCourantesFormModele_ImputationTfssCourantesAChange;
        }

        ~ImportDepuisExcelFormModele()
        {
            _imputationsCourantesFormModele.ImputationTfssCourantesAChange -= _imputationsCourantesFormModele_ImputationTfssCourantesAChange;
        }

        #endregion ctor

        #region IImportDepuisExcelFormModele

        #region Propriétés

        public IImputationH31perModele ImputationH31perModele
        {
            get { return _ImputationH31perModele; }
        }

        public IEnumerable<IImputationTfsNotifiable> ImputationTfss
        {
            get { return _imputationsCourantesFormModele.ImputationTfssCourantes; }
        }

        #endregion Propriétés

        #region Evennement

        public event NotifyCollectionChangedEventHandler ImputationTfssAChange;

        protected virtual void NotifierImputationTfssAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            ImputationTfssAChange.Notifier(sender, e);
        }

        #endregion Evennement

        #region Méthodes

        public void AjouterImputationTfs(IImputationTfsNotifiable imputationTfs)
        {
            _imputationsCourantesFormModele.AjouterImputationTfs(imputationTfs);
        }

        public void NettoyerImputationTfs()
        {
            _imputationsCourantesFormModele.NettoyerImputationTfs();
        }

        public void SupprimerImputationTfs(IImputationTfsNotifiable imputationTfs)
        {
            _imputationsCourantesFormModele.SupprimerImputationTfs(imputationTfs);
        }

        #endregion Méthodes

        #endregion IImportDepuisExcelFormModele

        #region Abonnement

        private void _imputationsCourantesFormModele_ImputationTfssCourantesAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifierImputationTfssAChange(this, e);
        }

        #endregion Abonnement
    }
}