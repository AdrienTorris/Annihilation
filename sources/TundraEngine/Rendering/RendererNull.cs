using System.Threading.Tasks;

namespace TundraEngine.Rendering
{
    public class RendererNull : IRenderer
    {
        public void Dispose()
        {

        }

        public Task RenderAsync()
        {
            return null;
        }
    }
}