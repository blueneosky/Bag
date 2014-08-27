using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Data;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;

namespace ImputationH31per.Vue.Main.Modele
{
    public class MainFormModele : IMainFormModele
    {
        #region Membres

        private readonly IImputationH31perModele _imputationH31perModele;

        #endregion Membres

        #region ctor

        public MainFormModele(IImputationH31perModele ImputationH31perModele)
        {
            _imputationH31perModele = ImputationH31perModele;
            _imputationH31perModele.ImputationTfssAChange += _ImputationH31perModele_ImputationTfssAChange;
            _imputationH31perModele.PropertyChanged += _imputationH31perModele_PropertyChanged;

            MiseAJourImputationH31perModele(_imputationH31perModele);
        }

        ~MainFormModele()
        {
            _imputationH31perModele.ImputationTfssAChange -= _ImputationH31perModele_ImputationTfssAChange;
            _imputationH31perModele.PropertyChanged -= _imputationH31perModele_PropertyChanged;
        }

        #endregion ctor

        #region IMainFormModele

        #region Propriétés

        private string _titre;

        public IImputationH31perModele ImputationH31perModele
        {
            get { return _imputationH31perModele; }
        }

        public IEnumerable<IImputationTfsNotifiable> ImputationTfss
        {
            get
            {
                IEnumerable<IImputationTfsNotifiable> historiques = _imputationH31perModele
                    .ObtenirImputationTfsCroissants();

                return historiques;
            }
        }

        public string Titre
        {
            get { return _titre; }
            set
            {
                if (_titre == value)
                    return;
                _titre = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteMainFormModele.ConstanteProprieteTitre));
            }
        }

        #endregion Propriétés

        #region Evennements

        public event NotifyCollectionChangedEventHandler ImputationTfssAChange;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifierImputationTfssAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            ImputationTfssAChange.Notifier(sender, e);
        }

        protected virtual void NotifierPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notifier(sender, e);
        }

        #endregion Evennements

        #endregion IMainFormModele

        #region Abonnements

        private void _ImputationH31perModele_ImputationTfssAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifierImputationTfssAChange(this, e);
        }

        private void _imputationH31perModele_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstanteIImputationH31perModele.ConstanteProprieteEstModifie:
                case ConstanteIImputationH31perModele.ConstanteProprieteServiceDataActif:
                    MiseAJourTitre(_imputationH31perModele.EstModifie, _imputationH31perModele.ServiceDataActif);
                    break;

                default:
                    Debug.Fail("Cas non géré");
                    break;
            }
        }

        #endregion Abonnements

        #region Mise à jour depuis IImputationH31perModele

        private void MiseAJourImputationH31perModele(IImputationH31perModele modele)
        {
            MiseAJourTitre(modele.EstModifie, modele.ServiceDataActif);
        }

        private void MiseAJourTitre(bool estModifie, IServiceData serviceData)
        {
            string nomApplication = Application.ProductName;
#if DEBUG
            nomApplication = "[DEBUG]" + nomApplication;
#endif
            string nomServiceData = (serviceData != null) ? serviceData.Nom : "~Aucun~";
            string texteEtatModifie = estModifie ? "*" : String.Empty;
            string titre = String.Concat(nomApplication, " - ", nomServiceData, texteEtatModifie);

            Titre = titre;
        }

        #endregion Mise à jour depuis IImputationH31perModele
    }
}