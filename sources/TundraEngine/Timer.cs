using static TundraEngine.SDL.SDL;

namespace TundraEngine
{
    public class Timer
    {
        public double DeltaTime { get; private set; }
        public ulong CurrentTime { get; private set; }

        private ulong _previousTime;

        public Timer()
        {
            CurrentTime = SDL_GetPerformanceCounter ();
            _previousTime = 0;
        }

        public void Update ()
        {
            _previousTime = CurrentTime;
            CurrentTime = SDL_GetPerformanceCounter ();
            DeltaTime = (CurrentTime - _previousTime) * 1000 / SDL_GetPerformanceFrequency ();
        }
    }
}