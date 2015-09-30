using System;
using System.Runtime.Serialization;

namespace Reference.Common.Exceptions
{
    [Serializable]
    public class TypeArgumentException<T> : ArgumentException
    {
        public Type InvalidType { get; private set; }
        public TypeArgumentException()
            : this("The type " + typeof(T) + " is invalid as a generic argument.")
        {
            InvalidType = typeof(T);
        }

        public TypeArgumentException(string message) : base(message)
        {
        }

        public TypeArgumentException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TypeArgumentException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}