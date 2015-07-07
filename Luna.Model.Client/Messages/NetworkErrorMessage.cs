using System;

namespace Luna.Model.Messages
{
    public class NetworkErrorMessage
    {
        public Exception Error { get; private set; }

        public NetworkErrorMessage(Exception exception)
        {
            Error = exception;
        }
    }
}