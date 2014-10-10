using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Loop.Common.Extension;
using Loop.Controls.Board.Model;
using Loop.Model;

namespace Loop.Forms.Model
{
    internal class MainFormBoardUserControlModel : IBoardUserControlModel
    {
        #region Members

        private readonly MainFormModel _model;

        #endregion Members

        #region ctor

        public MainFormBoardUserControlModel(MainFormModel model)
        {
            _model = model;

            _model.PropertyChanged += _model_PropertyChanged;
        }

        ~MainFormBoardUserControlModel()
        {
            _model.PropertyChanged -= _model_PropertyChanged;
        }

        #endregion ctor

        #region Model events

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstMainFormModel.ConstPropertyBoardModel:
                    OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIBoardUserControlModel.ConstPropertyBoardModel));
                    break;

                default:
                    break;
            }
        }

        #endregion Model events

        #region IBoardUserControlModel Membres

        public IBoardModel BoardModel
        {
            get { return _model.BoardModel; }
        }

        #endregion IBoardUserControlModel Membres

        #region INotifyPropertyChanged Membres

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion INotifyPropertyChanged Membres
    }
}