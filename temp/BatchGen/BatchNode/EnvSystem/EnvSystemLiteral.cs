using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Expression;

namespace BatchGen.BatchNode.EnvSystem
{
    [Serializable]
    public class EnvSystemLiteral : LiteralBatch
    {
        private static ValueExpression _batchFilePath = new ValueExpression(ConstEnvSystem.ConstBatchFilePath);

        private static ValueExpression _batchParam0 = GetBatchParam(0);

        private static ValueExpression _batchParam1 = GetBatchParam(1);

        private static LiteralValueExpression _date = new EnvSystemLiteral(ConstEnvSystem.ConstDate).LiteralValue;

        private static EnvSystemLiteral _path = new EnvSystemLiteral(ConstEnvSystem.ConstPath);

        private static LiteralValueExpression _programFiles = new EnvSystemLiteral(ConstEnvSystem.ConstProgramFiles).LiteralValue;

        private static LiteralValueExpression _programFilesX86 = new EnvSystemLiteral(ConstEnvSystem.ConstProgramFilesX86).LiteralValue;

        private static LiteralValueExpression _programW6432 = new EnvSystemLiteral(ConstEnvSystem.ConstProgramW6432).LiteralValue;

        private static LiteralValueExpression _time = new EnvSystemLiteral(ConstEnvSystem.ConstTime).LiteralValue;

        private EnvSystemLiteral(string name)
            : base(name)
        {
        }

        public static ValueExpression BatchFilePath { get { return _batchFilePath; } }

        public static ValueExpression BatchParam0 { get { return _batchParam0; } }

        public static ValueExpression BatchParam1 { get { return _batchParam1; } }

        public static LiteralValueExpression Date { get { return _date; } }

        public static EnvSystemLiteral Path { get { return _path; } }

        public static LiteralValueExpression ProgramFiles { get { return _programFiles; } }

        public static LiteralValueExpression ProgramFilesX86 { get { return _programFilesX86; } }

        public static LiteralValueExpression ProgramW6432 { get { return _programW6432; } }

        public static LiteralValueExpression Time { get { return _time; } }

        public static ValueExpression GetBatchParam(int indexParam)
        {
            return new ValueExpression(String.Format(ConstEnvSystem.ConstBatchParamFormat, indexParam));
        }
    }
}