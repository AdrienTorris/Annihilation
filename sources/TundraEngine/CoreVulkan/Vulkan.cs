using System;
using System.IO;
using System.Security;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CoreVulkan
{
    public static class Vulkan
    {
        public readonly static GetInstanceProcAddr GetInstanceProcAddr;

        private static IntPtr _library;
        
        static Vulkan()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _library = Win32.LoadLibrary("vulkan-1.dll");
                if (_library == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Could not load Vulkan library");
                }
                IntPtr getProcAddr = Win32.GetProcAddress(_library, "PFN_vkGetInstanceProcAddr");
                if (getProcAddr == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Could not load Vulkan loading function");
                }
                GetInstanceProcAddr = Marshal.GetDelegateForFunctionPointer<GetInstanceProcAddr>(getProcAddr);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Linux.dlerror();
                _library = Linux.dlopen("libvulkan.so.1", Linux.RTLD_NOW);
                if (_library == IntPtr.Zero && !Path.IsPathRooted("libvulkan.so.1"))
                {
                    string localPath = Path.Combine(AppContext.BaseDirectory, "libvulkan.so.1");
                    _library = Linux.dlopen(localPath, Linux.RTLD_NOW);
                }
                if (_library == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Could not load Vulkan library");
                }
                IntPtr getProcAddr = Linux.dlsym(_library, "PFN_vkGetInstanceProcAddr");
                if (getProcAddr == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Could not load Vulkan loading function");
                }
                GetInstanceProcAddr = Marshal.GetDelegateForFunctionPointer<GetInstanceProcAddr>(getProcAddr);
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        //
        // Methods
        //
        public unsafe static T LoadGlobalFunction<T>() => LoadInstanceFunction<T>(Instance.Null);

        public unsafe static T LoadInstanceFunction<T>(Instance instance)
        {
            string name = "vk" + typeof(T).Name;
            IntPtr function = GetInstanceProcAddr(instance, name);
            if (function == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }
            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }

        public unsafe static T LoadInstanceFunctionFromExtension<T>(Instance instance, Text extension, Text[] enabledExtensions)
        {
            foreach (Text enabledExtension in enabledExtensions)
            {
                if (enabledExtension == extension)
                {
                    return LoadInstanceFunction<T>(instance);
                }
            }
            return default(T);
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
        
        // Khronos
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
        
        // Multi-vendor
        public struct DebugReportCallback
        {
            public ulong Handle;
        }
        
        // Nvidia
        public struct ObjectTable
        {
            public ulong Handle;
        }

        public struct IndirectCommandsLayout
        {
            public ulong Handle;
        }

        //
        // Platforms
        //
        [SuppressUnmanagedCodeSecurity]
        private static class Win32
        {
            [DllImport("kernel32")]
            public static extern IntPtr LoadLibrary(string fileName);

            [DllImport("kernel32")]
            public static extern IntPtr GetProcAddress(IntPtr module, string procName);

            [DllImport("kernel32")]
            public static extern int FreeLibrary(IntPtr module);
        }

        [SuppressUnmanagedCodeSecurity]
        private static class Linux
        {
            public const int RTLD_NOW = 0x002;

            [DllImport("libdl.so")]
            public static extern IntPtr dlopen(string fileName, int flags);

            [DllImport("libdl.so")]
            public static extern IntPtr dlsym(IntPtr handle, string name);

            [DllImport("libdl.so")]
            public static extern int dlclose(IntPtr handle);

            [DllImport("libdl.so")]
            public static extern string dlerror();
        }
    }
}