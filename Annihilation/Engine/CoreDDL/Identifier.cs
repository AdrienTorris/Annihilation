namespace ODDL
{
    public class Identifier
    {
        private readonly string _identifier;

        public Identifier(string identifier)
        {
            _identifier = identifier;
        }

        public static implicit operator string(Identifier id)
        {
            return id?._identifier;
        }
    }
}