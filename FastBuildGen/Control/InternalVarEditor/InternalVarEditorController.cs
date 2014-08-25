using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;

namespace FastBuildGen.Control.InternalVarEditor
{
    internal class InternalVarEditorController
    {
        private readonly InternalVarEditorModel _model;
        private readonly IFastBuildInternalVarController _fastBuildInternalVarController;

        public InternalVarEditorController(InternalVarEditorModel model)
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

            SetValueCore(keyword, newValue);
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
            _fastBuildInternalVarController.SetValue(keyword, newValue);
        }

        #endregion Core
    }
}