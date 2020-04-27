using SmartLogger.Enums;
using System;

namespace SmartLogger
{
    internal interface ILogMessageCreator
    {
        string CreateMessage(LogInfo logInfo);
    }

    internal sealed class LogMessageCreator : ILogMessageCreator
    {
        public string CreateMessage(LogInfo logInfo)
        {
            string desirializedModelInfo = null;
            if (logInfo.Model != null)
            {
                desirializedModelInfo = Deserializers.ModelPropertiesDeserializer.Deserialize(logInfo.Model);
            }

            string fullExceptionMessage = null;
            if (logInfo.Exception != null)
            {
                fullExceptionMessage = logInfo.Exception.ToString();
            }

            string fullMessage = $"{logInfo.Message} {desirializedModelInfo} {fullExceptionMessage}";
            return string.Format(LogMessageFormats.DefaultFormat, logInfo.Prefix, DateTime.UtcNow, fullMessage);
        }
    }
}
