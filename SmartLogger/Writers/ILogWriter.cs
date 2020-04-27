using SmartLogger.Events;

namespace SmartLogger.Writers
{
    public interface ILogWriter
    {
        void Log(LogReceivedEventArgs logReceivedEvent);
    }
}