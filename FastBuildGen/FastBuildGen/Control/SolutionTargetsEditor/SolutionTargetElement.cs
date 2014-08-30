using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.SolutionTargetsEditor
{
    internal class SolutionTargetElement : ListEditorElement
    {
        private readonly FBSolutionTarget _solutionTarget;

        public SolutionTargetElement(FBSolutionTarget SolutionTarget)
            : base(SolutionTarget)
        {
            _solutionTarget = SolutionTarget;

            _solutionTarget.PropertyChanged += _solutionTarget_PropertyChanged;

            UpdateText();
        }

        public FBSolutionTarget SolutionTarget
        {
            get { return _solutionTarget; }
        }

        private void _solutionTarget_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstIParamDescriptionEvent.ConstName:
                    UpdateText();
                    break;

                default:
                    break;
            }
        }

        private void UpdateText()
        {
            Text = _solutionTarget.Keyword;
        }
    }
}