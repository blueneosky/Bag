using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;

namespace ImputationH31per.Vue.ImputationsCourantes.Modele
{
    public class ImputationsCourantesFormModele : IImputationsCourantesFormModele
    {
        #region Membres

        private readonly IImputationH31perModele _ImputationH31perModele;

        private readonly SortedObservableCollection<DateTimeOffset, IImputationTfsNotifiable> _imputationTfssCourantes;

        #endregion Membres

        #region ctor

        public ImputationsCourantesFormModele(IImputationH31perModele ImputationH31perModele)
        {
            _ImputationH31perModele = ImputationH31perModele;

            _imputationTfssCourantes = new SortedObservableCollection<DateTimeOffset, IImputationTfsNotifiable>(ImputationTfs.ComparateurDateHorodatageCroissant);
            _imputationTfssCourantes.CollectionChanged += _imputationTfssCourantes_CollectionChanged;
        }

        ~ImputationsCourantesFormModele()
        {
            _imputationTfssCourantes.CollectionChanged -= _imputationTfssCourantes_CollectionChanged;
        }

        #endregion ctor

        #region IImputationsCourantesFormModele

        #region Propriétés

        public IImputationH31perModele ImputationH31perModele
        {
            get { return _ImputationH31perModele; }
        }

        public IEnumerable<IImputationTfsNotifiable> ImputationTfssCourantes
        {
            get { return _imputationTfssCourantes.Values; }
        }

        #endregion Propriétés

        #region Evennement

        public event NotifyCollectionChangedEventHandler ImputationTfssCourantesAChange;

        protected virtual void NotifierImputationTfssCourantesAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            ImputationTfssCourantesAChange.Notifier(sender, e);
        }

        #endregion Evennement

        #region Méthode

        public void AjouterImputationTfs(IImputationTfsNotifiable imputationTfs)
        {
            _imputationTfssCourantes.Add(imputationTfs.DateHorodatage, imputationTfs);
            // notif géré par SortedObservableCollection
        }

        public void NettoyerImputationTfs()
        {
            _imputationTfssCourantes.Clear();
            // notif géré par SortedObservableCollection
        }

        public IEnumerable<string> ObtenirChoixNomGroupements()
        {
            return ObtenirChoixNomGroupementsCore();
        }

        public IEnumerable<int?> ObtenirChoixNumeroComplementaires()
        {
            return ObtenirChoixNumeroComplementairesCore();
        }

        public IImputationTfsNotifiable ObtenirDerniereImputationTfs(int numero, int? numeroComplementaire)
        {
            return ObtenirDerniereImputationTfsCore(numero, numeroComplementaire);
        }

        public IInformationTacheTfsNotifiable ObtenirInformationTacheTfs(int numero)
        {
            return ObtenirInformationTacheTfsCore(numero);
        }

        public bool SupprimerImputationTfs(IImputationTfsNotifiable imputationTfs)
        {
            bool succes = _imputationTfssCourantes.Remove(imputationTfs.DateHorodatage);
            // notif géré par SortedObservableCollection

            return succes;
        }

        #endregion Méthode

        #endregion IImputationsCourantesFormModele

        #region Abonnement

        private void _imputationTfssCourantes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifierImputationTfssCourantesAChange(this, e);
        }

        #endregion Abonnement

        #region Méthodes privée

        private IEnumerable<string> ObtenirChoixNomGroupementsCore()
        {
            IEnumerable<string> nomModeles = _ImputationH31perModele.TacheTfss
                .Select(t => t.NomGroupement);
            IEnumerable<string> nomCourants = _imputationTfssCourantes.Values
                .Select(i => i.NomGroupement);

            IEnumerable<string> resultat = Enumerable.Concat(nomModeles, nomCourants)
                .Distinct()
                .NaturalOrderBy();

            return resultat;
        }

        private IEnumerable<int?> ObtenirChoixNumeroComplementairesCore()
        {
            IEnumerable<int?> numeroComplementaireModeles = _ImputationH31perModele.TicketTfss
                .Select(t => t.NumeroComplementaire);
            IEnumerable<int?> numeroComplementaireCourants = _imputationTfssCourantes.Values
                .Select(i => i.NumeroComplementaire);

            IEnumerable<int?> resultat = Enumerable.Concat(numeroComplementaireModeles, numeroComplementaireCourants)
                .Distinct()
                .OrderBy(n => n ?? 0);

            return resultat;
        }

        private IImputationTfsNotifiable ObtenirDerniereImputationTfsCore(int numero, int? numeroComplementaire)
        {
            IImputationTfsNotifiable resultat = null;

            IImputationTfsNotifiable derniereImputationTfsHelper = _ImputationH31perModele.ObtenirDerniereImputationTfs(numero, numeroComplementaire);

            InformationTicketTfs informationTicketTfsData = new InformationTicketTfs(numero, numeroComplementaire);
            IImputationTfsNotifiable derniereImputationTfsCourante = _imputationTfssCourantes.Values
                .Where(i => i.EstLiee(informationTicketTfsData))
                .Reverse()  // optimisation
                .OrderBy(i => i.DateHorodatage, ImputationTfs.ComparateurDateHorodatageDecroissant)
                .FirstOrDefault();

            bool estNullDerniereImputationTfsHelper = (derniereImputationTfsHelper == null);
            bool estNullDerniereImputationTfsCourante = (derniereImputationTfsCourante == null);

            if (estNullDerniereImputationTfsCourante || estNullDerniereImputationTfsHelper)
            {
                resultat = derniereImputationTfsHelper ?? derniereImputationTfsCourante;
            }
            else
            {
                resultat = (ImputationTfs.ComparateurDateHorodatageDecroissant.Compare(derniereImputationTfsHelper.DateHorodatage, derniereImputationTfsCourante.DateHorodatage) < 0) ? derniereImputationTfsHelper : derniereImputationTfsCourante;
            }

            return resultat;
        }

        private IInformationTacheTfsNotifiable ObtenirInformationTacheTfsCore(int numero)
        {
            IInformationTacheTfsNotifiable resultat = _imputationTfssCourantes.Values
                .OrderBy(i => i, ImputationTfs.ComparateurImputationTfsCroissant)
                .Reverse()
                .Where(i => i.Numero == numero)
                .FirstOrDefault();

            if (resultat == null)
                resultat = _ImputationH31perModele.ObtenirTacheTfs(numero);

            return resultat;
        }

        #endregion Méthodes privée
    }
}