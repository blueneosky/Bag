using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;

namespace FastBuildGen.Control.ModulesEditor
{
    internal class ModulesEditorModel : ListEditorModel
    {
        private readonly IFastBuildParamModel _fastBuildParamModel;

        public ModulesEditorModel(IFastBuildParamModel fastBuildParamModel)
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
            IEnumerable<IParamDescriptionHeoModule> modules = _fastBuildParamModel.HeoModuleParams;

            IEnumerable<ModuleElement> elements = modules
                .Select(t => new ModuleElement(t));
            Elements = elements;
        }
    }
}