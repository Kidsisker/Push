using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concord.Logging.Exceptions
{
    public static class ExceptionExtensions
    {

        public static void Log(this Exception ex)
        {
            LogManager.Instance.Log(ex);
        }
    }
}
