﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;

namespace FastBuildGen.Common.Control
{
    internal class ModuleItem : ListBoxItem<IParamDescriptionHeoModule>
    {
        public ModuleItem(IParamDescriptionHeoModule value)
            : base(value)
        {
        }

        public override string ToString()
        {
            return Value.Name;
        }
    }
}