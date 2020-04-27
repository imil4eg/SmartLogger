namespace SmartLogger.Configuration
{
    internal interface IAzureConfigContainer
    {
        IAzureConfig Get(LogEvent logEvent);
    }
}
