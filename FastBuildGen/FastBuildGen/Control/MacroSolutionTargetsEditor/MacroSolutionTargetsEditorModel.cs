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
        private readonly IFastBuildParamModel _fastBuildParamModel;

        public MacroSolutionTargetsEditorModel(IFastBuildParamModel fastBuildParamModel)
        {
            _fastBuildParamModel = fastBuildParamModel;

            _fastBuildParamModel.HeoTargetParamsChanged += _fastBuildParamModel_HeoTargetParamsChanged;

            UpdateTargets();
        }

        public IFastBuildParamModel FastBuildParamModel
        {
            get { return _fastBuildParamModel; }
        }

        public IParamDescriptionHeoTarget TargetSelected
        {
            get
            {
                MacroSolutionTargetElement targetElement = ElementSelected as MacroSolutionTargetElement;
                IParamDescriptionHeoTarget target = (targetElement != null) ? targetElement.Target : null;

                return target;
            }
        }

        private void _fastBuildParamModel_HeoTargetParamsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateTargets();
        }

        private void UpdateTargets()
        {
            IEnumerable<IParamDescriptionHeoTarget> targets = _fastBuildParamModel.HeoTargetParams;

            IEnumerable<MacroSolutionTargetElement> elements = targets
                .Select(t => new MacroSolutionTargetElement(t));
            Elements = elements;
        }
    }
}