using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class JobActivityNotFoundException : Exception
    {
        public JobActivityNotFoundException()
        {
        }

        public JobActivityNotFoundException(string? message) : base(message)
        {
        }

        public JobActivityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected JobActivityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}