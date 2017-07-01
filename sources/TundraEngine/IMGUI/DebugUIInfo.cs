namespace TundraEngine.IMGUI
{
    public enum DebugUIType : byte
    {
        None,
        Vulkan,
        BGFX,
        DearIMGUI,
    }

    public struct DebugUISettings
    {
        public DebugUIType DebugUIType;
    }
}