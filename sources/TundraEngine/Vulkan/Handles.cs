using System;

namespace Vulkan
{
    //
    // Types
    //
    public struct Bool32 : IEquatable<Bool32>
    {
        private readonly int boolValue;

        public Bool32(bool boolValue)
        {
            this.boolValue = boolValue ? 1 : 0;
        }

        public Bool32(int boolValue)
        {
            this.boolValue = boolValue;
        }

        public bool Equals(Bool32 other)
        {
            return boolValue == other.boolValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Bool32 && Equals((Bool32)obj);
        }

        public override int GetHashCode()
        {
            return boolValue;
        }

        public static bool operator ==(Bool32 left, Bool32 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Bool32 left, Bool32 right)
        {
            return !left.Equals(right);
        }

        public static implicit operator bool(Bool32 booleanValue)
        {
            return booleanValue.boolValue != 0;
        }

        public static implicit operator Bool32(bool boolValue)
        {
            return new Bool32(boolValue);
        }

        public override string ToString()
        {
            return string.Format("{0}", boolValue != 0);
        }

        public static implicit operator int(Bool32 booleanValue)
        {
            return booleanValue.boolValue;
        }

        public static implicit operator Bool32(int boolValue)
        {
            return new Bool32(boolValue);
        }
    }

    public struct DeviceSize : IEquatable<DeviceSize>
    {
        private readonly ulong value;

        public DeviceSize(ulong value)
        {
            this.value = value;
        }

        public bool Equals(DeviceSize other)
        {
            return value == other.value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is DeviceSize && Equals((DeviceSize)obj);
        }

        public override int GetHashCode()
        {
            return (int)value;
        }

        public static bool operator ==(DeviceSize left, DeviceSize right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DeviceSize left, DeviceSize right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static implicit operator ulong(DeviceSize deviceSize)
        {
            return deviceSize.value;
        }

        public static implicit operator DeviceSize(ulong value)
        {
            return new DeviceSize(value);
        }
    }

    public struct SampleMask : IEquatable<SampleMask>
    {
        private readonly uint value;

        public SampleMask(uint value)
        {
            this.value = value;
        }

        public bool Equals(SampleMask other)
        {
            return value == other.value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is SampleMask && Equals((SampleMask)obj);
        }

        public override int GetHashCode()
        {
            return (int)value;
        }

        public static bool operator ==(SampleMask left, SampleMask right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SampleMask left, SampleMask right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static implicit operator uint(SampleMask SampleMask)
        {
            return SampleMask.value;
        }

        public static implicit operator SampleMask(uint value)
        {
            return new SampleMask(value);
        }
    }

    //
    // Handles
    //
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
    // KHR
    //
    public struct Surface
    {
        public ulong Handle;
    }
}