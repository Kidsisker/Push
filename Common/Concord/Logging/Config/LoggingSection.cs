using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Concord.Logging.Config
{
    public class LoggingSection : ConfigurationSection
    {
        [ConfigurationProperty("logger", DefaultValue = "Logging.Loggers.ElmahLogger", IsRequired = true)]
        public string Logger
        {
            get { return (string)this["logger"]; }
            set { this["logger"] = value; }
        }

        [ConfigurationProperty("level", DefaultValue = LogLevel.Error, IsRequired = true)]
        public LogLevel Level
        {
            get
            {
                LogLevel result = LogLevel.All;
                string level = (string)this["level"];
                
                LogLevel tmp = LogLevel.All;
                if (Enum.TryParse(level, true, out tmp)
                    && Enum.IsDefined(typeof(LogLevel), tmp))
                    result = tmp;

                return result;
            }
            set
            {
                this["level"] = value;
            }
        }
    }


    public enum LogLevel
    {
        All,
        Info,
        Warning,
        Error,
        Fatal,
        None
    }
}
