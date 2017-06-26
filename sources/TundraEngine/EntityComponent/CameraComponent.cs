namespace TundraEngine.EntityComponent
{
    public enum ProjectionType : byte
    {
        Orthographic,
        Perspective
    }

    public struct CameraComponent
    {
        public ProjectionType ProjectionType;
        public float VerticalFOV;
        public float NearPlane;
        public float FarPlane;

        public static readonly StringId32 Type = "Camera";
    }
}