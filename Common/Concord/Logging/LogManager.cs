using System;

namespace Concord.Logging
{
    /// <summary>
    /// manager class that handles all logging functionality
    /// </summary>
    public class LogManager
    {
        private static readonly Lazy<LogManager> _instance = new Lazy<LogManager>(() => new LogManager());

        private ILogger _logger;

        /// <summary>
        /// the current environment
        /// </summary>
        public Models.Environment CurrentEnvironment { get; set; }

        /// <summary>
        /// application settings
        /// </summary>
        public ApplicationDetails ApplicationSettings { get; set; }


        /// <summary>
        /// singleton instance
        /// </summary>
        public static LogManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }


        private LogManager()
        {
        }

        /// <summary>
        /// makes sure the Instance is live and has the proper ILogger implementation type
        /// </summary>
        /// <param name="logger">the logger instance to use when logging</param>
        public void Start(ILogger logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// logs the given xml to the database
        /// </summary>
        /// <param name="xml">the xml to log</param>
        /// <param name="isRequest">true if the XML represents a request message; false otherwise (default)</param>
        public void LogXml(string xml, bool isRequest = false)
        {
            _logger.LogXml(xml, isRequest);
        }

        /// <summary>
        /// logs a given exception to the database
        /// </summary>
        /// <param name="ex">the exception to log</param>
        public void Log(Exception ex)
        {
            _logger.Log(ex);

        }

        /// <summary>
        /// logs the given error message to the database as a ConcordException
        /// </summary>
        /// <param name="message">the error message to log</param>
        public void Log(string message)
        {
            _logger.Log(message);
        }

    }
}
