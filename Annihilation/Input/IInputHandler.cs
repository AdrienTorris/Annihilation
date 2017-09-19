namespace Annihilation.Input
{
    public interface IInputHandler
    {
        void OnKeyInput(ref KeyEvent keyEvent);
        void OnMouseButtonInput(ref MouseButtonEvent mouseButtonEvent);
        void OnMouseMoveInput(ref MouseMoveEvent mouseMoveEvent);
        void OnMouseWheelInput(ref MouseWheelEvent mouseWheelEvent);
        void OnTextInput(ref TextEvent textEvent);
    }
}