using System;
using System.IO;
using System.Text;

namespace Packet_Utility.Packet
{
    public sealed class PacketReader : AbstractPacket
    {
        private readonly BinaryReader _binaryReader;

        public PacketReader(byte[] packet)
        {
            MemoryStream = new MemoryStream(packet, false);
            _binaryReader = new BinaryReader(MemoryStream, Encoding.ASCII);
        }

        public PacketReader(byte[] packet, int index, int count)
        {
            MemoryStream = new MemoryStream(packet, index, count, false, false);
            _binaryReader = new BinaryReader(MemoryStream, Encoding.ASCII);
        }

        public int Remaining
        {
            get { return (int) (ToArray().Length - Position); }
        }

        public byte[] ReadBytes(int count)
        {
            return _binaryReader.ReadBytes(count);
        }

        public byte ReadByte()
        {
            return _binaryReader.ReadByte();
        }

        public bool ReadBool()
        {
            return _binaryReader.ReadBoolean();
        }

        public short ReadShort()
        {
            return _binaryReader.ReadInt16();
        }

        public ushort ReadUShort()
        {
            return _binaryReader.ReadUInt16();
        }

        public int ReadInt()
        {
            return _binaryReader.ReadInt32();
        }

        public long ReadLong()
        {
            return _binaryReader.ReadInt64();
        }

        public string ReadString(int length)
        {
            return new string(_binaryReader.ReadChars(length));
        }

        public string ReadMapleString()
        {
            return ReadString(ReadShort());
        }

        public override void Dispose()
        {
            _binaryReader.Dispose();
        }

        private void CheckLength(int length)
        {
            if (Position + length > ToArray().Length || length < 0)
                throw new Exception("Not enough space");
        }
    }
}