using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BatchGen.Common;

namespace BatchGen.Common
{
    public static class ExtensionStream
    {
        public static void WriteTo(this Stream source, Stream dest)
        {
            WriteTo(source, dest, 0x400);
        }

        public static void WriteTo(this Stream source, Stream dest, int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            int size;
            while ((size = source.Read(buffer, 0, bufferSize)) > 0)
            {
                dest.Write(buffer, 0, size);
            }
        }

        public static void WriteTo(this StreamReader source, Stream dest)
        {
            WriteTo(source, dest, 0x400);
        }

        public static void WriteTo(this StreamReader source, Stream dest, int bufferSize)
        {
            char[] buffer = new char[bufferSize];
            int size;
            using (StreamWriter writer = new ProxyStreamWriter(dest))
            {
                while ((size = source.ReadBlock(buffer, 0, bufferSize)) > 0)
                {
                    writer.Write(buffer, 0, size);
                }
            }
        }
    }
}