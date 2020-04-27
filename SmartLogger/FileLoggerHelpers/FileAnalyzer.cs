using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using SmartLogger.FileLoggerHelpers;

namespace SmartLogger
{
    internal interface IFileAnalyzer
    {
        string LogPath { get; set; }
        string FileName { get; set; }
        string FileSizeLimit { get; set; }

        string GetLogFile();
        string GetLastesFileWithSameName();
        IReadOnlyDictionary<int, string> GetSortedByNumberInFileName();
    }

    internal sealed class FileAnalyzer : IFileAnalyzer
    {
        private readonly IFileSizeComparator _fileSizeComparator;
        private readonly IFileNameCreator _fileNameCreator;
        private readonly IDirectoryWrapper _directoryWrapper;

        private string _logPath;
        public string LogPath
        { 
            get
            {
                if (string.IsNullOrEmpty(_logPath))
                {
                    throw new InvalidOperationException();
                }

                return _logPath;
            }
            set
            { 
                if (!_directoryWrapper.Exists(value))
                {
                    throw new DirectoryNotFoundException($"Directory {value} not found.");
                }

                _logPath = value;
            } 
        }

        private string _fileName;
        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_fileName))
                {
                    throw new InvalidOperationException();
                }

                return _fileName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException($"{value} is null or empty.");
                }

                _fileName = value;
            }
        }

        private string _fileSizeLimit;
        public string FileSizeLimit 
        {
            get 
            {
                if (string.IsNullOrEmpty(_fileSizeLimit))
                {
                    throw new InvalidOperationException();
                }

                return _fileSizeLimit;
            }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException($"File size limit is null or empty.");
                }

                _fileSizeLimit = value;
            } 
        }

        public FileAnalyzer(IFileSizeComparator fileSizeComparator, IFileNameCreator fileNameCreator, 
            IDirectoryWrapper directoryWrapper)
        {
            _fileSizeComparator = fileSizeComparator;
            _fileNameCreator = fileNameCreator;
            _directoryWrapper = directoryWrapper;
        }

        public string GetLogFile()
        {
            string latestFileWithSameName = GetLastesFileWithSameName();

            if (string.IsNullOrEmpty(latestFileWithSameName) ||
                FileSizeLimit.Equals("Unlimit"))
            {
                return Path.Combine(LogPath, FileName);
            }

            string filePath = Path.Combine(LogPath, latestFileWithSameName);
            bool isLimitReached = _fileSizeComparator.IsFileReachedSizeLimit(filePath, FileSizeLimit);

            if (!isLimitReached)
            {
                return filePath;
            }

            return _fileNameCreator.CreateUniqueFileName(filePath);
        }

        public string GetLastesFileWithSameName()
        {
            var fileNamesSorted = GetSortedByNumberInFileName().ToArray();

            for (int i = fileNamesSorted.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(fileNamesSorted[i].Value))
                {
                    return fileNamesSorted[i].Value;
                }
            }

            return "";
        }

        public IReadOnlyDictionary<int, string> GetSortedByNumberInFileName()
        {
            var fileNamesSorted = new SortedList<int, string>();

            string searchPattern = Path.GetFileNameWithoutExtension(FileName) + "*.txt";
            foreach (var file in _directoryWrapper.EnumerateFiles(LogPath, searchPattern, SearchOption.TopDirectoryOnly))
            {
                string name = Path.GetFileNameWithoutExtension(file);
                int indexOfNum = name.IndexOfNumberEndingPathBegging();

                if (indexOfNum < 0)
                {
                    fileNamesSorted.Add(0, file);
                    continue;
                }

                int fileNum = int.Parse(name.Substring(indexOfNum, name.Length - indexOfNum));

                fileNamesSorted.Add(fileNum, file);
            }

            return fileNamesSorted;
        }
    }
}
