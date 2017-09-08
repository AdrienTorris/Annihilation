namespace Engine
{
    public unsafe struct StringUtf8Array
    {
        public byte** ArrayPtr;

        public StringUtf8Array(uint size)
        {
            ArrayPtr = (byte**)Memory.AllocatePointers((int)size);
        }

        public byte* this[int index]
        {
            get => *(ArrayPtr + index);
            set => *(ArrayPtr + index) = value;
        }

        public byte* this[uint index]
        {
            get => *(ArrayPtr + index);
            set => *(ArrayPtr + index) = value;
        }
    }
}