using MessagePack;

namespace TundraEngine.Components
{
    [MessagePackObject]
    public class NameComponent
    {
        [Key(0)]
        public readonly string Name;
    }
}