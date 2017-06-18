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
        Everything = ~0
    }
}