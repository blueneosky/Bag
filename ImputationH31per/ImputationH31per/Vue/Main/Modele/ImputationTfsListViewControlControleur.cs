using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Controle.ImputationTfsListView.Modele;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.Main.Modele
{
    public class ImputationTfsListViewControlControleur : ImputationTfsListViewControlControleurBase, IImputationTfsListViewControlControleur
    {
        #region Membres

        private readonly IMainFormControleur _mainFormControleur;
        private readonly IImputationTfsListViewControlModele _modele;

        #endregion Membres

        #region ctor

        public ImputationTfsListViewControlControleur(IImputationTfsListViewControlModele modele, IMainFormControleur mainFormControleur)
            : base(modele)
        {
            _modele = modele;
            _mainFormControleur = mainFormControleur;
        }

        #endregion ctor

        #region ImputationTfsListViewControlControleurBase

        public override void ModifierImputationTfs(IImputationTfsNotifiable imputationTfs)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Etes-vous sûr de modifier l'imputation.", "Confirmation"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
                base.ModifierImputationTfs(imputationTfs);
        }

        public override void SupprimerImputationTfs(IImputationTfsNotifiable imputationTfs, bool avecModifieur)
        {
            Debug.Assert(_modele.EstImputationTfsSupprimable);
            _mainFormControleur.SupprimerImputationTfs(imputationTfs, avecModifieur);
        }

        #endregion ImputationTfsListViewControlControleurBase
    }
}