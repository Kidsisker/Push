using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Concord.Logging.Models
{
    /// <summary>
    /// details about the request that triggered the log entry
    /// </summary>
    public class Request
    {
        [Column("Request_ID")]
        public int ID { get; set; }

        /// <summary>
        /// details about the client being used when the log was triggered
        /// </summary>
        public virtual Client Client { get; set; }

        /// <summary>
        /// details about the host server
        /// </summary>
        public virtual Host Host { get; set; }

        /// <summary>
        /// the application for the request
        /// </summary>
        public virtual Application Application { get; set; }

    }
}
