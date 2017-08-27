namespace ODDL
{
    public static class Text
    {
        public static bool Compare(this string s1, string s2, int max)
        {
            for (int a = 0; ; a++)
            {
                if (--max < 0)
                {
                    break;
                }
                
                if (s1[a] != s2[a])
                {
                    return false;
                }

                if (s1[a] == 0)
                {
                    break;
                }
            }

            return true;
        }
    }
}