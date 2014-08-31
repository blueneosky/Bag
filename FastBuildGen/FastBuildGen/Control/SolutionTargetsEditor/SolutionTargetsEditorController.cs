using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.SolutionTargetsEditor
{
    internal class SolutionTargetsEditorController : ListEditorController
    {
        private readonly ApplicationController _applicationController;
        private readonly SolutionTargetsEditorModel _model;

        public SolutionTargetsEditorController(SolutionTargetsEditorModel model)
            : base(model)
        {
            _model = model;
            _applicationController = new ApplicationController(model.ApplicationModel);
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
            string baseKeyword = "newTarget";
            FBSolutionTarget newSolutionTarget = _applicationController.NewSolutionTarget(baseKeyword);
            ListEditorElement newElement = _model.Elements
                .Where(e => Object.Equals(e.Value, newSolutionTarget))
                .FirstOrDefault();
            SelectElement(newElement);
        }

        protected override bool RemoveCore(ListEditorElement element)
        {
            SolutionTargetElement moduleElement = element as SolutionTargetElement;
            if (moduleElement == null)
                return false;

            FBSolutionTarget solutionTarget = moduleElement.SolutionTarget;
            bool success = _applicationController.DeleteSolutionTarget(solutionTarget.Id);

            return success;
        }
    }
}