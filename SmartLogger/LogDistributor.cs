using SmartLogger.Events;
using System.Collections.Generic;

namespace SmartLogger
{
    internal interface ILogDistributor
    {
        CallAndWaitEvent<LogReceivedEventArgs> LogReceivedEventHandler { get; set; }

        void LogReceived(LogInfo logInfo);
    }

    internal sealed class LogDistributor : ILogDistributor
    {
        private static readonly object _locker = new object();

        private readonly Queue<LogInfo> LogsQueue;

        private bool _inProgress;

        public CallAndWaitEvent<LogReceivedEventArgs> LogReceivedEventHandler { get; set; }

        public LogDistributor()
        {
            LogReceivedEventHandler = new CallAndWaitEvent<LogReceivedEventArgs>();
            LogsQueue = new Queue<LogInfo>(1000);
        }

        public void LogReceived(LogInfo logInfo)
        {
            LogsQueue.Enqueue(logInfo);

            if (_inProgress)
            {
                return;
            }

            lock (_locker)
            {
                _inProgress = true;

                while (LogsQueue.Count > 0)
                {
                    var currentLogInfo = LogsQueue.Dequeue();
                    LogReceivedEventHandler.InvokeAllAndWait(new LogReceivedEventArgs(currentLogInfo));
                }

                _inProgress = false;
            }
        }
    }
}
