using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BatchGen.Common;
using BatchGen.Gen;
using FastBuildGen.BatchNode;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.Xml;
using FastBuildGen.Xml.Entity;

namespace FastBuildGen.File
{
    internal class FBFile : IDisposable
    {
        #region Factory

        public static FBFile Read(string fileName)
        {
            FBFile result = new FBFile(fileName, FileMode.Open);
            result.Read();  // note : throw formated exception

            return result;
        }

        public static void Write(string fileName, FBModel fbModel)
        {
            using (FBFile file = new FBFile(fileName, FileMode.Create))
            {
                file.Write(fbModel);
            }
        }

        #endregion Factory

        private const string ConstBatchRem = ":: ";
        private const string ConstBatchRemCaution = " (do not delete/modify this line)";
        private const string ConstBatchRemHeaderText = ConstBatchRem + ConstHeaderText + ConstBatchRemCaution;
        private const string ConstBatchRemSplitterConfigText = ConstBatchRem + ConstSplitterConfigText + ConstBatchRemCaution;
        private const string ConstHeaderText = "<<FastBuild auto generated file>>";
        private const string ConstSplitterConfigText = "<<FastBuild Configuration>>";

        private Stream _batchCodeStream;
        private Stream _xmlConfigStream;
        private string _fileName;
        private FileMode _fileMode;

        public FBFile(string fileName, FileMode fileMode)
        {
            _fileName = fileName;
            _fileMode = fileMode;
        }

        public Stream BatchCodeStream
        {
            get
            {
                if (_batchCodeStream == null)
                    throw new FastBuildGenException("BatchCodeStream not initialized");

                return new ProxyStream(_batchCodeStream);
            }
        }

        public XmlFastBuild XmlConfig
        {
            get { return GetXmlConfig(); }
        }

        public Stream XmlConfigStream
        {
            get
            {
                if (_batchCodeStream == null)
                    throw new FastBuildGenException("XmlConfigStream not initialized");

                return new ProxyStream(_xmlConfigStream);
            }
        }

        public XmlFastBuild GetXmlConfig()
        {
            XmlFastBuild result = XmlService.ReadXmlFastBuild(XmlConfigStream);

            return result;
        }

        public void Read()
        {
            try
            {
                ReadCore();
            }
            catch (FBFileException)
            {
                ResetDataStream();
                throw;  // rethrow
            }
            catch (Exception e)
            {
                ResetDataStream();
                throw new FBFileException("Read failed", e);
            }
        }

        public void Write(FBModel fbModel)
        {
            try
            {
                WriteCore(fbModel);
            }
            catch (FBFileException)
            {
                ResetDataStream();
                throw;  // rethrow
            }
            catch (Exception e)
            {
                ResetDataStream();
                throw new FBFileException("Write failed", e);
            }
        }

        #region Core

        private void ReadCore()
        {
            ResetDataStream();

            _batchCodeStream = new MemoryStream();
            _xmlConfigStream = new MemoryStream();

            using (FileStream fileStream = new FileStream(_fileName, _fileMode))
            using (StreamReader reader = new ProxyStreamReader(fileStream))
            {
                // read header (and check)
                string header = reader.ReadLine();
                if (false == header.Contains(ConstHeaderText))
                    throw new WrongFBFileException("Unexpected header");

                // read batch code
                string line = null;
                using (StreamWriter writer = new ProxyStreamWriter(_batchCodeStream))
                {
                    line = reader.ReadLine();
                    while ((line != null) && (false == line.Contains(ConstSplitterConfigText)))
                    {
                        writer.WriteLine(line);
                        line = reader.ReadLine();
                    }
                }
                _batchCodeStream.Seek(0, SeekOrigin.Begin);

                if (line == null)
                    throw new WrongFBFileException("Config section not found");

                reader.WriteTo(_xmlConfigStream);
                _xmlConfigStream.Seek(0, SeekOrigin.Begin);
            }
        }

        private void ResetDataStream()
        {
            if (_batchCodeStream != null)
            {
                _batchCodeStream.Dispose();
                _batchCodeStream = null;
            }

            if (_xmlConfigStream != null)
            {
                _xmlConfigStream.Dispose();
                _xmlConfigStream = null;
            }
        }

        private void WriteCore(FBModel fbModel)
        {
            ResetDataStream();

            // batch code
            _batchCodeStream = new MemoryStream();
            FastBuildBatchFile file = new FastBuildBatchFile(fbModel);
            BatchGenerator generator = new BatchGenerator(file, _batchCodeStream);
            generator.Write();
            _batchCodeStream.Seek(0, SeekOrigin.Begin);

            // config data
            _xmlConfigStream = new MemoryStream();
            XmlService.Write(_xmlConfigStream, fbModel);
            _xmlConfigStream.Seek(0, SeekOrigin.Begin);

            // Note : ProxyStream don't forward Flush() to its inner stream
            using (Stream stream = new MemoryStream())
            {
                using (StreamWriter writer = new ProxyStreamWriter(stream))
                {
                    writer.AutoFlush = true;    // easier

                    // header
                    writer.WriteLine(ConstBatchRemHeaderText);

                    // batch code
                    _batchCodeStream.WriteTo(stream);

                    // splitter between code and data
                    writer.WriteLine();
                    writer.WriteLine(ConstBatchRemSplitterConfigText);

                    // config data
                    _xmlConfigStream.WriteTo(stream);
                }
                using (FileStream fileStream = new FileStream(_fileName, _fileMode))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
            }
        }

        #endregion Core

        #region IDisposable

        private bool _isDisposed;

        ~FBFile()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            try
            {
                if (disposing)
                {
                    try
                    {
                        ResetDataStream();
                    }
                    finally { }
                }
            }
            finally
            {
                _isDisposed = true;
            }
        }

        #endregion IDisposable
    }
}