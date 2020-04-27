using Microsoft.Extensions.DependencyInjection;
using SmartLogger.Azure;
using SmartLogger.Configuration;
using SmartLogger.Events;
using SmartLogger.FileLoggerHelpers;
using SmartLogger.Loggers;
using SmartLogger.Writers;
using System;
using System.Runtime.InteropServices;

namespace SmartLogger.Builders
{
    public class LogBuilder : ILogBuilder
    {
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        private readonly AzureConfigContainer _azureConfigs;
        private readonly FileConfigContainer _fileConfigs;

        private IServiceProvider _serviceProvider;

        private bool _addAzureLogger;
        private bool _addConsole;
        private bool _addFileLogger;

        public LogBuilder()
        {
            _azureConfigs = new AzureConfigContainer();
            _fileConfigs = new FileConfigContainer();
        }

        public ILogBuilder AddAzureLogger(string connectionString, LogEvent eventType, string cloudFileDirectory, string fileShare)
        {
            if (string.IsNullOrEmpty(connectionString) ||
                string.IsNullOrEmpty(cloudFileDirectory) ||
                string.IsNullOrEmpty(fileShare))
            {
                throw new ArgumentNullException("Connectiong string, Cloud file directory or file share is null or empty.");
            }

            IAzureConfig azureConfig = new AzureConfig
            {
                ConnectionString = connectionString,
                LogEvent = eventType,
                FileDirectory = cloudFileDirectory,
                FileShare = fileShare
            };
            _azureConfigs.Add(eventType, azureConfig);
            _addAzureLogger = true;
            
            return this;
        }

        public ILogBuilder AddConsole()
        {
            _addConsole = true;
            return this;
        }

        public ILogBuilder AddFileLog(string fileName, string logPath, LogEvent eventType, string fileSizeLimit = "Unlimit")
        {
            IFileConfig fileConfig = new FileConfig
            {
                EventType = eventType,
                FileName = fileName,
                LogPath = logPath,
                FileSizeLimit = fileSizeLimit
            };
            _fileConfigs.Add(eventType, fileConfig);

            _addFileLogger = true;

            return this;
        }

        internal IServiceProvider Build()
        {
            var services = ResolveDependencies();

            _serviceProvider = services.BuildServiceProvider();

            SubscribeLogWriters();

            return _serviceProvider;
        }

        private void SubscribeLogWriters()
        {
            var logDistributor = _serviceProvider.GetService<ILogDistributor>();

            if (_addFileLogger)
            {
                var fileLogWriter = _serviceProvider.GetService<FileLogWriter>();
                logDistributor.LogReceivedEventHandler += fileLogWriter.Log;
            }

            if (_addAzureLogger)
            {
                var azureLogWriter = _serviceProvider.GetService<AzureLogWriter>();
                logDistributor.LogReceivedEventHandler += azureLogWriter.Log;
            }

            if (_addConsole)
            {
                var consoleLogWriter = _serviceProvider.GetService<ConsoleLogWritter>();
                logDistributor.LogReceivedEventHandler += consoleLogWriter.Log;
            }
        }

        private IServiceCollection ResolveDependencies()
        {
            var services = new ServiceCollection();

            var logDistributor = new LogDistributor();

            services.AddTransient<ILogMessageCreator, LogMessageCreator>();

            if (_addFileLogger)
            {
                services.AddTransient<IFileWrapper, FileWrapper>();
                services.AddTransient<IDirectoryWrapper, DirectoryWrapper>();
                services.AddTransient<IFileNameCreator, FileNameCreator>();
                services.AddTransient<ISizeCalculator, FileSizeCalculator>();
                services.AddTransient<IFileSizeComparator, FileSizeComparator>();
                services.AddTransient<IFileAnalyzer, FileAnalyzer>();
                services.AddSingleton<IConfigContainer<IFileConfig>>(_fileConfigs);
                services.AddSingleton<FileLogWriter>();
            }

            if (_addConsole)
            {
                AllocConsole();

                services.AddSingleton<ConsoleLogWritter>();
            }

            if (_addAzureLogger)
            {
                services.AddSingleton<IConfigContainer<IAzureConfig>>(_azureConfigs);
                services.AddSingleton<AzureLogWriter>();
                services.AddTransient<IAzureService, AzureService>();
            }

            services.AddSingleton<ILogDistributor>(logDistributor);
            services.AddTransient<ILogger, Logger>();

            return services;
        }
    }
}
