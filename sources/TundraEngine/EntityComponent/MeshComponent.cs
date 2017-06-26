namespace TundraEngine.EntityComponent
{
    public struct MeshComponent
    {
        public StringId64 MeshResource;
        public StringId64 MaterialResource;

        public static readonly StringId32 Type = "Mesh";
    }
}