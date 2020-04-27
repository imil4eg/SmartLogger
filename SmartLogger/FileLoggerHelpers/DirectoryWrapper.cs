using System.Collections.Generic;
using System.IO;

namespace SmartLogger.FileLoggerHelpers
{
    internal interface IDirectoryWrapper
    {
        bool Exists(string directory);

        IEnumerable<string> EnumerateFiles(string directory, string searchPattern = "*",
            SearchOption searchOption = SearchOption.TopDirectoryOnly);
    }

    internal sealed class DirectoryWrapper : IDirectoryWrapper
    {
        public bool Exists(string directory)
        {
            return Directory.Exists(directory);
        }

        public IEnumerable<string> EnumerateFiles(string directory, string searchPattern = "*",
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException($"{directory} not found.");
            }

            foreach (var file in Directory.EnumerateFiles(directory, searchPattern, searchOption))
            {
                yield return file;
            }
        }
    }
}
