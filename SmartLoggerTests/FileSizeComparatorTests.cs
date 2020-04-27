using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartLogger.FileLoggerHelpers;
using System.IO;

namespace SmartLoggerTests
{
    [TestClass]
    public sealed class FileSizeComparatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void IsFileReachedSizeLimit_FileNotExist_Throw()
        {
            // Arrange
            string path = "notExistingFile";
            string limitSize = "zero";

            var sizeCalculatorMock = new Mock<ISizeCalculator>();
            var fileWrapperMock = new Mock<IFileWrapper>();
            var fileSizeComparator = new FileSizeComparator(sizeCalculatorMock.Object, fileWrapperMock.Object);


            // Act
            fileSizeComparator.IsFileReachedSizeLimit(path, limitSize);

            // Assert
        }

        [TestMethod]
        public void IsFileReachedSizeLimit_NotReached_False()
        {
            // Arrange
            string path = "notExistingFile";
            string limitSize = "10B";

            var sizeCalculatorMock = new Mock<ISizeCalculator>();
            sizeCalculatorMock.Setup(c => c.Calculate(limitSize)).Returns(10);

            var fileWrapperMock = new Mock<IFileWrapper>();
            fileWrapperMock.Setup(a => a.Exists(path)).Returns(true);
            fileWrapperMock.Setup(a => a.GetFileSize(path)).Returns(11);

            var fileSizeComparator = new FileSizeComparator(sizeCalculatorMock.Object, fileWrapperMock.Object);

            // Act
            bool result = fileSizeComparator.IsFileReachedSizeLimit(path, limitSize);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsFileReachedSizeLimit_EqualsLimit_True()
        {
            // Arrange
            string path = "notExistingFile";
            string limitSize = "10B";

            var sizeCalculatorMock = new Mock<ISizeCalculator>();
            sizeCalculatorMock.Setup(c => c.Calculate(limitSize)).Returns(10);

            var fileWrapperMock = new Mock<IFileWrapper>();
            fileWrapperMock.Setup(a => a.Exists(path)).Returns(true);
            fileWrapperMock.Setup(a => a.GetFileSize(path)).Returns(10);

            var fileSizeComparator = new FileSizeComparator(sizeCalculatorMock.Object, fileWrapperMock.Object);


            // Act
            bool result = fileSizeComparator.IsFileReachedSizeLimit(path, limitSize);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsFileReachedSizeLimit_LessThanLimit_True()
        {
            // Arrange
            string path = "notExistingFile";
            string limitSize = "10B";

            var sizeCalculatorMock = new Mock<ISizeCalculator>();
            sizeCalculatorMock.Setup(c => c.Calculate(limitSize)).Returns(10);

            var fileWrapperMock = new Mock<IFileWrapper>();
            fileWrapperMock.Setup(a => a.Exists(path)).Returns(true);
            fileWrapperMock.Setup(a => a.GetFileSize(path)).Returns(9);

            var fileSizeComparator = new FileSizeComparator(sizeCalculatorMock.Object, fileWrapperMock.Object);


            // Act
            bool result = fileSizeComparator.IsFileReachedSizeLimit(path, limitSize);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
