namespace Engine.Input
{
    public interface IInputHandler
    {
        void OnKeyInput(KeyEvent keyEvent);
        void OnMouseInput();
        void OnTextInput(string text);
    }
}