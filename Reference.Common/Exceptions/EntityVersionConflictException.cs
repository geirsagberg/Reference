using System;

namespace Reference.Common.Exceptions
{
    public class EntityVersionConflictException : UserErrorException
    {
        public EntityVersionConflictException(Exception innerException) : base("Databasen er endret siden sist. Forsøk igjen.", innerException)
        {
        }

        public EntityVersionConflictException(string message, Exception inner, Enum errorCode = null) : base(message, inner, errorCode) {}
    }
}