using System;
using System.Collections.Generic;
using System.Threading;
using Engine.Collections;

namespace Engine.Input
{
    public static class InputEventPool
    {
        private static ThreadLocal<Pool<InputEvent>> _pool;

        static InputEventPool()
        {
            _pool = new ThreadLocal<Pool>(() => new Pool());
        }
    }
}