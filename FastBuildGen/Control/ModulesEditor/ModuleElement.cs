using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;

namespace FastBuildGen.Control.ModulesEditor
{
    internal class ModuleElement : ListEditorElement
    {
        private readonly IParamDescriptionHeoModule _module;

        public ModuleElement(IParamDescriptionHeoModule module)
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