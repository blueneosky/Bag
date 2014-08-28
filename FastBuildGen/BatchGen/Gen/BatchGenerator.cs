using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BatchGen.BatchNode;
using BatchGen.Common;

namespace BatchGen.Gen
{
    public class BatchGenerator
    {
        private readonly BatchNodeBase _root;
        private readonly Stream _stream;

        public BatchGenerator(BatchFileBase root, Stream stream)
        {
            _root = root;
            _stream = stream;
        }

        public void Write()
        {
            using (BatchCmdGenVisitor visitor = new BatchCmdGenVisitor(_stream))
            {
                _root.Accept(visitor);
            }
        }

        public static string GetText(BatchFileBase root)
        {
            string text = null;

            using (MemoryStream stream = new MemoryStream())
            {
                BatchGenerator generator = new BatchGenerator(root, stream);
                generator.Write();
                stream.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new ProxyStreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
            }

            return text;
        }
    }
}