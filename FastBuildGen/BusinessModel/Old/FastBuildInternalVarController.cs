using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel.Old
{
    internal class FastBuildInternalVarController : IFastBuildInternalVarController
    {
        private readonly IFastBuildInternalVarModel _model;

        public FastBuildInternalVarController(IFastBuildInternalVarModel model)
        {
            _model = model;
        }

        public void ResetToDefault()
        {
            _model.ResetToDefault();
        }

        public void SetValue(string keyword, string newValue)
        {
            _model[keyword] = newValue;
        }
    }
}