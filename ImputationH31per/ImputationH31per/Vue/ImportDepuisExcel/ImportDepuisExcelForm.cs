using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Controle.ImputationTfsListView.Modele;
using ImputationH31per.Modele;
using ImputationH31per.Util;
using ImputationH31per.Vue.ImportDepuisExcel.Modele;

namespace ImputationH31per.Vue.ImportDepuisExcel
{
    public partial class ImportDepuisExcelForm : IHForm
    {
        #region Membres

        private readonly IImportDepuisExcelFormControleur _controleur;
        private readonly IImputationTfsListViewControlControleur _imputationTfsListViewControlControleur;
        private readonly IImputationTfsListViewControlModele _imputationTfsListViewControlModele;
        private readonly IImportDepuisExcelFormModele _modele;

        #endregion Membres

        #region ctor

        public ImportDepuisExcelForm(IIHFormModele formModele, IIHFormControleur formControleur
            , IImportDepuisExcelFormModele modele, IImportDepuisExcelFormControleur controleur
            , IImputationTfsListViewControlModele imputationTfsListViewControlModele, IImputationTfsListViewControlControleur imputationTfsListViewControlControleur)
            : base(formModele, formControleur)
        {
            InitializeComponent();

            _modele = modele;
            _controleur = controleur;
            _imputationTfsListViewControlModele = imputationTfsListViewControlModele;
            _imputationTfsListViewControlControleur = imputationTfsListViewControlControleur;
        }

        private ImportDepuisExcelForm()
        {
            InitializeComponent();
        }

        public static ImportDepuisExcelForm Nouveau(IIHFormModele formModele, IImportDepuisExcelFormModele modele, IImportDepuisExcelFormControleur controleur)
        {
            IIHFormControleur formControleur = ServicePreferenceModele.Instance.ObtenirIHFormControleur(formModele);
            ImputationTfsListViewControlModele imputationTfsListViewControlModele = new ImputationTfsListViewControlModele(modele);
            ImputationTfsListViewControlControleur imputationTfsListViewControlControleur = new ImputationTfsListViewControlControleur(imputationTfsListViewControlModele, controleur);

            ImportDepuisExcelForm instance = new ImportDepuisExcelForm(
                 formModele, formControleur
                , modele, controleur
                , imputationTfsListViewControlModele, imputationTfsListViewControlControleur
                );

            return instance;
        }

        #endregion ctor

        #region Surcharges

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_modele == null)
                return; // en mode design

            // synchronisation de l'interface avec le modele

            // initialisation de _imputationTfsListViewControl
            _imputationTfsListViewControl.Initialiser(_imputationTfsListViewControlModele, _imputationTfsListViewControlControleur);
        }

        #endregion Surcharges

        #region Evennement modele

        #endregion Evennement modele

        #region Mise à jour interface depuis modele

        #endregion Mise à jour interface depuis modele

        #region Actions utilisateur

        private void _extraireButton_Click(object sender, EventArgs e)
        {
            string texteImport = _texteImportTextBox.Text;
            try
            {
                _controleur.Extraire(texteImport);
            }
            catch (IHException exception)
            {
                MessageBox.Show("Erreur durant le traitement : " + exception.Message);
            }
        }

        private void _validerButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        #endregion Actions utilisateur
    }
}