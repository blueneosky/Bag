using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Loop.Controls.Board.Model;
using Loop.Forms.Model;

namespace Loop.Forms
{
    internal partial class MainForm : Form
    {
        #region Members

        private readonly MainFormModel _model;
        private readonly MainFormController _controller;

        #endregion Members

        #region ctor

        private MainForm()
        {
            InitializeComponent();
        }

        public MainForm(MainFormModel model, MainFormController controller)
        {
            InitializeComponent();

            _model = model;
            _controller = controller;
            Disposed += MainForm_Disposed;
        }

        private void MainForm_Disposed(object sender, EventArgs e)
        {
            _model.PropertyChanged -= _model_PropertyChanged;
        }

        #endregion ctor

        #region Override

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_model == null)
                return;

            _model.PropertyChanged += _model_PropertyChanged;

            IBoardUserControlModel boardUserControlModel = new MainFormBoardUserControlModel(_model);
            BoardUserControlController boardUserControlController = new BoardUserControlController(boardUserControlModel);

            _boardUserControl.Initialize(boardUserControlModel, boardUserControlController);

            RefreshModel();
        }

        #endregion Override

        #region Model events

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                default:
                    break;
            }
        }

        #endregion Model events

        #region Refresh

        private void RefreshModel()
        {
        }

        #endregion Refresh
    }
}