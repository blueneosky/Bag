using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;
using ImputationH31per.Vue.EditeurImputationTfs;
using ImputationH31per.Vue.EditeurImputationTfs.Modele;

namespace ImputationH31per.Controle.ImputationTfsListView.Modele
{
    public abstract class ImputationTfsListViewControlControleurBase : IImputationTfsListViewControlControleur
    {
        private readonly IImputationTfsListViewControlModele _modele;

        public ImputationTfsListViewControlControleurBase(IImputationTfsListViewControlModele modele)
        {
            _modele = modele;
        }

        public virtual void ModifierImputationTfs(IImputationTfsNotifiable imputationTfs)
        {
            Debug.Assert(_modele.EstImputationTfsModifiable);
            ImputationTfsData imputationTfsDupliquee = ImputationTfsData.Copier(imputationTfs);  // ne pas travailler sur l'original
#warning TODO - créer un choix source modèle pour la construction
            IIHFormModele formModele = ServicePreferenceModele.Instance.ObtenirIHFormModele(ServicePreferenceModele.ConstanteNomFormModifierImputationTfs);
            IEditeurImputationTfsChoixSourceModele editeurImputationTfsChoixSourceModele = null;
            EditionImputationTfsFormModele editionImputationTfsFormModele = new EditionImputationTfsFormModele(imputationTfsDupliquee, editeurImputationTfsChoixSourceModele);
            EditionImputationTfsFormControleur editionImputationTfsFormControleur = new EditionImputationTfsFormControleur(editionImputationTfsFormModele);
            using (EditeurImputationTfsForm form = new EditeurImputationTfsForm(formModele, editionImputationTfsFormModele, editionImputationTfsFormControleur))
            {
                form.Text = "Modification";
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    IImputationTfsNotifiable imputationTfsModifiee = editionImputationTfsFormModele.ImputationTfs;
                    Debug.Assert(imputationTfsModifiee != null);
                    // appliquer les modifications
                    imputationTfs.DefinirProprietes(imputationTfsModifiee);
                }
            }
        }

        public abstract void SupprimerImputationTfs(IImputationTfsNotifiable imputationTfs, bool avecModifieur);
    }
}