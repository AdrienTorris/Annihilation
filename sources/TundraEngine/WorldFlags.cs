using System;

namespace TundraEngine
{
    [Flags]
    public enum WorldFlags
    {
        Nothing = 0,
        EnableSound = 1 << 0,
        EnableRendering = 1 << 1,
        EnablePhysics = 1 << 2,
        EnableTimeOfDay = 1 << 3,
        Everything = ~0
    }

    internal static class WorldFlagsExtensions
    {
        public static bool Has (this WorldFlags variable, WorldFlags flag)
        {
            return (variable & flag) != 0;
        }

        public static bool HasNot (this WorldFlags variable, WorldFlags flag)
        {
            return (variable & flag) == 0;
        }
    }
}