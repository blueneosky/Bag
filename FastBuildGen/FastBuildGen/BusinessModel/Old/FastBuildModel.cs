using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel.Old
{
    internal class FastBuildModel : IFastBuildModel
    {
        #region Fields

        private readonly FastBuildInternalVarModel _fastBuildInternalVarModel;
        private readonly FastBuildParamModel _fastBuildParamModel;
        private bool _dataChanged;
        private bool _innerDataChanged;

        private bool _withEchoOff;

        #endregion Fields

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FastBuildModel()
        {
            _fastBuildInternalVarModel = new FastBuildInternalVarModel();
            _fastBuildParamModel = new FastBuildParamModel();
        }

        public void Initialize()
        {
            // Default configuration

            _fastBuildParamModel.Initialize();

            _fastBuildInternalVarModel.Initialize();

            WithEchoOff = true;

            // DataChanged management
            _fastBuildParamModel.HeoModuleParamsChanged += _fastBuildParamModel_HeoModuleParamsChanged;
            _fastBuildParamModel.HeoTargetParamsChanged += _fastBuildParamModel_HeoTargetParamsChanged;
            _fastBuildParamModel.PropertyChanged += _fastBuildParamModel_PropertyChanged;
            _fastBuildInternalVarModel.PropertiesChanged += _fastBuildInternalVarModel_PropertiesChanged;
            this.PropertyChanged += FastBuildModel_PropertyChanged;
        }

        #endregion Ctor

        #region Property

        public bool DataChanged
        {
            get { return _dataChanged; }
            private set
            {
                if (_dataChanged == value)
                    return;
                _dataChanged = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIFastBuildModelEvent.ConstDataChanged));
            }
        }

        public IFastBuildInternalVarModel FastBuildInternalVarModel { get { return _fastBuildInternalVarModel; } }

        public IFastBuildParamModel FastBuildParamModel { get { return _fastBuildParamModel; } }

        public bool WithEchoOff
        {
            get { return _withEchoOff; }
            set
            {
                if (_withEchoOff == value)
                    return;
                _withEchoOff = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIFastBuildModelEvent.ConstWithEchoOff));
            }
        }

        private bool InnerDataChanged
        {
            get { return _innerDataChanged; }
            set
            {
                if (_innerDataChanged == value)
                    return;
                _innerDataChanged = value;
                UpdateDataChanged();
            }
        }

        #endregion Property

        #region Functions

        public void ResetDataChanged()
        {
            InnerDataChanged = false;
            _fastBuildParamModel.ResetDataChanged();
        }

        #endregion Functions

        #region Private

        private void SetInnerDataChanged()
        {
            InnerDataChanged = true;
        }

        private void UpdateDataChanged()
        {
            DataChanged = InnerDataChanged || _fastBuildParamModel.DataChanged;
        }

        #endregion Private

        #region Models events

        private void _fastBuildInternalVarModel_PropertiesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetInnerDataChanged();
        }

        private void _fastBuildParamModel_HeoModuleParamsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetInnerDataChanged();
        }

        private void _fastBuildParamModel_HeoTargetParamsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetInnerDataChanged();
        }

        private void _fastBuildParamModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstIFastBuildParamModelEvent.ConstDataChanged:
                    UpdateDataChanged();
                    break;

                default:
                    // ignored
                    break;
            }
        }

        private void FastBuildModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != ConstIFastBuildModelEvent.ConstDataChanged)
                SetInnerDataChanged();
        }

        #endregion Models events

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion Events
    }
}