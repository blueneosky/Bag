using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Vue.EditeurImputationTfs.Modele;

namespace ImputationH31per.Vue.ImputationsCourantes.Modele
{
    public class AjoutImputationTfsFormModele : EditeurImputationTfsFormModeleBase, IAjoutImputationTfsFormModele
    {
        private readonly IImputationsCourantesFormModele _modele;

        public AjoutImputationTfsFormModele(IImputationsCourantesFormModele modele)
            : base()
        {
            _modele = modele;

            EstNumeroImputationTfsModifiable = true;
        }

        public IImputationsCourantesFormModele ImputationsCourantesFormModele
        {
            get { return _modele; }
        }

        public void DefinirImputation(ImputationTfsDataEditeur imputationTfs)
        {
            ImputationTfs = imputationTfs;
        }

        public override IEnumerable<string> ObtenirChoixNomGroupements()
        {
            return _modele.ObtenirChoixNomGroupements();
        }

        public override IEnumerable<int?> ObtenirChoixNumeroComplementaires()
        {
            return _modele.ObtenirChoixNumeroComplementaires();
        }
    }
}