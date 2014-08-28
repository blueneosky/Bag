using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Expression;

namespace BatchGen.BatchNode.ExternCmd
{
    [Serializable]
    public class MSBuildCmd : BatchCmdBase
    {
        public const string ConstConfigurationDebug = "Debug";
        public const string ConstConfigurationRelease = "Release";

        public static Dictionary<EnumPlatform, BatchExpressionBase> ConstExpressionByPlatforms = new Dictionary<EnumPlatform, BatchExpressionBase>
        {
              { EnumPlatform.Default, null }
            , { EnumPlatform.Win32, new ValueExpression("win32") }
            , { EnumPlatform.X86, new ValueExpression("x86") }
            , { EnumPlatform.MixedPlatform, new ValueExpression("\"Mixed Platforms\"") }
            , { EnumPlatform.AnyCPU, new ValueExpression("AnyCpu") }
        };

        public static ValueExpression ConstExpressionConfigurationDebug = new ValueExpression(ConstConfigurationDebug);
        public static ValueExpression ConstExpressionConfigurationRelease = new ValueExpression(ConstConfigurationRelease);
        private BatchExpressionBase _cliExpression;

        public MSBuildCmd(LiteralBatch literalMSBuildCli)
        {
            _cliExpression = literalMSBuildCli.LiteralValue;
        }

        public MSBuildCmd(BatchExpressionBase cliExpression)
        {
            _cliExpression = cliExpression;
        }

        public BatchExpressionBase CliExpression
        {
            get { return _cliExpression; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}