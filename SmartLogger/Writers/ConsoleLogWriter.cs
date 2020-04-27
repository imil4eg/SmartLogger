using System;
using SmartLogger.Events;

namespace SmartLogger.Writers
{
    internal sealed class ConsoleLogWritter : ILogWriter
    {
        private readonly ILogMessageCreator _logMessageCreator;

        public ConsoleLogWritter(ILogMessageCreator logMessageCreator)
        {
            _logMessageCreator = logMessageCreator;
        }

        public void Log(LogReceivedEventArgs logReceivedEvent)
        {
            string message = _logMessageCreator.CreateMessage(logReceivedEvent.LogInfo);
            Console.WriteLine(message);
        }
    }
}
