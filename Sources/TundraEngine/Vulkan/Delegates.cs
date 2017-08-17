using System.Runtime.InteropServices;

namespace Vulkan
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void* AllocationFunction(void* userData, Size size, Size alignment, SystemAllocationScope allocationScope);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void* ReallocationFunction(void* userData, void* original, Size size, Size alignment, SystemAllocationScope allocationScope);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void FreeFunction(void* userData, void* memory);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void InternalAllocationNotification(void* userData, Size size, InternalAllocationType allocationType, SystemAllocationScope allocationScope);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void InternalFreeNotification(void* userData, Size size, InternalAllocationType allocationType, SystemAllocationScope allocationScope);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void VoidFunction();

    //
    // EXT
    //
    public unsafe delegate Bool32 DebugReportCallbackDelegate(DebugReportFlags flags, DebugReportObjectType objectType, ulong objectHandle, ulong location, int messageCode, Text layerPrefix, Text message, void* userData);
}