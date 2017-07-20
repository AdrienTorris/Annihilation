﻿using System;
using System.Threading.Tasks;

namespace TundraEngine.Rendering
{
    public interface IRenderer : IDisposable
    {
        void Render();
    }
}