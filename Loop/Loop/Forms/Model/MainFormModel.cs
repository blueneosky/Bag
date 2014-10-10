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
        private IBoardModel _boardModel;

        public IBoardModel BoardModel
        {
            get { return _boardModel; }
            set
            {
                if (_boardModel != null) _boardModel.PropertyChanged -= _boardModel_PropertyChanged;
                _boardModel = value;
                if (_boardModel != null) _boardModel.PropertyChanged += _boardModel_PropertyChanged;
            }
        }

        private void _boardModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #region INotifyPropertyChanged Membres

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion INotifyPropertyChanged Membres
    }
}