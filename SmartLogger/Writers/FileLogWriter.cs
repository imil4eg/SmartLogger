using SmartLogger.Configuration;
using SmartLogger.Events;
using System.Configuration;
using System.IO;

namespace SmartLogger.Writers
{
    internal sealed class FileLogWriter : ILogWriter
    {
        private readonly IConfigContainer<IFileConfig> _fileConfigContainer;
        private readonly IFileAnalyzer _fileAnalyzer;
        private readonly ILogMessageCreator _logMessageCreator;

        public FileLogWriter(IConfigContainer<IFileConfig> fileConfigContainer, IFileAnalyzer fileAnalyzer,
            ILogMessageCreator logMessageCreator)
        {
            _fileConfigContainer = fileConfigContainer;
            _fileAnalyzer = fileAnalyzer;
            _logMessageCreator = logMessageCreator;
        }

        public void Log(LogReceivedEventArgs logReceivedEvent)
        {
            IFileConfig currentConfig = _fileConfigContainer.Get(logReceivedEvent.LogInfo.LogEvent);

            if (currentConfig == null)
            {
                throw new ConfigurationErrorsException($"{System.Enum.GetName(typeof(LogEvent), logReceivedEvent.LogInfo.LogEvent)} is not supported");
            }

            _fileAnalyzer.FileName = currentConfig.FileName;
            _fileAnalyzer.LogPath = currentConfig.LogPath;
            _fileAnalyzer.FileSizeLimit = currentConfig.FileSizeLimit;

            string logFile = _fileAnalyzer.GetLogFile();

            string message = _logMessageCreator.CreateMessage(logReceivedEvent.LogInfo);

            File.AppendAllText(logFile, message + System.Environment.NewLine);
        }
    }
}
