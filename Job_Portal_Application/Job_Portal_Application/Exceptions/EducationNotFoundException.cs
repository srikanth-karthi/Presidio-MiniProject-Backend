using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class EducationNotFoundException : Exception
    {
        public EducationNotFoundException()
        {
        }

        public EducationNotFoundException(string? message) : base(message)
        {
        }

        public EducationNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EducationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}