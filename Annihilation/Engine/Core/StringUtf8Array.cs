namespace Engine
{
    public unsafe struct StringUtf8Array
    {
        public byte** ArrayPtr;

        public StringUtf8Array(uint size)
        {
            ArrayPtr = (byte**)Memory.AllocatePointers((int)size);
        }

        public StringUtf8 this[int index]
        {
            get => new StringUtf8(*(ArrayPtr + index));
            set => *(ArrayPtr + index) = value;
        }

        public StringUtf8 this[uint index]
        {
            get => new StringUtf8(*(ArrayPtr + index));
            set => *(ArrayPtr + index) = value;
        }
    }
}