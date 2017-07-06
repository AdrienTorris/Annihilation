﻿using System;

namespace TundraEngine
{
    public struct GameLoopData<T>
    {
        public T Data;
        public Action Initialize;
        public Action ProcessInput;
        public Action Update;
        public Action Draw;
        public Action Terminate;
    }
}