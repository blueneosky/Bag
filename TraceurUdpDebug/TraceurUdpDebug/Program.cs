using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace TraceurUdpDebug
{
    internal class Program
    {
        private static string _baseTitle;
        private static volatile bool _bufferActivated;
        private static bool _exitRequested;

        public static void Loop(UdpReceiver udpReceiver)
        {
            // Listen user keyboard input
            while (false == _exitRequested)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.Enter:
                            udpReceiver.Write(Environment.NewLine);
                            break;

                        case ConsoleKey.F12:
                            Console.Clear();
                            break;

                        case ConsoleKey.Escape:
                            _exitRequested = true;
                            break;

                        case ConsoleKey.Spacebar:
                            SwitchBuffer(udpReceiver);
                            Console.Title = _baseTitle + (_bufferActivated ? " - paused" : "");
                            break;

                        case ConsoleKey.F:
                            udpReceiver.Flush();
                            break;

                        default:
                            break;
                    }
                }

                System.Threading.Thread.Sleep(10);
            }
        }

        private static void Main(string[] args)
        {
            // log file fromm args
            Stream fileStream = null;
            if (args.Length > 0)
            {
                try
                {
                    string fileStreamPath = args[0];
                    fileStream = new FileStream(fileStreamPath, FileMode.Append, FileAccess.Write);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            _baseTitle = Console.Title;

#if DEBUG
            new Thread(TestFloodRandomTexte).Start();
#endif

            using (UdpReceiver receiver = new UdpReceiver(Console.Out, fileStream))
            {
                Loop(receiver);
            }

            if (fileStream != null)
                fileStream.Dispose();

            Console.Title = _baseTitle;
        }

        private static void TestFloodRandomTexte()
        {
            Random random = new Random();
            byte[] buffer = new byte[20];
            char[] bufferTexte = new char[20];
            while (false == _exitRequested)
            {
                random.NextBytes(buffer);
                for (int i = 0; i < bufferTexte.Length; i++)
                {
                    bufferTexte[i] = (char)(buffer[i] % (127 - 32) + 32);
                }
                string texte = new string(bufferTexte);
                System.Diagnostics.TraceurUdpDebug.Tracer(texte);
                Thread.Sleep(100);
            }
        }

        private static void SwitchBuffer(UdpReceiver udpReceiver)
        {
            _bufferActivated = !udpReceiver.Buffering;
            udpReceiver.Buffering = _bufferActivated;
        }
    }
}