using System;
using System.Runtime.InteropServices;

namespace Engine.SDL
{
    [Flags]
    public enum MessageBoxFlags : uint
    {
        Error = 1 << 4,
        Warning = 1 << 5,
        Information = 1 << 6
    }

    [Flags]
    public enum MessageBoxButtonFlags : uint
    {
        ReturnKeyDefault = 1 << 0,
        EscapeKeyDefault = 1 << 1
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MessageBoxButtonData
    {
        public MessageBoxButtonFlags Flags;
        public int ButtonID;
        public Text Text;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MessageBoxColor
    {
        public byte R;
        public byte G;
        public byte B;
    }

    public enum MessageBoxColorType
    {
        Background,
        Text,
        ButtonBorder,
        ButtonBackground,
        ButtonSelected,
        Max
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MessageBoxColorScheme
    {
        public MessageBoxColor BackgroundColor;
        public MessageBoxColor TextColor;
        public MessageBoxColor ButtonBorderColor;
        public MessageBoxColor ButtonBackgroundColor;
        public MessageBoxColor ButtonSelectedColor;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MessageBoxData
    {
        public MessageBoxFlags Flags;
        public Window Window;
        public Text Title;
        public Text Message;
        public int NumButtons;
        public MessageBoxButtonData* Buttons;
        public MessageBoxColorScheme* ColorScheme;
    }
}