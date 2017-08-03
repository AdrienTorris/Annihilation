namespace Engine.EntityComponent
{
    public struct SpriteComponentInfo
    {
        public StringHash64 SpriteResource;
        public StringHash64 MaterialResource;

        public static readonly StringHash32 Type = "Sprite";
    }
}