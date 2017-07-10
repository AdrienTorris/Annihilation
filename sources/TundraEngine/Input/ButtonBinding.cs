using System.Runtime.InteropServices;

namespace TundraEngine.Input
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ButtonBinding
    {
        public Button Button;
        public ModifierKeys Modifiers;
        public StringHash32 Action;

        public ButtonBinding(Button button, StringHash32 action)
        {
            Button = button;
            Modifiers = ModifierKeys.None;
            Action = action;
        }

        public ButtonBinding(Button button, ModifierKeys modifiers, StringHash32 action)
        {
            Button = button;
            Modifiers = modifiers;
            Action = action;
        }
    }
}