using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Concord.Logging.Helpers
{
    internal static class UserAgentUtility
    {
        internal static string ParseOSFromUserAgent(string userAgent)
        {
            // TODO: just hardcoding something for now.  add switch later
            return "Win NT 6.1";
        }

    }
}
