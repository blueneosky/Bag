using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;

namespace FastBuildGen.Control.MacroSolutionTargetsEditor
{
    internal class MacroSolutionTargetsEditorController : ListEditorController
    {
        private readonly ApplicationController _applicationController;
        private readonly MacroSolutionTargetsEditorModel _model;

        public MacroSolutionTargetsEditorController(MacroSolutionTargetsEditorModel model)
            : base(model)
        {
            _model = model;
            _applicationController = new ApplicationController(model.ApplicationModel);
        }

        internal bool SelectTarget(FBMacroSolutionTarget macroSOlutionTarget)
        {
            ListEditorElement element = null;
            if (macroSOlutionTarget != null)
            {
                element = _model.Elements
                    .Where(e => macroSOlutionTarget.SameAs(e.Value))
                    .FirstOrDefault();
                if (element == null)
                    return false;
            }

            SelectElement(element);

            return true;
        }

        protected override void NewElementCore()
        {
            string newKeyword = "newTarget";
            FBMacroSolutionTarget newMacroSolutionTarget = _applicationController.NewMacroSolutionTarget(newKeyword);
            ListEditorElement newElement = _model.Elements
                .Where(e => Object.Equals(e.Value, newMacroSolutionTarget))
                .FirstOrDefault();
            SelectElement(newElement);
        }

        protected override bool RemoveCore(ListEditorElement element)
        {
            MacroSolutionTargetElement targetElement = element as MacroSolutionTargetElement;
            if (targetElement == null)
                return false;

            FBMacroSolutionTarget macroSolutionTarget = targetElement.MacroSolutionTarget;
            bool success = _applicationController.DeleteMacroSolutionTarget(macroSolutionTarget.Id);

            return success;
        }
    }
}