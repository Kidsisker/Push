using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Elmah;

namespace Concord.Logging.Loggers
{
    public class ElmahLogger : ILogger
    {
        public ApplicationDetails ApplicationDetails { get; set; }


        public void Log(Exception ex)
        {
            try
            {
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(ex));
            }
            catch (Exception e)
            {
                // logging failed, what the heck do we do
            }
        }

        public void Log(string message)
        {
            this.Log(new Exception(message));
        }

        public void LogXml(string xml, bool isRequest = false)
        {
            // do nothing
        }

    }
}
