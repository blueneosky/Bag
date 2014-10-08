using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
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

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteGroupes:
                    RafraichirGroupes();
                    break;

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteGroupeSelectionne:
                    RafraichirGroupeSelectionne();
                    break;

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteTaches:
                    RafraichirTaches();
                    break;

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteTacheSelectionnee:
                    RafraichirTacheSelectionnee();
                    break;

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteTickets:
                    RafraichirTickets();
                    break;

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteTicketSelectionne:
                    RafraichirTicketSelectionne();
                    break;

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupementCourant:
                    RafraichirRegroupementCourant();
                    RafraichirRegroupementCourantPeutAjouter();
                    break;

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupementCourantNom:
                    RafraichirRegroupementCourantNom();
                    break;

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupementCourantTotalHeure:
                    RafraichirRegroupementCourantTotalHeure();
                    break;

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupementCourantItemSelectionne:
                    RafraichirRegroupementCourantItemSelectionne();
                    break;

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupements:
                    RafraichirRegroupements();
                    break;

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupementsItemSelectionne:
                    RafraichirRegroupementsItemSelectionne();
                    break;

                case ConstanteIRapportMensuelFormModele.ConstanteProprieteRegroupementRapports:
                    RafraichirRegroupementRapports();
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
            RafraichirGroupes();
            RafraichirGroupeSelectionne();
            RafraichirTaches();
            RafraichirTacheSelectionnee();
            RafraichirTickets();
            RafraichirTicketSelectionne();
            RafraichirRegroupementCourant();
            RafraichirRegroupementCourantNom();
            RafraichirRegroupementCourantPeutAjouter();
            RafraichirRegroupementCourantTotalHeure();
            RafraichirRegroupementCourantItemSelectionne();
            RafraichirRegroupements();
            RafraichirRegroupementsItemSelectionne();
            RafraichirRegroupementRapports();
        }

        private void RafraichirMoisAnnee()
        {
            CommencerMiseAJour();

            DateTime dateMoisAnnee = _modele.DateMoisAnnee.LocalDateTime;
            _moisAnneeDateTimePicker.Value = dateMoisAnnee;

            TerminerMiseAJour();
        }

        private void RafraichirGroupes()
        {
            CommencerMiseAJour();

            RafraichirListBox<GroupeItem, IInformationTacheTfs>(_groupesListBox, _modele.Groupes);
            RafraichirGroupeSelectionne();

            TerminerMiseAJour();
        }

        private void RafraichirGroupeSelectionne()
        {
            CommencerMiseAJour();

            RafraichirSelectionListBox<GroupeItem, IInformationTacheTfs>(_groupesListBox, _modele.GroupeSelectionne);

            TerminerMiseAJour();
        }

        private void RafraichirTaches()
        {
            CommencerMiseAJour();

            RafraichirListBox<TacheItem, IInformationTacheTfs>(_tachesListBox, _modele.Taches);
            RafraichirTacheSelectionnee();

            TerminerMiseAJour();
        }

        private void RafraichirTacheSelectionnee()
        {
            CommencerMiseAJour();

            RafraichirSelectionListBox<TacheItem, IInformationTacheTfs>(_tachesListBox, _modele.TacheSelectionnee);

            TerminerMiseAJour();
        }

        private void RafraichirTickets()
        {
            CommencerMiseAJour();

            RafraichirListBox<TicketItem, IInformationTicketTfs>(_ticketsListBox, _modele.Tickets);
            RafraichirTicketSelectionne();

            TerminerMiseAJour();
        }

        private void RafraichirTicketSelectionne()
        {
            CommencerMiseAJour();

            RafraichirSelectionListBox<TicketItem, IInformationTicketTfs>(_ticketsListBox, _modele.TicketSelectionne);

            TerminerMiseAJour();
        }

        private void RafraichirRegroupementCourant()
        {
            CommencerMiseAJour();

            IEnumerable<IInformationItem<IInformationTacheTfs>> items = _modele.RegroupementCourant;
            items = items ?? new IInformationItem<IInformationTacheTfs>[0];
            RafraichirListBox<IInformationItem<IInformationTacheTfs>, IInformationTacheTfs>(_regroupementListBox, items);
            RafraichirTicketSelectionne();

            TerminerMiseAJour();
        }

        private void RafraichirRegroupementCourantPeutAjouter()
        {
            CommencerMiseAJour();

            IEnumerable<IInformationItem<IInformationTacheTfs>> items = _modele.RegroupementCourant;
            _ajouterGroupementButton.Enabled = items.Any();

            TerminerMiseAJour();
        }

        private void RafraichirRegroupementCourantItemSelectionne()
        {
            CommencerMiseAJour();

            RafraichirSelectionListBox<IInformationItem<IInformationTacheTfs>, IInformationTacheTfs>(_regroupementListBox, _modele.RegroupementCourantItemSelectionne);

            TerminerMiseAJour();
        }

        private void RafraichirRegroupementCourantTotalHeure()
        {
            CommencerMiseAJour();

            const string constFormat = "Total heures : {0}";
            _totalHeureRegroupementLabel.Text = String.Format(constFormat, _modele.RegroupementCourantTotalHeure);

            TerminerMiseAJour();
        }

        private void RafraichirRegroupementCourantNom()
        {
            CommencerMiseAJour();

            string nom = _modele.RegroupementCourant.Nom;
            if (false == String.Equals(nom, _nomGroupementTextBox.Text))
                _nomGroupementTextBox.Text = nom;

            TerminerMiseAJour();
        }

        private void RafraichirRegroupements()
        {
            CommencerMiseAJour();

            RafraichirListBox<Regroupement, string>(_regroupementsListBox, _modele.Regroupements);

            TerminerMiseAJour();
        }

        private void RafraichirRegroupementsItemSelectionne()
        {
            CommencerMiseAJour();

            RafraichirSelectionListBox<Regroupement, string>(_regroupementsListBox, _modele.RegroupementsItemSelectionne);

            TerminerMiseAJour();
        }

        private void RafraichirRegroupementRapports()
        {
            CommencerMiseAJour();

            IEnumerable<Regroupement> regroupements = _modele.RegroupementRapports;
            StringBuilder sb = new StringBuilder();
            foreach (var regroupement in regroupements)
            {
                sb.AppendLine(regroupement.Libelle);
                foreach (var item in regroupement.Items)
                {
                    sb
                        .Append("  - ")
                        .AppendLine(item.Libelle);
                }
#warning TODO ALPHA ALPHA
            }
            _textBox.Text = sb.ToString();

            TerminerMiseAJour();
        }

        #region Utilitaire

        private void RafraichirListBox<TItem, T>(ListBox listBox, IEnumerable<TItem> items)
            where TItem : class, IItem<T>
        {
            listBox.BeginUpdate();

            ListBox.ObjectCollection listBoxCollection = listBox.Items;
            TItem[] nouveauItems = items
                .OrderBy(i => i.Libelle)
                .ToArray();

            int ancienIndexItem = 0;
            int nouveauIndexItem = 0;
            while (ancienIndexItem < listBoxCollection.Count || nouveauIndexItem < nouveauItems.Length)
            {
                ListBoxItem<TItem, T> ancienLBItem = ancienIndexItem < listBoxCollection.Count ? (ListBoxItem<TItem, T>)listBoxCollection[ancienIndexItem] : null;
                TItem nouveauItem = nouveauIndexItem < nouveauItems.Length ? nouveauItems[nouveauIndexItem] : null;
                Func<ListBoxItem<TItem, T>> nouveauLBItem = () => new ListBoxItem<TItem, T>(nouveauItem);

                if (ancienLBItem == null)
                {
                    Debug.Assert(nouveauItem != null);
                    listBoxCollection.Add(nouveauLBItem());
                    ancienIndexItem++;
                    nouveauIndexItem++;
                }
                else if (nouveauItem == null)
                {
                    listBoxCollection.RemoveAt(ancienIndexItem);
                }
                else if (ancienLBItem.Equals(nouveauItem))
                {
                    ancienIndexItem++;
                    nouveauIndexItem++;
                }
                else
                {
                    int comp = String.Compare(nouveauItem.Libelle, ancienLBItem.Item.Libelle);
                    if (comp < 0)
                    {
                        // nouveau avant l'ancien
                        listBoxCollection.Insert(ancienIndexItem, nouveauLBItem());
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

            listBox.EndUpdate();
        }

        private void RafraichirSelectionListBox<TItem, T>(ListBox listBox, TItem itemSelectionne)
            where TItem : class, IItem<T>
        {
            listBox.BeginUpdate();

            if (itemSelectionne == null)
            {
                listBox.SelectedIndex = -1;
            }
            else if ((listBox.SelectedIndex < 0) || (false == listBox.SelectedItem.Equals(itemSelectionne)))
            {
                ListBox.ObjectCollection listBoxCollection = listBox.Items;
                int index = listBoxCollection
                    .Cast<ListBoxItem<TItem, T>>()
                    .TakeWhile(ilvItem => false == ilvItem.Equals(itemSelectionne))
                    .Count();
                if (index == listBoxCollection.Count)
                    index = -1;
                listBox.SelectedIndex = index;
            }

            listBox.EndUpdate();
        }

        #endregion Utilitaire

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
            ListBoxItem<GroupeItem, IInformationTacheTfs> lbItem = _groupesListBox.SelectedItem as ListBoxItem<GroupeItem, IInformationTacheTfs>;
            _controleur.DefinirGroupeSelectionne(lbItem != null ? lbItem.Item : null);
        }

        private void _tachesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            ListBoxItem<TacheItem, IInformationTacheTfs> lbItem = _tachesListBox.SelectedItem as ListBoxItem<TacheItem, IInformationTacheTfs>;
            _controleur.DefinirTacheSelectionnee(lbItem != null ? lbItem.Item : null);
        }

        private void _ticketsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            ListBoxItem<TicketItem, IInformationTicketTfs> lbItem = _ticketsListBox.SelectedItem as ListBoxItem<TicketItem, IInformationTicketTfs>;
            _controleur.DefinirTicketSelectionne(lbItem != null ? lbItem.Item : null);
        }

        private void _groupesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            ListBoxItem<GroupeItem, IInformationTacheTfs> lbItem = _groupesListBox.SelectedItem as ListBoxItem<GroupeItem, IInformationTacheTfs>;
            _controleur.AjouterAuRegroupement(lbItem != null ? lbItem.Item : null);
        }

        private void _tachesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            ListBoxItem<TacheItem, IInformationTacheTfs> lbItem = _tachesListBox.SelectedItem as ListBoxItem<TacheItem, IInformationTacheTfs>;
            _controleur.AjouterAuRegroupement(lbItem != null ? lbItem.Item : null);
        }

        private void _ticketsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            ListBoxItem<TicketItem, IInformationTicketTfs> lbItem = _ticketsListBox.SelectedItem as ListBoxItem<TicketItem, IInformationTicketTfs>;
            _controleur.AjouterAuRegroupement(lbItem != null ? lbItem.Item : null);
        }

        private void _regroupementListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            ListBoxItem<IInformationItem<IInformationTacheTfs>, IInformationTacheTfs> lbItem = _regroupementListBox.SelectedItem as ListBoxItem<IInformationItem<IInformationTacheTfs>, IInformationTacheTfs>;
            _controleur.DefinirRegroupementCourantItemSelectionne(lbItem != null ? lbItem.Item : null);
        }

        private void _regroupementListBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            if (e.KeyCode == Keys.Delete)
            {
                ListBoxItem<IInformationItem<IInformationTacheTfs>, IInformationTacheTfs> lbItem = _regroupementListBox.SelectedItem as ListBoxItem<IInformationItem<IInformationTacheTfs>, IInformationTacheTfs>;
                _controleur.RetirerDuRegroupement(lbItem != null ? lbItem.Item : null);
            }
        }

        private void _regroupementsListBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            if (e.KeyCode == Keys.Delete)
            {
                ListBoxItem<Regroupement, string> lbItem = _regroupementsListBox.SelectedItem as ListBoxItem<Regroupement, string>;
                _controleur.RetirerDeRegroupements(lbItem != null ? lbItem.Item : null);
            }
        }

        private void _nomGroupementTextBox_Validated(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            _controleur.DefinirNomRegroupementCourant(_nomGroupementTextBox.Text);
        }

        private void _ajouterGroupementButton_Click(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            _controleur.AjouterRegroupementCourant();
        }

        private void _regroupementsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours) return;
            ListBoxItem<Regroupement, string> lbItem = _regroupementsListBox.SelectedItem as ListBoxItem<Regroupement, string>;
            _controleur.RegroupementsItemSelectionne(lbItem != null ? lbItem.Item : null);
        }

        #endregion Actions utilisateur

        #region ElementListViewItem

        private class ListBoxItem<TItem, T>
            where TItem : class, IItem<T>
        {
            private TItem _item;

            public ListBoxItem(TItem item)
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
                ListBoxItem<TItem, T> ilvItem = obj as ListBoxItem<TItem, T>;
                TItem item = (ilvItem != null) ? ilvItem.Item : obj as TItem;
                if (item == null) return false;
                return this.Item.Equals(item);
            }

            public override int GetHashCode()
            {
                return _item.GetHashCode();
            }

            public override string ToString()
            {
                return _item.Libelle;
            }
        }

        #endregion ElementListViewItem
    }
}