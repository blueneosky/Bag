using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ImputationH31per.Util;

namespace ImputationH31per.Modele
{
    public class IHFormControleur : IIHFormControleur
    {
        private readonly IIHFormModele _modele;

        public IHFormControleur(IIHFormModele modele)
        {
            _modele = modele;
        }

        public void MemoriserPreference(bool estAgrandi, Point localisation, Size taille)
        {
            _modele.EstAgrandi = estAgrandi;
            _modele.Localisation = localisation;
            _modele.Taille = taille;

            ServicePreferenceModele.Instance.EnregistrerIHFormModele(_modele);
        }
    }
}