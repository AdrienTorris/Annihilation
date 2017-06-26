using TundraEngine.Mathematics;

namespace TundraEngine.EntityComponent
{
    public struct Transform2DComponent
    {
        public Vector2 Position;
        public Angle Rotation;
        public Vector2 Scale;

        public static readonly StringId32 Type = "Transform2D"; 
    }
}