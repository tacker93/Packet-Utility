using System;
using System.IO;

namespace Packet_Utility.Packet
{
    public abstract class AbstractPacket : IDisposable
    {
        protected MemoryStream MemoryStream;

        protected long Position
        {
            get { return MemoryStream.Position; }
        }

        public long Length
        {
            get { return MemoryStream.Length; }
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected byte[] ToArray()
        {
            return MemoryStream.ToArray();
        }

        public override string ToString()
        {
            return BitConverter.ToString(ToArray()).Replace("-", " ");
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (MemoryStream == null) return;

            MemoryStream.Dispose();
            MemoryStream = null;
        }

        public static byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace(" ", "").Replace("-", "");

            int length = hex.Length;
            var buffer = new byte[length/2];
            for (int i = 0; i < length; i += 2)
                buffer[i/2] = Convert.ToByte(hex.Substring(i, 2), 0x10);

            return buffer;
        }
    }
}