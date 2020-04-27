using System.Collections.Generic;

namespace SmartLogger.Configuration
{
    internal class BaseConfigDictionary<TConfig> : Dictionary<LogEvent, TConfig>, IConfigContainer<TConfig>
    {
        public virtual TConfig Get(LogEvent logEvent)
        {
            if (this.ContainsKey(logEvent))
            {
                return this[logEvent];
            }

            if (this.ContainsKey(LogEvent.AllOther))
            {
                return this[LogEvent.AllOther];
            }

            return default(TConfig);
        }
    }
}
