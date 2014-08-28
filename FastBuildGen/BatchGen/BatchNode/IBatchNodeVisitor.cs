using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Boolean;
using BatchGen.BatchNode.Cmd;
using BatchGen.BatchNode.Expression;
using BatchGen.BatchNode.ExternCmd;
using BatchGen.BatchNode.Goto;
using BatchGen.BatchNode.Integer;
using BatchGen.BatchNode.Macro;
using BatchGen.BatchNode.Sub;

namespace BatchGen.BatchNode
{
    public interface IBatchNodeVisitor
    {
        object Visit(BatchFileBase batchFileBase);

        object Visit(EchoCmd cmdEcho);

        object Visit(RemBatch cmdRem);

        object Visit(SetExpressionCmd cmdSet);

        object Visit(TitleCmd cmdTitle);

        object Visit(CdCmd cmdCd);

        object Visit(IfCmd cmdIf);

        object Visit(BlocMacro macroBloc);

        object Visit(IfMacro macroIf);

        object Visit(NopBatch batchNop);

        object Visit(GotoCmd cmdGoto);

        object Visit(CallSubCmd callCmd);

        object Visit(PauseCmd pauseCmd);

        object Visit(LiteralBatch variableBatch);

        object Visit(LiteralValueExpression literalValueBatch);

        object Visit(ValueExpression valueExpression);

        object Visit(ShiftCmd shiftCmd);

        object Visit(SetlocalCmd setlocalCmd);

        object Visit(ExitCmd exitCmd);

        object Visit(SubMacro subMacro);

        object Visit(BlocMacroFileNodeBase blocMacroFileNode);

        object Visit(LabelSubBatch labelSubBatch);

        object Visit(LabelGotoBatch labelGotoBatch);

        object Visit(ComposedExpression composedExpression);

        object Visit(CallBatchCmd callBatchCmd);

        object Visit(ConditionalCodeNode conditionalCodeNode);

        object Visit(PipeToFileCmd pipeToFileCmd);

        object Visit(MSBuildCliMacro mSBuildCmd);

        object Visit(MSBuildCmd mSBuildCmd);

        object Visit(TaskKillCmd taskKillCmd);

        object Visit(SetIntegerCmd setIntegerCmd);

        object Visit(LiteralIntegerExpression literalIntegerExpression);

        object Visit(IntegerUnaryOperatorExpression integerUnaryOperatorExpression);

        object Visit(IntegerOperatorExpression integerOperatorExpression);

        object Visit(IntegerValueExpression integerValueExpression);

        object Visit(BooleanValueExpressionBase booleanValueExpressionBase);

        object Visit(BooleanOperatorExpression booleanOperatorExpression);

        object Visit(LiteralBooleanExpression literalBooleanExpression);

        object Visit(NotBooleanExpression notBooleanExpression);

        object Visit(SGenPlusCmd sGenPlusCmd);

        object Visit(GotoEofCmd gotoEofCmd);
    }
}