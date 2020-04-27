using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using SmartLogger;

namespace SmartLoggerTests
{
    [TestClass]
    public sealed class StringExtensionTests
    {
        [DataTestMethod]
        [DataRow(null, DisplayName = "Input is null.")]
        [DataRow("", DisplayName ="Input is empty.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IndexOfNumberEndingPathBegging_InvalidInput_Throw(string input)
        {
            // Arrange

            // Act
            input.IndexOfNumberEndingPathBegging();

            // Assert
        }

        [TestMethod]
        public void IndexOfNumberEndingPathBegging_NoPathWithNumber_NegativeOne()
        {
            // Arrange
            string word = "somefilename";

            // Act
            int result = word.IndexOfNumberEndingPathBegging();

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void IndexOfNumberEndingPathBegging_PathWithNumberExist_IndexOfNumberPath()
        {
            // Arrange
            string numberPath = "1234";
            string word = "somefilename";
            int indexOfNumberPathBegging = word.Length;

            word += numberPath;

            // Act
            int result = word.IndexOfNumberEndingPathBegging();

            // Assert
            Assert.AreEqual(indexOfNumberPathBegging, result);
        }

        [TestMethod]
        public void IndexOfNumberEndingPathBegging_WordContainsNumbersAtStart_IndexOfLastNumberPath()
        {
            // Arrange
            string numberPath = "1234";
            string word = "1234somefilename";
            int indexOfNumberPathBegging = word.Length;

            word += numberPath;

            // Act
            int result = word.IndexOfNumberEndingPathBegging();

            // Assert
            Assert.AreEqual(indexOfNumberPathBegging, result);
        }

        [TestMethod]
        public void IndexOfNumberEndingPathBegging_WordContainsNumbersAtTheMiddle_IndexOfLastNumberPath()
        {
            // Arrange
            string numberPath = "1234";
            string word = "some1234filename";
            int indexOfNumberPathBegging = word.Length;

            word += numberPath;

            // Act
            int result = word.IndexOfNumberEndingPathBegging();

            // Assert
            Assert.AreEqual(indexOfNumberPathBegging, result);
        }

        [TestMethod]
        public void IndexOfNumberEndingPathBegging_WordContainsNumbersAtTheMiddleOnly_NegativeOne()
        {
            // Arrange
            string word = "some1234filename";
            
            // Act
            int result = word.IndexOfNumberEndingPathBegging();

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void IndexOfNumberEndingPathBegging_WordContainsNumbersAtTheBegginingOnly_NegativeOne()
        {
            // Arrange
            string word = "1234somefilename";

            // Act
            int result = word.IndexOfNumberEndingPathBegging();

            // Assert
            Assert.AreEqual(-1, result);
        }

        [DataTestMethod]
        [DataRow(null, DisplayName = "Input is null.")]
        [DataRow("", DisplayName = "Input is empty.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSizeLetters_NotValidInput_Throw(string size)
        {
            // Arrange

            // Act
            size.GetSizeLetters(out string number);

            // Assert
        }

        [TestMethod]
        public void GetSizeLetters_NoSizeLetters_Empty()
        {
            // Arrange
            string size = "55";

            // Act
            string result = size.GetSizeLetters(out string number);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [DataTestMethod]
        [DataRow("5B", "B", DisplayName = "Size contains Byte.")]
        [DataRow("10KB", "KB", DisplayName = "Size contains KB.")]
        [DataRow("100MB", "MB", DisplayName = "Size contains MB.")]
        [DataRow("1000GB", "GB", DisplayName = "Size contains GB.")]
        public void GetSizeLetters_ContainsSizeLetters_SizeLetters(string size, string expectedLetters)
        {
            // Arrange

            // Act
            string result = size.GetSizeLetters(out string number);

            // Assert
            Assert.AreEqual(expectedLetters, result);
        }

        [DataTestMethod]
        [DataRow("5B", "5", DisplayName = "Size contains number 5.")]
        [DataRow("10KB", "10", DisplayName = "Size contains number 10.")]
        [DataRow("100MB", "100", DisplayName = "Size contains number 100.")]
        [DataRow("1000GB", "1000", DisplayName = "Size contains number 1000.")]
        public void GetSizeLetters_ContainsNumber_SizeLetters(string size, string expectedNumber)
        {
            // Arrange

            // Act
            string result = size.GetSizeLetters(out string number);

            // Assert
            Assert.AreEqual(expectedNumber, number);
        }
    }
}
