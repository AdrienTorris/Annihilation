namespace TundraEngine.EntityComponent
{
    unsafe public struct AnimationComponent
    {
        public StringId64 AnimationFSMResource;

        public static readonly StringId32 Type = "Animation";
        public static readonly int Size = sizeof(StringId64);
    }
}