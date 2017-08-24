using System;
using System.Collections.Generic;

namespace Engine.Input
{
    public interface IEventProvider : IDisposable
    {
        void PollEvents(out List<InputEvent> inputEvents);

    }
}