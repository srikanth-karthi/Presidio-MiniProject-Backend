using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class JobSkillsNotFoundException : Exception
    {
        public JobSkillsNotFoundException()
        {
        }

        public JobSkillsNotFoundException(string? message) : base(message)
        {
        }

        public JobSkillsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected JobSkillsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}