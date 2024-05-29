using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class SkillAlreadyExisitException : Exception
    {
        public SkillAlreadyExisitException()
        {
        }

        public SkillAlreadyExisitException(string? message) : base(message)
        {
        }

        public SkillAlreadyExisitException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SkillAlreadyExisitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}