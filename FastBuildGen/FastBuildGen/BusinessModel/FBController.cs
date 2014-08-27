using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    internal class FBController
    {
        private readonly FBModel _model;

        public FBController(FBModel model)
        {
            _model = model;
        }
    }
}