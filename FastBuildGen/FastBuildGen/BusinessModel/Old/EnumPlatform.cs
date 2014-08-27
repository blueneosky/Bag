using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel.Old
{
    public enum EnumPlatform
    {
        Default = BatchGen.BatchNode.ExternCmd.EnumPlatform.Default,

        Win32 = BatchGen.BatchNode.ExternCmd.EnumPlatform.Win32,
        X86 = BatchGen.BatchNode.ExternCmd.EnumPlatform.X86,
        // non used
        //AnyCPU = BatchGen.BatchNode.ExternCmd.EnumPlatform.AnyCPU,
        //MixedPlatform = BatchGen.BatchNode.ExternCmd.EnumPlatform.MixedPlatform,
    }
}