using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.SolutionTargetsEditor
{
    internal class SolutionTargetsEditorModel : ListEditorModel
    {
        private readonly ApplicationModel _applicationModel;

        public SolutionTargetsEditorModel(ApplicationModel applicationModel)
        {
            _applicationModel = applicationModel;

#warning TODO DELTA point - review code for event mngmt
            //_applicationModel.HeoModuleParamsChanged += _fastBuildParamModel_HeoModuleParamsChanged;

            UpdateModules();
        }

        public ApplicationModel ApplicationModel
        {
            get { return _applicationModel; }
        }

        public FBSolutionTarget SolutionTargetSelected
        {
            get
            {
                SolutionTargetElement moduleElement = ElementSelected as SolutionTargetElement;
                FBSolutionTarget module = (moduleElement != null) ? moduleElement.SolutionTarget : null;

                return module;
            }
        }

        private void _fastBuildParamModel_HeoModuleParamsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateModules();
        }

        private void UpdateModules()
        {
            FBModel fbModel = _applicationModel.FBModel;
            IEnumerable<FBSolutionTarget> solutionTargets;
            if (fbModel != null)
            {
                solutionTargets = fbModel.SolutionTargets;
            }
            else
            {
                solutionTargets = new FBSolutionTarget[0];
            }

            IEnumerable<SolutionTargetElement> elements = solutionTargets
                .Select(t => new SolutionTargetElement(t));
            Elements = elements;
        }
    }
}