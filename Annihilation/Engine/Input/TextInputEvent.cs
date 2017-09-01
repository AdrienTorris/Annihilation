namespace Engine.Input
{
    public enum TextEventType : byte
    {
        Submitted,
        Typing
    }

    public struct TextEvent
    {
        public string Text;
        public TextEventType Type;
        public ushort TypingStart;
        public ushort TypingLength;
    }
}