using SDL2;
using static SDL2.SDL;

namespace Engine
{
    public static class Log
    {
        public static void Info(string text)
        {
            Native.SDL_Log(text);
        }

        public static void Warning(string text)
        {
            Native.SDL_LogWarn(LogCategory.Application, text);
        }

        public static void Error(string text)
        {
            Native.SDL_LogError(LogCategory.Application, text);
        }

        public static unsafe void SetOutputFunction(LogOutputFunction callback)
        {
            Native.SDL_LogSetOutputFunction(callback, null);
        }
    }
}