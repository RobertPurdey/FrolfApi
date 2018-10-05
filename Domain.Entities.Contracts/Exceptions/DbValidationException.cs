using System;

namespace Domain.Entities.Contracts.Exceptions
{
    public class DbValidationException : ApplicationException
    {
        public DbValidationException(string message)
            : base(message)
        {

        }

        public DbValidationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
