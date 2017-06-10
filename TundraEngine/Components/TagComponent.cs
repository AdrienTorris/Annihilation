using MessagePack;

namespace TundraEngine.Components
{
    [MessagePackObject]
    public class TagComponent
    {
        [Key(0)]
        public readonly string[] Tags;
    }
}