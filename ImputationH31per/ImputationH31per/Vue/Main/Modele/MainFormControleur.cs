using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;
using ImputationH31per.Vue.ImportDepuisExcel;
using ImputationH31per.Vue.ImportDepuisExcel.Modele;
using ImputationH31per.Vue.ImputationsCourantes;
using ImputationH31per.Vue.ImputationsCourantes.Modele;
using ImputationH31per.Vue.RapportMail;
using ImputationH31per.Vue.RapportMail.Modele;
using ImputationH31per.Vue.RapportMensuel;
using ImputationH31per.Vue.RapportMensuel.Modele;

namespace ImputationH31per.Vue.Main.Modele
{
    public class MainFormControleur : IMainFormControleur
    {
        #region Membres

        private readonly IImputationH31perControleur _imputationH31perControleur;
        private readonly IMainFormModele _modele;

        #endregion Membres

        #region ctor

        public MainFormControleur(IMainFormModele modele)
            : this(modele, new ImputationH31perControleur(modele.ImputationH31perModele))
        {
        }

        public MainFormControleur(
            IMainFormModele modele
            , IImputationH31perControleur ImputationH31perControleur)
        {
            _modele = modele;
            _imputationH31perControleur = ImputationH31perControleur;
        }

        #endregion ctor

        #region IMainFormControleur

        #region Méthodes

        public void AfficherImportExcel()
        {
            IIHFormModele formModele = ServicePreferenceModele.Instance.ObtenirIHFormModele(ServicePreferenceModele.ConstanteNomFormImportExcel);
            IImportDepuisExcelFormModele importDepuisExcelFormModele = new ImportDepuisExcelFormModele(_modele.ImputationH31perModele);
            IImportDepuisExcelFormControleur importDepuisExcelFormControleur = new ImportDepuisExcelFormControleur(importDepuisExcelFormModele);

            using (ImportDepuisExcelForm form = ImportDepuisExcelForm.Nouveau(formModele, importDepuisExcelFormModele, importDepuisExcelFormControleur))
            {
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    importDepuisExcelFormControleur.Importer();
                }
            }
        }

        public void AfficherRapportMail()
        {
            IIHFormModele formModele = ServicePreferenceModele.Instance.ObtenirIHFormModele(ServicePreferenceModele.ConstanteNomFormRapportMail);
            IRapportMailFormModele rapportMailFormModele = new RapportMailFormModele(_modele.ImputationH31perModele);
            IRapportMailFormControleur rapportMailFormControleur = new RapportMailFormControleur(rapportMailFormModele);

            using (RapportMailForm form = new RapportMailForm(formModele, rapportMailFormModele, rapportMailFormControleur))
            {
                form.ShowDialog();
            }
        }

        public void AfficherRapportMensuel()
        {
            IIHFormModele formModele = ServicePreferenceModele.Instance.ObtenirIHFormModele(ServicePreferenceModele.ConstanteNomFormRapportMensuel);
            IRapportMensuelFormModele rapportMensuelFormModele = new RapportMensuelFormModele(_modele.ImputationH31perModele);
            IRapportMensuelFormControleur rapportMensuelFormControleur = new RapportMensuelFormControleur(rapportMensuelFormModele);

            using (RapportMensuelForm form = new RapportMensuelForm(formModele, rapportMensuelFormModele, rapportMensuelFormControleur))
            {
                form.ShowDialog();
            }
        }

        public void EnregistrerData()
        {
            try
            {
                _imputationH31perControleur.EnregistrerDataActive();
            }
            catch (IHException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        public void NouvelleImputation()
        {
            IImputationsCourantesFormModele imputationsCourantesFormModele = new ImputationsCourantesFormModele(_modele.ImputationH31perModele);
            IImputationsCourantesFormControleur imputationsCourantesFormControleur = new ImputationsCourantesFormControleur(imputationsCourantesFormModele);

            bool succes = imputationsCourantesFormControleur.AjouterImputationTfs();
            if (succes)
            {
                imputationsCourantesFormControleur.Enregistrer();
            }
        }

        public void NouvelleImputationsCourantes()
        {
            IIHFormModele formModele = ServicePreferenceModele.Instance.ObtenirIHFormModele(ServicePreferenceModele.ConstanteNomFormImputationsCourantes);
            IImputationsCourantesFormModele imputationsCourantesFormModele = new ImputationsCourantesFormModele(_modele.ImputationH31perModele);
            IImputationsCourantesFormControleur imputationsCourantesFormControleur = new ImputationsCourantesFormControleur(imputationsCourantesFormModele);
            using (ImputationsCourantesForm form = ImputationsCourantesForm.Nouveau(formModele, imputationsCourantesFormModele, imputationsCourantesFormControleur))
            {
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    imputationsCourantesFormControleur.Enregistrer();
                }
            }
        }

        public void SupprimerImputationTfs(IImputationTfs imputationTfs, bool avecModifieur)
        {
            if (avecModifieur)
            {
                string messag = "Supprimer toutes les imputations lié à" + Environment.NewLine
                    + imputationTfs.NumeroComplet() + " : " + imputationTfs.NomComplet();
                DialogResult dialogresult = MessageBox.Show(messag, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogresult == DialogResult.Yes)
                {
                    _modele.ImputationH31perModele.SupprimerImputationTfs(imputationTfs);
                }
            }
        }

        public bool VerifierEtEnregistrerData()
        {
            return _imputationH31perControleur.VerifierEtEnregistrerDonneAvantPerte();
        }

        #endregion Méthodes

        #endregion IMainFormControleur
    }
}