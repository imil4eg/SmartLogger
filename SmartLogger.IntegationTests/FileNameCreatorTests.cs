using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SmartLogger.IntegationTests
{
    [TestClass]
    public class FileNameCreatorTests
    {
        private string _path;

        [TestInitialize]
        public void InitTest()
        {
            _path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(_path);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Directory.Delete(_path, true);
        }

        [DataTestMethod]
        [DataRow(null, DisplayName = "Path is null.")]
        [DataRow("", DisplayName = "Path is empty.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateUniqueFileName_InvalidData_Throw(string filePath)
        {
            // Arrange
            var fileNameCreator = new FileNameCreator();

            // Act
            fileNameCreator.CreateUniqueFileName(filePath);

            // Assert
        }

        [TestMethod]
        public void CreateUniqueFileName_FileWithoutNumberExist_FileNameWithNumberOne()
        {
            // Arrange
            string fileNameWithoutNumber = "TestFileName.txt";

            string existingFileNamePath = Path.Combine(_path, fileNameWithoutNumber);

            File.Create(existingFileNamePath).Dispose();

            var fileNameCreator = new FileNameCreator();

            // Act
            string result = fileNameCreator.CreateUniqueFileName(existingFileNamePath);

            // Assert
            Assert.IsTrue(Path.GetFileNameWithoutExtension(result).EndsWith("1"));
        }

        [TestMethod]
        public void CreateUniqueFileName_TenFilesWithSameNumberExist_FileNameWithNumberEleven()
        {
            // Arrange
            string fileNameWithoutNumber = "TestFileName";

            string existingFileNamePath = Path.Combine(_path, fileNameWithoutNumber + ".txt");

            File.Create(existingFileNamePath).Dispose();
            for (int i = 1; i < 11; i++)
            {
                File.Create(Path.Combine(_path, fileNameWithoutNumber + i + ".txt")).Dispose();
            }

            var fileNameCreator = new FileNameCreator();

            // Act
            string result = fileNameCreator.CreateUniqueFileName(existingFileNamePath);

            // Assert
            Assert.IsTrue(Path.GetFileNameWithoutExtension(result).EndsWith("11"));
        }
    }
}
