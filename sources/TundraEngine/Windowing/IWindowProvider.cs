using System;

namespace TundraEngine.Windowing
{
    public interface IWindowProvider
    {
        int UndefinedPosition { get; }

        IntPtr CreateWindow(ref WindowInfo windowInfo);
        void DestroyWindow(IntPtr window);
        void GetWindowManagerInfo(IntPtr window, out WindowManagerInfo windowManagerInfo);
    }
}