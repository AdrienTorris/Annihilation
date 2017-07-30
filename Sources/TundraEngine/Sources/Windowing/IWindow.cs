using System;

namespace TundraEngine.Windowing
{
    public interface IWindow : IDisposable
    {
        WindowManagerInfo WindowManagerInfo { get; set; }
    }
}