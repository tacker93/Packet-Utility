using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Packet_Utility.Packet
{
    public class PacketWriter : AbstractPacket
    {
        private readonly BinaryWriter _binaryWriter;

        public PacketWriter(int size = 64)
        {
            MemoryStream = new MemoryStream(size);
            _binaryWriter = new BinaryWriter(MemoryStream, Encoding.ASCII);
        }

        public void WriteHexString(string hex)
        {
            hex = hex.Replace(" ", "");
            byte[] array = Enumerable.Range(0, hex.Length)
                .Where(x => x%2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
            _binaryWriter.Write(array);
        }

        public void WriteUInt(uint value = 0)
        {
            _binaryWriter.Write(value);
        }

        public void WriteBytes(byte[] value)
        {
            _binaryWriter.Write(value);
        }

        public void WriteSByte(sbyte value = 0)
        {
            _binaryWriter.Write(value);
        }

        public void WriteByte(byte value = 0)
        {
            _binaryWriter.Write(value);
        }

        public void WriteBool(bool value = false)
        {
            _binaryWriter.Write(value);
        }

        public void WriteShort(short value = 0)
        {
            _binaryWriter.Write(value);
        }

        public void WriteUShort(ushort value = 0)
        {
            _binaryWriter.Write(value);
        }

        public void WriteInt(int value = 0)
        {
            _binaryWriter.Write(value);
        }

        public void WriteLong(long value = 0)
        {
            _binaryWriter.Write(value);
        }

        public void WriteULong(ulong value = 0)
        {
            _binaryWriter.Write(value);
        }

        public void WriteString(string value)
        {
            foreach (char t in value)
                _binaryWriter.Write(t);
        }

        public void WritePaddedString(string value, int length)
        {
            for (int i = 0; i < length; i++)
                if (i < value.Length)
                    _binaryWriter.Write(value[i]);
                else
                    WriteByte();
        }

        public void WriteMapleString(string text)
        {
            if (text == null) text = String.Empty;

            WriteShort((short) text.Length);
            WriteString(text);
        }

        public void WriteZero(int count)
        {
            for (int i = 0; i < count; i++)
                WriteByte();
        }

        public override void Dispose()
        {
            _binaryWriter.Dispose();
            GC.SuppressFinalize(this);
        }

        internal void WriteReversedLong(long value)
        {
            WriteByte((byte) ((value >> 32) & 0xFF));
            WriteByte((byte) ((value >> 40) & 0xFF));
            WriteByte((byte) ((value >> 48) & 0xFF));
            WriteByte((byte) ((value >> 56) & 0xFF));
            WriteByte((byte) ((value & 0xFF)));
            WriteByte((byte) ((value >> 8) & 0xFF));
            WriteByte((byte) ((value >> 16) & 0xFF));
            WriteByte((byte) ((value >> 24) & 0xFF));
        }
    }
}