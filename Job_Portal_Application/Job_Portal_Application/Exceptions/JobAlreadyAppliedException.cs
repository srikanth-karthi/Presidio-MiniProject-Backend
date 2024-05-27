﻿using System.Runtime.Serialization;

namespace Job_Portal_Application.Exceptions
{
    [Serializable]
    internal class JobAlreadyAppliedException : Exception
    {
        public JobAlreadyAppliedException()
        {
        }

        public JobAlreadyAppliedException(string? message) : base(message)
        {
        }

        public JobAlreadyAppliedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected JobAlreadyAppliedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}