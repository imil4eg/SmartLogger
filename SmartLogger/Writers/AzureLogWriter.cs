using System.IO;

using Microsoft.Azure.Storage.File;

using SmartLogger.Azure;
using SmartLogger.Configuration;
using SmartLogger.Events;

namespace SmartLogger.Writers
{
    internal sealed class AzureLogWriter : ILogWriter
    {
        private readonly IAzureService _azureService;
        private readonly IConfigContainer<IFileConfig> _fileConfigContainer;

        public AzureLogWriter(IAzureService azureService, IConfigContainer<IFileConfig> fileConfigContainer)
        {
            _azureService = azureService;
            _fileConfigContainer = fileConfigContainer;
        }

        public void Log(LogReceivedEventArgs logReceivedEvent)
        {
            var azureFileDirectory = _azureService.GetCloudFileDirectoryByEvent(logReceivedEvent.LogInfo.LogEvent);

            if (azureFileDirectory == null)
            {
                return;
            }

            var fileConfig = _fileConfigContainer.Get(logReceivedEvent.LogInfo.LogEvent);

            if (fileConfig == null)
            {
                return;
            }

            CloudFile file = azureFileDirectory.GetFileReference(fileConfig.FileName);

            string filePath = Path.Combine(fileConfig.LogPath, fileConfig.FileName);
            
            if (File.Exists(filePath))
            {
                throw new FileNotFoundException($"{filePath} not found.");
            }

            using (var fileStream = File.OpenRead(filePath))
            {
                file.UploadFromStream(fileStream);
            }
        }
    }
}
