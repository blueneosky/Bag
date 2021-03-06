﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;

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

        internal bool SelectModule(FBSolutionTarget solutionTarget)
        {
            ListEditorElement element = null;
            if (solutionTarget != null)
            {
                element = _model.Elements
                  .Where(e => solutionTarget.SameAs(e.Value))
                  .FirstOrDefault();
                if (element == null)
                    return false;
            }

            SelectElement(element);

            return true;
        }

        protected override void NewElementCore()
        {
            Debug.Fail("Must not called");
        }

        protected override bool RemoveCore(ListEditorElement element)
        {
            Debug.Fail("Must not called");
            return false;
        }
    }
}