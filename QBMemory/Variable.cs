using System;

namespace QBasic.Memory
{
    public class Variable
    {
        private Block _block;
        private string _name;
        private uint _address;

        public Variable(Block block, string name, uint address)
        {
            _block = block;
            _name = name;
            _address = address;
        }

        public Int32 GetInt32()
        {
            return _block.GetInt32(_address);
        }

        public UInt32 GetUInt32()
        {
            return _block.GetUInt32(_address);
        }

        public Int16 GetInt16()
        {
            return _block.GetInt16(_address);
        }

        public UInt16 GetUInt16()
        {
            return _block.GetUInt16(_address);
        }

        public Single GetSingle()
        {
            return _block.GetSingle(_address);
        }

        public Double GetDouble()
        {
            return _block.GetDouble(_address);
        }

        public void Set<T>(T value)
        {
            _block.Set(_address, value);
        }
    }
}
