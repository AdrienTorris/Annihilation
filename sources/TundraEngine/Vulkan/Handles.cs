using System;

namespace Vulkan.Handle
{
    public struct Instance : IEquatable<Instance>
    {
        public IntPtr Handle;

        public readonly static Instance Null = new Instance();
        
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
            return Handle == other.Handle;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }

        public override string ToString()
        {
            return Handle.ToString();
        }
    }
    
    public struct PhysicalDevice : IEquatable<PhysicalDevice>
    {
        public IntPtr Handle;

        public readonly static PhysicalDevice Null = new PhysicalDevice();

        public override bool Equals(object obj)
        {
            return obj is PhysicalDevice && this == (PhysicalDevice)obj;
        }

        public static bool operator ==(PhysicalDevice left, PhysicalDevice right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PhysicalDevice left, PhysicalDevice right)
        {
            return !left.Equals(right);
        }

        public bool Equals(PhysicalDevice other)
        {
            return Handle == other.Handle;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }

        public override string ToString()
        {
            return Handle.ToString();
        }
    }

    public struct Device : IEquatable<Device>
    {
        public IntPtr Handle;

        public readonly static Device Null = new Device();

        public override bool Equals(object obj)
        {
            return obj is Device && this == (Device)obj;
        }

        public static bool operator ==(Device left, Device right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Device left, Device right)
        {
            return !left.Equals(right);
        }

        public bool Equals(Device other)
        {
            return Handle == other.Handle;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }

        public override string ToString()
        {
            return Handle.ToString();
        }
    }

    public struct Queue : IEquatable<Queue>
    {
        public IntPtr Handle;

        public readonly static Queue Null = new Queue();

        public override bool Equals(object obj)
        {
            return obj is Queue && this == (Queue)obj;
        }

        public static bool operator ==(Queue left, Queue right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Queue left, Queue right)
        {
            return !left.Equals(right);
        }

        public bool Equals(Queue other)
        {
            return Handle == other.Handle;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }

        public override string ToString()
        {
            return Handle.ToString();
        }
    }

    public struct CommandBuffer : IEquatable<CommandBuffer>
    {
        public IntPtr Handle;

        public readonly static CommandBuffer Null = new CommandBuffer();

        public override bool Equals(object obj)
        {
            return obj is CommandBuffer && this == (CommandBuffer)obj;
        }

        public static bool operator ==(CommandBuffer left, CommandBuffer right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CommandBuffer left, CommandBuffer right)
        {
            return !left.Equals(right);
        }

        public bool Equals(CommandBuffer other)
        {
            return Handle == other.Handle;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }

        public override string ToString()
        {
            return Handle.ToString();
        }
    }

    //
    // Non dispatchable handles
    //
    public struct Semaphore
    {
        public ulong Handle;
    }

    public struct Fence
    {
        public ulong Handle;
    }

    public struct DeviceMemory
    {
        public ulong Handle;
    }

    public struct Buffer
    {
        public ulong Handle;
    }

    public struct Image
    {
        public ulong Handle;
    }

    public struct Event
    {
        public ulong Handle;
    }

    public struct QueryPool
    {
        public ulong Handle;
    }

    public struct BufferView
    {
        public ulong Handle;
    }

    public struct ImageView
    {
        public ulong Handle;
    }

    public struct ShaderModule
    {
        public ulong Handle;
    }

    public struct PipelineCache
    {
        public ulong Handle;
    }

    public struct PipelineLayout
    {
        public ulong Handle;
    }

    public struct RenderPass
    {
        public ulong Handle;
    }

    public struct Pipeline
    {
        public ulong Handle;
    }

    public struct DescriptorSetLayout
    {
        public ulong Handle;
    }

    public struct Sampler
    {
        public ulong Handle;
    }

    public struct DescriptorPool
    {
        public ulong Handle;
    }

    public struct DescriptorSet
    {
        public ulong Handle;
    }

    public struct Framebuffer
    {
        public ulong Handle;
    }

    public struct CommandPool
    {
        public ulong Handle;
    }

    //
    // Khronos
    //
    public struct Surface
    {
        public ulong Handle;
    }
    
    public struct Swapchain
    {
        public ulong Handle;
    }
    
    public struct Display
    {
        public ulong Handle;
    }

    public struct DisplayMode
    {
        public ulong Handle;
    }
    
    public struct DescriptorUpdateTemplate
    {
        public ulong Handle;
    }

    //
    // Multi-vendor
    //
    public struct DebugReportCallback
    {
        public ulong Handle;
    }

    //
    // Nvidia X
    //
    public struct ObjectTable
    {
        public ulong Handle;
    }

    public struct IndirectCommandsLayout
    {
        public ulong Handle;
    }
}