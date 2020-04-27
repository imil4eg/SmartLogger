using Microsoft.Extensions.DependencyInjection;
using SmartLogger.Builders;
using SmartLogger.Loggers;
using System;

namespace SmartLogger.Creators
{
    public sealed class LoggerCreator
    {
        public static ILogger CreateLogger(Action<ILogBuilder> builder)
        {
            var logBuilder = new LogBuilder();
            builder.Invoke(logBuilder);
            IServiceProvider serviceProvider = logBuilder.Build();

            return serviceProvider.GetService<ILogger>();
        }
    }
}
