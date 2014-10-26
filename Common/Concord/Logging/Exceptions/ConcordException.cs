using System;

namespace Concord.Logging.Exceptions
{
    public class ConcordException : ApplicationException
    {
        public bool EnableLogging { get; set; }

        public bool EnableXmlLogging { get; set; }

        /// <summary>
        /// a friendly version of the error that can be used to report the problem
        /// to users
        /// </summary>
        public string FriendlyMessage { get; set; }

        public ConcordException(string message)
            : base(message)
        {
        }

        public ConcordException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
