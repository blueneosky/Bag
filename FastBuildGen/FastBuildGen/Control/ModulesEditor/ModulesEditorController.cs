using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.ModulesEditor
{
    internal class ModulesEditorController : ListEditorController
    {
        private readonly IFastBuildParamController _fastBuildParamController;
        private readonly ModulesEditorModel _model;

        public ModulesEditorController(ModulesEditorModel model)
            : base(model)
        {
            _model = model;
            _fastBuildParamController = new FastBuildParamController(model.FastBuildParamModel);
        }

        internal bool SelectModule(IParamDescriptionHeoModule module)
        {
            ListEditorElement element = null;
            if (module != null)
            {
                element = _model.Elements
                  .Where(e => module.SameAs(e.Value))
                  .FirstOrDefault();
                if (element == null)
                    return false;
            }

            SelectElement(element);

            return true;
        }

        protected override void NewElementCore()
        {
            string baseName = "newModule";
            IParamDescriptionHeoModule newModule = _fastBuildParamController.NewModule(baseName);
            ListEditorElement newElement = _model.Elements
                .Where(e => Object.Equals(e.Value, newModule))
                .FirstOrDefault();
            SelectElement(newElement);
        }

        protected override bool RemoveCore(ListEditorElement element)
        {
            ModuleElement moduleElement = element as ModuleElement;
            if (moduleElement == null)
                return false;

            IParamDescriptionHeoModule module = moduleElement.Module;
            bool success = _fastBuildParamController.DeleteModule(module.Name);

            return success;
        }
    }
}