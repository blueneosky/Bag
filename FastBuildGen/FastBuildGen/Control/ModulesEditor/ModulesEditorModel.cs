using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.ModulesEditor
{
    internal class ModulesEditorModel : ListEditorModel
    {
        private readonly FBModel _fbModel;

        public ModulesEditorModel(FBModel fbModel)
        {
            _fbModel = fbModel;

            _fbModel.HeoModuleParamsChanged += _fastBuildParamModel_HeoModuleParamsChanged;

            UpdateModules();
        }

        public FBModel FBModel
        {
            get { return _fbModel; }
        }

        public IParamDescriptionHeoModule ModuleSelected
        {
            get
            {
                ModuleElement moduleElement = ElementSelected as ModuleElement;
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
            IEnumerable<FBSolutionTarget> solutionTargets = _fbModel.SolutionTargets.Values;

            IEnumerable<ModuleElement> elements = solutionTargets
                .Select(t => new ModuleElement(t));
            Elements = elements;
        }
    }
}