using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BatchGen.Common
{
    public class ProxyStreamReader : StreamReader
    {
        public ProxyStreamReader(Stream stream)
            : base(new ProxyStream(stream))
        {
        }
    }
}