//using SmartLogger.ConfigReaders;
//using SmartLogger.Configuration;
//using SmartLogger.Events;
//using System;
//using System.Collections.Generic;
//using System.IO;

//namespace SmartLogger.Loggers
//{
//    internal class FileLogger : ILogger
//    {
//        private string _fileName;
//        private string _filePath;
//        private string _fullPath;

//        public FileLogger()
//        {
//        }

//        public event LogReceivedEventHandler LogReceivedEventHandler;

//        public void Debug(string message)
//        {
//            string formattedMessage = MessageManager.Instance.GetDebugMessage(message);
//        }

//        public void Debug(Exception exception)
//        {
//            throw new NotImplementedException();
//        }

//        public void Debug<TModel>(TModel model) where TModel : new()
//        {
//            throw new NotImplementedException();
//        }

//        public void Debug<TModel>(TModel model, string message) where TModel : new()
//        {
//            throw new NotImplementedException();
//        }

//        public void Debug<TModel>(TModel model, Exception exception) where TModel : new()
//        {
//            throw new NotImplementedException();
//        }

//        public void Debug<TModel>(TModel model, string message, Exception exception) where TModel : new()
//        {
//            throw new NotImplementedException();
//        }

//        public void Error(string message)
//        {
//            throw new NotImplementedException();
//        }

//        public void Error(Exception exception)
//        {
//            throw new NotImplementedException();
//        }

//        public void Error<TModel>(TModel model) where TModel : new()
//        {
//            throw new NotImplementedException();
//        }

//        public void Error<TModel>(TModel model, string message) where TModel : new()
//        {
//            throw new NotImplementedException();
//        }

//        public void Error<TModel>(TModel model, Exception exception) where TModel : new()
//        {
//            throw new NotImplementedException();
//        }

//        public void Error<TModel>(TModel model, string message, Exception exception) where TModel : new()
//        {
//            throw new NotImplementedException();
//        }

//        public void Info(string message)
//        {
//            throw new NotImplementedException();
//        }

//        public void Info(Exception exception)
//        {
//            throw new NotImplementedException();
//        }

//        public void Info<TModel>(TModel model) where TModel : new()
//        {
//            throw new NotImplementedException();
//        }

//        public void Info<TModel>(TModel model, string message) where TModel : new()
//        {
//            throw new NotImplementedException();
//        }

//        public void Info<TModel>(TModel model, Exception exception) where TModel : new()
//        {
//            throw new NotImplementedException();
//        }

//        public void Info<TModel>(TModel model, string message, Exception exception) where TModel : new()
//        {
//            throw new NotImplementedException();
//        }

//        protected async System.Threading.Tasks.Task WriteToFileAsync(string message)
//        {
//            using (var fs = File.CreateText(_fullPath))
//            {
//                await fs.WriteLineAsync(message);
//            }
//        }
//    }
//}
