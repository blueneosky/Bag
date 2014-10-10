using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Loop.Common.Extension;
using Loop.Model;

namespace Loop.Forms.Model
{
    internal class MainFormModel : INotifyPropertyChanged
    {
        #region Members

        private IBoardModel _boardModel;

        #endregion Members

        #region ctor

        public IBoardModel BoardModel
        {
            get { return _boardModel; }
            set
            {
                _boardModel = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstMainFormModel.ConstPropertyBoardModel));
            }
        }

        #endregion ctor

        #region INotifyPropertyChanged Membres

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion INotifyPropertyChanged Membres
    }
}