using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.IO.Ports;
public static class SerialPortExtensions
{
    public static void WriteLines(this SerialPort serialPort, IEnumerable<string> commands)
    {
        foreach (var command in commands)
        {
            serialPort.WriteLine(command);
        }
    }
}
