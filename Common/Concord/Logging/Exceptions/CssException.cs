using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Concord.Logging.Exceptions
{
    /// <summary>
    /// generic CSS exception class
    /// </summary>
    public class CssException : ConcordException
    {
        /// <summary>
        /// the action that was requested when the error occurred
        /// </summary>
        public string Action { get; set; }

        public Hashtable Parameters { get; set; }

        public CssException(string message)
            : base(message)
        {
            this.Parameters = new Hashtable();
        }

        public CssException(string action, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Action = action;
        }

        public CssException(string action, Hashtable parameters, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Parameters = parameters;
            this.Action = action;
        }

    }
}
