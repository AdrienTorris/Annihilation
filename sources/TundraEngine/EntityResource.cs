using System;
using ZeroFormatter;

namespace TundraEngine
{
    [ZeroFormattable]
    public struct EntityResource : IResource
    {
        [Index(0)]
        public Guid[] Components;

        public StringId32 Name { get; }
        public byte[] Bytes { get; }
    }
}