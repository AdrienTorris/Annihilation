using TundraEngine.Mathematics;
using MessagePack;

namespace TundraEngine.Components
{
    [MessagePackObject]
    public struct MeshComponent
    {
        [Key (0)] public readonly Vector3[] Vertices;
    }
}