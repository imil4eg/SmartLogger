using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartLogger;
using SmartLogger.FileLoggerHelpers;
using System;
using System.IO;

namespace SmartLoggerTests
{
    [TestClass]
    public sealed class FileAnalyzerTests
    {
        [TestMethod]
        public void GetLogFileName_NoFileWithSameName_SameFileNameWithoutNumber()
        {
            // Arrange
            string path = "testpath";
            string fileName = "somefilename";
            string expectedFileName = Path.Combine(path, fileName + ".txt");

            var fileSizeComparatorMock = new Mock<IFileSizeComparator>();
            var fileNameCreatorMock = new Mock<IFileNameCreator>();
            var directoryWrapperMock = new Mock<IDirectoryWrapper>();

            directoryWrapperMock.Setup(d => d.Exists(path))
                            .Returns(true);
            directoryWrapperMock.Setup(d => d.EnumerateFiles(path, fileName + "*txt", System.IO.SearchOption.TopDirectoryOnly))
                            .Returns(Array.Empty<string>());

            var fileAnalyzer = new FileAnalyzer(fileSizeComparatorMock.Object, fileNameCreatorMock.Object, directoryWrapperMock.Object)
            {
                LogPath = path,
                FileName = fileName + ".txt"
            };

            // Act
            string result = fileAnalyzer.GetLogFile();

            // Assert
            Assert.AreEqual(expectedFileName, result);
        }

        [TestMethod]
        public void GetLogFileName_FileWithSameNameExistAndLimitNotReached_FileNameWithNumberTwo()
        {
            // Arrange
            string path = "testpath";
            string fileName = "somefilename";
            string limitSize = "5B";
            string searchPattern = fileName + "*.txt";
            string expectedFileName = Path.Combine(path, fileName + "1.txt");

            var fileSizeComparatorMock = new Mock<IFileSizeComparator>();
            var fileNameCreatorMock = new Mock<IFileNameCreator>();
            var directoryWrapperMock = new Mock<IDirectoryWrapper>();

            directoryWrapperMock.Setup(d => d.Exists(path))
                            .Returns(true);
            directoryWrapperMock.Setup(d => d.EnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly))
                            .Returns(new string[] { fileName + ".txt", fileName + "1.txt" });

            fileSizeComparatorMock.Setup(s => s.IsFileReachedSizeLimit(path, limitSize))
                                  .Returns(false);

            var fileAnalyzer = new FileAnalyzer(fileSizeComparatorMock.Object, fileNameCreatorMock.Object, directoryWrapperMock.Object)
            {
                LogPath = path,
                FileName = fileName + ".txt",
                FileSizeLimit = limitSize
            };

            // Act
            string result = fileAnalyzer.GetLogFile();

            // Assert
            Assert.AreEqual(expectedFileName, result);
        }

        [TestMethod]
        public void GetLogFileName_FileWithSameNameExistAndLimitReached_FileNameWithNumberTwo()
        {
            // Arrange
            string path = "testpath";
            string fileName = "somefilename";
            string limitSize = "5B";
            string expectedFileName = Path.Combine(path, fileName + "2.txt");

            var fileSizeComparatorMock = new Mock<IFileSizeComparator>();
            var fileNameCreatorMock = new Mock<IFileNameCreator>();
            var directoryWrapperMock = new Mock<IDirectoryWrapper>();

            directoryWrapperMock.Setup(d => d.Exists(path))
                            .Returns(true);
            directoryWrapperMock.Setup(d => d.EnumerateFiles(path, fileName + "*.txt", SearchOption.TopDirectoryOnly))
                            .Returns(new string[] { fileName + ".txt", fileName + "1.txt" });

            string fileNameWithHighestNumber = Path.Combine(path, fileName + "1.txt");
            fileSizeComparatorMock.Setup(s => s.IsFileReachedSizeLimit(fileNameWithHighestNumber, limitSize))
                                  .Returns(true);

            fileNameCreatorMock.Setup(f => f.CreateUniqueFileName(fileNameWithHighestNumber))
                               .Returns(Path.Combine(path, fileName + "2.txt"));

            var fileAnalyzer = new FileAnalyzer(fileSizeComparatorMock.Object, fileNameCreatorMock.Object, directoryWrapperMock.Object)
            {
                LogPath = path,
                FileName = fileName + ".txt",
                FileSizeLimit = limitSize
            };

            // Act
            string result = fileAnalyzer.GetLogFile();

            // Assert
            Assert.AreEqual(expectedFileName, result);
        }
    }
}
