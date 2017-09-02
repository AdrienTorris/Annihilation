using System;

namespace Engine.Config
{
    [Flags]
    public enum ConfigVarFlags
    {
        None = 0,
        WriteToFile = 1 << 0,
    }
}