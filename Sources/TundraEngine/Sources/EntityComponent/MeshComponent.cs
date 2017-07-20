namespace TundraEngine.EntityComponent
{
    public struct MeshComponent
    {
        public StringHash64 MeshResource;
        public StringHash64 MaterialResource;

        public static readonly StringHash32 Type = "Mesh";
    }
}