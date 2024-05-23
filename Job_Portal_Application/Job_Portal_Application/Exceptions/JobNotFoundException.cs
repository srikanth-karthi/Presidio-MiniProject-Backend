using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    internal class JobNotFoundException : Exception
    {
        public JobNotFoundException()
        {
        }

        public JobNotFoundException(string? message) : base(message)
        {
        }

        public JobNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected JobNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}