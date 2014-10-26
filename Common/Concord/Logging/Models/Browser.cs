using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace Concord.Logging.Models
{
    /// <summary>
    /// browser details
    /// </summary>
    public class Browser
    {
        [Column("Browser_ID")]
        public int ID { get; set; }

        /// <summary>
        /// browser name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// full version of the browser
        /// </summary>
        [StringLength(50)]
        public string FullVersion { get; set; }

        /// <summary>
        /// browser major version
        /// </summary>
        public int MajorVersion { get; set; }

        /// <summary>
        ///  browser minor version
        /// </summary>
        public double MinorVersion { get; set; }


        public Browser()
        {
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                string userAgent = request.UserAgent.ToLower();

                if (!String.IsNullOrEmpty(userAgent))
                {
                    if (userAgent.Contains("trident"))
                        this.Name = "Internet Explorer";
                    else if (userAgent.Contains("opr/"))
                        this.Name = "Opera";
                    else if (userAgent.Contains("chrome"))
                        this.Name = "Chrome";
                    else if (userAgent.Contains("firefox"))
                        this.Name = "Firefox";
                    else if (userAgent.Contains("safari"))  // this one must go after chrome
                        this.Name = "Safari";

                }

                // figure out version too
                this.FullVersion = request.Browser.Version;
                this.MajorVersion = request.Browser.MajorVersion;
                this.MinorVersion = request.Browser.MinorVersion;
            }
            catch (Exception ex)
            {
                //TODO: do something, he's getting away!
            }
        }
    }
}
