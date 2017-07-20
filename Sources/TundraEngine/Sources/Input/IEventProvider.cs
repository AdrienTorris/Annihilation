using System;
using System.Collections.Generic;

namespace TundraEngine.Input
{
    public interface IEventProvider : IDisposable
    {
        void PollEvents(out List<InputEvent> inputEvents);

    }
}