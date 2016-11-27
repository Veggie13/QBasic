using System.Collections.Generic;

namespace QBasic.Memory
{
    public class Heap
    {
        private struct Allocation
        {
            public uint Position;
            public uint Length;
        }

        private Block _block;
        private uint _bottom;
        private List<Allocation> _allocations = new List<Allocation>();

        public Heap(Block block)
        {
            _block = block;
            _bottom = _block.Length;
        }

        public uint New(uint length)
        {
            if (length > _bottom)
            {
                throw new OutOfHeapException();
            }

            _bottom -= length;
            Allocation alloc = new Allocation()
            {
                Position = _bottom,
                Length = length
            };

            return _bottom;
        }
    }
}
