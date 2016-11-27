using System;

namespace QBasic.Memory
{
    public class Block
    {
        private byte[] _memory;

        public Block(uint nBytes)
        {
            _memory = new byte[nBytes];
        }

        public uint Length { get { return (uint)_memory.Length; } }

        public Int32 GetInt32(uint address)
        {
            return BitConverter.ToInt32(_memory, (int)address);
        }

        public UInt32 GetUInt32(uint address)
        {
            return BitConverter.ToUInt32(_memory, (int)address);
        }

        public Int16 GetInt16(uint address)
        {
            return BitConverter.ToInt16(_memory, (int)address);
        }

        public UInt16 GetUInt16(uint address)
        {
            return BitConverter.ToUInt16(_memory, (int)address);
        }

        public Single GetSingle(uint address)
        {
            return BitConverter.ToSingle(_memory, (int)address);
        }

        public Double GetDouble(uint address)
        {
            return BitConverter.ToDouble(_memory, (int)address);
        }

        public Char GetChar(uint address)
        {
            return BitConverter.ToChar(_memory, (int)address);
        }

        public void Set<T>(uint address, T value)
        {
            byte[] bytes = BitConverter.GetBytes((dynamic)value);
            Array.Copy(bytes, 0, _memory, address, bytes.Length);
        }
    }
}
