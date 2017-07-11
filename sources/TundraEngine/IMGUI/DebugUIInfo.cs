namespace TundraEngine.IMGUI
{
    public enum DebugUIType : byte
    {
        None,
        DearIMGUI,
    }

    public struct DebugUISettings
    {
        public DebugUIType DebugUIType;
    }
}