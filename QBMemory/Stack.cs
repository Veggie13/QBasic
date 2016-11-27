namespace QBasic.Memory
{
    public class Stack
    {
        private uint _top = 0;

        public Stack(Block block)
        {
            Block = block;
        }

        public Block Block { get; private set; }

        public uint Push16()
        {
            return PushBytes(2);
        }

        public uint Push32()
        {
            return PushBytes(4);
        }

        public uint Push64()
        {
            return PushBytes(8);
        }

        public uint PushBytes(uint nBytes)
        {
            _top += nBytes;
            if (_top >= Block.Length)
            {
                throw new OutOfStackException();
            }
            return (_top - nBytes);
        }
    }
}
