using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    internal class SkillNotFoundException : Exception
    {
        public SkillNotFoundException()
        {
        }

        public SkillNotFoundException(string? message) : base(message)
        {
        }

        public SkillNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SkillNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}