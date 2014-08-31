using System;
using System.Windows.Forms;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Control.MacroSolutionTargetEditor
{
    internal class MacroSolutionTargetEditorController
    {
        private readonly MacroSolutionTargetEditorModel _model;

        public MacroSolutionTargetEditorController(MacroSolutionTargetEditorModel model)
        {
            _model = model;
        }

        public void AddDependency(FBSolutionTarget solutionTarget)
        {
            if (solutionTarget == null)
                return;

            try
            {
                _model.MacroSolutionTarget.SolutionTargetIds.Add(solutionTarget.Id);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool RemoveDependency(FBSolutionTarget solutionTarget)
        {
            bool success = false;

            try
            {
                if (solutionTarget != null)
                    success = _model.MacroSolutionTarget.SolutionTargetIds.Remove(solutionTarget.Id);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return success;
        }
    }
}