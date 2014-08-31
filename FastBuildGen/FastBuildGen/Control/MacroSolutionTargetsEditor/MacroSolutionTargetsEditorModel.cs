using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.MacroSolutionTargetsEditor
{
    internal class MacroSolutionTargetsEditorModel : ListEditorModel
    {
        private readonly ApplicationModel _applicationModel;

        public MacroSolutionTargetsEditorModel(ApplicationModel applicationModel)
        {
            _applicationModel = applicationModel;

#warning TODO Beta point - review event mngmt
            //_applicationModel.HeoTargetParamsChanged += _fastBuildParamModel_HeoTargetParamsChanged;

            UpdateTargets();
        }

        public ApplicationModel ApplicationModel
        {
            get { return _applicationModel; }
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

        private void _fastBuildParamModel_HeoTargetParamsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateTargets();
        }

        private void UpdateTargets()
        {
            FBModel fbModel = _applicationModel.FBModel;
            IEnumerable<FBMacroSolutionTarget> macroSolutionTargets;
            if (fbModel != null)
            {
                macroSolutionTargets = fbModel.MacroSolutionTargets;
            }
            else
            {
                macroSolutionTargets = new FBMacroSolutionTarget[0];
            }

            IEnumerable<MacroSolutionTargetElement> elements = macroSolutionTargets
                .Select(t => new MacroSolutionTargetElement(t));
            Elements = elements;
        }
    }
}