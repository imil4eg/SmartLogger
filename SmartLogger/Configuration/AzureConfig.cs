namespace SmartLogger.Configuration
{
    internal class AzureConfig : IAzureConfig
    {
        public string ConnectionString { get; set; }
        public LogEvent LogEvent { get; set; }
        public string FileShare { get; set; }
        public string FileDirectory { get; set; }
    }
}
