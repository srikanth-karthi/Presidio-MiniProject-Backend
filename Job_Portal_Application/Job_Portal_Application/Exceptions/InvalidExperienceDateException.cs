using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    public class InvalidExperienceDateException : Exception
    {
        public InvalidExperienceDateException()
        {
        }

        public InvalidExperienceDateException(string? message) : base(message)
        {
        }

        public InvalidExperienceDateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidExperienceDateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}