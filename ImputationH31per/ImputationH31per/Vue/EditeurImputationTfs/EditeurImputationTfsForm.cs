using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;
using ImputationH31per.Vue.EditeurImputationTfs.Modele;

namespace ImputationH31per.Vue.EditeurImputationTfs
{
    public partial class EditeurImputationTfsForm : IHForm
    {
        #region Membre

        private readonly IEditeurImputationTfsFormControleur _controleur;
        private readonly IEditeurImputationTfsFormModele _modele;

        private ImputationTfsDataEditeur _imputationTfs;
        private System.Windows.Forms.TextBox _numeroComplementaireTextBox;

        #endregion Membre

        #region ctor

        public EditeurImputationTfsForm(IIHFormModele formModele, IEditeurImputationTfsFormModele modele, IEditeurImputationTfsFormControleur controleur)
            : this(formModele, ServicePreferenceModele.Instance.ObtenirIHFormControleur(formModele), modele, controleur)
        {
        }

        public EditeurImputationTfsForm(IIHFormModele formModele, IIHFormControleur formControleur, IEditeurImputationTfsFormModele modele, IEditeurImputationTfsFormControleur controleur)
            : base(formModele, formControleur)
        {
            InitializeComponent();

            _modele = modele;
            _controleur = controleur;

            // ajouter un TextBox pour permuter avec la ComboBox en cas de Numero Complémentaire non éditable
            _numeroComplementaireTextBox = new System.Windows.Forms.TextBox();
            _numeroComplementaireTextBox.Location = _numeroComplementaireComboBox.Location;
            _numeroComplementaireTextBox.Size = _numeroComplementaireComboBox.Size;
            _numeroComplementaireTextBox.Visible = false;
            this.Controls.Add(_numeroComplementaireTextBox);

            _modele.PropertyChanged += _modele_PropertyChanged;

            ImputationTfs = _modele.ImputationTfs;
        }

        /// <summary>
        /// Pour le designer
        /// </summary>
        protected EditeurImputationTfsForm()
        {
            InitializeComponent();
        }

        ~EditeurImputationTfsForm()
        {
            if (_modele == null)
                return; // en mode design

            ImputationTfs = null;
            _modele.PropertyChanged -= _modele_PropertyChanged;
        }

        #endregion ctor

        #region Propriétés

        private ImputationTfsDataEditeur ImputationTfs
        {
            get { return _imputationTfs; }
            set
            {
                if (_imputationTfs != null)
                {
                    _imputationTfs.PropertyChanged -= ImputationTfs_PropertyChanged;
                }
                _imputationTfs = value;
                if (_imputationTfs != null)
                {
                    _imputationTfs.PropertyChanged += ImputationTfs_PropertyChanged;
                }
            }
        }

        #endregion Propriétés

        #region Surcharges

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_modele == null)
                return; // en mode design

            // synchronisation de l'interface avec le modele
            MiseAJourEditeurImputationTfsFormModele(_modele);

            if (false == _modele.EstNumeroImputationTfsModifiable)
                _nomLabel.Focus();  // passer le focus au control suivant
        }

        #endregion Surcharges

        #region Evennement modele

        private void _modele_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstanteIEditeurImputationTfsFormModele.ConstanteProprieteEstNumeroImputationTfsModifiable:
                    MiseAJourEstNumeroImputationTfsModifiable(_modele.EstNumeroImputationTfsModifiable);
                    break;

                case ConstanteIEditeurImputationTfsFormModele.ConstanteProprieteImputationTfs:
                    MiseAJourImputationTfs(_modele.ImputationTfs);
                    break;

                default:
                    Debug.Fail("Cas non prévus");
                    break;
            }
        }

        private void ImputationTfs_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstanteImputationTfsDataEditeur.ConstanteProprieteDateEstimCourant:
                    MiseAJourDateEstimCourant(ImputationTfs.DateEstimCourant);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteDateSommeConsommee:
                    MiseAJourDateSommeConsommee(ImputationTfs.DateSommeConsommee);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteEstDateEstimCourantModifiable:
                    MiseAJourEtatModificationDateEstimCourant(ImputationTfs.EstDateEstimCourantModifiable);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteEstDateSommeConsommeeModifiable:
                    MiseAJourEtatModificationDateSommeConsommee(ImputationTfs.EstDateSommeConsommeeModifiable);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteEstEstimCourantModifiable:
                    MiseAJourEtatModificationEstimCourant(ImputationTfs.EstEstimCourantModifiable);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteEstEstTacheAvecEstimModifiablet:
                    MiseAJourEtatModificationEstTacheAvecEstim(ImputationTfs.EstEstTacheAvecEstimModifiable);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteEstimCourant:
                    MiseAJourEstimCourant(ImputationTfs.EstimCourant);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteEstImputationTfsValide:
                    MiseAJourEstImputationTfsValide(ImputationTfs.EstImputationTfsValide);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteEstNomComplementaireModifiable:
                    MiseAJourEtatModificationNomComplementaire(ImputationTfs.EstNomComplementaireModifiable);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteEstNomGroupementModifiable:
                    MiseAJourEtatModificationNomGroupement(ImputationTfs.EstNomGroupementModifiable);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteEstNomModifiable:
                    MiseAJourEtatModificationNom(ImputationTfs.EstNomModifiable);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteEstSommeConsommeeModifiable:
                    MiseAJourEtatModificationSommeConsommee(ImputationTfs.EstSommeConsommeeModifiable);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteEstCommentaireModifiable:
                    MiseAJourEtatModificationCommentaire(ImputationTfs.EstCommentaireModifiable);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteEstTacheAvecEstim:
                    MiseAJourEstTacheAvecEstim(ImputationTfs.EstTacheAvecEstim);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteNom:
                    MiseAJourNom(ImputationTfs.Nom);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteNomComplementaire:
                    MiseAJourNomComplementaire(ImputationTfs.NomComplementaire);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteNomGroupement:
                    MiseAJourNomGroupement(ImputationTfs.NomGroupement);
                    break;

                case ConstanteImputationTfsDataEditeur.ConstanteProprieteSommeConsommee:
                    MiseAJourSommeConsommee(ImputationTfs.SommeConsommee);
                    break;

                default:
                    break;
            }
        }

        #endregion Evennement modele

        #region Mise à jour interface depuis modele

        #region EditeurImputationTfsFormModele

        private void MiseAJourChoixNomGroupement(IEnumerable<string> choix)
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            source.AddRange(choix.ToArray());
            _nomGroupementComboBox.AutoCompleteCustomSource = source;
        }

        private void MiseAJourChoixNumeroComplementaire(IEnumerable<int?> choix)
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            string[] items = choix
                .Select(c => c.HasValue ? ("" + c) : "")
                .ToArray();
            source.AddRange(items);
            _numeroComplementaireComboBox.AutoCompleteCustomSource = source;
        }

        private void MiseAJourEditeurImputationTfsFormModele(IEditeurImputationTfsFormModele modele)
        {
            IEnumerable<int?> numeroComplementaires = _modele.ObtenirChoixNumeroComplementaires();
            IEnumerable<string> nomGroupements = _modele.ObtenirChoixNomGroupements();
            ImputationTfsDataEditeur imputationTfs = modele.ImputationTfs;
            bool estNumeroImputationTfsModifiable = modele.EstNumeroImputationTfsModifiable;

            MiseAJourChoixNumeroComplementaire(numeroComplementaires);
            MiseAJourChoixNomGroupement(nomGroupements);
            MiseAJourImputationTfs(imputationTfs);
            MiseAJourEstNumeroImputationTfsModifiable(estNumeroImputationTfsModifiable);
        }

        private void MiseAJourEstNumeroImputationTfsModifiable(bool estNumeroImputationTfsModifiable)
        {
            CommencerMiseAJour();

            bool estLectureSeul = (false == estNumeroImputationTfsModifiable);
            _numeroTextBox.ReadOnly = estLectureSeul;
            _numeroComplementaireTextBox.Visible = estLectureSeul;
            _numeroComplementaireTextBox.ReadOnly = estLectureSeul;
            _numeroComplementaireComboBox.Enabled = !estLectureSeul;
            _numeroComplementaireComboBox.Visible = !estLectureSeul;

            TerminerMiseAJour();
        }

        #endregion EditeurImputationTfsFormModele

        #region ImputationTfsDataEditeur

        #region Maj Valeurs éditable

        private void MiseAJourCommentaire(string commentaire)
        {
            CommencerMiseAJour();

            _commentaireTextBox.Text = commentaire;

            TerminerMiseAJour();
        }

        private void MiseAJourDateEstimCourant(DateTimeOffset? dateEstimCourant)
        {
            CommencerMiseAJour();

            _dateEstimationDateTimePicker.Value = (dateEstimCourant ?? DateTimeOffset.UtcNow).LocalDateTime;

            TerminerMiseAJour();
        }

        private void MiseAJourDateSommeConsommee(DateTimeOffset? dateSommeConsomme)
        {
            CommencerMiseAJour();

            _dateConsommeeDateTimePicker.Value = (dateSommeConsomme ?? DateTimeOffset.UtcNow).LocalDateTime;

            TerminerMiseAJour();
        }

        private void MiseAJourEstimCourant(double? estimCourant)
        {
            CommencerMiseAJour();

            _estimationCouranteTextBox.Text = estimCourant.HasValue ? (String.Empty + estimCourant) : String.Empty;

            TerminerMiseAJour();
        }

        private void MiseAJourEstTacheAvecEstim(bool estTacheAvecEstimation)
        {
            CommencerMiseAJour();

            _avecEstimationCheckBox.Checked = estTacheAvecEstimation;

            TerminerMiseAJour();
        }

        private void MiseAJourNom(string nom)
        {
            CommencerMiseAJour();

            _nomTextBox.Text = nom;

            TerminerMiseAJour();
        }

        private void MiseAJourNomComplementaire(string nomComplementaire)
        {
            CommencerMiseAJour();

            _nomComplementaireTextBox.Text = nomComplementaire;

            TerminerMiseAJour();
        }

        private void MiseAJourNomGroupement(string nomGroupement)
        {
            CommencerMiseAJour();

            _nomGroupementComboBox.Text = nomGroupement;

            TerminerMiseAJour();
        }

        private void MiseAJourNumero(int numero, bool estImputationTfsValide)
        {
            CommencerMiseAJour();

            bool estAvecNumero = estImputationTfsValide;
            _numeroTextBox.Text = estAvecNumero ? String.Empty + numero : String.Empty;

            TerminerMiseAJour();
        }

        private void MiseAJourNumeroComplementaire(int? numeroComplementaire, bool estImputationTfsValide)
        {
            CommencerMiseAJour();

            bool estAvecNumeroComplementaire = estImputationTfsValide && numeroComplementaire.HasValue;
            string texteNumeroComplementaire = estAvecNumeroComplementaire ? String.Empty + numeroComplementaire : String.Empty;
            _numeroComplementaireComboBox.Text = texteNumeroComplementaire;
            _numeroComplementaireTextBox.Text = texteNumeroComplementaire;

            TerminerMiseAJour();
        }

        private void MiseAJourSommeConsommee(double? sommeConsommee)
        {
            CommencerMiseAJour();

            _consommeeCouranteTextBox.Text = sommeConsommee.HasValue ? (String.Empty + sommeConsommee) : String.Empty;

            TerminerMiseAJour();
        }

        #endregion Maj Valeurs éditable

        #region Maj état modification

        private void MiseAJourEstImputationTfsValide(bool estImputationTfsValide)
        {
            CommencerMiseAJour();

            _okButton.Enabled = estImputationTfsValide;

            TerminerMiseAJour();
        }

        private void MiseAJourEtatModificationCommentaire(bool estCommentaireModifiable)
        {
            CommencerMiseAJour();

            _commentaireTextBox.Enabled = estCommentaireModifiable;

            TerminerMiseAJour();
        }

        private void MiseAJourEtatModificationDateEstimCourant(bool estDateEstimCourantModifiable)
        {
            CommencerMiseAJour();

            estDateEstimCourantModifiable = estDateEstimCourantModifiable || (false == String.IsNullOrEmpty(_estimationCouranteTextBox.Text));
            _dateEstimationDateTimePicker.Enabled = estDateEstimCourantModifiable;

            TerminerMiseAJour();
        }

        private void MiseAJourEtatModificationDateSommeConsommee(bool estDateSommeConsommeeModifiable)
        {
            CommencerMiseAJour();

            estDateSommeConsommeeModifiable = estDateSommeConsommeeModifiable || (false == String.IsNullOrEmpty(_consommeeCouranteTextBox.Text));
            _dateConsommeeDateTimePicker.Enabled = estDateSommeConsommeeModifiable;

            TerminerMiseAJour();
        }

        private void MiseAJourEtatModificationEstimCourant(bool estEstimCourantModifiable)
        {
            CommencerMiseAJour();

            _estimationCouranteTextBox.Enabled = estEstimCourantModifiable;

            TerminerMiseAJour();
        }

        private void MiseAJourEtatModificationEstTacheAvecEstim(bool estTacheAvecEstim)
        {
            CommencerMiseAJour();

            _avecEstimationCheckBox.Enabled = estTacheAvecEstim;

            TerminerMiseAJour();
        }

        private void MiseAJourEtatModificationNom(bool estNomModifiable)
        {
            CommencerMiseAJour();

            _nomTextBox.Enabled = estNomModifiable;

            TerminerMiseAJour();
        }

        private void MiseAJourEtatModificationNomComplementaire(bool estNomComplementaireModifiable)
        {
            CommencerMiseAJour();

            estNomComplementaireModifiable = estNomComplementaireModifiable || (false == String.IsNullOrEmpty(_numeroComplementaireComboBox.Text)); // prendre en compte l'état de l'interface
            _nomComplementaireTextBox.Enabled = estNomComplementaireModifiable;

            TerminerMiseAJour();
        }

        private void MiseAJourEtatModificationNomGroupement(bool estNomGroupementModifiable)
        {
            CommencerMiseAJour();

            _nomGroupementComboBox.Enabled = estNomGroupementModifiable;

            TerminerMiseAJour();
        }

        private void MiseAJourEtatModificationSommeConsommee(bool estSommeConsommeeModifiable)
        {
            CommencerMiseAJour();

            _consommeeCouranteTextBox.Enabled = estSommeConsommeeModifiable;

            TerminerMiseAJour();
        }

        #endregion Maj état modification

        private void MiseAJourImputationTfs(ImputationTfsDataEditeur imputationTfs)
        {
            CommencerMiseAJour();

            ImputationTfs = imputationTfs;

            bool estImputationTfsValide = imputationTfs.EstImputationTfsValide;
            MiseAJourEstImputationTfsValide(estImputationTfsValide);

            MiseAJourNumero(imputationTfs.Numero, estImputationTfsValide);
            MiseAJourNumeroComplementaire(imputationTfs.NumeroComplementaire, estImputationTfsValide);

            MiseAJourNom(imputationTfs.Nom);
            MiseAJourNomComplementaire(imputationTfs.NomComplementaire);
            MiseAJourNomGroupement(imputationTfs.NomGroupement);
            MiseAJourEtatModificationNomComplementaire(imputationTfs.EstNomComplementaireModifiable);
            MiseAJourCommentaire(imputationTfs.Commentaire);
            MiseAJourEtatModificationCommentaire(imputationTfs.EstCommentaireModifiable);

            MiseAJourEstTacheAvecEstim(imputationTfs.EstTacheAvecEstim);
            MiseAJourEtatModificationEstTacheAvecEstim(imputationTfs.EstEstTacheAvecEstimModifiable);

            MiseAJourEstimCourant(imputationTfs.EstimCourant);
            MiseAJourDateEstimCourant(imputationTfs.DateEstimCourant);
            MiseAJourEtatModificationEstimCourant(imputationTfs.EstEstimCourantModifiable);
            MiseAJourEtatModificationDateEstimCourant(imputationTfs.EstDateEstimCourantModifiable);

            MiseAJourSommeConsommee(imputationTfs.SommeConsommee);
            MiseAJourDateSommeConsommee(imputationTfs.DateSommeConsommee);
            MiseAJourEtatModificationSommeConsommee(imputationTfs.EstSommeConsommeeModifiable);
            MiseAJourEtatModificationDateSommeConsommee(imputationTfs.EstDateSommeConsommeeModifiable);

            TerminerMiseAJour();
        }

        #endregion ImputationTfsDataEditeur

        #endregion Mise à jour interface depuis modele

        #region Actions utilisateur

        private void _avecEstimationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            bool estTacheAvecEstim = _avecEstimationCheckBox.Checked;
            _controleur.DefinirEstTacheAvecEstim(estTacheAvecEstim);
        }

        //private void _consommeeCouranteTextBox_TextChanged(object sender, EventArgs e)
        //{
        //    if (EstMiseAJourDepuisModeleEnCours)
        //        return;

        //    string sommeConsommee = _consommeeCouranteTextBox.Text;
        //    Action action = () => _controleur.DefinirSommeConsommee(sommeConsommee);
        //    ValidationAvecErrorProvider(action, _consommeeCouranteTextBox, _errorProvider, null);
        //}

        private void _commentaireTextBox_TextChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            string commentaire = _commentaireTextBox.Text;
            Action action = () => _controleur.DefinirCommentaire(commentaire);
            ValidationAvecErrorProvider(action, _commentaireTextBox, _errorProvider, null);
        }

        private void _commentaireTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            string commentaire = _commentaireTextBox.Text;
            Action action = () => _controleur.DefinirCommentaire(commentaire);
            ValidationAvecErrorProvider(action, _commentaireTextBox, _errorProvider, null);
        }

        private void _consommeeCouranteTextBox_TextChanged(object sender, EventArgs e)
        {
            MiseAJourEtatModificationDateSommeConsommee(_imputationTfs.EstDateSommeConsommeeModifiable);
        }

        private void _consommeeCouranteTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            string sommeConsommee = _consommeeCouranteTextBox.Text;
            Action action = () => _controleur.DefinirSommeConsommee(sommeConsommee);
            ValidationAvecErrorProvider(action, _consommeeCouranteTextBox, _errorProvider, e);
        }

        private void _consommeeMaintenantButton_Click(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            _controleur.DefinirDateSommeConsommee(DateTimeOffset.UtcNow);
        }

        private void _copierDateButton_Click(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            _controleur.DefinirDateSommeConsommee(_dateEstimationDateTimePicker.Value.ToDateTimeOffset());
        }

        private void _dateConsommeeDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            // masquer/afficher pour forcer la reconnaissance de la saisie
            _dateConsommeeDateTimePicker.Visible = false;
            _dateConsommeeDateTimePicker.Visible = true;
            DateTimeOffset dateSommeConsommee = _dateConsommeeDateTimePicker.Value.ToDateTimeOffset();
            Action action = () => _controleur.DefinirDateSommeConsommee(dateSommeConsommee);
            ValidationAvecErrorProvider(action, _dateConsommeeDateTimePicker, _errorProvider, e);
        }

        private void _dateEstimationDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            // masquer/afficher pour forcer la reconnaissance de la saisie
            _dateEstimationDateTimePicker.Visible = false;
            _dateEstimationDateTimePicker.Visible = true;
            DateTimeOffset dateEstimCourant = _dateEstimationDateTimePicker.Value.ToDateTimeOffset();
            Action action = () => _controleur.DefinirDateEstimCourant(dateEstimCourant);
            ValidationAvecErrorProvider(action, _dateEstimationDateTimePicker, _errorProvider, e);
        }

        private void _estimationCouranteTextBox_TextChanged(object sender, EventArgs e)
        {
            MiseAJourEtatModificationDateEstimCourant(_imputationTfs.EstDateEstimCourantModifiable);
        }

        private void _estimationCouranteTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            string estimCourant = _estimationCouranteTextBox.Text;
            Action action = () => _controleur.DefinirEstimCourant(estimCourant);
            ValidationAvecErrorProvider(action, _estimationCouranteTextBox, _errorProvider, e);
        }

        private void _estimationMaintenantButton_Click(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            _controleur.DefinirDateEstimCourant(DateTimeOffset.UtcNow);
        }

        private void _groupementComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            string nomGroupement = _nomGroupementComboBox.Text;
            Action action = () => _controleur.DefinirNomGroupement(nomGroupement);
            ValidationAvecErrorProvider(action, _numeroTextBox, _errorProvider, e);
        }

        private void _nomComplementaireTextBox_TextChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            string nomComplementaire = _nomComplementaireTextBox.Text;
            Action action = () => _controleur.DefinirNomComplementaire(nomComplementaire);
            ValidationAvecErrorProvider(action, _nomComplementaireTextBox, _errorProvider, null);
        }

        private void _nomComplementaireTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            string nom = _nomComplementaireTextBox.Text;
            Action action = () => _controleur.DefinirNomComplementaire(nom);
            ValidationAvecErrorProvider(action, _nomComplementaireTextBox, _errorProvider, e);
        }

        private void _nomTextBox_TextChanged(object sender, EventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            string nom = _nomTextBox.Text;
            Action action = () => _controleur.DefinirNom(nom);
            ValidationAvecErrorProvider(action, _nomTextBox, _errorProvider, null);
        }

        private void _nomTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            string nom = _nomTextBox.Text;
            Action action = () => _controleur.DefinirNom(nom);
            ValidationAvecErrorProvider(action, _nomTextBox, _errorProvider, e);
        }

        private void _numeroComplementaireComboBox_TextChanged(object sender, EventArgs e)
        {
            MiseAJourEtatModificationNomComplementaire(_imputationTfs.EstNomComplementaireModifiable);
        }

        private void _numeroComplementaireComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            if (false == _modele.EstNumeroImputationTfsModifiable)
                return; // pas de validation

            string numero = _numeroTextBox.Text;
            string numeroComplementaire = _numeroComplementaireComboBox.Text;
            Action action = () => _controleur.DefinirNumeroEtNumeroComplementaire(numero, numeroComplementaire);
            ValidationAvecErrorProvider(action, _numeroComplementaireComboBox, _errorProvider, e);
        }

        private void _numeroTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (EstMiseAJourEnCours)
                return;

            if (false == _modele.EstNumeroImputationTfsModifiable)
                return; // pas de validation

            string numero = _numeroTextBox.Text;
            string numeroComplementaire = _numeroComplementaireComboBox.Text;
            Action action = () => _controleur.DefinirNumeroEtNumeroComplementaire(numero, numeroComplementaire);
            ValidationAvecErrorProvider(action, _numeroTextBox, _errorProvider, e);
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            Debug.Assert(_modele.ImputationTfs.EstImputationTfsValide);

            if (EstMiseAJourEnCours)
                return;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        #endregion Actions utilisateur
    }
}