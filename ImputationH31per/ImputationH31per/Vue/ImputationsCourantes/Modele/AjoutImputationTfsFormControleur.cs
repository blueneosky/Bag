using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Vue.EditeurImputationTfs.Modele;
using System.Diagnostics;
using ImputationH31per.Util;

namespace ImputationH31per.Vue.ImputationsCourantes.Modele
{
    public class AjoutImputationTfsFormControleur : EditeurImputationTfsFormControleurBase
    {
        private readonly IAjoutImputationTfsFormModele _modele;
        private readonly IImputationsCourantesFormModele _imputationsCourantesFormModele;

        public AjoutImputationTfsFormControleur(IAjoutImputationTfsFormModele modele, IImputationsCourantesFormModele imputationsCourantesFormModele)
            : base(modele)
        {
            this._modele = modele;
            this._imputationsCourantesFormModele = imputationsCourantesFormModele;
        }

        public override void DefinirNumeroEtNumeroComplementaire(int? numero, int? numeroComplementaire)
        {
            ImputationTfsDataEditeur imputationTfs = new ImputationTfsDataEditeur(numero, numeroComplementaire);
            IImputationTfsNotifiable imputationTfsModele = ObtenirImputationExistante(numero, numeroComplementaire);
            if (imputationTfsModele != null)
            {
                // numero existant : recopier info depuis buisness modele
                if (numero == null)
                    imputationTfs = new ImputationTfsDataEditeur(imputationTfsModele);
                imputationTfs.DefinirProprietes(imputationTfsModele);
            }
            else
            {
                IInformationTacheTfs tacheTfs = numero.HasValue ? _modele.ImputationsCourantesFormModele.ObtenirInformationTacheTfs(numero.Value) : null;
                if (tacheTfs != null)
                {
                    imputationTfs.DefinirProprietes(tacheTfs);
                }
                else
                {
                    // numero non existant : nvlle imput mais recopie des info de l'ancienne (conservation)
                    imputationTfs.DefinirProprietes(_modele.ImputationTfs);
                }
            }

            _modele.DefinirImputation(imputationTfs);
        }

        private IImputationTfsNotifiable ObtenirImputationExistante(int? numero, int? numeroComplementaire)
        {
            IImputationTfsNotifiable resultat = null;
            if (numero.HasValue)
            {
                resultat = _modele.ImputationsCourantesFormModele.ObtenirDerniereImputationTfs(numero.Value, numeroComplementaire);
            }
            else if (numeroComplementaire.HasValue)
            {
                Debug.Assert(numero==null);
                IEnumerable<ITicketTfsNotifiable<IImputationTfsNotifiable>> tickets = _imputationsCourantesFormModele.ImputationH31perModele.TicketTfss
                    .Where(t => t.NumeroComplementaire == numeroComplementaire)
                    .Execute();

                if (tickets.Count() == 1)
                {
                    // uniticité du ticket
                    ITicketTfsNotifiable<IImputationTfsNotifiable> ticket = tickets.First();
                    resultat = ticket.ObtenirDernierImputationTfs();
                }
            }
            // else rien de trouvable

            return resultat;
        }
    }
}