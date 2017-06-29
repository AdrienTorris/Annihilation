using System;

namespace TundraEngine.Windowing
{
    public interface IWindow : IDisposable
    {
        IntPtr Window { get; set; }
        WindowManagerInfo WindowManagerInfo { get; set; }
        int UndefinedPosition { get; }
    }
}