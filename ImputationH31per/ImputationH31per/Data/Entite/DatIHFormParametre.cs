using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImputationH31per.Data.Entite
{
    public class DatIHFormParametre : IDatIHFormParametre
    {
        private readonly string _nom;

        public DatIHFormParametre(string nom)
        {
            _nom = nom;
        }

        public bool EstAgrandi
        {
            get;
            set;
        }

        public Point Localisation
        {
            get;
            set;
        }

        public string Nom
        {
            get { return _nom; }
        }

        public Size Taille
        {
            get;
            set;
        }
    }
}