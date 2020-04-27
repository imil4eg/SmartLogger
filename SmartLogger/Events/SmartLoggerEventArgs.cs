using SmartLogger.Configuration;
using System;

namespace SmartLogger.Events
{
    public sealed class LogReceivedEventArgs : EventArgs
    {
        public LogInfo LogInfo { get; private set; }

        public LogReceivedEventArgs(LogEvent logEvent, string prefix, string message, object model, Exception exception)
        {
            LogInfo = new LogInfo(logEvent, prefix, message, model, exception);
        }

        public LogReceivedEventArgs(LogInfo logInfo)
        {
            LogInfo = logInfo;
        }
    }

    public delegate void LogReceivedEventHandler(object source, LogReceivedEventArgs e);
}
