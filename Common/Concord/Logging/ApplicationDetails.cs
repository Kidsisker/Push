using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concord.Logging
{
    /// <summary>
    /// details about the running application
    /// </summary>
    public class ApplicationDetails
    {
        /// <summary>
        /// the environment associate with this application
        /// </summary>
        public EnvironmentType Environment { get; set; }
        
        /// <summary>
        /// the name of the application
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// the version of the application
        /// </summary>
        public string ApplicationVersion { get; set; }


    }
}
