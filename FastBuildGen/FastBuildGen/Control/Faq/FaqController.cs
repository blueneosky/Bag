using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Control.Faq
{
    internal class FaqController
    {
        private readonly FaqModel _model;

        public FaqController(FaqModel model)
        {
            _model = model;
        }
    }
}