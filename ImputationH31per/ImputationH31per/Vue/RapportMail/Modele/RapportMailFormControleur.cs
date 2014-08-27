using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ImputationH31per.Vue.RapportMail.Modele
{
    public class RapportMailFormControleur : IRapportMailFormControleur
    {
        #region Membres

        private readonly IRapportMailFormModele _modele;

        #endregion Membres

        #region ctor

        public RapportMailFormControleur(IRapportMailFormModele modele)
        {
            _modele = modele;
        }

        #endregion ctor

        #region IRapportMailFormControleur

        public void CopierPressePapier()
        {
            Clipboard.SetText(_modele.TexteRapport);
        }

        public void DefinirTempsDebut(DateTimeOffset tempsDebut)
        {
            DateTimeOffset tempsFin = _modele.TempsFin;
            if (tempsFin < tempsDebut)
                tempsFin = tempsDebut;
            _modele.DefinirTempsDebutEtFin(tempsDebut, tempsFin);
        }

        public void DefinirTempsFin(DateTimeOffset tempsFin)
        {
            DateTimeOffset tempsDebut = _modele.TempsDebut;
            if (tempsDebut > tempsFin)
                tempsDebut = tempsFin;
            _modele.DefinirTempsDebutEtFin(tempsDebut, tempsFin);
        }

        #endregion IRapportMailFormControleur
    }
}