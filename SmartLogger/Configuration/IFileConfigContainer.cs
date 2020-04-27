namespace SmartLogger.Configuration
{
    internal interface IFileConfigContainer
    {
        IFileConfig Get(LogEvent eventType);
    }
}
