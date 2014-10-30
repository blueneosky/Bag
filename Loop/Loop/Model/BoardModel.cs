using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Loop.Common.Extension;

namespace Loop.Model
{
    public class BoardModel : IBoardModel
    {
        #region Members

        private int _nbLines;
        private int _nbColumns;
        private ICell[,] _cells;

        #endregion Members

        #region ctor

        public BoardModel(int nbLines, int nbColumns)
        {
            // Test of validity
            string message = null;
            if (((nbLines % 2) == 0) || ((nbColumns % 2) == 0))
            {
                message = "VirtualBoardModel has no proper size !";
            }
            if (message != null)
            {
                Debug.Fail(message);
                throw new Exception(message);
            }

            // Init
            _nbLines = nbLines;
            _nbColumns = nbColumns;
            _cells = new ICell[_nbLines, _nbColumns];
            for (int i = 0; i < _nbLines; i++)
            {
                bool dotDashI = (i % 2) == 0;
                for (int j = 0; j < _nbColumns; j++)
                {
                    bool dotDashJ = (j % 2) == 0;
                    ICell cell;
                    if (dotDashI && dotDashJ)
                    {
                        cell = new DotCell();
                    }
                    else if (dotDashI || dotDashJ)
                    {
                        cell = new DashCell(EnumDash.Empty);
                    }
                    else
                    {
                        cell = new NumberCell(null);
                    }
                    _cells[i, j] = cell;
                }
            }
        }

        #endregion ctor

        #region Properties

        public ICell this[int i, int j]
        {
            get { return _cells[i, j]; }
            private set
            {
                _cells[i, j] = value;
                OnCellChanged(this, new CellChangedEventArgs(i, j, value));
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

        #region Events

        public event EventHandler<CellChangedEventArgs> CellChanged;

        protected virtual void OnCellChanged(object sender, CellChangedEventArgs e)
        {
            CellChanged.Notify(sender, e);
        }

        #endregion Events

        #region Methods

        public void SetValue(int line, int column, int? value)
        {
            bool dotDashLine = (line % 2) == 0;
            bool dotDashColumn = (column % 2) == 0;
            ICell cell;
            if (dotDashLine && dotDashColumn)
            {
                cell = new DotCell();
            }
            else if (dotDashLine || dotDashColumn)
            {
                cell = new DashCell((EnumDash)value);
            }
            else
            {
                cell = new NumberCell(value);
            }
            this[line, column] = cell;
        }

        #endregion Methods
    }
}