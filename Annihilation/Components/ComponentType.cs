using System;

namespace Engine.EntityComponent
{
    [Flags]
    public enum Component
    {
        None = 0,
        Transform = 1 << 0,
        Camera = 1 << 1,
        Light = 1 << 2,
        StaticMesh = 1 << 3,
        Script = 1 << 4,

    }
}