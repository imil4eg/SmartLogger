using SmartLogger.Configuration;
using SmartLogger.Enums;
using System;

namespace SmartLogger
{
    public sealed class LogInfo
    {
        public LogEvent LogEvent { get; set; }
        public string Prefix { get; set; }
        public string Message { get; set; }
        public object Model { get; set; }
        public Exception Exception { get; set; }

        public LogInfo(LogEvent logEvent, string prefix, string message, object model, Exception exception)
        {
            LogEvent = logEvent;
            Prefix = prefix;
            Message = message;
            Model = model;
            Exception = exception;
        }
    }
}
