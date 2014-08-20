using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.Common.UI;
using FastBuildGen.Common.UndoRedo;

namespace FastBuildGen.Forms.Main
{
    internal class MainFormModel : UIModelBase, INotifyPropertyChanged
    {
        public const string ConstActivePanelInternalVarsEditor = "InternalVarsEditor";
        public const string ConstActivePanelModulesEditor = "ModulesEditor";
        public const string ConstActivePanelTargetsEditor = "TargetsEditor";

        private readonly IFastBuildModel _fastBuildModel;

        private string _activePanel;

        public MainFormModel(IFastBuildModel fastBuildModel, IUndoRedoManager undoRedoManager)
            : base(undoRedoManager)
        {
            _fastBuildModel = fastBuildModel;

            _fastBuildModel.PropertyChanged += _fastBuildModel_PropertyChanged;

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
            get { return _fastBuildModel.DataChanged; }
        }

        public IFastBuildModel FastBuildModel
        {
            get { return _fastBuildModel; }
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