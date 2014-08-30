using System.ComponentModel;
using FastBuildGen.BusinessModel.Old;
using FastBuildGen.Common;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Forms.Main
{
    internal class MainFormModel : INotifyPropertyChanged
    {
        private readonly ApplicationModel _applicationModel;

        private string _activePanel;

        public MainFormModel(ApplicationModel applicationModel)
            : base()
        {
            _applicationModel = applicationModel;

            _applicationModel.PropertyChanged += _applicationModel_PropertyChanged;
        }

        public bool FastBuildDataChanged
        {
            get { return _applicationModel.DataChanged; }
        }

        public ApplicationModel ApplicationModel
        {
            get { return _applicationModel; }
        }

        private void _applicationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstIFastBuildModelEvent.ConstDataChanged:
                    UpdateFastBuildDataChanged();
                    break;

                default:
                    // ignored
                    break;
            }
        }

        private void UpdateFastBuildDataChanged()
        {
            // notify
            OnPropertyChanged(this, new PropertyChangedEventArgs(ConstMainFormModelEvent.ConstFastBuildDataChanged));
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