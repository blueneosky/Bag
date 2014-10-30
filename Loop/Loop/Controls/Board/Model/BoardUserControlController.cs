using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void SetValue(int line, int column, int? value)
        {
            _model.BoardModel.SetValue(line, column, value);
        }

        #endregion Methodes
    }
}