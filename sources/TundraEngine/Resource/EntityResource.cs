using System.Runtime.InteropServices;

namespace TundraEngine.Resource
{
    public struct ComponentTypeData
    {
        public StringId32 Type;
        public uint NumInstances;
        public uint Size;
        public uint[] EntityIndices;
        public byte[] Data;
    }

    unsafe public struct EntityResource
    {
        public uint NumEntities;
        public uint NumComponentTypes;
        public uint[] ParentIndices;
        public ComponentTypeData[] Components;
    }
}