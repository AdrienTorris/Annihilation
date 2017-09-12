using System;

namespace Engine
{
    public static class Utility
    {
        public static int Pow(int number, int power)
        {
            int result = number;
            while (power-- > 0)
            {
                result *= result;
            }
            return result;
        }

        public static int GetDigitCount(int number)
        {
            if (Math.Abs(number) < 10) return 1;

            number = Math.Abs(number);
            int digitCount = 1;
            while ((number /= 10) >= 1)
            {
                digitCount++;
            }
            return digitCount;
        }

        public static (int, int) GetDigitCount(float number)
        {
            int numberInt = (int)number;
            int digitCount = GetDigitCount(numberInt);

            // Count decimal digits
            int decimalCount = 0;
            float diff = number - numberInt;
            while (Math.Abs(diff) >= float.Epsilon)
            {
                diff = diff * 10;
                decimalCount++;
                diff = diff - (int)diff;
            }
            return (digitCount, decimalCount);
        }

        public static unsafe char* ToChars(int number)
        {
            bool isNegative = number < 0;
            int charCount = GetDigitCount(number) + (isNegative ? 1 : 0);

            char* valueString = Memory.AllocateChars(charCount);

            if (isNegative) *valueString = '-';
            
            for (int i = 0; i < charCount; i++)
            {
                int place = charCount - i;
                int digit = number / (Pow(10, place));

                *(valueString + i + (isNegative ? 1 : 0)) = (char)(digit + '0');
            }

            return valueString;
        }

        public static unsafe char* ToChars(float number)
        {
            bool isNegative = number < 0;
            (int, int) counts = GetDigitCount(number);
            int charCount = counts.Item1 + counts.Item2 + (isNegative ? 1 : 0);

            char* valueString = Memory.AllocateChars(charCount);

            if (isNegative) *valueString = '-';

            int numberInt = (int)number;
            int i = 0;
            for (i = 0; i < counts.Item1; i++)
            {
                int place = charCount - i;
                int digit = numberInt / (Pow(10, place));

                *(valueString + i + (isNegative ? 1 : 0)) = (char)(digit + '0');
            }

            float diff = number - (int)number;
            numberInt = (int)(diff * Pow(10, counts.Item2));
            for (int j = 0; j < counts.Item2; j++)
            {
                int place = charCount - j;
                int digit = numberInt / (Pow(10, place));

                *(valueString + i + j + (isNegative ? 1 : 0)) = (char)(digit + '0');
            }

            return valueString;
        }
    }
}