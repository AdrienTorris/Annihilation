namespace Engine.Input
{
    public struct MouseWheelEvent
    {
        public readonly float WheelDelta;

        public MouseWheelEvent(float wheelDelta)
        {
            WheelDelta = wheelDelta;
        }

        public override string ToString()
        {
            return $"{nameof(WheelDelta)}: {WheelDelta}";
        }
    }
}