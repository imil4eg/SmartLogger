using System.Configuration;

namespace SmartLogger.Configuration
{
    internal sealed class FileConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("fileName")]
        public string FileName
        {
            get => (string)base["fileName"];
        }

        [ConfigurationProperty("logPath")]
        public string LogPath
        {
            get => (string)base["logPath"];
        }

        [ConfigurationProperty("maxFileSize")]
        public string MaxFileSize
        {
            get => (string)base["maxFileSize"];
        }

        [ConfigurationProperty("fileType")]
        public LogEvent? FileType
        {
            get => base["fileType"] as LogEvent?;
        }
    }

    public enum LogEvent
    {
        Error,
        Debug,
        Info,
        AllOther
    }
}
