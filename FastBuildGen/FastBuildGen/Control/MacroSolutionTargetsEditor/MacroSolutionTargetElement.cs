using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.MacroSolutionTargetsEditor
{
    internal class MacroSolutionTargetElement : ListEditorElement
    {
        private readonly FBMacroSolutionTarget _macroSolutionTarget;

        public MacroSolutionTargetElement(FBMacroSolutionTarget macroSolutionTarget)
            : base(macroSolutionTarget)
        {
            _macroSolutionTarget = macroSolutionTarget;

            _macroSolutionTarget.PropertyChanged += _target_PropertyChanged;

            UpdateText();
        }

        public FBMacroSolutionTarget MacroSolutionTarget
        {
            get { return _macroSolutionTarget; }
        }

        private void _target_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstIParamDescriptionHeoTargetEvent.ConstName:
                    UpdateText();
                    break;

                default:
                    break;
            }
        }

        private void UpdateText()
        {
            Text = _macroSolutionTarget.Keyword;
        }
    }
}