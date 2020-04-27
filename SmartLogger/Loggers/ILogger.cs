using SmartLogger.Enums;
using SmartLogger.Events;
using System;

namespace SmartLogger.Loggers
{

    // TODO: Think about builder for a logger instead of 
    // and you don't need separated logger for console/file/azure logers
    // instead of implement builder that will take argument like
    // AddConsole
    // AddFileLogger that will file name, log events, file size.
    // For Azure logging add something like AddAzureLoggin(Event Type on which to log).
    public interface ILogger
    {
        void Info(string message);
        void Info(Exception exception);
        void Info<TModel>(TModel model) where TModel : new();
        void Info<TModel>(TModel model, string message) where TModel : new();
        void Info<TModel>(TModel model, Exception exception) where TModel : new();
        void Info<TModel>(TModel model, string message, Exception exception) where TModel : new();

        void Error(string message);
        void Error(Exception exception);
        void Error<TModel>(TModel model) where TModel : new();
        void Error<TModel>(TModel model, string message) where TModel : new();
        void Error<TModel>(TModel model, Exception exception) where TModel : new();
        void Error<TModel>(TModel model, string message, Exception exception) where TModel : new();

        void Debug(string message);
        void Debug(Exception exception);
        void Debug<TModel>(TModel model) where TModel : new();
        void Debug<TModel>(TModel model, string message) where TModel : new();
        void Debug<TModel>(TModel model, Exception exception) where TModel : new();
        void Debug<TModel>(TModel model, string message, Exception exception) where TModel : new();
    }
}
