using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Util;
using ImputationH31per.Vue.EditeurImputationTfs.Modele;

namespace ImputationH31per.Controle.ImputationTfsListView.Modele
{
    public class EditionImputationTfsFormControleur : EditeurImputationTfsFormControleurBase
    {
        private readonly IEditeurImputationTfsFormModele _modele;

        public EditionImputationTfsFormControleur(EditionImputationTfsFormModele modele)
            : base(modele)
        {
            this._modele = modele;
        }

        public override void DefinirNumeroEtNumeroComplementaire(int? numero, int? numeroComplementaire)
        {
            throw new IHException("N'est pas utilisé ici");
        }
    }
}