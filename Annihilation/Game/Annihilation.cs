using System.Diagnostics;
using Annihilation;
using Annihilation.Core;
using Annihilation.Input;
using Annihilation.Graphics;

namespace Client
{
    public class Annihilation : Game
    {
        public override string Title => "Annihilation";
        public override string Organization => "iLLOGIKA";

        protected override void Startup()
        {
            
        }

        protected override void Update(float deltaTime)
        {
            for(int i = 0; i < (int)Button.Count; ++i)
            {
                Button button = (Button)i;
                if (InputSystem.WasPressed(button)) Trace.TraceInformation("Pressed " + button);
                if (InputSystem.WasReleased(button)) Trace.TraceInformation("Released " + button);
            }
        }

        protected override void RenderScene()
        {
        }

        protected override void RenderUI(GraphicsContext graphicsContext)
        {
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
