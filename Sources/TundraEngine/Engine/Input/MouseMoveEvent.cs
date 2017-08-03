using System;
using System.Numerics;

namespace Engine.Input
{
    public struct MouseMoveEvent
    {
        public Vector2 Position;
        public Vector2 DeltaPosition;
        public TimeSpan DeltaTime;
        public byte PlayerId;
    }
}