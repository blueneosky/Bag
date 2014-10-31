using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Loop.Model;

namespace Loop.Controls.Board.Model
{
    public class BoardUserControlController
    {
        #region Members

        private readonly IBoardUserControlModel _model;

        #endregion Members

        #region ctor

        public BoardUserControlController(IBoardUserControlModel model)
        {
            _model = model;
        }

        #endregion ctor

        #region Methodes

        private void SetValue(int line, int column, int? value)
        {
            _model.BoardModel.SetValue(line, column, value);
        }

        public void SetOrInvertDashDot(int line, int column, EnumDash preferedValue)
        {
            bool dotDashLine = (line % 2) == 0;
            bool dotDashColumn = (column % 2) == 0;
            if (dotDashLine == dotDashColumn)
                return; // not a dash/cross index

            EnumDash currentCellValue = (EnumDash)_model.BoardModel[line, column].Value;
            switch (preferedValue)
            {
                case EnumDash.Dash:
                    SetValue(line, column, (int?)(currentCellValue == EnumDash.Dash ? EnumDash.Empty : EnumDash.Dash));
                    break;

                case EnumDash.Cross:
                    SetValue(line, column, (int?)(currentCellValue == EnumDash.Cross ? EnumDash.Empty : EnumDash.Cross));
                    break;

                case EnumDash.Empty:
                default:
                    SetValue(line, column, (int?)EnumDash.Empty);
                    break;
            }
        }

        private EnumDash? _setOrInvertDashValue;

        public void BeginSetOrInvertDashDot(int line, int column, EnumDash preferedValue)
        {
            if (_setOrInvertDashValue != null)
                return;

            bool dotDashLine = (line % 2) == 0;
            bool dotDashColumn = (column % 2) == 0;
            if (dotDashLine == dotDashColumn)
                return; // not a dash/cross index

            EnumDash currentCellValue = (EnumDash)_model.BoardModel[line, column].Value;
            switch (preferedValue)
            {
                case EnumDash.Dash:
                    _setOrInvertDashValue = currentCellValue == EnumDash.Dash ? EnumDash.Empty : EnumDash.Dash;
                    break;

                case EnumDash.Cross:
                    _setOrInvertDashValue = currentCellValue == EnumDash.Cross ? EnumDash.Empty : EnumDash.Cross;
                    break;

                case EnumDash.Empty:
                default:
                    _setOrInvertDashValue = EnumDash.Empty;
                    break;
            }

            SetValue(line, column, (int?)_setOrInvertDashValue);
        }

        public void EndSetOrInvertDashDot()
        {
            _setOrInvertDashValue = null;
        }

        public void UpdateSetOrInvertDashDot(int line, int column)
        {
            if (_setOrInvertDashValue == null)
                return;

            bool dotDashLine = (line % 2) == 0;
            bool dotDashColumn = (column % 2) == 0;
            if (dotDashLine == dotDashColumn)
                return; // not a dash/cross index

            EnumDash currentCellValue = (EnumDash)_model.BoardModel[line, column].Value;
            if (currentCellValue == _setOrInvertDashValue)
                return;

            SetValue(line, column, (int?)_setOrInvertDashValue);
        }

        #endregion Methodes
    }
}