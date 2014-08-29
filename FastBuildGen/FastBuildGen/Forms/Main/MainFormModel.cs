using System.ComponentModel;
using FastBuildGen.BusinessModel.Old;
using FastBuildGen.Common;

namespace FastBuildGen.Forms.Main
{
    internal class MainFormModel : INotifyPropertyChanged
    {
        private readonly IFastBuildModel _fastBuildModel;

        private string _activePanel;

        public MainFormModel(IFastBuildModel fastBuildModel)
            : base()
        {
            _fastBuildModel = fastBuildModel;

            _fastBuildModel.PropertyChanged += _fastBuildModel_PropertyChanged;
        }

        public bool FastBuildDataChanged
        {
            get { return _fastBuildModel.DataChanged; }
        }

        public IFastBuildModel FastBuildModel
        {
            get { return _fastBuildModel; }
        }

        private void _fastBuildModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
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