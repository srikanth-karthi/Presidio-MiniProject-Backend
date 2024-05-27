using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    internal class JobDisabledException : Exception
    {
        public JobDisabledException()
        {
        }

        public JobDisabledException(string? message) : base(message)
        {
        }

        public JobDisabledException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected JobDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}