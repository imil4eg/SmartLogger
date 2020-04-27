namespace SmartLogger.Configuration
{
    internal interface IConfigContainer<TConfig>
    {
        TConfig Get(LogEvent logEvent);
    }
}
