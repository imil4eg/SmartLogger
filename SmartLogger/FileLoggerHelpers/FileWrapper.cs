using System;
using System.IO;

namespace SmartLogger.FileLoggerHelpers
{
    internal interface IFileWrapper
    {
        long GetFileSize(string path);

        bool Exists(string path);
    }

    internal sealed class FileWrapper : IFileWrapper
    {
        public long GetFileSize(string path)
        {
            if (!Exists(path))
            {
                throw new FileNotFoundException($"File {path} not found.");
            }

            return new FileInfo(path).Length;
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}
