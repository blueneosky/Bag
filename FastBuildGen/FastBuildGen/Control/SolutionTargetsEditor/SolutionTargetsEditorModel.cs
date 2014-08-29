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
        private readonly IFastBuildParamModel _fastBuildParamModel;

        public SolutionTargetsEditorModel(IFastBuildParamModel fastBuildParamModel)
        {
            _fastBuildParamModel = fastBuildParamModel;

            _fastBuildParamModel.HeoModuleParamsChanged += _fastBuildParamModel_HeoModuleParamsChanged;

            UpdateModules();
        }

        public IFastBuildParamModel FastBuildParamModel
        {
            get { return _fastBuildParamModel; }
        }

        public IParamDescriptionHeoModule ModuleSelected
        {
            get
            {
                SolutionTargetElement moduleElement = ElementSelected as SolutionTargetElement;
                IParamDescriptionHeoModule module = (moduleElement != null) ? moduleElement.Module : null;

                return module;
            }
        }

        private void _fastBuildParamModel_HeoModuleParamsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateModules();
        }

        private void UpdateModules()
        {
            IEnumerable<IParamDescriptionHeoModule> modules = _fastBuildParamModel.HeoModuleParams;

            IEnumerable<SolutionTargetElement> elements = modules
                .Select(t => new SolutionTargetElement(t));
            Elements = elements;
        }
    }
}