using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;

namespace FastBuildGen.Control.MacroSolutionTargetsEditor
{
    internal class MacroSolutionTargetsEditorModel : ListEditorModel
    {
        private readonly ApplicationModel _applicationModel;

        public MacroSolutionTargetsEditorModel(ApplicationModel applicationModel)
        {
            _applicationModel = applicationModel;

            _applicationModel.PropertyChanged += _applicationModel_PropertyChanged;

            UpdateMacroSolutionTargets();
        }

        public ApplicationModel ApplicationModel
        {
            get { return _applicationModel; }
        }

        private FBModel _fbModel;

        public FBModel FBModel
        {
            get { return _fbModel; }
            set
            {
                if (_fbModel != null)
                {
                    _fbModel.MacroSolutionTargets.CollectionChanged -= _fbModel_MacroSolutionTargets_CollectionChanged;
                }
                _fbModel = value;
                if (_fbModel != null)
                {
                    _fbModel.MacroSolutionTargets.CollectionChanged += _fbModel_MacroSolutionTargets_CollectionChanged;
                }
            }
        }

        public FBMacroSolutionTarget MacroSolutionTargetSelected
        {
            get
            {
                MacroSolutionTargetElement targetElement = ElementSelected as MacroSolutionTargetElement;
                FBMacroSolutionTarget target = (targetElement != null) ? targetElement.MacroSolutionTarget : null;

                return target;
            }
        }

        private void _applicationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstFBEvent.ConstApplicationModelFBModel:
                    UpdateFBModel();
                    break;

                default:
                    // ignored
                    break;
            }
        }

        private void _fbModel_MacroSolutionTargets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateMacroSolutionTargets();
        }

        private void UpdateFBModel()
        {
            FBModel = _applicationModel.FBModel;
            UpdateMacroSolutionTargets();
        }

        private void UpdateMacroSolutionTargets()
        {
            FBModel fbModel = FBModel;
            IEnumerable<MacroSolutionTargetElement> elements;
            if (fbModel != null)
            {
                elements = fbModel.MacroSolutionTargets
                    .Select(t => new MacroSolutionTargetElement(t));
            }
            else
            {
                elements = null;
            }

            if (Elements != null)
            {
                foreach (MacroSolutionTargetElement mste in Elements.Cast<MacroSolutionTargetElement>())
                {
                    mste.Dispose();
                }
            }

            Elements = elements;
        }
    }
}