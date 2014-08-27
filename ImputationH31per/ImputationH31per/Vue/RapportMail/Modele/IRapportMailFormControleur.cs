using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Vue.RapportMail.Modele
{
    public interface IRapportMailFormControleur
    {
        void CopierPressePapier();

        void DefinirTempsDebut(DateTimeOffset tempsDebut);

        void DefinirTempsFin(DateTimeOffset tempsFin);
    }
}