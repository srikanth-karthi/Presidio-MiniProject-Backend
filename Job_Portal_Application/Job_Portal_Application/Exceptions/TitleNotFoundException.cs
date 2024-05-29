using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public  class TitleNotFoundException : Exception
    {
        public TitleNotFoundException()
        {
        }

        public TitleNotFoundException(string? message) : base(message)
        {
        }

        public TitleNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TitleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}