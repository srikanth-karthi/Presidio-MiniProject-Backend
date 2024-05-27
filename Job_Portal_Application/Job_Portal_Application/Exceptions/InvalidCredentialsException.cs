using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    internal class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
        {
        }

        public InvalidCredentialsException(string? message) : base(message)
        {
        }

        public InvalidCredentialsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}