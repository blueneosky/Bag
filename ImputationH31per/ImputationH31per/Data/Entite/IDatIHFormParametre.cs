using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImputationH31per.Data.Entite
{
    public interface IDatIHFormParametre
    {
        bool EstAgrandi { get; }

        Point Localisation { get; }

        string Nom { get; }

        Size Taille { get; }
    }
}