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

            // show encoding file
            if (args.Any(arg => arg == "-s"))
                ShowEncodingThenExit(args.Where(arg => arg != "-s"));

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
            Encoding encodingOut = TryGetEncodingByEncodingName(encodingNameOut);
            //if (encodingIn == null)
            //    ShowHelpThenExit(String.Format("Input encoding '{0}' is not a valid encoding", encodingNameIn));
            if (encodingOut == null)
                ShowHelpThenExit(String.Format("Output encoding '{0}' is not a valid encoding", encodingNameOut));

            Deamon deamon = new Deamon(/*encodingIn,*/ encodingOut, fileIn, fileOut);
            deamon.Start();
        }

        private static void ShowEncodingThenExit(System.Collections.Generic.IEnumerable<string> argsWithoutFlag)
        {
            string fileName = argsWithoutFlag.FirstOrDefault();
            if (false == File.Exists(fileName))
                ShowHelpThenExit(String.Format("File '{0}' is not valide", fileName));

            Encoding encoding = TryGetEncodingFromFileName(fileName);
            if (encoding == null)
            {
                Console.WriteLine("Invalide or unrecognised encoding.");
            }
            else
            {
                ShowEncoding(encoding);
            }
            throw new ExitException("(encoding)");
        }

        private static void ShowEncoding(EncodingInfo encodingInfo)
        {
            ShowEncoding(encodingInfo.GetEncoding());
        }

        private static void ShowEncoding(Encoding encoding)
        {
            Console.WriteLine("{0} [{1}]", encoding.WebName, encoding.EncodingName);
        }

        private static Encoding TryGetEncodingByEncodingName(string encodingName)
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

        private static Encoding TryGetEncodingFromFileName(string fileName)
        {
            Encoding encoding;

            try
            {
                using (StreamReader streamReader = new StreamReader(fileName, true))
                {
                    streamReader.ReadToEnd();
                    encoding = streamReader.CurrentEncoding;
                }
            }
            catch (Exception)
            {
                encoding = null;
            }

            return encoding;
        }

        private static void ShowEncodingsThenExit()
        {
            var encodingInfos = Encoding.GetEncodings();
            foreach (var encodingInfo in encodingInfos)
            {
                ShowEncoding(encodingInfo);
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
            Console.WriteLine("Usage :");
            Console.WriteLine("  {0} <encoding_out> <file_in> <file_out>", name);
            Console.WriteLine("  {0} -l", name);
            Console.WriteLine("  {0} -s <file>", name);
            Console.WriteLine("  {0} -h -help", name);
            Console.WriteLine("    <encoding_out> : one of the available encoding");
            Console.WriteLine("    <file>         : file to examine");
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