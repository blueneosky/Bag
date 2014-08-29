using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.MacroSolutionTargetsEditor
{
    internal class MacroSolutionTargetsEditorController : ListEditorController
    {
        private readonly IFastBuildParamController _fastBuildParamController;
        private readonly MacroSolutionTargetsEditorModel _model;

        public MacroSolutionTargetsEditorController(MacroSolutionTargetsEditorModel model)
            : base(model)
        {
            _model = model;
            _fastBuildParamController = new FastBuildParamController(model.FastBuildParamModel);
        }

        internal bool SelectTarget(IParamDescriptionHeoTarget target)
        {
            ListEditorElement element = null;
            if (target != null)
            {
                element = _model.Elements
                    .Where(e => target.SameAs(e.Value))
                    .FirstOrDefault();
                if (element == null)
                    return false;
            }

            SelectElement(element);

            return true;
        }

        protected override void NewElementCore()
        {
            string baseName = "newTarget";
            IParamDescriptionHeoTarget newTarget = _fastBuildParamController.NewTarget(baseName);
            ListEditorElement newElement = _model.Elements
                .Where(e => Object.Equals(e.Value, newTarget))
                .FirstOrDefault();
            SelectElement(newElement);
        }

        protected override bool RemoveCore(ListEditorElement element)
        {
            MacroSolutionTargetElement targetElement = element as MacroSolutionTargetElement;
            if (targetElement == null)
                return false;

            IParamDescriptionHeoTarget target = targetElement.Target;
            bool success = _fastBuildParamController.DeleteTarget(target.Name);

            return success;
        }
    }
}