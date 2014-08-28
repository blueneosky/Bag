using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BatchGen.Common
{
    public class ProxyStream : Stream
    {
        private Stream _stream;

        public ProxyStream(Stream stream)
        {
            _stream = stream;
        }

        #region Stream

        public override bool CanRead
        {
            get { return _stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _stream.CanWrite; }
        }

        public override long Length
        {
            get { return _stream.Length; }
        }

        public override long Position
        {
            get { return _stream.Position; }
            set { _stream.Position = value; }
        }

        public override void Flush()
        {
            // DO NOTHING
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        public override void Close()
        {
            // DO NOTHING
        }

        #endregion Stream

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            // DO NOTHING
            // _stream is not under the responsability of ProxyStream

            base.Dispose(disposing);
        }

        #endregion IDisposable
    }
}