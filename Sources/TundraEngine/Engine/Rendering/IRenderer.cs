using System;
using System.Threading.Tasks;

namespace Engine.Rendering
{
    public interface IRenderer : IDisposable
    {
        void Render();
    }
}