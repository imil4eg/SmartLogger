namespace SmartLogger.Configuration
{
    internal interface IAzureConfig
    {
        string ConnectionString { get; set; }
        string FileShare { get; set; }
        string FileDirectory { get; set; }
        LogEvent LogEvent { get; set; }
    }
}
