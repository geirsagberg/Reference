using System;
using System.Runtime.Serialization;

namespace Reference.Common.Exceptions
{
    /// <summary>
    /// An exception that should be safe to show to the user.
    /// </summary>
    [Serializable]
    public class UserErrorException : Exception
    {
        public UserErrorException() {}

        public UserErrorException(string message, Enum errorCode = null)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public UserErrorException(string message, Exception inner, Enum errorCode = null)
            : base(message, inner)
        {
            ErrorCode = errorCode;
        }

        protected UserErrorException(SerializationInfo info, StreamingContext context, Enum errorCode = null)
            : base(info, context)
        {
            ErrorCode = errorCode;
        }

        public Enum ErrorCode { get; set; }
    }
}