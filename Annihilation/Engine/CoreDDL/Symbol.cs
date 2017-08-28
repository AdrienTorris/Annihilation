namespace OpenDDL
{
    public static class Symbol
    {
        public const char OpenBracketToken = '{';
        public const char CloseBracketToken = '}';

        public const char OpenPropertyToken = '(';
        public const char ClosePropertyToken = ')';

        public const char OpenArrayToken = '[';
        public const char CloseArrayToken = ']';

        public const char CommaSeparator = ',';
        public const char EqualSign = '=';

        public const char SingleQuote = '\'';
        public const char DoubleQuote = '"';

        public const char EscapeChar = '\\';

        public const char GlobalName = '$';
        public const char LocalName = '%';

        public const char UnicodeChar4 = 'u';
        public const char UnicodeChar6 = 'U';

        public const char CommentDash = '/';
        public const char CommentStar = '*';

        public const string BoolTrue = "true";
        public const string BoolFalse = "false";
    }
}