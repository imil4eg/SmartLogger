//using SmartLogger.ConfigReaders;
//using SmartLogger.Enums;
//using SmartLogger.Events;
//using System;
//using System.Runtime.InteropServices;

//namespace SmartLogger.Loggers
//{
//    internal sealed class ConsoleLogger : ILogger
//    {
//        public LoggerType LoggerType => throw new NotImplementedException();

//        public event LogReceivedEventHandler LogReceivedEventHandler;

//        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
//        private static extern int AllocConsole();

//        public ConsoleLogger()
//        {
//            if (ConfigReader.AllocateConsole())
//            {
//                AllocConsole();
//            }
//        }

//        private void OnLogReceived(string prefix, string message, object model, Exception exception)
//        {
//            LogReceivedEventHandler(null, new LogReceivedEventArgs(this.GetType(), prefix, message, model, exception));
//        }

//        public void Debug(string message)
//        {
//            OnLogReceived(LoggerWords.Debug, message, null, null);
//        }

//        public void Debug(Exception exception)
//        {
//            OnLogReceived(LoggerWords.Debug, null, null, exception);
//        }

//        public void Debug<TModel>(TModel model) where TModel : new()
//        {
//            OnLogReceived(LoggerWords.Debug, null, model, null);
//        }

//        public void Debug<TModel>(TModel model, string message) where TModel : new()
//        {
//            OnLogReceived(LoggerWords.Debug, message, model, null);
//        }

//        public void Debug<TModel>(TModel model, Exception exception) where TModel : new()
//        {
//            OnLogReceived(LoggerWords.Debug, null, model, exception);
//        }

//        public void Debug<TModel>(TModel model, string message, Exception exception) where TModel : new()
//        {
//            OnLogReceived(LoggerWords.Debug, message, model, exception);
//        }

//        public void Error(string message)
//        {
//            OnLogReceived(LoggerWords.Error, message, null, null);
//        }

//        public void Error(Exception exception)
//        {
//            OnLogReceived(LoggerWords.Error, null, null, exception);
//        }

//        public void Error<TModel>(TModel model) where TModel : new()
//        {
//            OnLogReceived(LoggerWords.Error, null, model, null);
//        }

//        public void Error<TModel>(TModel model, string message) where TModel : new()
//        {
//            OnLogReceived(LoggerWords.Error, message, model, null);
//        }

//        public void Error<TModel>(TModel model, Exception exception) where TModel : new()
//        {
//            OnLogReceived(LoggerWords.Error, null, model, exception);
//        }

//        public void Error<TModel>(TModel model, string message, Exception exception) where TModel : new()
//        {
//            OnLogReceived(LoggerWords.Error, message, model, exception);
//        }

//        public void Info(string message)
//        {
//            OnLogReceived(LoggerWords.Info, message, null, null);
//        }

//        public void Info(Exception exception)
//        {
//            OnLogReceived(LoggerWords.Info, null, null, exception);
//        }

//        public void Info<TModel>(TModel model) where TModel : new()
//        {
//            OnLogReceived(LoggerWords.Info, null, model, null);
//        }

//        public void Info<TModel>(TModel model, string message) where TModel : new()
//        {
//            OnLogReceived(LoggerWords.Info, message, model, null);
//        }

//        public void Info<TModel>(TModel model, Exception exception) where TModel : new()
//        {
//            OnLogReceived(LoggerWords.Info, null, model, exception);
//        }

//        public void Info<TModel>(TModel model, string message, Exception exception) where TModel : new()
//        {
//            OnLogReceived(LoggerWords.Info, message, model, exception);
//        }
//    }
//}
