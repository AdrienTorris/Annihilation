using ZeroFormatter;

namespace TundraEngine
{
    [ZeroFormattable]
    public struct EntityResource : IResource
    {
        [Index(0)]
        public StringId32[] Components;

        public StringId32 Name { get; }
        public byte[] Bytes { get; }
    }
}