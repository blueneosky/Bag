using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Encodeamon
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                Process(args);
            }
            catch (ExitException e)
            {
#if DEBUG
                Console.WriteLine("");
                Console.WriteLine("[i] Exit message {0}", e.Message);
#endif
                DebugWait();
                return e.ExitCode;
            }
            catch (Exception e)
            {
                Console.WriteLine("[!] Unexpected error : {0}", e);
                DebugWait();
                return -1;
            }

            return 0;
        }

        private static void Process(string[] args)
        {
            // help
            if (args.Any(arg => arg == "--help" || arg == "-h"))
                ShowHelpThenExit();

            // encoding's listing
            if (args.Any(arg => arg == "-l"))
                ShowEncodingsThenExit();

            //*** test input ***
            if (args.Count() > 3)//4)
                ShowHelpThenExit("Too many argumets");
            if (args.Count() < 3)//4)
                ShowHelpThenExit("Too few arguments, or not recognised");

            int i = 0;
            //string encodingNameIn = args[i++];
            string encodingNameOut = args[i++];
            string fileIn = args[i++];
            string fileOut = args[i++];

            if (false == File.Exists(fileIn))
                ShowHelpThenExit(String.Format("Input file '{0}' do not exist", fileIn));

            //Encoding encodingIn = TryGetEncoding(encodingNameIn);
            Encoding encodingOut = TryGetEncoding(encodingNameOut);
            //if (encodingIn == null)
            //    ShowHelpThenExit(String.Format("Input encoding '{0}' is not a valid encoding", encodingNameIn));
            if (encodingOut == null)
                ShowHelpThenExit(String.Format("Output encoding '{0}' is not a valid encoding", encodingNameOut));

            Deamon deamon = new Deamon(/*encodingIn,*/ encodingOut, fileIn, fileOut);
            deamon.Start();

        }

        private static Encoding TryGetEncoding(string encodingName)
        {
            try
            {
                return Encoding.GetEncoding(encodingName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static void ShowEncodingsThenExit()
        {
            var encodings = Encoding.GetEncodings();
            foreach (var encoding in encodings)
            {
                Console.WriteLine("{0} : {1}", encoding.Name, encoding.DisplayName);
            }
            throw new ExitException("(encodings)");
        }

        private static void ShowHelpThenExit(string errorMessage = null)
        {
            bool withErrorMessage = false == String.IsNullOrEmpty(errorMessage);
            if (withErrorMessage)
            {
                Console.WriteLine("[!] {0}", errorMessage);
                Console.WriteLine("");
            }

            string name = Assembly.GetExecutingAssembly().GetName().Name;
            Console.WriteLine("Copy content of file into an other file with specific encoding.");
            //Console.WriteLine("Get content file from specific encoding and write into an other file encoding.");
            Console.WriteLine("Usage :");
            //Console.WriteLine("  {0} <encoding_in> <encoding_out> <file_in> <file_out>", name);
            Console.WriteLine("  {0} <encoding_out> <file_in> <file_out>", name);
            Console.WriteLine("  {0} -l", name);
            Console.WriteLine("  {0} -h -help", name);
            //Console.WriteLine("    <encoding_in>  : one of the available encoding");
            Console.WriteLine("    <encoding_out> : one of the available encoding");
            Console.WriteLine("    <file_in>      : file monitored and re-encode");
            Console.WriteLine("    <file_out>     : file with result");
            Console.WriteLine("    -l --list      : list available encoding");
            Console.WriteLine("    -h --help      : this help");

            string message = String.Format("({0})", withErrorMessage ? errorMessage : "help");
            int exitCode = withErrorMessage ? -1 : 0;
            throw new ExitException(message, exitCode);
        }

        private static void DebugWait()
        {
#if DEBUG
            // add "press any key to continue"
            Console.WriteLine("press any key to continue");
            Console.ReadKey(true);
#endif
        }
    }
}