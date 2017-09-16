using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Components
{
    public enum CameraProjectionMode : byte
    {
        Perspective,
        Orthographic
    }

    public struct CameraComponent
    {
        public const float DefaultAspectRatio = 16.0f / 9.0f;
        public const float DefaultOrthographicSize = 10.0f;
        public const float DefaultVerticalFieldOfView = 45.0f;
        public const float DefaultNearClipPlane = 0.1f;
        public const float DefaultFarClipPlane = 1000.0f;
    }
}