using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImputationH31per.Modele;
using ImputationH31per.Util;
using ImputationH31per.Vue.RapportMensuel.Modele;
using ImputationH31per.Vue.RapportMensuel.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel
{
    public partial class RapportMensuelForm : IHForm
    {
        #region Membres

        private readonly IRapportMensuelFormControleur _controleur;
        private readonly IRapportMensuelFormModele _modele;

        #endregion Membres

        #region ctor

        public RapportMensuelForm(IIHFormModele formModele, IRapportMensuelFormModele modele, IRapportMensuelFormControleur controleur)
            : this(formModele, ServicePreferenceModele.Instance.ObtenirIHFormControleur(formModele), modele, controleur)
        {
        }

        public RapportMensuelForm(IIHFormModele formModele, IIHFormControleur formControleur, IRapportMensuelFormModele modele, IRapportMensuelFormControleur controleur)
            : base(formModele, formControleur)
        {
            InitializeComponent();

            _modele = modele;
            _controleur = controleur;

            _modele.PropertyChanged += _modele_PropertyChanged;
        }

        /// <summary>
        /// Pour le designer
        /// </summary>
        private RapportMensuelForm()
        {
            InitializeComponent();
        }

        ~RapportMensuelForm()
        {
            _modele.PropertyChanged -= _modele_PropertyChanged;
        }

        #endregion ctor

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_modele == null)
                return;

            RafraichirModele();
        }

        #endregion Overrides

        #region Evennement modele

        private void _modele_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstanteIRapportMensuelFormModele.ConstanteProprieteDateMoisAnnee:
                    RafraichirMoisAnnee();
                    break;

                default:
                    // non géré
                    break;
            }
        }

        #endregion Evennement modele

        #region Mise à jour interface depuis modele

        private void RafraichirModele()
        {
            RafraichirMoisAnnee();
        }

        private void RafraichirMoisAnnee()
        {
            CommencerMiseAJour();

            DateTime dateMoisAnnee = _modele.DateMoisAnnee;
            _moisAnneeDateTimePicker.Value = dateMoisAnnee;

            TerminerMiseAJour();
        }

        private void RacraichirGroupes()
        {
            CommencerMiseAJour();

            //_modele.Groupes.

            TerminerMiseAJour();
        }
#warning TODO - point ALPHA - implémenter !

        private void UpdateListBox<TItem, T>(ListBox.ObjectCollection listBoxCollection, IEnumerable<TItem> items)
            where TItem :  class, IItem<T>
        {
            ItemListViewItem<TItem, T>[] nouveauItems = items
                .Select(m => new ItemListViewItem<TItem, T>(m))
                .OrderBy(m => m.ToString())
                .ToArray();

            int ancienIndexItem = 0;
            int nouveauIndexItem = 0;
            while (ancienIndexItem < listBoxCollection.Count || nouveauIndexItem < nouveauItems.Length)
            {
                ItemListViewItem<TItem, T> ancienIlvItem = ancienIndexItem < listBoxCollection.Count ? (ItemListViewItem<TItem, T>)listBoxCollection[ancienIndexItem] : null;
                ItemListViewItem<TItem, T> nouveauIlvItem = nouveauIndexItem < nouveauItems.Length ? nouveauItems[nouveauIndexItem] : null;

                if (ancienIlvItem == null)
                {
                    Debug.Assert(nouveauIlvItem != null);
                    listBoxCollection.Add(nouveauIlvItem);
                    ancienIndexItem++;
                    nouveauIndexItem++;
                }
                else if (nouveauIlvItem == null)
                {
                    listBoxCollection.RemoveAt(ancienIndexItem);
                }
                else if (ancienIlvItem.Equals(nouveauIlvItem))
                {
                    ancienIndexItem++;
                    nouveauIndexItem++;
                }
                else
                {
                    int comp = String.Compare(nouveauIlvItem.ToString(), ancienIlvItem.ToString());
                    if (comp < 0)
                    {
                        // nouveau avant l'ancien
                        listBoxCollection.Insert(ancienIndexItem, nouveauIlvItem);
                        ancienIndexItem++;
                        nouveauIndexItem++;
                    }
                    else
                    {
                        // nouveau après l'ancien, mais n'est pas reconnu (n'existe plus => supprimer)
                        listBoxCollection.RemoveAt(ancienIndexItem);
                    }
                }
            }
        }

        #endregion Mise à jour interface depuis modele

        #region Actions utilisateur

        private void _moisAnneeDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            _controleur.DefinirDateMoisAnnee(_moisAnneeDateTimePicker.Value);
        }

        private void _groupesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            _controleur.DefinirGroupeSelectionne(_groupesListBox.SelectedItem as GroupeItem);
        }

        private void _tachesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            _controleur.DefinirTacheSelectionnee(_groupesListBox.SelectedItem as TacheItem);
        }

        private void _ticketsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            _controleur.DefinirTicketSelectionne(_groupesListBox.SelectedItem as TicketItem);
        }

        #endregion Actions utilisateur

        #region ElementListViewItem

        private class ItemListViewItem<TItem, T> : ListViewItem
            where TItem :class, IItem<T>
        {
            private TItem _item;

            public ItemListViewItem(TItem item)
                : base(item.Libelle)
            {
                _item = item;
            }

            public TItem Item
            {
                get { return _item; }
            }

            public override bool Equals(object obj)
            {
                if (Object.ReferenceEquals(this, obj)) return true;
                ItemListViewItem<TItem, T> ilvItem = obj as ItemListViewItem<TItem, T>;
                TItem item = (ilvItem != null) ? ilvItem.Item : obj as TItem;
                if (item == null) return false;
                return this.Item.Equals(item);
            }

        }

        #endregion ElementListViewItem
    }
}