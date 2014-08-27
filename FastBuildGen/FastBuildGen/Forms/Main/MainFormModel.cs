using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Forms.Main
{
    internal class MainFormModel : INotifyPropertyChanged
    {
        public const string ConstActivePanelInternalVarsEditor = "InternalVarsEditor";
        public const string ConstActivePanelModulesEditor = "ModulesEditor";
        public const string ConstActivePanelTargetsEditor = "TargetsEditor";

        private readonly FBModel _fbModel;

        private string _activePanel;

        public MainFormModel(FBModel fbModel)
            : base()
        {
            _fbModel = fbModel;

            _fbModel.PropertyChanged += _fastBuildModel_PropertyChanged;

            ActivePanel = ConstActivePanelModulesEditor;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ActivePanel
        {
            get { return _activePanel; }
            set
            {
                if (_activePanel == value)
                    return;
                _activePanel = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstMainFormModelEvent.ConstActivePanel));
            }
        }

        public bool FastBuildDataChanged
        {
#warning TODO DELTA point - revoir l'implémentation de cette fonctionnalité
            //get { return _fbModel.DataChanged; }
            get { return true; }
        }

        public FBModel FBModel
        {
            get { return _fbModel; }
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
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
    }
}