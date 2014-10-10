using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Loop.Common.Extension;

namespace Loop.Model
{
    internal class VirtualBoardModel : IBoardModel
    {
        private readonly IBoardModel _source;

        public VirtualBoardModel(IBoardModel source)
        {
            _source = source;
        }

        public IBoardModel Source
        {
            get { return _source; }
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