using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;
using ImputationH31per.Vue.RapportMail.Modele;

namespace ImputationH31per.Vue.RapportMail
{
#warning TODO - point BETA - remplacer la ListBox par le controle dédié au ImputationTfs et activer Checbox (faire implémentation)

    public partial class RapportMailForm : IHForm
    {
        #region Membres

        private readonly IRapportMailFormControleur _controleur;
        private readonly GestionnaireRaccourcis _gestionnaireRaccourcis;
        private readonly IRapportMailFormModele _modele;

        #endregion Membres

        #region ctor

        public RapportMailForm(IIHFormModele formModele, IIHFormControleur formControleur, IRapportMailFormModele modele, IRapportMailFormControleur controleur)
            : base(formModele, formControleur)
        {
            InitializeComponent();

            _modele = modele;
            _controleur = controleur;

            // gestion raccourcis
            _gestionnaireRaccourcis = new GestionnaireRaccourcis()
            {
                { Keys.Control | Keys.C, _controleur.CopierPressePapier },
                { Keys.Control | Keys.Q , this.Close },
                { Keys.Control | Keys.W , this.Close },
                { Keys.Escape , this.Close },
            };

            _modele.PropertyChanged += _modele_PropertyChanged;
        }

        public RapportMailForm(IIHFormModele formModele, IRapportMailFormModele modele, IRapportMailFormControleur controleur)
            : this(formModele, ServicePreferenceModele.Instance.ObtenirIHFormControleur(formModele), modele, controleur)
        {
        }

        private RapportMailForm()
        {
            InitializeComponent();
        }

        ~RapportMailForm()
        {
            _modele.PropertyChanged -= _modele_PropertyChanged;
        }

        #endregion ctor

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_modele == null)
                return; // en mode design

            // synchronisation de l'interface avec le modele
            MiseAJourIRapportMailFormModele(_modele);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool traite = _gestionnaireRaccourcis.ProcessKey(keyData);
            if (traite)
                return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion Overrides

        #region Evennement modele

        private void _modele_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstanteIRapportMailFormModele.ConstanteProprieteTempsDebut:
                    MiseAJourTempsDebut(_modele.TempsDebut);
                    break;

                case ConstanteIRapportMailFormModele.ConstanteProprieteTempsFin:
                    MiseAJourTempsFin(_modele.TempsFin);
                    break;

                case ConstanteIRapportMailFormModele.ConstanteProprieteTexteRapport:
                    MiseAJourTexteRapport(_modele.TexteRapport);
                    break;

                case ConstanteIRapportMailFormModele.ConstanteProprieteImputationTfsDisponibles:
                    MiseAJourImputationTfsDisponibles(_modele.ImputationTfsDisponibles);
                    break;

                case ConstanteIRapportMailFormModele.ConstanteProprieteImputationTfsSelectionnees:
                    MiseAJourImputationTfsSelectionnees(_modele.ImputationTfsSelectionnees);
                    break;

                case ConstanteIRapportMailFormModele.ConstanteProprieteSommeDifferenceHeureConsommee:
                    MiseAJourSommeDifferenceHeureConsommee(_modele.SommeDifferenceHeureConsommee);
                    break;

                default:
                    Debug.Fail("Cas non prévus");
                    break;
            }
        }

        #endregion Evennement modele

        #region Mise à jour interface depuis modele

        private void MiseAJourImputationTfsDisponibles(IEnumerable<IInformationImputationTfs> imputationTfsDisponibles)
        {
            CommencerMiseAJour();

            _listBox.BeginUpdate();

            _listBox.Items.Clear();

            ListBoxItem[] items = imputationTfsDisponibles
                .Select(i => new ListBoxItem(i))
                .ToArray();
            _listBox.Items.AddRange(items);

            _listBox.EndUpdate();

            TerminerMiseAJour();
        }

        private void MiseAJourImputationTfsSelectionnees(IEnumerable<IInformationImputationTfs> imputationTfsSelectionnees)
        {
            CommencerMiseAJour();

            _listBox.BeginUpdate();

            HashSet<IInformationImputationTfs> source = new HashSet<IInformationImputationTfs>(imputationTfsSelectionnees);

            _listBox.SelectedItems.Clear();
            IEnumerable<ListBoxItem> items = _listBox.Items
                .OfType<ListBoxItem>()
                .ToArray();
            foreach (ListBoxItem item in items)
            {
                if (source.Contains(item.ImputationTfs))
                    _listBox.SelectedItems.Add(item);
            }

            _listBox.EndUpdate();

            TerminerMiseAJour();
        }

        private void MiseAJourIRapportMailFormModele(IRapportMailFormModele modele)
        {
            CommencerMiseAJour();

            MiseAJourImputationTfsSelectionnees(modele.ImputationTfsSelectionnees);
            MiseAJourImputationTfsDisponibles(modele.ImputationTfsDisponibles);
            MiseAJourTexteRapport(modele.TexteRapport);
            MiseAJourSommeDifferenceHeureConsommee(modele.SommeDifferenceHeureConsommee);
            MiseAJourTempsDebut(modele.TempsDebut);
            MiseAJourTempsFin(modele.TempsFin);

            TerminerMiseAJour();
        }

        private void MiseAJourSommeDifferenceHeureConsommee(int sommeDifferenceHeureConsommee)
        {
            CommencerMiseAJour();

            int heures = sommeDifferenceHeureConsommee % 8;
            int jours = sommeDifferenceHeureConsommee / 8;

            string texte = "Total : ";
            if (jours > 0)
                texte += jours + " jour" + (jours > 1 ? "s" : String.Empty) + " et ";
            texte += heures + " heures";
            _rapportLabel.Text = texte;

            TerminerMiseAJour();
        }

        private void MiseAJourTempsDebut(DateTimeOffset tempsDebut)
        {
            CommencerMiseAJour();

            _dateDebutDateTimePicker.Value = tempsDebut.LocalDateTime;

            TerminerMiseAJour();
        }

        private void MiseAJourTempsFin(DateTimeOffset tempsFin)
        {
            CommencerMiseAJour();

            _dateFinDateTimePicker.Value = tempsFin.LocalDateTime;

            TerminerMiseAJour();
        }

        private void MiseAJourTexteRapport(string rapportTexte)
        {
            CommencerMiseAJour();

            _rapportTextBox.Text = rapportTexte;

            TerminerMiseAJour();
        }

        #endregion Mise à jour interface depuis modele

        #region Actions utilisateur

        private void _copierButton_Click(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            _controleur.CopierPressePapier();
        }

        private void _copierEtFermerButton_Click(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            _controleur.CopierPressePapier();
            this.Close();
        }

        private void _dateDebutDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            _controleur.DefinirTempsDebut(_dateDebutDateTimePicker.Value);
        }

        private void _dateFinDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            _controleur.DefinirTempsFin(_dateFinDateTimePicker.Value);
        }

        #endregion Actions utilisateur

        #region ListBoxItem

        private class ListBoxItem
        {
            private readonly IInformationImputationTfs _imputationTfs;

            public ListBoxItem(IInformationImputationTfs imputationTfs)
            {
                Debug.Assert(imputationTfs != null);
                _imputationTfs = imputationTfs;
            }

            public IInformationImputationTfs ImputationTfs
            {
                get { return _imputationTfs; }
            }

            // override object.Equals
            public override bool Equals(object obj)
            {
                bool resultat = false;

                IInformationImputationTfs imputationTfs = obj as IInformationImputationTfs;
                if (imputationTfs != null)
                {
                    resultat = imputationTfs.EstLiee(_imputationTfs);
                }
                else
                {
                    ListBoxItem listBoxItem = obj as ListBoxItem;
                    if (listBoxItem != null)
                    {
                        resultat = Object.ReferenceEquals(this, obj);
                    }
                }

                return resultat;
            }

            // override object.GetHashCode
            public override int GetHashCode()
            {
                return _imputationTfs.DateHorodatage.GetHashCode();
            }

            public override string ToString()
            {
                return "" + _imputationTfs.DateHorodatage + " - " + _imputationTfs.NumeroComplet() + " - " + _imputationTfs.NomComplet();
            }
        }

        #endregion ListBoxItem
    }
}