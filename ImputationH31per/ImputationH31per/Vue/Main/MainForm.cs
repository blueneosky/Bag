using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Modele;
using ImputationH31per.Util;
using ImputationH31per.Vue.Main.Modele;
using ImputationH31per.Controle.ImputationTfsListView.Modele;

namespace ImputationH31per.Vue.Main
{
    public partial class MainForm : IHForm
    {
        #region Membres

        private readonly IMainFormControleur _controleur;
        private readonly GestionnaireRaccourcis _gestionnaireRaccourcis;
        private readonly IMainFormModele _modele;
        private readonly IImputationTfsListViewControlModele _imputationTfsListViewControlModele;
        private readonly IImputationTfsListViewControlControleur _imputationTfsListViewControlControleur;

        #endregion Membres

        #region ctor

        public static MainForm Nouveau(IIHFormModele formModele, IMainFormModele modele, IMainFormControleur controleur)
        {
            IIHFormControleur formControleur = ServicePreferenceModele.Instance.ObtenirIHFormControleur(formModele);
            ImputationTfsListViewControlModele imputationTfsListViewControlModele = new ImputationTfsListViewControlModele(modele);
            ImputationTfsListViewControlControleur imputationTfsListViewControlControleur = new ImputationTfsListViewControlControleur(imputationTfsListViewControlModele, controleur);

            MainForm instance = new MainForm(
                 formModele, formControleur
                , modele, controleur
                , imputationTfsListViewControlModele, imputationTfsListViewControlControleur
                );

            return instance;
        }

        public MainForm(IIHFormModele formModele, IIHFormControleur formControleur
            , IMainFormModele modele, IMainFormControleur controleur
            , IImputationTfsListViewControlModele imputationTfsListViewControlModele, IImputationTfsListViewControlControleur imputationTfsListViewControlControleur)
            : base(formModele, formControleur)
        {
            InitializeComponent();

            _modele = modele;
            _controleur = controleur;
            _imputationTfsListViewControlModele = imputationTfsListViewControlModele;
            _imputationTfsListViewControlControleur = imputationTfsListViewControlControleur;

            // gestion raccourcis
            _gestionnaireRaccourcis = new GestionnaireRaccourcis()
            {
                { Keys.Control | Keys.N, _controleur.NouvelleImputation },
                { Keys.Control | Keys.Shift | Keys.N , _controleur.NouvelleImputationsCourantes },
                { Keys.Control | Keys.R , _controleur.AfficherRapportMail },
                { Keys.Control | Keys.S , _controleur.EnregistrerData },
                { Keys.Control | Keys.Q , this.Close },
                { Keys.Control | Keys.W , this.Close },
            };

            _modele.PropertyChanged += _modele_PropertyChanged;
        }

        /// <summary>
        /// Réservé au designer
        /// </summary>
        private MainForm()
        {
            InitializeComponent();
        }

        ~MainForm()
        {
            _modele.PropertyChanged -= _modele_PropertyChanged;
        }

        #endregion ctor

        #region Surcharges

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_modele == null)
                return; // en mode design

            // synchronisation de l'interface avec le modele
            MiseAJourMainFormModele(_modele);

            // initialisation de _imputationTfsListViewControl
            _imputationTfsListViewControl.Initialiser(_imputationTfsListViewControlModele, _imputationTfsListViewControlControleur);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool traite = _gestionnaireRaccourcis.ProcessKey(keyData);
            if (traite)
                return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion Surcharges

        #region Evennement Modele

        private void _modele_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstanteMainFormModele.ConstanteProprieteTitre:
                    MiseAJourTitre(_modele.Titre);
                    break;

                default:
                    Debug.Fail("Cas non géré");
                    break;
            }
        }

        #endregion Evennement Modele

        #region Mise à jour interface

        private void MiseAJourMainFormModele(IMainFormModele modele)
        {
            CommencerMiseAJour();

            MiseAJourTitre(modele.Titre);

            TerminerMiseAJour();
        }

        private void MiseAJourTitre(string titre)
        {
            CommencerMiseAJour();

            this.Text = titre;

            TerminerMiseAJour();
        }

        #endregion Mise à jour interface

        #region Actions utilisateur

        private void _enregistrerButton_Click(object sender, EventArgs e)
        {
            _controleur.EnregistrerData();
        }

        private void _enregistrerEtFermerButton_Click(object sender, EventArgs e)
        {
            _controleur.EnregistrerData();
            this.Close();
        }

        private void _importExcelButton_Click(object sender, EventArgs e)
        {
            _controleur.AfficherImportExcel();
        }

        private void _nouvelleSaisieButton_Click(object sender, EventArgs e)
        {
            _controleur.NouvelleImputationsCourantes();
        }

        private void _saisieRapideButton_Click(object sender, EventArgs e)
        {
            _controleur.NouvelleImputation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _controleur.AfficherRapportMensuel();
        }

        private void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            bool valide = _controleur.VerifierEtEnregistrerData();
            if (!valide)
            {
                e.Cancel = true;
            }
        }

        private void rapportButton_Click(object sender, EventArgs e)
        {
            _controleur.AfficherRapportMail();
        }

        #endregion Actions utilisateur
    }
}