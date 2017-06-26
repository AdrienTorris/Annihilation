using TundraEngine.Mathematics;

namespace TundraEngine.EntityComponent
{
    public struct Transform3DComponent
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public static readonly StringId32 Type = "Transform3D";
    }
}