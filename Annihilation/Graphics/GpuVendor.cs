using System.Runtime.InteropServices;

namespace Annihilation.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GpuVendor
    {
        public const uint Amd = 0x1002;
        public const uint Nvidia = 0x10DE;
        public const uint Intel = 0x8086;
        public const uint Arm = 0x13B5;
        public const uint Qualcomm = 0x5143;
        public const uint ImgTec = 0x1010;

        private readonly uint _id;

        public GpuVendor(uint id)
        {
            _id = id;
        }

        public override string ToString()
        {
            switch (_id)
            {
                case Amd: return "AMD";
                case Nvidia: return "Nvidia";
                case Intel: return "Intel";
                case Arm: return "ARM";
                case Qualcomm: return "Qualcomm";
                case ImgTec: return "Imagination Technologies";
                default: return "Unkwown";
            }
        }

        public static implicit operator uint(GpuVendor vendor) => vendor._id;
        public static implicit operator GpuVendor(uint id) => new GpuVendor(id);
    }
}