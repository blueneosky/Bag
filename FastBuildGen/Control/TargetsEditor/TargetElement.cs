using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;

namespace FastBuildGen.Control.TargetsEditor
{
    internal class TargetElement : ListEditorElement
    {
        private readonly IParamDescriptionHeoTarget _target;

        public TargetElement(IParamDescriptionHeoTarget target)
            : base(target)
        {
            _target = target;

            _target.PropertyChanged += _target_PropertyChanged;

            UpdateText();
        }

        public IParamDescriptionHeoTarget Target
        {
            get { return _target; }
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
            Text = _target.Name;
        }
    }
}