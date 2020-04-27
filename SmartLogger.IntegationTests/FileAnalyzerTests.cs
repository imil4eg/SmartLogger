using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartLogger.FileLoggerHelpers;
using System;
using System.IO;

namespace SmartLogger.IntegationTests
{
    [TestClass]
    public sealed class FileAnalyzerTests
    {
        private string WorkingPath;

        [TestInitialize]
        public void InitTest()
        {
            WorkingPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(WorkingPath);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Directory.Delete(WorkingPath, true);
        }

        [TestMethod]
        public void GetLastesFileWithSameName_FileWithSameDoesNotExist_Null()
        {
            // Arrange
            string fileName = "somefilename.txt";

            var fileSizeComparatorMock = new Mock<IFileSizeComparator>();
            var fileNameCreatorMock = new Mock<IFileNameCreator>();
            var directoryWrapper = new DirectoryWrapper();

            var fileNameAnalyzer = new FileAnalyzer(fileSizeComparatorMock.Object, fileNameCreatorMock.Object, directoryWrapper)
            {
                LogPath = WorkingPath,
                FileName = fileName
            };

            // Act
            var result = fileNameAnalyzer.GetLastesFileWithSameName();

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetLastesFileWithSameName_FileWithSameNameExistWithoutNumbers_FileName()
        {
            // Arrange
            string fileName = "somefilename.txt";

            File.WriteAllText(Path.Combine(WorkingPath, fileName), "");

            var fileSizeComparatorMock = new Mock<IFileSizeComparator>();
            var fileNameCreatorMock = new Mock<IFileNameCreator>();
            var directoryWrapper = new DirectoryWrapper();

            var fileAnalyzer = new FileAnalyzer(fileSizeComparatorMock.Object, fileNameCreatorMock.Object, directoryWrapper)
            {
                LogPath = WorkingPath,
                FileName = fileName
            };

            // Act
            var result = fileAnalyzer.GetLastesFileWithSameName();

            // Assert
            Assert.AreEqual(fileName, Path.GetFileName(result));
        }

        [TestMethod]
        public void GetLastesFileWithSameName_TenFilesWithSameNameExist_FileNameWithNumberNine()
        {
            // Arrange
            string fileName = "somefilename";

            File.WriteAllText(Path.Combine(WorkingPath, fileName + ".txt"), "");
            for (int i = 1; i < 10; i++)
            {
                File.WriteAllText(Path.Combine(WorkingPath, fileName + i + ".txt"), "");
            }

            var fileSizeComparatorMock = new Mock<IFileSizeComparator>();
            var fileNameCreatorMock = new Mock<IFileNameCreator>();
            var directoryWrapper = new DirectoryWrapper();

            var fileAnalyzer = new FileAnalyzer(fileSizeComparatorMock.Object, fileNameCreatorMock.Object, directoryWrapper)
            {
                LogPath = WorkingPath,
                FileName = fileName + ".txt"
            };

            // Act
            var result = fileAnalyzer.GetLastesFileWithSameName();

            // Assert
            Assert.AreEqual(fileName + "9" + ".txt", Path.GetFileName(result));
        }

        [TestMethod]
        public void GetLastesFileWithSameName_ThreeFilesExistWithSameNameButNumberNotOrdered_FileNameWithHigherNumber()
        {
            // Arrange
            string fileName = "somefilename";

            int higherNumber = 25;
            File.WriteAllText(Path.Combine(WorkingPath, fileName + higherNumber + ".txt"), "");
            File.WriteAllText(Path.Combine(WorkingPath, fileName + "5" + ".txt"), "");
            File.WriteAllText(Path.Combine(WorkingPath, fileName + "20" + ".txt"), "");

            var fileSizeComparatorMock = new Mock<IFileSizeComparator>();
            var fileNameCreatorMock = new Mock<IFileNameCreator>();
            var directoryWrapper = new DirectoryWrapper();

            var fileAnalyzer = new FileAnalyzer(fileSizeComparatorMock.Object, fileNameCreatorMock.Object, directoryWrapper)
            {
                LogPath = WorkingPath,
                FileName = fileName + ".txt"
            };

            // Act
            var result = fileAnalyzer.GetLastesFileWithSameName();

            // Assert
            Assert.AreEqual(fileName + higherNumber + ".txt", Path.GetFileName(result));
        }
    }
}
