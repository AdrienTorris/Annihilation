using System;

namespace TundraEngine
{
    [Flags]
    public enum WorldFlags
    {
        Nothing = 0,
        EnableSound = 1 << 0,
        Enable3D = 1 << 1,
        Enable2D = 1 << 2,
        EnablePhysics = 1 << 3,
        EnableTimeOfDay = 1 << 4,
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