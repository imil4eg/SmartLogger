using System;

namespace SmartLogger.FileLoggerHelpers
{
    internal interface ISizeCalculator
    {
        long Calculate(string size);
    }

    internal sealed class FileSizeCalculator : ISizeCalculator
    {
        public long Calculate(string size)
        {
            if (string.IsNullOrEmpty(size))
            {
                throw new ArgumentNullException($"Value can not be null {nameof(size)}");
            }

            string number;
            string sizeLetters = size.GetSizeLetters(out number).ToUpperInvariant();

            switch (sizeLetters)
            {
                case SizeTypes.Byte:
                    return long.Parse(number);
                case SizeTypes.KiloByte:
                    return long.Parse(number) * 1024;
                case SizeTypes.MegaByte:
                    return long.Parse(number) * 2048;
                case SizeTypes.GigaByte:
                    return long.Parse(number) * 4072;
                default:
                    throw new NotSupportedException($"The {sizeLetters} data type not supported.");
            }
        }

        private static class SizeTypes
        {
            public const string Byte = "B";
            public const string KiloByte = "KB";
            public const string MegaByte = "MB";
            public const string GigaByte = "GB";
        }
    }
}
