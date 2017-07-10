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

        public static readonly StringHash32 Type = "Camera";

        public const float DefaultAspectRatio = 16f / 9f;
        public const float DefaultOrthographicSize = 10f;
        public const float DefaultVerticalFieldOfView = 45f;
        public const float DefaultNearClipPlane = 0.1f;
        public const float DefaultFarClipPlace = 100f;
    }
}