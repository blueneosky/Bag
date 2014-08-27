using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImputationH31per.Util
{
    public interface IIHFormModele
    {
        bool EstAgrandi { get; set; }

        bool EstDefini { get; set; }

        Point Localisation { get; set; }

        string Nom { get; }

        Size Taille { get; set; }
    }
}