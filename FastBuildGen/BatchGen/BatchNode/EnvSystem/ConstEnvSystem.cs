using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.EnvSystem
{
    internal static class ConstEnvSystem
    {
        public const string ConstBatchFilePath = "%~dp0";
        public const string ConstBatchParamFormat = "%{0}";
        public const string ConstDate = "DATE";
        public const string ConstPath = "PATH";
        public const string ConstProgramFiles = "ProgramFiles";
        public const string ConstProgramFilesX86 = "ProgramFiles(x86)";
        public const string ConstProgramW6432 = "ProgramW6432";
        public const string ConstTime = "TIME";
    }
}