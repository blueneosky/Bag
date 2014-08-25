using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.BusinessModel;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.TargetEditor
{
    internal class TargetEditorController
    {
        private readonly TargetEditorModel _model;

        public TargetEditorController(TargetEditorModel model)
        {
            _model = model;
        }

        public void AddDependency(IParamDescriptionHeoModule dependency)
        {
            if (dependency == null)
                return;

            try
            {
                _model.Target.AddDependency(dependency);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool RemoveDependency(IParamDescriptionHeoModule dependency)
        {
            bool success = false;

            try
            {
                if (dependency != null)
                    success = _model.Target.RemoveDependency(dependency.Name);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return success;
        }
    }
}