using System;
using SmartLogger.Configuration;
using SmartLogger.Enums;

namespace SmartLogger.Loggers
{
    internal class Logger : ILogger
    {
        private readonly ILogDistributor _logDistributor;

        public Logger(ILogDistributor logDistributor)
        {
            _logDistributor = logDistributor;
        }

        private void OnLogReceived(LogEvent logEvent, string prefix, string message, object model, Exception exception)
        {
            _logDistributor.LogReceived(new LogInfo(logEvent, prefix, message, model, exception));
        }

        public void Debug(string message)
        {
            OnLogReceived(LogEvent.Debug, LoggerWords.Debug, message, null, null);
        }

        public void Debug(Exception exception)
        {
            OnLogReceived(LogEvent.Debug, LoggerWords.Debug, null, null, exception);
        }

        public void Debug<TModel>(TModel model) where TModel : new()
        {
            OnLogReceived(LogEvent.Debug, LoggerWords.Debug, null, model, null);
        }

        public void Debug<TModel>(TModel model, string message) where TModel : new()
        {
            OnLogReceived(LogEvent.Debug, LoggerWords.Debug, message, model, null);
        }

        public void Debug<TModel>(TModel model, Exception exception) where TModel : new()
        {
            OnLogReceived(LogEvent.Debug, LoggerWords.Debug, null, model, exception);
        }

        public void Debug<TModel>(TModel model, string message, Exception exception) where TModel : new()
        {
            OnLogReceived(LogEvent.Debug, LoggerWords.Debug, message, model, exception);
        }

        public void Error(string message)
        {
            OnLogReceived(LogEvent.Error, LoggerWords.Error, message, null, null);
        }

        public void Error(Exception exception)
        {
            OnLogReceived(LogEvent.Error, LoggerWords.Error, null, null, exception);
        }

        public void Error<TModel>(TModel model) where TModel : new()
        {
            OnLogReceived(LogEvent.Error, LoggerWords.Error, null, model, null);
        }

        public void Error<TModel>(TModel model, string message) where TModel : new()
        {
            OnLogReceived(LogEvent.Error, LoggerWords.Error, message, model, null);
        }

        public void Error<TModel>(TModel model, Exception exception) where TModel : new()
        {
            OnLogReceived(LogEvent.Error, LoggerWords.Error, null, model, exception);
        }

        public void Error<TModel>(TModel model, string message, Exception exception) where TModel : new()
        {
            OnLogReceived(LogEvent.Error, LoggerWords.Error, message, model, exception);
        }

        public void Info(string message)
        {
            OnLogReceived(LogEvent.Info, LoggerWords.Info, message, null, null);
        }

        public void Info(Exception exception)
        {
            OnLogReceived(LogEvent.Info, LoggerWords.Info, null, null, exception);
        }

        public void Info<TModel>(TModel model) where TModel : new()
        {
            OnLogReceived(LogEvent.Info, LoggerWords.Info, null, model, null);
        }

        public void Info<TModel>(TModel model, string message) where TModel : new()
        {
            OnLogReceived(LogEvent.Info, LoggerWords.Info, message, model, null);
        }

        public void Info<TModel>(TModel model, Exception exception) where TModel : new()
        {
            OnLogReceived(LogEvent.Info, LoggerWords.Info, null, model, exception);
        }

        public void Info<TModel>(TModel model, string message, Exception exception) where TModel : new()
        {
            OnLogReceived(LogEvent.Info, LoggerWords.Info, message, model, exception);
        }
    }
}
