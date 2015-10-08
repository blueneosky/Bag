using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gitfs.Util
{
    public class ProcessHelper
    {
        private readonly ProcessStartInfo _processStartInfo;

        public ProcessHelper(string command, params string[] args)
        {
            _processStartInfo = new ProcessStartInfo(command);
            if ((args != null) && args.Any())
            {
                string cliArgs = String.Join(" ", args);
                _processStartInfo.Arguments = cliArgs;
            }

            _processStartInfo.CreateNoWindow = true;
            _processStartInfo.ErrorDialog = false;
            _processStartInfo.UseShellExecute = false;
            _processStartInfo.RedirectStandardOutput = true;
        }

        public int Launch(out string standarOutput)
        {
            Process process = new Process();
            process.StartInfo = _processStartInfo;
            process.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            int exitCode = process.ExitCode;
            //Output.WriteLine("Exit code (" + _processStartInfo.FileName + " " + _processStartInfo.Arguments + ") : " + exitCode);

            standarOutput = output;
            LastOutput = standarOutput;
            return exitCode;
        }

        public static int Launch(out string standarOutput, string command, params string[] args)
        {
            return new ProcessHelper(command, args).Launch(out standarOutput);
        }

        public static string LastOutput { get; private set; }

    }
}