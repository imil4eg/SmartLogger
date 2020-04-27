namespace SmartLogger.Configuration
{
    internal interface IFileConfig
    {
        string FileName { get; set; }
        string LogPath { get; set; }
        LogEvent EventType { get; set; }
        string FileSizeLimit { get; set; }
    }
}
