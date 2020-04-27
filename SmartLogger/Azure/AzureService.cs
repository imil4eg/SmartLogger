using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.File;

using SmartLogger.Configuration;

namespace SmartLogger.Azure
{
    internal interface IAzureService
    {
        CloudFileDirectory GetCloudFileDirectoryByEvent(LogEvent logEvent);
    }

    internal sealed class AzureService : IAzureService
    {
        private readonly IConfigContainer<IAzureConfig> _azureConfigContainer;

        public AzureService(IConfigContainer<IAzureConfig> azureConfigContainer)
        {
            _azureConfigContainer = azureConfigContainer;
        }

        public CloudFileDirectory GetCloudFileDirectoryByEvent(LogEvent logEvent)
        {
            var azureConfig = _azureConfigContainer.Get(logEvent);

            if (azureConfig == null)
            {
                return null;
            }

            var storageAccount = CloudStorageAccount.Parse(azureConfig.ConnectionString);

            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();

            CloudFileShare share = fileClient.GetShareReference(azureConfig.FileShare);

            if (!share.Exists())
            {
                throw new StorageException($"{azureConfig.FileDirectory} not found.");
            }

            CloudFileDirectory rootDir = share.GetRootDirectoryReference();

            CloudFileDirectory cloudFileDirectory = rootDir.GetDirectoryReference(azureConfig.FileDirectory);

            return cloudFileDirectory;
        }
    }
}
