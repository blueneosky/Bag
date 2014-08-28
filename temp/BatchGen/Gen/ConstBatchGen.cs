using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.Gen
{
    internal class ConstBatchGen
    {
        public const string ConstCmdCall = "CALL";
        public const string ConstCmdCd = "CD";
        public const string ConstCmdEcho = "ECHO";
        public const string ConstCmdEchoParamEmptyText = ".";
        public const string ConstCmdEchoParamOff = ConstCmdSwitchEchoOffPrefix + ConstCmdEcho + " off";
        public const string ConstCmdExit = "EXIT";
        public const string ConstCmdExitParamExitScript = " /B";
        public const string ConstCmdGoto = "GOTO";
        public const string ConstCmdLabelGotoEof = ":EOF";
        public const string ConstCmdIf = "IF";
        public const string ConstCmdMSBuild = "MSBuild";
        public const string ConstCmdPause = "PAUSE";
        public const string ConstCmdRem = "::";
        public const string ConstCmdSet = "SET";
        public const string ConstCmdSetlocal = "SETLOCAL";
        public const string ConstCmdSGenPlus = "SGenPlus";
        public const string ConstCmdShift = "SHIFT";
        public const string ConstCmdSwitchEchoOffPrefix = "@";
        public const string ConstCmdTaskKill = "TASKKILL";
        public const string ConstCmdTitle = "TITLE";
        public const string ConstGotoBlocElsePrefix = "else_";
        public const string ConstGotoBlocEndIfPrefix = "endif_";
        public const string ConstGotoEndForPrefix = "endFor_";
        public const string ConstGotoEndWhilePrefix = "endWhile_";
        public const string ConstGotoForPrefix = "for_";
        public const string ConstGotoLabelPrefix = ":";
        public const string ConstGotoWhilePrefix = "while_";
        public const string ConstKeywordElse = " ELSE ";
        public const string ConstKeywordEquality = "==";
        public const string ConstKeywordErrorLevel = "ERRORLEVEL";
        public const string ConstKeywordExist = "EXIST";
        public const string ConstKeywordFalseValue = "0";
        public const string ConstKeywordNot = "NOT";
        public const string ConstKeywordTrueValue = "1";
    }
}