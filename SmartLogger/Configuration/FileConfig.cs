namespace SmartLogger.Configuration
{
    internal sealed class FileConfig : IFileConfig
    {
        public string FileName { get; set; }
        public string LogPath { get; set; }
        public LogEvent EventType { get; set; }
        public string FileSizeLimit { get; set; }
    }
}
