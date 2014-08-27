using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Vue.EditeurImputationTfs.Modele;

namespace ImputationH31per.Controle.ImputationTfsListView.Modele
{
    public class EditionImputationTfsFormModele : EditeurImputationTfsFormModeleBase, IEditeurImputationTfsFormModele
    {
        private readonly IEditeurImputationTfsChoixSourceModele _choixSourceModele;

        public EditionImputationTfsFormModele(IImputationTfsNotifiable imputationTfs, IEditeurImputationTfsChoixSourceModele choixSourceModele)
        {
            _choixSourceModele = choixSourceModele;
            ImputationTfs = ImputationTfsDataEditeur.Copier(imputationTfs);

            this.EstNumeroImputationTfsModifiable = false;
        }

        public override IEnumerable<string> ObtenirChoixNomGroupements()
        {
            IEnumerable<string> resultat = (_choixSourceModele != null) ? _choixSourceModele.ObtenirChoixNomGroupements() : new String[0];
            return resultat;
        }

        public override IEnumerable<int?> ObtenirChoixNumeroComplementaires()
        {
            IEnumerable<int?> resultat = (_choixSourceModele != null) ? _choixSourceModele.ObtenirChoixNumeroComplementaires() : new int?[0];
            return resultat;
        }
    }
}