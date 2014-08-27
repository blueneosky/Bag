using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Controle.ImputationTfsListView.Modele;
using ImputationH31per.Modele;
using ImputationH31per.Util;
using ImputationH31per.Vue.ImputationsCourantes.Modele;

namespace ImputationH31per.Vue.ImputationsCourantes
{
    public partial class ImputationsCourantesForm : IHForm
    {
        #region Membres

        private readonly IImputationsCourantesFormControleur _controleur;
        private readonly GestionnaireRaccourcis _gestionnaireRaccourcis;
        private readonly IImputationTfsListViewControlControleur _imputationTfsListViewControlControleur;
        private readonly IImputationTfsListViewControlModele _imputationTfsListViewControlModele;
        private readonly IImputationsCourantesFormModele _modele;

        #endregion Membres

        #region ctor

        public ImputationsCourantesForm(IIHFormModele formModele, IIHFormControleur formControleur
            , IImputationsCourantesFormModele modele, IImputationsCourantesFormControleur controleur
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
                { Keys.Control | Keys.N, () => _controleur.AjouterImputationTfs() },
                { Keys.Control | Keys.Enter, FermetureAvecValidation },
                { Keys.Control | Keys.W, this.Close },
            };
        }

        /// <summary>
        /// réservé designer
        /// </summary>
        private ImputationsCourantesForm()
        {
            InitializeComponent();
        }

        ~ImputationsCourantesForm()
        {
        }

        public static ImputationsCourantesForm Nouveau(IIHFormModele formModele, IImputationsCourantesFormModele modele, IImputationsCourantesFormControleur controleur)
        {
            IIHFormControleur formControleur = ServicePreferenceModele.Instance.ObtenirIHFormControleur(formModele);
            ImputationTfsListViewControlModele imputationTfsListViewControlModele = new ImputationTfsListViewControlModele(modele);
            ImputationTfsListViewControlControleur imputationTfsListViewControlControleur = new ImputationTfsListViewControlControleur(imputationTfsListViewControlModele, controleur);

            ImputationsCourantesForm instance = new ImputationsCourantesForm(
                 formModele, formControleur
                , modele, controleur
                , imputationTfsListViewControlModele, imputationTfsListViewControlControleur
                );

            return instance;
        }

        #endregion ctor

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_modele == null)
                return; // en mode design

            // synchronisation de l'interface avec le modele

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

        #endregion Overrides

        #region Evennement modele

        #endregion Evennement modele

        #region Mise à jour interface depuis modele

        #endregion Mise à jour interface depuis modele

        #region Actions utilisateur

        private void _ajouterImputationTfsButton_Click(object sender, EventArgs e)
        {
            _controleur.AjouterImputationTfs();
        }

        private void _enregistrerEtFermerButton_Click(object sender, EventArgs e)
        {
            FermetureAvecValidation();
        }

        private void FermetureAvecValidation()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        #endregion Actions utilisateur
    }
}