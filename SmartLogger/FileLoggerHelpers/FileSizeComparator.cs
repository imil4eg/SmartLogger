using System;
using System.IO;

namespace SmartLogger.FileLoggerHelpers
{
    internal interface IFileSizeComparator
    {
        bool IsFileReachedSizeLimit(string path, string limitSize);
    }

    internal sealed class FileSizeComparator : IFileSizeComparator
    {
        private readonly ISizeCalculator _sizeCalculator;
        private readonly IFileWrapper _fileWrapper;

        public FileSizeComparator(ISizeCalculator sizeCalculator, IFileWrapper fileWrapper)
        {
            _sizeCalculator = sizeCalculator;
            _fileWrapper = fileWrapper;
        }

        public bool IsFileReachedSizeLimit(string path, string limitSize)
        {
            if (!_fileWrapper.Exists(path))
            {
                throw new FileNotFoundException(path);
            }

            if (string.IsNullOrEmpty(limitSize))
            {
                throw new ArgumentNullException(nameof(limitSize));
            }

            long limitSizeNum = _sizeCalculator.Calculate(limitSize);
            long fileSize = _fileWrapper.GetFileSize(path);

            return fileSize <= limitSizeNum;
        }
    }
}
