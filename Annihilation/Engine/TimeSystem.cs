using SDL2;

namespace Engine
{
    public static class TimeSystem
    {
        public static double SecondsSinceStart => SDL.GetTicks() / 1000f;
        public static float FrameRate => DeltaTime == 0f ? 0f : 1f / DeltaTime;

        public static float DeltaTime { get; private set; } = 1f / 144f;

        private static uint _lastTime;

        public static void Initialize()
        {

        }

        public static void Update()
        {
            uint time = SDL.GetTicks();
            DeltaTime = (time - _lastTime) / 1000f;
            _lastTime = time;
        }
    }
}