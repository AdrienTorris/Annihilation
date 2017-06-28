using System;
using System.Threading.Tasks;

using TundraEngine.Windowing;

namespace TundraEngine.Rendering
{
    public interface IRenderer : IDisposable
    {
        void Initialize(ref ApplicationInfo applicationInfo, IWindow window);
        Task RenderAsync();
    }
}