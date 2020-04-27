using System;

namespace SmartLogger
{
    public static class StringExtension
    {
        public static int IndexOf(this string word, Func<char, bool> predicate)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException(nameof(word));
            }

            int index = 0;
            foreach (char symbol in word)
            {
                if (predicate(symbol))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        public static int IndexOfNumberEndingPathBegging(this string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException(nameof(word));
            }

            int resultIndex = -1;
            for (int i = word.Length - 1; i > 0; i--)
            {
                if (!char.IsNumber(word[i]))
                {
                    return resultIndex;
                }

                resultIndex = i;
            }

            return resultIndex;
        }

        public static string GetSizeLetters(this string size, out string number)
        {
            if (string.IsNullOrEmpty(size))
            {
                throw new ArgumentNullException($"Value can not be null {nameof(size)}");
            }

            int indexOfFirstLetter = size.LastIndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }) + 1;
            number = size.Substring(0, indexOfFirstLetter);
            return size.Substring(indexOfFirstLetter, size.Length - indexOfFirstLetter);
        }
    }
}
