using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.Control.ListEditor
{
    internal abstract class ListEditorController 
    {
        private readonly ListEditorModel _model;

        public ListEditorController(ListEditorModel model)
        {
            _model = model;
        }

        public void RemoveSelectedElement()
        {
            if (_model.RemoveEnabled)
            {
                ListEditorElement element = _model.ElementSelected;
                ListEditorElement nextElement = _model.Elements.NextOrPrevious(element);
                SelectElement(nextElement); // UI optim (don't blink)
                bool success = RemoveCore(element);
                if (false == success)
                    SelectElement(element); // revert if cancelled
            }
        }

        public void SelectElement(ListEditorElement element)
        {
            _model.ElementSelected = element;
        }

        internal void NewElement()
        {
            if (_model.AddEnabled)
                NewElementCore();
        }

        protected abstract void NewElementCore();

        protected abstract bool RemoveCore(ListEditorElement element);
    }
}