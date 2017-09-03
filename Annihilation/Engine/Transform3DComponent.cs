using System.Numerics;

namespace Engine.EntityComponent
{
    public struct Transform3DComponent
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        //public static readonly StringHash32 Type = "Transform3D";
    }
}