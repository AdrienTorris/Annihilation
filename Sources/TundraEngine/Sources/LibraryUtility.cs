using static TundraEngine.SDL.SDL;

namespace TundraEngine
{
    public static class LibraryUtility
    {
        internal static void InitializeSDL()
        {
            {
                bool result = SDL_SetHint(HintFrameBufferAcceleration, "1");
                Assert.IsTrue(result, "Unable to set hint \"" + HintFrameBufferAcceleration + "\"");
            }
            {
                // BUG: Initializing more than video crashes
                int result = SDL_Init(SDL_InitFlags.Video);
                Assert.IsZero(result, "Unable to init SDL");
            }
        }
    }
}