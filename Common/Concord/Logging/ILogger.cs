using System;

namespace Concord.Logging
{
    public interface ILogger
    {
        ApplicationDetails ApplicationDetails { get; set; }

        void Log(Exception ex);

        void Log(string message);

        void LogXml(string xml, bool isRequest = false);
    }
}
