using System;
using System.Numerics;

namespace Engine.Input
{
    public struct MouseMoveEvent
    {
        public readonly Vector2 Position;
        public readonly Vector2 DeltaPosition;
        public readonly TimeSpan DeltaTime;

        public MouseMoveEvent(Vector2 position, Vector2 deltaPosition, TimeSpan deltaTime)
        {
            Position = position;
            DeltaPosition = deltaPosition;
            DeltaTime = deltaTime;
        }

        public override string ToString()
        {
            return $"{nameof(Position)}: {Position}, {nameof(DeltaPosition)}: {DeltaPosition}, {nameof(DeltaTime)}: {DeltaTime}";
        }
    }
}