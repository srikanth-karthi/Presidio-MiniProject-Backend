using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class CompanyNotFoundException : Exception
    {
        public CompanyNotFoundException()
        {
        }

        public CompanyNotFoundException(string? message) : base(message)
        {
        }

        public CompanyNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CompanyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}