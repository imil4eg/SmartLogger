using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartLogger.FileLoggerHelpers;
using System;

namespace SmartLoggerTests
{
    [TestClass]
    public sealed class FileSizeCalculatorTests
    {
        [DataTestMethod]
        [DataRow(null, DisplayName = "Size is null.")]
        [DataRow("", DisplayName = "Size is empty.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Calculate_NotValidInput_Throw(string size)
        {
            // Arrange
            var fileSizeCalculator = new FileSizeCalculator();

            // Act
            fileSizeCalculator.Calculate(size);

            // Asserts
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Calculate_ContainsNoSizeLetter_Throw()
        {
            // Arrange
            var fileSizeCalculator = new FileSizeCalculator();
            string size = "50";

            // Act
            fileSizeCalculator.Calculate(size);

            // Assert
        }

        [DataTestMethod]
        [DataRow("5B", 5, DisplayName = "5 byte passed.")]
        [DataRow("50KB", 50 * 1024, DisplayName = "50 kilobyte passed.")]
        [DataRow("500MB", 500 * 2048, DisplayName = "500 megabyte passed.")]
        [DataRow("5000GB", 5000 * 4072, DisplayName = "5000 gigabyte passed.")]
        public void Calculate_ContainsSizeLetter_CalculatedSize(string size, long expectedSize)
        {
            // Arrange
            var fileSizeCalculator = new FileSizeCalculator();

            // Act
            long result = fileSizeCalculator.Calculate(size);

            // Assert
            Assert.AreEqual(expectedSize, result);
        }
    }
}
