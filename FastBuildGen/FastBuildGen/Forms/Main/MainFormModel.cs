using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;

namespace FastBuildGen.Forms.Main
{
    internal class MainFormModel : INotifyPropertyChanged
    {
        private readonly ApplicationModel _applicationModel;
        private string _filePath;

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

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath == value) return;
                _filePath = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstMainFormModelEvent.ConstApplicationModelFilePath));
            }
        }

        private void _applicationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstFBEvent.ConstApplicationModelDataChanged:
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