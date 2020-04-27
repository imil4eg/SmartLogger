using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartLogger.Creators;
using System.Configuration;
using System.IO;

namespace SmartLogger.IntegationTests
{
    [TestClass]
    public sealed class LoggerTests
    {
        private string LogDirectory;

        [TestInitialize]
        public void InitTest()
        {
            LogDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(LogDirectory);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Directory.Delete(LogDirectory, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void FileLogger_InfoLogFileSpecifiedButDebugEventCalled_Throw()
        {
            // Arrange
            string debugMessage = "DebugMessage.";

            string fileName = "logFile.txt";
            var logger = LoggerCreator.CreateLogger(a => a.AddFileLog(fileName, LogDirectory, Configuration.LogEvent.Info));

            // Act
            logger.Info("Testing.");
            logger.Debug(debugMessage);

            // Assert
            string text = File.ReadAllText(Path.Combine(LogDirectory, fileName));
            Assert.IsFalse(text.Contains(debugMessage));
        }

        [TestMethod]
        public void FileLogger_FileExistAtPath_True()
        {
            // Arrange
            string fileName = "logFile.txt";
            var logger = LoggerCreator.CreateLogger(a => a.AddFileLog(fileName, LogDirectory, Configuration.LogEvent.Info));
            
            // Act
            logger.Info("Testing.");

            // Assert
            Assert.IsTrue(File.Exists(Path.Combine(LogDirectory, fileName)));
        }

        [TestMethod]
        public void FileLogger_FileContainsSpecifiedText_True()
        {
            // Arrange
            string message = "Testing.";

            string fileName = "logFile.txt";
            var logger = LoggerCreator.CreateLogger(a => a.AddFileLog(fileName, LogDirectory, Configuration.LogEvent.Info));
            
            // Act
            logger.Info(message);

            // Assert
            string text = File.ReadAllText(Path.Combine(LogDirectory, fileName));
            Assert.IsTrue(text.Contains(message));
        }

        [TestMethod]
        public void FileLogger_SpecifiedFileForInfoLoggerAndFileForOtherEvents_InfoFileExist()
        {
            // Arrange
            string infoMessage = "InfoMessage.";
            string debugMessage = "DebugMessage.";

            string infoFileName = "infoLogFile.txt";
            string otherFileName = "otherLogFile.txt";

            var logger = LoggerCreator.CreateLogger(a => a.AddFileLog(infoFileName, LogDirectory, Configuration.LogEvent.Info)
                                                          .AddFileLog(otherFileName, LogDirectory, Configuration.LogEvent.AllOther));

            // Act
            logger.Info(infoMessage);
            logger.Debug(debugMessage);

            // Assert
            Assert.IsTrue(File.Exists(Path.Combine(LogDirectory, infoFileName)));
        }

        [TestMethod]
        public void FileLogger_SpecifiedFileForInfoLoggerAndFileForOtherEvents_OtherFileExist()
        {
            // Arrange
            string infoMessage = "InfoMessage.";
            string debugMessage = "DebugMessage.";

            string infoFileName = "infoLogFile.txt";
            string otherFileName = "otherLogFile.txt";

            var logger = LoggerCreator.CreateLogger(a => a.AddFileLog(infoFileName, LogDirectory, Configuration.LogEvent.Info)
                                                          .AddFileLog(otherFileName, LogDirectory, Configuration.LogEvent.AllOther));

            // Act
            logger.Info(infoMessage);
            logger.Debug(debugMessage);

            // Assert
            Assert.IsTrue(File.Exists(Path.Combine(LogDirectory, otherFileName)));
        }

        [TestMethod]
        public void FileLogger_SpecifiedFileForInfoLoggerAndFileForOtherEvents_InfoFileContainsInfoMessage()
        {
            // Arrange
            string infoMessage = "InfoMessage.";
            string debugMessage = "DebugMessage.";

            string infoFileName = "infoLogFile.txt";
            string otherFileName = "otherLogFile.txt";

            var logger = LoggerCreator.CreateLogger(a => a.AddFileLog(infoFileName, LogDirectory, Configuration.LogEvent.Info)
                                                          .AddFileLog(otherFileName, LogDirectory, Configuration.LogEvent.AllOther));

            // Act
            logger.Info(infoMessage);
            logger.Debug(debugMessage);

            // Assert
            string text = File.ReadAllText(Path.Combine(LogDirectory, infoFileName));
            Assert.IsTrue(text.Contains(infoMessage));
        }

        [TestMethod]
        public void FileLogger_SpecifiedFileForInfoLoggerAndFileForOtherEvents_OtherFileContainsDebugMessage()
        {
            // Arrange
            string infoMessage = "InfoMessage.";
            string debugMessage = "DebugMessage.";

            string infoFileName = "infoLogFile.txt";
            string otherFileName = "otherLogFile.txt";

            var logger = LoggerCreator.CreateLogger(a => a.AddFileLog(infoFileName, LogDirectory, Configuration.LogEvent.Info)
                                                          .AddFileLog(otherFileName, LogDirectory, Configuration.LogEvent.AllOther));

            // Act
            logger.Info(infoMessage);
            logger.Debug(debugMessage);

            // Assert
            string text = File.ReadAllText(Path.Combine(LogDirectory, otherFileName));
            Assert.IsTrue(text.Contains(debugMessage));
        }
    }
}
