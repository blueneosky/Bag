using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ImputationH31per.Util;

namespace ImputationH31per.Modele
{
    public class IHFormModele : IIHFormModele
    {
        private readonly string _nomIHForm;

        internal IHFormModele(string nomIHForm)
        {
            _nomIHForm = nomIHForm;
        }

        public bool EstAgrandi { get; set; }

        public bool EstDefini { get; set; }

        public Point Localisation { get; set; }

        public string Nom
        {
            get { return _nomIHForm; }
        }

        public Size Taille { get; set; }
    }
}