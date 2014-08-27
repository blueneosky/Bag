using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele;
using ImputationH31per.Util;
using ImputationH31per.Vue.RapportMensuel.Modele;
using ImputationH31per.Modele.Entite;

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
        }

        /// <summary>
        /// Pour le designer
        /// </summary>
        private RapportMensuelForm()
        {
            InitializeComponent();
        }

        #endregion ctor

    }
}