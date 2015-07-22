using System;
using System.Collections.Generic;
using System.Linq;

namespace System.IO
{
    public static class MemoryStreamExtension
    {
        public static MemoryStream Clone(this MemoryStream stream)
        {
            MemoryStream result = new MemoryStream();

            stream.Seek(0, SeekOrigin.Begin);
            stream.WriteTo(result);

            return result;
        }

        public static bool SameContents(this MemoryStream x, MemoryStream y)
        {
            if (x == y)
                return true;

            if ((x == null) || (y == null))
                return false;

            x.Seek(0, SeekOrigin.Begin);
            y.Seek(0, SeekOrigin.Begin);

            const int ConstBufferSize = 1 << 12;  // 4k
            byte[] xBuffer = new byte[ConstBufferSize];
            byte[] yBuffer = new byte[ConstBufferSize];

            while (true)
            {
                int xLength = x.Read(xBuffer, 0, ConstBufferSize);
                int yLength = y.Read(yBuffer, 0, ConstBufferSize);
                if (xLength != yLength)
                    return false;

                int length = xLength;
                for (int i = 0; i < length; i++)
                {
                    if (xBuffer[i] != yBuffer[i])
                        return false;
                }

                if (length < ConstBufferSize)
                    return true;
            }
        }
    }
}