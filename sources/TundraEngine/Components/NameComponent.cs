using System;
using MessagePack;

namespace TundraEngine.Components
{
    [MessagePackObject]
    public struct NameComponent : IComponent
    {
        [Key (0)]
        public readonly string Name;

        public Guid Guid => new Guid ("5268e62a-c918-4940-b327-6081f7f4466e");
    }
}