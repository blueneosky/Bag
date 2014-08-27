using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImputationH31per.Util
{
    public interface IIHFormControleur
    {
        void MemoriserPreference(bool estAgrandi, Point localisation, Size taille);
    }
}