using System.Runtime.CompilerServices;

namespace Engine.SDL
{
    public static class WindowFlagsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Has(this WindowFlags variable, WindowFlags flag)
        {
            return (variable & flag) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasNot(this WindowFlags variable, WindowFlags flag)
        {
            return (variable & flag) == 0;
        }
    }
}