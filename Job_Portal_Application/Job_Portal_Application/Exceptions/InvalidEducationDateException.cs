using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    public class InvalidEducationDateException : Exception
    {
        public InvalidEducationDateException()
        {
        }

        public InvalidEducationDateException(string? message) : base(message)
        {
        }

        public InvalidEducationDateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidEducationDateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}