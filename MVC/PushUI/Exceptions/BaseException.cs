using System;
using Concord.Logging.Exceptions;   

namespace PushUI.Exceptions
{
    public class BaseException : ConcordException
    {


        public BaseException(string message)
            : base(message)
        {
        }

        public BaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}