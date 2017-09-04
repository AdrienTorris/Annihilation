namespace Engine
{
    public delegate void CommandFunction();

    public unsafe class Command
    {
        public Command Next;
        public char* Name;
        public CommandFunction Function;
    }
}