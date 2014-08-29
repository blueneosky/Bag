using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TraceurUdpDebug
{
    internal class UdpReceiver : IDisposable
    {
        private const int ConstantePortEcoute = 9990;
        private static Encoding ConstEncoding = Encoding.Unicode;

        private readonly TextWriter _console;
        private readonly Stream _output;
        private readonly object _writingLock = new object();
        private MemoryStream _buffer;
        private bool _buffering;
        private Thread _thread;
        private UdpClient _udpClient;
        private IPEndPoint _localEp;

        public UdpReceiver(TextWriter console, Stream output)
        {
            _console = console ?? TextWriter.Null;
            _output = new BufferedStream(output ?? Stream.Null);

            _localEp = new IPEndPoint(IPAddress.Any, ConstantePortEcoute);
            _udpClient = new UdpClient(ConstantePortEcoute);

            _thread = new Thread(Listening);
            _thread.Start();
        }

        #region Listening

        private void Listening()
        {
            try
            {
                ListeningCore();
            }
            catch (Exception)
            {
                // exit from dispose
                _console.WriteLine("[UDP: end of reception]");
                return;
            }
        }

        private void ListeningCore()
        {
            while (true)
            {
                while (_udpClient.Available > 0)
                {
                    byte[] buffer = _udpClient.Receive(ref _localEp);
                    int count = buffer.Length;
                    if (count > 0)
                    {
                        Write(buffer, count);
                    }
                }
                while (_udpClient.Available == 0)
                    Thread.Sleep(10);
            }
        }

        #endregion Listening

        #region Writing with buffering

        public bool Buffering
        {
            get { return _buffering; }
            set
            {
                lock (_writingLock)
                {
                    if (_buffering == value)
                        return;

                    _buffering = value;
                    if (_buffering)
                    {
                        if (_buffer != null)
                            _buffer.Dispose();
                        _buffer = new MemoryStream();
                        _buffer.Seek(0, SeekOrigin.Begin);
                    }
                    else
                    {
                        const int ConstBufferSize = 2 << 14;
                        byte[] buffer = new byte[ConstBufferSize];
                        _buffer.Seek(0, SeekOrigin.Begin);
                        int count;
                        while ((count = _buffer.Read(buffer, 0, ConstBufferSize)) > 0)
                        {
                            Write(buffer, count);
                        }
                    }
                }
            }
        }

        public void Flush()
        {
            lock (_writingLock)
            {
                if ((_console != null) && (_output != null))
                {
                    if (Buffering)
                    {
                        Buffering = false;
                        Buffering = true;
                    }
                }

                if (_console != null)
                    _console.Flush();
                if (_output != null)
                    _output.Flush();
            }
        }

        public void Write(string text)
        {
            byte[] buffer = ConstEncoding.GetBytes(text);
            Write(buffer, buffer.Length);
        }

        private void Write(byte[] buffer, int count)
        {
            lock (_writingLock)
            {
                if (_buffering)
                {
                    _buffer.Write(buffer, 0, count);
                }
                else
                {
                    string text = ConstEncoding.GetString(buffer, 0, count);
                    _console.Write(text);
                    _output.Write(buffer, 0, count);
                }
            }
        }

        #endregion Writing with buffering

        #region IDisposable

        private bool _isDisposed;

        ~UdpReceiver()
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
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (Buffering)
                    {
                        Buffering = false;
                        if (_buffer != null)
                            _buffer.Dispose();
                    }

                    if (_udpClient != null)
                    {
                        _udpClient.Close();
                    }

                    if (_thread != null)
                    {
                        _thread.Interrupt();
                        _thread.Join(10000);
                        _thread = null;
                    }

                    _udpClient = null;

                    Flush();

                    _isDisposed = true;
                }
            }
        }

        #endregion IDisposable
    }
}