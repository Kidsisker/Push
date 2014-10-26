using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

using Concord.Logging.Helpers;

namespace Concord.Logging.Models
{
    public class Client
    {
        [Column("Client_ID")]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string IP { get; set; }

        [StringLength(50)]
        public string OSVersion { get; set; }

        /// <summary>
        /// the user agent string from the request that triggered the log
        /// </summary>
        [Required]
        [StringLength(200)]
        public string UserAgent { get; set; }

        /// <summary>
        /// details about the browser used by the client
        /// </summary>
        public virtual Browser Browser { get; set; }


        public Client()
        {
            try
            {
                HttpRequest request = HttpContext.Current.Request;

                this.Name = request.UserHostName;
                this.OSVersion = UserAgentUtility.ParseOSFromUserAgent(request.UserAgent);
                this.UserAgent = request.UserAgent;
                this.IP = request.UserHostAddress;
                this.Browser = new Browser();

            }
            catch (Exception ex)
            {
            }

        }

    }
}
