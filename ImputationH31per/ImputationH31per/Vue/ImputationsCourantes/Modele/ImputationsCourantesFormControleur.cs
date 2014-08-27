using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;
using ImputationH31per.Vue.EditeurImputationTfs;

namespace ImputationH31per.Vue.ImputationsCourantes.Modele
{
    public class ImputationsCourantesFormControleur : IImputationsCourantesFormControleur
    {
        private readonly IImputationH31perControleur _ImputationH31perControleur;
        private readonly IImputationsCourantesFormModele _modele;

        public ImputationsCourantesFormControleur(IImputationsCourantesFormModele modele)
            : this(modele, new ImputationH31perControleur(modele.ImputationH31perModele))
        {
        }

        public ImputationsCourantesFormControleur(
            IImputationsCourantesFormModele modele
            , IImputationH31perControleur ImputationH31perControleur)
        {
            _modele = modele;
            _ImputationH31perControleur = ImputationH31perControleur;
        }

        public bool AjouterImputationTfs()
        {
            bool succes = false;

            IIHFormModele formModele = ServicePreferenceModele.Instance.ObtenirIHFormModele(ServicePreferenceModele.ConstanteNomFormAjouterImputationTfs);
            AjoutImputationTfsFormModele ajoutImputationTfsFormModele = new AjoutImputationTfsFormModele(_modele);
            AjoutImputationTfsFormControleur ajoutImputationTfsFormControleur = new AjoutImputationTfsFormControleur(ajoutImputationTfsFormModele, _modele);
            using (EditeurImputationTfsForm form = new EditeurImputationTfsForm(formModele, ajoutImputationTfsFormModele, ajoutImputationTfsFormControleur))
            {
                form.Text = "Ajout";
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    // récupération de l'imputation tfs définit par l'utilisateur
                    IImputationTfsNotifiable imputationTfsEntree = ajoutImputationTfsFormModele.ImputationTfs;

                    // création de l'entrée dans le buisness modele
                    //imputationTfs = _modele.ImputationH31perModele.CreerImputationTfs(imputationTfsEntree.Numero);
                    ImputationTfsData imputationTfs = new ImputationTfsData(imputationTfsEntree.Numero, imputationTfsEntree.NumeroComplementaire, DateTimeOffset.UtcNow);
                    // recopie des valeurs
                    imputationTfs.DefinirProprietes(imputationTfsEntree);
                    // mise à jour du modele (ajout)
                    _modele.AjouterImputationTfs(imputationTfs);

                    succes = true;
                }
            }

            return succes;
        }

        public void Enregistrer()
        {
            IEnumerable<IImputationTfsNotifiable> imputationTfsAEnregistrers = _modele.ImputationTfssCourantes
                .OrderBy(i => i.DateHorodatage, ImputationTfs.ComparateurDateHorodatageCroissant);

            _ImputationH31perControleur.AjouterImputationTfs(imputationTfsAEnregistrers);
        }

        public void SupprimerImputationTfs(IImputationTfsNotifiable imputationTfs)
        {
            _modele.SupprimerImputationTfs(imputationTfs);
        }
    }
}