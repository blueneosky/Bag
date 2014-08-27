using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Controle.ImputationTfsListView.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;

namespace ImputationH31per.Controle.ImputationTfsListView
{
#warning TODO - cette vue n'utilise pas le patterne CommencerMiseAJour/TerminerMiseAJour

    public partial class ImputationTfsListViewControl : IHUserControl
    {
        #region Membres

        private IImputationTfsListViewControlControleur _controleur;
        private IImputationTfsListViewControlModele _modele;

        #endregion Membres

        #region ctor

        /// <summary>
        /// Réservé au designer
        /// </summary>
        public ImputationTfsListViewControl()
        {
            InitializeComponent();
        }

        ~ImputationTfsListViewControl()
        {
            _modele.ImputationTfssAChange -= _modele_ImputationTfssAChange;
            _modele.PropertyChanged -= _modele_PropertyChanged;
        }

        public void Initialiser(IImputationTfsListViewControlModele modele, IImputationTfsListViewControlControleur controleur)
        {
            _modele = modele;
            _controleur = controleur;

            _modele.ImputationTfssAChange += _modele_ImputationTfssAChange;
            _modele.PropertyChanged += _modele_PropertyChanged;

            // apparance graphique
            _imputationTfsListView.FullRowSelect = true;

            // synchronisation de l'interface avec le modele
            MiseAJourEstImputationTfsModifiable(_modele.EstImputationTfsModifiable);
            MiseAJourEstImputationTfsSupprimable(_modele.EstImputationTfsSupprimable);
            MiseAJourIndefinieImputationTfss();
        }

        #endregion ctor

        #region Evennement modele

        private void _modele_ImputationTfssAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    MiseAJourAjoutImputationTfss(e.NewItems.OfType<IImputationTfsNotifiable>());
                    break;

                case NotifyCollectionChangedAction.Remove:
                    MiseAJourSuppressionImputationTfss(e.OldItems.OfType<IImputationTfsNotifiable>());
                    break;

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                    MiseAJourIndefinieImputationTfss();
                    break;

                default:
                    Debug.Fail("Cas non prévus");
                    break;
            }
        }

        private void _modele_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstanteIImputationTfsListViewControlModele.ConstanteProprieteEstImputationTfsModifiable:
                    MiseAJourEstImputationTfsModifiable(_modele.EstImputationTfsModifiable);
                    break;

                case ConstanteIImputationTfsListViewControlModele.ConstanteProprieteEstImputationTfsSupprimable:
                    MiseAJourEstImputationTfsSupprimable(_modele.EstImputationTfsSupprimable);
                    break;

                case ConstanteIImputationTfsListViewControlModele.ConstanteProprieteComparateurTriAffichage:
                    MiseAJourIndefinieImputationTfss();
                    break;

                default:
                    Debug.Fail("Cas non prévus");
                    break;
            }
        }

        #endregion Evennement modele

        #region Mise à jour interface depuis modele

        private void MiseAJourAjoutImputationTfss(IEnumerable<IImputationTfsNotifiable> imputationTfss)
        {
            IComparer<IImputationTfsNotifiable> comparateurTriAffichage = _modele.ComparateurTriAffichage;

            List<IImputationTfsNotifiable> source = imputationTfss
                    .OrderByDescending(imp => imp, comparateurTriAffichage)
                    .ToList();

            if (source.Empty())
                return;

            _imputationTfsListView.BeginUpdate();

            int i = _imputationTfsListView.Items.Count - 1;
            if (i < 0)
            {
                // vide -> ajout simple
                ImputationTfsListViewItem[] items = source
                    .Select(imp => new ImputationTfsListViewItem(imp))
                    .ToArray();
                _imputationTfsListView.Items.AddRange(items);
                source.Clear();
            }

            IImputationTfsNotifiable imputationTfs = source.FirstOrDefault();
            while ((i >= 0) && source.Any())
            {
                ImputationTfsListViewItem item = _imputationTfsListView.Items[i] as ImputationTfsListViewItem;
                int comp = comparateurTriAffichage.Compare(imputationTfs, item.ImputationTfs);
                Debug.Assert(comp != 0, "Comparateur pas sufisament discriminant");

                if (comp > 0)
                {
                    // insertion
                    _imputationTfsListView.Items.Insert(i + 1, new ImputationTfsListViewItem(imputationTfs));
                    source.RemoveAt(0);
                    imputationTfs = source.FirstOrDefault();
                }
                else
                {
                    i--;
                }
            }

            while (source.Any())
            {
                // insertion
                _imputationTfsListView.Items.Insert(0, new ImputationTfsListViewItem(imputationTfs));
                source.RemoveAt(0);
                imputationTfs = source.FirstOrDefault();
            }

            _imputationTfsListView.EndUpdate();
        }

        private void MiseAJourEstImputationTfsModifiable(bool estImputationTfssModifiable)
        {
            // rien
        }

        private void MiseAJourEstImputationTfsSupprimable(bool estImputationTfssModifiable)
        {
            // rien
        }

        private void MiseAJourIndefinieImputationTfss()
        {
            _imputationTfsListView.BeginUpdate();

            ImputationTfsListViewItem[] items = _modele.ImputationTfss
                .OrderBy(i => i, _modele.ComparateurTriAffichage)
                .Select(i => new ImputationTfsListViewItem(i))
                .ToArray();
            _imputationTfsListView.Items.Clear();
            _imputationTfsListView.Items.AddRange(items);

            _imputationTfsListView.EndUpdate();
        }

        private void MiseAJourSuppressionImputationTfss(IEnumerable<IImputationTfsNotifiable> imputationTfss)
        {
            IComparer<IImputationTfsNotifiable> comparateurTriAffichage = _modele.ComparateurTriAffichage;

            List<IImputationTfsNotifiable> source = imputationTfss
                    .OrderByDescending(imp => imp, comparateurTriAffichage)
                    .ToList();

            if (source.Empty())
                return;

            _imputationTfsListView.BeginUpdate();

            IImputationTfsNotifiable imputationTfs = source.FirstOrDefault();
            int i = _imputationTfsListView.Items.Count - 1;
            while ((i >= 0) && source.Any())
            {
                ImputationTfsListViewItem item = _imputationTfsListView.Items[i] as ImputationTfsListViewItem;
                bool estEgale = imputationTfs == item.ImputationTfs;

                if (estEgale)
                {
                    // suppression
                    _imputationTfsListView.Items.RemoveAt(i);
                    source.RemoveAt(0);
                    imputationTfs = source.FirstOrDefault();
                }
                else
                {
                    i--;
                }
            }

            _imputationTfsListView.EndUpdate();
        }

        #endregion Mise à jour interface depuis modele

        #region Actions utilisateur

        private IImputationTfsNotifiable SelectedImputationTfs
        {
            get
            {
                ImputationTfsListViewItem item = SelectedItem;
                if (item == null)
                    return null;
                IImputationTfsNotifiable imputationTfs = item.ImputationTfs;
                Debug.Assert(imputationTfs != null);

                return imputationTfs;
            }
        }

        private ImputationTfsListViewItem SelectedItem
        {
            get
            {
                ImputationTfsListViewItem item = _imputationTfsListView.SelectedItems
                    .OfType<ImputationTfsListViewItem>()
                    .FirstOrDefault();
                return item;
            }
        }

        private void _imputationTfsListView_DoubleClick(object sender, EventArgs e)
        {
            IImputationTfsNotifiable imputationTfs = SelectedImputationTfs;
            if (imputationTfs == null)
                return;

            if (_modele.EstImputationTfsModifiable)
                _controleur.ModifierImputationTfs(imputationTfs);
        }

        private void _imputationTfsListView_KeyDown(object sender, KeyEventArgs e)
        {
            IImputationTfsNotifiable imputationTfs = SelectedImputationTfs;

            if ((e.KeyCode == Keys.Delete) && (imputationTfs != null))
            {
                if (_modele.EstImputationTfsSupprimable)
                {
                    bool avecModifieur = e.Modifiers.HasFlag(Keys.Shift);
                    _controleur.SupprimerImputationTfs(imputationTfs, avecModifieur);
                }
            }
        }

        #endregion Actions utilisateur

        #region ImputationTfsListViewItem

        private class ImputationTfsListViewItem : ListViewItem
        {
            private const int ConstanteColonneDateEstimCourant = 4;
            private const int ConstanteColonneDateSommeConsommee = 6;
            private const int ConstanteColonneEstimCourant = 3;
            private const int ConstanteColonneNom = 2;
            private const int ConstanteColonneNomGroupement = 1;
            private const int ConstanteColonneSommeConsommee = 5;
            private readonly IImputationTfsNotifiable _imputationTfs;

            public ImputationTfsListViewItem(IImputationTfsNotifiable imputationTfs)
                : base(imputationTfs.NumeroComplet())
            {
                _imputationTfs = imputationTfs;

                SubItems.Add(String.Empty);
                SubItems.Add(String.Empty);
                SubItems.Add(String.Empty);
                SubItems.Add(String.Empty);
                SubItems.Add(String.Empty);
                SubItems.Add(String.Empty);
                MiseAJourNomGroupement(_imputationTfs.NomGroupement);
                MiseAJourNomComplet();
                MiseAJourEstimCourant(_imputationTfs.EstimCourant);
                MiseAJourDateEstimCourant(_imputationTfs.DateEstimCourant);
                MiseAJourSommeConsommee(_imputationTfs.SommeConsommee);
                MiseAJourDateSommeConsommee(_imputationTfs.DateSommeConsommee);

                _imputationTfs.PropertyChanged += _imputationTfs_PropertyChanged;
            }

            ~ImputationTfsListViewItem()
            {
                _imputationTfs.PropertyChanged -= _imputationTfs_PropertyChanged;
            }

            public IImputationTfsNotifiable ImputationTfs
            {
                get { return _imputationTfs; }
            }

            #region Evennement modele

            private void _imputationTfs_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                switch (e.PropertyName)
                {
                    case ConstanteIImputationTfsNotifiable.ConstanteProprieteEstTacheAvecEstim:
                        // rien
                        break;

                    case ConstanteIImputationTfsNotifiable.ConstanteProprieteNom:
                    case ConstanteIImputationTfsNotifiable.ConstanteProprieteNomComplementaire:
                        MiseAJourNomComplet();
                        break;

                    case ConstanteIImputationTfsNotifiable.ConstanteProprieteNomGroupement:
                        MiseAJourNomGroupement(ImputationTfs.NomGroupement);
                        break;

                    case ConstanteIImputationTfsNotifiable.ConstanteProprieteDateEstimCourant:
                        MiseAJourDateEstimCourant(ImputationTfs.DateEstimCourant);
                        break;

                    case ConstanteIImputationTfsNotifiable.ConstanteProprieteDateSommeConsommee:
                        MiseAJourDateSommeConsommee(ImputationTfs.DateSommeConsommee);
                        break;

                    case ConstanteIImputationTfsNotifiable.ConstanteProprieteEstimCourant:
                        MiseAJourEstimCourant(ImputationTfs.EstimCourant);
                        break;

                    case ConstanteIImputationTfsNotifiable.ConstanteProprieteSommeConsommee:
                        MiseAJourSommeConsommee(ImputationTfs.SommeConsommee);
                        break;

                    default:
                        break;
                }
            }

            #endregion Evennement modele

            #region Mise à jour interface depuis modele

            private static string ObtenirRepresentationAvecDate(DateTimeOffset? valeur)
            {
                return valeur.HasValue ? valeur.Value.LocalDateTime.ToString() : String.Empty;
            }

            private static string ObtenirRepresentationAvecHeure(double? valeur)
            {
                return valeur.HasValue ? valeur.ToString() : String.Empty;
            }

            private void MiseAJourDateEstimCourant(DateTimeOffset? nouvelleValeur)
            {
                SubItems[ConstanteColonneDateEstimCourant].Text = ObtenirRepresentationAvecDate(nouvelleValeur);
            }

            private void MiseAJourDateSommeConsommee(DateTimeOffset? nouvelleValeur)
            {
                SubItems[ConstanteColonneDateSommeConsommee].Text = ObtenirRepresentationAvecDate(nouvelleValeur);
            }

            private void MiseAJourEstimCourant(double? nouvelleValeur)
            {
                SubItems[ConstanteColonneEstimCourant].Text = ObtenirRepresentationAvecHeure(nouvelleValeur);
            }

            private void MiseAJourNomComplet()
            {
                SubItems[ConstanteColonneNom].Text = _imputationTfs.NomComplet();
            }

            private void MiseAJourNomGroupement(string nouvelleValeur)
            {
                SubItems[ConstanteColonneNomGroupement].Text = nouvelleValeur;
            }

            private void MiseAJourSommeConsommee(double? nouvelleValeur)
            {
                SubItems[ConstanteColonneSommeConsommee].Text = ObtenirRepresentationAvecHeure(nouvelleValeur);
            }

            #endregion Mise à jour interface depuis modele
        }

        #endregion ImputationTfsListViewItem
    }
}