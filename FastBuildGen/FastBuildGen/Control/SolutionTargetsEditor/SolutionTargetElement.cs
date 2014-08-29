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
        private readonly IParamDescriptionHeoModule _module;

        public SolutionTargetElement(IParamDescriptionHeoModule module)
            : base(module)
        {
            _module = module;

            _module.PropertyChanged += _module_PropertyChanged;

            UpdateText();
        }

        public IParamDescriptionHeoModule Module
        {
            get { return _module; }
        }

        private void _module_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            Text = _module.Name;
        }
    }
}