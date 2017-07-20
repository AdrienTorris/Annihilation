using System;

namespace TundraEngine.Windowing
{
    public interface IWindowProvider : IDisposable
    {
        uint Width { get; }
        uint Height { get; }
        WindowManagerInfo WindowManagerInfo { get; set; }
        int UndefinedPosition { get; }
    }
}