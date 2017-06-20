using System;

namespace TundraEngine
{
    public interface IComponent
    {
        StringId32 Type { get; }
    }
}