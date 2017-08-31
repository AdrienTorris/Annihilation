namespace Engine.Input
{
    public interface IInputHandler
    {
        void OnKeyInput();
        void OnMouseInput();
        void OnTextInput(string text);
    }
}