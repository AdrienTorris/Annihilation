using SDL2;

namespace Engine
{
    public class Timer
    {
        public double DeltaTime { get; private set; }
        public ulong CurrentTime { get; private set; }

        private ulong _previousTime;

        public Timer()
        {
            CurrentTime = SDL.GetPerformanceCounter();
            _previousTime = 0;
        }

        public void Update()
        {
            _previousTime = CurrentTime;
            CurrentTime = SDL.GetPerformanceCounter();
            DeltaTime = (CurrentTime - _previousTime) * 1000 / SDL.GetPerformanceFrequency();
        }
    }
}