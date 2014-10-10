using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Loop.Controls.Board.Model;
using Loop.Model;

namespace Loop.Controls.Board
{
    public partial class BoardUserControl : UserControl
    {
        #region Members

        private IBoardUserControlModel _model;
        private BoardUserControlController _controller;

        #endregion Members

        #region ctor

        public BoardUserControl()
        {
            InitializeComponent();
        }

        public void Initialize(IBoardUserControlModel model, BoardUserControlController controller)
        {
            _model = model;
            _controller = controller;

            Disposed += BoardUserControl_Disposed;

            _model.PropertyChanged += _model_PropertyChanged;

            RefreshModel();
        }

        private void BoardUserControl_Disposed(object sender, EventArgs e)
        {
            _model.PropertyChanged -= _model_PropertyChanged;
        }

        #endregion ctor

        #region Properties

        private IBoardModel _boardModel;

        public IBoardModel BoardModel
        {
            get { return _boardModel; }
            set
            {
                if (_boardModel != null) _boardModel.CellChanged += _boardModel_CellChanged;
                _boardModel = value;
                if (_boardModel != null) _boardModel.CellChanged += _boardModel_CellChanged;
            }
        }

        #endregion Properties

        #region Model events

        private void _boardModel_CellChanged(object sender, CellChangedEventArgs e)
        {
#warning TODO
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstIBoardUserControlModel.ConstPropertyBoardModel:
                    RefreshBoardModel();
                    break;

                default:
                    break;
            }
        }

        #endregion Model events

        #region Refreshes

        private void RefreshModel()
        {
            RefreshBoardModel();
        }

        private void RefreshBoardModel()
        {
            BoardModel = _model.BoardModel;
        }

        #endregion Refreshes
    }
}