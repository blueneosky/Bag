using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace VerMgr
{
    internal class Program
    {
        #region Startup

        private const string CstPLCmdHelp = "/help";
        private const string CstPLCmdHelp2 = "/h";

        private static void Main(string[] args)
        {
            #region Test
            AssemblyInfo ai = AssemblyInfoFactory.ReadAssemblyInfoCs(@"C:\Users\prioulj\Documents\Visual Studio 2010\Projects\VerMgr\VerMgr\Properties\AssemblyInfo.cs");
            int?[] versions = ai.SplittedNumericVersion;
            versions[2]++;
            ai.SplittedNumericVersion = versions;
            AssemblyInfoFactory.WriteAssemblyInfoCs(@"C:\Users\prioulj\Documents\Visual Studio 2010\Projects\VerMgr\VerMgr\Properties\AssemblyInfo.cs", ai);

            return;
            #endregion

            bool isHelp = args
                .Where(arg =>
                    String.Equals(arg, CstPLCmdHelp, StringComparison.OrdinalIgnoreCase)
                    || String.Equals(arg, CstPLCmdHelp2, StringComparison.OrdinalIgnoreCase)
                )
                .Any();
            if (isHelp)
            {
                ShowHelp();
            }
            else
            {
                DoWorks(args);
            }

#if DEBUG
            Console.In.ReadLine();
#endif
        }

        #endregion Startup

        #region Help

        private static void ShowHelp()
        {
            string[] messages = new[]
            {
                Assembly.GetAssembly(typeof(Program)).GetName().FullName,
                "",
                CstPLCmdAssemblyInfoPath+":<path>",
                CstPLCmdAssemblyInfoPath2+":<path>",
                "\t<path> to AssemblyInfo.cs",
                "",
            };

            foreach (string message in messages)
            {
                Console.WriteLine(message);
            }
        }

        #endregion Help

        private const string CstPLCmdAssemblyInfoPath = "/AssemblyInfoPath:";
        private const string CstPLCmdAssemblyInfoPath2 = "/AIP:";
        private const string CstPLCmdDefaultPath = "/";

        private static void DoWorks(string[] args)
        {
            // vars form cmd line
            string assemblyInfoPath = null;
            string defaultPath = null;

            // read cmd lines parameters
            bool allValidated = true;
            foreach (string arg in args)
            {
                allValidated = allValidated
                    && CheckPLCMd(arg, CstPLCmdAssemblyInfoPath, p => assemblyInfoPath = p)
                    && CheckPLCMd(arg, CstPLCmdAssemblyInfoPath2, p => assemblyInfoPath = p)

                    && CheckPLCMd(arg, CstPLCmdDefaultPath, p => defaultPath = p, true);
            }

            // check cmd lines parameters
            bool ok = allValidated
                && (false == String.IsNullOrEmpty(assemblyInfoPath))
                ;

            if (false == ok)
            {
                ShowHelp();
                return;
            }

            // TODO
        }

        private static bool CheckPLCMd(string argument, string plcmd, Action<string> assignAction, bool inverse = false)
        {
            bool validated = false;

            if (argument.StartsWith(plcmd) != inverse)
            {
                string param = null;
                if (argument.Length > plcmd.Length)
                {
                    param = argument.Substring(plcmd.Length);
                }
                assignAction(param);
                validated = true;
            }

            return validated;
        }
    }
}