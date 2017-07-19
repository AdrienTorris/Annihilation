using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TundraEngine.Rendering
{
    [Flags]
    public enum InstanceCreateFlags : uint
    {
        None = 0
    }

    public struct ApplicationInfo
    {
        public StructureType Type;
        public IntPtr Next;
        public IntPtr ApplicationName;
        public Version ApplicationVersion;
        public IntPtr EngineName;
        public Version EngineVersion;
        public Version ApiVersion;
    }

    public struct InstanceCreateInfo
    {
        public StructureType Type;
        public IntPtr Next;
        public InstanceCreateFlags Flags;
        public IntPtr ApplicationInfo;
        public uint EnabledLayerCount;
        public IntPtr EnabledLayerNames;
        public uint EnabledExtensionCount;
        public IntPtr EnabledExtensionNames;
    }

    public struct Instance : IEquatable<Instance>
    {
        public readonly static Instance Null = new Instance();

        internal IntPtr NativeHandle;

        public unsafe void Destroy(AllocationCallbacks* allocator = null)
        {
            vkDestroyInstance(this, allocator);
        }
        
        [DllImport(Vulkan.LibraryName), SuppressUnmanagedCodeSecurity]
        internal static extern unsafe void vkDestroyInstance(Instance instance, AllocationCallbacks* allocator);
        
        public override bool Equals(object obj)
        {
            return obj is Instance && this == (Instance)obj;
        }

        public static bool operator ==(Instance left, Instance right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Instance left, Instance right)
        {
            return !left.Equals(right);
        }

        public bool Equals(Instance other)
        {
            return NativeHandle == other.NativeHandle;
        }

        public override int GetHashCode()
        {
            return NativeHandle.GetHashCode();
        }

        public override string ToString()
        {
            return NativeHandle.ToString();
        }
    }
}