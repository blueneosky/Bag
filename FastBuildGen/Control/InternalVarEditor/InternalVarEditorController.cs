using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FastBuildGen.Common;
using FastBuildGen.Common.UI;
using FastBuildGen.Common.UndoRedo;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Control.InternalVarEditor
{
    internal class InternalVarEditorController : UIControllerBase
    {
        private readonly InternalVarEditorModel _model;
        private readonly IFastBuildInternalVarController _fastBuildInternalVarController;

        public InternalVarEditorController(InternalVarEditorModel model)
            : base(model)
        {
            _model = model;
            _fastBuildInternalVarController = new FastBuildInternalVarController(model.FastBuildInternalVarModel);
        }

        public void SetValue(string newValue)
        {
            string keyword = _model.Keyword;

            string oldValue = _model.FastBuildInternalVarModel[keyword];
            if (newValue == oldValue)
                return;

            bool success = CheckValueAvailabality(newValue, keyword);
            if (!success)
                return;

            string name = this.GetType().Name + "_SetValue";
            string title = "Set '" + newValue + "' to " + keyword;
            using (_model.UndoRedoManager.NewUndoRedoActionMacroBloc(name, title))
            {
                Action uiEnableViewAction = delegate
                {
                    bool uiSuccess = _model.UIEnableView(keyword);
                    Debug.Assert(uiSuccess);
                };

                SetValueCore(keyword, newValue);

                _model.UndoRedoManager.Do(name, title, null, uiEnableViewAction, uiEnableViewAction);
            }
        }

        private bool CheckValueAvailabality(string value, string keyword)
        {
            bool isValueUsed = _model.FastBuildInternalVarModel.ContainsPropertyValue(value, keyword);
            if (isValueUsed)
            {
                throw new FastBuildGenException("Value already used by an other ");
            }

            return true;
        }

        #region Core

        private void SetValueCore(string keyword, string newValue)
        {
            string oldValue = _model.FastBuildInternalVarModel[keyword];

            string name = this.GetType().Name + "_SetValueCore";
            string title = "Set internal var value '" + newValue + "' to " + keyword;
            Action doAction = delegate { _fastBuildInternalVarController.SetValue(keyword, newValue); };
            Action undoAction = delegate { _fastBuildInternalVarController.SetValue(keyword, oldValue); };
            _model.UndoRedoManager.Do(name, title, doAction, undoAction);
        }

        #endregion
    }
}