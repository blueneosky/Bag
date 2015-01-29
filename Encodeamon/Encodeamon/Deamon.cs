using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Encodeamon
{
    internal class Deamon
    {
        private readonly string _watchedFolderPath;
        private readonly string _fileIn;
        private readonly string _fileOut;
        //private readonly Encoding _encodingIn;
        private readonly Encoding _encodingOut;
        private readonly FileSystemWatcher _fileSystemWatcher;

        public Deamon(/*Encoding encodingIn,*/ Encoding encodingOut, string fileIn, string fileOut)
        {
            // Path.GetFullPath() to ensure a good String.Equals
            _fileIn = Path.GetFullPath(fileIn);
            _fileOut = Path.GetFullPath(fileOut);
            //_encodingIn = encodingIn;
            _encodingOut = encodingOut;

            _watchedFolderPath = Path.GetDirectoryName(_fileIn);

            _fileSystemWatcher = new FileSystemWatcher(_watchedFolderPath);
            // reduce monitoring to LastWrite and CreationTime
            _fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
        }

        public void Start()
        {
            // run once
            Recode();

            // activate file watcher
            _fileSystemWatcher.EnableRaisingEvents = true;

            while (true)
            {
                var result = _fileSystemWatcher.WaitForChanged(WatcherChangeTypes.All);
                string fileName = result.Name;
                string filePath = Path.Combine(_watchedFolderPath, fileName);
                if (String.Equals(filePath, _fileIn))
                {
                    Recode();
                }
            }
        }

        private void Recode()
        {
            Thread.Sleep(100);  // sometime, the file is not yet ready

            try
            {
                string text;
                Encoding encoding;
                using (StreamReader streamReader = new StreamReader(_fileIn, true))
                {
                    text = streamReader.ReadToEnd();
                    encoding = streamReader.CurrentEncoding;
                    File.WriteAllText(_fileOut, text, _encodingOut);
                }

#if DEBUG
                Console.WriteLine("Convert file : {0} ({1})", text, encoding.EncodingName);
#else
                Console.Write(".");
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine("");
                Console.WriteLine("Error while converting file : {0}", e.Message);
            }
        }
    }
}