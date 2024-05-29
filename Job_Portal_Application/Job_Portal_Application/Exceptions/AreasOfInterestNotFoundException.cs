using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class AreasOfInterestNotFoundException : Exception
    {
        public AreasOfInterestNotFoundException()
        {
        }

        public AreasOfInterestNotFoundException(string? message) : base(message)
        {
        }

        public AreasOfInterestNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AreasOfInterestNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}