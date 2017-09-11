using Engine;
using Engine.Input;
using Engine.Graphics;

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
                //if (InputSystem.IsPressed(button)) Log.Info("Pressing " + button);
                if (InputSystem.WasPressed(button)) Log.Info("Pressed " + button);
                if (InputSystem.WasReleased(button)) Log.Info("Released " + button);

                float mouseWheel = InputSystem.GetTimeCorrectedAnalogInput(Analog.MouseWheel);
                if (mouseWheel != 0f) Log.Info(mouseWheel.ToString("0.0000"));
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
