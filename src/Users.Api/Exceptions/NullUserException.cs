using System;

namespace Users.Api.Exceptions
{
    public class NullUserException : Exception
    {
        public NullUserException()
        {
        }

        public NullUserException(string message) : base(message)
        {
        }
    }
}