using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class TitleAlreadyExisitException : Exception
    {
        public TitleAlreadyExisitException()
        {
        }

        public TitleAlreadyExisitException(string? message) : base(message)
        {
        }

        public TitleAlreadyExisitException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TitleAlreadyExisitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}