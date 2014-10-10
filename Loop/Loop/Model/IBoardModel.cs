using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loop.Model
{
    public interface IBoardModel
    {
        ICell this[int i, int j] { get; }

        int NbLines { get; }

        int NbColumns { get; }

        event EventHandler<CellChangedEventArgs> CellChanged;
    }
}