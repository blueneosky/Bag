using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BatchGen.BatchNode;
using BatchGen.BatchNode.Boolean;
using BatchGen.BatchNode.Cmd;
using BatchGen.BatchNode.Expression;
using BatchGen.BatchNode.ExternCmd;
using BatchGen.BatchNode.Goto;
using BatchGen.BatchNode.Integer;
using BatchGen.BatchNode.Macro;
using BatchGen.BatchNode.Sub;
using BatchGen.Gen;
using FastBuildGen.BatchNode;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common.UndoRedo;

namespace FastBuildGen.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool withEchoOff = _withEchoOff.Checked;

            BatchFile file = new BatchFile(withEchoOff);
            NopBatch nop = new NopBatch();

            file.Add(new RemBatch("Title"));
            file.Add(new TitleCmd("my title"));
            file.Add(nop);

            file.Add(new RemBatch("REM : null, titi"));
            file.Add(new RemBatch(null));
            file.Add(new RemBatch("titi"));
            file.Add(nop);

            file.Add(new RemBatch("CD : null, titi"));
            file.Add(new CdCmd((string)null));
            file.Add(new CdCmd("titi"));
            file.Add(nop);

            file.Add(new RemBatch("Shift"));
            file.Add(new ShiftCmd());
            file.Add(nop);

            file.Add(new RemBatch("Setlocal"));
            file.Add(new SetlocalCmd());
            file.Add(nop);

            file.Add(new RemBatch("ECHO : null, empty, titi, Valued(titi)"));
            file.Add(new EchoCmd((string)null));
            file.Add(new EchoCmd(String.Empty));
            file.Add(new EchoCmd("titi"));
            file.Add(new EchoCmd(new ValueExpression("titi")));
            file.Add(nop);

            file.Add(new RemBatch("GOTO : label_1"));
            LabelGotoBatch labelGoto1 = new LabelGotoBatch("MyGoto1");
            file.Add(new GotoCmd(labelGoto1));

            file.Add(new RemBatch("MacroFileNode"));
            BlocMacroFileNode blocMacroFileNode = new BlocMacroFileNode();
            blocMacroFileNode.Add(labelGoto1);
            file.Add(blocMacroFileNode);
            file.Add(nop);

            file.Add(new RemBatch("IfCMD"));
            BatchCmdBase cmd = new EchoCmd("toto");
            file.Add(new IfCmd(2, cmd));
            file.Add(new IfCmd("fileName", cmd));
            file.Add(new IfCmd(new ValueExpression("titi"), new ValueExpression("toto"), cmd));
            file.Add(new IfCmd(new LiteralBatch("titiName"), new ValueExpression("toto"), cmd));
            file.Add(new IfCmd(new LiteralBatch("titiName"), new LiteralBatch("totoName"), cmd));
            file.Add(nop);

            file.Add(new RemBatch("IfCMD NOT"));
            file.Add(new IfCmd(2, cmd, true));
            file.Add(new IfCmd("fileName", cmd, true));
            file.Add(new IfCmd(new ValueExpression("titi"), new ValueExpression("toto"), cmd, true));
            file.Add(new IfCmd(new LiteralBatch("titiName"), new ValueExpression("toto"), cmd, true));
            file.Add(new IfCmd(new LiteralBatch("titiName"), new LiteralBatch("totoName"), cmd, true));
            file.Add(nop);

            file.Add(new RemBatch("Call Sub, return \"titi\""));
            LabelSubBatch labelSub1 = new LabelSubBatch("MySub1");
            BlocMacro subBody = new BlocMacro();
            LiteralBatch subVar1 = new LiteralBatch("subVar1");
            LiteralBatch subVar2 = new LiteralBatch("subVar2");
            subBody.Add(new SetExpressionCmd(subVar1, new ValueExpression("titi")));
            subBody.Add(new SetExpressionCmd(subVar2, new ComposedExpression(new LiteralValueExpression(subVar1), new ValueExpression("_et_toto"))));
            LiteralValueExpression subVarValue = new LiteralValueExpression(subVar2);
            SubMacro sub = new SubMacro(labelSub1, subBody, subVarValue);
            file.Add(new CallSubCmd(labelSub1));
            file.Add(nop);

            file.Add(new RemBatch("IfMacro If_Then_Else"));
            BlocMacro nodeThen = new BlocMacro();
            nodeThen.Add(new EchoCmd("Then"));
            BlocMacro nodeElse = new BlocMacro();
            nodeElse.Add(new EchoCmd("Else"));
            file.Add(new IfMacro(new LiteralBatch("titiName"), new ValueExpression("toto"), nodeThen, nodeElse));
            file.Add(nop);

            string labelBase1 = "MyIfMacro1";
            file.Add(new RemBatch("IfGotoMacro If_Then_Else"));
            file.Add(new IfGotoMacro(new LiteralBatch("titiName"), new ValueExpression("toto"), nodeThen, nodeElse, labelBase1));
            file.Add(nop);

            file.Add(new RemBatch("IfMacro If_Then"));
            file.Add(new IfMacro(new LiteralBatch("titiName"), new ValueExpression("toto"), nodeThen, null));
            file.Add(nop);

            string labelBase2 = "MyIfMacro2";
            file.Add(new RemBatch("IfGotoMacro If_Then"));
            file.Add(new IfGotoMacro(new LiteralBatch("titiName"), new ValueExpression("toto"), nodeThen, null, labelBase2));
            file.Add(nop);

            file.Add(new RemBatch("CallBatch : myBatch"));
            file.Add(new CallBatchCmd("myBatch"));
            file.Add(nop);

            BatchCmdBase cmdConditional = new EchoCmd("titi");
            LiteralBatch literalConditional = new LiteralBatch("conditional_var");
            file.Add(new RemBatch("IsTrueConditional"));
            file.Add(new IfCmd(new IsTrueConditional(literalConditional), cmdConditional));
            file.Add(new RemBatch("IsFalseConditional"));
            file.Add(new IfCmd(new IsFalseConditional(literalConditional), cmdConditional));
            file.Add(new RemBatch("TrueConditional"));
            file.Add(new IfCmd(TrueConditional.ValueExpression, cmdConditional));
            file.Add(new RemBatch("FalseConditional"));
            file.Add(new IfCmd(FalseConditional.ValueExpression, cmdConditional));
            file.Add(new RemBatch("SetFalse()"));
            file.Add(new SetFalseCmd(literalConditional));
            file.Add(new RemBatch("SetTrue"));
            file.Add(new SetTrueCmd(literalConditional));
            file.Add(nop);

            file.Add(new RemBatch("MSBuild0"));
            LiteralBatch literalMSBuildCli0 = new LiteralBatch("MSBuildCli0");
            file.Add(new MSBuildCliMacro(
                literalMSBuildCli0
                , null
                , null
                , null
                , null
                , null
                , null
                , null
                , null
                , BatchGen.BatchNode.ExternCmd.EnumPlatform.Default
                , null
                , null
                , null
                ));
            file.Add(new MSBuildCmd(literalMSBuildCli0));
            file.Add(nop);

            file.Add(new RemBatch("MSBuild1"));
            LiteralBatch literalMSBuildCli1 = new LiteralBatch("MSBuildCli1");
            file.Add(new MSBuildCliMacro(
                literalMSBuildCli1
                , FalseValueExpression.ValueExpression
                , FalseValueExpression.ValueExpression
                , FalseValueExpression.ValueExpression
                , FalseValueExpression.ValueExpression
                , null
                , FalseValueExpression.ValueExpression
                , FalseValueExpression.ValueExpression
                , FalseValueExpression.ValueExpression
                , BatchGen.BatchNode.ExternCmd.EnumPlatform.Default
                , null
                , null
                , null
                ));
            file.Add(new MSBuildCmd(literalMSBuildCli1));
            file.Add(nop);

            file.Add(new RemBatch("MSBuild2"));
            LiteralBatch literalMSBuildCli2 = new LiteralBatch("MSBuildCli2");
            file.Add(new MSBuildCliMacro(
                literalMSBuildCli2
                , TrueValueExpression.ValueExpression
                , TrueValueExpression.ValueExpression
                , TrueValueExpression.ValueExpression
                , TrueValueExpression.ValueExpression
                , MSBuildCmd.ConstExpressionConfigurationDebug
                , TrueValueExpression.ValueExpression
                , TrueValueExpression.ValueExpression
                , TrueValueExpression.ValueExpression
                , BatchGen.BatchNode.ExternCmd.EnumPlatform.X86
                , new Tuple<BooleanExpressionBase, BatchExpressionBase>(
                    new LiteralBatch("logActivated").LiteralBoolean
                    , new ValueExpression("my_msbuild_log.txt")
                    )
                , new Tuple<BooleanExpressionBase, BatchExpressionBase>(
                    new LiteralBatch("logActivated").LiteralBoolean
                    , new ValueExpression(@"c:\temp\build_path\")
                    )
                , new[] { new Tuple<BooleanExpressionBase, BatchExpressionBase>(new LiteralBatch("myTarget").LiteralBoolean, new ValueExpression("MyTarget")) }
                ));
            file.Add(new MSBuildCmd(literalMSBuildCli2));
            file.Add(nop);

            file.Add(new RemBatch("MSBuild3"));
            LiteralBatch literalMSBuildCli3 = new LiteralBatch("MSBuildCli3");
            LiteralBatch literalConditionalMSBuildCli = new LiteralBatch("cond_msbuild_cli");
            LiteralValueExpression literalValueMSBuildCli = literalConditionalMSBuildCli.LiteralValue;
            LiteralBooleanExpression literalBooleanMSBuildCli = literalConditionalMSBuildCli.LiteralBoolean;
            file.Add(new MSBuildCliMacro(
                literalMSBuildCli3
                , literalBooleanMSBuildCli
                , literalBooleanMSBuildCli
                , literalBooleanMSBuildCli
                , literalBooleanMSBuildCli
                , MSBuildCmd.ConstExpressionConfigurationDebug
                , literalBooleanMSBuildCli
                , literalBooleanMSBuildCli
                , literalBooleanMSBuildCli
                , BatchGen.BatchNode.ExternCmd.EnumPlatform.X86
                , new Tuple<BooleanExpressionBase, BatchExpressionBase>(new LiteralBatch("withLog").LiteralBoolean, literalValueMSBuildCli)
                , new Tuple<BooleanExpressionBase, BatchExpressionBase>(new LiteralBatch("withOutput").LiteralBoolean, literalValueMSBuildCli)
                , new[] { new Tuple<BooleanExpressionBase, BatchExpressionBase>(new LiteralBatch("myTarget").LiteralBoolean, new ValueExpression("MyTarget")) }
                ));
            file.Add(new MSBuildCmd(literalMSBuildCli3));
            file.Add(nop);

            file.Add(new RemBatch("SetIntegerCmd titi-=((1+4+3)/2/1)"));
            file.Add(new SetIntegerCmd(
                new LiteralBatch("titi")
                , EnumIntegerOperator.Sub
                , new IntegerOperatorExpression(
                    EnumIntegerOperator.Div
                    , new IntegerOperatorExpression(
                        EnumIntegerOperator.Add
                        , new IntegerValueExpression(1)
                        , new IntegerValueExpression(4)
                        , new IntegerValueExpression(3)
                        )
                    , new IntegerValueExpression(2)
                    , new IntegerValueExpression(1)
                    )
                ));
            file.Add(nop);

            file.Add(new PauseCmd());
            file.Add(nop);

            file.Add(new ExitCmd());
            file.Add(nop);

            file.Add(sub);

            string text = BatchGenerator.GetText(file);
            _textBox.Text = text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool withEchoOff = _withEchoOff.Checked;

            FastBuildModel model = new FastBuildModel();
            model.Initialize();

            // configuration
            model.WithEchoOff = withEchoOff;

            // generation
            FastBuildBatchFile file = new FastBuildBatchFile(model);
            string text = BatchGenerator.GetText(file);
            _textBox.Text = text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_textBox.Text);
            this.Close();
        }
    }
}