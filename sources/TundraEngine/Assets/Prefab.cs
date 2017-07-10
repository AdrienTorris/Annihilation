using System.Runtime.InteropServices;

namespace TundraEngine
{
    public struct ComponentTypeData
    {
        public StringHash32 Type;
        public uint NumInstances;
        public int Size;
        public uint[] EntityIndices;
        public byte[] Data;
    }

    unsafe public struct Prefab
    {
        public uint NumEntities;
        public uint NumComponentTypes;
        public uint[] ParentIndices;
        public ComponentTypeData[] Components;
    }
}