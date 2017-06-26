using System.Runtime.InteropServices;

namespace TundraEngine.Input
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ButtonBinding // Size: 6 bytes
    {
        public Button Button;
        public ModifierKeys Modifiers;
        public StringId32 Action;

        public ButtonBinding(Button button, StringId32 action)
        {
            Button = button;
            Modifiers = ModifierKeys.None;
            Action = action;
        }

        public ButtonBinding(Button button, ModifierKeys modifiers, StringId32 action)
        {
            Button = button;
            Modifiers = modifiers;
            Action = action;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AxisBinding // Size: 5 bytes
    {
        public Axis Axis;
        public StringId32 Action;

        public AxisBinding(Axis axis, StringId32 action)
        {
            Axis = axis;
            Action = action;
        }
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct InputBindings
    {
        public StringId32 Name;
        public ButtonBinding[] ButtonBindings;
        public AxisBinding[] AxisBindings;

        public static InputBindings Default => new InputBindings
        {
            Name = "Default Bindings".ToHash32(),
            ButtonBindings = new ButtonBinding[]
            {
            }
        };
    }
}