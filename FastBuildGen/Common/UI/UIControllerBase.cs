using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UI
{
    public class UIControllerBase : IUIController
    {
        private readonly IUIModel _uiModel;

        public UIControllerBase(IUIModel uiModel)
        {
            _uiModel = uiModel;
        }

        public bool UIEnableView(object param)
        {
            bool success = false;

            if (_uiModel != null)
                success = _uiModel.UIEnableView(param);

            return success;
        }
    }
}