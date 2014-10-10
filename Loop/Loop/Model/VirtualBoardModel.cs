using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Loop.Model
{
    public class VirtualBoardModel : IBoardModel
    {
        #region Members

        private readonly IBoardModel _source;
        private readonly int _lineOffset;
        private readonly int _columnOffset;
        private readonly int _nbLines;
        private readonly int _nbColumns;

        #endregion Members

        #region ctor

        public VirtualBoardModel(IBoardModel source, int lineOffset, int columnOffset, int nbLines, int nbColumns)
        {
            // Test of validity
            string message = null;
            if (source == null)
            {
                message = "VirtualBoardModel has no proper size !";
            }
            if (((nbLines % 2) == 1) || ((nbColumns % 2) == 1))
            {
                message = "VirtualBoardModel has no proper size !";
            }
            if ((lineOffset < 0) || (lineOffset + nbLines > source.NbLines)
                || (columnOffset < 0 || (columnOffset + nbColumns > source.NbColumns)))
            {
                message = "VirtualBoardModel out of limit !";
            }
            if (message != null)
            {
                Debug.Fail(message);
                throw new Exception(message);
            }

            // init
            _source = source;
            _lineOffset = lineOffset;
            _columnOffset = columnOffset;
            _nbLines = nbLines;
            _nbColumns = nbColumns; ;
        }

        #endregion ctor

        #region Properties

        public IBoardModel Source
        {
            get { return _source; }
        }

        public ICell this[int i, int j]
        {
            get
            {
                int sI = VirtualToSourceLine(i);
                int sJ = VirtualToSourceColumn(j);
                return _source[sI, sJ];
            }
        }

        public int NbLines
        {
            get { return _nbLines; }
        }

        public int NbColumns
        {
            get { return _nbColumns; ; }
        }

        #endregion Properties

        private int VirtualToSourceLine(int virtualLine)
        {
            return virtualLine + _lineOffset;
        }

        private int VirtualToSourceColumn(int virtualColumn)
        {
            return virtualColumn + _columnOffset;
        }

        #region Events

        public event EventHandler<CellChangedEventArgs> CellChanged
        {
            add { _source.CellChanged += value; }
            remove { _source.CellChanged -= value; }
        }

        #endregion Events
    }
}