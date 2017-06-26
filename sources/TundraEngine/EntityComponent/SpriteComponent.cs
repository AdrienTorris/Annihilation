namespace TundraEngine.EntityComponent
{
    public struct SpriteComponentInfo
    {
        public StringId64 SpriteResource;
        public StringId64 MaterialResource;

        public static readonly StringId32 Type = "Sprite";
    }
}