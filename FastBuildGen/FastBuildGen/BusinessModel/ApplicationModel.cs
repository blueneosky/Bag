using System.ComponentModel;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel
{
    internal class ApplicationModel : INotifyPropertyChanged
    {
        private FBModel _fbModel;

        public ApplicationModel()
        {
            _fbModel = null;
        }

       public FBModel FBModel
        {
            get { return _fbModel; }
            set
            {
                if (_fbModel != null)
                {
                    _fbModel.PropertyChanged -= _fbModel_PropertyChanged;
                }
                _fbModel = value;
                if (_fbModel != null)
                {
                    _fbModel.PropertyChanged += _fbModel_PropertyChanged;
                }
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstApplicationModelFBModel));
            }
        }

       void _fbModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
       {
#warning TODO DELTA point - add implementation
           throw new System.NotImplementedException();
       }
       public bool DataChanged
       {
           get
           {
#warning TODO DELTA point - add DataChanged management here
               return false; // _fastBuildModel.DataChanged;
           }
       }
        
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion INotifyPropertyChanged
    }
}