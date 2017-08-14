namespace Engine.Rendering
{
    public enum Result : int
    {
        Success = 0,
        NotReady = 1,
        Timeout = 2,
        EventSet = 3,
        EventReset = 4,
        Incomplete = 5,
        ErrorOutOfHostMemory = -1,
        ErrorOutOfDeviceMemory = -2,
        ErrorInitializationFailed = -3,
        ErrorDeviceLost = -4,
        ErrorMemoryMapFailed = -5,
        ErrorLayerNotPresent = -6,
        ErrorExtensionNotPresent = -7,
        ErrorFeatureNotPresent = -8,
        ErrorIncompatibleDriver = -9,
        ErrorTooManyObjects = -10,
        ErrorFormatNotSupported = -11,
        ErrorFragmentedPool = -12,
        ErrorSurfaceLost = -1000000000,
        ErrorNativeWindowInUse = -1000000001,
        Suboptimal = 1000001003,
        ErrorOutOfDate = -1000001004,
        ErrorIncompatibleDisplay = -1000003001,
        ErrorValidationFailed = -1000011001,
        ErrorInvalidShader = -1000012000,
        ErrorOutOfPoolMemory = -1000069000,
        ErrorInvalidExternalHandle = -1000072003,
    }

    public static class ResultExtensions
    {
        public static void CheckError(this Result result)
        {
            if (result < 0)
            {
                throw new VulkanException(result);
            }
        }
    }
}