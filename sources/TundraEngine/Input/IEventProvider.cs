using System;

namespace TundraEngine.Input
{
    public interface IEventProvider : IDisposable
    {
        void PumpEvents(out InputEvent inputEvent);
    }
}