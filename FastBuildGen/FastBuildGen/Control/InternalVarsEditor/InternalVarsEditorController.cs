using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FastBuildGen.Control.ListEditor;

namespace FastBuildGen.Control.InternalVarsEditor
{
    internal class InternalVarsEditorController : ListEditorController
    {
        private readonly InternalVarsEditorModel _model;

        public InternalVarsEditorController(InternalVarsEditorModel model)
            : base(model)
        {
            _model = model;
        }

        internal bool SelectKeyword(string keyword)
        {
            ListEditorElement element = null;

            if (false == String.IsNullOrEmpty(keyword))
            {
                element = _model.Elements
                   .Where(e => e.Value.ToString() == keyword)
                   .FirstOrDefault();
                if (element == null)
                    return false;
            }

            SelectElement(element);

            return true;
        }

        protected override void NewElementCore()
        {
            Debug.Fail("Not allowed");
        }

        protected override bool RemoveCore(ListEditorElement element)
        {
            Debug.Fail("Not allowed");
            return false;
        }
    }
}