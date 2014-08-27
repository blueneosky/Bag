using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace VerMgr
{
    public static class AssemblyInfoFactory
    {
        private const string CstAicsAssemblyTitle = "AssemblyTitle";
        private const string CstAicsAssemblyDescription = "AssemblyDescription";
        private const string CstAicsAssemblyConfiguration = "AssemblyConfiguration";
        private const string CstAicsAssemblyCompany = "AssemblyCompany";
        private const string CstAicsAssemblyProduct = "AssemblyProduct";
        private const string CstAicsAssemblyCopyright = "AssemblyCopyright";
        private const string CstAicsAssemblyTrademark = "AssemblyTrademark";
        private const string CstAicsAssemblyVersion = "AssemblyVersion";
        private const string CstAicsAssemblyFileVersion = "AssemblyFileVersion";

        private const string CstRegexPatternSpace = @"\s*";
        private const string CstRegexPatternCaptureValeur1 = @"@""(?:""""|[^""])*""";
        private const string CstRegexPatternCaptureValeur2 = @"""(?:\\""|[^""])*""";
        private const string CstRegexPatternCaptureValeur = @"((?(@)" + CstRegexPatternCaptureValeur1 + @"|" + CstRegexPatternCaptureValeur2 + @"))";
        private const string CstRegregexPatternCaptureAttributName = @"(\w+)";
        private const string CstRegexPatternCaptureAttribut = CstRegregexPatternCaptureAttributName + CstRegexPatternSpace + @"\(" + CstRegexPatternSpace + CstRegexPatternCaptureValeur + CstRegexPatternSpace + @"\)";
        private const string CstRegexPatternCaptureAssemblyAttribut = @"\[" + CstRegexPatternSpace + @"assembly" + CstRegexPatternSpace + @":" + CstRegexPatternSpace + CstRegexPatternCaptureAttribut + CstRegexPatternSpace + @"\]";
        private const string CstRegexPattern = @"(?:^|\r|\n)\s*" + CstRegexPatternCaptureAssemblyAttribut + @"\s*";
        private const int CstRegExPatternGlobalMatchIndex = 0;
        private const int CstRegExPatternAttributNameMatchIndex = 1;
        private const int CstRegExPatternQuotedValueMatchIndex = 2;

        #region Serialization

        public static void Serialize(AssemblyInfo assemblyInfo, string path)
        {
            string basePath = Path.GetDirectoryName(path);
            Directory.CreateDirectory(basePath);

            try
            {
                using (XmlTextWriter writer = new XmlTextWriter(path, null))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;

                    XmlSerializer serializer = new XmlSerializer(typeof(AssemblyInfo));
                    serializer.Serialize(writer, assemblyInfo);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                string message = "Error : saving '" + path + "' failed (" + e.Message + ")";
                throw new VMException(message, e);
            }
        }

        public static AssemblyInfo Deserialize(string path)
        {
            AssemblyInfo result = null;

            try
            {
                using (StreamReader file = new StreamReader(path))
                {
                    XmlSerializer reader = new XmlSerializer(typeof(AssemblyInfo));
                    AssemblyInfo data = reader.Deserialize(file) as AssemblyInfo;

                    if (data != null)
                    {
                        result = data;
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }

            if (result == null)
            {
                result = AssemblyInfo.Default;
            }

            return result;
        }

        #endregion Serialization

        #region AssemblyInfo.cs reader/writer

        public static AssemblyInfo ReadAssemblyInfoCs(string path)
        {
            AssemblyInfo result = new AssemblyInfo();

            try
            {
                using (StreamReader stream = new StreamReader(path))
                {
                    result = ReadAssemblyInfoCs(stream);
                }
            }
            catch (Exception e)
            {
                result = AssemblyInfo.Default;
                throw new VMException("Reading failed (" + e.Message + ")", e);
            }
            return result;
        }

        private static AssemblyInfo ReadAssemblyInfoCs(TextReader stream)
        {
            string text = stream.ReadToEnd();

            AssemblyInfo assemblyInfo = new AssemblyInfo();

            MatchCollection matches = Regex.Matches(text, CstRegexPattern);

            IEnumerable<string[]> infos = matches
                .OfType<Match>()
                .Where(m => m.Success)
                .Where(m => m.Groups
                    .OfType<Group>()
                    .Take(1)
                    .All(g => g.Success)
                )
                .Select(m => m.Groups
                    .OfType<Group>()
                    .Select(g => g.Success ? g.Value : null)
                    .ToArray()
                )
                ;

            foreach (string[] infoAssemblyAttribut in infos)
            {
                Debug.Assert(infoAssemblyAttribut.Length == 3);

                string attributName = infoAssemblyAttribut[CstRegExPatternAttributNameMatchIndex];
                string attributValue = CodeToValue(infoAssemblyAttribut[CstRegExPatternQuotedValueMatchIndex]);

                switch (attributName)
                {
                    case CstAicsAssemblyTitle:
                        assemblyInfo.Title = attributValue;
                        break;

                    case CstAicsAssemblyDescription:
                        assemblyInfo.Description = attributValue;
                        break;

                    case CstAicsAssemblyConfiguration:
                        assemblyInfo.Configuration = attributValue;
                        break;

                    case CstAicsAssemblyCompany:
                        assemblyInfo.Company = attributValue;
                        break;

                    case CstAicsAssemblyProduct:
                        assemblyInfo.Product = attributValue;
                        break;

                    case CstAicsAssemblyCopyright:
                        assemblyInfo.Copyright = attributValue;
                        break;

                    case CstAicsAssemblyTrademark:
                        assemblyInfo.Tradmark = attributValue;
                        break;

                    case CstAicsAssemblyVersion:
                        assemblyInfo.Version = attributValue;
                        break;

                    case CstAicsAssemblyFileVersion:
                        assemblyInfo.FileVersion = attributValue;
                        break;

                    default:
                        // ignored
                        break;
                }
            }

            return assemblyInfo;
        }

        public static void WriteAssemblyInfoCs(string path, AssemblyInfo assemblyInfo)
        {
            try
            {
                string data = null;
                using (StreamReader stream = new StreamReader(path))
                {
                    data = GetWriteAssemblyInfoCs(stream, assemblyInfo);
                }
                using (StreamWriter stream = new StreamWriter(path))
                {
                    stream.Write(data);
                }
            }
            catch (Exception e)
            {
                throw new VMException("Writing failed (" + e.Message + ")", e);
            }
        }

        private static string GetWriteAssemblyInfoCs(TextReader sourceStream, AssemblyInfo assemblyInfo)
        {
            string source = sourceStream.ReadToEnd();

            MatchEvaluator matchEvaluator = (match) => WriteAssemblyInfoCsMatchEvaluator(match, assemblyInfo);

            string result = Regex.Replace(source, CstRegexPattern, matchEvaluator);

            return result;
        }

        private static string WriteAssemblyInfoCsMatchEvaluator(Match match, AssemblyInfo assemblyInfo)
        {
            string result = match.Value;

            bool success = match.Success
                && match.Groups.OfType<Group>().Take(1).All(g => g.Success);
            if (success)
            {
                Group attributNameGroup = match.Groups[CstRegExPatternAttributNameMatchIndex];
                Capture quotedValueCapture = match.Groups[CstRegExPatternQuotedValueMatchIndex].Captures[0];
                Capture globalCapture = match.Groups[CstRegExPatternGlobalMatchIndex].Captures[0];
                string attributName = attributNameGroup.Value;
                string globalCaptureText = globalCapture.Value;

                int startIndex = match.Index;
                int localStartIndex1 = 0;
                int localLength1 = quotedValueCapture.Index - startIndex;
                int localStartIndex3 = localLength1 + quotedValueCapture.Length;
                //int localLength3 = captureGlobalText.Length - localStartIndex3;
                string text1 = globalCaptureText.Substring(localStartIndex1, localLength1);
                string text3 = globalCaptureText.Substring(localStartIndex3);

                string value = null;
                switch (attributName)
                {
                    case CstAicsAssemblyTitle:
                        value = assemblyInfo.Title ?? String.Empty;
                        break;

                    case CstAicsAssemblyDescription:
                        value = assemblyInfo.Description ?? String.Empty;
                        break;

                    case CstAicsAssemblyConfiguration:
                        value = assemblyInfo.Configuration ?? String.Empty;
                        break;

                    case CstAicsAssemblyCompany:
                        value = assemblyInfo.Company ?? String.Empty;
                        break;

                    case CstAicsAssemblyProduct:
                        value = assemblyInfo.Product ?? String.Empty;
                        break;

                    case CstAicsAssemblyCopyright:
                        value = assemblyInfo.Copyright ?? String.Empty;
                        break;

                    case CstAicsAssemblyTrademark:
                        value = assemblyInfo.Tradmark ?? String.Empty;
                        break;

                    case CstAicsAssemblyVersion:
                        value = assemblyInfo.Version ?? String.Empty;
                        break;

                    case CstAicsAssemblyFileVersion:
                        value = assemblyInfo.FileVersion ?? String.Empty;
                        break;

                    default:
                        // ignored
                        break;
                }

                if (value != null)
                {
                    string text2 = ValueToCode(value);
                    result = String.Concat(text1, text2, text3);
                }
            }

            return result;
        }

        #region Code <-> Value

        // ', ", \, 0, a, b, f, n, r, t, u, U, x, v
        private static Tuple<string, string>[] CstValueToCodeEscaping = new Tuple<string, string>[]
        {
            //new Tuple<string, string>({ "\'", "\\'" ),  // not usable in string
            new Tuple<string, string>( "\"", "\\\"" ),
            new Tuple<string, string>( "\\", "\\\\" ),
            //new Tuple<string, string>( "\0", "\\0" ), // not use in AssemblyInfo
            //new Tuple<string, string>( "\a", "\\a" ), // not use in AssemblyInfo
            //new Tuple<string, string>( "\b", "\\b" ), // not use in AssemblyInfo
            //new Tuple<string, string>( "\f", "\\f" ), // not use in AssemblyInfo
            //new Tuple<string, string>( "\n", "\\n" ), // not use in AssemblyInfo
            //new Tuple<string, string>( "\r", "\\r" ), // not use in AssemblyInfo
            //new Tuple<string, string>( "\t", "\\t" ), // not use in AssemblyInfo
            //new Tuple<string, string>( "\u", "\\u" ), // not usable in string
            //new Tuple<string, string>( "\U", "\\U" ), // not usable in string
            //new Tuple<string, string>( "\x", "\\x" ), // not usable in string
            //new Tuple<string, string>( "\v", "\\v" ), // not use in AssemblyInfo
        };

        private static string CstPatternValueToCodeEscaping = "(?:"
            + CstValueToCodeEscaping
                .Select(t => "(" + Regex.Escape(t.Item1) + ")")
                .Aggregate((acc, p) => acc + "|" + p)
            + ")";

        private static MatchEvaluator CstMatchEvaluatorValueToCodeEscaping = (match) =>
        {
            int index = match.Groups
                .OfType<Group>()
                .Skip(1)    // don't take the first one (global match)
                .TakeWhile(m => !m.Success)
                .Count();
            Debug.Assert(index < CstValueToCodeEscaping.Length);
            string result = CstValueToCodeEscaping[index].Item2;

            return result;
        };

        private static string CstPatternCodeToValueUnescaping = "(?:"
            + CstValueToCodeEscaping
                .Select(t => "(" + Regex.Escape(t.Item2) + ")")
                .Aggregate((acc, p) => acc + "|" + p)
            + ")";

        private static MatchEvaluator CstMatchEvaluatorCodeToValueUnescaping = (match) =>
        {
            int index = match.Groups
                .OfType<Group>()
                .Skip(1)    // don't take the first one (global match)
                .TakeWhile(m => !m.Success)
                .Count();
            Debug.Assert(index < CstValueToCodeEscaping.Length);
            string result = CstValueToCodeEscaping[index].Item1;

            return result;
        };

        private static String ValueToCode(string source)
        {
            // "it's \t\"foo\"" => "\"it's \t\"foo\"\""

            if (source == null)
                source = String.Empty;

            string result = Regex.Replace(source, CstPatternValueToCodeEscaping, CstMatchEvaluatorValueToCodeEscaping);

            result = "\"" + result + "\"";

            return result;
        }

        private static String CodeToValue(string source)
        {
            // "\"it's \t\"foo\"\"" =>  "it's \t\"foo\""
            // "@\"it's \"\"foo\\bar\"\"" => "it's \t\"foo\\bar\""

            if (source == null)
                source = String.Empty;

            string result = null;

            if (source.StartsWith("@\""))
            {
                Debug.Assert(source.EndsWith("\""));

                result = source.Substring(2, source.Length - 3);
                result = result.Replace("\"\"", "\"");
            }
            else
            {
                Debug.Assert(source.StartsWith("\""));
                Debug.Assert(source.EndsWith("\""));

                result = source.Substring(1, source.Length - 2);

                result = Regex.Replace(result, CstPatternCodeToValueUnescaping, CstMatchEvaluatorCodeToValueUnescaping);

                //foreach (Tuple<string, string> valueToCode in CstValueToCodeEscaping)
                //{
                //    result = result.Replace(valueToCode.Item2, valueToCode.Item1);
                //}
            }
            Debug.Assert(result != null);

            return result;
        }

        #endregion Code <-> Value

        #endregion AssemblyInfo.cs reader/writer
    }
}