using System;

namespace Engine.Components
{
    public enum CameraProjectionMode : byte
    {
        Perspective,
        Orthographic
    }

    public struct CameraComponent
    {
        private static readonly Name _componentType;
        public static Name ComponentType => _componentType == Name.Null ? new Name("Camera") : _componentType;

        public CameraProjectionMode ProjectionType;
        public float VerticalFOV;
        public float NearPlane;
        public float FarPlane;

        public const float DefaultAspectRatio = 16.0f / 9.0f;
        public const float DefaultOrthographicSize = 10.0f;
        public const float DefaultVerticalFieldOfView = 45.0f;
        public const float DefaultNearClipPlane = 0.1f;
        public const float DefaultFarClipPlane = 1000.0f;
    }
}