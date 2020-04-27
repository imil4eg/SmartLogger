using SmartLogger.Configuration;

namespace SmartLogger.Builders
{
    public interface ILogBuilder
    {
        ILogBuilder AddConsole();
        ILogBuilder AddFileLog(string fileName, string logPath, LogEvent eventType, string fileSizeLimit = "Unlimit");
        ILogBuilder AddAzureLogger(string connectionString, LogEvent eventType, string cloudFileDirectory, string fileShare);
    }
}
