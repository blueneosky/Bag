using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loop.Model
{
    public class CellChangedEventArgs : EventArgs
    {
        public CellChangedEventArgs(int line, int column, ICell cell)
        {
            Line = line;
            Column = column;
            Cell = cell;
        }

        public int Line { get; private set; }

        public int Column { get; private set; }

        public ICell Cell { get; private set; }
    }
}