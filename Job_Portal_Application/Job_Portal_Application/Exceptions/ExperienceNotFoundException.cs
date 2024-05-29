using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class ExperienceNotFoundException : Exception
    {
        public ExperienceNotFoundException()
        {
        }

        public ExperienceNotFoundException(string? message) : base(message)
        {
        }

        public ExperienceNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ExperienceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}