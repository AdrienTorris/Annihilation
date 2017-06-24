namespace TundraEngine.Input
{
    public interface IEventProvider
    {
        void PumpEvents(out InputEvent inputEvent);
    }
}