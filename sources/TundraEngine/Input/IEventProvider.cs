﻿using System;

namespace TundraEngine.Input
{
    public interface IEventProvider : IDisposable
    {
        void PollEvents();
    }
}