using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class UserSkillsNotFoundException : Exception
    {
        public UserSkillsNotFoundException()
        {
        }

        public UserSkillsNotFoundException(string? message) : base(message)
        {
        }

        public UserSkillsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserSkillsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}