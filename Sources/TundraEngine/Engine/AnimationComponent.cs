namespace Engine.EntityComponent
{
    unsafe public struct AnimationComponent
    {
        public StringHash64 AnimationFSMResource;

        public static readonly StringHash32 Type = "Animation";
        public static readonly int Size = sizeof(StringHash64);
    }
}